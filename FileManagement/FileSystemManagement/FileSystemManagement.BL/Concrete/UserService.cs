using FileSystemManagement.BL.Validation;
using FileSystemManagement.Core.DTO;
using FileSystemManagement.Core.Models;
using FileSystemManagement.DAL.Abstract;
using FileSystemManagement.DAL.Repositories;
using FluentValidation;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TCKimlik;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace FileSystemManagement.BL
{
    public class UserService : IUserManager
    {
        private readonly IUser _user;
        private UserValidator userValidator;
        private RegisterValidator registerValidator;
        private EmailValidator emailValidator;
        private ConfirmPasswordValidator confirmValidator;
        public UserService()
        {
            _user = new UserRepository();
            userValidator = new UserValidator();
            registerValidator = new RegisterValidator();
            emailValidator = new EmailValidator();
            confirmValidator = new ConfirmPasswordValidator();
        }

        public ServiceResult<LoginResponseDTO> LoginUser(LoginRequestDTO loginRequest)
        {
            ServiceResult<LoginResponseDTO> loginMessage = new ServiceResult<LoginResponseDTO>();

            var result = userValidator.Validate(loginRequest);
            if(!result.IsValid)
            {
                foreach(var failure in result.Errors)
                {
                    loginMessage = new ServiceResult<LoginResponseDTO>()
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage,
                        Data = new LoginResponseDTO()
                        {

                        }
                    };
                }
              
            }
            else
            {
                var user = _user.Login(loginRequest);


                if (user == null)
                {
                    foreach(var failure in result.Errors)
                    {
                        loginMessage = new ServiceResult<LoginResponseDTO>()
                        {
                            StatusCode = 400,
                            IsSuccess = false,
                            Message = failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage,
                            Data = new LoginResponseDTO()
                            {

                            }
                        };
                    }
                    
                }
                else
                {
                    loginMessage = new ServiceResult<LoginResponseDTO>()
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Operation Succesful",
                        Data = new LoginResponseDTO()
                        {
                            Id = user.UserId,
                            Username = user.Username,


                        }
                    };
                }
            }
  
            return loginMessage;

        }

        public ServiceResult Register(RegisterRequestDTO registerRequest)
        {
            ServiceResult registerMessage = new ServiceResult();

            var result = registerValidator.Validate(registerRequest);
            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    registerMessage = new ServiceResult()
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage
                     
                    };
                }

            }
            else
            {
                var usernameControl = _user.Username(registerRequest.Username);
                if (usernameControl != null)
                {
                    registerMessage = new ServiceResult()
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Kullanıcı adı alınmış",

                    };
                }
                else
                {

                    BasicHttpsBinding binding = new BasicHttpsBinding();
                    EndpointAddress address = new EndpointAddress("https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx");

                    long Tc = (long)registerRequest.TC;
                    string name = registerRequest.Name.ToUpper();
                    string surname = registerRequest.Surname.ToUpper();
                    int birthday = registerRequest.Birthday.Year;

                    TCKimlik.KPSPublicSoapClient kPSPublicSoap = new KPSPublicSoapClient(binding, address);
                    bool status = kPSPublicSoap.TCKimlikNoDogrula(Tc, name, surname, birthday);

                    if (!status)
                    {
                        registerMessage = new ServiceResult()
                        {
                            StatusCode = 400,
                            IsSuccess = false,
                            Message = "Kimlik bilgileri yanlış",
                        };
                    }
                    else
                    {
                        var user = _user.Register(registerRequest);

                        if (user == null)
                        {
                            foreach (var failure in result.Errors)
                            {
                                registerMessage = new ServiceResult()
                                {
                                    StatusCode = 400,
                                    IsSuccess = false,
                                    Message = failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage,

                                };
                            }

                        }
                        else
                        {
                            registerMessage = new ServiceResult()
                            {
                                StatusCode = 200,
                                IsSuccess = true,
                                Message = "Operation Succesful",

                            };
                        }
                    }               
                }                              
                }
            return registerMessage;
        }


        public ServiceResult ForgotPassword(ResetPasswordRequestDTO passwordRequestDTO)
        {
            ServiceResult serviceResult = new ServiceResult();

            var result = emailValidator.Validate(passwordRequestDTO);
            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    serviceResult = new ServiceResult()
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage

                    };
                }
            }
            else
            {
                var emailControl = _user.Email(passwordRequestDTO.Email);
                if (emailControl != null)
                {
                    serviceResult = new ServiceResult()
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Mail gönderildi. Mail hesabınızı kontrol edin.",
                    };

                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse("Eda"));
                    email.To.Add(MailboxAddress.Parse(emailControl.Email));
                    email.Subject = "Şifre Sıfırlama";

                    Random random = new Random();
                    int number = random.Next(100000, 999999);


                    var codeConfirm = _user.CreateCode(number, emailControl.Email);
                    if(codeConfirm != null)
                    {
                        string url = $"http://localhost:54057/ChangePassword?ActivationCode={number}";

                        email.Body = new TextPart(TextFormat.Html) { Text = "<h3>Şifrenizi sıfırlamak için linke tıklayınız:</h3>" + $"<a href= {url}>Link</a>" };


                        var smtp = new MailKit.Net.Smtp.SmtpClient();
                        smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                        smtp.Authenticate("daviiddjohnsonn@gmail.com", "david123johnson");
                        smtp.Send(email);
                    }
                    else
                    {
                        serviceResult = new ServiceResult()
                        {
                            StatusCode = 400,
                            IsSuccess = false,
                            Message = "Hatalı",
                        };
                    }

                }
            }
            return serviceResult;
        }


        public ServiceResult<IdResponseDTO> CodeControl(int code)
        {
            ServiceResult<IdResponseDTO> serviceResult = new ServiceResult<IdResponseDTO>();
            var codeControl = _user.ConfirmCode(code);
            if (codeControl != null)
            {
                serviceResult = new ServiceResult<IdResponseDTO>()
                {
                    IsSuccess = true,
                    Message = "başarılı",
                    StatusCode = 200,
                    Data = new IdResponseDTO()
                    {
                        Id = codeControl.UserId
                    }
                };
            }
            else
            {
                serviceResult = new ServiceResult<IdResponseDTO>()
                {
                    IsSuccess = false,
                    Message = "geçersiz kod",
                    StatusCode = 400,
                    Data = new IdResponseDTO()
                    {

                    }
                };
            }

            return serviceResult;
           
        }


        public ServiceResult ChangePassword(ConfirmPasswordRequestDTO confirmPasswordRequest)
        {
            ServiceResult serviceResult = new ServiceResult();
            var result = confirmValidator.Validate(confirmPasswordRequest);
            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    serviceResult = new ServiceResult()
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage

                    };
                }
            }
            else
            {
                var changePassword = _user.UpdatePassword(confirmPasswordRequest.ActivationCode,confirmPasswordRequest.Password);
                if (changePassword == null)
                {
                    serviceResult = new ServiceResult()
                    {
                        IsSuccess = false,
                        Message = "Şifre değiştirme başarısız",
                        StatusCode = 400
                    };
                }
                else
                {
                    serviceResult = new ServiceResult()
                    {
                        IsSuccess = true,
                        Message = "Şifre değiştirme başarılı",
                        StatusCode = 200
                    };
                }
            }
            return serviceResult;
        }



        public ServiceResult ConfirmCode(ActivationCodeRequestDTO activationCodeRequestDTO)
        {
            ServiceResult serviceResult = new ServiceResult();
            var activationCode = _user.ConfirmCode(activationCodeRequestDTO.ActivationCode);
            if (activationCode != null)
            {
                serviceResult = new ServiceResult()
                {
                    IsSuccess = true,
                    Message = "Doğru",
                    StatusCode = 200
                };

            }
            else
            {
                serviceResult = new ServiceResult()
                {
                    IsSuccess = false,
                    Message = "Hatalı Kod",
                    StatusCode = 400
                };
            }

            return serviceResult;
           
        }

    }
}

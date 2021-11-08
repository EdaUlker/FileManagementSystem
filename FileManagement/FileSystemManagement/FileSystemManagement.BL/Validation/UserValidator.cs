using FileSystemManagement.Core.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileSystemManagement.BL.Validation
{
    public class UserValidator : AbstractValidator<LoginRequestDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username).MinimumLength(5).WithMessage("Kullanıcı adı en az 5 karakter olmalıdır.").MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olmalıdır.").NotEmpty();


            RuleFor(x => x.Password).MinimumLength(3).WithMessage("Şifre en az 3 karakter olmalıdır.").MaximumLength(50).WithMessage("Şifre en fazla 50 karakter olmalıdır.").NotEmpty();

            //Must(IsPasswordValid).WithMessage("Şifre en az 1 harf ve 1 sayı içermelidir.");        
            //RuleFor(x => x.Name).MinimumLength(3).WithMessage("İsim en az 3 karakter olamlıdır.").MaximumLength(20).WithMessage("İsim en fazla 20 karakter olmalıdır.");

            //RuleFor(x => x.Surname).MinimumLength(3).WithMessage("Soyisim en az 3 karakter olmalıdır.").MaximumLength(25).WithMessage("Soyisim en fazla 25 karakter olmalıdır.");
            //RuleFor(x => x.Birthday).LessThanOrEqualTo(x => DateTime.Now).WithMessage("Geçersiz doğum tarihi.");
        }

        private bool IsPasswordValid(string password)
        {
            Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]");
            return regex.IsMatch(password);
        }


    }

    public class RegisterValidator : AbstractValidator<RegisterRequestDTO>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username).MinimumLength(5).WithMessage("Kullanıcı adı en az 5 karakter olmalıdır.").MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olmalıdır.").NotEmpty();


            RuleFor(x => x.Password).MinimumLength(3).WithMessage("Şifre en az 3 karakter olmalıdır.").MaximumLength(50).WithMessage("Şifre en fazla 50 karakter olmalıdır.").NotEmpty();

            //Must(IsPasswordValid).WithMessage("Şifre en az 1 harf ve 1 sayı içermelidir.");        
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("İsim en az 3 karakter olamlıdır.").MaximumLength(20).WithMessage("İsim en fazla 20 karakter olmalıdır.");

            RuleFor(x => x.Surname).MinimumLength(3).WithMessage("Soyisim en az 3 karakter olmalıdır.").MaximumLength(25).WithMessage("Soyisim en fazla 25 karakter olmalıdır.");
            RuleFor(x => x.Birthday).LessThanOrEqualTo(x => DateTime.Now).WithMessage("Geçersiz doğum tarihi.");
        }


    }


    public class EmailValidator : AbstractValidator<ResetPasswordRequestDTO>
    {
        public EmailValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("Mail adresi boş geçilemez.");
        }
    }




    public class ConfirmPasswordValidator : AbstractValidator<ConfirmPasswordRequestDTO>
    {
        public ConfirmPasswordValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş geçilemez.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre boş geçilemez.").Equal(x=>x.Password).WithMessage("Şifreler aynı olmalıdır.");
            RuleFor(x => x.ActivationCode).NotEmpty().WithMessage("Aktivasyon kodu boş geçilemez.");
        }
    }








}

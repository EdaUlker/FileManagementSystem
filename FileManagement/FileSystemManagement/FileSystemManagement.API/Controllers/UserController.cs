using FileSystemManagement.BL;
using FileSystemManagement.BL.Validation;
using FileSystemManagement.Core.DTO;
using FileSystemManagement.Core.Models;
using FileSystemManagement.DAL.Abstract;
using FileSystemManagement.DAL.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;
       
        IUserManager userService;


        public UserController(IConfiguration config)
        {
          
            _config = config;
            userService = new UserService();
        }


        private string GenerateJSONWebToken(LoginResponseDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost]
        public ServiceResult<LoginResponseDTO> Login([FromBody]LoginRequestDTO user)
        {
            ServiceResult<LoginResponseDTO> loginMessage = new ServiceResult<LoginResponseDTO>();
           
           
            loginMessage = userService.LoginUser(user);
            if (loginMessage.IsSuccess)
            {
                loginMessage.Data.Token = GenerateJSONWebToken(loginMessage.Data);
            }
            return loginMessage;
        }

        [HttpPost]
        public ServiceResult Register(RegisterRequestDTO registerDTO)
        {
            ServiceResult serviceResult = new ServiceResult();

            serviceResult = userService.Register(registerDTO);

            if (serviceResult.IsSuccess)
            {
                return serviceResult;
            }

            return serviceResult;
        }

        [HttpPost]
        public ServiceResult ForgotPassword(ResetPasswordRequestDTO passwordRequestDTO)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult = userService.ForgotPassword(passwordRequestDTO);
            if (serviceResult.IsSuccess)
            {
                return serviceResult;

            }
            return serviceResult;
        }


        [HttpGet]
        public ServiceResult<IdResponseDTO> ChangePassword(int activationCode)
        {
            ServiceResult<IdResponseDTO> serviceResult = new ServiceResult<IdResponseDTO>();
            serviceResult = userService.CodeControl(activationCode);
            if (serviceResult.IsSuccess)
            {
                return serviceResult;
            }
            return serviceResult;
        }




        public ServiceResult ConfirmCode(ActivationCodeRequestDTO activationCodeRequestDTO)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult = userService.ConfirmCode(activationCodeRequestDTO);
            if (serviceResult.IsSuccess)
            {
                return serviceResult;

            }
            return serviceResult;
        }


        [HttpPost]
        public ServiceResult ChangePassword(ConfirmPasswordRequestDTO confirmPasswordRequest)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult = userService.ChangePassword(confirmPasswordRequest);
            if (serviceResult.IsSuccess)
            {
                return serviceResult;
            }
            return serviceResult;
        }
    }
}

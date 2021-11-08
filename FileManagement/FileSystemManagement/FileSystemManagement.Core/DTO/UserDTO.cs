using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemManagement.Core.DTO
{
    public class UserDTO
    {
        public string Username { get; set; }

    }

    public class LoginRequestDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class RegisterRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public double TC { get; set; }
        public DateTime Birthday { get; set; }
    }


    public class IdResponseDTO
    {
        public int Id { get; set; }
    }


    public class LoginResponseDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

    }

    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public T Data { get; set; }
    }

    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    
    }


    public class ResetPasswordRequestDTO
    {
        public string Email { get; set; }
    }


    public class ActivationCodeRequestDTO
    {
        public int ActivationCode { get; set; }
    }


    public class ConfirmPasswordRequestDTO
    {
        public int ActivationCode { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}

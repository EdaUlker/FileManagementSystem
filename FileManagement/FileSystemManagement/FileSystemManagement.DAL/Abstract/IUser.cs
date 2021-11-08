using FileSystemManagement.Core.DTO;
using FileSystemManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemManagement.DAL.Abstract
{
    public interface IUser
    {
        TblUser Login(LoginRequestDTO user);

        TblUser Register(RegisterRequestDTO registerDTO);

        TblUser Username(string username);

        TblUser Email(string email);

        TblUser UpdatePassword(int code, string password);

        //TblUser ChangePassword(int activationCode);

        TblUser CreateCode(int code,string email);

        TblUser ConfirmCode(int code);

        //TblUser GetMethodAC(ActivationCodeRequestDTO activationCodeRequestDTO);


    }
}

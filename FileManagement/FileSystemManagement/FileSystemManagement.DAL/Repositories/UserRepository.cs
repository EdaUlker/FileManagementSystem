using FileSystemManagement.Core.DTO;
using FileSystemManagement.Core.Models;
using FileSystemManagement.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemManagement.DAL.Repositories
{
    public class UserRepository : IUser
    {
        FileManagementContext context = new FileManagementContext();
        public TblUser Login(LoginRequestDTO user)
        {
            var info = context.TblUsers.FirstOrDefault(x => x.Password == user.Password && x.Username == user.Username);
            return info;
        }

        public TblUser Register(RegisterRequestDTO registerDTO)
        {
            TblUser tblUser = new TblUser();
            tblUser.Name = registerDTO.Name;
            tblUser.Surname = registerDTO.Surname;
            tblUser.Tc = (long?)registerDTO.TC;
            tblUser.Username = registerDTO.Username;
            tblUser.Password = registerDTO.Password;
            tblUser.Birthday = registerDTO.Birthday;
            tblUser.Email = registerDTO.Email;
            context.TblUsers.Add(tblUser);
            context.SaveChanges();
            return tblUser;
        }

        public TblUser Username(string username)
        {
            var info = context.TblUsers.FirstOrDefault(x => x.Username == username);
            return info;
        }

        public TblUser Email(string email)
        {
            var info = context.TblUsers.FirstOrDefault(x => x.Email == email);
            return info;
        }


        public TblUser CreateCode(int code, string email)
        {
            var info = context.TblUsers.FirstOrDefault(x => x.Email == email);
            var codecontrol = context.TblUsers.FirstOrDefault(x => x.Code == code);
            if(codecontrol == null)
            {
                info.Code = code;
                context.SaveChanges();
            }
            return info;
        }



        public TblUser ConfirmCode(int code)
        {
            var info = context.TblUsers.FirstOrDefault(x => x.Code == code);
            return info;
        }


        public TblUser UpdatePassword(int code,string password)
        {
            var info = context.TblUsers.FirstOrDefault(x => x.Code == code);
            if (info != null)
            {
                info.Password = password;
                context.SaveChanges();
            }
           
            return info;
        }
    }
}

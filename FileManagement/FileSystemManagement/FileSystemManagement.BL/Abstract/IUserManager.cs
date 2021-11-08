using FileSystemManagement.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemManagement.BL
{
    public interface IUserManager
    {
        ServiceResult<LoginResponseDTO> LoginUser(LoginRequestDTO loginRequest);

        ServiceResult Register(RegisterRequestDTO registerRequest);

        ServiceResult ForgotPassword(ResetPasswordRequestDTO resetPasswordRequestDTO);

        ServiceResult ChangePassword(ConfirmPasswordRequestDTO confirmPasswordRequest);

        ServiceResult ConfirmCode(ActivationCodeRequestDTO activationCode);
        ServiceResult<IdResponseDTO> CodeControl(int code);

        

    }
}

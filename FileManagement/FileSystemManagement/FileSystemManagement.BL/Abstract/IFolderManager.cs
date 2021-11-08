using FileSystemManagement.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static FileSystemManagement.Core.DTO.FolderDTO;

namespace FileSystemManagement.BL
{
    public interface IFolderManager
    {
        List<FolderResponseDTO> ListAll(FolderRequestDTO folderRequest, int userid);

        FolderResponseDTO GetListById(int id);

        ServiceResult CreateFolder(FolderRequestDTO folderRequest,int userid);

        ServiceResult DeleteFolder(FolderRequestDTO folderRequest);

        FolderResponseDTO UpdateFolder(FolderRequestDTO folderRequest);

        ServiceResult UploadFile(FolderUpload folderUpload);

    }
}

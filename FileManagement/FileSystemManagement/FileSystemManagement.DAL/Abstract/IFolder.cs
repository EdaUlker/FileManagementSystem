 using FileSystemManagement.Core.DTO;
using FileSystemManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FileSystemManagement.Core.DTO.FolderDTO;

namespace FileSystemManagement.DAL.Abstract
{
    public interface IFolder
    {
        List<TblFolder>GetAll(FolderRequestDTO folder,int userid);
        TblFolder Update(FolderRequestDTO folder);
        TblFolder Delete(FolderRequestDTO folder);
        TblFolder Add(FolderRequestDTO folder, int userid);
        TblFolder GetOne(int folderId);

        TblFolder FolderName(string filename, int userid);

        TblFolder UploadFile(FolderUpload folderUpload, int userid);

        int FileName(string filename, int userid);


    }
}

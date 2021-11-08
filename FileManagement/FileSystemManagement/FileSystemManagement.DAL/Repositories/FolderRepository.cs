using FileSystemManagement.Core.DTO;
using FileSystemManagement.Core.Models;
using FileSystemManagement.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace FileSystemManagement.DAL.Repositories
{
    public class FolderRepository : IFolder
    {
        public FileManagementContext _context;


        public FolderRepository()
        {
            _context = new FileManagementContext();
        }

        public TblFolder Add(FolderRequestDTO folder, int userid)
        {
            TblFolder tblFolder = new TblFolder();
            tblFolder.CreationDate = DateTime.Now;
            tblFolder.FileName = folder.Name;
            tblFolder.UserId = userid;
            tblFolder.ParentId = folder.ParentId;
            tblFolder.FolderSize = 0;
            tblFolder.Status = 1;
            _context.Add(tblFolder);
            _context.SaveChanges();
            return tblFolder;
        }

        public TblFolder Delete(FolderRequestDTO folderRequest)
        {
            TblFolder tblFolder = new TblFolder();
            tblFolder.FolderId = folderRequest.FolderId;
            tblFolder.Status = folderRequest.Status;   
            _context.TblFolders.Update(tblFolder);
            _context.SaveChanges();
            return tblFolder;
        }

        public List<TblFolder> GetAll(FolderRequestDTO folder,int userid)
        {
           
            return _context.TblFolders.Where(x => x.ParentId == folder.ParentId && x.UserId == userid).ToList();   
        }

        public TblFolder GetOne(int folderId)
        {
            var folder = _context.TblFolders.FirstOrDefault(x => x.FolderId == folderId);
            return folder;
        }

        public TblFolder Update(FolderRequestDTO folder)
        {
            TblFolder tblFolder = GetOne(folder.FolderId);
            if (folder.FolderId!=null)
            {
              
                tblFolder.Status = folder.Status;
            }
            _context.SaveChanges();
            return tblFolder;
        }

        public TblFolder UploadFile(FolderUpload folderUpload)
        {
            TblFolder tblFolder = GetOne(folderUpload.FolderId);
            if (folderUpload.FolderId != null)
            {

                

            }
            _context.SaveChanges();
            return tblFolder;
        }


       

       
    }
}

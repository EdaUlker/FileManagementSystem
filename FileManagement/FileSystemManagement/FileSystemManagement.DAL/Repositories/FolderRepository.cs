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
using System.Reflection;

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
            tblFolder.Type = "folder";
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


        public TblFolder FolderName(string filename, int userid)
        {
            var info = _context.TblFolders.FirstOrDefault(x => x.FileName == filename && x.UserId == userid && x.Status == 1);
            return info;
        }

        public TblFolder CheckFileName(string filename,int userid)
        {
            var info = _context.TblFolders.FirstOrDefault(x => x.FileName == filename && x.UserId == userid && x.Type == "file" && x.Status == 1);
            return info;
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
                if(folder.Name != null)
                {
                    tblFolder.FileName = folder.Name;
                }         
            }
            _context.SaveChanges();
            return tblFolder;
        }

        public TblFolder UploadFile(FolderUpload folderUpload, int userid)
        {
            TblFolder tblFolder = new TblFolder();
            if (folderUpload.FolderId != null)
            {
                tblFolder.CreationDate = DateTime.Now;
                tblFolder.FileName = folderUpload.FileName;
                tblFolder.UserId = userid;
                tblFolder.ParentId = folderUpload.FolderId;
                tblFolder.FolderSize = (int?)folderUpload.FormFile.Length;
                tblFolder.Status = 1;
                tblFolder.Type = "file";
                _context.Add(tblFolder);
                

            }
            _context.SaveChanges();
            return tblFolder;
        }


       

       
    }
}

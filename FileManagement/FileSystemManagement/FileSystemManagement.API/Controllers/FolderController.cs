using FileSystemManagement.BL;
using FileSystemManagement.Core.DTO;
using FileSystemManagement.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileSystemManagement.API.Controllers
{


    [Route("[controller]/[action]")]
    [ApiController]

  
    public class FolderController : ControllerBase
    {
        IFolderManager folderService;
   
        public FolderController()
        {
            folderService = new FolderService();
        }

        //[HttpPost]
        //[Authorize]
        //public FolderResponseDTO CreateFolder(FolderRequestDTO folder)
        //{
        //    var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        //    var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
        //    //folder.UserId = Convert.ToInt32(userId);
        //    var folderDTO = folderService.CreateFolder(folder, username);
        //    var folderid = folderDTO.FolderId;
        //    var path = GetParentFileName(folderid);
        //    var fullPath = "C:\\FileManagement\\" + username + "\\" + path;
        //    string path2 = Path.Combine(fullPath + folder.Name);
        //    Directory.CreateDirectory(path2);



        //    return folderDTO;

        //}


        [HttpPost]
        [Authorize]
        public ServiceResult CreateFolder(FolderRequestDTO folder)
        {
            ServiceResult serviceResult = new ServiceResult();
            var userid = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            serviceResult = folderService.CreateFolder(folder,Convert.ToInt32(userid));
            if (serviceResult.IsSuccess)
            {
                var path = GetParentFileName((int)folder.ParentId);
                var fullPath = "C:\\FileManagement\\" + username + "\\" + path;
                string path2 = Path.Combine(fullPath + folder.Name);
                Directory.CreateDirectory(path2);
            }
            return serviceResult;
        }


        [HttpPost]
        [Authorize]
        public List<FolderResponseDTO> ListFolder([FromBody] FolderRequestDTO folder)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var folderDTO = folderService.ListAll(folder,Convert.ToInt32(userId));
            return folderDTO;

 
        }


        [HttpPost]
        [Authorize]
        public ServiceResult UploadFile([FromForm] FolderUpload folderUpload)
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            ServiceResult serviceResult = new ServiceResult();
            serviceResult = folderService.UploadFile(folderUpload);
            if (serviceResult.IsSuccess)
            {
                string name = folderUpload.FileName;
                var image = folderUpload.FormFile;
                var folderid = folderUpload.FolderId;
                var path = GetParentFileName(folderid);
                var fullPath = "C:\\FileManagement\\" + username + "\\" + path;

                if (image.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(fullPath + folderUpload.FormFile.FileName), FileMode.Create, FileAccess.Write))
                    {
                        image.CopyTo(fileStream);
                    }
                }
            }
           
            return serviceResult;
        }




        private string GetParentFileName(int id)
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var folder = folderService.GetListById(id);
            string path = folder.FileName + "\\";
            while (folder.ParentId != null)
            {
                folder = folderService.GetListById((int)folder.ParentId);
                path = folder.FileName + "\\" + path;
            }
            return path;
        }



        [HttpPost]
        [Authorize]
        public FolderResponseDTO UpdateFolder(FolderRequestDTO folderRequest)
        {
            //var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            //folderRequest.UserId = Convert.ToInt32(userId);
            
            var folderDTO = folderService.UpdateFolder(folderRequest);
            return folderDTO;
        }
    }
}

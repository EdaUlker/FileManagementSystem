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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
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


        [HttpPost]
        [Authorize]
        public ServiceResult CreateFolder(FolderRequestDTO folder)
        {
            ServiceResult serviceResult = new ServiceResult();
            var userid = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            serviceResult = folderService.CreateFolder(folder, Convert.ToInt32(userid));
            if (serviceResult.IsSuccess)
            {
                var path = GetParentFileName((int?)folder.ParentId);
                var fullPath = "C:\\FileManagement\\" + username + "\\" + path;
                string path2 = Path.Combine(fullPath + folder.Name);
                Directory.CreateDirectory(path2);
            }
            return serviceResult;
        }





        [HttpGet("{folderid}")]
        [Authorize]
        public IActionResult DownloadFile(int folderid)
        {
            var userid = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var fileName = GetFileNameAndParentId(folderid).FileName;
            int parentId = (int)GetFileNameAndParentId(folderid).ParentId;
            var path = GetParentFileName(parentId);
            var fullPath = "C:\\FileManagement\\" + username + path;
            string path2 = Path.Combine(fullPath + fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path2);
            return File(fileBytes, "application/octet-stream", fileName);
        }









        //[HttpGet]
        //[Authorize]
        //public ServiceResult Download(string filename, int folderId, int parentId)
        //{
        //    var request = Request.ContentLength; 
        //    ServiceResult serviceResult = new ServiceResult();
        //    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //    var userid = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        //    var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

        //    var path = GetParentFileName(parentId);
        //    var fullPath = "C:\\FileManagement\\" + username + path;
        //    string path2 = Path.Combine(fullPath + filename);

        //    serviceResult = folderService.DownloadFile(filename, folderId, parentId, Convert.ToInt32(userid));


        //    if (serviceResult.IsSuccess)
        //    {
        //        var stream = new FileStream(path2, FileMode.Open, FileAccess.Read);
        //        result.Content = new StreamContent(stream);
        //        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //        {
        //            FileName = filename

        //        };
        //        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //        stream.Close();

        //    }

        //    return serviceResult;
        //}


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
            var userid = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            ServiceResult serviceResult = new ServiceResult();
            serviceResult = folderService.UploadFile(folderUpload, Convert.ToInt32(userid));
            var abc = Request.Form.Files;
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



        private string GetParentFileName(int? id)
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var folder = folderService.GetListById((int)id);
            string path = folder.FileName + "\\";
            if (folder.ParentId != null)
            {
                folder = folderService.GetListById((int)folder.ParentId);
                path = folder.FileName + "\\" + path;
            }
            return path;
        }


        private FolderResponseDTO GetFileNameAndParentId(int id)
        {
            var folder = folderService.GetFileNameAndParentId(id);
            return folder;
        }



        [HttpPost]
        [Authorize]
        public ServiceResult UpdateFolder(FolderRequestDTO folderRequest)
         {
            ServiceResult serviceResult = new ServiceResult();
            var userid = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            serviceResult = folderService.UpdateFolder(folderRequest);
            if (serviceResult.IsSuccess)
            {
                return serviceResult;
            }
            return serviceResult;
        }



        [HttpPost]
        [Authorize]
        public ServiceResult DeleteFolder(FolderRequestDTO folderRequestDTO)
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var userid = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            ServiceResult serviceResult = new ServiceResult();
            var folderDTO = folderService.DeleteFolder(folderRequestDTO);
            return folderDTO;


        }
    }
}

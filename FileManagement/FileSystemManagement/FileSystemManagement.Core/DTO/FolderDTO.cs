using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemManagement.Core.DTO
{

    public class FolderUpload
    {
        public int FolderId { get; set; }
        public string FileName { get; set; }

        public IFormFile FormFile { get; set; }
    }


    public class FolderDTO
    {
        public int ParentId { get; set; }
        public string FileName { get; set; }
    }
    public class FolderRequestDTO
    {
        public int? UserId { get; set; }
        public int? ParentId { get; set; }
        public int FolderId { get; set; }
        public string Name { get; set; }
        public byte Status { get; set; }


    }

    public class FolderResponseDTO
    {
       
        public int FolderId { get; set; }
        public int? ParentId { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public byte Status { get; set; }
        
    }
       



}


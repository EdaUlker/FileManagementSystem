using System;
using System.Collections.Generic;

#nullable disable

namespace FileSystemManagement.Core.Models
{
    public partial class TblFolder
    {
        public int FolderId { get; set; }
        public string FilePath { get; set; }
        public int? FolderSize { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? UserId { get; set; }
        public int? ParentId { get; set; }
        public string FileName { get; set; }
        public byte? Status { get; set; }

        public virtual TblUser User { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace FileSystemManagement.Core.Models
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblFolders = new HashSet<TblFolder>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long? Tc { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public int? Code { get; set; }
        public byte? Status { get; set; }

        public virtual ICollection<TblFolder> TblFolders { get; set; }
    }
}

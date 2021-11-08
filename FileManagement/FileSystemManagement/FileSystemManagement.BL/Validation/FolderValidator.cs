using FileSystemManagement.Core.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemManagement.BL.Validation
{
    public class FolderValidator : AbstractValidator<FolderRequestDTO>
    {
        public FolderValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Dosya adı boş olamaz");
        }
    }
}

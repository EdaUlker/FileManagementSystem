using FileSystemManagement.BL.Validation;
using FileSystemManagement.Core.DTO;
using FileSystemManagement.Core.Models;
using FileSystemManagement.DAL.Abstract;
using FileSystemManagement.DAL.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FileSystemManagement.Core.DTO.FolderDTO;

namespace FileSystemManagement.BL
{
    public class FolderService : IFolderManager
    {
        private readonly IFolder _folder;
        private FolderValidator folderValidator;

        public FolderService()
        {
            _folder = new FolderRepository();
            folderValidator = new FolderValidator();
        }

        public ServiceResult CreateFolder(FolderRequestDTO folderRequest, int userid)
        {
            ServiceResult loginMessage = new ServiceResult();
            var result = folderValidator.Validate(folderRequest);
            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    loginMessage = new ServiceResult()
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage,
                    };
                }

            }
            else
            {
                var folder = _folder.Add(folderRequest, userid);


                if (folder == null)
                {
                    foreach (var failure in result.Errors)
                    {
                        loginMessage = new ServiceResult()
                        {
                            StatusCode = 400,
                            IsSuccess = false,
                            Message = failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage,
                        };
                    }

                }
                else
                {
                    loginMessage = new ServiceResult()
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Operation Succesful",
                    };
                }
            }


            return loginMessage;
        }

    


        public ServiceResult DeleteFolder(FolderRequestDTO folderRequest)
        {
            ServiceResult loginMessage = new ServiceResult();

            if (folderRequest.FolderId == null)
            {
                loginMessage = new ServiceResult()
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Invalid folder id",

                };
            }

            if (folderRequest.FolderId != null)
            {
                var folder = _folder.Update(folderRequest);


                if (folder == null)
                {
                    loginMessage = new ServiceResult()
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Invalid folder id",

                    };
                }
                else
                {
                    loginMessage = new ServiceResult()
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Operation Succesful",

                    };
                }
            }

            return loginMessage;
        }


        public FolderResponseDTO GetListById(int id)
        {
            FolderResponseDTO folderResponseDTO = new FolderResponseDTO();
            if (id != null)
            {
                var folder = _folder.GetOne(id);
                folderResponseDTO.FolderId = folder.FolderId;
                folderResponseDTO.ParentId = folder.ParentId;
                folderResponseDTO.FileName = folder.FileName;
            }
            return folderResponseDTO;
        }



        public List<FolderResponseDTO> ListAll(FolderRequestDTO folderRequest, int userid)
        {
            var responseList = _folder.GetAll(folderRequest,userid);

            List<FolderResponseDTO> list = new List<FolderResponseDTO>();

            foreach(var item in responseList)
            {
                FolderResponseDTO dto = new FolderResponseDTO();
              
                dto.FileName = item.FileName;
                dto.FileSize = (int)item.FolderSize;
                dto.FolderId = item.FolderId;
                dto.ParentId = item.ParentId;
                dto.Status = (byte)item.Status;
                list.Add(dto);
            }
            
            return list;
        }


        public FolderResponseDTO UpdateFolder(FolderRequestDTO folderRequest)
        {
            if (!string.IsNullOrEmpty(folderRequest.Name))
            {
                var folder = _folder.Update(folderRequest);
                FolderResponseDTO folderResponseDTO = new FolderResponseDTO();
                folderResponseDTO.Status = (byte)folder.Status;
                return folderResponseDTO;
            }

            return null;
        }

        public ServiceResult UploadFile(FolderUpload folderUpload)
        {
            ServiceResult serviceResult = new ServiceResult();
            if (folderUpload.FormFile == null)
            {
                serviceResult = new ServiceResult()
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Invalid file",

                };
            }

            if (folderUpload.FormFile != null)
            {
                var folder = _folder.UploadFile(folderUpload);


                if (folder == null)
                {
                    serviceResult = new ServiceResult()
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Invalid file",

                    };
                }
                else
                {
                    serviceResult = new ServiceResult()
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Operation Succesful",

                    };
                }
            }

            return serviceResult;
        }
    }
}

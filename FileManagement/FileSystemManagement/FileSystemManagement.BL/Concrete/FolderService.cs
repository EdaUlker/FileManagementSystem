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
                var foldernameControl = _folder.FolderName(folderRequest.Name, userid);
                if (foldernameControl != null)
                {
                    loginMessage = new ServiceResult()
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Aynı isimli dosya zaten var.",

                    };
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

            else
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


        public FolderResponseDTO GetListById(int? id)
        {
            FolderResponseDTO folderResponseDTO = new FolderResponseDTO();
            if (id != null)
            {
                var folder = _folder.GetOne((int)id);
                //folderResponseDTO.FolderId = folder.FolderId;
                //folderResponseDTO.ParentId = folder.ParentId;
                //folderResponseDTO.FileName = folder.FileName;
            }
            return folderResponseDTO;
        }



        public List<FolderResponseDTO> ListAll(FolderRequestDTO folderRequest, int userid)
        {
            var responseList = _folder.GetAll(folderRequest,userid);

            List<FolderResponseDTO> list = new List<FolderResponseDTO>();

            foreach(var item in responseList)
            {
                if(item.Status == 1)
                {
                    FolderResponseDTO dto = new FolderResponseDTO();

                    dto.FileName = item.FileName;
                    dto.FileSize = (int)item.FolderSize;
                    dto.FolderId = item.FolderId;
                    dto.ParentId = item.ParentId;
                    dto.Status = (byte)item.Status;
                    dto.Type = item.Type;
                    
                    list.Add(dto);
                }

            }

            return list.OrderByDescending(x=>x.Type).ToList();

          
        }

   

        public ServiceResult UpdateFolder(FolderRequestDTO folderRequest)
        {
            ServiceResult serviceResult = new ServiceResult();

            if(folderRequest.FolderId == null || folderRequest.Name == "")
            {
                serviceResult = new ServiceResult()
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Invalid folder id",

                };
            }

            else
            {
                var folder = _folder.Update(folderRequest);


                if (folder == null)
                {
                    serviceResult = new ServiceResult()
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Invalid folder id",

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


        //public FolderResponseDTO UpdateFolder(FolderRequestDTO folderRequest)
        //{
        //    FolderResponseDTO folderResponseDTO = new FolderResponseDTO();
        //    if (!string.IsNullOrEmpty(folderRequest.Name))
        //    {
        //        var folder = _folder.Update(folderRequest);
        //        folderResponseDTO.Status = (byte)folder.Status;
        //        return folderResponseDTO;
        //    }


        //    return folderResponseDTO;
        //}




        public ServiceResult UploadFile(FolderUpload folderUpload, int userid)
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


            else
            {

                int count = 1;
                string fileNameOnly = Path.GetFileNameWithoutExtension(folderUpload.FileName);
                string extension = Path.GetExtension(folderUpload.FileName);
                string newFullPath = folderUpload.FileName;
    
                if(_folder.CheckFileName(newFullPath, userid) != null)
                {
                    do
                    {
                        string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                        folderUpload.FileName = tempFileName + extension;
                    } while (_folder.CheckFileName(folderUpload.FileName, userid) != null);
                }
               



                var folder = _folder.UploadFile(folderUpload, userid);


                if (folder == null)
                {
                    serviceResult = new ServiceResult()
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Invalid file",
                    };
                }
                else if (folder.FolderSize > 4194304)
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

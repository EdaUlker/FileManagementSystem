using FileSystemManagement.Core.Models;
using FileSystemManagement.DAL.Abstract;
using FileSystemManagement.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileFolderManagement.Web.Controllers
{
    public class LoginController : Controller
    {
        FileManagementContext context = new FileManagementContext();
        IUser userDal = new UserRepository();

        public IActionResult Index()
        {
            return View();
        }

      //  [HttpGet("login")]

        public IActionResult Login(string returnUrl)
        {
           // ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(TblUser user,string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var info = userDal.Login(user);
            if (info != null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                TempData["Error"] = "Username or password is invalid!";
                return View("login");
            }
            
        }


  
    }
}

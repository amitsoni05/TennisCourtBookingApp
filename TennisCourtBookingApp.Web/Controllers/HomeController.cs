using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TennisCourtBookingApp.Common.CommonEntities;
using System.Diagnostics;
using TennisCourtBookingApp.Common.BusinessEntities;
using TennisCourtBookingApp.Common.Utility;
using TennisCourtBookingApp.Provider.IProvider;
using TennisCourtBookingApp.Web.Models;
using Microsoft.AspNetCore.DataProtection;

namespace TennisCourtBookingApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommonProvider _commonProvider;
        private readonly IUserProvider _userProvider;
        private IWebHostEnvironment _webHostEnvironment;
        private ISessionManager _sessionManager;

        public HomeController(ICommonProvider commonProvider, IUserProvider userProvider, IWebHostEnvironment webHostEnvironment, ISessionManager sessionManager)
        {
            _commonProvider = commonProvider;
            _webHostEnvironment = webHostEnvironment;
            _sessionManager = sessionManager;
            _userProvider = userProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {

            if (HttpContext.Session.GetString("AdminSession") != null)
            {
                return RedirectToAction("TennisCourt", "Index");
            }
            else if (HttpContext.Session.GetString("UserSession") != null)
            {
                int? userId = HttpContext.Session.GetInt32("UserId");
                return RedirectToAction("UserDetails", "User", new { userId = userId });
            }
            CaptchaResult captcha = Captcha.Generate(CaptchaType.Simple);
           
            TennisCourtBookingViewModel model = new TennisCourtBookingViewModel
            {
                CaptchaImage = captcha.CaptchaBase64
            };
            HttpContext.Session.SetString("Code", captcha.CatpchaCode);
            return PartialView("_UserLogin",model);
        }


        [HttpPost]
        public IActionResult Login(TennisCourtBookingViewModel loginDetails)
        {
                     ResponseModel model = new ResponseModel();
            var details = _commonProvider.FindRole(loginDetails.TennisCourtBookingUserModel);
            if (details != null)
            {
               
                    if (details.RoleName == "Admin")
                    {
                        HttpContext.Session.SetString("AdminSession", "Login");
                        int userId = _commonProvider.FindUser(loginDetails.TennisCourtBookingUserModel);
                        HttpContext.Session.SetInt32("UserId", userId);
                        //model.Message = details.RoleName;
                        //var id = details.RoleId;
                        return Json(details.RoleId);

                    }
                    else
                    {
                        HttpContext.Session.SetString("UserSession", "Login");
                        int userId = _commonProvider.FindUser(loginDetails.TennisCourtBookingUserModel);
                        HttpContext.Session.SetInt32("UserId", userId);
                        //return RedirectToAction("UserDetails", "User", new { userId = userId });
                        //return Json(userId);
                        //return RedirectToAction("UserDetails", "User", new { userId = userId });
                        //model.Message = details.RoleName;
                        return Json(details.RoleId);
                    }
              
            }
            else
            {
                TempData["Message"] = "UserName Or Password Incorrect..";
                TempData.Keep();
            }
            return Json("");
        }

        [HttpPost]
        public IActionResult CaptchaCheck(TennisCourtBookingViewModel loginDetails)
        {
             if (HttpContext.Session.GetString("Code") == loginDetails.CaptchaCode)
             {
                return Json(1);
             }
            else
            {
                TempData["Message"] = "Wrong Captcha..";
                TempData.Keep();
                return Json(2);
            }
        }
        [HttpGet]
        public JsonResult RefreshCaptcha()
        {
            CaptchaResult captcha = Captcha.Generate(CaptchaType.Simple);
            HttpContext.Session.SetString("Code", captcha.CatpchaCode);
            return Json(captcha.CaptchaBase64);
        }


        [HttpGet]
        public IActionResult UserSignUp()
        {
            TennisCourtBookingViewModel model = new TennisCourtBookingViewModel();
            model.IsEdit = false;
            model.IsUser = true;
            return PartialView("_UserPartial", model);
        }

        //[HttpPost]
        //public IActionResult UserSignUp(TennisCourtBookingViewModel signupDetails)
        //{
        //    _commonProvider.RegisterUser(signupDetails.TennisCourtBookingUserModel);
        //    return Json("");

        //}

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Index");
            }
            if (HttpContext.Session.GetString("AdminSession") != null)
            {
                HttpContext.Session.Remove("AdminSession");
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult SignUp(TennisCourtBookingViewModel inputModel)
        {
            TennisCourtBookingViewModel model = new TennisCourtBookingViewModel();
            if (inputModel.Image != null && inputModel.Image.Length > 0)
            {
                //changing the file name
                string fileName = Path.GetFileNameWithoutExtension(inputModel.Image.FileName);
                string prfileName = Guid.NewGuid().ToString() + "__" + fileName + Path.GetExtension(inputModel.Image.FileName);
                FileInfo fi = new FileInfo(prfileName);
                //Checking if the file is image or not
                if (fi.Extension.ToLower() == ".image" || fi.Extension.ToLower() == ".png" || fi.Extension.ToLower() == ".jpg" || fi.Extension.ToLower() == ".jpeg")
                {
                    var rootPath = _webHostEnvironment.WebRootPath;
                    //Checking the path to save the image
                    string documentFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ExtraFolder\\ProfileImage");

                    if (!Directory.Exists(documentFolder))
                        Directory.CreateDirectory(documentFolder);

                    string prfilePath = Path.Combine(documentFolder, prfileName);
                    using (var ms = new FileStream(prfilePath, FileMode.Create))
                    {
                        inputModel.Image.CopyTo(ms);
                    }
                    //string Imagee = prfileName;

                    //model = _userProvider.SaveEmployeeProfileImg(inputModel.TennisCourtBookingUserModel);
                    int? userId = HttpContext.Session.GetInt32("UserId");
                    if (userId != null)
                    {
                        inputModel.TennisCourtBookingUserModel = new TennisCourtBookingUserModel();
                        inputModel.TennisCourtBookingUserModel.Image = prfileName;
                        _userProvider.UpdateUserImage(inputModel.TennisCourtBookingUserModel, userId);
                    }
                    else
                    {
                        inputModel.TennisCourtBookingUserModel.Image = prfileName;
                        _commonProvider.RegisterUser(inputModel.TennisCourtBookingUserModel);
                        return Json("");
                    }
                    //_commonProvider.SaveImage(userId, Imagee);
                }
                else
                {
                    model.IsSuccess = false;
                    model.Message = "You can only upload image or jpg files";
                }
            }
            return Json("");
        }
        public IActionResult ResetPassword()
        {

            return PartialView("_ResetPassword");
        }
        [HttpPost]
        public IActionResult ResetPassword(string email)
        {
           var message= _commonProvider.VerifyUsername(email);
            return Json(message);
        }

        [HttpGet]
        public IActionResult ChangePassword(string id)
        {
            //if (!string.IsNullOrEmpty(id))
            //{
            //    string parm = "";
            //    string[] parms;
            //    try
            //    {
            //        //parm = _protector.Unprotect(id);
            //        parms = parm.Split('|');
            //        DateTime dt = new DateTime(Convert.ToInt64(parms[1]));
            //        dt = dt.AddMinutes(30);
            //        if (DateTime.Now > dt)
            //            return RedirectToAction("ForgotPassword", "Account", new { @Id = _commonProvider.ProtectString("1") });
            //    }
            //    catch (Exception)
            //    {
            //        return RedirectToAction("Index", "Account");
            //    }
            //    ResetPasswordModel model = new ResetPasswordModel();
            //    var user = _userMasterProvider.GetUserById(Convert.ToInt32(parms[0]));
            //    if (user != null)
            //    {
            //        model = new ResetPasswordModel
            //        {
            //            EncId = _protector.Protect(user.UserMasterId.ToString()),
            //            Username = user.Email
            //        };
            //        CaptchaResult captcha = Captcha.Generate(CaptchaType.Simple);
            //        _sessionManager.CaptchaCode = captcha.CatpchaCode;
            //        model.CaptchaImage = captcha.CaptchaBase64;
            //        model.CaptchaCode = "";
            //    }
            //    else
            //        return RedirectToAction("Index", "Account");

            //    return View(model);
            //}
            //else
            //    return RedirectToAction("Index", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(TennisCourtBookingViewModel model)
        {
            var msg = _userProvider.ChangePassword(model.TennisCourtBookingUserModel);
            return Json(msg.IsSuccess);

        }
    }
}
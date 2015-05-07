using AutoMapper;
using Distance.Business;
using Distance.MVC.Models;
using Distance.Service.IServices;
using Distance.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Distance.MVC.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public IServiceUserAuth authService { get; set; }

        static AuthController()
        {
            Mapper.CreateMap<UserAuth, UserAuthModel>();
            Mapper.CreateMap<UserAuthModel, UserAuth>();
            Mapper.CreateMap<UserAuth, AuthModel>();
            Mapper.CreateMap<AuthModel, UserAuth>();

        }

        public AuthController()
            : this(new ServiceUserAuth())
        {

        }
        public AuthController(IServiceUserAuth authService)
        {
            this.authService = authService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AuthModel authModel)
        {
            if (!ModelState.IsValid)
            return View(authModel);

            try
            {
                var userData = authService.GetUserByUsername(authModel.Username);

                if (userData == null)
                {
                    ModelState.AddModelError(string.Empty, "Username or Password is wrong");
                    ViewBag.SigninStatusLog = "Fail";
                    ViewBag.SigninMessageLog = "Username not found.";
                    ViewBag.SigninUserNameLog = authModel.Username;

                    return View(authModel);
                }
                else
                {

                    string pass = authModel.Password;
                    if (pass == userData.Password && authModel.Username == userData.Username)
                    {
                        Session["UserIdAuth"] = userData.UID;
                        Session["UsernameAuth"] = userData.Username;
                        Session["Role"] = userData.Role;

                        ViewBag.SigninUserNameLog = authModel.Username;
                        ViewBag.SigninStatusLog = "Success";

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Username or Password is wrong");
                        ViewBag.SigninStatusLog = "Fail";
                        ViewBag.SigninMessageLog = "Username or password is wrong.";
                        ViewBag.SigninUserNameLog = authModel.Username;

                        return View(authModel);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "เกิดข้อผิดพลาดจากระบบ กรุณาลองใหม่ในภายหลัง");
                ViewBag.SigninStatusLog = "Fail";
                ViewBag.SigninMessageLog = ex.Message;
                ViewBag.SigninUserNameLog = authModel.Username;

                throw new Exception("เกิดข้อผิดพลาดจากระบบ กรุณาลองใหม่ในภายหลัง");

            }

            return RedirectToAction("Home");

            //return View();
        }
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult NotAuthorized()
        {

            return View();
        }
    }
}
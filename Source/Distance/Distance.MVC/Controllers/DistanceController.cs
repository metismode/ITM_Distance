using DXTms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Distance.MVC.Controllers
{
    public class DistanceController : Controller
    {
        // GET: Distance
        public ActionResult Distance()
        {
            return View();
        }

        public ActionResult ImportForm()
        {
            return View();
        }

        public ActionResult ImportView(HttpPostedFileBase file)
        {


            string messages = "";
            try
            {
                messages = ExcelToJson.Execute(file);
                Response.StatusCode = (int)HttpStatusCode.Accepted;
                return Content(messages);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                messages = "ไม่สำเร็จ";
                return Content(messages);
            }

        }

        public FileResult Download()
        {
            string part = "~/App_Data/Distance_Matrix_Template.xlsx";

            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(part));
            string[] words = part.Split('.');
            foreach (var item in words)
            {
                part = item;
            }
            string fileName = "DistanceMatrixTemplate." + part;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }


    }
}
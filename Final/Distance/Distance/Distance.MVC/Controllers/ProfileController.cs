using AutoMapper;
using Distance.Business;
using Distance.MVC.Models;
using Distance.Service.IServices;
using Distance.Service.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Distance.MVC.Controllers
{
    public class ProfileController : Controller
    {

        public IServiceEmployee service { get; set; }
        public ProfileController(IServiceEmployee service) { this.service = service; }
        public ProfileController() : this(new ServiceEmployee()) { }
        static ProfileController()
        {
            // create Mapping      Model Presentation / DX_Business
            Mapper.CreateMap<EmployeeModel, Employee>();
            Mapper.CreateMap<Employee, EmployeeModel>();
            Mapper.CreateMap<EmployeeListModel, EmployeeList>();
            Mapper.CreateMap<EmployeeList, EmployeeListModel>();
        }


        // GET: Profile
        public ActionResult Load(int id)
        {

            var dataEmp = service.GetEmployee(id);

            var empModel = Mapper.Map<Employee, EmployeeModel>(dataEmp);

            return Content(JsonConvert.SerializeObject(empModel));
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var employee = service.GetEmployee(id);
                var model = Mapper.Map<Employee, EmployeeModel>(employee);
               
                return View(model);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }

        public ActionResult AjaxEdit(EmployeeModel model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    string messages = string.Join("<br/> ", ModelState.Values
                             .SelectMany(x => x.Errors)
                             .Select(x => x.ErrorMessage));

                    Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    return Content(messages);
                }
                else
                {
                   
                    var emp = Mapper.Map<EmployeeModel, Employee>(model);
                    int id = service.UpdateProfile(emp);
                    Response.StatusCode = (int)HttpStatusCode.Accepted;
                    string messages = "Save Successfully";

                    return Content(messages);
                }
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
    }
}
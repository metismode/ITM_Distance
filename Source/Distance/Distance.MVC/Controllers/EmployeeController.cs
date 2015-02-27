using AutoMapper;
using Distance.Business;
using Distance.MVC.Models;
using Distance.Service.IServices;
using Distance.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Distance.MVC.Controllers
{
    public class EmployeeController : Controller
    {

        public IServiceEmployee service { get; set; }
        public EmployeeController(IServiceEmployee service) { this.service = service; }
        public EmployeeController() : this(new ServiceEmployee()) { }
        static EmployeeController()
        {
            // create Mapping      Model Presentation / DX_Business
            Mapper.CreateMap<EmployeeModel, Employee>();
            Mapper.CreateMap<Employee, EmployeeModel>();
            Mapper.CreateMap<EmployeeListModel, EmployeeList>();
            Mapper.CreateMap<EmployeeList, EmployeeListModel>();
        }

        // GET: Employee
        public ActionResult List(string sort = "\"id\"", string order = "ASC", int page = 1, int pageSize = 10, string keyword = null, string filterData = null, int status = 0)
        {
            try
            {
                //var uid = (int)Session["UserIdAuth"];
                EmployeeListModel models = new EmployeeListModel { Sort = sort, Order = order, Page = page, PageSize = pageSize };
                var empList = service.GetEmployeeList(sort + " " + order, out models.TotalRows, page, pageSize, keyword, filterData, status);
                var empListModel = Mapper.Map<List<Employee>, List<EmployeeModel>>(empList);
                models.List = empListModel;

                
                ViewData["keyword"] = !string.IsNullOrEmpty(keyword) ? keyword : "";
                ViewData["filterData"] = !string.IsNullOrEmpty(filterData) ? filterData : "";

                return View(models);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }


        public ActionResult Add(Employee model)
        {

           return View(model);
        }

        [HttpPost]
        public ActionResult AddA(Employee model)
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
                   // var emp = Mapper.Map<EmployeeModel, Employee>(model);
                    int id = service.InsertEmployee(model);
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
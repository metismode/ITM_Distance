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
    }
}
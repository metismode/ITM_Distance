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
    public class MasterController : Controller
    {

         public IServiceMaster service { get; set; }
         public MasterController(IServiceMaster service) { this.service = service; }
         public MasterController() : this(new ServiceMaster()) { }
        static MasterController()
        {
            // create Mapping      Model Presentation / DX_Business
            Mapper.CreateMap<MasterDataModel, Master>();
            Mapper.CreateMap<Master, MasterDataModel>();
            Mapper.CreateMap<MasterDataListModel, MasterList>();
            Mapper.CreateMap<MasterList, MasterDataListModel>();
        }

        // GET: Master
        public ActionResult ListProvince(string sort = "\"id\"", string order = "ASC", int page = 1, int pageSize = 10, string keyword = null, string filterData = null, int status = 0)
        {
            try
            {
                MasterDataListModel models = new MasterDataListModel { Sort = sort, Order = order, Page = page, PageSize = pageSize };
                var List = service.GetProvinceList(sort + " " + order, out models.TotalRows, page, pageSize, keyword, filterData, status);
                var ListModel = Mapper.Map<List<Master>, List<MasterDataModel>>(List);
                models.List = ListModel;

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

        public ActionResult ListAmphur(string sort = "\"id\"", string order = "ASC", int page = 1, int pageSize = 10, string keyword = null, string filterData = null, int status = 0)
        {
            try
            {
                MasterDataListModel models = new MasterDataListModel { Sort = sort, Order = order, Page = page, PageSize = pageSize };
                var List = service.GetAmphurList(sort + " " + order, out models.TotalRows, page, pageSize, keyword, filterData, status);
                var ListModel = Mapper.Map<List<Master>, List<MasterDataModel>>(List);
                models.List = ListModel;

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

        public ActionResult ListTambon(string sort = "\"id\"", string order = "ASC", int page = 1, int pageSize = 10, string keyword = null, string filterData = null, int status = 0)
        {
            try
            {
                MasterDataListModel models = new MasterDataListModel { Sort = sort, Order = order, Page = page, PageSize = pageSize };
                var List = service.GetTambonList(sort + " " + order, out models.TotalRows, page, pageSize, keyword, filterData, status);
                var ListModel = Mapper.Map<List<Master>, List<MasterDataModel>>(List);
                models.List = ListModel;

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
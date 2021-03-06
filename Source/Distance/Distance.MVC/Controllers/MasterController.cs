﻿using AutoMapper;
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


        public ActionResult AddProvince(MasterDataModel model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult AddProvinceAjax(MasterDataModel model)
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
                    var p = Mapper.Map<MasterDataModel, Master>(model);
                    int id = service.InsertProvince(p);
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

        public ActionResult EditProvince(int id)
        {
            var p = service.GetProvince(id);
            var model = Mapper.Map<Master, MasterDataModel>(p);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditProvinceAjax(MasterDataModel model)
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
                    var p = Mapper.Map<MasterDataModel, Master>(model);
                    int id = service.UpdateProvince(p);
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

        public ActionResult AddAmphur(MasterDataModel model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult AddAmphurAjax(MasterDataModel model)
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
                    var a = Mapper.Map<MasterDataModel, Master>(model);
                    int id = service.InsertAmphur(a);
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

        public ActionResult EditAmphur(int id)
        {
            var a = service.GetAmphur(id);
            var model = Mapper.Map<Master, MasterDataModel>(a);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditAmphurAjax(MasterDataModel model)
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
                    var a = Mapper.Map<MasterDataModel, Master>(model);
                    int id = service.UpdateAmphur(a);
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
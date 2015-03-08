using AutoMapper;
using Distance.Business.Dropdown;
using Distance.MVC.Models;
using Distance.Service.Dropdown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Distance.MVC.Controllers.DropDown
{
    public class DropDownController : Controller
    {
        // GET: DropDown
        public IServicesDD ddservice { get; set; }
        public DropDownController(IServicesDD ddservice)
        {
            this.ddservice = ddservice;

           
            Mapper.CreateMap<DDStatus, DropDownDataModel>()
                  .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.id))
                  .ForMember(dest => dest.Display, opt => opt.MapFrom(src => src.name));
            Mapper.CreateMap<DDProvice, DropDownDataModel>()
                  .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.LatLon))
                  .ForMember(dest => dest.Display, opt => opt.MapFrom(src => src.Name));
            Mapper.CreateMap<DDAmphur, DropDownDataModel>()
                  .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.LatLon))
                  .ForMember(dest => dest.Display, opt => opt.MapFrom(src => src.Name));
            Mapper.CreateMap<DDTambon, DropDownDataModel>()
                  .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.LatLon))
                  .ForMember(dest => dest.Display, opt => opt.MapFrom(src => src.Name));
          
        }
        public DropDownController()
            : this(new Service.Dropdown.ServicesDD())
        {

        }

        private ActionResult ShowDropDownList(DropDownListModel dropDownListModel)
        {

            return PartialView("PartialDropdown", dropDownListModel);
        }


        public ActionResult StatusList(DropDownListModel dropDownListModel)
        {
            //----------------Load Data Dropdown-------------------------------

            var status = ddservice.GetStatusList();

            //----------------------------------------------------------

            List<DropDownDataModel> dataModelList = Mapper.Map<List<DDStatus>, List<DropDownDataModel>>(status);

            dropDownListModel.List = dataModelList;
            dropDownListModel.OptionalText = "--- Select Status ---";

            return ShowDropDownList(dropDownListModel);
        }

       
        public ActionResult ProviceList(DropDownListModel dropDownListModel)
        {
            //----------------Load Data Dropdown-------------------------------

            var p = ddservice.GetProviceList();

            //----------------------------------------------------------

            List<DropDownDataModel> dataModelList = Mapper.Map<List<DDProvice>, List<DropDownDataModel>>(p);

            dropDownListModel.List = dataModelList;
            dropDownListModel.OptionalText = "--- Select Provice ---";

            return ShowDropDownList(dropDownListModel);
        }

       
        public ActionResult AmphurList(DropDownListModel dropDownListModel)
        {
            //----------------Load Data Dropdown-------------------------------

            var a = ddservice.GetAmphurList();

            //----------------------------------------------------------

            List<DropDownDataModel> dataModelList = Mapper.Map<List<DDAmphur>, List<DropDownDataModel>>(a);

            dropDownListModel.List = dataModelList;
            dropDownListModel.OptionalText = "--- Select Amphur ---";

            return ShowDropDownList(dropDownListModel);
        }
        public ActionResult TambonList(DropDownListModel dropDownListModel)
        {
            //----------------Load Data Dropdown-------------------------------

            var t = ddservice.GetTambonList();

            //----------------------------------------------------------

            List<DropDownDataModel> dataModelList = Mapper.Map<List<DDTambon>, List<DropDownDataModel>>(t);

            dropDownListModel.List = dataModelList;
            dropDownListModel.OptionalText = "--- Select Tambon ---";

            return ShowDropDownList(dropDownListModel);
        }
    }
}
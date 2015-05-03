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
            Mapper.CreateMap<DDProvince, DropDownDataModel>()
                  .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.LatLon))
                  .ForMember(dest => dest.Display, opt => opt.MapFrom(src => src.Name));
            Mapper.CreateMap<DDAmphur, DropDownDataModel>()
                  .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.LatLon))
                  .ForMember(dest => dest.Display, opt => opt.MapFrom(src => src.Name));
            Mapper.CreateMap<DDTambon, DropDownDataModel>()
                  .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.LatLon))
                  .ForMember(dest => dest.Display, opt => opt.MapFrom(src => src.Name));
            Mapper.CreateMap<DDRole, DropDownDataModel>()
                   .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.id))
                   .ForMember(dest => dest.Display, opt => opt.MapFrom(src => src.name));
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

       
        public ActionResult ProvinceList(DropDownListModel dropDownListModel)
        {
            //----------------Load Data Dropdown-------------------------------

            var p = ddservice.GetProvinceList();

            //----------------------------------------------------------

            List<DropDownDataModel> dataModelList = Mapper.Map<List<DDProvince>, List<DropDownDataModel>>(p);

            dropDownListModel.List = dataModelList;
            dropDownListModel.OptionalText = "--- Select Province ---";

            return ShowDropDownList(dropDownListModel);
        }


        public ActionResult AmphurList(DropDownListModel dropDownListModel, string ProvinceId = "-1")
        {
            //----------------Load Data Dropdown-------------------------------

            if (ProvinceId == "")
                ProvinceId = "-1";

            string[] words = ProvinceId.Split('/');
            ProvinceId = words[0];

            var a = ddservice.GetAmphurList(Convert.ToInt32(ProvinceId));

            //----------------------------------------------------------

            List<DropDownDataModel> dataModelList = Mapper.Map<List<DDAmphur>, List<DropDownDataModel>>(a);

            dropDownListModel.List = dataModelList;
            dropDownListModel.OptionalText = "--- Select Amphur ---";
            if (Convert.ToInt32(ProvinceId) == -1 || dataModelList.Count() <= 0)
            {
                dropDownListModel.Disabled = true;
            }

            return ShowDropDownList(dropDownListModel);
        }

        public ActionResult ProvinceList2(DropDownListModel dropDownListModel)
        {
            //----------------Load Data Dropdown-------------------------------

            var p = ddservice.GetProvinceList();

            //----------------------------------------------------------

            List<DropDownDataModel> dataModelList = Mapper.Map<List<DDProvince>, List<DropDownDataModel>>(p);

            dropDownListModel.List = dataModelList;
            dropDownListModel.OptionalText = "--- Select Province ---";

            return ShowDropDownList(dropDownListModel);
        }


        public ActionResult AmphurList2(DropDownListModel dropDownListModel, string ProvinceId = "-1")
        {
            //----------------Load Data Dropdown-------------------------------

            if (ProvinceId == "")
                ProvinceId = "-1";

            string[] words = ProvinceId.Split('/');
            ProvinceId = words[0];

            var a = ddservice.GetAmphurList(Convert.ToInt32(ProvinceId));

            //----------------------------------------------------------

            List<DropDownDataModel> dataModelList = Mapper.Map<List<DDAmphur>, List<DropDownDataModel>>(a);

            dropDownListModel.List = dataModelList;
            dropDownListModel.OptionalText = "--- Select Amphur ---";
            if (Convert.ToInt32(ProvinceId) == -1 || dataModelList.Count() <= 0)
            {
                dropDownListModel.Disabled = true;
            }

            return ShowDropDownList(dropDownListModel);
        }



        public ActionResult TambonList(DropDownListModel dropDownListModel, int AmphurId = -1)
        {
            //----------------Load Data Dropdown-------------------------------

            var t = ddservice.GetTambonList(AmphurId);

            //----------------------------------------------------------

            List<DropDownDataModel> dataModelList = Mapper.Map<List<DDTambon>, List<DropDownDataModel>>(t);

            dropDownListModel.List = dataModelList;
            dropDownListModel.OptionalText = "--- Select Tambon ---";
            if (AmphurId == -1 || dataModelList.Count <= 0)
            {
                dropDownListModel.Disabled = true;
            }

            return ShowDropDownList(dropDownListModel);
        }

        public ActionResult RoleList(DropDownListModel dropDownListModel)
        {
            //----------------Load Data Dropdown-------------------------------

            var t = ddservice.GetRoleList();

            //----------------------------------------------------------

            List<DropDownDataModel> dataModelList = Mapper.Map<List<DDRole>, List<DropDownDataModel>>(t);

            dropDownListModel.List = dataModelList;
            dropDownListModel.OptionalText = "--- Select Role ---";
          
            return ShowDropDownList(dropDownListModel);
        }

    }
}
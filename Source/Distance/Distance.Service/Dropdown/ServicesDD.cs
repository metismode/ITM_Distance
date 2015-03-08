using Distance.Business.Dropdown;
using Distance.Data;
using Distance.Data.Dropdown;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Service.Dropdown
{
    public class ServicesDD : IServicesDD
    {
        static readonly string provider = ConfigurationManager.AppSettings.Get("DataProvider");
        static readonly IDDDaoFactory factory = DDDaoFactories.GetFactory(provider);
        static readonly IStatusDao StatusDao = factory.StatusDao;

        public List<DDStatus> GetStatusList()
        {
            return StatusDao.GetStatusList();
        }
    }
}

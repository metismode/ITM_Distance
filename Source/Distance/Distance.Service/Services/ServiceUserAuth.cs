using Distance.Business;
using Distance.Data;
using Distance.Service.IServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Service.Services
{
    public class ServiceUserAuth : IServiceUserAuth
    {
        static readonly string provider = ConfigurationManager.AppSettings.Get("DataProvider");
        static readonly IDaoFactory factory = DaoFactories.GetFactory(provider);
        static readonly IDaoUserAuth DaoUserAuth = factory.DaoUserAuth;

        public UserAuth GetUserByUsername(string username)
        {
            try
            {
                return DaoUserAuth.GetUserByUsername(username);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}

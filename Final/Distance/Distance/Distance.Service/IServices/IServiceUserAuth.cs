using Distance.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Service.IServices
{
    public interface IServiceUserAuth
    {
        UserAuth GetUserByUsername(string username);
    }
}

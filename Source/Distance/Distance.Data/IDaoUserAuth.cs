using Distance.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Data
{
    public interface IDaoUserAuth
    {
        UserAuth GetUserByUsername(string username);
    }
}

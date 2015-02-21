using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Data
{
    public interface IDaoFactory
    {
        //IDaoEmployee DaoEmployee { get; }


        IDaoUserAuth DaoUserAuth { get; }

        IDaoEmployee DaoEmployee { get; }
       
    }
}

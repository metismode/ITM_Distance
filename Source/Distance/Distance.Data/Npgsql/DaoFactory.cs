using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Data.Npgsql
{
   
    public class DaoFactory : IDaoFactory
    {
        //public IDaoEmployee DaoEmployee
        //{
        //    get { return new DaoEmployee(); }
        //}

        public IDaoUserAuth DaoUserAuth
        {
            get { return new DaoUserAuth(); }
        }

        public IDaoEmployee DaoEmployee
        {
            get { return new DaoEmployee(); }
        }
        
    }
    
}

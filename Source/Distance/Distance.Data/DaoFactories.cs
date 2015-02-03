using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Data
{
    public class DaoFactories
    {
        public static IDaoFactory GetFactory(string dataProvider)
        {
            switch (dataProvider.ToLower())
            {
                case "npgsql": return new Npgsql.DaoFactory();

                default: return new Npgsql.DaoFactory();
            }
        }


    }

}

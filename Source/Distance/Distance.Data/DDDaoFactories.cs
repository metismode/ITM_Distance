using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Data
{
    public class DDDaoFactories
    {
        public static IDDDaoFactory GetFactory(string dataProvider)
        {
            switch (dataProvider.ToLower())
            {
                case "npgsql": return new Npgsql.Dropdown.DDDaoFactory();

                default: return new Npgsql.Dropdown.DDDaoFactory();
            }
        }

    }
}

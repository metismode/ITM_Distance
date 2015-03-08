using Distance.Data.Dropdown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.Data.Npgsql.Dropdown
{
    public class DDDaoFactory : IDDDaoFactory
    {

        public IStatusDao StatusDao
        {
            get { return new StatusDao(); }
        }

        public IProviceDao ProviceDao
        {
            get { return new ProviceDao(); }
        }
        public IAmphurDao AmphurDao
        {
            get { return new AmphurDao(); }
        }
        public ITambonDao TambonDao
        {
            get { return new TambonDao(); }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLUProject.Data
{
    public class EFDataContext : DbContext
    {
        public EFDataContext(string connectionString)
            : base(connectionString)
        {

        }
    }
}

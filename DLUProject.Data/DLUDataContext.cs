using BLToolkit.Data.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLUProject.Data
{
    public class DLUDataContext : DataContext
    {
        public DLUDataContext(string connectionString) : base(connectionString) { }
    }
}

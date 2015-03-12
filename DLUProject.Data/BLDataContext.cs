using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLToolkit.Mapping;
using BLToolkit.Data;
using BLToolkit.DataAccess;
using BLToolkit.Data.Linq;
namespace DLUProject.Data
{
    public class BLDataContext : DataContext
    {
        public BLDataContext(string connString)
            : base(connString)
        {
             
        }
    }
}

using DLUProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLUProject.Model
{
    public   class SingleOrganizationModel
    {
        public Department Item { get; set; }        
        public List<StaffDepartmentModel> ListStaff { get; set; }
    }
}

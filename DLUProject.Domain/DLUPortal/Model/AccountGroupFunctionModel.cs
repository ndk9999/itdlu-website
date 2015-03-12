using DLUProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLUProject.Model
{
    public class AccountGroupFunctionModel
    {
        public List<AccountGroup> AccountGroups { get; set; }
        public List<Function> Functions { get; set; }
        public List<AccountGroupFunction> GroupFunctions { get; set; }
        public bool HasPermission(int groupId, int funcId)
        {
            var gr = GroupFunctions.FirstOrDefault(c => c.GroupID == groupId && c.FunctionID == funcId);
            return gr != null;
        }
        public WorkGroup WorkGroupByFunctionID { get; set; }
    }
}
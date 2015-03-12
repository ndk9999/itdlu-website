/* 
FileName: IAccountGroupExtension.cs
Project Name: ColorLife.Data
Date Created: 13/5/2014 7:41:38 AM
Description: Auto-generated
Version: 1.0.0.0
Author:	Lê Thanh Tuấn - Khoa CNTT
Author Email: tuanitpro@gmail.com
Author Mobile: 0976060432
Author URI: http://tuanitpro.com
License: 

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DLUProject.Domain;
using DLUProject.Data;
using DLUProject.Model;
namespace DLUProject.Services
{
    public class AccountGroupExtension
    {
        private IRepository<AccountGroup> _accountGroupProxy;
        private IServices<Function> _functionProxy;
        private IRepository<AccountGroupFunction> _accountGroupFunctionProxy;
        public AccountGroupExtension(IRepository<AccountGroup> accountGroupProxy, IServices<Function> functionProxy,
            IRepository<AccountGroupFunction> accountGroupFunctionProxy)
        {
            this._accountGroupProxy = accountGroupProxy;
            this._functionProxy = functionProxy;
            this._accountGroupFunctionProxy = accountGroupFunctionProxy;
        }
        public bool CheckFunctionInGroup(int groupId, int functionId)
        {
            var exits = _accountGroupFunctionProxy.Get(c => c.GroupID == groupId && c.FunctionID == functionId);
            return exits == null;
        }
        public int AddFunctionToGroup(int groupId, int functionId)
        {
            var exits = _accountGroupFunctionProxy.Get(c => c.GroupID == groupId && c.FunctionID == functionId);
            if (exits == null)
            {
                var gr = new AccountGroupFunction { GroupID = groupId, FunctionID = functionId };
                return _accountGroupFunctionProxy.Insert2(gr);
            }
            return -1;
        }
        public int RemoveFunctionFromGroup(int groupId, int functionId)
        {
            var exits = _accountGroupFunctionProxy.Get(c => c.GroupID == groupId && c.FunctionID == functionId);
            if (exits != null)
            {
                return _accountGroupFunctionProxy.Delete(exits);
            }
            return -1;
        }
        public int RemoveAllFunctionInGroup()
        {

            return _accountGroupFunctionProxy.Delete();
        }
        public AccountGroupFunctionModel AccountGroupFunctionModel()
        {
            return new AccountGroupFunctionModel
            {
                AccountGroups = _accountGroupProxy.All(),
                Functions = _functionProxy.All(),
                GroupFunctions = _accountGroupFunctionProxy.All(),
            };
        }
        public AccountGroupFunctionModel AccountGroupFunctionModel(int accGroupId)
        {
            return new AccountGroupFunctionModel
            {
                AccountGroups = _accountGroupProxy.Table.Where(c => c.GroupID.Equals(accGroupId)).ToList(),
                Functions = _functionProxy.All(),
                GroupFunctions = _accountGroupFunctionProxy.All(),
            };
        }
        public AccountGroupFunctionModel AccountGroupFunctionModel(int accGroupId, int systemId)
        {
            var accountGroups = _accountGroupProxy.Table;
            var functions = _functionProxy.All();
            if (accGroupId > 0)
            {
                accountGroups = accountGroups.Where(c => c.GroupID.Equals(accGroupId));
            }
            if (systemId > 0)
            {
                functions = functions.Where(c => c.WorkGroup.SystemID.Equals(systemId)).ToList();
            }
            return new AccountGroupFunctionModel
            {
                AccountGroups = accountGroups.ToList(),
                Functions = functions,
                GroupFunctions = _accountGroupFunctionProxy.All(),
            };
        }
    }
}



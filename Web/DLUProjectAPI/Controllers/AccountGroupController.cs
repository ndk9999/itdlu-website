/* FileName: CategoryController.cs
Project Name: DLUProject
Date Created: 10/11/2014 10:29:06 AM
Description: Auto-generated
Version: 1.0.0.0
Author:	Lê Thanh Tuấn - Khoa CNTT
Author Email: tuanitpro@gmail.com
Author Mobile: 0976060432
Author URI: http://tuanitpro.com
License: 

*/

using System.Web.Http;
using System.Linq;
using DLUProject.Domain;
using ColorLife.Core.Helper;
using DLUProject.Services;


namespace DLUProject.API.Controllers
{

    public class AccountGroupController : ApiController
    {
        IServices<AccountGroup> _service;
        IServices<AccountInGroup> _accountInGroup;
        AccountGroupExtension _accountGroupExt;
        public AccountGroupController(IServices<AccountGroup> service, IServices<AccountInGroup> accountInGroup,
            AccountGroupExtension accountGroupExt)
        {
            this._service = service;
            this._accountInGroup = accountInGroup;
            this._accountGroupExt = accountGroupExt;
            
        }
        // GET api/account
        public JsonResponse<AccountGroup> Get()
        {
            // return new JsonResponse<string > { Success = true,  Data="Chao the gioi" };
            var items = _service.All();
            return new JsonResponse<AccountGroup> { Success = true, ListData = items };
        }

        // GET api/Category/5
        public JsonResponse<AccountGroup> Get(int id)
        {
            var model = _service.Get(id);
            return new JsonResponse<AccountGroup> { Success = model != null, Data = model };
        }

        // POST api/Category
        public JsonResponse Post([FromBody]AccountGroup value)
        {
            int rs = _service.Insert(value);
            string msg = "";
            if (rs > 0)
            {
                msg = "Thêm dữ liệu thành công.";
            }
            else
            {
                if (rs == -9999)
                {
                    msg = "Dữ liệu đã tồn tại, vui lòng nhập lại.";
                }
                else
                {
                    msg = "Thêm dữ liệu thất bại.";
                }
            }
            return new JsonResponse { Success = rs > 0, Message = string.Format(msg, "...") };
        }

        // PUT api/Category/5
        public JsonResponse Put([FromBody]AccountGroup value)
        {
            bool rs = _service.Update(value) > 0;
            string msg = "";
            if (rs)
                msg = "Cập nhật dữ liệu thành công.";
            else msg = "Cập nhật dữ liệu thất bại.";
            return new JsonResponse { Success = rs, Message = string.Format(msg, "...") };
        }

        // DELETE api/Category/5
        [HttpDelete]
        public JsonResponse Delete(int id)
        {
            string msg = "";
            bool rs = _service.Delete(id) > 0;
            if (rs)
                msg = "Xóa dữ liệu <" + id + "> thành công.";
            else msg = "Xóa dữ liệu <" + id + "> thất bại.";
            return new JsonResponse() { Success = rs, Message = string.Format(msg, "...") };
        }


        public JsonResponse GrantPermission(int id, [FromBody]AccountGroupFunction value)
        {
            string msg = ""; bool rs;
            var exists = _accountGroupExt.CheckFunctionInGroup(value.GroupID, value.FunctionID);
            if (exists)
            {
                rs = _accountGroupExt.AddFunctionToGroup(value.GroupID, value.FunctionID) > 0;
                if (rs)
                    msg = "Thêm quyền cho nhóm <" + value.GroupID + "> thành công.";
                else msg = "Thêm quyền cho nhóm <" + value.GroupID + "> thất bại.";

            }
            else
            {
                rs = _accountGroupExt.RemoveFunctionFromGroup(value.GroupID, value.FunctionID) > 0;
                if (rs)
                    msg = "Xóa quyền cho nhóm <" + value.GroupID + "> thành công.";
                else msg = "Xóa quyền cho nhóm <" + value.GroupID + "> thất bại.";
            }
            return new JsonResponse() { Success = rs, Message = string.Format(msg, "...") };
        }
    }
}

//Default



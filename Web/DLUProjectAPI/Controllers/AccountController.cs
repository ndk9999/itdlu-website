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

    public class AccountController : ApiController
    {
        IServices<Account> _service;
        IServices<AccountInGroup> _accountInGroup;
        IServices<AccountGroup> _accountGroup;
        public AccountController(IServices<Account> service, IServices<AccountInGroup> accountInGroup, IServices<AccountGroup>accountGroup)
        {
            this._service = service;
            this._accountInGroup = accountInGroup;
            this._accountGroup = accountGroup;
        }
        // GET api/account
        public JsonResponse<Account> Get()
        {
            // return new JsonResponse<string > { Success = true,  Data="Chao the gioi" };
            var items = _service.All();
            return new JsonResponse<Account> { Success = true, ListData = items };
        }

        // GET api/Category/5
        public JsonResponse<Account> Get(int id)
        {
            var model = _service.Get(id);
            return new JsonResponse<Account> { Success = model != null, Data = model };
        }

        // POST api/Category
        public JsonResponse Post([FromBody]Account value)
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
        public JsonResponse Put([FromBody]Account value)
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
        [HttpPut]
        public JsonResponse PutIsPublished(int id)
        {
            var m = _service.Get(id);
            bool curr = m.IsApproved;
            m.IsApproved = !curr;
            bool rs = _service.Update(m) > 0;
            string msg = "";
            if (rs)
                msg = "Cập nhật dữ liệu thành công.";
            else msg = "Cập nhật dữ liệu thất bại.";
            return new JsonResponse { Success = rs, Message = string.Format(msg, "...") };
        }
        [HttpPut]
        public JsonResponse PutIsLocked(int id, int ok = 1)
        {
            var m = _service.Get(id);
            bool curr = m.IsLockedOut;
            m.IsLockedOut = !curr;
            bool rs = _service.Update(m) > 0;
            string msg = "";
            if (rs)
                msg = "Cập nhật dữ liệu thành công.";
            else msg = "Cập nhật dữ liệu thất bại.";
            return new JsonResponse { Success = rs, Message = string.Format(msg, "...") };
        }
        [HttpPost]
        public JsonResponse AddAccountToGroup(int id, [FromBody]AccountInGroup value)
        {
            string msg = ""; bool rs;
          //  var group = _accountGroup.Table.FirstOrDefault(c=>c.GroupID.Equals(value.GroupID)).Name;
            var exists = _accountInGroup.Table.FirstOrDefault(c => c.AccountID.Equals(value.AccountID) && c.GroupID.Equals(value.GroupID));
            if (exists == null)
            {
                rs = _accountInGroup.Insert(new AccountInGroup { AccountID = value.AccountID, GroupID = value.GroupID }) > 0;
                if (rs)
                    msg = "Thêm Tài khoản vào nhóm <" + value.GroupID + "> thành công.";
                else msg = "Thêm Tài khoản vào nhóm <" + value.GroupID + "> thất bại.";

            }
            else
            {                
                rs = _accountInGroup.Delete(exists) > 0;
                if (rs)
                    msg = "Xóa Tài khoản khỏi nhóm <" + value.GroupID + "> thành công.";
                else msg = "Xóa Tài khoản khỏi nhóm <" + value.GroupID + "> thất bại.";
            }
            return new JsonResponse() { Success = rs, Message = string.Format(msg, "...") };
        }
    }
}

 



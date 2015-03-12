/* FileName: PagesController.cs
Project Name: DLUProject
Date Created: 14/11/2014 2:49:19 PM
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
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ColorLife.Core.Helper;
using DLUProject.Domain;
using DLUProject.Data;
using DLUProject.Services;
using DLUProject.Domain;
 

namespace DLUProject.API.Controllers
{
    public class PagesController : ApiController
    {
        public IServices<Pages> _service { get; set; }
        public PagesController(IServices<Pages> service)
        {
            this._service = service;
        }
        // GET api/account
        public JsonResponse<Pages> Get()
        {
            var items = _service.All();
            return new JsonResponse<Pages> { Success = true, ListData = items };
        }

        // GET api/Pages/5
        public JsonResponse<Pages> Get(int id)
        {
            var model = _service.Get(id);
            return new JsonResponse<Pages> { Success = model != null, Data = model };
        }

        // POST api/Pages
        public JsonResponse Post([FromBody]Pages value)
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
            return new JsonResponse { Success = rs>0, Message = string.Format(msg, "...") };
        }

        // PUT api/Pages/5
        public JsonResponse Put([FromBody]Pages value)
        {           
            bool rs = _service.Update(value) > 0;
            string msg = "";
            if (rs)
                msg = "Cập nhật dữ liệu thành công.";
            else msg = "Cập nhật dữ liệu thất bại.";
            return new JsonResponse { Success = rs, Message = string.Format(msg, "...") };
        }

        // DELETE api/Pages/5
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
    }
}

//Default



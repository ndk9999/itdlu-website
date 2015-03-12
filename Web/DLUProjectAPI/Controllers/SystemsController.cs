﻿/* FileName: CategoryController.cs
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

using DLUProject.Domain;
 
using ColorLife.Core.Helper;
using DLUProject.Services;


namespace DLUProject.API.Controllers
{
    
    public class SystemsController : ApiController
    {
        IServices<Systems> _service;

        public SystemsController(IServices<Systems> service)
        {
            this._service = service;
        }
        // GET api/account
        public JsonResponse<Systems> Get()
        {
               // return new JsonResponse<string > { Success = true,  Data="Chao the gioi" };
              var items = _service.All();
              return new JsonResponse<Systems> { Success = true, ListData = items };
        }

        // GET api/Category/5
        public JsonResponse<Systems> Get(int id)
        {
            var model = _service.Get(id);
            return new JsonResponse<Systems> { Success = model != null, Data = model };
        }

        // POST api/Category
        public JsonResponse Post([FromBody]Systems value)
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

        // PUT api/Category/5
        public JsonResponse Put([FromBody]Systems value)
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
            bool curr = m.IsEnabled;
            m.IsEnabled = !curr;
            bool rs = _service.Update(m) > 0;
            string msg = "";
            if (rs)
                msg = "Cập nhật dữ liệu thành công.";
            else msg = "Cập nhật dữ liệu thất bại.";
            return new JsonResponse { Success = rs, Message = string.Format(msg, "...") };
        }         
    }
}

//Default


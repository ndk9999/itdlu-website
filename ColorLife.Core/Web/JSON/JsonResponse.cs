/* FileName: JsonResponse.cs
Project Name: ColorLife
DateCreated: 
Description: 
Version: 1.0
Author:	Lê Thanh Tuấn
Author Email: tuanitpro@gmail.com
Author Mobile: 097 6060 432
Author URI:	http://facebook.com/tuanitpro
License: 
*/
using System.Collections.Generic;
namespace ColorLife.Core.Helper
{
    public class JsonResponse
    {
        public string Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
    public class JsonResponse<T>
    {
        public T Data { get; set; }
        public List<T> ListData { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
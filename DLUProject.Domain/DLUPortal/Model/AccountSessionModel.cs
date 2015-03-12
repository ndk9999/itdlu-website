using DLUProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DLUProject.Model
{
    public class AccountSessionModel
    {
        public Account Account { get; set; }
        public string FullName { get { return Account.FullName; } }
        public List<AccountGroup> InGroup { get; set; }
        public List<Systems> InSystem { get; set; }
        public List<WorkGroup> InWorkgroup { get; set; }
        public List<Function> InFunction { get; set; }
        public bool IsAuthenticated { get; set; }
        /// <summary>
        /// Co phan quyen cho MVC. Cap nhat 16/07/2014
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool IsAllowAccess(string controller, string action)
        {
            if (string.IsNullOrEmpty(controller)) controller = "dashboard";
          //  if (string.IsNullOrEmpty(action) || action.Equals("index")) action = "list";

            var exits = InFunction.FirstOrDefault(c => c.WorkGroup.Url.ToLower().Equals(controller.ToLower()) && c.Url.ToLower().Equals(action.ToLower()));
            return exits != null;
        }
        private Dictionary<string, int> dic;
        private string GenerateKey(string area, string controller)
        {
            if (area == null)
                area = "";
            return (area + "-" + controller).ToLower();
        }
        public bool IsAllowAccess(string area, string controller, string action)
        {
            var access = false;
            if (area != null)
                area = area.ToLower();
            controller = controller.ToLower();
            action = action.ToLower();
            var key = GenerateKey(area, controller);
            if (dic.ContainsKey(key))
            {
                var flags = dic[key];
                if (flags > 0)
                {
                    if (action == "" || action.StartsWith("index") || action.StartsWith("list"))
                    {
                       // access = Enums.HasFlag(flags, AccessFlag.Read);
                        access = true;
                    }
                    else if (action.StartsWith("create"))
                    {
                       // access = Enums.HasFlag(flags, AccessFlag.Create);
                        access = true;
                    }
                    else if (action.StartsWith("edit"))
                    {
                       // access = Enums.HasFlag(flags, AccessFlag.Edit);
                        access = true;
                    }
                    else
                    {
                        access = true;
                    }
                }
            }
            return access;
        }
        public bool IsAllowAccess(string pageUrl, AccessFlag flags)
        {
            string folder = "";
            string page = "";

            string[] split = pageUrl.ToString().Split(new Char[] { '/' });
            folder = split[split.Length - 2].ToLower();

            //var controller = System.Web.HttpContext.Current.Request.QueryString["controller"];
            page = flags.ToString().ToLower() ?? "list";

            var exits = InFunction.FirstOrDefault(c => c.WorkGroup.Url.ToLower().Equals(folder) && c.Url.ToLower().Equals(page));
            return exits != null;

            //var newPage = page.ToLower();

            //string[] aa = new string[InFunction.Count];
            //for (int i = 0; i < InFunction.Count; i++)
            //{
            //    aa[i] = InFunction[i].UrlFull;
            //}
            //if (newPage.Contains("controller"))
            //    return aa.Any(newPage.Contains);

            //if (newPage.Contains("/?") || newPage.Contains("/default?"))
            //    newPage = newPage.Substring(0, newPage.LastIndexOf("/"));

            //return InFunction.Any(c => c.UrlFull.ToLower().Contains(newPage));
        }
        /// <summary>
        /// Code phan quyen. Cap nhat 28/06/2014
        /// </summary>
        /// <returns></returns>
        public bool IsAllowAccess()
        {
            string folder = "";
            string page = "";
            var currentUrl = System.Web.HttpContext.Current.Request.RawUrl;
            string[] split = currentUrl.ToString().Split(new Char[] { '/' });
            folder = split[split.Length - 2].ToLower();

            var controller = System.Web.HttpContext.Current.Request.QueryString["controller"];
            page = string.IsNullOrEmpty(controller) ? "list" : controller.ToLower();

            var exits = InFunction.FirstOrDefault(c => c.WorkGroup.Url.ToLower().Equals(folder) && c.Url.ToLower().Equals(page));
            return exits != null;
        }
        public bool HasFunction { get { return InFunction.Count > 0; } }

        public  bool CanAccess()
        {
            var area = HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"].ToString();
            var controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            var action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            return false;// CanAccess(area, controller, action);
        }
    }
}

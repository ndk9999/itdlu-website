using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColorLife.Core.Web
{
    [DefaultProperty("ColorLifeVideoPlayer")]
    [ToolboxData("<{0}:ColorLifeVideoPlayer runat=server></{0}:ColorLifeVideoPlayer>")]
    //[ToolboxBitmap(typeof(ColorLifeVideoPlayer), "icon.bmp")]
    public class ColorLifeVideoPlayer : WebControl
    {
        public string Url
        {
            get
            {
                string s = (string)ViewState["Url"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["Url"] = value;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string html = "<video width=\"320\" height=\"240\" controls>";
            html += "<source src=\"" + this.Url + "\" type=\"video/mp4\">";
            html += "<source src=\"" + this.Url + "\" type=\"video/ogg\">";
            html += "Your browser does not support the video tag.";
            html += "</video>";
            writer.Write(html);
        }
    }
}

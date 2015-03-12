using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColorLife.Core.Web
{
    [DefaultValue("ColorLifeButtonEx")]
    [ToolboxData("<{0}:ColorLifeButtonEx runat=\"server\"></{0}:ColorLifeButtonEx>")]
    public class ColorLifeButtonEx : Button
    {
        public string IconClass
        {
            get
            {
                string s = (string)ViewState["IconClass"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["IconClass"] = value;
            }
        }
        
        public bool IsShowButton
        {
            get
            {
                object o = ViewState["IsShowButton"];
                if (o != null)
                    return (bool)o;
                return false; // Default value
            }
            set
            {
                bool oldValue = IsShowButton;
                if (value != oldValue)
                {
                    ViewState["IsShowButton"] = value;
                }
            }
        }
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (!IsShowButton)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:none");
            }
            base.AddAttributesToRender(writer);
        }
        protected override void RenderContents(HtmlTextWriter writer)
        {            
            base.RenderContents(writer);
            if (!IsShowButton)
            {
                writer.Write("&nbsp;");
                writer.Write("<label for=\"" + this.ID + "\"  class=\"" + this.CssClass + "\" rel=\"tooltip\" title=\"" + this.ToolTip + "\"><i class=\"" + IconClass + "\"></i>" + this.Text + "</label>");
            }
        }
    }
}

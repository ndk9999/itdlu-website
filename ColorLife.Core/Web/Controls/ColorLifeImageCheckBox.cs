using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
namespace ColorLife.Core.Web
{
    public class ColorLifeImageCheckBox : CheckBox
    {
      
        protected override void Render(HtmlTextWriter output)
        {
            var image = new Image();
            image.ID = this.ClientID + "_Image";
            image.AlternateText = this.Checked.ToString();
            if (!this.ShowCheckBox)
            {
                base.Style.Add("display", "none");
            }
            if (this.Checked)
            {
                image.ImageUrl = this.ImageChecked;
            }
            else
            {
                image.ImageUrl = this.ImageUnchecked;
            }
            image.RenderControl(output);
            base.Render(output);
        }

        [Bindable(true), DefaultValue(0), Description("Specifies Image for the Checked Image"), Category("Appearance")]
        public string ImageChecked
        {
            get
            {
                string str = (string)this.ViewState["ImageChecked"];
                if (str == null)
                {
                    return "~/images/tick_circle.png";
                }
                return str;
            }
            set
            {
                this.ViewState["ImageChecked"] = value;
            }
        }

        [Category("Appearance"), Description("Specifies Image for the Checked Image"), Bindable(true), DefaultValue(0)]
        public string ImageUnchecked
        {
            get
            {
                string str = (string)this.ViewState["ImageUnchecked"];
                if (str == null)
                {
                    return "~/images/cross_circle.png";
                }
                return str;
            }
            set
            {
                this.ViewState["ImageUnchecked"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(0), Description("Show or hide the checkbox")]
        public bool ShowCheckBox
        {
            get
            {
                if (this.ViewState["ShowCheckBox"] == null)
                {
                    return false;
                }
                return (bool)this.ViewState["ShowCheckBox"];
            }
            set
            {
                this.ViewState["ShowCheckBox"] = value;
            }
        }
    }
}


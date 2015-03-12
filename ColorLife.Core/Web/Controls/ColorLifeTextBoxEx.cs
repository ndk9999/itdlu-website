using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
namespace ColorLife.Core.Web
{
    [DefaultValue("ColorLifeRequiredTextBox")]
    [ToolboxData("<{0}:ColorLifeTextBoxEx runat=server></{0}:ColorLifeTextBoxEx>")]
    public class ColorLifeTextBoxEx : CompositeControl
    {
        private TextBox textBoxEx;
        private RequiredFieldValidator rqValidatorTextBoxEx;

        public string Text
        {
            get
            {
                EnsureChildControls();
                return textBoxEx.Text;
            }
            set
            {
                EnsureChildControls();
                textBoxEx.Text = value;
            }
        }


        public string RequiredErrorMessage
        {
            get
            {
                string s = (string)ViewState["RequiredTextBoxRequiredErrorMessage"];
                return (s == null) ? "Dữ liệu bắt buộc nhập" : s;
            }
            set
            {
                ViewState["RequiredTextBoxRequiredErrorMessage"] = value;
            }
        }

        public string CustomeCssClass
        {
            get
            {
                string s = (string)ViewState["CustomeCssClass"];
                return (s == null) ? "form-control" : s;
            }
            set
            {
                ViewState["CustomeCssClass"] = value;
            }
        }
        public string Placeholder
        {
            get
            {
                string s = (string)ViewState["Placeholder"];
                return (s == null) ? this.Text : s;
            }
            set
            {
                ViewState["Placeholder"] = value;

            }
        }
        public string MinLength
        {
            get
            {
                string s = (string)ViewState["MinLength"];
                return (s == null) ? this.Text : s;
            }
            set
            {
                ViewState["MinLength"] = value;

            }
        }
        public string ValidationGroup
        {
            get
            {
                string s = (string)ViewState["ValidationGroup"];
                return (s == null) ? string.Empty : s;
            }
            set
            {
                ViewState["ValidationGroup"] = value;

            }
        }
        public bool IsVal
        {
            get
            {
                object o = ViewState["IsVal"];
                if (o != null)
                    return (bool)o;
                return false; // Default value
            }
            set
            {
                bool oldValue = IsVal;
                if (value != oldValue)
                {
                    ViewState["IsVal"] = value;
                }
            }
        }
        protected override void CreateChildControls()
        {
            textBoxEx = new TextBox();
            textBoxEx.ID = "ColorLifeTextBoxEx" + this.ID;
            textBoxEx.CssClass = CustomeCssClass;
            textBoxEx.Attributes.Add("placeholder", Placeholder);
          
            textBoxEx.Attributes.Add("rel", "tooltip");
            textBoxEx.Attributes.Add("title", this.ToolTip);

            if (IsVal)
            {

                textBoxEx.Attributes.Add("required", "required");
                textBoxEx.Attributes.Add("data-rule-minlength", MinLength);
                textBoxEx.Attributes.Add("data-rule-required", "true");
            }

            this.Controls.Add(textBoxEx);

           
            if (IsVal)
            {
               
                rqValidatorTextBoxEx = new RequiredFieldValidator();
                rqValidatorTextBoxEx.ID = "ColorLifeTextBoxExrq" + this.ID;
                rqValidatorTextBoxEx.ControlToValidate = "ColorLifeTextBoxEx" + this.ID;
                rqValidatorTextBoxEx.ErrorMessage = RequiredErrorMessage;
                rqValidatorTextBoxEx.ForeColor = System.Drawing.Color.Red;
                rqValidatorTextBoxEx.Display = ValidatorDisplay.Dynamic;

                if (!string.IsNullOrEmpty(ValidationGroup))
                    rqValidatorTextBoxEx.ValidationGroup = ValidationGroup;

                this.Controls.Add(rqValidatorTextBoxEx);
            }
            base.CreateChildControls();
        }
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }
}

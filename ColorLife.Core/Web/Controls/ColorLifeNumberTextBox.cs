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
    [DefaultValue("ColorLifeNumberTextBox")]
    [ToolboxData("<{0}:ColorLifeNumberTextBox runat=server></{0}:ColorLifeNumberTextBox>")]
    public class ColorLifeNumberTextBox : CompositeControl
    {
        private TextBox textBoxEmail;
        private RequiredFieldValidator rqValidatorNumber;
        private RegularExpressionValidator rqExpValidatorNumber;
        
        public string Text
        {
            get {
                EnsureChildControls();
                return textBoxEmail.Text;
            }
            set {
                EnsureChildControls();
                textBoxEmail.Text = value;
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
        public string RequiredErrorMessage
        {
            get
            {
                string s = (string)ViewState["RequiredErrorMessage"];
                return (s == null) ? "Dữ liệu bắt buộc nhập" : s;
            }
            set
            {
                ViewState["RequiredErrorMessage"] = value;
            }
        }
        
        
        public string NumberExpressionErrorMessage
        {
            get
            {
                string s = (string)ViewState["NumberExpressionErrorMessage"];
                return (s == null) ? "Hãy nhập số." : s;
            }
            set
            {
                ViewState["NumberExpressionErrorMessage"] = value;
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
            textBoxEmail = new TextBox();
            textBoxEmail.ID = "ColorLifeNumberTextBox" + this.ID;
            textBoxEmail.CssClass = CustomeCssClass;
            textBoxEmail.Attributes.Add("placeholder", Placeholder);

            textBoxEmail.Attributes.Add("rel", "tooltip");
            textBoxEmail.Attributes.Add("title", this.ToolTip);
            textBoxEmail.Attributes.Add("data-rule-number", "true");

            if (IsVal)
            {

                textBoxEmail.Attributes.Add("required", "required");
                textBoxEmail.Attributes.Add("data-rule-minlength", MinLength);
                textBoxEmail.Attributes.Add("data-rule-required", "true");
            }


            if (!string.IsNullOrEmpty(ValidationGroup))
                textBoxEmail.ValidationGroup = ValidationGroup;
            this.Controls.Add(textBoxEmail);
             
            if (IsVal)
            {

                rqValidatorNumber = new RequiredFieldValidator();
                rqValidatorNumber.ID = "ColorLifeNumberTextBoxRq" + this.ID;
                rqValidatorNumber.ControlToValidate = "ColorLifeNumberTextBox" + this.ID;
                rqValidatorNumber.ErrorMessage = RequiredErrorMessage;
                rqValidatorNumber.ForeColor = System.Drawing.Color.Red;
                rqValidatorNumber.Display = ValidatorDisplay.Dynamic;

                if (!string.IsNullOrEmpty(ValidationGroup))
                    rqValidatorNumber.ValidationGroup = ValidationGroup;
                this.Controls.Add(rqValidatorNumber);
            }

            rqExpValidatorNumber = new RegularExpressionValidator();
            rqExpValidatorNumber.ID = "ColorLifeRqNumberTextBoxEx" + this.ID;
            rqExpValidatorNumber.ControlToValidate = "ColorLifeNumberTextBox" + this.ID;

            rqExpValidatorNumber.ErrorMessage = NumberExpressionErrorMessage;
            rqExpValidatorNumber.ForeColor = System.Drawing.Color.Red;

            rqExpValidatorNumber.ValidationExpression = "\\d+";
            rqExpValidatorNumber.Display = ValidatorDisplay.Dynamic;


            if (!string.IsNullOrEmpty(ValidationGroup))
                rqExpValidatorNumber.ValidationGroup = ValidationGroup;

            this.Controls.Add(rqExpValidatorNumber);

            base.CreateChildControls();
        }
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }
}

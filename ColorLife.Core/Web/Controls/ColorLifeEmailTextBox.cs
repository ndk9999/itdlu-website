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
    [DefaultValue("ColorLifeEmailTextBox")]
    [ToolboxData("<{0}:ColorLifeEmailTextBox runat=server></{0}:ColorLifeEmailTextBox>")]
    public class ColorLifeEmailTextBox : CompositeControl
    {
        private TextBox textBoxEmail;
        private RequiredFieldValidator rqValidatorEmail;
        private RegularExpressionValidator rqExpValidatorEmail;
        
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
        
        
        public string EmailExpressionErrorMessage
        {
            get
            {
                string s = (string)ViewState["EmailExpressionErrorMessage"];
                return (s == null) ? "Email không đúng." : s;
            }
            set
            {
                ViewState["EmailExpressionErrorMessage"] = value;
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
            textBoxEmail.ID = "ColorLifeEmailTextBox" + this.ID;
            textBoxEmail.CssClass = CustomeCssClass;
            textBoxEmail.Attributes.Add("placeholder", Placeholder);
       
            textBoxEmail.Attributes.Add("data-rule-email", "true");

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
                rqValidatorEmail = new RequiredFieldValidator();
                rqValidatorEmail.ID = "ColorLifeEmailTextBoxRq" + this.ID;
                rqValidatorEmail.ControlToValidate = "ColorLifeEmailTextBox" + this.ID;
                rqValidatorEmail.ErrorMessage = RequiredErrorMessage;
                rqValidatorEmail.ForeColor = System.Drawing.Color.Red;
                rqValidatorEmail.Display = ValidatorDisplay.Dynamic;

                if (!string.IsNullOrEmpty(ValidationGroup))
                    rqValidatorEmail.ValidationGroup = ValidationGroup;
                this.Controls.Add(rqValidatorEmail);
            }

            rqExpValidatorEmail = new RegularExpressionValidator();
            rqExpValidatorEmail.ID = "ColorLifeRqEmailTextBoxEx" + this.ID;
            rqExpValidatorEmail.ControlToValidate = "ColorLifeEmailTextBox" + this.ID;

            rqExpValidatorEmail.ErrorMessage = EmailExpressionErrorMessage;
            rqExpValidatorEmail.ForeColor = System.Drawing.Color.Red;

            rqExpValidatorEmail.ValidationExpression = "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            rqExpValidatorEmail.Display = ValidatorDisplay.Dynamic;


            if (!string.IsNullOrEmpty(ValidationGroup))
                rqExpValidatorEmail.ValidationGroup = ValidationGroup;

            this.Controls.Add(rqExpValidatorEmail);

            base.CreateChildControls();
        }
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }
}

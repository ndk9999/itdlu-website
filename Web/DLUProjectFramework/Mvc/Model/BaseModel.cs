using System.Collections.Generic;
using System.Web.Mvc;

namespace DLUProjectFramework.Mvc
{
    /// <summary>
    /// Base DLU model
    /// </summary>
    public partial class BaseModel
    {
        public BaseModel()
        {
            this.CustomProperties = new Dictionary<string, object>();
        }
        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }

        /// <summary>
        /// Use this property to store any custom value for your models. 
        /// </summary>
        public Dictionary<string, object> CustomProperties { get; set; }
    }
}
 

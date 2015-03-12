using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;
namespace DLUProjectFramework.Mvc
{
    public static class TextBoxPlaceHolderExtensions
    {

        public static MvcHtmlString TextBoxPlaceHolder(this HtmlHelper helper, string name)
        {
            return TextBoxPlaceHolder(helper, name, null, name);
        }

        public static MvcHtmlString TextBoxPlaceHolder(this HtmlHelper helper, string name, string text,string placeholder)
        {
            return TextBoxPlaceHolder(helper, name, text, placeholder, null);
        }

        public static MvcHtmlString TextBoxPlaceHolder(this HtmlHelper helper, string name, string text, string placeholder, object htmlAttributes)
        {
            TagBuilder textBox = new TagBuilder("input");
            textBox.Attributes.Add("type", "text");
            textBox.Attributes.Add("name", name);
            textBox.Attributes.Add("id", name);
            textBox.Attributes.Add("placeholder", placeholder);
            if (!string.IsNullOrEmpty(text))
            {
                textBox.Attributes.Add("value", text);
            }

            textBox.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(textBox.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString TextBoxPlaceHolderFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.TextBoxPlaceHolderFor(expression, dict);
        }
        public static MvcHtmlString TextBoxPlaceHolderFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.TextBoxPlaceHolderFor(expression, htmlAttributes);
        }

        public static MvcHtmlString TextBoxPlaceHolderFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (!String.IsNullOrEmpty(labelText))
            {
                if (htmlAttributes == null)
                {
                    htmlAttributes = new Dictionary<string, object>();
                }
                htmlAttributes.Add("placeholder", labelText);
            }
            return html.TextBoxFor(expression, htmlAttributes);
        }

    }
}

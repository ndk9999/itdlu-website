using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace DLUProjectFramework.Mvc
{
    public static class TextBoxEmailExtensions
    {
        public static MvcHtmlString TextBoxEmail(this HtmlHelper helper, string name)
        {
            return TextBoxEmail(helper, name, null, name);
        }

        public static MvcHtmlString TextBoxEmail(this HtmlHelper helper, string name, string text, string placeholder)
        {
            return TextBoxEmail(helper, name, text, placeholder, null);
        }

        public static MvcHtmlString TextBoxEmail(this HtmlHelper helper, string name, string text, string placeholder, object htmlAttributes)
        {
            TagBuilder textBox = new TagBuilder("input");
            textBox.Attributes.Add("type", "email");
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

        public static MvcHtmlString TextBoxEmailFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.TextBoxEmailFor(expression, dict);
        }
        public static MvcHtmlString TextBoxPlaceHolderFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.TextBoxEmailFor(expression, htmlAttributes);
        }

        public static MvcHtmlString TextBoxEmailFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
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
            TagBuilder textBox = new TagBuilder("input");
            textBox.Attributes.Add("type", "email");
            textBox.Attributes.Add("name", metadata.PropertyName);
            textBox.Attributes.Add("id", metadata.PropertyName);
                       
            textBox.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(textBox.ToString(TagRenderMode.Normal));
           // return html.TextBoxFor(expression, htmlAttributes);
        }
    }
}

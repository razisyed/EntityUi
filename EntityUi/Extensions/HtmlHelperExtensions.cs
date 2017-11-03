using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using EntityUi.Helpers;

namespace EntityUi.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString TryPartial(this HtmlHelper helper, string viewName, object model)
        {
            try
            {
                return helper.Partial(viewName, model);
            }
            catch (Exception)
            {
            }
            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString Editor(this HtmlHelper helper, string expression, Dictionary<string, object> htmlAttributes)
        {
            const string tagEnd = ">";
            var html = helper.Editor(expression).ToString();
            var tagEndIndex = html.IndexOf(tagEnd);

            if (tagEndIndex > 0)
            {
                foreach (var attribute in htmlAttributes)
                {
                    html = html.Insert(tagEndIndex, string.Format(" {0}=\"{1}\" ", attribute.Key, attribute.Value));
                    tagEndIndex = html.IndexOf(tagEnd, StringComparison.Ordinal);
                }
            }
            return MvcHtmlString.Create(html);
        }

        public static string GetDataBind(this HtmlHelper helper, string expresion)
        {
            var metadata = ModelMetadata.FromStringExpression(expresion, helper.ViewData);
            return metadata.AdditionalValues.ContainsKey("DataBindText") ? 
                    (string) metadata.AdditionalValues["DataBindText"] : "";
        }

        public static MvcHtmlString LabelKo(this HtmlHelper helper, string expression, object htmlAttributes, string databind)
        {

            var html = helper.Label(expression, htmlAttributes).ToString();
            html = Helper.AddKnockoutBinding(html, expression, databind, false);
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString EditorKo(this HtmlHelper helper, string expression, string databind)
        {            
            var html = helper.Editor(expression).ToString();
            html = Helper.AddKnockoutBinding(html, expression, databind);
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString EditorKo(this HtmlHelper helper, string expression, string databind, string cssclass)
        {
            var html = helper.Editor(expression, new { htmlAttributes = new { @class = cssclass } }).ToString();
            html = Helper.AddKnockoutBinding(html, expression, databind);
            
            return MvcHtmlString.Create(html);
        }

        public static string GetHelpTip(this HtmlHelper helper, string expresion)
        {
            var metadata = ModelMetadata.FromStringExpression(expresion, helper.ViewData);
            return metadata.AdditionalValues.ContainsKey("HelpTipText") ?
                    (string)metadata.AdditionalValues["HelpTipText"] : "";
        }

    }
}
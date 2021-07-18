using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace richmedical.Helper
{
    public static class CustomHelper
    {
        public static IHtmlString CustomCheckBox(string id)
        {
            TagBuilder tb = new TagBuilder("input");
            tb.Attributes.Add("type", "checkBox");
            tb.Attributes.Add("id", id);
            return new MvcHtmlString(tb.ToString());
        }
        public static MvcHtmlString BootstrapCheckBoxFor<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, bool>> expression)
        {

            TagBuilder innerContainer = new TagBuilder("div");
            innerContainer.AddCssClass("col-sm-5");
            innerContainer.InnerHtml = helper.CheckBoxFor(expression, new { @class = "form-control" }).ToString();

            StringBuilder html = new StringBuilder();
            html.Append(helper.LabelFor(expression, new { @class = "col-sm-3 control-label" }));
            html.Append(innerContainer.ToString());

            TagBuilder outerContainer = new TagBuilder("div");
            outerContainer.AddCssClass("form-group");
            outerContainer.InnerHtml = html.ToString();

            return MvcHtmlString.Create(outerContainer.ToString());

        }

        public static string SaveImg(string imgBase64, System.Web.HttpServerUtilityBase server)
        {
            var name = Guid.NewGuid().ToString() + ".png";
            var userfolderpath = Path.Combine(server.MapPath("~/Images/"), name);
            var img = imgBase64;
            byte[] imageBytes = Convert.FromBase64String(img.Replace("data:image/png;base64,", ""));
            System.IO.File.WriteAllBytes(userfolderpath, imageBytes);

            return name;
        }
        public static void DeleteImg(string imgName, System.Web.HttpServerUtilityBase server)
        {
            string fullPath = server.MapPath("~/Images/" + imgName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

    }
}
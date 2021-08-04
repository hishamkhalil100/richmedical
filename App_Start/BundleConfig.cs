using System.Web;
using System.Web.Optimization;

namespace richmedical
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                "~/Scripts/Public/jquery.js",
                "~/Scripts/Public/jquery-ui.js",
                "~/Scripts/Public/tilt.jquery.min.js",
                "~/Scripts/Public/jquery.fancybox.js",
                "~/Scripts/Public/jquery.paroller.min.js",
                "~/Scripts/Public/jquery.mCustomScrollbar.concat.min.js",
                "~/Scripts/Public/bootstrap.min.js",
                "~/Scripts/Public/popper.min.js",
                "~/Scripts/Public/appear.js",
                "~/Scripts/Public/parallax.min.js",
                "~/Scripts/Public/jquery.bootstrap-touchspin.js",
                "~/Scripts/Public/owl.js",
                "~/Scripts/Public/wow.js",
                "~/Scripts/Public/nav-tool.js",
                "~/Scripts/Public/script.js",
                "~/Scripts/Public/color-settings.js"));



            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/Public/bootstrap.css"));
    
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/Admin/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/Admin/jquery-ui.min.js"));

         //   BundleTable.EnableOptimizations = true;
        }
    }
}

using System.Web;
using System.Web.Optimization;

namespace Libra.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/scrollreveal").Include(
                     "~/Scripts/scrollreveal.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/waypoints").Include(
                     "~/Scripts/jquery.waypoints.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/counterup").Include(
                     "~/Scripts/jquery.counterup.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/carousel").Include(
                    "~/Scripts/owl.carousel.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/isotope").Include(
                    "~/Scripts/isotope.pkgd.min.js",
                    "~/Scripts/isotope_custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/masonry").Include(
                   "~/Scripts/masonry.pkgd.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/prettyPhoto").Include(
                   "~/Scripts/jquery.prettyPhoto.js"));

            bundles.Add(new ScriptBundle("~/bundles/theme").Include(
                   "~/Scripts/theme.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/owl.carousel.min.css",
                      "~/Content/animate.min.css",
                      "~/Content/prettyPhoto.css",
                      "~/Content/default.css",
                      "~/Content/typography.css",
                      "~/Content/style.css",
                      "~/Content/custom.css",
                      "~/Content/responsive.css",
                      "~/Content/site.css"));
        }
    }
}

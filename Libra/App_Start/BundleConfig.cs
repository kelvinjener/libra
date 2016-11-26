using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace Libra
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.css",
                     "~/fonts/css/font-awesome.min.css",
                     "~/Content/animate.min.css",
                     "~/Content/icheck/flat/green.css",
                     "~/Content/maps/jquery-jvectormap-2.0.3.css",
                     "~/Content/icheck/flat/green.css",
                     "~/Content/floatexamples.css",
                     "~/Content/switchery/switchery.min.css",
                     "~/Content/datatables/tools/css/dataTables.tableTools.css",
                     "~/Content/datatables/tools/css/responsive.bootstrap.min.css",
                     "~/Content/custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Scripts/jquery-3.1.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                      "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/Scripts/bootstrap.js",
                       "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/nicescroll").Include(
                        "~/Scripts/nicescroll/jquery.nicescroll.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/progressbar").Include(
                        "~/Scripts/progressbar/bootstrap-progressbar.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/nprogress").Include(
                        "~/Scripts/nprogress.js"));

            bundles.Add(new ScriptBundle("~/bundles/icheck").Include(
                        "~/Scripts/icheck/icheck.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/pace").Include(
                        "~/Scripts/pace/pace.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                       "~/Scripts/datatables/js/jquery.dataTables.js",
                       "~/Scripts/datatables/js/dataTables.responsive.min.js",
                       "~/Scripts/datatables/js/responsive.bootstrap.min.js",
                       "~/Scripts/datatables/tools/js/dataTables.tableTools.js"));


            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                        "~/Scripts/custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/flot").Include(
                        "~/Scripts/flot/jquery*",
                        "~/Scripts/flot/date.js",
                        "~/Scripts/flot/curvedLines.js"));

            bundles.Add(new ScriptBundle("~/bundles/gauge").Include(
                        "~/Scripts/gauge/gauge.min.js",
                        "~/Scripts/gauge/gauge_demo.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                        "~/Scripts/moment/moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                        "~/Scripts/datepicker/daterangepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/chartjs").Include(
                        "~/Scripts/chartjs/chart.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/excanvas").Include(
                        "~/Scripts/excanvas.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/skycons").Include(
                        "~/Scripts/skycons/skycons.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/maps").Include(
                        "~/Scripts/maps/jquery*",
                        "~/Scripts/maps/gdp-data.js"));

            bundles.Add(new ScriptBundle("~/bundles/switchery").Include(
                       "~/Scripts/switchery/switchery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/notify").Include(
                        "~/Scripts/notify/pnotify.core.js",
                        "~/Scripts/notify/pnotify.buttons.js",
                        "~/Scripts/notify/pnotify.nonblock.js"));



            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/respond.min.js",
                    DebugPath = "~/Scripts/respond.js",
                });
        }
    }
}
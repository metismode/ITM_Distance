using System.Web;
using System.Web.Optimization;

namespace Distance.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/script").Include(

                     "~/Scripts/JS/bootstrap.js",
                     "~/Scripts/JS/moment.js",
                     "~/Scripts/JS/jquery-2.1.3.min.js",
                     "~/Scripts/JS/bootstrapValidator.js",
                     "~/Scripts/JS/bootstrap-select.js",
                     "~/Scripts/JS/bootbox.js",
                     "~/Scripts/JS/bootstrap-tagsinput.js",
                     "~/Scripts/JS/fileinput.js"

                    ));

            bundles.Add(new StyleBundle("~/css").Include(


                      //"~/Content/CSS/bootstrap_office.css",
                      //"~/Content/CSS/bootstrap.min.css", ///CSS new //bootswatch.com/cosmo/

                      "~/Content/CSS/bootstrap.css", ///CSS new //bootswatch.com/flatly/
                      "~/Content/CSS/DXbootstrap.css", ///CSS new DX
                      "~/Content/CSS/bootstrap-responsive.css",
                      "~/Content/CSS/bootstrapValidator.css",
                      "~/Content/CSS/bootstrap-select.css",
                      "~/Content/CSS/bootstrap-glyphicons.css",
                      "~/Content/CSS/bootstrap-tagsinput.css",
                      "~/Content/CSS/fileinput.css"
                    
                      ));



            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));




        }
    }
}

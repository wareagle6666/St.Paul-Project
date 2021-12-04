using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace WebApplication2
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/app/home.viewmodel.js",
                "~/Scripts/app/_run.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.

            bundles.Add(new Bundle("~/bundles/Theme").Include(
                "~/Scripts/jquery-3.5.1.min.js",
                 "~/Scripts/bootstrap.bundle.min.js",
                  "~/Scripts/jquery.ajaxchimp.min.js",
                   "~/Scripts/jquery.appear.min.js",
                    "~/Scripts/jquery.circle-progress.min.js",
                     "~/Scripts/jquery.magnific-popup.min.js",
                      "~/Scripts/jquery.validate.min.js",
                       "~/Scripts/nouislider.min.js",
                        "~/Scripts/odometer.min.js",
                         "~/Scripts/swiper.min.js",
                          "~/Scripts/tiny-slider.min.js",
                           "~/Scripts/wNumb.min.js",
                            "~/Scripts/wow.js",
                             "~/Scripts/isotope.js",
                              "~/Scripts/countdown.min.js",
                               "~/Scripts/owl.carousel.min.js",
                                "~/Scripts/halpes.js"));



            bundles.Add(new ScriptBundle("~/bundles/Scripts").Include(
                "~/Scripts/fontawesome/all.js"));



            bundles.Add(new ScriptBundle("~/bundles/Scripts/fontawesome").Include(
                "~/Scripts/fontawesome/all.js", "~/Scripts/fontawesome/solid.js", "~/Scripts/fontawesome/fontawesome.js"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
             "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css",
                 "~/Content/Site.css", "~/Content/fontawesome.css", "~/Content/fontawesome-all.css", "~/Content/solid.css", "~/Content/bootstrap.min.css"
                 , "~/Content/animate.min.css"
                 , "~/Content/all.min.css"
                 , "~/Content/jarallax.css"
                 , "~/Content/jquery.magnific-popup.css"
                 , "~/Content/nouislider.min.css"
                 , "~/Content/nouislider.pips.css"
                 , "~/Content/odometer.min.css"
                 , "~/Content/swiper.min.css"
                 , "~/Content/style.css"
                 , "~/Content/tiny-slider.min.css"
                 , "~/Content/stylesheet.css"
                 , "~/Content/owl.carousel.min.css"
                 , "~/Content/owl.theme.default.min.css"
                 , "~/Content/halpes.css"
                 , "~/Content/halpes-responsive.css"));
        }
    }
}

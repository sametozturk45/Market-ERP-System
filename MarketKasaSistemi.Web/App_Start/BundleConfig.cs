using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MarketKasaSistemi.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include("~/Content/font-awesome.min.css","~/Content/mdb.min.css","~/Content/SEO.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/Scripts/mdb.min.js", "~/Scripts/script.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
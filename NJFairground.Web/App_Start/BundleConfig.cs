
namespace NJFairground.Web
{
    using System;
    using System.Web;
    using System.Web.Optimization;
    using NJFairground.Web.Utilities;

    public class BundleConfig
    {
        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            try
            {
                RegisterStyleBundles(bundles);
                RegisterScriptBundles(bundles);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(bundles);
            }
        }

        /// <summary>
        /// Registers the script bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            try
            {
                bundles.Add(new ScriptBundle("~/Scripts/CommonScript")
                    .Include("~/Scripts/jquery-2.1.1.min.js",
                    "~/Scripts/jquery-migrate-1.2.1.min.js",
                    "~/Scripts/jquery.mobile-1.4.2.min.js",
                    "~/Scripts/common-script.js"
                    ));
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(bundles);
            }
        }

        /// <summary>
        /// Registers the style bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            try
            {
                bundles.Add(new StyleBundle("~/Styles/CommonStyle")
                    .Include("~/Styles/jquery.mobile-1.4.2.min.css",
                    "~/Styles/style.css"));
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(bundles);
            }
        }
    }
}
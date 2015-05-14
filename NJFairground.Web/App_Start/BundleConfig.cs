
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
                Bundle scriptBundle = new Bundle("~/Scripts/CommonScript", new JsMinify());
                scriptBundle.Include("~/Scripts/jquery-2.1.3.min.js",
                    "~/Scripts/jquery-migrate-1.2.1.min.js",
                    "~/Scripts/jquery.mobile-1.4.5.min.js",
                    "~/Scripts/common-script.js",
                    "~/Scripts/common-events.js"
                );
                BundleTable.Bundles.Add(scriptBundle);

                scriptBundle = new Bundle("~/Scripts/CommonAdminScript", new JsMinify());
                scriptBundle.Include("~/Scripts/jquery-2.1.3.min.js",
                    "~/Scripts/jquery-migrate-1.2.1.min.js",
                    "~/Areas/Admin/Scripts/jquery-ui-1.11.4.custom.min.js",
                    "~/Areas/Admin/Scripts/json3.js",
                    "~/Areas/Admin/Scripts/jquery.formatDateTime.js",
                    "~/Areas/Admin/Scripts/jquery.blockUI.js",
                    "~/Scripts/common-script.js",
                    "~/Areas/Admin/Scripts/common-admin-ui-script.js"
                );
                BundleTable.Bundles.Add(scriptBundle);

                scriptBundle = new Bundle("~/Scripts/MapScript", new JsMinify());
                scriptBundle.Include("~/Scripts/imageMapResizer.min.js",
                    "~/Scripts/LiteTooltip.js",
                    "~/Scripts/e-smart-zoom-jquery.js"
                    //TOOD: Add responsive image map
                );
                BundleTable.Bundles.Add(scriptBundle);

                scriptBundle = new Bundle("~/Scripts/GoogleMapAPIScript", new JsMinify());
                scriptBundle.Include("~/Scripts/gmap3.min.js",
                    "~/Scripts/jquery.autocomplete.min.js"
                );
                BundleTable.Bundles.Add(scriptBundle);

                scriptBundle = new Bundle("~/Scripts/GoogleMapAPIScriptForApps", new JsMinify());
                scriptBundle.Include("~/Scripts/jquery-2.1.3.min.js",
                    "~/Scripts/jquery-migrate-1.2.1.min.js",
                    "~/Scripts/gmap3.min.js",
                    "~/Scripts/jquery.autocomplete.min.js",
                    "~/Scripts/DirectionScript.js"
                );
                BundleTable.Bundles.Add(scriptBundle);
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
                Bundle styleBundle = new Bundle("~/Styles/CommonStyle", new CustomCssMinify(), new CssMinify());
                styleBundle.Include("~/Styles/jquery.mobile-1.4.2.min.css",
                    "~/Styles/style.css");
                BundleTable.Bundles.Add(styleBundle);

                styleBundle = new Bundle("~/Styles/CommonAdminStyle", new CustomCssMinify(), new CssMinify());
                styleBundle.Include("~/Areas/Admin/Styles/jquery-ui-1.10.4.custom.min.css",
                    "~/Areas/Admin/Styles/admin-styles.css");
                BundleTable.Bundles.Add(styleBundle);

                styleBundle = new Bundle("~/Styles/MapStyle", new CustomCssMinify(), new CssMinify());
                styleBundle.Include("~/Styles/litetooltip.min.css");
                BundleTable.Bundles.Add(styleBundle);

                styleBundle = new Bundle("~/Styles/MapStyleForApps", new CustomCssMinify(), new CssMinify());
                styleBundle.Include("~/Styles/DirectionStyle.css");
                BundleTable.Bundles.Add(styleBundle);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(bundles);
            }
        }
    }
}
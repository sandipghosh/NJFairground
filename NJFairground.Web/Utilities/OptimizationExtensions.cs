﻿
namespace NJFairground.Web.Utilities
{
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;

    public static class OptimizationExtensions
    {
        const string ScriptTemplate = "<script type=\"text/javascript\" src=\"{0}\"></script>";
        const string CssTemplate = "<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />";

        /// <summary>
        /// Gets a value indicating whether this instance is js minify.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is js minify; otherwise, <c>false</c>.
        /// </value>
        public static bool IsJsMinify { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["JsMinify"] ?? "false"); } }
        /// <summary>
        /// Gets a value indicating whether this instance is CSS minify.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is CSS minify; otherwise, <c>false</c>.
        /// </value>
        public static bool IsCssMinify { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["CssMinify"] ?? "false"); } }

        /// <summary>
        /// Jses the minify.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="markup">The markup.</param>
        /// <returns></returns>
        public static MvcHtmlString JsMinify(this HtmlHelper helper, Func<object, object> markup)
        {
            if (helper == null || markup == null)
            {
                return MvcHtmlString.Empty;
            }

            var sourceJs = (markup.DynamicInvoke(helper.ViewContext) ?? String.Empty).ToString();

            if (!IsJsMinify)
            {
                return new MvcHtmlString(sourceJs);
            }

            var minifier = new Minifier();

            var minifiedJs = minifier.MinifyJavaScript(sourceJs, new CodeSettings
            {
                EvalTreatment = EvalTreatment.MakeImmediateSafe,
                PreserveImportantComments = false
            });

            return new MvcHtmlString(minifiedJs);
        }

        /// <summary>
        /// CSSs the minify.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="markup">The markup.</param>
        /// <returns></returns>
        public static MvcHtmlString CssMinify(this HtmlHelper helper, Func<object, object> markup)
        {
            if (helper == null || markup == null)
            {
                return MvcHtmlString.Empty;
            }

            var sourceCss = (markup.DynamicInvoke(helper.ViewContext) ?? String.Empty).ToString();

            if (!IsJsMinify)
            {
                return new MvcHtmlString(sourceCss);
            }

            var minifier = new Minifier();

            var minifiedCss = minifier.MinifyStyleSheet(sourceCss, new CssSettings
            {
                CommentMode = CssComment.None
            });

            return new MvcHtmlString(minifiedCss);
        }

        /// <summary>
        /// Renders the scripts.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="urls">The urls.</param>
        /// <returns></returns>
        public static MvcHtmlString RenderScripts(this HtmlHelper helper, params string[] urls)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in urls)
            {
                if (IsJsMinify)
                {
                    sb.AppendLine(string.Format(ScriptTemplate, BundleTable.Bundles.ResolveBundleUrl(item)));
                }
                else
                {
                    //sb.AppendLine(System.Web.Optimization.Scripts.Render(item).ToHtmlString());
                    RenderProcess(sb, item, ScriptTemplate);
                }
            }

            return new MvcHtmlString(sb.ToString());
        }

        /// <summary>
        /// Renders the styles.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="urls">The urls.</param>
        /// <returns></returns>
        public static MvcHtmlString RenderStyles(this HtmlHelper helper, params string[] urls)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in urls)
            {
                if (IsCssMinify)
                {
                    sb.AppendLine(string.Format(CssTemplate, BundleTable.Bundles.ResolveBundleUrl(item)));
                }
                else
                {
                    //sb.AppendLine(System.Web.Optimization.Styles.Render(item).ToHtmlString());
                    RenderProcess(sb, item, CssTemplate);
                }
            }

            return new MvcHtmlString(sb.ToString());
        }

        /// <summary>
        /// Renders the process.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="bundleUrl">The bundle URL.</param>
        /// <param name="template">The template.</param>
        private static void RenderProcess(StringBuilder sb, string bundleUrl, string template)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var bundle = BundleTable.Bundles.GetBundleFor(bundleUrl);
            foreach (BundleFile file in bundle.EnumerateFiles(new BundleContext
                (new HttpContextWrapper(HttpContext.Current), BundleTable.Bundles, bundleUrl)))
            {
                string lastModified = File.GetLastWriteTime(HttpContext.Current.Server.MapPath(file.VirtualFile.VirtualPath)).Ticks.ToString();

                sb.AppendFormat(template + Environment.NewLine, urlHelper.Content(file.VirtualFile.VirtualPath
                    + "?v=" + lastModified));
            }
        }
    }

    public class CustomCssMinify : IBundleTransform
    {
        private const string CssContentType = "text/css";

        static CustomCssMinify()
        {
        }

        public virtual void Process(BundleContext context, BundleResponse response)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (response == null)
                throw new ArgumentNullException("response");
            if (!context.EnableInstrumentation)
            {
                var minifier = new Minifier();
                FixCustomCssErrors(response);
                string str = minifier.MinifyStyleSheet(response.Content, new CssSettings()
                {
                    CommentMode = CssComment.None
                });
                if (minifier.ErrorList.Count > 0)
                    GenerateErrorResponse(response, minifier.ErrorList);
                else
                    response.Content = str;
            }
            response.ContentType = CssContentType;
        }

        /// <summary>
        /// Add some extra fixes here
        /// </summary>
        /// <param name="response">BundleResponse</param>
        private void FixCustomCssErrors(BundleResponse response)
        {
            response.Content = Regex.Replace(response.Content, @"@import[\s]+([^\r\n]*)[\;]", String.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }

        private static void GenerateErrorResponse(BundleResponse bundle, IEnumerable<object> errors)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("/* ");
            stringBuilder.Append("CSS Minify Error").Append("\r\n");
            foreach (object obj in errors)
                stringBuilder.Append(obj.ToString()).Append("\r\n");
            stringBuilder.Append(" */\r\n");
            stringBuilder.Append(bundle.Content);
            bundle.Content = stringBuilder.ToString();
        }
    }
}
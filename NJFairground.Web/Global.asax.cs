﻿

namespace NJFairground.Web
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using FluentValidation.Mvc;
    using MultipartDataMediaFormatter;
    using NJFairground.Web.Models.ValidationRules.Factory;
    using NJFairground.Web.Utilities;
    using NJFairground.Web.Filters;

    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            try
            {
                InjectorInitializer.Initialize(ContainerProvider.Instance);
                var injector = new SimpleDependencyInjector();
                FluentValidationModelValidatorProvider.Configure(provider =>
                {
                    provider.ValidatorFactory = new FluentValidatorFactory(injector);
                });

                AreaRegistration.RegisterAllAreas();
                //InjectorInitializer.Initialize();
                WebApiConfig.Register(GlobalConfiguration.Configuration);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                FilterConfig.RegisterGlobalApiFilters(GlobalConfiguration.Configuration.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                AuthConfig.RegisterAuth();

                GlobalConfiguration.Configuration.Formatters.Add(new FormMultipartEncodedMediaTypeFormatter());
                GlobalConfiguration.Configuration.MessageHandlers.Add(new MessageLoggingHandler());
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
        }

        /// <summary>
        /// Handles the Error event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is System.Threading.ThreadAbortException)
                return;

            ex.ExceptionValueTracker(new Dictionary<string, object>() { 
                {"Is AJAX Request",HttpContext.Current.Request.IsAjaxRequest().ToString()},
                {"Request URL",HttpContext.Current.Request.Url},
                {"Request Type", HttpContext.Current.Request.HttpMethod},
                {"Request Context", HttpContext.Current.Request.Form}
            });

            if (!HttpContext.Current.Request.IsAjaxRequest())
            {
                HttpContext.Current.Response.Redirect(string.Format("{0}/Error/Index?msg={1}",
                    ConfigurationManager.AppSettings["VirtualDirectory"].ToString(), ex.Message));
            }
            else
            {
                HttpContext.Current.Response.StatusCode = 500;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine(string.Format("Error Source: {0}", ex.InnerException.Source));
                sb.AppendLine(string.Format("Error Message: {0}", ex.InnerException.Message));
                sb.AppendLine(string.Format("Error Stack: {0}", ex.InnerException.StackTrace));
                HttpContext.Current.Response.Write(sb.ToString());
            }
        }
    }
}
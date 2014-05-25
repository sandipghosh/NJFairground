
namespace NJFairground.Web.Utilities
{
    using Newtonsoft.Json;
    using NJFairground.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class CommonUtility
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public const string DEFAULT_ALLOWED_CHARACTER = @"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxy";
        public const string AnonymousFolder = "anonymous";
        public const int DEFAULT_SALT_LENGTH = 8;

        public const int DISPLAY_IMAGE_DPI = 96;
        public const int ACTUAL_IMAGE_DPI = 300;
        public const float scaleRatio = 2.635542099663718f;
        private static Random rand = new Random();

        /// <summary>
        /// Determines whether [contains search ex] [the specified search context].
        /// </summary>
        /// <param name="searchContext">The search context.</param>
        /// <param name="searchWith">The search with.</param>
        /// <returns></returns>
        public static bool ContainsSearchEx(this string searchContext, string searchWith)
        {
            return searchContext.IndexOf(searchWith, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }
        /// <summary>
        /// Nots the contains search ex.
        /// </summary>
        /// <param name="searchContext">The search context.</param>
        /// <param name="searchWith">The search with.</param>
        /// <returns></returns>
        public static bool NotContainsSearchEx(this string searchContext, string searchWith)
        {
            return !(searchContext.IndexOf(searchWith, StringComparison.CurrentCultureIgnoreCase) >= 0);
        }
        /// <summary>
        /// Equalses the search ex.
        /// </summary>
        /// <param name="searchContext">The search context.</param>
        /// <param name="searchWith">The search with.</param>
        /// <returns></returns>
        public static bool EqualsSearchEx(this string searchContext, string searchWith)
        {
            return searchContext.Equals(searchWith, StringComparison.CurrentCultureIgnoreCase);
        }
        /// <summary>
        /// Nots the equals search ex.
        /// </summary>
        /// <param name="searchContext">The search context.</param>
        /// <param name="searchWith">The search with.</param>
        /// <returns></returns>
        public static bool NotEqualsSearchEx(this string searchContext, string searchWith)
        {
            return !searchContext.Equals(searchWith, StringComparison.CurrentCultureIgnoreCase);
        }
        /// <summary>
        /// Startses the with search ex.
        /// </summary>
        /// <param name="searchContext">The search context.</param>
        /// <param name="searchWith">The search with.</param>
        /// <returns></returns>
        public static bool StartsWithSearchEx(this string searchContext, string searchWith)
        {
            return searchContext.StartsWith(searchWith, StringComparison.CurrentCultureIgnoreCase);
        }
        /// <summary>
        /// Nots the starts with search ex.
        /// </summary>
        /// <param name="searchContext">The search context.</param>
        /// <param name="searchWith">The search with.</param>
        /// <returns></returns>
        public static bool NotStartsWithSearchEx(this string searchContext, string searchWith)
        {
            return !searchContext.StartsWith(searchWith, StringComparison.CurrentCultureIgnoreCase);
        }
        /// <summary>
        /// Endses the with search ex.
        /// </summary>
        /// <param name="searchContext">The search context.</param>
        /// <param name="searchWith">The search with.</param>
        /// <returns></returns>
        public static bool EndsWithSearchEx(this string searchContext, string searchWith)
        {
            return searchContext.EndsWith(searchWith, StringComparison.CurrentCultureIgnoreCase);
        }
        /// <summary>
        /// Nots the ends with search ex.
        /// </summary>
        /// <param name="searchContext">The search context.</param>
        /// <param name="searchWith">The search with.</param>
        /// <returns></returns>
        public static bool NotEndsWithSearchEx(this string searchContext, string searchWith)
        {
            return !searchContext.EndsWith(searchWith, StringComparison.CurrentCultureIgnoreCase);
        }
        /// <summary>
        /// Gets the type of the compatible data.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Type GetCompatibleDataType(this string value)
        {
            Decimal outTypeDecimal;
            int outTypeInteger;
            DateTime outTypeDate;
            bool outTypeBoolean;

            if (Decimal.TryParse(value, out outTypeDecimal))
                return typeof(Decimal);
            else if (int.TryParse(value, out outTypeInteger))
                return typeof(int);
            else if (DateTime.TryParse(value, out outTypeDate))
                return typeof(DateTime);
            else if (bool.TryParse(value, out outTypeBoolean))
                return typeof(bool);
            else
                return typeof(string);
        }


        /// <summary>
        /// Gets the lamda expression from filter.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="strFilter">The string filter.</param>
        /// <returns></returns>
        public static Expression<Func<TModel, bool>> GetLamdaExpressionFromFilter<TModel>(string strFilter)
        {
            strFilter = strFilter.IsBase64() ? strFilter.ToBase64Decode() : strFilter;
            Expression<Func<TModel, bool>> filterExp = NJFairground.Web.Utilities.Expression.ExpressionBuilder
                .BuildLamdaExpression<TModel, bool>(strFilter);
            return filterExp;
        }

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="context">The context.</param>
        public static void SetPropertiesFromContext<T>(this T source, HttpContext context)
        {
            try
            {
                var properties = typeof(T)
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public);
                NameValueCollection values = null;

                if (context.Request.HttpMethod.ToUpper() == HttpVerbs.Post.ToString().ToUpper())
                    values = context.Request.Form;
                else if (context.Request.HttpMethod.ToUpper() == HttpVerbs.Get.ToString().ToUpper())
                    values = context.Request.QueryString;

                foreach (var prop in properties)
                {
                    if (values.AllKeys.Contains(prop.Name))
                    {
                        Type propType = prop.PropertyType;
                        if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            if (!String.IsNullOrEmpty(values[prop.Name]))
                            {
                                var value = Convert.ChangeType(values[prop.Name], propType.GetGenericArguments()[0]);
                                prop.SetValue(source, value, null);
                            }
                        }
                        else
                        {
                            if (propType.Namespace.StartsWith("System"))
                            {
                                var value = Convert.ChangeType(values[prop.Name], propType);
                                prop.SetValue(source, value, null);
                            }
                            else
                            {
                                string propValue = values[prop.Name].IsBase64() ?
                                    values[prop.Name].ToBase64Decode() : values[prop.Name].ToString();

                                var value = JsonConvert.DeserializeObject(propValue, propType);
                                prop.SetValue(source, value, null);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static NameValueCollection GetContext(HttpContext context)
        {
            NameValueCollection values = null;

            if (context.Request.HttpMethod.ToUpper() == HttpVerbs.Post.ToString().ToUpper())
                values = context.Request.Form;

            else if (context.Request.HttpMethod.ToUpper() == HttpVerbs.Get.ToString().ToUpper())
                values = context.Request.QueryString;

            return values;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="involveAlnnotation">if set to <c>true</c> [involve alnnotation].</param>
        /// <param name="displayProperty">The display property.</param>
        /// <returns></returns>
        public static string GetDisplayName<TModel, TField>
            (Expression<Func<TModel, TField>> selector, bool involveAlnnotation = false,
            DisplayProperty displayProperty = DisplayProperty.Name)
        {
            try
            {
                System.Linq.Expressions.Expression body = selector;
                if (body is LambdaExpression)
                {
                    body = ((LambdaExpression)body).Body;
                }

                if (body.NodeType == ExpressionType.MemberAccess)
                {
                    PropertyInfo propInfo = (PropertyInfo)((MemberExpression)body).Member;
                    if (involveAlnnotation)
                    {
                        DisplayAttribute attribute = propInfo.GetCustomAttributes(typeof(DisplayAttribute), true)
                            .Select(prop => (DisplayAttribute)prop).FirstOrDefault();

                        if (attribute != null)
                            return attribute.GetType().GetProperty(displayProperty.ToString())
                                .GetValue(attribute, null).ToString();
                        else
                            return propInfo.Name;
                    }
                    return propInfo.Name;
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(selector, involveAlnnotation);
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the grid dropdown data.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="keyField">The key field.</param>
        /// <param name="valueField">The value field.</param>
        /// <returns></returns>
        public static string GetGridDropdownData<TModel>
            (IEnumerable<TModel> data, string keyField, string valueField)
        {
            try
            {
                var strData = data.Select(x =>
                {
                    return string.Format("{0}:{1}",
                        x.GetType().GetProperty(keyField).GetValue(x, null),
                        x.GetType().GetProperty(valueField).GetValue(x, null));
                });

                return string.Join(";", strData);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(data, keyField, valueField);
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the client ip address.
        /// </summary>
        /// <returns></returns>
        public static string GetClientIPAddress()
        {
            string clientIP = "";
            try
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                { clientIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); }
                else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
                { clientIP = HttpContext.Current.Request.UserHostAddress; }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return clientIP;
        }



        /// <summary>
        /// Gets the secure module.
        /// </summary>
        /// <value>
        /// The secure module.
        /// </value>
        public static string SecureModule { get { return "SecureUrl"; } }

        /// <summary>
        /// Gets the secure query string key.
        /// </summary>
        /// <value>
        /// The secure query string key.
        /// </value>
        public static string SecureQueryStringKey { get { return "enc"; } }

        /// <summary>
        /// Converts to timestamp.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ConvertToTimestamp(DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;
            return (long)elapsedTime.TotalSeconds;
        }

        /// <summary>
        /// Logs to file.
        /// </summary>
        /// <param name="logContent">Content of the log.</param>
        public static void LogToFileWithStack(string logContent)
        {
            //string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string baseDir = HttpContext.Current.Server
                .MapPath(string.Format("~/{0}/", ConfigurationManager.AppSettings["ErrorLogFolder"]));

            string logFilePath = string.Format("{0}LogFile-{1}{2}{3}-{4}{5}{6}.txt", baseDir,
                DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year,
                DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            StackFrame frame = new StackFrame(1, true);
            MethodBase lastCalling = frame.GetMethod();
            string lastCallingFunction = lastCalling.Name;
            string callingModule = lastCalling.Module.Name;
            string fileName = frame.GetFileName();
            string lineNumber = frame.GetFileLineNumber().ToString();

            FileLogger log = new FileLogger(logFilePath, true, FileLogger.LogType.TXT, FileLogger.LogLevel.All);
            log.LogRaw(string.Format("{0} :{1}-{2}; {3}.{4} ==> {5}", DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"),
                fileName, lineNumber, callingModule, lastCallingFunction, logContent));
        }

        /// <summary>
        /// Exceptions the value tracker.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="parameters">The parameters.</param>
        public static void ExceptionValueTracker(this Exception ex, params object[] parameters)
        {
            StackFrame stackFrame = new StackTrace().GetFrame(1);
            var methodInfo = stackFrame.GetMethod();
            var paramInfos = methodInfo.GetParameters();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("[Function: {0}.{1}]", methodInfo.DeclaringType.FullName, methodInfo.Name));
            for (int i = 0; i < paramInfos.Length; i++)
            {
                var currentParameterInfo = paramInfos[i];

                string paramValue = string.Empty;
                if (parameters.Length - 1 >= i)
                {
                    var currentParameter = parameters[i];
                    if (parameters[i] != null)
                    {
                        paramValue = (currentParameter.GetType().Namespace.StartsWith("System")) ?
                            currentParameter.ToString() : JsonConvert.SerializeObject(currentParameter,
                                new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                });
                    }
                }

                sb.AppendLine(string.Format("   {0} : {1}", currentParameterInfo.Name, paramValue));
            }
            sb.AppendLine("[Function End]");

            ex.Data.Clear();
            ex.Data.Add("FunctionInfo", sb.ToString());

            if (ConfigurationManager.AppSettings["EnableErrorLog"].ToString().ToLower() == "true")
            {
                LogToFileWithStack(string.Format("{0}\r\n{1}", ex.Message, sb.ToString()));
            }
        }

        /// <summary>
        /// Exceptions the value tracker.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="parameters">The parameters.</param>
        public static void ExceptionValueTracker(this Exception ex, IDictionary<string, object> parameters)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[Application Error Starts]");

            foreach (KeyValuePair<string, object> item in parameters)
            {
                string paramValue = (item.Value.GetType().Namespace.StartsWith("System")) ?
                    item.Value.ToString() : JsonConvert.SerializeObject(item.Value, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    });

                sb.AppendLine(string.Format("   {0} : {1}", item.Key, paramValue));
            }
            sb.AppendLine("[Application Error Ends]");

            ex.Data.Clear();
            ex.Data.Add("FunctionInfo", sb.ToString());

            if (ConfigurationManager.AppSettings["EnableErrorLog"].ToString().ToLower() == "true")
            {
                LogToFileWithStack(string.Format("{0}\r\n{1}", ex.Message, sb.ToString()));
            }
        }

        /// <summary>
        /// Gets the session data.
        /// </summary>
        /// <param name="sessionKey">The session key.</param>
        /// <returns></returns>
        public static T GetSessionData<T>(string sessionKey)
        {
            return (T)HttpContext.Current.Session[sessionKey];
        }

        /// <summary>
        /// Sets the session data.
        /// </summary>
        /// <param name="sessionKey">The session key.</param>
        /// <param name="sessionValue">The session value.</param>
        public static void SetSessionData<T>(string sessionKey, T sessionValue)
        {
            HttpContext.Current.Session[sessionKey] = sessionValue;
        }

        /// <summary>
        /// Gets the application setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static T GetAppSetting<T>(string name)
        {
            string value = ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                throw new Exception(String.Format("Could not find setting '{0}',", name));
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Charactors the limit.
        /// </summary>
        /// <param name="inputStr">The input string.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        public static string CharactorLimit(this string inputStr, int limit)
        {
            if (!string.IsNullOrEmpty(inputStr))
                return inputStr.Substring(0, Math.Min(limit, inputStr.Length));
            else
                return string.Empty;
        }

        /// <summary>
        /// Toes the base64 encode.
        /// </summary>
        /// <param name="toEncode">To encode.</param>
        /// <returns></returns>
        public static string ToBase64Encode(this string toEncode)
        {
            try
            {
                byte[] toEncodeAsBytes = System.Text.Encoding.UTF8.GetBytes(toEncode);
                string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
                return returnValue;
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(toEncode);
            }
            return string.Empty;
        }

        /// <summary>
        /// Toes the base64 decode.
        /// </summary>
        /// <param name="encodedData">The encoded data.</param>
        /// <returns></returns>
        public static string ToBase64Decode(this string encodedData)
        {
            try
            {
                byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
                string returnValue = System.Text.Encoding.UTF8.GetString(encodedDataAsBytes);
                return HttpContext.Current.Server.UrlDecode(returnValue);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(encodedData);
            }
            return string.Empty;
        }

        /// <summary>
        /// Determines whether the specified base64 string is base64.
        /// </summary>
        /// <param name="base64String">The base64 string.</param>
        /// <returns></returns>
        public static bool IsBase64(this string base64String)
        {
            if (base64String.Replace(" ", "").Length % 4 != 0)
            {
                return false;
            }

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception exception)
            {
                exception.ExceptionValueTracker(base64String);
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is ajax request] [the specified request].
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            return ((request.Headers.AllKeys.Contains("x-requested-with") &&
                request.Headers["x-requested-with"] == "XMLHttpRequest") ||
                (request.Headers.AllKeys.Contains("x-my-custom-header") &&
                request.Headers["x-my-custom-header"] == "AjaxRequest"));
        }

        /// <summary>
        /// Renders the view to string.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="viewData">The view data.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="additionalData">The additional data.</param>
        /// <returns></returns>
        public static string RenderViewToString(string viewName, object viewData,
            ControllerBase controller, IDictionary<string, object> additionalData)
        {
            try
            {
                HttpContextBase contextBase = new HttpContextWrapper(HttpContext.Current);
                TempDataDictionary tempData = new TempDataDictionary();

                foreach (var item in additionalData)
                {
                    tempData[item.Key] = item.Value;
                }

                var routeData = new RouteData();
                routeData.Values.Add("controller", controller.GetType().Name.Replace("Controller", ""));
                var controllerContext = new ControllerContext(contextBase, routeData, controller);

                var razorViewEngine = new RazorViewEngine();
                var razorViewResult = razorViewEngine.FindView(controllerContext, viewName, "", false);

                var writer = new StringWriter();
                var viewContext = new ViewContext(controllerContext, razorViewResult.View,
                       new ViewDataDictionary(viewData), tempData, writer);
                razorViewResult.View.Render(viewContext, writer);

                return writer.ToString();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(viewName, viewData, controller, additionalData);
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the random string.
        /// </summary>
        /// <param name="rnd">The RND.</param>
        /// <param name="allowedChars">The allowed chars.</param>
        /// <param name="minLength">Length of the min.</param>
        /// <param name="maxLength">Length of the max.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetRandomString(Random rnd,
            string allowedChars = DEFAULT_ALLOWED_CHARACTER,
            int minLength = DEFAULT_SALT_LENGTH,
            int maxLength = DEFAULT_SALT_LENGTH, int count = 1)
        {
            char[] chars = new char[maxLength];
            int setLength = allowedChars.Length;

            while (count-- > 0)
            {
                int length = rnd.Next(minLength, maxLength + 1);
                for (int i = 0; i < length; ++i)
                {
                    chars[i] = allowedChars[rnd.Next(setLength)];
                }
                yield return new string(chars, 0, length);
            }
        }

        /// <summary>
        /// Genarates the random string.
        /// </summary>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns></returns>
        public static string GenarateRandomString
            (int minLength = DEFAULT_SALT_LENGTH,
            int maxLength = DEFAULT_SALT_LENGTH)
        {
            //int seed = (int)DateTime.Now.Ticks;
            //Random rnd = new Random(seed);
            //return GetRandomString(rnd, DEFAULT_ALLOWED_CHARACTER, minLength, maxLength).First();

            return GetRandomString(rand, DEFAULT_ALLOWED_CHARACTER, minLength, maxLength).First();
        }

        /// <summary>
        /// Adds the business days.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="days">The days.</param>
        /// <returns></returns>
        public static DateTime AddBusinessDays(DateTime date, int days)
        {
            if (days == 0) return date;

            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                date = date.AddDays(2);
                days -= 1;
            }
            else if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
                days -= 1;
            }

            date = date.AddDays(days / 5 * 7);
            int extraDays = days % 5;

            if ((int)date.DayOfWeek + extraDays > 5)
            {
                extraDays += 2;
            }

            return date.AddDays(extraDays);
        }

        #region Private Members
        /// <summary>
        /// Logs to file.
        /// </summary>
        /// <param name="logPath">The log path.</param>
        /// <param name="logFormat">The log format.</param>
        /// <param name="logContent">Content of the log.</param>
        private static void LogToFile(string logPath, string logFormat, params object[] logContent)
        {
            string fileName = string.Format("LogFile-{0}{1}{2}-{3}{4}{5}.txt",
                DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year,
                DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            string logFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logPath, fileName);

            FileLogger log = new FileLogger(logFilePath, true, FileLogger.LogType.TXT, FileLogger.LogLevel.All);
            log.LogRaw(string.Format(logFormat, logContent));
        }

        #endregion
    }
}
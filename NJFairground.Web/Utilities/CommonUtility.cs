
namespace NJFairground.Web.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Newtonsoft.Json;
    using NJFairground.Web.Models;

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
        /// Saves the image with watermark from file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="pageId">The page id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="waterMarkText">The water mark text.</param>
        /// <param name="targetFolder">The target folder.</param>
        /// <param name="opacity">The opacity.</param>
        /// <returns></returns>
        public static string SaveImageWithWatermarkFromFile(string filePath, int templateId, int pageId,
            int? userId, string waterMarkText, string targetFolder, int opacity)
        {
            string imageName = string.Format("{0}_{1}_{2}.jpg", HttpContext.Current.Session.SessionID, templateId, pageId);
            string imagePath = Path.Combine(targetFolder, imageName);

            try
            {
                Image imgPhoto = Image.FromFile(filePath); //byteArrayToImage(byteArray);
                filePath = SetWatermarkText(imgPhoto, waterMarkText, targetFolder, opacity, imageName, imagePath);
                imgPhoto.Dispose();
                return filePath;
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(filePath, waterMarkText, targetFolder, opacity);
            }
            return File.Exists(imagePath) ? imageName : string.Empty;
        }

        /// <summary>
        /// Sets the watermark.
        /// </summary>
        /// <param name="imgPhoto">The img photo.</param>
        /// <param name="waterMarkText">The water mark text.</param>
        /// <param name="targetFolder">The target folder.</param>
        /// <param name="opacity">The opacity.</param>
        /// <param name="imageName">Name of the image.</param>
        /// <param name="imagePath">The image path.</param>
        /// <returns></returns>
        public static string SetWatermarkText(Image imgPhoto, string waterMarkText,
            string targetFolder, int opacity, string imageName, string imagePath)
        {
            try
            {
                //Image imgPhoto = byteArrayToImage(byteArray);
                int phWidth = imgPhoto.Width;
                int phHeight = imgPhoto.Height;

                Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

                Graphics grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                grPhoto.DrawImage(
                    imgPhoto,                               // Photo Image object
                    new Rectangle(0, 0, phWidth, phHeight), // Rectangle structure
                    0,                                      // x-coordinate of the portion of the source image to draw. 
                    0,                                      // y-coordinate of the portion of the source image to draw. 
                    phWidth,                                // Width of the portion of the source image to draw. 
                    phHeight,                               // Height of the portion of the source image to draw. 
                    GraphicsUnit.Pixel);                    // Units of measure 

                double tangent = (double)bmPhoto.Height / (double)bmPhoto.Width;
                double angle = Math.Atan(tangent) * (180 / Math.PI);
                double halfHypotenuse = (Math.Sqrt((bmPhoto.Height
                    * bmPhoto.Height) + (bmPhoto.Width * bmPhoto.Width))) / 2;

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                int[] sizes = new int[] { 200, 150, 96, 72, 60, 48, 36, 30, 24, 22, 20, 18, 16, 14, 12, 10, 8, 6, 4 };

                Font crFont = null;
                SizeF crSize = new SizeF();
                for (int i = 0; i <= sizes.Length - 1; i++)
                {
                    crFont = new Font("arial", sizes[i], FontStyle.Bold);
                    crSize = grPhoto.MeasureString(waterMarkText, crFont);

                    if ((ushort)crSize.Width < (ushort)phWidth)
                        break;
                }

                Matrix matrix = new Matrix();
                matrix.Translate(bmPhoto.Width / 2, bmPhoto.Height / 2);
                matrix.Rotate(-45.0f);

                grPhoto.Transform = matrix;

                SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(opacity, 0, 0, 0));
                grPhoto.DrawString(waterMarkText, crFont, semiTransBrush2,
                    2, 2, stringFormat);

                SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(opacity, 255, 255, 255));
                grPhoto.DrawString(waterMarkText, crFont, semiTransBrush,
                    0, 0, stringFormat);

                imgPhoto = bmPhoto;
                grPhoto.Dispose();

                imgPhoto.Save(imagePath, ImageFormat.Jpeg);
                imgPhoto.Dispose();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(imgPhoto, waterMarkText,
                    targetFolder, opacity, imageName, imagePath);
            }
            return File.Exists(imagePath) ? imageName : string.Empty;
        }

        public static string SetWatermarkTextWithImage(Image imgPhoto, string waterMarkText,
            string waterMarkImagePath, string targetFolder, int opacity, string imageName, string imagePath)
        {
            try
            {
                int phWidth = imgPhoto.Width;
                int phHeight = imgPhoto.Height;

                Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
                Graphics grPhoto = Graphics.FromImage(bmPhoto);

                Image imgWatermark = new Bitmap(waterMarkImagePath);
                //int wmWidth = (int)Math.Round((imgWatermark.Width * 0.2m), 0);
                //int wmHeight = (int)Math.Round((imgWatermark.Height * 0.2m), 0);
                int wmWidth = imgWatermark.Width;
                int wmHeight = imgWatermark.Height;

                //------------------------------------------------------------
                //Step #1 - Insert Copyright message
                //------------------------------------------------------------
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, phWidth, phHeight), 0, 0, phWidth, phHeight, GraphicsUnit.Pixel);

                int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };

                Font crFont = null;
                SizeF crSize = new SizeF();
                for (int i = 0; i <= sizes.Length - 1; i++)
                {
                    crFont = new Font("arial", sizes[i], FontStyle.Bold);
                    crSize = grPhoto.MeasureString(waterMarkText, crFont);
                    if ((ushort)crSize.Width < (ushort)phWidth) break;
                }

                int yPixlesFromBottom = (int)(phHeight * .05);
                float yPosFromBottom = ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));
                float xCenterOfImg = (phWidth / 2);

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;

                SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(opacity, 0, 0, 0));
                grPhoto.DrawString(waterMarkText, crFont, semiTransBrush2, new PointF(xCenterOfImg + 1, yPosFromBottom + 1), stringFormat);

                SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(opacity, 255, 255, 255));
                grPhoto.DrawString(waterMarkText, crFont, semiTransBrush, new PointF(xCenterOfImg, yPosFromBottom), stringFormat);


                //------------------------------------------------------------
                //Step #2 - Insert Watermark image
                //------------------------------------------------------------
                Bitmap bmWatermark = new Bitmap(bmPhoto);
                bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
                Graphics grWatermark = Graphics.FromImage(bmWatermark);

                ImageAttributes imageAttributes = new ImageAttributes();
                //ColorMap colorMap = new ColorMap();
                //colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                //colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                //ColorMap[] remapTable = { colorMap };
                //imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                float[][] colorMatrixElements = { 
					new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},       
					new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},        
					new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},        
					new float[] {0.0f,  0.0f,  0.0f,  0.9f, 0.0f},        
					new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                };
                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                int xPosOfWm = ((phWidth - wmWidth) - 10);
                int yPosOfWm = 10;

                grWatermark.DrawImage(imgWatermark,
                    new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight), 0, 0,
                    wmWidth, wmHeight, GraphicsUnit.Pixel, imageAttributes);

                imgPhoto = bmWatermark;
                grPhoto.Dispose();
                grWatermark.Dispose();

                //save new image to file system.
                imgPhoto.Save(imagePath, ImageFormat.Jpeg);
                imgPhoto.Dispose();
                imgWatermark.Dispose();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(imgPhoto, waterMarkText,
                    waterMarkImagePath, targetFolder, opacity, imageName, imagePath);
            }
            return File.Exists(imagePath) ? imageName : string.Empty;
        }

        /// <summary>
        /// Saves the image without watermark from byte array.
        /// </summary>
        /// <param name="byteArray">The byte array.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="pageId">The page id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="targetFolder">The target folder.</param>
        /// <returns></returns>
        public static string SaveImageWithoutWatermarkFromByteArray(byte[] byteArray,
            int templateId, int pageId, int? userId, string targetFolder)
        {
            string imageName = string.Format("{0}_{1}_{2}.jpg", HttpContext.Current.Session.SessionID, templateId, pageId);
            string imagePath = Path.Combine(targetFolder, imageName);

            try
            {
                Image imgPhoto = ByteArrayToImage(byteArray);
                Bitmap bmp = new Bitmap(imgPhoto);
                bmp.SetResolution(ACTUAL_IMAGE_DPI, ACTUAL_IMAGE_DPI);
                bmp.Save(imagePath, ImageFormat.Jpeg);
                bmp.Dispose();
                imgPhoto.Dispose();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(byteArray, targetFolder);
            }
            return File.Exists(imagePath) ? imageName : string.Empty;
        }

        /// <summary>
        /// Images to byte array.
        /// </summary>
        /// <param name="imageIn">The image in.</param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Gif);
            return ms.ToArray();
        }

        /// <summary>
        /// Bytes the array to image.
        /// </summary>
        /// <param name="byteArrayIn">The byte array in.</param>
        /// <returns></returns>
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
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
        public static void LogToFileWithStack(string logContent, string fileNameInput = "")
        {
            //string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string baseDir = HostingEnvironment
                .MapPath(string.Format("~/{0}/", ConfigurationManager.AppSettings["ErrorLogFolder"]));

            string filePatter = string.IsNullOrEmpty(fileNameInput) ? "{0}LogFile-{1}{2}{3}-{4}{5}{6}.txt" : "{0}LogFile-{7}.txt";
            string logFilePath = string.Format(filePatter, baseDir,
                DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year,
                DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, fileNameInput);

            StackFrame frame = new StackFrame(1, true);
            MethodBase lastCalling = frame.GetMethod();
            string lastCallingFunction = lastCalling.Name;
            string callingModule = lastCalling.Module.Name;
            string fileName = frame.GetFileName();
            string lineNumber = frame.GetFileLineNumber().ToString();

            FileLogger log = new FileLogger(logFilePath, true, FileLogger.LogType.TXT, FileLogger.LogLevel.All);
            log.LogRaw(string.Format("{0} :{1}-{2}; {3}.{4} ==>\r\n{5}", DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"),
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
                                    Formatting = Newtonsoft.Json.Formatting.Indented,
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
                StackLogger.LogMessage(ex.ToString());
                LogToFileWithStack(string.Format("{0}\r\n{1}", StackLogger.GetCurrentLog(), sb.ToString()));
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
                        Formatting = Newtonsoft.Json.Formatting.Indented,
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    });

                sb.AppendLine(string.Format("   {0} : {1}", item.Key, paramValue));
            }
            sb.AppendLine("[Application Error Ends]");

            ex.Data.Clear();
            ex.Data.Add("FunctionInfo", sb.ToString());

            if (ConfigurationManager.AppSettings["EnableErrorLog"].ToString().ToLower() == "true")
            {
                StackLogger.LogMessage(ex.ToString());
                LogToFileWithStack(string.Format("{0}\r\n{1}", StackLogger.GetCurrentLog(), sb.ToString()));
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

        /// <summary>
        /// Resolves the server URL.
        /// </summary>
        /// <param name="serverUrl">The server URL.</param>
        /// <param name="forceHttps">if set to <c>true</c> [force HTTPS].</param>
        /// <returns></returns>
        public static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            if (serverUrl.IndexOf("://") > -1)
                return serverUrl;

            string newUrl = VirtualPathUtility.ToAbsolute(serverUrl);
            if (System.Web.HttpContext.Current != null)
            {
                Uri originalUri = System.Web.HttpContext.Current.Request.Url;
                newUrl = (forceHttps ? "https" : originalUri.Scheme) +
                    "://" + originalUri.Authority + newUrl;
            }
            else
            {
                newUrl = GetAppSetting<string>("HostUrl") + newUrl;
            }
            return newUrl;
        }

        /// <summary>
        /// Gets the RSS feedas string.
        /// </summary>
        /// <param name="feedLink">The feed link.</param>
        /// <returns></returns>
        public static string GetRSSFeedAsString(string feedLink)
        {
            if (!string.IsNullOrEmpty(feedLink))
            {
                var webClient = new WebClient();
                string feedUrl = CommonUtility
                    .GetAppSetting<string>(feedLink).Replace("&amp;", "&");

                webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                // fetch feed as string
                var content = webClient.OpenRead(feedUrl);
                var contentReader = new StreamReader(content);
                var rssFeedAsString = contentReader.ReadToEnd();
                return rssFeedAsString;
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the facebook json feed as string.
        /// </summary>
        /// <returns></returns>
        public static string GetFacebookJsonFeedAsString()
        {
            Func<string, string> replaceAmp = (input) => input.Replace("&amp;", "&");
            string selectedFields = "fields=id,from,name,caption,description,message,picture,link,created_time,updated_time";
            string authTokenUrl = replaceAmp(CommonUtility.GetAppSetting<string>("Facebook:AuthTokenUrl"));
            string jsonFeedUrl = replaceAmp(CommonUtility.GetAppSetting<string>("Facebook:JsonFeed"));

            var webClient = new WebClient();
            string access_token = webClient.DownloadString(authTokenUrl);

            if (!string.IsNullOrEmpty(access_token))
            {
                webClient = new WebClient();
                string facebookjson = webClient.DownloadString(replaceAmp(string.Format(jsonFeedUrl, selectedFields, access_token)));
                return facebookjson;
            }
            return string.Empty;
        }

        /// <summary>
        /// Toes the posted file base.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns></returns>
        public static List<HttpPostedFileBase> ToPostedFileBase(this HttpFileCollection files)
        {
            if (files != null && files.Count > 0)
            {
                var newFiles = new List<HttpPostedFileBase>();
                foreach (string name in files)
                {
                    var file = files[name];
                    if (file.ContentLength > 0)
                        newFiles.Add(new HttpPostedFileWrapper(file));
                }
                return newFiles;
            }
            else
                return new List<HttpPostedFileBase>();
        }

        /// <summary>
        /// Scrubs the HTML.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ScrubHtml(this string value)
        {
            var step1 = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
            var step2 = Regex.Replace(step1, @"\s{2,}", " ");
            return step2;
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

        /// <summary>
        /// Sets the watermark.
        /// </summary>
        /// <param name="imgPhoto">The img photo.</param>
        /// <param name="waterMarkText">The water mark text.</param>
        /// <param name="targetFolder">The target folder.</param>
        /// <param name="opacity">The opacity.</param>
        /// <param name="imageName">Name of the image.</param>
        /// <param name="imagePath">The image path.</param>
        /// <returns></returns>

        #endregion
    }
}
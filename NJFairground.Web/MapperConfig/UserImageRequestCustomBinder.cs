

namespace NJFairground.Web.MapperConfig
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using Newtonsoft.Json;
    using NJFairground.Web.DTO.RequestDto;
    using NJFairground.Web.Utilities;

    public class UserImageRequestCustomBinder : System.Web.Http.ModelBinding.IModelBinder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserImageRequestCustomBinder"/> class.
        /// </summary>
        public UserImageRequestCustomBinder()
        {

        }

        /// <summary>
        /// Binds the model.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="bindingContext">The binding context.</param>
        /// <returns></returns>
        public bool BindModel(System.Web.Http.Controllers.HttpActionContext actionContext, System.Web.Http.ModelBinding.ModelBindingContext bindingContext)
        {
            try
            {
                HttpRequestBase request = new HttpRequestWrapper(HttpContext.Current.Request);
                if (request.Form.AllKeys.Length > 0)
                {
                    bindingContext.Model = GetDataFromFormContext<UserImageRequestDto>(request.Form[0]);
                }
                return true;
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(actionContext, bindingContext);
            }
            return false;
        }

        /// <summary>
        /// Gets the data from form context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        private T GetDataFromFormContext<T>(string str) where T : new()
        {
            try
            {
                var data = HttpUtility.ParseQueryString(str);
                return (data == null || data.AllKeys.Any(x => x == null))
                    ? GetFromJsonString<T>(str) : GetFromQueryString<T>(str);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(str);
            }
            return new T();
        }

        /// <summary>
        /// Gets from json string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr">The json string.</param>
        /// <returns></returns>
        private T GetFromJsonString<T>(string jsonStr) where T : new()
        {
            try
            {
                return (string.IsNullOrEmpty(jsonStr)) ? new T() :
                JsonConvert.DeserializeObject<T>(jsonStr);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(jsonStr);
            }
            return new T();
        }

        /// <summary>
        /// Gets from query string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qStr">The q string.</param>
        /// <returns></returns>
        private T GetFromQueryString<T>(string qStr) where T : new()
        {
            try
            {
                if (string.IsNullOrEmpty(qStr)) return new T();
                var obj = new T();
                var dataCollection = HttpUtility.ParseQueryString(qStr);
                dataCollection.AllKeys.ToList().ForEach(x =>
                {
                    SetProperty(x, obj, dataCollection[x]);
                });
                return obj;
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(qStr);
            }
            return new T();
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <param name="compoundProperty">The compound property.</param>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        private void SetProperty(string compoundProperty, object target, object value)
        {
            try
            {
                string[] bits = compoundProperty.Split('.');
                for (int i = 0; i < bits.Length - 1; i++)
                {
                    PropertyInfo propertyToGet = target.GetType().GetProperty(bits[i]);
                    target = propertyToGet.GetValue(target, null);
                }
                PropertyInfo propertyToSet = target.GetType().GetProperty(bits.Last());
                propertyToSet.SetValue(target, propertyToSet.PropertyType.IsEnum ?
                    Enum.Parse(propertyToSet.PropertyType, value.ToString())
                    : Convert.ChangeType(value, propertyToSet.PropertyType), null);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(compoundProperty, target, value);
            }
        }
    }
}
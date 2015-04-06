
namespace NJFairground.Web.DTO.Base
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Xml.Linq;
    using System.Xml.XPath;

    public class RequestBase
    {
        public RequestBase()
        {
            this.Action = CrudAction.Select;
            this.ItemIndex = 0;
            this.ItemCount = 0;
        }

        public string AuthToken { get; set; }
        public string RequestToken { get; set; }

        private CrudAction crudAction = CrudAction.Select;
        public CrudAction Action
        {
            get { return this.crudAction; }
            set { this.crudAction = value; }
        }

        private string filter = string.Empty;
        public string Filter
        {
            get { return filter; }
            set { this.filter = this.ProcessFilter(value); }
        }

        public int ItemIndex { get; set; }
        public int ItemCount { get; set; }

        /// <summary>
        /// Deserialize filter expression sent from client
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal Expression<Func<T, bool>> GetExpression<T>()
        {
            if (!string.IsNullOrEmpty(this.filter))
            {
                XElement xFilter = XElement.Parse(this.filter);
                NJFairground.Web.Utilities.Expression.ExpressionSerializer expressionSpecification
                    = new NJFairground.Web.Utilities.Expression.ExpressionSerializer();

                return expressionSpecification.Deserialize<Func<T, bool>>(xFilter);
            }
            return null;
        }

        /// <summary>
        /// Processes the expression filter XML.
        /// </summary>
        /// <param name="strFilter">The STR filter.</param>
        /// <returns></returns>
        private string ProcessFilter(string strFilter)
        {
            if (string.IsNullOrEmpty(strFilter)) return strFilter;
            string xPathExpression = @"//*['Dto'=substring(@Name, string-length(@Name) - string-length('Dto') +1)]";

            XElement filterXML = XElement.Parse(strFilter);
            IEnumerable<XElement> filterElements = ((IEnumerable)filterXML
                .XPathEvaluate(xPathExpression)).Cast<XElement>();

            foreach (XElement item in filterElements)
            {
                string nameSpace = item.Attribute("Name").Value;
                string dtoName = nameSpace.Substring(nameSpace.LastIndexOf(".") + 1,
                    nameSpace.Length - (nameSpace.LastIndexOf(".") + 1));

                item.Attribute("Name").Value = string.Format("{0}.DTO.{1}",
                    Assembly.GetExecutingAssembly().GetName().Name, dtoName);
            }
            return filterXML.ToString();
        }
    }
}
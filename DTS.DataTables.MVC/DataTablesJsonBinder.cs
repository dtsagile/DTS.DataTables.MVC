using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DTS.DataTables.MVC
{
    /// <summary>
    /// Defines an abstract DataTables binder to bind a model with the JSON request from DataTables.
    /// </summary>
    public abstract class DataTablesJsonBinder : IModelBinder
    {
        /// <summary>
        /// Get's the JSON parameter name to retrieve data. 
        /// You may override this to change to your parameter.
        /// </summary>
        protected virtual string JSON_PARAMETER_NAME { get { return "json"; } }
        /// <summary>
        /// Binds a new model with the DataTables request parameters.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.RequestContext.HttpContext.Request;
            var requestMethod = request.HttpMethod.ToLower();
            var requestType = request.ContentType;

            // If the request type does not contains "JSON", it's supposed not to be a JSON request so we skip.
            if (!IsJsonRequest(request))
                throw new ArgumentException("You must provide a JSON request and set it's content type to match a JSON request content type.");

            // Desserializes the JSON request using the .Net Json implementation.
            return Deserialize(bindingContext.ValueProvider.GetValue(JSON_PARAMETER_NAME).AttemptedValue);
        }
        /// <summary>
        /// Checks if a request is a JsonRequest or not. 
        /// You may override this to check for other values or indicators.
        /// </summary>
        /// <param name="request">The HttpRequestBase object representing the MVC request.</param>
        /// <returns>True if the ContentType contains "json", False otherwise.</returns>
        public virtual bool IsJsonRequest(HttpRequestBase request)
        {
            return request.ContentType.ToLower().Contains("json");
        }
        /// <summary>
        /// When overriden, deserializes the JSON data into a DataTablesRequest object.
        /// </summary>
        /// <param name="jsonData">The JSON data to be deserialized.</param>
        /// <returns>The DataTablesRequest object.</returns>
        protected abstract IDataTablesRequest Deserialize(string jsonData);
    }
}

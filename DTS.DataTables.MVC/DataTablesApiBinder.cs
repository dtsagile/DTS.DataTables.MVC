﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.ModelBinding;

namespace DTS.DataTables.MVC
{
    public class DataTablesApiModelBinder : System.Web.Http.ModelBinding.IModelBinder
    {
        /// <summary>
        /// Formatting to retrieve data for each column.
        /// </summary>
        protected readonly string COLUMN_DATA_FORMATTING = "columns[{0}][data]";

        /// <summary>
        /// Formatting to retrieve name for each column.
        /// </summary>
        protected readonly string COLUMN_NAME_FORMATTING = "columns[{0}][name]";

        /// <summary>
        /// Formatting to retrieve searchable indicator for each column.
        /// </summary>
        protected readonly string COLUMN_SEARCHABLE_FORMATTING = "columns[{0}][searchable]";

        /// <summary>
        /// Formatting to retrieve orderable indicator for each column.
        /// </summary>
        protected readonly string COLUMN_ORDERABLE_FORMATTING = "columns[{0}][orderable]";

        /// <summary>
        /// Formatting to retrieve search value for each column.
        /// </summary>
        protected readonly string COLUMN_SEARCH_VALUE_FORMATTING = "columns[{0}][search][value]";

        /// <summary>
        /// Formatting to retrieve search regex indicator for each column.
        /// </summary>
        protected readonly string COLUMN_SEARCH_REGEX_FORMATTING = "columns[{0}][search][regex]";

        /// <summary>
        /// Formatting to retrieve ordered columns.
        /// </summary>
        protected readonly string ORDER_COLUMN_FORMATTING = "order[{0}][column]";

        /// <summary>
        /// Formatting to retrieve columns order direction.
        /// </summary>
        protected readonly string ORDER_DIRECTION_FORMATTING = "order[{0}][dir]";


        public bool BindModel(System.Web.Http.Controllers.HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            // get current request and http method
            var request = HttpContext.Current.Request;

            // request model instance
            var model = new DataTablesRequest();

            // resolve the parameters
            NameValueCollection requestParameters = ResolveNameValueCollection(request);

            // Populates the model with the draw count from DataTables.
            model.Draw = Get<int>(requestParameters, "draw");

            // Populates the model with page info (server-side paging).
            model.Start = Get<int>(requestParameters, "start");
            model.Length = Get<int>(requestParameters, "length");

            // Populates the model with search (global search).
            var searchValue = Get<string>(requestParameters, "search[value]");
            var searchRegex = Get<bool>(requestParameters, "search[regex]");
            model.Search = new Search(searchValue, searchRegex);

            // Get's the column collection from the request parameters.
            var columns = GetColumns(requestParameters);

            // Parse column ordering.
            ParseColumnOrdering(requestParameters, columns);

            // Attach columns into the model.
            model.SetColumns(columns);

            // bind model to context
            bindingContext.Model = (IDataTablesRequest)model;

            return true;

        }


        /// <summary>
        /// Resolves the NameValueCollection from the request.
        /// Default implementation supports only GET and POST methods.
        /// You may override this method to support other HTTP verbs.
        /// </summary>
        /// <param name="request">The HttpRequestBase object that represents the MVC request.</param>
        /// <returns>The NameValueCollection with request variables.</returns>
        protected virtual NameValueCollection ResolveNameValueCollection(HttpRequest request)
        {
            if (request.HttpMethod.ToLower().Equals("get"))
            {
                return request.QueryString;
            }
            else if (request.HttpMethod.ToLower().Equals("post") || request.HttpMethod.ToLower().Equals("put"))
            {
                return request.Form;
            }
            else
            {
                throw new ArgumentException(String.Format("The provided HTTP method ({0}) is not a valid method to use with DataTablesBinder. Please, use HTTP GET or POST methods only.", request.HttpMethod), "method");
            }
        }


        /// <summary>
        /// Get's a typed value from the collection using the provided key.
        /// This method is provided as an option for you to override the default behavior and add aditional
        /// check or change the returned value.
        /// </summary>
        /// <typeparam name="T">The type of the object to be returned.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key to access the collection item.</param>
        /// <returns>The stringly-typed object.</returns>
        protected virtual T Get<T>(NameValueCollection collection, string key)
        {
            return collection.G<T>(key);
        }

        /// <summary>
        /// Return's the column collection from the request values.
        /// This method is provided as an option for you to override the default behavior and add additional
        /// check or change the returned value.
        /// </summary>
        /// <param name="collection">The request value collection.</param>
        /// <returns>The column collection or an empty list. For default behavior, do not return null!</returns>
        protected virtual List<Column> GetColumns(NameValueCollection collection)
        {
            try
            {
                var columns = new List<Column>();

                // Loop through every request parameter to avoid missing any DataTable column.
                for (int i = 0; i < collection.Count; i++)
                {
                    var columnData = Get<string>(collection, String.Format(COLUMN_DATA_FORMATTING, i));
                    var columnName = Get<string>(collection, String.Format(COLUMN_NAME_FORMATTING, i));

                    if (columnData != null && columnName != null)
                    {
                        var columnSearchable = Get<bool>(collection, String.Format(COLUMN_SEARCHABLE_FORMATTING, i));
                        var columnOrderable = Get<bool>(collection, String.Format(COLUMN_ORDERABLE_FORMATTING, i));
                        var columnSearchValue = Get<string>(collection, String.Format(COLUMN_SEARCH_VALUE_FORMATTING, i));
                        var columnSearchRegex = Get<bool>(collection, String.Format(COLUMN_SEARCH_REGEX_FORMATTING, i));

                        columns.Add(new Column(columnData, columnName, columnSearchable, columnOrderable, columnSearchValue, columnSearchRegex));
                    }
                    else break; // Stops iterating because there's no more columns.
                }

                return columns;
            }
            catch
            {
                // Returns an empty column collection to avoid null exceptions.
                return new List<Column>();
            }
        }

        /// <summary>
        /// Configure column's ordering.
        /// This method is provided as an option for you to override the default behavior.
        /// </summary>
        /// <param name="collection">The request value collection.</param>
        /// <param name="columns">The column collection as returned from GetColumns method.</param>
        protected virtual void ParseColumnOrdering(NameValueCollection collection, IEnumerable<Column> columns)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                var orderColumn = Get<int>(collection, String.Format(ORDER_COLUMN_FORMATTING, i));
                var orderDirection = Get<string>(collection, String.Format(ORDER_DIRECTION_FORMATTING, i));

                if (orderColumn > -1 && orderDirection != null)
                    columns.ElementAt(orderColumn).SetColumnOrdering(i, orderDirection);
            }
        }

    }

}

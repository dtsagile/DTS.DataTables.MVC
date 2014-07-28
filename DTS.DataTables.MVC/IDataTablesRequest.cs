using System.Collections.Generic;

namespace DTS.DataTables.MVC
{
    /// <summary>
    /// Defines a server-side request for use with DataTables.
    /// </summary>
    /// <remarks>
    /// Variable syntax does NOT match DataTables names because auto-mapping won't work anyway.
    /// Use the DataTablesModelBinder or provide your own binder to bind your model with DataTables's request.
    /// </remarks>
    public interface IDataTablesRequest
    {
        /// <summary>
        /// Gets the draw counter from client-side to give back on the server's response.
        /// </summary>
        int Draw { get; }
        /// <summary>
        /// Gets the start record number (count) for paging.
        /// </summary>
        int Start { get; }
        /// <summary>
        /// Gets the length of the page (max records per page).
        /// </summary>
        int Length { get; }
        /// <summary>
        /// Gets the global search pagameters.
        /// </summary>
        Search Search { get; }
        /// <summary>
        /// Gets the read-only collection of client-side columns with their options and configs.
        /// </summary>
        ColumnCollection Columns { get; }

        string Extra { get; }



    }
    /// <summary>
    /// For internal use only.
    /// Represents DataTables request parameters.
    /// </summary>
    public class DataTablesRequest : IDataTablesRequest
    {
        public DataTablesRequest() { }

        /// <summary>
        /// For internal use only.
        /// Gets/Sets the draw counter from DataTables.
        /// </summary>
        public int Draw { get; set; }
        /// <summary>
        /// For internal use only.
        /// Gets/Sets the start record number (jump) for paging.
        /// </summary>
        public int Start { get; set; }
        /// <summary>
        /// For internal use only.
        /// Gets/Sets the length of the page (paging).
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// For internal use only.
        /// Gets/Sets the global search term.
        /// </summary>
        public Search Search { get; set; }
        /// <summary>
        /// For internal use only.
        /// Gets/Sets the column collection.
        /// </summary>
        //public ColumnCollection Columns { get; private set; }
        public ColumnCollection Columns { get; set; }
        /// <summary>
        /// For internal use only.
        /// Set the new columns on the mechanism.
        /// </summary>
        /// <param name="columns">The columns to be set.</param>
        public void SetColumns(IEnumerable<Column> columns) { Columns = new ColumnCollection(columns); }

        public string Extra { get; set; }

    }
}
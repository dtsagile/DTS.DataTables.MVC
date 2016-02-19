using System;

namespace DTS.DataTables.MVC
{
    /// <summary>
    /// Attribute for decorating enums
    /// </summary>
    public class DataTablesColumnAttribute : Attribute
    {
        public DataTablesColumnAttribute()
        {
            this.Orderable = true;
            this.Visible = true;
            this.Searchable = true;
        }

        /// <summary>
        /// Class(es) assigned to column
        /// see https://datatables.net/reference/option/columns.className
        /// </summary>
        public virtual string ClassName { get; set; }


        /// <summary>
        /// Add padding to the text content used when calculating the optimal with for a table.
        /// Generally will not need this, see https://datatables.net/reference/option/columns.contentPadding
        /// </summary>
        public virtual string ContentPadding { get; set; }


        /// <summary>
        /// Set the data source for the column from the rows data object / array
        /// see https://datatables.net/reference/option/columns.data
        /// </summary>
        public virtual string Data { get; set; }

        /// <summary>
        /// Set default, static, content for a column
        /// see https://datatables.net/reference/option/columns.defaultContent
        /// </summary>
        public virtual string DefaultContent { get; set; }


        /// <summary>
        /// Set a descriptive name for a column
        /// see https://datatables.net/reference/option/columns.name
        /// </summary>
        public virtual string Name { get; set; }


        /// <summary>
        /// Enable or disable ordering on this column
        /// see https://datatables.net/reference/option/columns.orderable
        /// </summary>
        public virtual bool Orderable { get; set; }


        /// <summary>
        /// Define multiple column ordering as the default order for a column 
        /// see https://datatables.net/reference/option/columns.orderData
        /// </summary>
        public virtual int[] OrderData { get; set; }


        /// <summary>
        /// Order direction application sequence
        /// see https://datatables.net/reference/option/columns.orderSequence
        /// </summary>
        public virtual string[] OrderSequence { get; set; }


        /// <summary>
        /// Enable or disable filtering on the data in this column
        /// see https://datatables.net/reference/option/columns.searchable
        /// </summary>
        public virtual bool Searchable { get; set; }

        /// <summary>
        /// Set the column title
        /// see https://datatables.net/reference/option/columns.title
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Set the column type - used for filtering and sorting string processing.
        /// see https://datatables.net/reference/option/columns.title
        /// </summary>
        public virtual ColumnTypes Type { get; set; }

        /// <summary>
        /// Enable or disable the display of this column
        /// see https://datatables.net/reference/option/columns.visible
        /// </summary>
        public virtual bool Visible { get; set; }

        /// <summary>
        /// Column width assignment
        /// see https://datatables.net/reference/option/columns.width
        /// </summary>
        public virtual string Width { get; set; }
    }
}

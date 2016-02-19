using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTS.DataTables.MVC
{
    public class ColumnHeader
    {


        /// <summary>
        /// Class(es) assigned to column
        /// </summary>
        public  string className { get; set; }


        /// <summary>
        /// Add padding to the text content used when calculating the optimal with for a table.
        /// </summary>
        public  string contentPadding { get; set; }


        /// <summary>
        /// Set the data source for the column from the rows data object / array
        /// </summary>
        public  string data { get; set; }


        /// <summary>
        /// Set default, static, content for a column
        /// </summary>
        public  string defaultContent { get; set; }


        /// <summary>
        /// Set a descriptive name for a column
        /// </summary>
        public  string name { get; set; }


        /// <summary>
        /// Enable or disable ordering on this column
        /// </summary>
        public  bool orderable { get; set; }


        /// <summary>
        /// Define multiple column ordering as the default order for a column 
        /// </summary>
        public  int[] orderData { get; set; }


        /// <summary>
        /// Order direction application sequence
        /// </summary>
        public string[] orderSequence { get; set; }


        /// <summary>
        /// Enable or disable filtering on the data in this column
        /// </summary>
        public  bool searchable { get; set; }

        /// <summary>
        /// Set the column title
        /// </summary>
        public  string title { get; set; }

        public string type { get; set; }

        /// <summary>
        /// Enable or disable the display of this column
        /// </summary>
        public  bool visible { get; set; }

        /// <summary>
        /// Column width assignment 
        /// </summary>
        public  string width { get; set; }

        public ColumnHeader() {
            this.orderable = true;
            this.searchable = true;
            this.visible = true;
        }

        public ColumnHeader(DataTablesColumnAttribute attr, string dataName)
        {
            this.className = getValue(attr.ClassName);
            this.contentPadding = getValue(attr.ContentPadding);
            this.data = string.IsNullOrWhiteSpace(attr.Data) ? dataName : attr.Data;
            this.defaultContent = getValue(attr.DefaultContent);
            this.name = getValue(attr.Name);
            this.orderable = attr.Orderable;
            this.orderData = attr.OrderData != null ? attr.OrderData : default(int[]);
            this.orderSequence = attr.OrderSequence != null ? attr.OrderSequence : default(string[]);
            this.searchable = attr.Searchable;
            this.title = getValue(attr.Title);
            this.type = EnumHelpers.GetDescription(attr.Type);
            this.visible = attr.Visible;
            this.width = getValue(attr.Width);
        }

        private string getValue(string attr)
        {
            if (string.IsNullOrWhiteSpace(attr))
            {
                return string.Empty;
            }
            return attr;
        }


        #region Json Serialation 
        /* 
        Only serialize properties that are not null. DataTables will complain if it stumbles upon a null, 'connect get length of null'
        see http://www.newtonsoft.com/json/help/html/ConditionalProperties.htm for this handy serialization trick
        */

        public bool ShouldSerializeclassName()
        {
            return !string.IsNullOrWhiteSpace(this.className);  
        }

        public bool ShouldSerializecontentPadding()
        {
            return !string.IsNullOrWhiteSpace(this.contentPadding);
        }

        public bool ShouldSerializedata()
        {
            return !string.IsNullOrWhiteSpace(this.data);
        }

        public bool ShouldSerializedefaultContent()
        {
            return !string.IsNullOrWhiteSpace(this.defaultContent);
        }

        public bool ShouldSerializename()
        {
            return !string.IsNullOrWhiteSpace(this.name);
        }

        public bool ShouldSerializeorderData()
        {
            return this.orderData != null;
        }

        public bool ShouldSerializeorderSequence()
        {
            return this.orderSequence != null;
        }

        public bool ShouldSerializetitle()
        {
            return !string.IsNullOrWhiteSpace(this.title);
        }

        public bool ShouldSerializewidth()
        {
            return !string.IsNullOrWhiteSpace(this.width);
        }

        #endregion

    }

}

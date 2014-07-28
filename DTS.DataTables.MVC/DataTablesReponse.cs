using System.Collections;

namespace DTS.DataTables.MVC
{
    /// <summary>
    /// Represents a server-side response for use with DataTables.
    /// </summary>
    /// <remarks>
    /// Variable syntax matches DataTables names to avoid error and avoid aditional parse.
    /// </remarks>
    public class DataTablesResponse
    {
        /// <summary>
        /// Gets the draw counter for DataTables.
        /// </summary>
        public int draw { get; private set; }
        /// <summary>
        /// Gets the data collection.
        /// </summary>
        public IEnumerable data { get; private set; }
        /// <summary>
        /// Gets the total number of records (without filtering - total dataset).
        /// </summary>
        public int recordsTotal { get; private set; }
        /// <summary>
        /// Gets the resulting number of records after filtering.
        /// </summary>
        public int recordsFiltered { get; private set; }
        /// <summary>
        /// Creates a new DataTables response object with it's elements.
        /// </summary>
        /// <param name="draw">The draw counter as received from the DataTablesRequest.</param>
        /// <param name="data">The data collection (data page).</param>
        /// <param name="recordsFiltered">The resulting number of records after filtering.</param>
        /// <param name="recordsTotal">The total number of records (total dataset).</param>
        public DataTablesResponse(int draw, IEnumerable data, int recordsFiltered, int recordsTotal)
        {
            this.draw = draw;
            this.data = data;
            this.recordsFiltered = recordsFiltered;
            this.recordsTotal = recordsTotal;
        }
    }
}

using System;

namespace DTS.DataTables.MVC
{
    /// <summary>
    /// Stores parameters and configs from DataTables search engine.
    /// </summary>
    public class Search
    {
        /// <summary>
        /// Gets the value of the search.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Indicates if the value of the search is a regex value or not.
        /// </summary>
        public bool IsRegexValue { get; set; }
        /// <summary>
        /// Creates a new search values holder object.
        /// </summary>
        /// <param name="value">The value of the search.</param>
        /// <param name="isRegexValue">True if the value is a regex value or false otherwise.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the provided search value is null.</exception>
        public Search(string value, bool isRegexValue)
        {
            if (value == null) throw new ArgumentNullException("value", "The value of the search cannot be null. If there's no search performed, provide an empty string.");

            this.Value = value;
            this.IsRegexValue = isRegexValue;
        }
    }
}

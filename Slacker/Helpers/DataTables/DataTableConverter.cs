using FastMember;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Slacker.Helpers.DataTables
{
    // <summary>
    /// Utility class for generating DataTable objects from List Objects
    /// </summary>
    /// <typeparam name="T">List object's type</typeparam>
    public class DataTableConverter<T>
    {
        /// <summary>
        /// Converts a list of T to list
        /// </summary>
        /// <param name="list">List of T</param>
        /// <returns>DataTable converted from list</returns>
        public DataTable ConvertListToDataTable(List<T> list)
        {
            return DoConvertListToDataTable(list);
        }

        /// <summary>
        /// Converts a list of T to list
        /// </summary>
        /// <param name="list">List of T</param>
        /// <returns>DataTable converted from list</returns>
        public DataTable ConvertListToDataTable<T>(List<T> list)
        {
            return DoConvertListToDataTable(list);
        }


        /// <summary>
        /// Performs actual conversion from List of type to datatable
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="list">List of T</param>
        /// <returns>DataTable converted from type</returns>
        private static DataTable DoConvertListToDataTable<T>(IEnumerable<T> list)
        {
            var table = new DataTable();
            using (var reader = ObjectReader.Create(list))
            {
                table.Load(reader);
            }
            return table;
        }

        /// <summary>
        /// Converts a list of string[] to list
        /// </summary>
        /// <param name="list">List of String[]</param>
        /// <returns>DataTable converted from list</returns>
        public static DataTable ConvertListToDataTable(List<string[]> list)
        {
            // New table.
            var table = new DataTable();

            // Get max columns.
            var columns = list.Select(array => array.Length).Concat(new[] { 0 }).Max();

            // Add columns.
            for (var i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }
    }
}

using System.Collections.Generic;

namespace WebAPI.Helper
{
    public class SortingHelper
    {
        public static KeyValuePair<string, string>[] getSortFields()
        {
            return new[] { SortFields.Title, SortFields.CreationDate };
        }
    }

    public class SortFields
    {
        public static KeyValuePair<string, string> Title { get; } = new KeyValuePair<string, string>("title", "Title");
        public static KeyValuePair<string, string> CreationDate { get; } = new KeyValuePair<string, string>("creationdate", "Created");

    }
}

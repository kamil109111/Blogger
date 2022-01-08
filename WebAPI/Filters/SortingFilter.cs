﻿using System.Linq;
using WebAPI.Helper;

namespace WebAPI.Filters
{
    public class SortingFilter
    {
        public string SortField { get; set; }
        public bool Ascending { get; set; } = true;

        public SortingFilter()
        {
            SortField = "Id";
        }

        public SortingFilter(string sortField, bool ascending)
        {
            var sortFields = SortingHelper.getSortFields();

            sortField = sortField.ToLower();

            if (sortFields.Select(x => x.Key).Contains(sortField.ToLower()))
                sortField = sortFields.Where(x => x.Key == sortField).Select(x => x.Value).SingleOrDefault();
            else
                sortField = "Id";

            SortField = sortField;
            Ascending = ascending;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using WebAPI.Filters;
using WebAPI.Wrappers;

namespace WebAPI.Helper
{
    public class PaginationHelper
    {
        public static PageResponse<IEnumerable<T>> CreatePageResponse<T>(IEnumerable<T> pagedData,
            PaginationFilter validPaginationFilter, int totalRecords)
        {
            var response = new PageResponse<IEnumerable<T>>(pagedData, validPaginationFilter.PageNumber,
                validPaginationFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validPaginationFilter.PageSize);
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            int currentPage = validPaginationFilter.PageNumber;

            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;
            response.PreviousPage = currentPage > 1 ? true : false;
            response.NextPage = currentPage < roundedTotalPages ? true : false;

            return response;
        }
    }
}

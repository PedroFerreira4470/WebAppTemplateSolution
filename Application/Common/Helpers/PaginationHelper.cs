using Application.Common.Contracts.V1.ResponseType;
using Application.Common.Interfaces;
using Application.Common.MethodExtensions;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;

namespace Application.Common.Helpers
{
    public static class PaginationHelper
    {

        public static PagedResponse<T> CreatePagedResponse<T>(IEnumerable<T> pagedData, int pageSize, int pageNumber, int totalRecords, IUriService uriService)
        {
            var totalPages = (totalRecords / (double)pageSize);
            var lastPageNumber = Convert.ToInt32(Math.Ceiling(totalPages));
            var previewsPageNumber = pageNumber - 1;
            var nextPageNumber = pageNumber + 1;
            const int firstPageNumber = 1;

            var response = new PagedResponse<T>
            {
                Data = pagedData,
                PageNumber = pageNumber,
                PageSize = pageSize,
                NextPage = pageNumber.IsBiggerOrEqualsThan(firstPageNumber) && pageNumber.IsSmallerThan(lastPageNumber)
                    ? GetPagedPath(nextPageNumber, pageSize, uriService)
                    : null,
                PreviousPage = previewsPageNumber.IsBiggerOrEqualsThan(firstPageNumber) && pageNumber.IsSmallerOrEqualsThan(lastPageNumber)
                    ? GetPagedPath(previewsPageNumber, pageSize, uriService)
                    : null,
                FirstPage = GetPagedPath(firstPageNumber, pageSize, uriService),
                LastPage = GetPagedPath(lastPageNumber, pageSize, uriService),
                TotalPages = lastPageNumber,
                TotalRecords = totalRecords,
            };

            return response;
        }

        private static string GetPagedPath(int pageNumber, int pageSize, IUriService uri)
        {
            var absolutePath = uri.GetAbsolutePath();
            var modifiedUri = QueryHelpers.AddQueryString(absolutePath, nameof(pageNumber), pageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, nameof(pageSize), pageSize.ToString());
            return modifiedUri;
        }
    }
}

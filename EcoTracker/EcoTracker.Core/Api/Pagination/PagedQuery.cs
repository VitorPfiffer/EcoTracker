using System.ComponentModel.DataAnnotations;

namespace EcoTracker.Core.Api.Pagination
{
    public sealed class PagedQuery
    {
        [Required(ErrorMessage = "Page is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0.")]
        public int Page { get; set; }

        [Required(ErrorMessage = "PageSize is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "PageSize must be greater than 0.")]
        public int PageSize { get; set; }

        public string? OrderBy { get; set; }

        public string? Filters { get; set; }

        public PagedQuery()
        {
        }

        public PagedQuery(int page, int pageSize, string filters, string? orderBy = null)
        {
            Page = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            Filters = filters;
        }
    }
}

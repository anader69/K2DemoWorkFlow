using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

namespace Framework.Core.Data
{
    public abstract class PagingDto
    {
        public StaticPagedList<object> Items { get; set; }

        [HiddenInput]
        public int PageNumber { get; set; } = 1;

        public int? PageSize { get; set; }

        public bool IsExport { get; set; }
        [HiddenInput]
        public bool IsDescending { get; set; } = true;

        public bool IsSearchOpen { get; set; }

        public string ReturnUrl { get; set; }

    }
}

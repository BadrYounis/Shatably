using Microsoft.AspNetCore.Mvc;

namespace Shared
{
    public class ProjectSpecificationsParameters
    {
        public string? Title { get; set; }
        public ProjectSortOptions? Sort { get; set; }
        public string? Search { get; set; }
        public int pageIndex { get; set; } = 1;
        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 5;
        private int _pageSize = DefaultPageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value <= 0 ? DefaultPageSize : (value > MaxPageSize ? MaxPageSize : value);
        }
    }
    public enum ProjectSortOptions
    {
        TitleAsc,  //0
        TitleDesc,
        IdAsc,
        IdDesc,
    }
}


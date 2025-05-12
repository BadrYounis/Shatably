using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class DesignTemplateSpecificationsParameters
    {
        public string? category { get; set; }
        public DesignTemplateSortOptions? Sort { get; set; }
        public string? Search { get; set; }
        public int pageIndex { get; set; } = 1;
        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 5;
        private int _pageSize = DefaultPageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
    public enum DesignTemplateSortOptions
    {
        NameAsc,  //0
        NameDesc,
        //PriceAsc,
        //PriceDesc,
    }
}

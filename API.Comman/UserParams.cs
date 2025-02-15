using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Comman
{
    public class UserParams
    {
        public const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        public int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? Search { get; set; }
    }
}
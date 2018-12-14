using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strongpoint.Models
{
    public class Paging
    {
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public int? TotalRows { get; set; }
    }
}

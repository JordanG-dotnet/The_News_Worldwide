using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWorldwide.Models
{
    public class TopNewsViewModel
    {
        public string Country { get; set; }
        public int CurrentPage { get; set; }
        public decimal TotalNumPages { get; set; }
        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }
}

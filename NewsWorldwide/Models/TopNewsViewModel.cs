using System.Collections.Generic;

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

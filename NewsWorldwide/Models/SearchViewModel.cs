using System.Collections.Generic;

namespace NewsWorldwide.Models
{
    public class SearchViewModel
    {
        public string Criteria { get; set; }
        public int CurrentPage { get; set; }
        public decimal TotalNumPages { get; set; }
        public string Language { get; set; }
        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }
}

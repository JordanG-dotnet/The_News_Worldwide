using System;
using System.Collections.Generic;
using NewsAPI.Models;

namespace NewsWorldwide.Models
{
    public class SearchViewModel
    {
        public string Criteria { get; set; }
        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }
}

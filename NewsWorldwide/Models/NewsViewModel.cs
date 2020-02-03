using System;
using System.Collections.Generic;

namespace NewsWorldwide.Models
{
    public class NewsViewModel
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public ICollection<ArticleViewModel> Articles { get; set; }
    }
}

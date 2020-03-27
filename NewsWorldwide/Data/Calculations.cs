using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWorldwide.Data
{
    public static class Calculations
    {
        public static decimal TotalNumPages(int listCount)
        {
            var pages = listCount / 3.0M;
            return Math.Ceiling(pages);
        }
    }
}

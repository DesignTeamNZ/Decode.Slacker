using System;
using System.Collections.Generic;
using System.Text;

namespace Slacker {
    
    public class QueryProps : SqlProps {
        public int? Top { get; set; }
        public string OrderBy { get; set; }
        public int? Offset { get; set; }
        public int? Limit { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Slacker {

    public class SqlProps {
        public string WhereSql { get; set; }
        public object WhereParams { get; set; }

        public Func<string, string> PostEditSQL { get; set; } = sql => sql;
    }

}

using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Exceptions {
    public class SlackerSqlException : Exception {
        
        public string SqlStatement { get; private set; }
        public object ParameterObject { get; private set; }

        public SlackerSqlException(SqlException inner, string sql, object parameter) 
            : base("Slacker caught a general SQL exception.", inner) {
            this.SqlStatement = sql;
            this.ParameterObject = parameter;
        }

    }
}

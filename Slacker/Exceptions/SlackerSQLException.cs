using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Exceptions {
    public class SlackerSqlException : Exception {
        
        public string SqlStatement { get; private set; }
        public object ParameterObject { get; private set; }

        public SlackerSqlException(SqlException inner, string sql, object parameter) 
            : base("Internal Slacker exception.", inner) {
            this.SqlStatement = sql;
            this.ParameterObject = parameter;
        }


    }
}

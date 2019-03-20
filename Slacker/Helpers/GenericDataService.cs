using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker.Connection;

namespace Slacker.Helpers {
    /// <summary>
    /// A generic dataservice for models that don't need an extensible dataservice
    /// </summary>
    public class GenericDataService<T> : DataService<T> where T : DataModel, new() {
        public GenericDataService(DataServiceConnectionManager connectionManager = null) 
            : base(connectionManager) {
        }
    }
}

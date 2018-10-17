using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Slacker {

    public class SlackerApp {
        
        /// <summary>
        /// Initalize all dataservices matching a specific type filter with SqlConnection
        /// </summary>
        public static void InitializeDataServices(Func<Type, bool> filter, SqlConnection conn) {
            foreach (Type type in GetAllDataServices()) {
                if (filter != null && !filter(type)) {

                    var constructor = type.GetConstructors().FirstOrDefault(
                        constr => {
                            var prams = constr.GetParameters();
                            return prams.Count() == 1 
                                && prams[0].ParameterType.IsAssignableFrom(typeof(SqlConnection));
                        }
                    );

                    if (constructor == null) {
                        throw new Exception($"Could not initialize DS '{type.Name}'. " + 
                            $"DataSource requires a constructor that takes " +
                            $"only '{ typeof(SqlConnection) }' argument.");
                    }

                    constructor.Invoke(new object[] { conn });
                }
            }
        }

        private static IEnumerable<Type> GetAllDataServices() {
            return   
                from assem in AppDomain.CurrentDomain.GetAssemblies()
                from types in assem.GetTypes().Where(
                    typ => typeof(IDataService).IsAssignableFrom(typ) 
                        && !typ.IsAbstract
                        && !typ.IsInterface
                )
                select types;
        }
    }
}

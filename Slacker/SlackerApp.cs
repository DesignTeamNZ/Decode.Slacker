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
        
        
        public static void InitializeDataServices(Func<Type, bool> filter, object conn) {
            var services = GetAllDataServices();
            foreach (Type type in GetAllDataServices()) {
                if (filter != null && !filter(type)) {
                    continue;
                }

                var constructor = type.GetConstructors().FirstOrDefault(
                    construc => {
                        var parameters = construc.GetParameters();
                        return parameters.Count() == 1 && 
                            parameters[0].ParameterType == conn.GetType();
                    }
                );

                if (constructor == null) {
                    throw new Exception($"Could not initialize DS '{type.Name}'. "
                        + $"DS requires a constructor that takes only '{conn.GetType()}' argument.");
                }

                constructor.Invoke(new object[] { conn });
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

    [Flags]
    public enum SlackerFlags {
        PRE_LOOKUP_MODELS = 1,
        PRE_INITIALIZE_DATASERVICES = 2,
        ON_EXCEPTION_THROW = 3,
        ON_EXCEPTION_DISPLAY_MESSAGEBOX = 4
    }

}

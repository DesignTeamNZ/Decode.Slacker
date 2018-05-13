using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Slacker {

    public class SlackerApp {

        /// <summary>
        /// Slacker Operation Flags with Defaults
        /// </summary>
        public static SlackerFlags Flags =
            SlackerFlags.PRE_LOOKUP_MODELS |
            SlackerFlags.ON_EXCEPTION_THROW;
        
        public static void Initialize(SlackerFlags flags, SqlConnection connection) {
            SlackerApp.Flags = flags;

            // Pre-Initialize Data Services
            if (flags.HasFlag(SlackerFlags.PRE_INITIALIZE_DATASERVICES)) {
                foreach (Type type in GetAllDataServices()) {
                    var constructor = type.GetConstructors().FirstOrDefault(
                        construc => 
                            construc.GetParameters().Count() == 1 && 
                            construc.GetParameters()[0].GetType() == typeof(SqlConnection)
                    );

                    if (constructor == null) {
                        continue;
                    }

                    constructor.Invoke(new object[] { connection });
                }
            }
        }

        private static IEnumerable<Type> GetAllDataServices() {
            return   
                from assem in AppDomain.CurrentDomain.GetAssemblies()
                from types in assem.GetTypes().Where(
                    typ => typ.IsSubclassOf(typeof(IDataService))
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

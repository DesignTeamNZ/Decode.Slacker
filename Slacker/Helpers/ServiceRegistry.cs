using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Helpers {
    public class ServiceRegistry {

        protected Dictionary<Type, object> serviceRegistry 
            = new Dictionary<Type, object>();

        public bool Register(Type managingType, object service) {
            if (serviceRegistry.ContainsKey(managingType)) { 
                return false;
            }

            serviceRegistry.Add(managingType, service);
            return true;
        }

        public void Update<T>(Type managingType, object service) {
            if (serviceRegistry.ContainsKey(managingType)) { 
                serviceRegistry.Remove(managingType);
            }

            serviceRegistry.Add(managingType, service);
        }
        

        public object GetService(Type managingType) {
            if (serviceRegistry.TryGetValue(managingType, out object value)) {
                return value;
            }

            return null;
        }

    }
}

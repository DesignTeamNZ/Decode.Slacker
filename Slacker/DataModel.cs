using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Slacker {

    public interface IDataModel {
        void PropChanged([CallerMemberName] string propertyName = "");
    }
    
    public class DataModel {

        /// <summary>
        /// Keeps track of what properties were changed on Model
        /// </summary>
        protected List<String> ChangedProperties = new List<String>();
        
        /// <summary>
        /// Used to notify DataService of what properties were changed on this model
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        protected void PropChanged([CallerMemberName] String propertyName = "") {
            if (ChangedProperties.Contains(propertyName)) {
                return; 
            }
            
            ChangedProperties.Add(propertyName);
        }

        /// <summary>
        /// Resets change tracking on a given model
        /// </summary>
        public void ClearChangeTracking() {
            ChangedProperties.Clear();
        }

        /// <summary>
        /// Gets a list of changed fields for a given model
        /// </summary>
        /// <returns></returns>
        public List<String> GetChangedFields() {
            return ChangedProperties;
        }

    }
    
}

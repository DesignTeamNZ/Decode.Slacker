using Slacker.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Slacker {

    public interface IDataModel {

    }
    
    public class DataModel : IDataModel {

        /// <summary>
        /// Keeps track of what properties were changed on Model
        /// </summary>
        [Field(Ignored = true)]
        public IList<string> ChangedProperties { get; set; } = new List<string>();

        /// <summary>
        /// Enables/Disables change tracking.
        /// </summary>
        [Field(Ignored = true)]
        public bool ChangeTracking { get; set; } = true;
        
        /// <summary>
        /// Used to notify DataService of what properties were changed on this model
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        protected void PropChanged([CallerMemberName] string propertyName = null) {
            if (propertyName == null || !ChangeTracking) {
                return;
            }

            if (ChangedProperties.Contains(propertyName)) {
                return; 
            }
            
            ChangedProperties.Add(propertyName);
        }
    }
    
}

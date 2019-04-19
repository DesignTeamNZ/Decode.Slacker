using PropertyChanged;
using Slacker.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Slacker {

    public interface IDataModel : INotifyPropertyChanged {
        void OnPropertyChanged(string propertyName, object before, object after);
    }
    
    public class DataModel : IDataModel {
        
        /// <summary>
        /// Keeps track of what properties were changed on Model
        /// </summary>
        [DoNotNotify]
        [Field(Ignored = true)]
        private IList<string> ChangedProperties { get; set; } = new List<string>();


        private bool _changeTrackingDisabled;
        /// <summary>
        /// Enables/Disables change tracking.
        /// </summary>
        [DoNotNotify]
        [Field(Ignored = true)]
        private bool ChangeTrackingDisabled {
            get {
                return _changeTrackingDisabled;
            }
            set {
                _changeTrackingDisabled = value;
                if (value) ClearChangedPropertiesList();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises a Property Changed event
        /// </summary>
        public void OnPropertyChanged(string propertyName, object before, object after) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == null || ChangeTrackingDisabled ) {
                return;
            }

            // Register Property Changed
            if (!ChangedProperties.Contains(propertyName) && before != after) {
                ChangedProperties.Add(propertyName);
            }

        }

        /// <summary>
        /// Gets the Change Tracking disabled status for this model
        /// </summary>
        public bool IsChangeTrackingDisabledStatus() {
            return ChangeTrackingDisabled;
        }

        /// <summary>
        /// Sets the Change Tracking Disabled status for this model
        /// </summary>
        public void SetChangeTrackingDisabledStatus(bool disabled) {
            this.ChangeTrackingDisabled = disabled;
        }

        /// <summary>
        /// Keeps track of what properties were changed on Model
        /// </summary>
        public IList<string> GetChangedPropertiesList() {
            return this.ChangedProperties;
        }

        /// <summary>
        /// Clears current changed properties list
        /// </summary>
        public void ClearChangedPropertiesList() {
            this.ChangedProperties.Clear();
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Slacker {
    
    public class DataModel : IDataModel {

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Keeps track of what properties were changed on Model
        /// </summary>
        private IList<string> ChangedProperties { get; set; } = new List<string>();


        private bool _changeTrackingDisabled;
        /// <summary>
        /// Enables/Disables change tracking.
        /// </summary>
        private bool ChangeTrackingDisabled {
            get {
                return _changeTrackingDisabled;
            }
            set {
                _changeTrackingDisabled = value;
                if (value) ClearChangedPropertiesList();
            }
        }

        public virtual bool SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = "") {
            if (object.Equals(field, value)) {
                return false;
            }

            var oldValue = field;
            field = value;
            OnPropertyChanged(propertyName, oldValue, value);
            return true;
        }

        /// <summary>
        /// Raises a Property Changed event
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName, object before, object after) {

            // Register Property Changed
            if (!ChangedProperties.Contains(propertyName) && propertyName != "" && before != after) {
                ChangedProperties.Add(propertyName);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets the Change Tracking disabled status for this model
        /// </summary>
        public bool IsChangeTrackingDisabledStatus() {
            return ChangeTrackingDisabled;
        }

        /// <summary>
        /// Raise property changed event
        /// </summary>
        protected void RaisePropertyChanged(object sender, PropertyChangedEventArgs e) {
            this.PropertyChanged?.Invoke(sender, e);
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

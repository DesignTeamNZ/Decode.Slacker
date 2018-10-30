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

        private IList<string> _changedProperties = new List<string>();
        /// <summary>
        /// Keeps track of what properties were changed on Model
        /// </summary>
        [DoNotNotify]
        [Field(Ignored = true)]
        public IList<string> ChangedProperties {
            get {
                return _changedProperties;    
            }
            set {
                _changedProperties = value;
            }
        }


        private bool _changeTrackingDisabled;
        /// <summary>
        /// Enables/Disables change tracking.
        /// </summary>
        [DoNotNotify]
        [Field(Ignored = true)]
        public bool ChangeTrackingDisabled {
            get {
                return _changeTrackingDisabled;
            }
            set {
                _changeTrackingDisabled = value;
                if (value) ChangedProperties.Clear();
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
    }
    
}

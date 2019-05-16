using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Slacker {

    public interface IDataModel : INotifyPropertyChanged {
        void OnPropertyChanged(string propertyName, object before, object after);
    }

}

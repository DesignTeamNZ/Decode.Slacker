using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.AutoModeller.Models {
    
    // Using a Partial Class here lets us append custom functionality to generated models
    public partial class InformationSchemaColumn : DataModel {

        [SlackerIgnore]
        public bool IsNullable {
            get => IsNullableText == "YES";
            set => IsNullableText = value ? "YES" : "NO";
        }

    }
}

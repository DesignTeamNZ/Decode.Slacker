using System;
using System.Collections.Generic;
using System.Text;

namespace Slacker.API.Attributes {

    /// <summary>
    /// Marks this properties dependencies. Their property changed events raise this 
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DependsOnAttribute : Attribute {
        
        public string[] Names { get; set; }

        public DependsOnAttribute(params string[] names) {
            this.Names = names;
        }

    }
}

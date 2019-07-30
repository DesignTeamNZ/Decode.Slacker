using System;
using System.Collections.Generic;
using System.Text;

namespace Slacker.API.Attributes {

    /// <summary>
    /// Notifies that the target member/(members of class) aren't data fields
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class)]
    public class SlackerIgnoreAttribute : System.Attribute { }

}

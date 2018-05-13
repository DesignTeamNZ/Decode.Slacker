using System;
using Slacker;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SlackerTests {
    class Program {

        [STAThread]
        static void Main(string[] args) {

            SlackerFlags slackerFlags =
                SlackerFlags.PRE_LOOKUP_MODELS |
                SlackerFlags.PRE_INITIALIZE_DATASERVICES |
                SlackerFlags.ON_EXCEPTION_THROW;

            // TODO: Setup Test Connection
            SlackerApp.Initialize(slackerFlags, null);

        }

    }
}

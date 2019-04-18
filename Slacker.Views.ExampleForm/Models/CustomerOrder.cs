using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Views.ExampleForm.Models {
    public class CustomerOrder : DataModel {

        public int OrderId { get; set; }

        public string OrderSummary { get; set; }

        public int CustomerID { get; set; }
        
        public bool OrderPaid { get; set; }

    }
}

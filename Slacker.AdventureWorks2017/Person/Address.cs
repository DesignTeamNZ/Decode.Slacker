using Slacker.Connection;
using Slacker.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slacker.AdventureWorks2017.Person {

    [Table("Person.PersonAddress")]
    public class Address : DataModel {
        #region Instance Properties

        public Int32 AddressID { get; set; }

        public String AddressLine1 { get; set; }

        public String AddressLine2 { get; set; }

        public String City { get; set; }

        public Int32 StateProvinceID { get; set; }

        public String PostalCode { get; set; }

        public Object SpatialLocation { get; set; }

        [Field(Name = "rowguid")]
        public Guid RowGuid { get; set; }

        public DateTime ModifiedDate { get; set; }

        #endregion Instance Properties
    }

    public class AddressDataService : DataService<Address> {

        public AddressDataService(DataServiceConnectionManager connManager)
            : base(connManager) {
        }

    }
}

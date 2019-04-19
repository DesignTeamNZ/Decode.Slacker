using Slacker.Connection;
using Slacker.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slacker.AdventureWorks2017.Person {
    [Table("Person.Person", "personPerson")]
    public class Person : DataModel {
        #region Instance Properties

        public Int32 BusinessEntityID { get; set; }

        public String PersonType { get; set; }

        public Boolean NameStyle { get; set; }

        public String Title { get; set; }

        public String FirstName { get; set; }

        public String MiddleName { get; set; }

        public String LastName { get; set; }

        public String Suffix { get; set; }

        public Int32 EmailPromotion { get; set; }

        public Object AdditionalContactInfo { get; set; }

        public Object Demographics { get; set; }

        [Field(Name = "rowguid")]
        public Guid RowGuid { get; set; }

        public DateTime ModifiedDate { get; set; }

        #endregion Instance Properties
    }

    public class PersonDataService : DataService<Person> {
        
        public PersonDataService(DataServiceConnectionManager connManager) 
            : base(connManager) {
        }
        
    }
}

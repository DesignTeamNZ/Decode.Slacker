using Slacker;
using Slacker.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackerTests {

    /// <summary>
    /// Example UserDataService for User DataModel
    /// </summary>
    public class UserDataService : DataService<UserModel> {
        public UserDataService(SqlConnection conn) : base(conn) { }
    }

    /// <summary>
    /// Example User DataModel
    /// </summary>
    [Table("Users", "u")]
    public class UserModel : DataModel {

        [Field(IsPrimary = true)]
        public int? Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }

}

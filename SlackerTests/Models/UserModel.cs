using Slacker;
using Slacker.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackerTests
{

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


        private string _username;
        public string Username {
            get {
                return _username;
            }
            set {
                _username = value;
                PropChanged();
            }
        }

        private string _password;
        public string Password {
            get {
                return _password;
            }
            set {
                _password = value;
                PropChanged();
            }
        }

        private string _email;
        public string Email {
            get {
                return _email;
            }
            set {
                _email = value;
                PropChanged();
            }
        }

    }

}

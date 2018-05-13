using Slacker;
using Slacker.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackerTests
{

    /// <summary>
    /// Example UserDataService for User DataModel
    /// </summary>
    public class UserDataService : DataService<User> { }

    /// <summary>
    /// Example User DataModel
    /// </summary>
    public class User : DataModel {

        [Field(IsPrimary = true)]
        public int ID { get; private set; }

        public string Username {
            get {
                return Username;
            }
            set {
                this.Username = value;
                PropChanged();
            }
        }

        public string Password {
            get {
                return Password;
            }
            set {
                this.Password = value;
                PropChanged();
            }
        }

        public string Email {
            get {
                return Email;
            }
            set {
                this.Email = value;
                PropChanged();
            }
        }

    }

}

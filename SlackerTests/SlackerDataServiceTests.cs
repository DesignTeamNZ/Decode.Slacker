using System;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Slacker;

namespace SlackerTests {

    /// <summary>
    /// I'm aware these unit tests are lacking alot. I intend on writing more
    /// in future just a little slack (See what I did there ;D). Feel free to
    /// contribute to this test project if you're reading this.
    /// </summary>
    [TestClass]
    public class SlackerDataServiceTests {
        
        public const string CONN_STR = @"Server=localhost\SQLEXPRESS;Database=SlackerDevelopment;Trusted_Connection=True;";

        public SlackerDataServiceTests() {
            Initialize();
        }
        
        /// <summary>
        /// Initializes the Slacker UserDataService
        /// </summary>
        public void Initialize() {
            var connection = new SqlConnection(CONN_STR);
            connection.OpenAsync().Wait();

            SlackerApp.InitializeDataServices(
                (type) => type == typeof(UserDataService),
                connection
            );
        }


        [TestMethod]
        public void TestInsertSelect() {
            // Get the UserModel managing DataService
            var service = DataService<UserModel>.GetService();

            var insertModel = new UserModel() {
                Username = "TestInsertSelectUser",
                Password = "TestInsertSelectPass",
                Email = "TestInsertSelectUser@localhost"
            };

            service.Insert(insertModel);
            
            // Test generated Id is saved back to model
            Assert.IsNotNull(insertModel.Id);

            // Reselect using username and test values updated
            var result = service.Select("Username=@Username", new {
                Username = "TestInsertSelectUser"
            }).LastOrDefault();

            Assert.AreEqual(insertModel.Id, result.Id);
            Assert.AreEqual(result.Username, "TestInsertSelectUser");
            Assert.AreEqual(result.Password, "TestInsertSelectPass");
            Assert.AreEqual(result.Email, "TestInsertSelectUser@localhost");
        }


        [TestMethod]
        public void TestInsertFind() {
            // Get the UserModel managing DataService
            var service = DataService<UserModel>.GetService();

            // Insert a new model
            var insertModel = new UserModel() {
                Username = "TestInsertFindUser",
                Password = "TestInsertFindPass",
                Email = "TestInsertFindUser@localhost"
            };

            service.Insert(insertModel);

            // Test generated Id is saved back to model
            Assert.IsNotNull(insertModel.Id);

            // Reselect Model and test values updated
            var result = service.Find(insertModel).LastOrDefault();
            Assert.AreEqual(result.Username, "TestInsertFindUser");
            Assert.AreEqual(result.Password, "TestInsertFindPass");
            Assert.AreEqual(result.Email, "TestInsertFindUser@localhost");
        }

        [TestMethod]
        public void TestInsertFindUpdateFind() {

            var service = DataService<UserModel>.GetService();
            var insertModel = new UserModel() {
                Username = "TestInsertUpdateFindUser",
                Password = "TestInsertUpdateFindPass",
                Email = "TestInsertUpdateFind@localhost"
            };

            service.Insert(insertModel);

            // Test generated Id is saved back to model
            Assert.IsNotNull(insertModel.Id);

            // Reselect the model and test values updated
            var result = service.Find(insertModel).LastOrDefault();
            Assert.AreEqual(result.Username, "TestInsertUpdateFindUser");
            Assert.AreEqual(result.Password, "TestInsertUpdateFindPass");
            Assert.AreEqual(result.Email, "TestInsertUpdateFind@localhost");


            insertModel.Password = "NewTestInsertUpdateFindPass";
            service.Update(insertModel);
            
            // Reselect model and compare values and the new password
            result = service.Find(insertModel).LastOrDefault();
            Assert.AreEqual(result.Password, "NewTestInsertUpdateFindPass");
        }

        [TestMethod]
        public void TestDelete() {

            var service = DataService<UserModel>.GetService();

            // Test Delete disabled by default
            Assert.ThrowsException<Exception>(new Action(() => {
                service.Delete("Username=@Username", new {
                    Username = "TestUser"
                });
            }));
            
            service.AllowDelete = true;
            service.Delete("Username=@Username", new {
                Username = "TestUser"
            });

            // Test Global Delete disabled by default
            Assert.ThrowsException<Exception>(new Action(() => {
                service.DeleteAll();
            }));

            service.AllowGlobalDelete = true;
            service.DeleteAll();
        }
        



    }
}

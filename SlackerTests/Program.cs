using System;
using Slacker;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SlackerTests {
    class Program {

        public const string CONN_STR = @"Server=localhost\SQLEXPRESS;Database=SlackerDevelopment;Trusted_Connection=True;";

        static void Main(string[] args) {

            var connection = new SqlConnection(CONN_STR);
            connection.OpenAsync().Wait();

            SlackerApp.InitializeDataServices(
                (type) => type == typeof(UserModel),
                connection
            );

            TestSelect();
        }

        static void TestSelect() {
            var service = DataService<UserModel>.GetModelService();
            var results = service.SelectWhere("Username=@Username", new {
                Username = "TestUser"
            });

            return;
        }

        static void TestInsertUpdate() { 

            var service = DataService<UserModel>.GetModelService();
            var insertModel = new UserModel() {
                Username = "TestUser",
                Password = "TestPass",
                Email = "test@localhost"
            };

            service.Insert(insertModel);


            insertModel.Password = "NewPasswordTest";
            service.Update(insertModel);

            return;

        }

    }
}

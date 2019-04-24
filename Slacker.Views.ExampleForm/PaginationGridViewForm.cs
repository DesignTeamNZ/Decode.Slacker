using Slacker.AdventureWorks2017.Person;
using Slacker.Connection;
using Slacker.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slacker.Views.ExampleForm {
    public partial class PaginationGridViewForm : Form {

        public DataServiceGridPagination<Person> Pagination;

        public PaginationGridViewForm() {
            InitializeComponent();
            
            // Build Pagination
            this.Pagination = DataServiceGridPagination<Person>.Create(true, 500);
            this.Pagination.QueryCondition = "FirstName = @FirstName";
            this.Pagination.QueryConditionParams = new { FirstName = "Kevin" };
            
            // Set DataService
            var connectionManager = DataServiceConnectionManager.FromConfig("AdventureWorks2017");
            Pagination.DataService = new PersonDataService(connectionManager) {
                AllowDelete = true
            };

            // Events
            this.Pagination.ModelEdited += Pagination_ModelEdited;
            this.Pagination.ModelDeleted += Pagination_ModelDeleted;

            // Load Grid
            this.simplePaginationBar1.SetPagination(Pagination);
            this.paginationGridView1.LoadPagination(Pagination);
        }

        private void Pagination_ModelDeleted(object sender, ModelDeletedEventArgs<Person> e) {
            Pagination.DataService.Delete(e.Model);
        }

        private void Pagination_ModelEdited(object sender, ModelEditedEventArgs<Person> e) {
            Pagination.DataService.Update(e.Model);
        }
    }
}

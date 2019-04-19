using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Slacker.Connection;

namespace Slacker.Views.ExampleForm {
    public partial class FormPersonServiceGrid : Form {
        public FormPersonServiceGrid() {
            InitializeComponent();

            personServiceGrid1.DataService = new AdventureWorks2017.Person.PersonDataService(
                DataServiceConnectionManager.FromConfig("AdventureWorks2017")
            );

            personServiceGrid1.PartialLoading = false;
            personServiceGrid1.LoadRecordSet();
        }
    }
}

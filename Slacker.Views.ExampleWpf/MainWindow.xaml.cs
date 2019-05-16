using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Slacker.Connection;
using Slacker.AdventureWorks2017.Person;
using Slacker.Views.Controls.Wpf;

namespace Slacker.Views.ExampleWpf {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public DataService<Person> DataService { get; set; } = new PersonDataService(
            DataServiceConnectionManager.FromConfig("AdventureWorks2017")
        );

        public MainWindow() {
            InitializeComponent();
            DataGrid.DataService = DataService;
            DataGrid.PartialLoading = false;
            DataGrid.LoadRecordSet();
        }
    }
}

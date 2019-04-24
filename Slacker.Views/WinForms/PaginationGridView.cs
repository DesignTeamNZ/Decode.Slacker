using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Slacker.Views.Grid;

namespace Slacker.Views.WinForms {
    public partial class PaginationGridView : UserControl {
 
        public IGridPagination Pagination { get; protected set; }
        protected BindingSource GridBinding { get; set; } = new BindingSource();

        public PaginationGridView() {
            InitializeComponent();
        }

        public void LoadPagination(IGridPagination pagination) {
            if (this.Pagination != null) {
                this.Pagination.RecordSetLoaded -= Pagination_OnRecordSetLoaded;
            }
            this.Pagination = pagination;

            // Set Binding
            gridView.DataSource = GridBinding;

            // Load Pagination
            pagination.RecordSetLoaded += Pagination_OnRecordSetLoaded;
            pagination.Load();
        }

        private void Pagination_OnRecordSetLoaded(object sender, EventArgs e) {
            GridBinding.Clear();
            Pagination?.RecordSet?.ToList().ForEach(
                record => GridBinding.Add(record)
            );
        }

        private void GridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e) {
            e.Cancel = Pagination?.RaisePreModelEditedEvent(
                Pagination.RecordSet[e.RowIndex],
                gridView.Columns[e.ColumnIndex].DataPropertyName
            );
        }

        private void GridView_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            Pagination?.RaiseModelEditedEvent(
                Pagination.RecordSet[e.RowIndex],
                gridView.Columns[e.ColumnIndex].DataPropertyName
            );
        }

        private void GridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) {
            Pagination?.RaiseModelDeletedEvent(Pagination.RecordSet[e.RowIndex]);
        }

    }
}

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
    public partial class SimplePaginationBar : UserControl {

        public IGridPagination Pagination { get; protected set; }

        public SimplePaginationBar() {
            InitializeComponent();
        }

        public void SetPagination(IGridPagination pagination) {
            if (this.Pagination != null) {
                this.Pagination.RecordSetLoaded -= Pagination_OnRecordSetLoaded;
            }
            this.Pagination = pagination;

            Pagination.RecordSetLoaded += Pagination_OnRecordSetLoaded;
            Pagination_OnRecordSetLoaded(this, null);
        } 

        private void Pagination_OnRecordSetLoaded(object sender, EventArgs e) {
            labelPage.Text = $"Page {Pagination.CurrentPage} of {Pagination.PageCount}";
            numericUpDownPageSize.Value = (decimal)Pagination.PageSize;
        }

        private void ButtonPrev_Click(object sender, EventArgs e) {
            if (Pagination.CurrentPage <= 1) {
                return;
            }

            Pagination.SetPage(Pagination.CurrentPage - 1);
        }

        private void ButtonFirst_Click(object sender, EventArgs e) {
            if (Pagination.CurrentPage <= 1) {
                return;
            }

            Pagination.SetPage(1);
        }

        private void ButtonNext_Click(object sender, EventArgs e) {
            if (Pagination.CurrentPage >= Pagination.PageCount) {
                return;
            }

            Pagination.SetPage(Pagination.CurrentPage + 1);
        }

        private void ButtonLast_Click(object sender, EventArgs e) {
            if (Pagination.CurrentPage >= Pagination.PageCount) {
                return;
            }

            Pagination.SetPage(Pagination.PageCount);
        }

        private void ButtonRefresh_Click(object sender, EventArgs e) {
            Pagination.Load();
        }

        private void NumericUpDownPageSize_ValueChanged(object sender, EventArgs e) {
            Pagination.SetPageSize((int)numericUpDownPageSize.Value);
        }
    }
}

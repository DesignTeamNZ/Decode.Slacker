using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slacker.Views.Winforms.Controls {
    public partial class DateTimePickerDialog : Form {
        private DateTimePickerDialog() {
            InitializeComponent();
        }

        public DateTime Date {
            get => dateTimePickerControl1.Date;
            set => dateTimePickerControl1.Date = value;
        }

        private void ButtonOk_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        public static DateTimePickerDialog Show(DateTime dateTime, IWin32Window parent = null) {
            var dialog = new DateTimePickerDialog {
                Date = dateTime
            };

            dialog.ShowDialog(parent);
            return dialog;
        }

    }
}

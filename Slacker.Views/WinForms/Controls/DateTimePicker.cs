using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slacker.Views.Winforms.Controls {
    
    public partial class DateTimePicker : UserControl, IDataGridViewEditingControl {
        
        public DateTime Value {
            get => timePicker.Value;
            set => timePicker.Value = value;
        }
        
        public string CustomFormat {
            get => timePicker.CustomFormat;
            set => timePicker.CustomFormat = value;
        }

        public DataGridView EditingControlDataGridView { get; set; }

        public object EditingControlFormattedValue {
            get => Value;
            set => Value = (
                DateTime.TryParse(value.ToString(), out DateTime result) ? 
                    result : default(DateTime)
            );
        }

        public int EditingControlRowIndex { get; set; }
        public bool EditingControlValueChanged { get; set; }
        public Cursor EditingPanelCursor => Cursor.Current;
        public bool RepositionEditingControlOnValueChange { get; set; }

        public DateTimePicker() {
            InitializeComponent();
        }
        
        private void Button_Click(object sender, EventArgs e) {
            BeginEdit();
        }

        public void BeginEdit() {
            var dialog = DateTimePickerDialog.Show(Value, this);
            if (dialog.DialogResult != DialogResult.OK) {
                return;
            }

            this.Value = dialog.Date;
        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle) {
            //TODO

        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey) {
            return false;
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context) {
            return EditingControlFormattedValue;
        }

        public void PrepareEditingControlForEdit(bool selectAll) {

        }
    }



    public class DateTimePickerColumn : DataGridViewColumn {

        public DateTimePickerColumn() : base(new DateTimePickerCell()) { }

        public override DataGridViewCell CellTemplate {
            get {
                return base.CellTemplate;
            }
            set {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DateTimePickerCell))) {
                    throw new InvalidCastException("Must be a DateTimePickerCell");
                }
                base.CellTemplate = value;
            }
        }

    }

    public class DateTimePickerCell : DataGridViewTextBoxCell {

        public DateTimePickerCell() : base() {
        }

        public override Type EditType {
            get => typeof(DateTimePicker);
        }

        public override Type ValueType {
            get => typeof(DateTime);
        }

        public override object DefaultNewRowValue {
            get => DateTime.Now;
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle) {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            var control = DataGridView.EditingControl as DateTimePicker;
            if (this.Value == null) {
                control.Value = (DateTime) DefaultNewRowValue;
                return;
            }

            control.Value = (DateTime) Value;
        }

    }
}

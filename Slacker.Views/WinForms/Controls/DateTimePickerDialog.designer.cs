namespace Slacker.Views.Winforms.Controls {
    partial class DateTimePickerDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.dateTimePickerControl1 = new Slacker.Views.Winforms.Controls.DateTimePickerControl();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateTimePickerControl1
            // 
            this.dateTimePickerControl1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dateTimePickerControl1.Date = new System.DateTime(2018, 11, 5, 14, 15, 51, 762);
            this.dateTimePickerControl1.Location = new System.Drawing.Point(0, 0);
            this.dateTimePickerControl1.MaximumSize = new System.Drawing.Size(442, 283);
            this.dateTimePickerControl1.MinimumSize = new System.Drawing.Size(442, 283);
            this.dateTimePickerControl1.Name = "dateTimePickerControl1";
            this.dateTimePickerControl1.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(31)))), ((int)(((byte)(36)))));
            this.dateTimePickerControl1.Size = new System.Drawing.Size(442, 283);
            this.dateTimePickerControl1.TabIndex = 0;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(346, 286);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(96, 27);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Save";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(240, 286);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 27);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // DateTimePickerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 320);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.dateTimePickerControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(460, 320);
            this.Name = "DateTimePickerDialog";
            this.Text = "DateTimePickerDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private DateTimePickerControl dateTimePickerControl1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}
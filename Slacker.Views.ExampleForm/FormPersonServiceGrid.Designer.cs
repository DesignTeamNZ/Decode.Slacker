
namespace Slacker.Views.ExampleForm {
    partial class FormPersonServiceGrid {
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
            this.panelControls = new System.Windows.Forms.Panel();
            this.personServiceGrid1 = new Slacker.Views.ExampleForm.Forms.Controls.PersonServiceGrid();
            this.SuspendLayout();
            // 
            // panelControls
            // 
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControls.Location = new System.Drawing.Point(0, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(200, 450);
            this.panelControls.TabIndex = 1;
            // 
            // personServiceGrid1
            // 
            this.personServiceGrid1.CurrentPage = 1;
            this.personServiceGrid1.DataService = null;
            this.personServiceGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.personServiceGrid1.Location = new System.Drawing.Point(200, 0);
            this.personServiceGrid1.Name = "personServiceGrid1";
            this.personServiceGrid1.PageSize = 100;
            this.personServiceGrid1.PartialLoading = true;
            this.personServiceGrid1.QueryCondition = null;
            this.personServiceGrid1.QueryConditionParams = null;
            this.personServiceGrid1.Size = new System.Drawing.Size(654, 450);
            this.personServiceGrid1.TabIndex = 2;
            this.personServiceGrid1.TotalRecordCount = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 450);
            this.Controls.Add(this.personServiceGrid1);
            this.Controls.Add(this.panelControls);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.Panel panelControls;
        private Forms.Controls.PersonServiceGrid personServiceGrid1;
    }
}


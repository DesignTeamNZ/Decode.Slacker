using Slacker.Views.ExampleForm.Models;

namespace Slacker.Views.ExampleForm {
    partial class Form1 {
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
            this.customerServiceGrid1 = new Slacker.Views.ExampleForm.Forms.Controls.CustomerServiceGrid();
            this.panelControls = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // customerServiceGrid1
            // 
            this.customerServiceGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customerServiceGrid1.Location = new System.Drawing.Point(0, 0);
            this.customerServiceGrid1.Name = "customerServiceGrid1";
            this.customerServiceGrid1.Size = new System.Drawing.Size(854, 450);
            this.customerServiceGrid1.TabIndex = 0;
            // 
            // panelControls
            // 
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControls.Location = new System.Drawing.Point(0, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(200, 450);
            this.panelControls.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 450);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.customerServiceGrid1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Slacker.Views.Controls.ServiceGrid<CustomerOrder> customerGrid;
        private Forms.Controls.CustomerServiceGrid customerServiceGrid1;
        private System.Windows.Forms.Panel panelControls;
    }
}


namespace Slacker.Views.ExampleForm {
    partial class PaginationGridViewForm {
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
            this.paginationGridView1 = new Slacker.Views.WinForms.PaginationGridView();
            this.simplePaginationBar1 = new Slacker.Views.WinForms.SimplePaginationBar();
            this.SuspendLayout();
            // 
            // paginationGridView1
            // 
            this.paginationGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paginationGridView1.Location = new System.Drawing.Point(0, 35);
            this.paginationGridView1.Name = "paginationGridView1";
            this.paginationGridView1.Size = new System.Drawing.Size(800, 415);
            this.paginationGridView1.TabIndex = 0;
            // 
            // simplePaginationBar1
            // 
            this.simplePaginationBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.simplePaginationBar1.Location = new System.Drawing.Point(0, 0);
            this.simplePaginationBar1.Name = "simplePaginationBar1";
            this.simplePaginationBar1.Size = new System.Drawing.Size(800, 29);
            this.simplePaginationBar1.TabIndex = 1;
            // 
            // PaginationGridViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.simplePaginationBar1);
            this.Controls.Add(this.paginationGridView1);
            this.Name = "PaginationGridViewForm";
            this.Text = "PaginationGridViewForm";
            this.ResumeLayout(false);

        }

        #endregion

        private WinForms.PaginationGridView paginationGridView1;
        private WinForms.SimplePaginationBar simplePaginationBar1;
    }
}
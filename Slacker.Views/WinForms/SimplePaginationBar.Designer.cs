namespace Slacker.Views.WinForms {
    partial class SimplePaginationBar {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonFirst = new System.Windows.Forms.Button();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownPageSize = new System.Windows.Forms.NumericUpDown();
            this.labelPage = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonLast = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPageSize)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.buttonFirst);
            this.flowLayoutPanel2.Controls.Add(this.buttonPrev);
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.numericUpDownPageSize);
            this.flowLayoutPanel2.Controls.Add(this.labelPage);
            this.flowLayoutPanel2.Controls.Add(this.buttonNext);
            this.flowLayoutPanel2.Controls.Add(this.buttonLast);
            this.flowLayoutPanel2.Controls.Add(this.buttonRefresh);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(396, 29);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // buttonFirst
            // 
            this.buttonFirst.Location = new System.Drawing.Point(3, 3);
            this.buttonFirst.Name = "buttonFirst";
            this.buttonFirst.Size = new System.Drawing.Size(28, 23);
            this.buttonFirst.TabIndex = 1;
            this.buttonFirst.Text = "<<";
            this.buttonFirst.UseVisualStyleBackColor = true;
            this.buttonFirst.Click += new System.EventHandler(this.ButtonFirst_Click);
            // 
            // buttonPrev
            // 
            this.buttonPrev.Location = new System.Drawing.Point(37, 3);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(28, 23);
            this.buttonPrev.TabIndex = 2;
            this.buttonPrev.Text = "<";
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Click += new System.EventHandler(this.ButtonPrev_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.label2.Size = new System.Drawing.Size(42, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "Results";
            // 
            // numericUpDownPageSize
            // 
            this.numericUpDownPageSize.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownPageSize.Location = new System.Drawing.Point(119, 5);
            this.numericUpDownPageSize.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.numericUpDownPageSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownPageSize.Name = "numericUpDownPageSize";
            this.numericUpDownPageSize.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownPageSize.TabIndex = 6;
            this.numericUpDownPageSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownPageSize.ValueChanged += new System.EventHandler(this.NumericUpDownPageSize_ValueChanged);
            // 
            // labelPage
            // 
            this.labelPage.AutoSize = true;
            this.labelPage.Location = new System.Drawing.Point(193, 5);
            this.labelPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.labelPage.Name = "labelPage";
            this.labelPage.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.labelPage.Size = new System.Drawing.Size(62, 19);
            this.labelPage.TabIndex = 3;
            this.labelPage.Text = "Page 1 of 1";
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(261, 3);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(28, 23);
            this.buttonNext.TabIndex = 4;
            this.buttonNext.Text = ">";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.ButtonNext_Click);
            // 
            // buttonLast
            // 
            this.buttonLast.Location = new System.Drawing.Point(295, 3);
            this.buttonLast.Name = "buttonLast";
            this.buttonLast.Size = new System.Drawing.Size(28, 23);
            this.buttonLast.TabIndex = 5;
            this.buttonLast.Text = ">>";
            this.buttonLast.UseVisualStyleBackColor = true;
            this.buttonLast.Click += new System.EventHandler(this.ButtonLast_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(329, 3);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(62, 23);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // SimplePaginationBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel2);
            this.Name = "SimplePaginationBar";
            this.Size = new System.Drawing.Size(396, 29);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPageSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button buttonFirst;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownPageSize;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonLast;
        private System.Windows.Forms.Button buttonRefresh;
    }
}

namespace MIS_Shell
{
    partial class MIS_T5
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkedListBoxT5 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkedListBoxT8 = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.select_all = new System.Windows.Forms.CheckBox();
            this.T8CheckAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkedListBoxT5
            // 
            this.checkedListBoxT5.CheckOnClick = true;
            this.checkedListBoxT5.Location = new System.Drawing.Point(83, 59);
            this.checkedListBoxT5.Name = "checkedListBoxT5";
            this.checkedListBoxT5.Size = new System.Drawing.Size(315, 516);
            this.checkedListBoxT5.TabIndex = 23;
            this.checkedListBoxT5.ThreeDCheckBoxes = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "T5：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(404, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 25;
            this.label2.Text = "T8：";
            // 
            // checkedListBoxT8
            // 
            this.checkedListBoxT8.CheckOnClick = true;
            this.checkedListBoxT8.Location = new System.Drawing.Point(439, 59);
            this.checkedListBoxT8.Name = "checkedListBoxT8";
            this.checkedListBoxT8.Size = new System.Drawing.Size(352, 516);
            this.checkedListBoxT8.TabIndex = 26;
            this.checkedListBoxT8.ThreeDCheckBoxes = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(83, 587);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 27;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_Click);
            // 
            // select_all
            // 
            this.select_all.AutoSize = true;
            this.select_all.Location = new System.Drawing.Point(83, 27);
            this.select_all.Name = "select_all";
            this.select_all.Size = new System.Drawing.Size(60, 16);
            this.select_all.TabIndex = 28;
            this.select_all.Text = "T5全选";
            this.select_all.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.select_all.UseVisualStyleBackColor = true;
            this.select_all.CheckedChanged += new System.EventHandler(this.select_all_CheckedChanged);
            // 
            // T8CheckAll
            // 
            this.T8CheckAll.AutoSize = true;
            this.T8CheckAll.Location = new System.Drawing.Point(439, 27);
            this.T8CheckAll.Name = "T8CheckAll";
            this.T8CheckAll.Size = new System.Drawing.Size(60, 16);
            this.T8CheckAll.TabIndex = 29;
            this.T8CheckAll.Text = "T8全选";
            this.T8CheckAll.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.T8CheckAll.UseVisualStyleBackColor = true;
            this.T8CheckAll.CheckedChanged += new System.EventHandler(this.T8CheckAll_CheckedChanged);
            // 
            // MIS_T5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 622);
            this.Controls.Add(this.T8CheckAll);
            this.Controls.Add(this.select_all);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkedListBoxT8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBoxT5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MIS_T5";
            this.Text = "MIS_T5";
            this.Load += new System.EventHandler(this.MIS_T5_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxT5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox checkedListBoxT8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox select_all;
        private System.Windows.Forms.CheckBox T8CheckAll;
    }
}
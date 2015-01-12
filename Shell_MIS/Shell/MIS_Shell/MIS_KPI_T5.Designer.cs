namespace MIS_Shell
{
    partial class MIS_KPI_T5
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
            this.T8CheckAll = new System.Windows.Forms.CheckBox();
            this.select_all = new System.Windows.Forms.CheckBox();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.checkedListBoxT8 = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBoxT5 = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // T8CheckAll
            // 
            this.T8CheckAll.AutoSize = true;
            this.T8CheckAll.Font = new System.Drawing.Font("Arial", 10F);
            this.T8CheckAll.Location = new System.Drawing.Point(426, 14);
            this.T8CheckAll.Name = "T8CheckAll";
            this.T8CheckAll.Size = new System.Drawing.Size(72, 20);
            this.T8CheckAll.TabIndex = 36;
            this.T8CheckAll.Text = "T8全选";
            this.T8CheckAll.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.T8CheckAll.UseVisualStyleBackColor = true;
            this.T8CheckAll.CheckedChanged += new System.EventHandler(this.T8CheckAll_CheckedChanged);
            // 
            // select_all
            // 
            this.select_all.AutoSize = true;
            this.select_all.Font = new System.Drawing.Font("Arial", 10F);
            this.select_all.Location = new System.Drawing.Point(58, 14);
            this.select_all.Name = "select_all";
            this.select_all.Size = new System.Drawing.Size(72, 20);
            this.select_all.TabIndex = 35;
            this.select_all.Text = "T5全选";
            this.select_all.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.select_all.UseVisualStyleBackColor = true;
            this.select_all.CheckedChanged += new System.EventHandler(this.select_all_CheckedChanged);
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(58, 379);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(87, 25);
            this.btn_Ok.TabIndex = 34;
            this.btn_Ok.Text = "确定";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // checkedListBoxT8
            // 
            this.checkedListBoxT8.CheckOnClick = true;
            this.checkedListBoxT8.Font = new System.Drawing.Font("Arial", 10F);
            this.checkedListBoxT8.Location = new System.Drawing.Point(437, 41);
            this.checkedListBoxT8.Name = "checkedListBoxT8";
            this.checkedListBoxT8.Size = new System.Drawing.Size(247, 328);
            this.checkedListBoxT8.TabIndex = 33;
            this.checkedListBoxT8.ThreeDCheckBoxes = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F);
            this.label2.Location = new System.Drawing.Point(388, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 32;
            this.label2.Text = "T8：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F);
            this.label1.Location = new System.Drawing.Point(9, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 31;
            this.label1.Text = "T5：";
            // 
            // checkedListBoxT5
            // 
            this.checkedListBoxT5.CheckOnClick = true;
            this.checkedListBoxT5.Font = new System.Drawing.Font("Arial", 10F);
            this.checkedListBoxT5.Location = new System.Drawing.Point(58, 41);
            this.checkedListBoxT5.Name = "checkedListBoxT5";
            this.checkedListBoxT5.Size = new System.Drawing.Size(319, 328);
            this.checkedListBoxT5.TabIndex = 30;
            this.checkedListBoxT5.ThreeDCheckBoxes = true;
            // 
            // MIS_KPI_T5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 406);
            this.Controls.Add(this.T8CheckAll);
            this.Controls.Add(this.select_all);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.checkedListBoxT8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBoxT5);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MIS_KPI_T5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MIS_KPI_T5";
            this.Load += new System.EventHandler(this.MIS_KPI_T5_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox T8CheckAll;
        private System.Windows.Forms.CheckBox select_all;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.CheckedListBox checkedListBoxT8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListBoxT5;
    }
}
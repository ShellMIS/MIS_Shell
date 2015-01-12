namespace MIS_Shell
{
    partial class UpdateGroup2
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
            this.but_Submit = new System.Windows.Forms.Button();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Group2Name = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboGroup1Id = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboSta2U = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // but_Submit
            // 
            this.but_Submit.Font = new System.Drawing.Font("宋体", 10F);
            this.but_Submit.Location = new System.Drawing.Point(120, 169);
            this.but_Submit.Name = "but_Submit";
            this.but_Submit.Size = new System.Drawing.Size(75, 23);
            this.but_Submit.TabIndex = 21;
            this.but_Submit.Text = "提交";
            this.but_Submit.UseVisualStyleBackColor = true;
            this.but_Submit.Click += new System.EventHandler(this.but_Submit_Click);
            // 
            // txt_ID
            // 
            this.txt_ID.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_ID.Location = new System.Drawing.Point(121, 32);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.ReadOnly = true;
            this.txt_ID.Size = new System.Drawing.Size(173, 23);
            this.txt_ID.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F);
            this.label1.Location = new System.Drawing.Point(84, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "ID：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 10F);
            this.label11.Location = new System.Drawing.Point(57, 123);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 16);
            this.label11.TabIndex = 17;
            this.label11.Text = "Status：";
            // 
            // txt_Group2Name
            // 
            this.txt_Group2Name.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_Group2Name.Location = new System.Drawing.Point(120, 91);
            this.txt_Group2Name.Name = "txt_Group2Name";
            this.txt_Group2Name.Size = new System.Drawing.Size(173, 23);
            this.txt_Group2Name.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 10F);
            this.label6.Location = new System.Drawing.Point(13, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Group1Name：";
            // 
            // comboGroup1Id
            // 
            this.comboGroup1Id.Font = new System.Drawing.Font("Arial", 10F);
            this.comboGroup1Id.FormattingEnabled = true;
            this.comboGroup1Id.Location = new System.Drawing.Point(121, 61);
            this.comboGroup1Id.Name = "comboGroup1Id";
            this.comboGroup1Id.Size = new System.Drawing.Size(171, 24);
            this.comboGroup1Id.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F);
            this.label2.Location = new System.Drawing.Point(13, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "Group2Name：";
            // 
            // comboSta2U
            // 
            this.comboSta2U.Font = new System.Drawing.Font("Arial", 10F);
            this.comboSta2U.FormattingEnabled = true;
            this.comboSta2U.Items.AddRange(new object[] {
            "Active",
            "Block",
            "Closed"});
            this.comboSta2U.Location = new System.Drawing.Point(120, 120);
            this.comboSta2U.Name = "comboSta2U";
            this.comboSta2U.Size = new System.Drawing.Size(173, 24);
            this.comboSta2U.TabIndex = 24;
            // 
            // UpdateGroup2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 246);
            this.Controls.Add(this.comboSta2U);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboGroup1Id);
            this.Controls.Add(this.but_Submit);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txt_Group2Name);
            this.Controls.Add(this.label6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateGroup2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateGroup2";
            this.Load += new System.EventHandler(this.UpdateGroup2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button but_Submit;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_Group2Name;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboGroup1Id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboSta2U;
    }
}
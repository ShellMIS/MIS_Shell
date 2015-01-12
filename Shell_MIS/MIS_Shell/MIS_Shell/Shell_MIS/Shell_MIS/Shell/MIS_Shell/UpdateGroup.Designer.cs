namespace MIS_Shell
{
    partial class UpdateGroup
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
            this.txt_Gr1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.but_Submit = new System.Windows.Forms.Button();
            this.comboStatU = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txt_Gr1
            // 
            this.txt_Gr1.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_Gr1.Location = new System.Drawing.Point(125, 76);
            this.txt_Gr1.Name = "txt_Gr1";
            this.txt_Gr1.Size = new System.Drawing.Size(173, 23);
            this.txt_Gr1.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 10F);
            this.label6.Location = new System.Drawing.Point(16, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Group1Name：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 10F);
            this.label11.Location = new System.Drawing.Point(59, 119);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 16);
            this.label11.TabIndex = 12;
            this.label11.Text = "Status：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F);
            this.label1.Location = new System.Drawing.Point(85, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "ID：";
            // 
            // txt_ID
            // 
            this.txt_ID.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_ID.Location = new System.Drawing.Point(126, 35);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.ReadOnly = true;
            this.txt_ID.Size = new System.Drawing.Size(173, 23);
            this.txt_ID.TabIndex = 13;
            // 
            // but_Submit
            // 
            this.but_Submit.Location = new System.Drawing.Point(124, 165);
            this.but_Submit.Name = "but_Submit";
            this.but_Submit.Size = new System.Drawing.Size(75, 23);
            this.but_Submit.TabIndex = 14;
            this.but_Submit.Text = "提交";
            this.but_Submit.UseVisualStyleBackColor = true;
            this.but_Submit.Click += new System.EventHandler(this.but_Submit_Click);
            // 
            // comboStatU
            // 
            this.comboStatU.Font = new System.Drawing.Font("Arial", 10F);
            this.comboStatU.FormattingEnabled = true;
            this.comboStatU.Items.AddRange(new object[] {
            "Active",
            "Block",
            "Closed"});
            this.comboStatU.Location = new System.Drawing.Point(124, 116);
            this.comboStatU.Name = "comboStatU";
            this.comboStatU.Size = new System.Drawing.Size(174, 24);
            this.comboStatU.TabIndex = 15;
            // 
            // UpdateGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 235);
            this.Controls.Add(this.comboStatU);
            this.Controls.Add(this.but_Submit);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txt_Gr1);
            this.Controls.Add(this.label6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateGroup";
            this.Load += new System.EventHandler(this.UpdateGroup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Gr1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Button but_Submit;
        private System.Windows.Forms.ComboBox comboStatU;

    }
}
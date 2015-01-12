namespace MIS_Shell
{
    partial class UpdateT5Setting
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
            this.But_Update = new System.Windows.Forms.Button();
            this.txt_deptPinYin = new System.Windows.Forms.TextBox();
            this.txt_deptCH = new System.Windows.Forms.TextBox();
            this.txt_t5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // But_Update
            // 
            this.But_Update.Font = new System.Drawing.Font("宋体", 10F);
            this.But_Update.Location = new System.Drawing.Point(153, 151);
            this.But_Update.Name = "But_Update";
            this.But_Update.Size = new System.Drawing.Size(75, 23);
            this.But_Update.TabIndex = 13;
            this.But_Update.Text = "修改";
            this.But_Update.UseVisualStyleBackColor = true;
            this.But_Update.Click += new System.EventHandler(this.But_Update_Click);
            // 
            // txt_deptPinYin
            // 
            this.txt_deptPinYin.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_deptPinYin.Location = new System.Drawing.Point(153, 122);
            this.txt_deptPinYin.Name = "txt_deptPinYin";
            this.txt_deptPinYin.Size = new System.Drawing.Size(173, 23);
            this.txt_deptPinYin.TabIndex = 12;
            // 
            // txt_deptCH
            // 
            this.txt_deptCH.Location = new System.Drawing.Point(153, 94);
            this.txt_deptCH.Name = "txt_deptCH";
            this.txt_deptCH.Size = new System.Drawing.Size(173, 21);
            this.txt_deptCH.TabIndex = 11;
            // 
            // txt_t5
            // 
            this.txt_t5.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_t5.Location = new System.Drawing.Point(153, 65);
            this.txt_t5.Name = "txt_t5";
            this.txt_t5.Size = new System.Drawing.Size(173, 23);
            this.txt_t5.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10F);
            this.label5.Location = new System.Drawing.Point(23, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "DeptNamePinYin：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10F);
            this.label4.Location = new System.Drawing.Point(43, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "DeptNameCH：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10F);
            this.label3.Location = new System.Drawing.Point(76, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "SCL_T5：";
            // 
            // txt_ID
            // 
            this.txt_ID.Enabled = false;
            this.txt_ID.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_ID.Location = new System.Drawing.Point(153, 36);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.ReadOnly = true;
            this.txt_ID.Size = new System.Drawing.Size(173, 23);
            this.txt_ID.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F);
            this.label1.Location = new System.Drawing.Point(115, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "ID：";
            // 
            // UpdateT5Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 262);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.But_Update);
            this.Controls.Add(this.txt_deptPinYin);
            this.Controls.Add(this.txt_deptCH);
            this.Controls.Add(this.txt_t5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "UpdateT5Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateT5Setting";
            this.Load += new System.EventHandler(this.UpdateT5Setting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button But_Update;
        private System.Windows.Forms.TextBox txt_deptPinYin;
        private System.Windows.Forms.TextBox txt_deptCH;
        private System.Windows.Forms.TextBox txt_t5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Label label1;
    }
}
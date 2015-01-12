namespace MIS_Shell
{
    partial class UpdateJVT5
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
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.But_Update = new System.Windows.Forms.Button();
            this.txt_jvt5 = new System.Windows.Forms.TextBox();
            this.txt_t5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_CoCd = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txt_ID
            // 
            this.txt_ID.Enabled = false;
            this.txt_ID.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_ID.Location = new System.Drawing.Point(86, 22);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.ReadOnly = true;
            this.txt_ID.Size = new System.Drawing.Size(513, 23);
            this.txt_ID.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F);
            this.label1.Location = new System.Drawing.Point(51, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "ID：";
            // 
            // But_Update
            // 
            this.But_Update.Font = new System.Drawing.Font("宋体", 10F);
            this.But_Update.Location = new System.Drawing.Point(86, 170);
            this.But_Update.Name = "But_Update";
            this.But_Update.Size = new System.Drawing.Size(87, 25);
            this.But_Update.TabIndex = 22;
            this.But_Update.Text = "修改";
            this.But_Update.UseVisualStyleBackColor = true;
            this.But_Update.Click += new System.EventHandler(this.But_Update_Click);
            // 
            // txt_jvt5
            // 
            this.txt_jvt5.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_jvt5.Location = new System.Drawing.Point(86, 88);
            this.txt_jvt5.Name = "txt_jvt5";
            this.txt_jvt5.Size = new System.Drawing.Size(513, 23);
            this.txt_jvt5.TabIndex = 20;
            // 
            // txt_t5
            // 
            this.txt_t5.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_t5.Location = new System.Drawing.Point(86, 54);
            this.txt_t5.Name = "txt_t5";
            this.txt_t5.Size = new System.Drawing.Size(513, 23);
            this.txt_t5.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10F);
            this.label5.Location = new System.Drawing.Point(26, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "CoCd：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10F);
            this.label4.Location = new System.Drawing.Point(21, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "JV_T5：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10F);
            this.label3.Location = new System.Drawing.Point(8, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "SCL_T5：";
            // 
            // cb_CoCd
            // 
            this.cb_CoCd.Font = new System.Drawing.Font("Arial", 10F);
            this.cb_CoCd.FormattingEnabled = true;
            this.cb_CoCd.Location = new System.Drawing.Point(86, 123);
            this.cb_CoCd.Name = "cb_CoCd";
            this.cb_CoCd.Size = new System.Drawing.Size(513, 24);
            this.cb_CoCd.TabIndex = 37;
            // 
            // UpdateJVT5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 213);
            this.Controls.Add(this.cb_CoCd);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.But_Update);
            this.Controls.Add(this.txt_jvt5);
            this.Controls.Add(this.txt_t5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Name = "UpdateJVT5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateJVT5";
            this.Load += new System.EventHandler(this.UpdateJVT5_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button But_Update;
        private System.Windows.Forms.TextBox txt_jvt5;
        private System.Windows.Forms.TextBox txt_t5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_CoCd;
    }
}
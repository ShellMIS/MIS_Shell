namespace MIS_Shell
{
    partial class UpdateJV_COA
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
            this.button1 = new System.Windows.Forms.Button();
            this.txt_AccountD = new System.Windows.Forms.TextBox();
            this.txt_Account = new System.Windows.Forms.TextBox();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_AccountCode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_CoCd = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 10F);
            this.button1.Location = new System.Drawing.Point(187, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 29;
            this.button1.Text = "提交";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_AccountD
            // 
            this.txt_AccountD.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_AccountD.Location = new System.Drawing.Point(187, 82);
            this.txt_AccountD.Name = "txt_AccountD";
            this.txt_AccountD.Size = new System.Drawing.Size(231, 23);
            this.txt_AccountD.TabIndex = 28;
            // 
            // txt_Account
            // 
            this.txt_Account.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_Account.Location = new System.Drawing.Point(187, 53);
            this.txt_Account.Name = "txt_Account";
            this.txt_Account.Size = new System.Drawing.Size(231, 23);
            this.txt_Account.TabIndex = 25;
            // 
            // txt_ID
            // 
            this.txt_ID.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_ID.Location = new System.Drawing.Point(187, 24);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.ReadOnly = true;
            this.txt_ID.Size = new System.Drawing.Size(231, 23);
            this.txt_ID.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10F);
            this.label5.Location = new System.Drawing.Point(12, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 16);
            this.label5.TabIndex = 22;
            this.label5.Text = "JV_Account_Description：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F);
            this.label1.Location = new System.Drawing.Point(152, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "ID：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10F);
            this.label4.Location = new System.Drawing.Point(57, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 16);
            this.label4.TabIndex = 19;
            this.label4.Text = "JV_AccountCode：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 10F);
            this.label6.Location = new System.Drawing.Point(43, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(142, 16);
            this.label6.TabIndex = 30;
            this.label6.Text = "SCL_AccountCode：";
            // 
            // cb_AccountCode
            // 
            this.cb_AccountCode.Font = new System.Drawing.Font("Arial", 10F);
            this.cb_AccountCode.FormattingEnabled = true;
            this.cb_AccountCode.Location = new System.Drawing.Point(187, 111);
            this.cb_AccountCode.Name = "cb_AccountCode";
            this.cb_AccountCode.Size = new System.Drawing.Size(231, 24);
            this.cb_AccountCode.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F);
            this.label2.Location = new System.Drawing.Point(127, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 30;
            this.label2.Text = "CoCd：";
            // 
            // cb_CoCd
            // 
            this.cb_CoCd.Font = new System.Drawing.Font("Arial", 10F);
            this.cb_CoCd.FormattingEnabled = true;
            this.cb_CoCd.Location = new System.Drawing.Point(187, 141);
            this.cb_CoCd.Name = "cb_CoCd";
            this.cb_CoCd.Size = new System.Drawing.Size(231, 24);
            this.cb_CoCd.TabIndex = 32;
            // 
            // UpdateJV_COA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 250);
            this.Controls.Add(this.cb_CoCd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_AccountCode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_AccountD);
            this.Controls.Add(this.txt_Account);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateJV_COA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateJV_COA";
            this.Load += new System.EventHandler(this.UpdateJV_COA_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_AccountD;
        private System.Windows.Forms.TextBox txt_Account;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_AccountCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_CoCd;
    }
}
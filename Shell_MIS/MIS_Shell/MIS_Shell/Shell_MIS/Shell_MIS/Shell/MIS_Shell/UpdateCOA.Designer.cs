namespace MIS_Shell
{
    partial class UpdateCOA
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
            this.txt_AccountD = new System.Windows.Forms.TextBox();
            this.txt_Account = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_AT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboStatus = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txt_AccountD
            // 
            this.txt_AccountD.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_AccountD.Location = new System.Drawing.Point(164, 157);
            this.txt_AccountD.Name = "txt_AccountD";
            this.txt_AccountD.Size = new System.Drawing.Size(173, 23);
            this.txt_AccountD.TabIndex = 15;
            // 
            // txt_Account
            // 
            this.txt_Account.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_Account.Location = new System.Drawing.Point(165, 120);
            this.txt_Account.Name = "txt_Account";
            this.txt_Account.Size = new System.Drawing.Size(173, 23);
            this.txt_Account.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 10F);
            this.label6.Location = new System.Drawing.Point(96, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "Status：";
            // 
            // txt_AT
            // 
            this.txt_AT.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_AT.Location = new System.Drawing.Point(164, 86);
            this.txt_AT.Name = "txt_AT";
            this.txt_AT.Size = new System.Drawing.Size(173, 23);
            this.txt_AT.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10F);
            this.label5.Location = new System.Drawing.Point(10, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Account_Description：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10F);
            this.label4.Location = new System.Drawing.Point(89, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Account：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10F);
            this.label3.Location = new System.Drawing.Point(58, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "AccountType：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F);
            this.label1.Location = new System.Drawing.Point(127, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "ID：";
            // 
            // txt_ID
            // 
            this.txt_ID.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_ID.Location = new System.Drawing.Point(165, 50);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.ReadOnly = true;
            this.txt_ID.Size = new System.Drawing.Size(173, 23);
            this.txt_ID.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 10F);
            this.button1.Location = new System.Drawing.Point(165, 233);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "提交";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboStatus
            // 
            this.comboStatus.Font = new System.Drawing.Font("Arial", 10F);
            this.comboStatus.FormattingEnabled = true;
            this.comboStatus.Items.AddRange(new object[] {
            "Active",
            "InActive",
            "Closed"});
            this.comboStatus.Location = new System.Drawing.Point(163, 191);
            this.comboStatus.Name = "comboStatus";
            this.comboStatus.Size = new System.Drawing.Size(173, 24);
            this.comboStatus.TabIndex = 17;
            // 
            // UpdateCOA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 314);
            this.Controls.Add(this.comboStatus);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_AccountD);
            this.Controls.Add(this.txt_Account);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.txt_AT);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateCOA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateCOA";
            this.Load += new System.EventHandler(this.UpdateCOA_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_AccountD;
        private System.Windows.Forms.TextBox txt_Account;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_AT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboStatus;
    }
}
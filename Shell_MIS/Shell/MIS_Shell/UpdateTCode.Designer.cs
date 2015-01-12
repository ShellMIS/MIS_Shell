namespace MIS_Shell
{
    partial class UpdateTCode
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
            this.txt_TN = new System.Windows.Forms.TextBox();
            this.txt_Tcode = new System.Windows.Forms.TextBox();
            this.txt_TT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cb_CoCd = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_TN
            // 
            this.txt_TN.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_TN.Location = new System.Drawing.Point(174, 118);
            this.txt_TN.Name = "txt_TN";
            this.txt_TN.Size = new System.Drawing.Size(254, 23);
            this.txt_TN.TabIndex = 11;
            // 
            // txt_Tcode
            // 
            this.txt_Tcode.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_Tcode.Location = new System.Drawing.Point(174, 89);
            this.txt_Tcode.Name = "txt_Tcode";
            this.txt_Tcode.Size = new System.Drawing.Size(254, 23);
            this.txt_Tcode.TabIndex = 10;
            // 
            // txt_TT
            // 
            this.txt_TT.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_TT.Location = new System.Drawing.Point(174, 60);
            this.txt_TT.Name = "txt_TT";
            this.txt_TT.Size = new System.Drawing.Size(254, 23);
            this.txt_TT.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10F);
            this.label5.Location = new System.Drawing.Point(72, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "TcodeName：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10F);
            this.label4.Location = new System.Drawing.Point(108, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tcode：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10F);
            this.label3.Location = new System.Drawing.Point(77, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "TcodeType：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F);
            this.label1.Location = new System.Drawing.Point(133, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "ID：";
            // 
            // txt_ID
            // 
            this.txt_ID.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_ID.Location = new System.Drawing.Point(174, 31);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.ReadOnly = true;
            this.txt_ID.Size = new System.Drawing.Size(254, 23);
            this.txt_ID.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 10F);
            this.button1.Location = new System.Drawing.Point(174, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "提交";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cb_CoCd
            // 
            this.cb_CoCd.Font = new System.Drawing.Font("宋体", 10F);
            this.cb_CoCd.FormattingEnabled = true;
            this.cb_CoCd.Location = new System.Drawing.Point(174, 147);
            this.cb_CoCd.Name = "cb_CoCd";
            this.cb_CoCd.Size = new System.Drawing.Size(254, 21);
            this.cb_CoCd.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F);
            this.label2.Location = new System.Drawing.Point(108, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "CoCd：";
            // 
            // UpdateTCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 249);
            this.Controls.Add(this.cb_CoCd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_TN);
            this.Controls.Add(this.txt_Tcode);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.txt_TT);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateTCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateTCode";
            this.Load += new System.EventHandler(this.UpdateTCode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_TN;
        private System.Windows.Forms.TextBox txt_Tcode;
        private System.Windows.Forms.TextBox txt_TT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cb_CoCd;
        private System.Windows.Forms.Label label2;
    }
}
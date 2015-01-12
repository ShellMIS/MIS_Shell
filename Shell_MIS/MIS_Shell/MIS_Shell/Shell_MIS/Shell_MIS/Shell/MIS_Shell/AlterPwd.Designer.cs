namespace MIS_Shell
{
    partial class AlterPwd
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbnSave = new System.Windows.Forms.Button();
            this.txtconfirmPwd = new System.Windows.Forms.TextBox();
            this.txtpwd = new System.Windows.Forms.TextBox();
            this.txtoldPwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbUserName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbnSave);
            this.groupBox1.Controls.Add(this.txtconfirmPwd);
            this.groupBox1.Controls.Add(this.txtpwd);
            this.groupBox1.Controls.Add(this.txtoldPwd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lbUserName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(121, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 210);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "密码修改";
            // 
            // tbnSave
            // 
            this.tbnSave.Font = new System.Drawing.Font("宋体", 10F);
            this.tbnSave.Location = new System.Drawing.Point(132, 157);
            this.tbnSave.Name = "tbnSave";
            this.tbnSave.Size = new System.Drawing.Size(75, 23);
            this.tbnSave.TabIndex = 17;
            this.tbnSave.Text = "提交";
            this.tbnSave.UseVisualStyleBackColor = true;
            this.tbnSave.Click += new System.EventHandler(this.tbnSave_Click);
            // 
            // txtconfirmPwd
            // 
            this.txtconfirmPwd.Font = new System.Drawing.Font("宋体", 10F);
            this.txtconfirmPwd.Location = new System.Drawing.Point(131, 120);
            this.txtconfirmPwd.Name = "txtconfirmPwd";
            this.txtconfirmPwd.Size = new System.Drawing.Size(146, 23);
            this.txtconfirmPwd.TabIndex = 16;
            // 
            // txtpwd
            // 
            this.txtpwd.Font = new System.Drawing.Font("宋体", 10F);
            this.txtpwd.Location = new System.Drawing.Point(132, 86);
            this.txtpwd.Name = "txtpwd";
            this.txtpwd.Size = new System.Drawing.Size(146, 23);
            this.txtpwd.TabIndex = 15;
            // 
            // txtoldPwd
            // 
            this.txtoldPwd.Font = new System.Drawing.Font("宋体", 10F);
            this.txtoldPwd.Location = new System.Drawing.Point(131, 54);
            this.txtoldPwd.Name = "txtoldPwd";
            this.txtoldPwd.Size = new System.Drawing.Size(146, 23);
            this.txtoldPwd.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F);
            this.label4.Location = new System.Drawing.Point(63, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 13;
            this.label4.Text = "确认密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F);
            this.label3.Location = new System.Drawing.Point(76, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 12;
            this.label3.Text = "新密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(75, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 11;
            this.label2.Text = "旧密码：";
            // 
            // lbUserName
            // 
            this.lbUserName.AutoSize = true;
            this.lbUserName.Font = new System.Drawing.Font("宋体", 10F);
            this.lbUserName.Location = new System.Drawing.Point(130, 30);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(0, 14);
            this.lbUserName.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(75, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "用户名：";
            // 
            // AlterPwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 363);
            this.Controls.Add(this.groupBox1);
            this.Name = "AlterPwd";
            this.Text = "个人密码修改";
            this.Load += new System.EventHandler(this.AlterPwd_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button tbnSave;
        private System.Windows.Forms.TextBox txtconfirmPwd;
        private System.Windows.Forms.TextBox txtpwd;
        private System.Windows.Forms.TextBox txtoldPwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.Label label1;

    }
}
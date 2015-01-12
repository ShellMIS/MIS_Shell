namespace MIS_Shell
{
    partial class DepartmentAlter
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
            this.lbDepartmentName = new System.Windows.Forms.Label();
            this.txtDepartName = new System.Windows.Forms.TextBox();
            this.lbenglish = new System.Windows.Forms.Label();
            this.txtEnglish = new System.Windows.Forms.TextBox();
            this.btnAlter = new System.Windows.Forms.Button();
            this.lbmsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbDepartmentName
            // 
            this.lbDepartmentName.AutoSize = true;
            this.lbDepartmentName.Font = new System.Drawing.Font("宋体", 10F);
            this.lbDepartmentName.Location = new System.Drawing.Point(40, 65);
            this.lbDepartmentName.Name = "lbDepartmentName";
            this.lbDepartmentName.Size = new System.Drawing.Size(77, 14);
            this.lbDepartmentName.TabIndex = 2;
            this.lbDepartmentName.Text = "部门名称：";
            // 
            // txtDepartName
            // 
            this.txtDepartName.Font = new System.Drawing.Font("宋体", 10F);
            this.txtDepartName.Location = new System.Drawing.Point(119, 59);
            this.txtDepartName.Name = "txtDepartName";
            this.txtDepartName.Size = new System.Drawing.Size(202, 23);
            this.txtDepartName.TabIndex = 3;
            // 
            // lbenglish
            // 
            this.lbenglish.AutoSize = true;
            this.lbenglish.Font = new System.Drawing.Font("宋体", 10F);
            this.lbenglish.Location = new System.Drawing.Point(13, 98);
            this.lbenglish.Name = "lbenglish";
            this.lbenglish.Size = new System.Drawing.Size(105, 14);
            this.lbenglish.TabIndex = 4;
            this.lbenglish.Text = "部门英文缩写：";
            // 
            // txtEnglish
            // 
            this.txtEnglish.Font = new System.Drawing.Font("宋体", 10F);
            this.txtEnglish.Location = new System.Drawing.Point(119, 95);
            this.txtEnglish.Name = "txtEnglish";
            this.txtEnglish.Size = new System.Drawing.Size(202, 23);
            this.txtEnglish.TabIndex = 5;
            // 
            // btnAlter
            // 
            this.btnAlter.Font = new System.Drawing.Font("宋体", 10F);
            this.btnAlter.Location = new System.Drawing.Point(119, 136);
            this.btnAlter.Name = "btnAlter";
            this.btnAlter.Size = new System.Drawing.Size(75, 23);
            this.btnAlter.TabIndex = 6;
            this.btnAlter.Text = "修改";
            this.btnAlter.UseVisualStyleBackColor = true;
            this.btnAlter.Click += new System.EventHandler(this.btnAlter_Click);
            // 
            // lbmsg
            // 
            this.lbmsg.AutoSize = true;
            this.lbmsg.Location = new System.Drawing.Point(179, 147);
            this.lbmsg.Name = "lbmsg";
            this.lbmsg.Size = new System.Drawing.Size(0, 12);
            this.lbmsg.TabIndex = 7;
            // 
            // DepartmentAlter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 209);
            this.Controls.Add(this.lbmsg);
            this.Controls.Add(this.btnAlter);
            this.Controls.Add(this.txtEnglish);
            this.Controls.Add(this.lbenglish);
            this.Controls.Add(this.txtDepartName);
            this.Controls.Add(this.lbDepartmentName);
            this.Name = "DepartmentAlter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改部门信息";
            this.Load += new System.EventHandler(this.DepartmentAlter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbDepartmentName;
        private System.Windows.Forms.TextBox txtDepartName;
        private System.Windows.Forms.Label lbenglish;
        private System.Windows.Forms.TextBox txtEnglish;
        private System.Windows.Forms.Button btnAlter;
        private System.Windows.Forms.Label lbmsg;
    }
}
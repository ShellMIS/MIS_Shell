namespace MIS_Shell
{
    partial class UpdateSiteCategory
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
            this.btn_Up = new System.Windows.Forms.Button();
            this.textSites1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textSiteId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textSiteName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textSite2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_Up
            // 
            this.btn_Up.Font = new System.Drawing.Font("宋体", 10F);
            this.btn_Up.Location = new System.Drawing.Point(208, 166);
            this.btn_Up.Name = "btn_Up";
            this.btn_Up.Size = new System.Drawing.Size(75, 23);
            this.btn_Up.TabIndex = 9;
            this.btn_Up.Text = "提交";
            this.btn_Up.UseVisualStyleBackColor = true;
            this.btn_Up.Click += new System.EventHandler(this.btn_Up_Click);
            // 
            // textSites1
            // 
            this.textSites1.Font = new System.Drawing.Font("宋体", 10F);
            this.textSites1.Location = new System.Drawing.Point(208, 70);
            this.textSites1.Name = "textSites1";
            this.textSites1.Size = new System.Drawing.Size(173, 23);
            this.textSites1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F);
            this.label2.Location = new System.Drawing.Point(90, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "SiteCategory1：";
            // 
            // textSiteId
            // 
            this.textSiteId.BackColor = System.Drawing.SystemColors.Control;
            this.textSiteId.Font = new System.Drawing.Font("宋体", 10F);
            this.textSiteId.Location = new System.Drawing.Point(208, 41);
            this.textSiteId.Name = "textSiteId";
            this.textSiteId.Size = new System.Drawing.Size(173, 23);
            this.textSiteId.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F);
            this.label1.Location = new System.Drawing.Point(85, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "SiteCategoryID：";
            // 
            // textSiteName
            // 
            this.textSiteName.Font = new System.Drawing.Font("宋体", 10F);
            this.textSiteName.Location = new System.Drawing.Point(208, 128);
            this.textSiteName.Name = "textSiteName";
            this.textSiteName.Size = new System.Drawing.Size(173, 23);
            this.textSiteName.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10F);
            this.label3.Location = new System.Drawing.Point(85, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "CategoryName：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10F);
            this.label4.Location = new System.Drawing.Point(89, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "SiteCategory2：";
            // 
            // textSite2
            // 
            this.textSite2.Font = new System.Drawing.Font("宋体", 10F);
            this.textSite2.Location = new System.Drawing.Point(208, 99);
            this.textSite2.Name = "textSite2";
            this.textSite2.Size = new System.Drawing.Size(173, 23);
            this.textSite2.TabIndex = 14;
            // 
            // UpdateSiteCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 229);
            this.Controls.Add(this.textSite2);
            this.Controls.Add(this.textSiteName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Up);
            this.Controls.Add(this.textSites1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textSiteId);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateSiteCategory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateSiteCategory";
            this.Load += new System.EventHandler(this.UpdateSiteCategory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Up;
        private System.Windows.Forms.TextBox textSites1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textSiteId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textSiteName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textSite2;
    }
}
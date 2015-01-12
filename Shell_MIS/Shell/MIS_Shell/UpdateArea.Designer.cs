namespace MIS_Shell
{
    partial class UpdateArea
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
            this.txt_ANEN = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_ACT0 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cCocd = new System.Windows.Forms.ComboBox();
            this.txt_ANCH = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_ANEN
            // 
            this.txt_ANEN.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_ANEN.Location = new System.Drawing.Point(120, 143);
            this.txt_ANEN.Name = "txt_ANEN";
            this.txt_ANEN.Size = new System.Drawing.Size(173, 23);
            this.txt_ANEN.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 10F);
            this.label6.Location = new System.Drawing.Point(57, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "CoCd：";
            // 
            // txt_ACT0
            // 
            this.txt_ACT0.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_ACT0.Location = new System.Drawing.Point(120, 80);
            this.txt_ACT0.Name = "txt_ACT0";
            this.txt_ACT0.Size = new System.Drawing.Size(173, 23);
            this.txt_ACT0.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10F);
            this.label5.Location = new System.Drawing.Point(10, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "AreaNameEN：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10F);
            this.label4.Location = new System.Drawing.Point(10, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "AreaNameCH：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10F);
            this.label3.Location = new System.Drawing.Point(14, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "AreaCodeT0：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F);
            this.label1.Location = new System.Drawing.Point(80, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "ID：";
            // 
            // txt_ID
            // 
            this.txt_ID.Font = new System.Drawing.Font("Arial", 10F);
            this.txt_ID.Location = new System.Drawing.Point(120, 44);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.ReadOnly = true;
            this.txt_ID.Size = new System.Drawing.Size(173, 23);
            this.txt_ID.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 10F);
            this.button1.Location = new System.Drawing.Point(120, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "提交";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cCocd
            // 
            this.cCocd.Font = new System.Drawing.Font("Arial", 10F);
            this.cCocd.FormattingEnabled = true;
            this.cCocd.Location = new System.Drawing.Point(120, 173);
            this.cCocd.Name = "cCocd";
            this.cCocd.Size = new System.Drawing.Size(173, 24);
            this.cCocd.TabIndex = 21;
            // 
            // txt_ANCH
            // 
            this.txt_ANCH.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_ANCH.Location = new System.Drawing.Point(120, 113);
            this.txt_ANCH.Name = "txt_ANCH";
            this.txt_ANCH.Size = new System.Drawing.Size(176, 23);
            this.txt_ANCH.TabIndex = 13;
            // 
            // UpdateArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 301);
            this.Controls.Add(this.cCocd);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_ANEN);
            this.Controls.Add(this.txt_ANCH);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.txt_ACT0);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateArea";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateArea";
            this.Load += new System.EventHandler(this.UpdateArea_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_ANEN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_ACT0;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cCocd;
        private System.Windows.Forms.TextBox txt_ANCH;
    }
}
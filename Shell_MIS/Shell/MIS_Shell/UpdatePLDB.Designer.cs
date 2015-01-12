namespace MIS_Shell
{
    partial class UpdatePLDB
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
            this.label1 = new System.Windows.Forms.Label();
            this.textId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textItem = new System.Windows.Forms.TextBox();
            this.btn_Up = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F);
            this.label1.Location = new System.Drawing.Point(56, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID：";
            // 
            // textId
            // 
            this.textId.BackColor = System.Drawing.SystemColors.Control;
            this.textId.Font = new System.Drawing.Font("Arial", 10F);
            this.textId.Location = new System.Drawing.Point(92, 35);
            this.textId.Name = "textId";
            this.textId.Size = new System.Drawing.Size(173, 23);
            this.textId.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F);
            this.label2.Location = new System.Drawing.Point(45, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Item：";
            // 
            // textItem
            // 
            this.textItem.Font = new System.Drawing.Font("Arial", 10F);
            this.textItem.Location = new System.Drawing.Point(93, 72);
            this.textItem.Name = "textItem";
            this.textItem.Size = new System.Drawing.Size(173, 23);
            this.textItem.TabIndex = 3;
            // 
            // btn_Up
            // 
            this.btn_Up.Font = new System.Drawing.Font("宋体", 10F);
            this.btn_Up.Location = new System.Drawing.Point(93, 101);
            this.btn_Up.Name = "btn_Up";
            this.btn_Up.Size = new System.Drawing.Size(75, 23);
            this.btn_Up.TabIndex = 4;
            this.btn_Up.Text = "提交";
            this.btn_Up.UseVisualStyleBackColor = true;
            this.btn_Up.Click += new System.EventHandler(this.btn_Up_Click);
            // 
            // UpdatePLDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 190);
            this.Controls.Add(this.btn_Up);
            this.Controls.Add(this.textItem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textId);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdatePLDB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdatePLDB";
            this.Load += new System.EventHandler(this.UpdatePLDB_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textItem;
        private System.Windows.Forms.Button btn_Up;
    }
}
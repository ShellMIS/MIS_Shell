namespace MIS_Shell
{
    partial class MIS_AllocationRights
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
            this.comboBoxRole = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBoxHave = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.checkBox_SelecAll = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnok = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxRole
            // 
            this.comboBoxRole.Font = new System.Drawing.Font("Arial", 10F);
            this.comboBoxRole.FormattingEnabled = true;
            this.comboBoxRole.Location = new System.Drawing.Point(103, 38);
            this.comboBoxRole.Name = "comboBoxRole";
            this.comboBoxRole.Size = new System.Drawing.Size(150, 24);
            this.comboBoxRole.TabIndex = 0;
            this.comboBoxRole.SelectedValueChanged += new System.EventHandler(this.comboBoxRole_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F);
            this.label1.Location = new System.Drawing.Point(21, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "角色名称：";
            // 
            // checkedListBoxHave
            // 
            this.checkedListBoxHave.CheckOnClick = true;
            this.checkedListBoxHave.Font = new System.Drawing.Font("Arial", 10F);
            this.checkedListBoxHave.FormattingEnabled = true;
            this.checkedListBoxHave.Location = new System.Drawing.Point(103, 100);
            this.checkedListBoxHave.Name = "checkedListBoxHave";
            this.checkedListBoxHave.Size = new System.Drawing.Size(629, 472);
            this.checkedListBoxHave.TabIndex = 2;
            this.checkedListBoxHave.ThreeDCheckBoxes = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F);
            this.label2.Location = new System.Drawing.Point(7, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "已拥有模块：";
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(778, 562);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(87, 25);
            this.btn_ok.TabIndex = 4;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // checkBox_SelecAll
            // 
            this.checkBox_SelecAll.AutoSize = true;
            this.checkBox_SelecAll.Font = new System.Drawing.Font("Arial", 10F);
            this.checkBox_SelecAll.Location = new System.Drawing.Point(103, 76);
            this.checkBox_SelecAll.Name = "checkBox_SelecAll";
            this.checkBox_SelecAll.Size = new System.Drawing.Size(55, 20);
            this.checkBox_SelecAll.TabIndex = 5;
            this.checkBox_SelecAll.Text = "全选";
            this.checkBox_SelecAll.UseVisualStyleBackColor = true;
            this.checkBox_SelecAll.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnok);
            this.groupBox1.Controls.Add(this.checkedListBoxHave);
            this.groupBox1.Controls.Add(this.checkBox_SelecAll);
            this.groupBox1.Controls.Add(this.comboBoxRole);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1065, 635);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "权限管理";
            // 
            // btnok
            // 
            this.btnok.Font = new System.Drawing.Font("宋体", 10F);
            this.btnok.Location = new System.Drawing.Point(103, 579);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(75, 23);
            this.btnok.TabIndex = 6;
            this.btnok.Text = "保存";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // MIS_AllocationRights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 635);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_ok);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MIS_AllocationRights";
            this.Text = "角色权限分配";
            this.Load += new System.EventHandler(this.MIS_AllocationRights_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxRole;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListBoxHave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.CheckBox checkBox_SelecAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnok;
    }
}
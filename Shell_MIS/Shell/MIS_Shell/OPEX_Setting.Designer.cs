namespace MIS_Shell
{
    partial class OPEX_Setting
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_add = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboPltype = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textAccountDes = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textAccountCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBuowner = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textOpline = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_select = new System.Windows.Forms.Button();
            this.comboPLTy = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textAccDesc = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textAcCode = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBOwner = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textPline = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btn_down = new System.Windows.Forms.Button();
            this.btn_export = new System.Windows.Forms.Button();
            this.btn_import = new System.Windows.Forms.Button();
            this.btn_open = new System.Windows.Forms.Button();
            this.textPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 10F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(940, 185);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage1.Controls.Add(this.btn_add);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.comboPltype);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.textAccountDes);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.textAccountCode);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.textBuowner);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.textOpline);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(932, 158);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "增加";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(159, 101);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 23;
            this.btn_add.Text = "提交";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F);
            this.label6.Location = new System.Drawing.Point(6, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 14);
            this.label6.TabIndex = 7;
            this.label6.Text = "对应数据库表内容预览";
            // 
            // comboPltype
            // 
            this.comboPltype.FormattingEnabled = true;
            this.comboPltype.Location = new System.Drawing.Point(457, 46);
            this.comboPltype.Name = "comboPltype";
            this.comboPltype.Size = new System.Drawing.Size(173, 21);
            this.comboPltype.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 10F);
            this.label10.Location = new System.Drawing.Point(382, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 16);
            this.label10.TabIndex = 21;
            this.label10.Text = "PLType：";
            // 
            // textAccountDes
            // 
            this.textAccountDes.Location = new System.Drawing.Point(159, 43);
            this.textAccountDes.Name = "textAccountDes";
            this.textAccountDes.Size = new System.Drawing.Size(173, 23);
            this.textAccountDes.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10F);
            this.label5.Location = new System.Drawing.Point(6, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 16);
            this.label5.TabIndex = 19;
            this.label5.Text = "Account_Description：";
            // 
            // textAccountCode
            // 
            this.textAccountCode.Location = new System.Drawing.Point(457, 17);
            this.textAccountCode.Name = "textAccountCode";
            this.textAccountCode.Size = new System.Drawing.Size(173, 23);
            this.textAccountCode.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10F);
            this.label4.Location = new System.Drawing.Point(348, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "AccountCode：";
            // 
            // textBuowner
            // 
            this.textBuowner.Location = new System.Drawing.Point(159, 72);
            this.textBuowner.Name = "textBuowner";
            this.textBuowner.Size = new System.Drawing.Size(173, 23);
            this.textBuowner.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10F);
            this.label3.Location = new System.Drawing.Point(48, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "BudgetOwner：";
            // 
            // textOpline
            // 
            this.textOpline.Location = new System.Drawing.Point(159, 17);
            this.textOpline.Name = "textOpline";
            this.textOpline.Size = new System.Drawing.Size(173, 23);
            this.textOpline.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F);
            this.label2.Location = new System.Drawing.Point(75, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "OpexLine：";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage2.Controls.Add(this.btn_select);
            this.tabPage2.Controls.Add(this.comboPLTy);
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Controls.Add(this.textAccDesc);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.textAcCode);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.textBOwner);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.textPline);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(932, 158);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "查询";
            // 
            // btn_select
            // 
            this.btn_select.Location = new System.Drawing.Point(159, 72);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(75, 23);
            this.btn_select.TabIndex = 39;
            this.btn_select.Text = "查询";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // comboPLTy
            // 
            this.comboPLTy.FormattingEnabled = true;
            this.comboPLTy.Location = new System.Drawing.Point(470, 43);
            this.comboPLTy.Name = "comboPLTy";
            this.comboPLTy.Size = new System.Drawing.Size(173, 21);
            this.comboPLTy.TabIndex = 38;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Arial", 10F);
            this.label19.Location = new System.Drawing.Point(398, 50);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(69, 16);
            this.label19.TabIndex = 37;
            this.label19.Text = "PLType：";
            // 
            // textAccDesc
            // 
            this.textAccDesc.Location = new System.Drawing.Point(159, 43);
            this.textAccDesc.Name = "textAccDesc";
            this.textAccDesc.Size = new System.Drawing.Size(173, 23);
            this.textAccDesc.TabIndex = 36;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial", 10F);
            this.label15.Location = new System.Drawing.Point(6, 50);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(152, 16);
            this.label15.TabIndex = 35;
            this.label15.Text = "Account_Description：";
            // 
            // textAcCode
            // 
            this.textAcCode.Location = new System.Drawing.Point(761, 17);
            this.textAcCode.Name = "textAcCode";
            this.textAcCode.Size = new System.Drawing.Size(173, 23);
            this.textAcCode.TabIndex = 21;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial", 10F);
            this.label16.Location = new System.Drawing.Point(652, 21);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(107, 16);
            this.label16.TabIndex = 20;
            this.label16.Text = "AccountCode：";
            // 
            // textBOwner
            // 
            this.textBOwner.Location = new System.Drawing.Point(470, 17);
            this.textBOwner.Name = "textBOwner";
            this.textBOwner.Size = new System.Drawing.Size(173, 23);
            this.textBOwner.TabIndex = 19;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Arial", 10F);
            this.label17.Location = new System.Drawing.Point(359, 21);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(108, 16);
            this.label17.TabIndex = 18;
            this.label17.Text = "BudgetOwner：";
            // 
            // textPline
            // 
            this.textPline.Location = new System.Drawing.Point(159, 17);
            this.textPline.Name = "textPline";
            this.textPline.Size = new System.Drawing.Size(173, 23);
            this.textPline.TabIndex = 17;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial", 10F);
            this.label18.Location = new System.Drawing.Point(75, 21);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(82, 16);
            this.label18.TabIndex = 16;
            this.label18.Text = "OpexLine：";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage3.Controls.Add(this.btn_down);
            this.tabPage3.Controls.Add(this.btn_export);
            this.tabPage3.Controls.Add(this.btn_import);
            this.tabPage3.Controls.Add(this.btn_open);
            this.tabPage3.Controls.Add(this.textPath);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(932, 158);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "操作";
            // 
            // btn_down
            // 
            this.btn_down.Location = new System.Drawing.Point(281, 64);
            this.btn_down.Name = "btn_down";
            this.btn_down.Size = new System.Drawing.Size(75, 23);
            this.btn_down.TabIndex = 5;
            this.btn_down.Text = "下载模版";
            this.btn_down.UseVisualStyleBackColor = true;
            this.btn_down.Click += new System.EventHandler(this.btn_down_Click);
            // 
            // btn_export
            // 
            this.btn_export.Location = new System.Drawing.Point(165, 63);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(75, 24);
            this.btn_export.TabIndex = 4;
            this.btn_export.Text = "导出";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // btn_import
            // 
            this.btn_import.Location = new System.Drawing.Point(56, 63);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(75, 24);
            this.btn_import.TabIndex = 3;
            this.btn_import.Text = "导入";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(585, 25);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(163, 25);
            this.btn_open.TabIndex = 2;
            this.btn_open.Text = "打开Excel文件";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // textPath
            // 
            this.textPath.Location = new System.Drawing.Point(56, 25);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(523, 23);
            this.textPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(936, 424);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseUp);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除toolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(101, 26);
            // 
            // 删除toolStripMenuItem
            // 
            this.删除toolStripMenuItem.Name = "删除toolStripMenuItem";
            this.删除toolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除toolStripMenuItem.Text = "删除";
            this.删除toolStripMenuItem.Click += new System.EventHandler(this.删除toolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(971, 185);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 185);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(971, 424);
            this.panel2.TabIndex = 3;
            // 
            // OPEX_Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(971, 609);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OPEX_Setting";
            this.Text = "OPEX_Setting";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.OPEX_Setting_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 删除toolStripMenuItem;
        private System.Windows.Forms.TextBox textAccountCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBuowner;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textOpline;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textAcCode;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBOwner;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textPline;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btn_down;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.ComboBox comboPLTy;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textAccDesc;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.ComboBox comboPltype;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textAccountDes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;

    }
}
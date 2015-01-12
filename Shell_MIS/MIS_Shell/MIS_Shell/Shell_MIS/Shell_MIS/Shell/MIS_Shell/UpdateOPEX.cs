using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;

namespace MIS_Shell
{
    public partial class UpdateOPEX : Form
    {
        public UpdateOPEX()
        {
            InitializeComponent();
        }
        DataTable dt=new DataTable() ;
        OPEXDal op = new OPEXDal();
        Pldb pl = new Pldb();
        /// <summary>
        /// 修改 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_up_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = OPEX_Setting.ID;
                string pltype = comboT.SelectedValue.ToString();

                string plline = "";
                if (comboT.SelectedText.ToString() == "")
                {
                    plline =pl.PlDbSelect(int.Parse(pltype)).Rows[0][1].ToString();
                }
                else
                {
                    plline = comboT.SelectedText.ToString();
                }

               
                string pls = this.textOpexLine.Text.ToString().Trim();
                string bo = this.textBudgetOwner.Text.ToString().Trim();
                string ac = this.textAccountCode.Text.ToString().Trim();
                string acd = this.textAccount_Des.Text.ToString().Trim();

                if (op.updateOPEXSetting(ID, pltype, plline, pls, bo, ac, acd) == 1)
                {
                    MessageBox.Show("修改成功！");
                    
                    this.Close();
                }
                else
                {
                    MessageBox.Show("操作失败！");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }

        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateOPEX_Load(object sender, EventArgs e)
        {
             this.comboT.DataSource = pl.PlDbSelect();
            comboT.DisplayMember = "PIDB_Item";
            comboT.ValueMember = "PLDB_Id";


            try
            {
                int ID = OPEX_Setting.ID;
                dt = op.OpexSettingSelect(ID);
                this.textID.Text = ID.ToString();
                this.comboT.SelectedValue=dt.Rows[0]["PLType"].ToString();

                this.textOpexLine.Text = dt.Rows[0]["OpexLine"].ToString();
                this.textBudgetOwner.Text = dt.Rows[0]["BudgetOwner"].ToString();
                this.textAccountCode.Text = dt.Rows[0]["AccountCode"].ToString();
                this.textAccount_Des.Text = dt.Rows[0]["Account_Description"].ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }
        }

    }
}

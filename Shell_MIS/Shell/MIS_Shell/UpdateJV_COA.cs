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
    /// <summary>
    /// 修改人：ydx
    /// 修改时间：2014-08-15
    /// </summary>
    public partial class UpdateJV_COA : Form
    {
        public UpdateJV_COA()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        JV_COADal jvcoadal = new JV_COADal();
        COADal coadal = new COADal();
        JVDal jvdal = new JVDal();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = JV_COASetting.ID;
                string AccountCode = this.txt_Account.Text.ToString().Trim();
                string Account_Description = this.txt_AccountD.Text.ToString().Trim();
                string Account = this.cb_AccountCode.SelectedValue.ToString();
                string CoCd = this.cb_CoCd.SelectedValue.ToString();

                if (jvcoadal.JV_COAUpdate(ID, AccountCode, Account_Description,Account,CoCd) == 1)
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

        private void UpdateJV_COA_Load(object sender, EventArgs e)
        {
            try
            {
                //修改人：ydx
                //修改时间：2014-08-15
                //修改目的：accountcode
                dt = coadal.COASelectcb();
                DataTable jv = new DataTable();
                cb_AccountCode.DataSource = dt;//accountCode
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    cb_AccountCode.DisplayMember = "cb";
                    cb_AccountCode.ValueMember = "Account";
                }
                //修改人：ydx
                //修改时间：2014-08-15
                //修改目的：cocd
                jv = jvdal.JVSelectImport();
                cb_CoCd.DataSource = jv;//CoCd
                for (int j = 0; j < jv.Rows.Count; j++)
                {

                    cb_CoCd.DisplayMember = "cb";
                    cb_CoCd.ValueMember = "CoCd";
                }
                int ID = JV_COASetting.ID;
                dt = jvcoadal.JV_COASelect(ID);
                this.txt_ID.Text = ID.ToString();
                this.txt_Account.Text = dt.Rows[0]["AccountCode"].ToString().Trim();
                this.txt_AccountD.Text = dt.Rows[0]["Account_Description"].ToString().Trim();
                this.cb_AccountCode.SelectedValue = dt.Rows[0]["SCLAccountCode"].ToString().Trim();
                this.cb_CoCd.SelectedValue = dt.Rows[0]["CoCd"].ToString().Trim();
               
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }
        }
    }
}

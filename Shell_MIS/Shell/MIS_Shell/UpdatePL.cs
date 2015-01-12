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
    public partial class UpdatePL : Form
    {
        public UpdatePL()
        {
            InitializeComponent();
        }
        PLDal pldal = new PLDal();
        DataTable dt = new DataTable();
        private void UpdatePL_Load(object sender, EventArgs e)
        {
            try
            {
                int ID = PLSetting.ID;
                dt = pldal.PLSelect(ID);
                this.txt_ID.Text = ID.ToString();
                this.txt_SAccGroup.Text = dt.Rows[0]["AccGroup"].ToString();
                this.txt_SAccSubGroup.Text = dt.Rows[0]["AccSubGroup"].ToString();
                this.txt_SAccType.Text = dt.Rows[0]["AccType"].ToString();
                this.txt_SAccSubType.Text = dt.Rows[0]["AccSubType"].ToString();
                this.txt_SAccountCode.Text = dt.Rows[0]["AccountCode"].ToString();
                this.txt_SAccountD.Text = dt.Rows[0]["Account_Description"].ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = PLSetting.ID;
                string AccGroup = this.txt_SAccGroup.Text.ToString().Trim();
                string AccSubGroup = this.txt_SAccSubGroup.Text.ToString().Trim();
                string AccType = this.txt_SAccType.Text.ToString().Trim();
                string AccSubType = this.txt_SAccSubType.Text.ToString().Trim();
                string AccountCode = this.txt_SAccountCode.Text.ToString().Trim();
                string Account_Description = this.txt_SAccountD.Text.ToString().Trim();

                if (pldal.PLUpdate(ID, AccGroup, AccSubGroup, AccType, AccSubType, AccountCode, Account_Description) == 1)
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
    }
}

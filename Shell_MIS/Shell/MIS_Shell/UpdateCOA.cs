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
    public partial class UpdateCOA : Form
    {
        public UpdateCOA()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        COADal coadal = new COADal(); int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        private void UpdateCOA_Load(object sender, EventArgs e)
        {
            try
            {
                int ID = COASetting.ID;
                dt = coadal.COASelect(ID);
                this.txt_ID.Text = ID.ToString();
                this.txt_AT.Text = dt.Rows[0]["AccountType"].ToString();
                this.txt_Account.Text = dt.Rows[0]["Account"].ToString();
                this.txt_AccountD.Text = dt.Rows[0]["Account_Description"].ToString();
                this.comboStatus.SelectedText= dt.Rows[0]["Status"].ToString();
                this.comboStatus.SelectedItem = dt.Rows[0]["Status"].ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString()); int result = logDal.OpertionLogInsert(userid, "COA修改操作", DateTime.Now.ToString(), "COA操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = COASetting.ID;
                string AccountType = this.txt_AT.Text.ToString().Trim();
                string Account = this.txt_Account.Text.ToString().Trim();
                string Account_Description = this.txt_AccountD.Text.ToString().Trim();
               // string Status = this.comboStatus.SelectedItem.ToString();
                string Status = this.comboStatus.Text;
                string Update = DateTime.Now.ToString();

                if (coadal.COAUpdate(ID,AccountType,Account,Account_Description,Status,Update) == 1)
                {
                    MessageBox.Show("修改成功！");
                    int result = logDal.OpertionLogInsert(userid, "COA修改操作", DateTime.Now.ToString(), "COA修改成功");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("操作失败！");
                    int result = logDal.OpertionLogInsert(userid, "COA修改操作", DateTime.Now.ToString(), "COA操作失败");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }               
            }
            
catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "COA修改操作", DateTime.Now.ToString(), "COA操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
    }
}

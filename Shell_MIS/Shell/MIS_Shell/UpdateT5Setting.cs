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
    public partial class UpdateT5Setting : Form
    {

        public UpdateT5Setting()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        T5SettingDAL t5dal = new T5SettingDAL();
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志


        private void But_Update_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = T5_Settingcs.ID;
                string t5 = this.txt_t5.Text.ToString().Trim();
                string deptCH = this.txt_deptCH.Text.ToString().Trim();
                string deptPinYin = this.txt_deptPinYin.Text.ToString().Trim();

                if (t5dal.Update(ID, t5, deptCH, deptPinYin)==1)
                {
                    MessageBox.Show("修改成功！");
                    int result = logDal.OpertionLogInsert(userid, "T5Setting修改操作", DateTime.Now.ToString(), "T5Setting修改成功");
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
                    int result = logDal.OpertionLogInsert(userid, "T5Setting修改操作", DateTime.Now.ToString(), "T5Setting操作失败");
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
                int result = logDal.OpertionLogInsert(userid, "T5Setting修改操作", DateTime.Now.ToString(), "T5Setting操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }

        private void UpdateT5Setting_Load(object sender, EventArgs e)
        {
            try
            {
                int ID = T5_Settingcs.ID;

                dt = t5dal.T5Select(" and ID='" + ID + "'");
                this.txt_ID.Text = ID.ToString();
                this.txt_t5.Text = dt.Rows[0]["SCL_T5"].ToString();
                this.txt_deptCH.Text = dt.Rows[0]["DeptNameCH"].ToString();
                this.txt_deptPinYin.Text = dt.Rows[0]["DeptNamePinYin"].ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString()); int result = logDal.OpertionLogInsert(userid, "T5Setting修改操作", DateTime.Now.ToString(), "T5Setting操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
    }
}

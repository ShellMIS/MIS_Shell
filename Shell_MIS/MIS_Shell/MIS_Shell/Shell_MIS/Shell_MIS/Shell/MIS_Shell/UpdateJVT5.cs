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
    public partial class UpdateJVT5 : Form
    {
        DataTable dt = new DataTable();
        JVT5SettingDAL jvt5dal = new JVT5SettingDAL();
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志

        public UpdateJVT5()
        {
            InitializeComponent();
        }

        private void But_Update_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = JV_T5Setting.ID;
                string t5 = this.txt_t5.Text.ToString().Trim();
                string jv_t5 = this.txt_jvt5.Text.ToString().Trim();
                string cocd = this.cb_CoCd.SelectedValue.ToString();

                if (jvt5dal.Update(ID, t5, jv_t5, cocd) == 1)
                {
                    MessageBox.Show("修改成功！");
                    int result = logDal.OpertionLogInsert(userid, "JVT5Setting修改操作", DateTime.Now.ToString(), "JVT5Setting修改成功");
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
                    int result = logDal.OpertionLogInsert(userid, "JVT5Setting修改操作", DateTime.Now.ToString(), "JVT5Setting操作失败");
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
                int result = logDal.OpertionLogInsert(userid, "JVT5Setting修改操作", DateTime.Now.ToString(), "JVT5Setting操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }

        private void UpdateJVT5_Load(object sender, EventArgs e)
        {
            DataTable dt = jvt5dal.SelectCocd("");
            cb_CoCd.DataSource = dt;//添加
            cb_CoCd.DisplayMember = "cb";
            cb_CoCd.ValueMember = "CoCd";
            try
            {
                int ID = JV_T5Setting.ID;

                dt = jvt5dal.JVT5Select(" and ID='" + ID + "'");
                this.txt_ID.Text = ID.ToString();
                this.txt_t5.Text = dt.Rows[0]["SCL_T5"].ToString();
                this.txt_jvt5.Text = dt.Rows[0]["JV_T5"].ToString();
                this.cb_CoCd.SelectedValue = dt.Rows[0]["CoCd"].ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString()); int result = logDal.OpertionLogInsert(userid, "JVT5Setting修改操作", DateTime.Now.ToString(), "JVT5Setting操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
    }

}

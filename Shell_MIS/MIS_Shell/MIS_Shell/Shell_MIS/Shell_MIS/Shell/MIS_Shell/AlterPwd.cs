using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MIS_Shell
{
    public partial class AlterPwd : Form
    {
        private static string pwd;
        public AlterPwd()
        {
            InitializeComponent();
        }

        private void AlterPwd_Load(object sender, EventArgs e)
        {
            DataTable dt = MIS_Login.dt;
            if (dt.Rows.Count == 1)
            {
                string username = dt.Rows[0]["UserName"].ToString().Trim();
                int userid = MIS_Login.userId;
                lbUserName.Text = username;
                pwd = dt.Rows[0]["UserPwd"].ToString().Trim();
            }

        }

        private void tbnSave_Click(object sender, EventArgs e)
        {
            string oldpwd = txtoldPwd.Text.Trim();
            string newpwd = txtpwd.Text.Trim();
            string confirmpwd = txtconfirmPwd.Text.Trim();
            if (oldpwd != pwd)
            {
                MessageBox.Show("旧密码输入错误");
                return;
            }
            if (oldpwd == "" || newpwd == "" || confirmpwd == "")
            {
                MessageBox.Show("有空白信息，请填写完整");
                return;
            }
            if (newpwd != confirmpwd)
            {
                MessageBox.Show("新密码与确认密码输入不一致");
                return;
            }
            DAL.UserDAL userdal = new DAL.UserDAL();
            int i = userdal.updateUserPwd(newpwd);
            if (i == 1)
            {
                if (MessageBox.Show("修改成功,请重新登录", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Restart();
                }
            }
            else
            {
                MessageBox.Show("修改失败!");
            }
        }

      
    }
}

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
    public partial class MIS_Login : Form
    {
        public MIS_Login()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 修改人 ：ydx
        /// 修改时间：2014-07-14 
        /// 修改目的：判断登录失败的原因：用户名错误、密码错误、并且用户名密码都区分大小写。当用户成功登录后将此用户的信息保存到dt里
        /// </summary>
       UserDAL userdal = new UserDAL();
       public static DataTable dt = new DataTable();
       public static int userId = 0;//用户编号
        private void button1_Click(object sender, EventArgs e)
        {
            string username = this.txt_loginname.Text.ToString().Trim();//ToolStripMenuItem
            string pwd = this.txt_pwd.Text.ToString().Trim();//"MIS"; //
            int sta =-1;
            dt = userdal.ExitUser(username);
            if(dt.Rows.Count>0)
            {
                bool flag=true;
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    if (dt.Rows[i]["UserName"].ToString().Trim().ToLower()!=username.ToLower())//判断用户名是否正确
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                        sta = i;
                        break;
                    }
                 }
                if (flag == false)
                {
                   if(sta>-1)
                   {
                       if (dt.Rows[sta]["UserPwd"].ToString().Trim().ToLower()==pwd.ToLower())//判断密码是否正确
                       {
                           dt = userdal.UserSelect(int.Parse(dt.Rows[sta]["UserID"].ToString()));
                           userId = int.Parse(dt.Rows[sta]["UserID"].ToString().Trim());//用户编号
                           this.Hide();
                           MIS_MDIParent mismdiparent = new MIS_MDIParent();
                           mismdiparent.ShowDialog();
                       }
                       else
                       {
                           MessageBox.Show("密码不正确！");
                       }
                   }
                }
                else
                {
                    MessageBox.Show("用户名不正确！");
                }
            }
            else
            {
                MessageBox.Show("不存在此用户~！");
            }
        }
        private void MIS_Login_Load(object sender, EventArgs e)
        {
            this.skinEngine1.SkinFile = Application.StartupPath + "\\SSK\\OneOrange.ssk";
        }
    }
}

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
    //添加人：ydx
    //添加目的:用户组管理->选中某个人进行修改
    public partial class UpdateUser : Form
    {
        DateTime now = DateTime.Now;
        DataTable dt = new DataTable();
        RoleDAL rd = new RoleDAL();
        UserDAL ud = new UserDAL();
        UserAndRole uandrDal = new UserAndRole();
        OptionLogDAL logDal = new OptionLogDAL();//日志
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());

        public UpdateUser()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 修改提交按钮 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_up_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = MIS_UsersManager.ID;
                xuID.Text = ID.ToString();
                string roleId = xuComb.SelectedValue.ToString().Trim();
                string uid = xuID.Text.ToString().Trim();
                string name = xuName.Text.ToString().Trim();
                string pass = xuPass.Text.ToString().Trim();
                string num1 = xuNum1.Text.ToString().Trim();
                string num2 = xuNum2.Text.ToString().Trim();
                string emai = xuEmail.Text.ToString().Trim();
                string dep = xuDep.Text.ToString().Trim();
                string real = xuReal.Text.ToString().Trim();
                string modiBy = MIS_Login.dt.Rows[0]["UserName"].ToString().Trim();
                string modyDate = now.Year + "-" + now.Month + "-" + now.Day + " " + now.Hour + ":" + now.Minute + ":" + now.Second;

                if (ud.updateUser(int.Parse(uid), name, pass, dep, real, num1, num2, emai, modiBy, modyDate) == 1 && uandrDal.updateUandR(uid, roleId, modiBy, modyDate) == 1)
                {
                    MessageBox.Show("修改成功！");
                    int result = logDal.OpertionLogInsert(userid, "修改用户信息", DateTime.Now.ToString(), "修改用户信息成功！");
                    if (result < 0)
                    {
                        MessageBox.Show("插入日志失败！");
                        return;
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("操作失败！");
                    int result = logDal.OpertionLogInsert(userid, "修改用户信息", DateTime.Now.ToString(), "修改用户信息失败！");
                    if (result < 0)
                    {
                        MessageBox.Show("插入日志失败！");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作异常，找管理员处理！" + ex.Message);
                int result = logDal.OpertionLogInsert(userid, "修改用户信息", DateTime.Now.ToString(), "操作异常！");
                if (result < 0)
                {
                    MessageBox.Show("插入日志失败！");
                    return;
                }
            }
        }
        /// <summary>
        /// 窗体加载 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateUser_Load(object sender, EventArgs e)
        {
            this.xuComb.DataSource = rd.RoleSelect();
            xuComb.DisplayMember = "RoleName";
            xuComb.ValueMember = "RoleID";
            try
            {
                int ID = MIS_UsersManager.ID;
                dt = ud.UserSelect(ID);
                this.xuID.Text = ID.ToString();
                this.xuComb.SelectedValue = dt.Rows[0]["RoleID"].ToString();

                this.xuName.Text = dt.Rows[0]["UserName"].ToString().Trim();
                this.xuPass.Text = dt.Rows[0]["UserPwd"].ToString().Trim();
                this.xuNum1.Text = dt.Rows[0]["PhoneNum1"].ToString().Trim();
                this.xuNum2.Text = dt.Rows[0]["PhoneNum2"].ToString().Trim();
                this.xuEmail.Text = dt.Rows[0]["Email"].ToString().Trim();
                this.xuDep.Text = dt.Rows[0]["DeptID"].ToString().Trim();
                this.xuReal.Text = dt.Rows[0]["RealName"].ToString().Trim();

            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败" + ex.Message);
                int result = logDal.OpertionLogInsert(userid, "双击修改用户", DateTime.Now.ToString(), "双击修改用户时加载修改窗体失败");
                if (result < 0)
                {
                    MessageBox.Show("插入日志失败！");
                }
            
            }
        }
    }
}

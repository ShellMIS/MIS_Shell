using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using System.Web;

namespace MIS_Shell
{
    //添加人：ydx
    //添加目的:用户管理的所有处理都在此窗体完成
    public partial class MIS_UsersManager : Form
    {
        DateTime now = DateTime.Now;
        //黄晓艳 2014/11/14
        OptionLogDAL logDal = new OptionLogDAL();//操作日志
        int UserID = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户ID
        DepartmentDAL deparDal = new DepartmentDAL();//部门


        RoleDAL roleDal = new RoleDAL();
        UserDAL usDal = new UserDAL();
        UserAndRole uarDal = new UserAndRole();
        public static int ID;

        public MIS_UsersManager()
        {
            InitializeComponent();
            this.Text = "人员管理";
        }
        /// <summary>
        /// 窗体加载进来后要绑定角色下拉框 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MIS_UsersManager_Load(object sender, EventArgs e)
        {

            //添加选项卡里  绑定角色到下拉列表里
            this.comboRole.DataSource = roleDal.RoleSelect();
            comboRole.DisplayMember = "RoleName";
            comboRole.ValueMember = "RoleID";
            //查询选项卡里 绑定角色到下拉列表里
            this.tURole.DataSource = roleDal.RoleSelect();
            tURole.DisplayMember = "RoleName";
            tURole.ValueMember = "RoleID";

            //部门下拉列表  黄晓艳 2014-11-14
            DataTable dt = deparDal.GetDepartmentCombox();
            DataRow drDepart = dt.NewRow();
            drDepart["ID"] = 0;
            drDepart["Name"] = "请选择";
            dt.Rows.InsertAt(drDepart, 0);
            comboDep.DataSource = dt;
            comboDep.DisplayMember = "Name";
            comboDep.ValueMember = "ID";

            comDe.DataSource = dt;
            DataRow dr = dt.NewRow();
            dr["ID"] = 0;
            dr["Name"] = "请选择";
            dt.Rows.InsertAt(dr, 0);
            comDe.DisplayMember = "Name";
            comDe.ValueMember = "ID";
            dataGridView1.DataSource = usDal.UserSelect();

            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridView1.Columns["RealName"].DefaultCellStyle.Font = new Font("宋体", 10);

            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.Font = new System.Drawing.Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
        }

        /// <summary>
        /// dataGridView单元格双击事件  弹出修改页面  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            UpdateUser up = new UpdateUser();
            up.Owner = this;
            up.ShowDialog();
            dataGridView1.DataSource = usDal.UserSelect();
        }
        /// <summary>
        /// dataGridView的单元格鼠标弹起事件  单选、多选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                this.contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                dataGridView1.Rows[e.RowIndex].Selected = true;                 
            }
        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.SelectedRows.Count;
            if (row == 0)
            {
                MessageBox.Show("没有选中任何行", "Error");
                return;
            }
            else if (MessageBox.Show("确认删除选中的" + row.ToString() + "条记录吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = dataGridView1.SelectedRows.Count; i > 0; i--)
                {
                    int rno = dataGridView1.SelectedRows[i - 1].Index;
                    int id = Convert.ToInt32(dataGridView1.Rows[rno].Cells[0].Value.ToString());
                    if (uarDal.deleteUserAndRole(id.ToString()) == 1 && usDal.deleteUser(id) == 1)
                    {
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);
                    }
                    else
                    {
                        MessageBox.Show("操作失败");
                    }
                }
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_add_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            string uname = textUserName.Text;
            //string upass = textUserPass.Text;
            string upass = "MIS";
            string depid = comboDep.SelectedValue.ToString();
            if (depid=="0")
            {
                MessageBox.Show("请选择部门!");
                return;
            }
            string realname = textRealName.Text;
            string num1 = textNum1.Text;
            string num2 = textNum2.Text;
            string emai = textEmail.Text;
            string createBy = MIS_Login.dt.Rows[0]["UserName"].ToString();
            string createDate = now.Year + "-" + now.Month + "-" + now.Day + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
            string modBY = " ";
            string modeDate = " ";
            string roleId = comboRole.SelectedValue.ToString();
            if (uname != "" && upass != "")
            {
                try
                {
                    //查询是否已经存在改用户名
                    if (usDal.IsExsit(uname))
                    {
                        MessageBox.Show("该用户名已经被使用，请使用其他用户名！");
                        return;
                    }
                    //插入
                    int userId = usDal.UserInsert(uname, upass, depid, realname, num1, num2, emai, createBy, createDate, modBY, modeDate);
                    if (userId > 0)
                    {
                        string cby = MIS_Login.dt.Rows[0]["UserName"].ToString();
                        string cdate = now.Year + "-" + now.Month + "-" + now.Day + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
                        string moby = "";
                        string modate = "";
                        if (uarDal.UandRInsert(userId.ToString(), roleId, cby, cdate, moby, modate) > 0)
                        {
                            MessageBox.Show("添加成功");
                            dataGridView1.DataSource = usDal.UserSelect();
                            #region 操作日志记录
                            int result = logDal.OpertionLogInsert(UserID, "添加用户", DateTime.Now.ToString(), "添加用户成功");
                            if (result <= 0)
                            {
                                MessageBox.Show("日志插入失败！");

                            }
                            #endregion
                        }
                    }
                    else
                    {
                        MessageBox.Show("添加用户失败");
                        #region 操作日志记录
                        int result = logDal.OpertionLogInsert(UserID, "添加用户", DateTime.Now.ToString(), "添加用户失败");
                        if (result <= 0)
                        {
                            MessageBox.Show("日志插入失败！");
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("操作异常，请联系开发人员" + ex.Message);
                }

            }
            else
            {
                MessageBox.Show("用户名密码不能为空");


            }
        }

        /// <summary>
        /// 查询按钮 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            string uname = tUName.Text;
            string upass = tUPass.Text;
            string depid = "";
            string realname = tUReal.Text;
            string num1 = tUN1.Text;
            string num2 = tuNu2.Text;
            string emai = tUEmai.Text;
            string roleId = tURole.SelectedValue.ToString();
            try
            {
                dataGridView1.DataSource = usDal.UserSelect(uname, upass, depid, realname, num1, num2, emai, roleId);
                #region 日志
                int result = logDal.OpertionLogInsert(UserID, "用户查询", DateTime.Now.ToString(), "用户查询成功！");
                if (result <= 0)
                {
                    MessageBox.Show("日志插入失败！");
                }
                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败" + ex.Message);
                #region 日志记录
                int result = logDal.OpertionLogInsert(UserID, "用户查询", DateTime.Now.ToString(), "用户查询失败！");
                if (result <= 0)
                {
                    MessageBox.Show("日志插入失败！");
                }
                #endregion

            }
        }
    }
}

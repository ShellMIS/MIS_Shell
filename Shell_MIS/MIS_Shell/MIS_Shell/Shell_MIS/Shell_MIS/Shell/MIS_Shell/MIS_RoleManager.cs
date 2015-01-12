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
    /// 添加人：ydx
    /// 添加时间：2014-11-03
    /// 添加目的：角色管理
    /// </summary>
    public partial class MIS_RoleManager : Form
    {
        RoleDAL roleDal = new RoleDAL();//角色对象
        DataTable roleTable = new DataTable();//角色下拉列表
        UserDAL userDal = new UserDAL();//用户对象
        UserAndRole userAndRoleDal = new UserAndRole();//用户角色
        DataTable userTable = new DataTable();//
        DepartmentDAL departDal = new DepartmentDAL();//部门
        DataTable departTable = new DataTable();//部门下拉列表
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//操作日志
        public MIS_RoleManager()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-11-03
        /// 添加目的：角色管理 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MIS_RoleManager_Load(object sender, EventArgs e)
        {
            //角色
            roleTable = roleDal.RoleSelectCombox();
            DataRow dr = roleTable.NewRow();
            dr["RoleID"] = "0";
            dr["RoleName"]="请选择";
            roleTable.Rows.InsertAt(dr,0);
            comboBoxRole.DataSource = roleTable;
            comboBoxRole.ValueMember = "RoleID";
            comboBoxRole.DisplayMember = "RoleName";

            //部门
            departTable = departDal.GetDepartmentCombox();
            DataRow drDepart = departTable.NewRow();
            drDepart["ID"] = 0;
            drDepart["Name"] = "请选择";
            departTable.Rows.InsertAt(drDepart,0);
            comboBoxDepart.DataSource = departTable;
            comboBoxDepart.ValueMember = "ID";
            comboBoxDepart.DisplayMember = "Name";

            //人员复选框
            userTable=userDal.UserListComb();
            checkedListBoxUserList.DataSource = userTable;
            checkedListBoxUserList.ValueMember = "UserID";
            checkedListBoxUserList.DisplayMember = "ur";
            //设置复选列表
            checkedListBoxUserList.MultiColumn = true;
            checkedListBoxUserList.ColumnWidth = 300;
            
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-11-14
        /// 添加目的：选择不同的角色 右面的人员复选列表里属于此角色的被选中，否则不被选中
        /// </summary>
        private void RoleBind()
        {
            //选择部分人员之后  如果下拉列表被选择了，则显示此部门下在这角色下的显示情况
            if (this.comboBoxRole.SelectedValue.ToString().Trim() != "System.Data.DataRowView" && comboBoxRole.SelectedValue.ToString().Trim() != "0")//不同类别的选择条件
            {
                for (int i = 0; i < checkedListBoxUserList.Items.Count; i++)
                {
                    checkedListBoxUserList.SetItemChecked(i, false);
                }
                DataTable table = userAndRoleDal.userIdByRoleId(int.Parse(comboBoxRole.SelectedValue.ToString().Trim()));//所选角色具有的权限
                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        for (int j = 0; j < checkedListBoxUserList.Items.Count; j++)
                        {
                            DataRowView dv = ((DataRowView)checkedListBoxUserList.Items[j]);
                            if (table.Rows[i]["UserID"].ToString().Trim() == dv["UserID"].ToString().Trim()) //checkedListBoxHave.GetItemText(checkedListBoxHave.Items[j]).ToString().Trim())// tableCheck.Rows[j]["PrivilegeID"].ToString().Trim())
                            {
                                checkedListBoxUserList.SetItemChecked(j, true);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < checkedListBoxUserList.Items.Count; i++)
                    {
                        checkedListBoxUserList.SetItemChecked(i, false);
                    }
                }
            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-11-03
        /// 添加目的:确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if(comboBoxRole.SelectedValue.ToString().Trim()!="0")
            {
                //给此角色分配用户之前 先删除此角色的用户
                 userAndRoleDal.userAndRoleDelete(comboBoxRole.SelectedValue.ToString().Trim());
                
                //给此角色分配用户
                bool flag = false;
                for (int i = 0; i < checkedListBoxUserList.CheckedItems.Count; i++)
                {
                    DataRowView dv = ((DataRowView)checkedListBoxUserList.CheckedItems[i]);
                    if (int.Parse(userAndRoleDal.IfExistsRoleAndUser(int.Parse(comboBoxRole.SelectedValue.ToString().Trim()), int.Parse(dv["UserID"].ToString().Trim())).Rows[0][0].ToString().Trim()) <= 0)
                    {
                        string date = DateTime.Now.ToString();
                        if (userAndRoleDal.UandRInsert(dv["UserID"].ToString().Trim(), comboBoxRole.SelectedValue.ToString().Trim(), "添加人", date, "修改人", date) > 0)
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                }
                if (flag)
                {
                    MessageBox.Show("角色管理异常，请联系开发人员！");
                    int addDepartResult = logDal.OpertionLogInsert(userid, "给角色分配用户", DateTime.Now.ToString(), "角色管理异常，请联系开发人员！");
                    if (addDepartResult < 0)
                    {
                        MessageBox.Show("插入日志失败！");
                    }
                }
                else
                {
                    MessageBox.Show("角色管理成功！");
                    int addDepartResult = logDal.OpertionLogInsert(userid, "给角色分配用户", DateTime.Now.ToString(), "角色管理成功！");
                    if (addDepartResult < 0)
                    {
                        MessageBox.Show("插入日志失败！");
                    }
                }
            }
            else if (comboBoxRole.SelectedValue.ToString().Trim()=="0")
            {
                MessageBox.Show("请选择角色");
            }
           
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-11-03
        /// 添加目的：全选复选框 选择改变 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxAll.Checked)
            {
                for (int i = 0; i < checkedListBoxUserList.Items.Count;i++ )
                {
                    checkedListBoxUserList.SetItemChecked(i,true);
                }
            }else
            {
                for (int i = 0; i < checkedListBoxUserList.Items.Count; i++)
                {
                    checkedListBoxUserList.SetItemChecked(i, false);
                }
            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-11-03
        /// 添加目的：角色下拉列表 选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxRole_SelectedValueChanged(object sender, EventArgs e)
        {
            RoleBind();//如果角色下拉列表选中，右侧人员复选列表中此角色下的人员被选中不属于此角色的 不被选中
             if (comboBoxRole.SelectedValue.ToString().Trim() == "0")//不同类别的选择条件
            {
                for (int i = 0; i < checkedListBoxUserList.Items.Count; i++)
                {
                    checkedListBoxUserList.SetItemChecked(i, false);
                }
            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-11-13
        /// 添加目的：选择不同部门 人员列表显示此部门下的所有用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxDepart_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.comboBoxDepart.SelectedValue.ToString().Trim() != "System.Data.DataRowView" && comboBoxDepart.SelectedValue.ToString().Trim() != "0")//不同类别的选择条件
            {
                //先清空 复选列表
                if (checkedListBoxUserList.Items.Count>0)
                {
                    checkedListBoxUserList.DataSource = null;
                }
                DataTable table=new DataTable();
                try
                {
                    table = userDal.userIdByDepId(int.Parse(comboBoxDepart.SelectedValue.ToString().Trim()));
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                    int addDepartResult = logDal.OpertionLogInsert(userid, "选择部门,显示此部门下的所有用户", DateTime.Now.ToString(), "选择部门时发生错误！");
                    if (addDepartResult < 0)
                    {
                        MessageBox.Show("插入日志失败！");
                    }
                }
             
                if (table.Rows.Count > 0)
                {
                    //绑定 人员复选列表
                    checkedListBoxUserList.DataSource = table;
                    checkedListBoxUserList.ValueMember = "UserID";
                    checkedListBoxUserList.DisplayMember = "ur";
                    checkedListBoxUserList.MultiColumn = true;
                    checkedListBoxUserList.ColumnWidth = 300;
                }
               
            }
            else if (comboBoxDepart.SelectedValue.ToString().Trim() == "0")
            {
                    userTable = userDal.UserListComb();
                    checkedListBoxUserList.DataSource = userTable;
                    checkedListBoxUserList.ValueMember = "UserID";
                    checkedListBoxUserList.DisplayMember = "ur";
                    //设置复选列表
                    checkedListBoxUserList.MultiColumn = true;
                    checkedListBoxUserList.ColumnWidth = 300;
            }
            //如果角色下拉列表选中，右侧人员复选列表中此角色下的人员被选中不属于此角色的 不被选中
            RoleBind();
          
        }
    }
}

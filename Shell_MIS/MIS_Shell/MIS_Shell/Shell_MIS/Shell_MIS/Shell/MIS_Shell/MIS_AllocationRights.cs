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
    /// 添加时间：2014-10-30
    /// 添加目的：给不同的角色 分配权限
    /// </summary>
    public partial class MIS_AllocationRights : Form
    {
        DataTable table = new DataTable();//角色 下拉列表
        RoleDAL roleDal = new RoleDAL();//角色
        DataTable tableCheck = new DataTable();//所有模块
        PrivilegeDAL privilegeDal = new PrivilegeDAL();//模块
        RolePrivilegeDAL rolePrivilegeDal = new RolePrivilegeDAL();//角色 权限
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        public MIS_AllocationRights()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MIS_AllocationRights_Load(object sender, EventArgs e)
        {
            //角色下拉列表
            table = roleDal.RoleSelectCombox();
            DataRow dr = table.NewRow();
            dr["RoleID"] = "0";//此处是int型 一定为数字
            dr["RoleName"] = "请选择";
            table.Rows.InsertAt(dr,0);
            comboBoxRole.DataSource = table;
            comboBoxRole.DisplayMember = "RoleName";
            comboBoxRole.ValueMember = "RoleID";
            comboBoxRole.SelectedIndex=0;
            //所有模块 复选框
            tableCheck = privilegeDal.PrivilegeSelect();
            checkedListBoxHave.DataSource = tableCheck;
            checkedListBoxHave.ValueMember = "PrivilegeID";
            checkedListBoxHave.DisplayMember = "Description";
            //设置复选列表
            checkedListBoxHave.MultiColumn = true;
            checkedListBoxHave.ColumnWidth = 300;
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-30
        /// 添加目的：分配权限  按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (comboBoxRole.SelectedValue.ToString().Trim()=="0")
            {
                MessageBox.Show("请选择角色");
            }else
            {
                //在分配权限之前 先删除此角色下的权限
                //for (int j = 0; j < checkedListBoxHave.CheckedItems.Count;j++ )
                //{
                   // DataRowView dv = ((DataRowView)checkedListBoxHave.CheckedItems[j]);
                    rolePrivilegeDal.rolePrivilegeDelete(int.Parse(comboBoxRole.SelectedValue.ToString().Trim()));
                //}
                 //删除此角色下的权限之后，重新分配权限
                bool flag = false;
                for (int i = 0; i < checkedListBoxHave.CheckedItems.Count; i++)
                {
                    DataRowView dv = ((DataRowView)checkedListBoxHave.CheckedItems[i]);

                    if (int.Parse(rolePrivilegeDal.IfExistsRoleAndPrivilege(int.Parse(comboBoxRole.SelectedValue.ToString().Trim()), int.Parse(dv["PrivilegeID"].ToString().Trim())).Rows[0][0].ToString().Trim()) <= 0)
                    {
                        if (rolePrivilegeDal.rolePrivilegeInsert(int.Parse(comboBoxRole.SelectedValue.ToString().Trim()), int.Parse(dv["PrivilegeID"].ToString().Trim()), "0") > 0)
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
                    MessageBox.Show("权限分配出现异常，请联系开发人员！");
                    int result = logDal.OpertionLogInsert(userid, "权限分配操作", DateTime.Now.ToString(), "权限分配出现异常");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("权限分配成功！");
                    int result = logDal.OpertionLogInsert(userid, "权限分配操作", DateTime.Now.ToString(), "权限分配成功");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
            }    
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-30
        /// 添加目的:全选 复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_SelecAll.Checked)
            {
                for (int i = 0; i < checkedListBoxHave.Items.Count; i++)
                {
                    checkedListBoxHave.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < checkedListBoxHave.Items.Count; i++)
                {
                    checkedListBoxHave.SetItemChecked(i, false);
                }
            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-30
        /// 添加目的:角色 选择改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxRole_SelectedValueChanged(object sender, EventArgs e)
        {
                if (this.comboBoxRole.SelectedValue.ToString().Trim() != "System.Data.DataRowView" && comboBoxRole.SelectedValue.ToString().Trim() != "0")//不同类别的选择条件
                {
                    for (int i = 0; i < checkedListBoxHave.Items.Count; i++)
                    {
                        checkedListBoxHave.SetItemChecked(i, false);
                    }
                    DataTable table = rolePrivilegeDal.RolePrivilegeSelect(int.Parse( comboBoxRole.SelectedValue.ToString().Trim()));//所选角色具有的权限
                   if(table.Rows.Count>0)
                   {
                       for (int i = 0; i < table.Rows.Count; i++)
                       {
                           for (int j = 0; j < checkedListBoxHave.Items.Count; j++)
                           {
                               DataRowView dv = ((DataRowView)checkedListBoxHave.Items[j]);
                               if (table.Rows[i]["PrivilegeId"].ToString().Trim() == dv["PrivilegeID"].ToString().Trim()) //checkedListBoxHave.GetItemText(checkedListBoxHave.Items[j]).ToString().Trim())// tableCheck.Rows[j]["PrivilegeID"].ToString().Trim())
                               {
                                   checkedListBoxHave.SetItemChecked(j, true);
                               }
                           }
                       }
                   }else
                   {
                       for (int i = 0; i < checkedListBoxHave.Items.Count;i++ )
                       {
                           checkedListBoxHave.SetItemChecked(i,false);
                       }
                   }
                }
                else if (this.comboBoxRole.SelectedValue.ToString().Trim() != "System.Data.DataRowView" && comboBoxRole.SelectedValue.ToString().Trim() == "0")//不同类别的选择条件
                {
                   // MessageBox.Show("请选择角色");
                    //for (int i = 0; i < checkedListBoxHave.Items.Count; i++)
                    //{
                    //    checkedListBoxHave.SetItemChecked(i, false);
                    //}
                }

        }

    }
}

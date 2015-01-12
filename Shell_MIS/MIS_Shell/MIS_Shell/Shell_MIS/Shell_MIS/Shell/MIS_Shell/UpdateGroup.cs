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
    /// 添加人:ydx
    /// 添加时间：2014-07-24
    /// 添加目的：一级分组修改
    /// </summary>
    public partial class UpdateGroup : Form
    {
        public UpdateGroup()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        Group1Dal groupdal = new Group1Dal();
        private void UpdateGroup_Load(object sender, EventArgs e)
        {
            try
            {
                int ID = GroupSetting.ID;
                dt = groupdal.GroupSelect(ID);
                this.txt_ID.Text = ID.ToString().Trim();
                this.txt_Gr1.Text = dt.Rows[0]["Group1Name"].ToString().Trim();
                this.comboStatU.SelectedItem = dt.Rows[0]["Status"].ToString().Trim();
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }
        }
        ///<summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = GroupSetting.ID;

                string Group1Name = this.txt_Gr1.Text.ToString().Trim();
                string State = this.comboStatU.SelectedItem.ToString().Trim();
                //DataTable dtExis=groupdal.GroupSelect(Group1Name,"");
                //if(dtExis.Rows.Count>0)
                //{
                //    MessageBox.Show("已有此记录：" + Group1Name);
                //}else
                //{
                    if (groupdal.GroupUpdate(ID, Group1Name, State) == 1)
                    {
                        MessageBox.Show("修改成功！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                    }
                
                //}
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }
        }
       
    }
}

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
    /// 添加时间:2014-07-24
    /// 添加目的:二级分组修改
    /// </summary>
    public partial class UpdateGroup2 : Form
    {
        public UpdateGroup2()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        Group2Dal gr2Dal = new Group2Dal();
        Group1Dal gr1Dal = new Group1Dal();
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = Group2Setting.ID;
                string Group2Name = this.txt_Group2Name.Text.ToString().Trim();
                string State = "";
                if (this.comboSta2U.SelectedItem.ToString().Trim()!="请选择")
                {
                    State = this.comboSta2U.SelectedItem.ToString().Trim();
                }
                if (comboGroup1Id.SelectedValue.ToString().Trim()=="0")
                {
                    MessageBox.Show("请选择一级组别");
                }
                else if (Group2Name == "" || State == "")
                {

                    MessageBox.Show("有空白项，不允许提交");
                }
                else{
                    string gr1Id = comboGroup1Id.SelectedValue.ToString().Trim();
                    //DataTable dtExis = gr2Dal.GroupSelect(Group2Name, "", gr1Id);
                    //if (dtExis.Rows.Count > 0)
                    //{
                    //    MessageBox.Show("已有此记录：" + Group2Name);
                    //}
                   
                    //else
                    //{
                        if (gr2Dal.GroupUpdate(ID, Group2Name, State, gr1Id) == 1)
                        {
                            MessageBox.Show("修改成功！");
                            int result = logDal.OpertionLogInsert(userid, "Group2Setting修改操作", DateTime.Now.ToString(), "Group2Setting修改成功");
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
                            int result = logDal.OpertionLogInsert(userid, "Group2Setting修改操作", DateTime.Now.ToString(), "Group2Setting操作失败");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }

                   // }
                
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "Group2Setting修改操作", DateTime.Now.ToString(), "Group2Setting操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateGroup2_Load(object sender, EventArgs e)
        {

            DataTable dts = gr1Dal.GroupSelect();
            DataRow drS = dts.NewRow();
            drS["Id"] = "0";
            drS["Group1Name"] = "请选择";
            drS["Status"] = "0";
            dts.Rows.InsertAt(drS, 0);
            this.comboGroup1Id.DataSource = dts.DefaultView;
            comboGroup1Id.DisplayMember = "Group1Name";
            comboGroup1Id.ValueMember = "Id";
            try
            {
                int ID = Group2Setting.ID;
                dt = gr2Dal.GroupSelect(ID);
                this.txt_ID.Text = ID.ToString().Trim();
                this.txt_Group2Name.Text = dt.Rows[0]["Group2Name"].ToString().Trim();
                this.comboSta2U.SelectedItem = dt.Rows[0]["Status"].ToString().Trim();
                comboGroup1Id.SelectedValue = dt.Rows[0]["Group1Id"].ToString().Trim();
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "Group2Setting加载操作", DateTime.Now.ToString(), "Group2Setting操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
    }
}

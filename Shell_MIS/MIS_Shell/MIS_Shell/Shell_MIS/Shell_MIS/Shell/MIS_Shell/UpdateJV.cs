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
    public partial class UpdateJV : Form
    {
        public UpdateJV()
        {
            InitializeComponent();
        }
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        DataTable dt = new DataTable();
        JVDal jvdal = new JVDal();
        Group1Dal gr1Dal = new Group1Dal();
        Group2Dal gr2Dal = new Group2Dal();
        private void UpdateJV_Load(object sender, EventArgs e)
        {
            this.cbG1update.DataSource = gr1Dal.GroupSelect();
            cbG1update.DisplayMember = "Group1Name";
            cbG1update.ValueMember = "Id";

            this.cbg2Update.DataSource = gr2Dal.GroupSe();
            cbg2Update.DisplayMember = "Group2Name";
            cbg2Update.ValueMember = "Id";
            try
            {
                int ID = JVSetting.ID;
                dt = jvdal.JVSelect(ID);
                this.txt_ID.Text = ID.ToString();
                this.txt_CoCd.Text = dt.Rows[0]["CoCd"].ToString();
                this.txt_CoNameCH.Text = dt.Rows[0]["CoNameCH"].ToString();
                this.txt_CoNameEN.Text = dt.Rows[0]["CoNameEN"].ToString();
                this.txt_Share.Text = dt.Rows[0]["Share"].ToString();
                this.cbG1update.SelectedValue = dt.Rows[0]["JVGroup1"].ToString();
                this.cbg2Update.SelectedValue = dt.Rows[0]["JVGroup2"].ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "JV Setting修改操作", DateTime.Now.ToString(), "操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }

        private void but_submit_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = JVSetting.ID;
                string CoCd = this.txt_CoCd.Text.ToString().Trim();
                string CoNameCH = this.txt_CoNameCH.Text.ToString().Trim();
                string CoNameEN = this.txt_CoNameEN.Text.ToString().Trim();
                string Share = this.txt_Share.Text.ToString().Trim();
                string gr1 = this.cbG1update.SelectedValue.ToString().Trim();
                string gr2 = this.cbg2Update.SelectedValue.ToString().Trim();
                if (jvdal.JVUpdate(ID, CoCd, CoNameCH, CoNameEN, Share, gr1, gr2) == 1)
                {
                    MessageBox.Show("修改成功！");
                    int result = logDal.OpertionLogInsert(userid, "JV Setting修改操作", DateTime.Now.ToString(), "修改成功");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("操作成功！");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "JV Setting修改操作", DateTime.Now.ToString(), "操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-08-05
        /// 添加目的：选择分组1的同时分组2自动显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbG1update_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbG1update.SelectedValue.ToString() != "System.Data.DataRowView" && cbG1update.SelectedValue.ToString() != "0")//&& cg1.SelectedValue.ToString() != "0")
            {
                DataTable dts = gr2Dal.GroupSelectByGr1Id(int.Parse(cbG1update.SelectedValue.ToString()));
                cbg2Update.DataSource = dts.DefaultView;
                cbg2Update.DisplayMember = "Group2Name";
                cbg2Update.ValueMember = "Id";
            }
        }

       
    }
}

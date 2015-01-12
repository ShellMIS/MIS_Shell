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
    public partial class UpdateTCode : Form
    {
        public UpdateTCode()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        TCodeDal tcodedal = new TCodeDal();
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        JVDal jvdal = new JVDal();
        private void UpdateTCode_Load(object sender, EventArgs e)
        {
            try
            {
                int ID = TCodeSetting.ID;
                dt = tcodedal.TCodeSelect(ID);
                this.txt_ID.Text = ID.ToString();
                this.txt_TT.Text = dt.Rows[0]["TcodeType"].ToString();
                this.txt_Tcode.Text = dt.Rows[0]["Tcode"].ToString();
                this.txt_TN.Text = dt.Rows[0]["TcodeName"].ToString();
                DataTable jv = new DataTable();
                jv = jvdal.JVSelectImport();
                for (int j = 0; j < jv.Rows.Count; j++)
                {
                    cb_CoCd.DataSource = jv;
                    cb_CoCd.DisplayMember = "cb";
                    cb_CoCd.ValueMember = "CoCd";
                }
                this.cb_CoCd.SelectedValue = dt.Rows[0]["CoCd"].ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "TCode修改操作", DateTime.Now.ToString(), "TCode操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-08-14
        /// 修改目的：cocd默认显示为已经选择的条目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = TCodeSetting.ID;
                string TcodeType = this.txt_TT.Text.ToString().Trim();
                string Tcode = this.txt_Tcode.Text.ToString().Trim();
                string TcodeName = this.txt_TN.Text.ToString().Trim();
                string CoCd = cb_CoCd.SelectedValue.ToString();
                if (tcodedal.TCodeUpdate(ID, TcodeType, Tcode, TcodeName,CoCd) == 1)
                {
                    MessageBox.Show("修改成功！");
                    int result = logDal.OpertionLogInsert(userid, "TCode修改操作", DateTime.Now.ToString(), "TCode修改成功");
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
                    int result = logDal.OpertionLogInsert(userid, "TCode修改操作", DateTime.Now.ToString(), "TCode操作失败");
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
                int result = logDal.OpertionLogInsert(userid, "TCode修改操作", DateTime.Now.ToString(), "TCode操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
    }
}

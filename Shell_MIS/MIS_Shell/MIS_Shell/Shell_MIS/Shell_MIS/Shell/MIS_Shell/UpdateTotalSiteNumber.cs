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
    /// 添加时间：2014-08-28
    /// 添加目的：TotalSite 修改页面
    /// </summary>
    public partial class UpdateTotalSiteNumber : Form
    {
        DataTable dt = new DataTable();
        TotalSiteDAL totalSiteDal = new TotalSiteDAL();
        JVDal jvDal = new JVDal();//绑定coce

        public UpdateTotalSiteNumber()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTotalSiteNumber_Load(object sender, EventArgs e)
        {
            DataTable tableAdd = new DataTable();
            tableAdd = jvDal.JVCocd();
            DataRow dr = tableAdd.NewRow();
            dr["CoCd"] = "0";
            dr["cdNameEn"] = "请选择";
            tableAdd.Rows.InsertAt(dr, 0);
            comboCoCd.DataSource = tableAdd.DefaultView;
            comboCoCd.DisplayMember = "cdNameEn";
            comboCoCd.ValueMember = "CoCd";
            comboCoCd.SelectedItem = "请选择";
            int ID = TotalSiteSetting.ID;
            try
            {
                dt = totalSiteDal.TotalSiteSelect(ID);
                this.txtId.Text = ID.ToString();
                this.txtPeriod.Text = dt.Rows[0]["Period"].ToString().Trim();
                this.comboCoCd.SelectedValue = dt.Rows[0]["Cocd"].ToString().Trim();
                this.txtT3Code.Text = dt.Rows[0]["T3Code"].ToString().Trim();
                this.txtOperation.Text = dt.Rows[0]["TotalSiteinOperation"].ToString().Trim();
                this.txtTemporyary.Text = dt.Rows[0]["TemporaryClosedSites"].ToString().Trim();
                this.txtNewOpen.Text = dt.Rows[0]["NewOpenedSites"].ToString().Trim();
                this.txtClosdSite.Text = dt.Rows[0]["closedSites"].ToString().Trim();
                this.txtNewSecured.Text = dt.Rows[0]["NewSecuredSites"].ToString().Trim();
                this.txtUnopend.Text = dt.Rows[0]["Unopened"].ToString().Trim();

            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }
        }

        /// <summary>
        /// 修改页面 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_up_Click(object sender, EventArgs e)
        {
            string period = this.txtPeriod.Text.ToString().Trim();
            string cocd = this.comboCoCd.SelectedValue.ToString().Trim();
            if (cocd == "")
            {
                MessageBox.Show("请选择合资公司!");
                return;
            }
            string CoNameEn = this.comboCoCd.Text.ToString().Trim();
            if (this.comboCoCd.Text.ToString().Trim().IndexOf(" ") > 0)
            {
                CoNameEn = CoNameEn.Substring(CoNameEn.IndexOf(" ") + 1);
            }
            string T3Code = this.txtT3Code.Text.ToString().Trim();
            string operation = this.txtOperation.Text.ToString().Trim();
            string temperoray = this.txtTemporyary.Text.ToString().Trim();
            string NewOpen = this.txtNewOpen.Text.ToString().Trim();
            string closeSite = this.txtClosdSite.Text.ToString().Trim();
            string newSecured = this.txtNewSecured.Text.ToString().Trim();
            string unopend = this.txtUnopend.Text.ToString().Trim();
            try
            {
                int ID = TotalSiteSetting.ID;
                if (totalSiteDal.TotalSiteUpdate(ID, period, cocd, CoNameEn, T3Code, operation, temperoray, NewOpen, closeSite, newSecured, unopend) == 1)
                {
                    MessageBox.Show("修改成功！");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("操作失败！");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }
        }
    }
}

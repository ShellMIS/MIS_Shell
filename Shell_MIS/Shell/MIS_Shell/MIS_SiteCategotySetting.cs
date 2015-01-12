using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Office.Interop;
using DAL;
using MIS_Shell.CommExcel;

namespace MIS_Shell
{
    /// <summary>
    /// 添加人：ydx
    /// 修改人：ydx
    /// 修改时间：2014-07-31
    /// 添加目的：类别管理
    /// </summary>
    public partial class MIS_SiteCategotySetting : Form
    {

        SiteCategoryDal siteDal = new SiteCategoryDal();
        DataTable dt = new DataTable();
        ImportToExcel imp = new ImportToExcel();//ydx 导出
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        public MIS_SiteCategotySetting()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MIS_SiteCategotySetting_Load(object sender, EventArgs e)
        {

            dgvinfo.DataSource = siteDal.SiteCategorySelect();
            dgvinfo.DefaultCellStyle.Font = new Font("Arial", 10);
            dgvinfo.Font = new Font("Arial", 10);
            dgvinfo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dgvinfo.ColumnHeadersDefaultCellStyle.Font = new Font(dgvinfo.Font, FontStyle.Bold);

        }
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            dgvinfo.ColumnHeadersDefaultCellStyle.Font = new Font(dgvinfo.Font, FontStyle.Bold);
            try
            {
                DataTable dtExis = siteDal.SiteCategorySelect(txtSite1.Text.ToString().Trim(), txtSite2.Text.ToString().Trim(), txtSiteName.Text.ToString().Trim());
                if (txtSite1.Text.ToString().Trim() == "" || txtSite2.Text.ToString().Trim() == "" || txtSiteName.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("有空白项，请填写完整");

                }
                else
                {
                    if (dtExis.Rows.Count > 0)
                    {
                        MessageBox.Show("已经存在此记录:" + txtSite1.Text);
                    }
                    else
                    {
                        if (siteDal.SiteCategoryInsert(txtSite1.Text.ToString().Trim(), txtSite2.Text.ToString().Trim(), txtSiteName.Text.ToString().Trim()) == 1)
                        {
                            MessageBox.Show("添加成功");
                            txtSite1.Text = "";
                            txtSite2.Text = "";
                            txtSiteName.Text = "";
                            int result = logDal.OpertionLogInsert(userid, "MIS_SiteCategotySetting添加操作", DateTime.Now.ToString(), "MIS_SiteCategotySetting添加成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("添加失败");
                            int result = logDal.OpertionLogInsert(userid, "MIS_SiteCategotySetting添加操作", DateTime.Now.ToString(), "MIS_SiteCategotySetting添加失败");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常" + ex.Message);
            }
            finally
            {
                this.dgvinfo.DataSource = siteDal.SiteCategorySelect();
            }
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_select_Click(object sender, EventArgs e)
        {
            dgvinfo.ColumnHeadersDefaultCellStyle.Font = new Font(dgvinfo.Font, FontStyle.Bold);
            try
            {
                if (textSite1.Text.ToString().Trim() == "" && textSite2.Text.ToString().Trim() == "" && textSiteName.Text.ToString().Trim() == "")
                {
                    dgvinfo.DataSource = siteDal.SiteCategorySelect();

                }
                else
                {
                    dgvinfo.DataSource = siteDal.SiteCategorySelect(textSite1.Text.ToString().Trim(), textSite2.Text.ToString().Trim(), textSiteName.Text.ToString().Trim());

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败！" + ex.ToString());
                int result = logDal.OpertionLogInsert(userid, "MIS_SiteCategotySetting查询操作", DateTime.Now.ToString(), "MIS_SiteCategotySetting查询失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row = dgvinfo.SelectedRows.Count;
            if (row == 0)
            {
                MessageBox.Show("没有选中任何行", "Error");
                return;
            }
            else if (MessageBox.Show("确认删除选中的" + row.ToString() + "条记录吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = dgvinfo.SelectedRows.Count; i > 0; i--)
                {
                    int rno = dgvinfo.SelectedRows[i - 1].Index;
                    if (Convert.ToBoolean(dgvinfo.Rows[rno].Cells[0].Value) == true)//全选的时候 删除最后一行会报错 如果不添加这行代码
                    {
                        int id = Convert.ToInt32(dgvinfo.Rows[rno].Cells[0].Value.ToString());
                        if (siteDal.SiteCategoryDelete(id) == 1)
                        {
                            dgvinfo.Rows.RemoveAt(rno);
                            int result = logDal.OpertionLogInsert(userid, "MIS_SiteCategotySetting删除操作", DateTime.Now.ToString(), "MIS_SiteCategotySetting删除成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("操作失败");
                            int result = logDal.OpertionLogInsert(userid, "MIS_SiteCategotySetting删除操作", DateTime.Now.ToString(), "MIS_SiteCategotySetting删除失败");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                    }
                }
            }
        }

        public static int ID;
        /// <summary>
        /// 导出按钮 2014-07-31
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_import_Click(object sender, EventArgs e)
        {
            #region NPOI导出d
            DataTable dt = (DataTable)dgvinfo.DataSource;
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据可导出");
                int result = logDal.OpertionLogInsert(userid, "MIS_SiteCategotySetting导出操作", DateTime.Now.ToString(), "MIS_SiteCategotySetting没有数据可导出");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {
                ImportToExcel.Export(dt, "MIS_SiteCategotySettingReport", "MIS_SiteCategotySettingReport", "", DateTime.Now.ToString("yyyy-MM--dd"));
                int result = logDal.OpertionLogInsert(userid, "MIS_SiteCategotySetting导出操作", DateTime.Now.ToString(), "MIS_SiteCategotySetting导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }


            #endregion

            #region 修改原来的导出
            //    if (imp.toExcel(dgvinfo, "SiteCategorySetting") == true)
            //    {
            //        MessageBox.Show("导出成功");
            //        int result = logDal.OpertionLogInsert(userid, "MIS_SiteCategotySetting导出操作", DateTime.Now.ToString(), "MIS_SiteCategotySetting导出成功");
            //        if (result < 0)
            //        {
            //            MessageBox.Show("日志插入失败！");
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("没有数据可导出");
            //        int result = logDal.OpertionLogInsert(userid, "MIS_SiteCategotySetting导出操作", DateTime.Now.ToString(), "MIS_SiteCategotySetting没有数据可导出");
            //        if (result < 0)
            //        {
            //            MessageBox.Show("日志插入失败！");
            //            return;
            //        }

            //    }

            #endregion
        }
        /// <summary>
        /// 选中删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvinfo_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                dgvinfo.Rows[e.RowIndex].Selected = true;
            }
        }
        /// <summary>
        /// 双击 弹出修改页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvinfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dgvinfo.CurrentRow.Cells[0].Value.ToString());
            UpdateSiteCategory up = new UpdateSiteCategory();
            up.Owner = this;
            up.ShowDialog();
            dgvinfo.DataSource = siteDal.SiteCategorySelect();
        }
    }
}

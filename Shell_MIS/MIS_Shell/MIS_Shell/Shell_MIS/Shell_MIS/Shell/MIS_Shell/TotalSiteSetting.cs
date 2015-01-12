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
    /// 添加时间：2014-08-28
    /// 添加目的：油站开放 、关闭数据管理
    /// </summary>
    public partial class TotalSiteSetting : Form
    {
        JVDal jvDal = new JVDal();//用于绑定下拉类表的COcd下拉列表
        TotalSiteDAL totalSiteDal = new TotalSiteDAL();//油站开放、关闭数据管理
        ImportToExcel imp = new ImportToExcel();//导出
        OptionLogDAL logDal = new OptionLogDAL();//操作日志
        int UserID = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户ID

        ToolTip tt = null;
        DataTable dt = new DataTable();
        public TotalSiteSetting()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 绑定指定文件 要导入、导出的文件
        /// </summary>
        private void bind(string fileName)
        {
            string strConn = " Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [Total Site Number$]", strConn);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                dt = ds.Tables[0];
                this.textPath.Text = fileName.ToString().Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败！" + ex.Message);
            }
        }
        /// <summary>
        ///导入的过程中循环插入数据
        /// </summary>
        /// <param name="dr"></param>
        private int insertToSql(DataRow dr)
        {
            return totalSiteDal.TotalSiteInsert(dr["Period"].ToString(), dr["Cocd"].ToString(), dr["CoName EN"].ToString(), dr["T3Code"].ToString(), Convert.ToInt32(dr["Total Site in Operation"].ToString()), Convert.ToInt32(dr["No# of Temporary Closed Sites"].ToString()), Convert.ToInt32(dr["No# of New Opened Sites"].ToString()), Convert.ToInt32(dr["No# of closed Sites"].ToString()), Convert.ToInt32(dr["No# of New Secured Sites"].ToString()), Convert.ToInt32(dr["Unopened"].ToString()));

        }
        /// <summary>
        ///【操作】 打开文件夹 浏览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_open_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string fileName = fd.FileName;
                bind(fileName);
            }
        }
        /// <summary>
        /// 【操作】导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_import_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            DataTable dtCh = null;
            if (dt.Rows.Count > 0)
            {
                DataRow dr = null;
                dtCh = dt.Clone();
                bool flag = true;
                for (int i = 0; i < dt.Rows.Count; i++)//循环验证是否有重复数据
                {
                    dr = dt.Rows[i];
                    DataTable dtExis = null;
                    dtExis = totalSiteDal.TotalSiteSelectS(dr[0].ToString().Trim(), dr[1].ToString().Trim(), dr[3].ToString().Trim(), "", "", "", "", "", "");
                    if (dtExis != null && dtExis.Rows.Count > 0)//将重复数据导出
                    {
                        dtCh.ImportRow(dr);
                        flag = false;
                    }
                }
                if (flag == false)
                {
                    MessageBox.Show("有重复数据，将重复数据保存到以下位置！");
                    if (dtCh.Rows.Count > 0)//将重复数据导出到Excel中
                    {
                        if (imp.tableToExcel(dtCh, "Total Site Number重复数据") == true)
                        {
                            MessageBox.Show("已经将重复数据导出！");
                            #region 操作日志记录
                            int result = logDal.OpertionLogInsert(UserID, "TotalSiteNumber 导入", DateTime.Now.ToString(), "已经将重复数据导出");
                            if (result <= 0)
                            {
                                MessageBox.Show("日志插入失败！");

                            }
                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("重复数据导出异常,请联系管理员！");
                            #region 操作日志记录
                            int result = logDal.OpertionLogInsert(UserID, "TotalSiteNumber 导入", DateTime.Now.ToString(), "重复数据导出异常,请联系管理员");
                            if (result <= 0)
                            {
                                MessageBox.Show("日志插入失败！");

                            }
                            #endregion
                        }
                    }
                }
                else
                {
                    //没有重复数据 直接导入
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        insertToSql(dr);
                    }
                }
            }
            else
            {
                MessageBox.Show("没有数据");
                #region 操作日志记录
                int result = logDal.OpertionLogInsert(UserID, "TotalSiteNumber 导入", DateTime.Now.ToString(), "导入文件中没有数据");
                if (result <= 0)
                {
                    MessageBox.Show("日志插入失败！");

                }
                #endregion
            }
            this.dataGridView1.DataSource = totalSiteDal.TotalSiteSelect();
        }
        /// <summary>
        /// 【操作】导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_export_Click(object sender, EventArgs e)
        {
            #region npoi导出
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据可导出");
                #region 操作日志记录
                int result = logDal.OpertionLogInsert(UserID, "TotalSiteNumber 导出", DateTime.Now.ToString(), "没有数据可导出");
                if (result <= 0)
                {
                    MessageBox.Show("日志插入失败！");

                }
                #endregion
            }
            else
            {
                ImportToExcel.Export(dt, "TotalSiteSetting Report", "TotalSiteSetting Report", "", DateTime.Now.ToString());
                #region 操作日志记录
                int result = logDal.OpertionLogInsert(UserID, "TotalSiteNumber 导出", DateTime.Now.ToString(), "导出成功");
                if (result <= 0)
                {
                    MessageBox.Show("日志插入失败！");

                }
                #endregion
            }

            #endregion

            #region 以前的
            //if (imp.toExcel(dataGridView1, "Total Site Number") == true)
            //{
            //    MessageBox.Show("导出成功");
            //    #region 操作日志记录
            //    int result = logDal.OpertionLogInsert(UserID, "TotalSiteNumber 导出", DateTime.Now.ToString(), "导出成功");
            //    if (result <= 0)
            //    {
            //        MessageBox.Show("日志插入失败！");

            //    }
            //    #endregion
            //}
            //else
            //{
            //    MessageBox.Show("没有数据可导出");
            //    #region 操作日志记录
            //    int result = logDal.OpertionLogInsert(UserID, "TotalSiteNumber 导出", DateTime.Now.ToString(), "没有数据可导出");
            //    if (result <= 0)
            //    {
            //        MessageBox.Show("日志插入失败！");

            //    }
            //    #endregion
            //}
            #endregion


        }
        /// <summary>
        /// 【操作】下载模版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Dodn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Excel模版文件到";
            saveFileDialog.FileName = "Total Site Number.xlsx";
            saveFileDialog.ShowDialog();
            string downpath = Application.StartupPath + "\\Model\\Total Site Number.xlsx";
            System.IO.File.Delete(saveFileDialog.FileName);
            System.IO.File.Copy(downpath, saveFileDialog.FileName);
            MessageBox.Show("下载模版成功！");
            #region 操作日志记录
            int result = logDal.OpertionLogInsert(UserID, "TotalSiteNumber 下载模板", DateTime.Now.ToString(), "下载模板成功");
            if (result <= 0)
            {
                MessageBox.Show("日志插入失败！");

            }
            #endregion
        }
        /// <summary>
        /// 【查询】 查询按钮
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_select_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            string perid = textPeriod.Text.ToString().Trim();
            string cocd = comboCocd.SelectedValue.ToString();
            string T3Code = textT3Code.Text.ToString().Trim();
            string TotalSiteinOperation = textsOperation.Text.ToString().Trim();
            string TemporaryClosedSites = textsTempClosed.Text.ToString().Trim();
            string NewOpenedSites = textsNewOpen.Text.ToString().Trim();
            string closedSites = textsClosedSite.Text.ToString().Trim();
            string NewSecuredSites = textsNewSecurd.Text.ToString().Trim();
            string Unopened = textsUnopend.Text.ToString().Trim();
            try
            {
                DataTable dt = null;
                if (perid == "" && cocd == "" && T3Code == "" && TotalSiteinOperation == "" && TemporaryClosedSites == "" && NewOpenedSites == "" && closedSites == "" && NewSecuredSites == "" && Unopened == "")
                {
                    dt = totalSiteDal.TotalSiteSelect();
                }
                else
                {
                    dt = totalSiteDal.TotalSiteSelect(perid, cocd, T3Code, TotalSiteinOperation, TemporaryClosedSites, NewOpenedSites, closedSites, NewSecuredSites, Unopened);
                }
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败！" + ex.Message);
                #region 操作日志记录
                int result = logDal.OpertionLogInsert(UserID, "TotalSiteNumber 查询", DateTime.Now.ToString(), "查询失败");
                if (result <= 0)
                {
                    MessageBox.Show("日志插入失败！");

                }
                #endregion
            }
        }
        /// <summary>
        /// 【增加】 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_add_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            string perid = textPeriod.Text.ToString().Trim();
            //if (comboCocd.SelectedItem.ToString() != "System.Data.DataRowView" && comboCocd.SelectedItem.ToString() == "")
            if (comboCocd.SelectedValue.ToString()=="0")
            {
                MessageBox.Show("请选择合资公司！");
                return;
            }
                string cocd = comboCocd.SelectedValue.ToString();

                string CoNameEn = comboCocd.Text.Trim();
                if (comboCocd.Text.IndexOf(" ") > 0)
                {
                    CoNameEn = CoNameEn.Substring(CoNameEn.IndexOf(" ") + 1);
                }
                string T3Code = textT3Code.Text.ToString().Trim();
                int TotalSiteinOperation = 0;
                if (textTotalSite.Text.ToString().Trim() != "")
                {
                    TotalSiteinOperation = Convert.ToInt32(textTotalSite.Text.ToString().Trim());
                }
                int TemporaryClosedSites = 0;
                if (textTempClosedSite.Text.ToString().Trim() != "")
                {
                    TemporaryClosedSites = Convert.ToInt32(textTempClosedSite.Text.ToString().Trim());
                }
                int NewOpenedSites = 0;
                if (textNewOpenSite.Text.ToString().Trim() != "")
                {
                    NewOpenedSites = Convert.ToInt32(textNewOpenSite.Text.ToString().Trim());
                }
                int closedSites = 0;
                if (textCloseSite.Text.ToString().Trim() != "")
                {
                    closedSites = Convert.ToInt32(textCloseSite.Text.ToString().Trim());
                }
                int NewSecuredSites = 0;
                if (textSecuredSite.Text.ToString().Trim() != "")
                {
                    NewSecuredSites = Convert.ToInt32(textSecuredSite.Text.ToString().Trim());
                }
                int Unopened = 0;
                if (textUnopend.Text.ToString().Trim() != "")
                {
                    Unopened = Convert.ToInt32(textUnopend.Text.ToString().Trim());
                }
                if (perid == "" || cocd == "" || T3Code == "")
                {
                    MessageBox.Show("有空白项，请填写完整");
                }
                else
                {
                    DataTable dtExists = totalSiteDal.TotalSiteSelectS(perid, cocd, T3Code, "", "", "", "", "", "");// totalSiteDal.TotalSiteSelect(perid, cocd, T3Code, TotalSiteinOperation, TemporaryClosedSites, NewOpenedSites, closedSites, NewSecuredSites, Unopened);
                    if (dtExists.Rows.Count > 0)
                    {
                        MessageBox.Show("此记录已存在");
                        #region 操作日志记录
                        int result = logDal.OpertionLogInsert(UserID, "TotalSiteNumber 添加操作", DateTime.Now.ToString(), "记录已存在");
                        if (result <= 0)
                        {
                            MessageBox.Show("日志插入失败！");

                        }
                        #endregion
                    }
                    else
                    {
                        int totalSiteId = totalSiteDal.TotalSiteInsert(perid, cocd, CoNameEn, T3Code, TotalSiteinOperation, TemporaryClosedSites, NewOpenedSites, closedSites, NewSecuredSites, Unopened);
                        if (totalSiteId > 0)
                        {
                            MessageBox.Show("添加成功！");
                            textPeriod.Clear();
                            comboCocd.SelectedItem = "请选择";
                            textT3Code.Clear();
                            textTotalSite.Clear();
                            textTempClosedSite.Clear();
                            textNewOpenSite.Clear();
                            textCloseSite.Clear();
                            textSecuredSite.Clear();
                            textUnopend.Clear();
                            #region 操作日志记录
                            int result = logDal.OpertionLogInsert(UserID, "TotalSiteNumber 添加操作", DateTime.Now.ToString(), "添加 "+cocd+" 成功");
                            if (result <= 0)
                            {
                                MessageBox.Show("日志插入失败！");

                            }
                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("添加失败");
                            #region 操作日志记录
                            int result = logDal.OpertionLogInsert(UserID, "TotalSiteNumber 添加", DateTime.Now.ToString(), "添加 " + cocd + " 成功");
                            if (result <= 0)
                            {
                                MessageBox.Show("日志插入失败！");

                            }
                            #endregion
                        }
                        dataGridView1.DataSource = totalSiteDal.TotalSiteSelect();
                    }
                }
            
         
        }
        /// <summary>
        /// 窗体加载  
        /// 【添加】cocd绑定
        /// 【修改】code绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalSiteSetting_Load(object sender, EventArgs e)
        {
      
            dataGridView1.Font = new System.Drawing.Font("Arial", 10);
            //【添加】下拉列表绑定
            DataTable tableAdd = new DataTable();
            tableAdd = jvDal.JVCocd();
            DataRow dr = tableAdd.NewRow();
            dr["CoCd"] = "0";
            dr["cdNameEn"] = "请选择";
            tableAdd.Rows.InsertAt(dr, 0);

            comboCocd.DataSource = tableAdd.DefaultView;
            comboCocd.DisplayMember = "cdNameEn";
            comboCocd.ValueMember = "CoCd";
            comboCocd.SelectedItem = "请选择";


            //comboCocd.DrawMode = DrawMode.OwnerDrawFixed;        

            //comboCocd.DrawItem += new DrawItemEventHandler(com_DrawITem);
            //comboCocd.DropDownClosed += new EventHandler(cb_DropDownClosed);
            //tt = new ToolTip();
            //tt.SetToolTip(comboCocd, "zj");

            //【修改】下拉列表绑定
            DataTable tableSelect = new DataTable();
            tableSelect = jvDal.JVCocd();
            DataRow drs = tableSelect.NewRow();
            drs["CoCd"] = "0";
            drs["cdNameEn"] = "请选择";
            tableSelect.Rows.InsertAt(drs, 0);
            combosCocd.DataSource = tableSelect.DefaultView;
            combosCocd.DisplayMember = "cdNameEn";
            combosCocd.ValueMember = "CoCd";
            combosCocd.SelectedItem = "请选择";
            dataGridView1.DataSource = totalSiteDal.TotalSiteSelect();//获取所有信息
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);

        }
        /// <summary>
        /// 删除 功能
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
                    if (Convert.ToBoolean(dataGridView1.Rows[rno].Cells[0].Value) == true)//全选的时候 删除最后一行会报错 如果不添加这行代码
                    {

                        int id = Convert.ToInt32(dataGridView1.Rows[rno].Cells[0].Value.ToString());
                        if (totalSiteDal.TotalSiteDelete(id.ToString().Trim()) == 1)
                        {
                            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);
                        }
                        else
                        {
                            MessageBox.Show("操作失败");
                            #region 操作日志记录
                            int result = logDal.OpertionLogInsert(UserID, "TotalSiteSetting 删除", DateTime.Now.ToString(), "操作失败");
                            if (result <= 0)
                            {
                                MessageBox.Show("日志插入失败！");

                            }
                            #endregion
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 单元格鼠标抬起事件  与【删除】相关
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
        public static int ID;
        /// <summary>
        /// 弹出 修改页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            UpdateTotalSiteNumber updateTotalSite = new UpdateTotalSiteNumber();
            updateTotalSite.Owner = this;
            updateTotalSite.ShowDialog();
            this.dataGridView1.DataSource = totalSiteDal.TotalSiteSelect();
        }
        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || dataGridView1.Rows.Count <= 0) return;
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? string.Empty).ToString();


        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        

        //public void com_DrawITem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        //{

        //    // 绘制背景
        //    e.DrawBackground();
        //    //绘制列表项目
        //    e.Graphics.DrawString(comboCocd.Items[e.Index].ToString(), e.Font, System.Drawing.Brushes.Black, e.Bounds);
        //    //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
        //    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
        //        tt.Show(comboCocd.Items[e.Index].ToString(), comboCocd, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
        //    e.DrawFocusRectangle();
        //}
        //void cb_DropDownClosed(object sender, EventArgs e)
        //{
        //    tt.Hide(comboCocd);
        //}
    }
}

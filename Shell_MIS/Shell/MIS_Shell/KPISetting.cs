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
    /// 添加时间：2014-07-28
    /// 添加目的：kpi字典管理
    /// </summary>
    public partial class KPISetting : Form
    {
        KPIDAL kpiDal = new KPIDAL();
        DataTable dt = new DataTable();
        ImportToExcel imp = new ImportToExcel();//数据导出 ydx  2014-08-25
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志

        public KPISetting()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载 2014-07-28
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KPISetting_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = kpiDal.KPISettingSelect();
            this.dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            //dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.Font = new System.Drawing.Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
        }
        /// <summary>
        /// 绑定指定文件 要导入、导出的文件
        /// </summary>
        private void bind(string fileName)
        {
            string strConn = " Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [KPI Mapping$]", strConn);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                dt = ds.Tables[0];
                this.textPath.Text = fileName.ToString().Trim();
                int result = logDal.OpertionLogInsert(userid, "KPISetting绑定指定文件", DateTime.Now.ToString(), "KPISetting绑定指定文件成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败！" + ex.Message);
                int result = logDal.OpertionLogInsert(userid, "KPISetting绑定指定文件", DateTime.Now.ToString(), "KPISetting绑定指定文件失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
        /// <summary>
        /// 打开Excel文件
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
        ///导入的过程中循环插入数据
        /// </summary>
        /// <param name="dr"></param>
        private int insertToSql(DataRow dr)
        {
            return kpiDal.KPISettingInsert(dr["Item"].ToString(), dr["Item Description"].ToString(), dr["Code"].ToString(), dr["Code Description"].ToString(), dr["Report Group"].ToString(), dr["ReportType"].ToString(), dr["ReportSubType"].ToString(), dr["AccountCode"].ToString(), dr["T2"].ToString(), dr["T5"].ToString());
        }
        /// <summary>
        /// 添加 按钮 2014-07-28
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_add_Click(object sender, EventArgs e)
        {
            string Item = textItem.Text.ToString().Trim();
            string ItemDescription = textItemDesc.Text.ToString().Trim();
            string Code = textCode.Text.ToString().Trim();
            string CodeDescription = textCodeDesc.Text.ToString().Trim();
            string ReportGroup = textReportGroup.Text.ToString().Trim();
            string ReportType = textReportType.Text.ToString().Trim();
            string ReportSubType = textReportSubType.Text.ToString().Trim();
            string AccountCode = textAccountCode.Text.ToString().Trim();
            string T2 = textT2.Text.ToString().Trim();
            string T5 = textT5.Text.ToString().Trim();
            //黄晓艳 修改判断所有文本框都为空 2014/11/25
            if (Item == "" && ItemDescription == "" && Code == "" && CodeDescription == "" && ReportGroup == "" && ReportType == "" && ReportSubType == "" && AccountCode == "" && T2 == "" && T5 == "")
            {
                MessageBox.Show("您没有填写添加的信息！");
                return;
            }
            DataTable dtExists = kpiDal.KPISettingSelect(Item, ItemDescription, Code, CodeDescription, ReportGroup, ReportType, ReportSubType, AccountCode, T2, T5);
            if (dtExists.Rows.Count > 0)
            {
                MessageBox.Show("已经存在Item为：" + Item + "的记录");

                int result = logDal.OpertionLogInsert(userid, "KPISetting添加操作", DateTime.Now.ToString(), "KPISetting已存在这条记录");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {
                int kpiId = kpiDal.KPISettingInsert(Item, ItemDescription, Code, CodeDescription, ReportGroup, ReportType, ReportSubType, AccountCode, T2, T5);
                if (kpiId > 0)
                {
                    if (T2 != "" && T5 == "")
                    {
                        //获得t2
                        string[] arr = T2.Split(',');
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if (kpiDal.kpiTempInsert(kpiId.ToString().Trim(), arr[i].ToString().Trim(), "") != 1)
                            {
                                MessageBox.Show("有异常");
                            }
                        }
                    }
                    else if (T2 == "" && T5 != "" && T5 != "R*")
                    {
                        //获得t5
                        string[] strT5 = T5.Split(',');
                        for (int i = 0; i < strT5.Length; i++)
                        {
                            if (kpiDal.kpiTempInsert(kpiId.ToString().Trim(), "", strT5[i].ToString().Trim()) != 1)
                            {
                                MessageBox.Show("有异常");
                            }
                        }
                    }
                    MessageBox.Show("添加成功！");
                    textItem.Clear();
                    textItemDesc.Clear();
                    textCode.Text = "";
                    textCodeDesc.Text = "";
                    textReportGroup.Text = "";
                    textReportType.Text = "";
                    textReportSubType.Text = "";
                    textAccountCode.Text = "";
                    textT2.Text = "";
                    textT5.Text = "";
                    int result = logDal.OpertionLogInsert(userid, "KPISetting添加操作", DateTime.Now.ToString(), "KPISetting添加成功");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("添加失败");
                    int result = logDal.OpertionLogInsert(userid, "KPISetting添加操作", DateTime.Now.ToString(), "KPISettin添加失败");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
                dataGridView1.DataSource = kpiDal.KPISettingSelect();
            }
        }
        /// <summary>
        /// 导入按钮  2014-07-28
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_import_Click(object sender, EventArgs e)
        {
            DataTable dtCh = null;
            if (dt.Rows.Count > 0)
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                DataRow dr = null;
                dtCh = dt.Clone();
                bool flag = true;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    DataTable dtExis = null;
                    dtExis = kpiDal.KPISettingSelect(dr[0].ToString().Trim(), dr[1].ToString().Trim(), dr[2].ToString().Trim(), dr[3].ToString().Trim(), dr[4].ToString().Trim(), dr[5].ToString().Trim(), dr[6].ToString().Trim(), dr[7].ToString().Trim(), dr[8].ToString().Trim(), dr[9].ToString().Trim());
                    if (dtExis != null && dtExis.Rows.Count > 0)
                    {
                        dtCh.ImportRow(dr);
                        flag = false;
                    }
                }
                if (flag == false)
                {
                    MessageBox.Show("有重复数据，将重复数据保存到以下位置！");
                    if (dtCh.Rows.Count > 0)
                    {
                        // this.dataGriCh.DataSource = dtCh;
                        if (imp.tableToExcel(dtCh, "KPI Mapping重复数据") == true)
                        {
                            MessageBox.Show("已经将重复数据导出！");
                            int result = logDal.OpertionLogInsert(userid, "KPISetting导入操作", DateTime.Now.ToString(), "KPISetting已经将重复数据导出");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("重复数据导出异常！");
                            int result = logDal.OpertionLogInsert(userid, "KPISetting导入操作", DateTime.Now.ToString(), "KPISetting重复数据导出异常");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        int kpid = insertToSql(dr);
                        if (kpid > 0)
                        {
                            if (dr[8].ToString().Trim() != "" && dr[9].ToString().Trim() == "")
                            {
                                //获得t2
                                string[] arr = dr[8].ToString().Trim().Split(',');
                                for (int k = 0; k < arr.Length; k++)
                                {
                                    if (kpiDal.kpiTempInsert(kpid.ToString().Trim(), arr[k].ToString().Trim(), "") != 1)
                                    {
                                        MessageBox.Show("有异常");
                                    }
                                }
                            }
                            else if (dr[8].ToString().Trim() == "" && dr[9].ToString().Trim() != "" && dr[9].ToString().Trim() != "R*")
                            {
                                //获得t5
                                string[] strT5 = dr[9].ToString().Trim().Split(',');
                                for (int j = 0; j < strT5.Length; j++)
                                {
                                    if (kpiDal.kpiTempInsert(kpid.ToString().Trim(), "", strT5[j].ToString().Trim()) != 1)
                                    {
                                        MessageBox.Show("有异常");
                                    }
                                }

                            }
                        }
                    }
                }
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
            else
            {
                MessageBox.Show("没有数据");
                int result = logDal.OpertionLogInsert(userid, "KPISetting导入操作", DateTime.Now.ToString(), "KPISetting没有数据可以导入");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            this.dataGridView1.DataSource = kpiDal.KPISettingSelect();
        }

        /// <summary>
        /// 导出按钮 2014-07-28
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_export_Click(object sender, EventArgs e)
        {
            #region npoi 导出excel
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("请先在该表中导入数据！");
                int result = logDal.OpertionLogInsert(userid, "KPISetting导出操作", DateTime.Now.ToString(), "KPISetting导出操作，先在该表中导入数据");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {
                ImportToExcel.Export(dt, "KPISetting Report", "KPISetting Report", "", DateTime.Now.ToString());
                int result = logDal.OpertionLogInsert(userid, "KPISetting导出操作", DateTime.Now.ToString(), "KPISetting导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            #endregion


            //if (imp.toExcel(dataGridView1, "KPI Mapping") == true)
            //{
            //    MessageBox.Show("导出成功");
            //    int result = logDal.OpertionLogInsert(userid, "KPISetting导出操作", DateTime.Now.ToString(), "KPISetting导出操作成功");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("没有数据可导出");
            //    int result = logDal.OpertionLogInsert(userid, "KPISetting导出操作", DateTime.Now.ToString(), "KPISetting没有数据可导出");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}

        }
        /// <summary>
        /// 查询 2014-07-28
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_select_Click(object sender, EventArgs e)
        {
            string Item = txtItem.Text.ToString().Trim();
            string ItemDescription = txtItemDesc.Text.ToString().Trim();
            string Code = txtCode.Text.ToString().Trim();
            string CodeDescription = txtCodeDesc.Text.ToString().Trim();
            string ReportGroup = txtReportGroup.Text.ToString().Trim();
            string ReportType = txtReportType.Text.ToString().Trim();
            string ReportSubType = txtReportSubType.Text.ToString().Trim();
            string AccountCode = txtAccountCode.Text.ToString().Trim();
            string t2 = txtT2.Text.ToString().Trim();
            string t5 = txtT5.Text.ToString().Trim();
            try
            {
                DataTable dt = null;
                if (Item == "" && ItemDescription == "" && Code == "" && CodeDescription == "" && ReportGroup == "" && ReportType == "" && ReportSubType == "" && AccountCode == "" && t2 == "" && t5 == "")
                {
                    dt = kpiDal.KPISettingSelect();
                }
                else
                {

                    dt = kpiDal.KPISettingSelect(Item, ItemDescription, Code, CodeDescription, ReportGroup, ReportType, ReportSubType, AccountCode, t2, t5);
                }
                dataGridView1.DataSource = dt;
                int result = logDal.OpertionLogInsert(userid, "KPISetting查询操作", DateTime.Now.ToString(), "KPISetting查询成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败！" + ex.Message);
                int result = logDal.OpertionLogInsert(userid, "KPISetting查询操作", DateTime.Now.ToString(), "KPISetting查询失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }

        /// <summary>
        /// 删除 2014-07-28
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
                        if (kpiDal.deleteKPISetting(id) == 1)
                        {
                            kpiDal.deleteKPItemp(id.ToString());//将临时表里与之相关的条目删除 2014-08-07
                            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);
                            int result = logDal.OpertionLogInsert(userid, "KPISetting删除操作", DateTime.Now.ToString(), "KPISetting删除成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("操作失败");
                            int result = logDal.OpertionLogInsert(userid, "KPISetting删除操作", DateTime.Now.ToString(), "KPISetting删除失败");
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
        /// 双击 弹出修改页面  当修改完成又跳会本页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            UpdateKPI upKpi = new UpdateKPI();
            upKpi.Owner = this;
            upKpi.ShowDialog();
            dataGridView1.DataSource = kpiDal.KPISettingSelect();
        }
        /// <summary>
        /// 鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseUp_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                this.contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-08-01
        /// 添加目的：模版下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Dodn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Excel模版文件到";
            saveFileDialog.FileName = "KPI Mapping.xlsx";
            saveFileDialog.ShowDialog();
            string downpath = Application.StartupPath + "\\Model\\KPI Mapping.xlsx";
            System.IO.File.Delete(saveFileDialog.FileName);
            System.IO.File.Copy(downpath, saveFileDialog.FileName);
            MessageBox.Show("下载模版成功！");
            int result = logDal.OpertionLogInsert(userid, "KPISetting导出Excel模板操作", DateTime.Now.ToString(), "KPISetting导出Excel模板成功");
            if (result < 0)
            {
                MessageBox.Show("日志插入失败！");
                return;
            }
        }


    }
}

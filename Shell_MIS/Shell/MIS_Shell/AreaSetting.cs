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
    public partial class AreaSetting : Form
    {
        public AreaSetting()
        {
            InitializeComponent();
        }
        AreaDal areadal = new AreaDal();
        JVDal jvdal = new JVDal();//公司代码绑定到下拉列表
        DataTable dt = new DataTable();
        ImportToExcel imp = new ImportToExcel();//导出数据 2014-08-25 ydx
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志

        private void AreaSetting_Load(object sender, EventArgs e)
        {
            //添加面板
            //公司代码
            DataTable dt = new DataTable();
            dt = jvdal.JVCocd();
            //dt = jvdal.JVSelectImport();
            DataRow dr = dt.NewRow();
            dr["CoCd"] = "请选择";
            dr["cdNameEn"] = "请选择";
            dt.Rows.InsertAt(dr, 0);
            comboCocd.DataSource = dt;
            comboCocd.DisplayMember = "cdNameEn";
            comboCocd.ValueMember = "CoCd";


            //查询面板
            //公司代码
            dt = jvdal.JVCocd();
            DataRow drc = dt.NewRow();
            drc["CoCd"] = "请选择";
            drc["cdNameEn"] = "请选择";
            dt.Rows.InsertAt(drc, 0);
            cbCocd.DataSource = dt;
            cbCocd.ValueMember = "CoCd";
            cbCocd.DisplayMember = "cdNameEn";

            this.dataGridView2.DataSource = areadal.AreaSelect();
            this.dataGridView2.Font = new Font("Arial", 10);
            this.dataGridView2.Columns[0].DefaultCellStyle.Font = new Font("Arial", 10);
            this.dataGridView2.Columns[1].DefaultCellStyle.Font = new Font("Arial", 10);
            this.dataGridView2.Columns[3].DefaultCellStyle.Font = new Font("Arial", 10);
            this.dataGridView2.Columns[4].DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
        }

        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                dataGridView2.Rows[e.RowIndex].Selected = true;
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row = dataGridView2.SelectedRows.Count;
            if (row == 0)
            {
                MessageBox.Show("没有选中任何行", "Error");
                return;
            }
            else if (MessageBox.Show("确认删除选中的" + row.ToString() + "条记录吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = dataGridView2.SelectedRows.Count; i > 0; i--)
                {
                    int rno = dataGridView2.SelectedRows[i - 1].Index;
                    if (Convert.ToBoolean(dataGridView2.Rows[rno].Cells[0].Value) == true)//全选的时候 删除最后一行会报错 如果不添加这行代码
                    {
                        string id = dataGridView2.Rows[rno].Cells[0].Value.ToString();
                        if (areadal.AreaDelete(id) == 1)
                        {
                            dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[i - 1].Index);
                            int result = logDal.OpertionLogInsert(userid, "AreaSetting删除操作", DateTime.Now.ToString(), "AreaSetting删除成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {

                            MessageBox.Show("操作失败！");
                            int result = logDal.OpertionLogInsert(userid, "AreaSetting删除操作", DateTime.Now.ToString(), "AreaSetting删除失败");
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
        /// <summary>
        /// 添加
        /// 修改人：ydx
        /// 修改时间：2014-07-21
        /// 修改目的：添加公司代码、分组1、分组2字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_Insert_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            if (comboCocd.SelectedValue.ToString().Trim() == "请选择")
            {
                MessageBox.Show("请选择公司代码");
            }

            else if (txt_ACT0.Text.ToString().Trim() == "" || txt_ANCH.Text.ToString().Trim() == "" || txt_ANEN.Text.ToString().Trim() == "")
            {
                MessageBox.Show("有空白项，请填写完整");
            }
            else
            {
                DataTable dtExiste = areadal.AreaSelect(txt_ACT0.Text.ToString().Trim(), txt_ANCH.Text.ToString().Trim(), txt_ANEN.Text.ToString().Trim(), comboCocd.SelectedValue.ToString().Trim());
                if (dtExiste.Rows.Count > 0)
                {
                    MessageBox.Show("已经存在此记录:" + txt_ACT0.Text);
                }
                else
                {
                    string AreaCodeT0 = txt_ACT0.Text.ToString().Trim();
                    string AreaNameCH = txt_ANCH.Text.ToString().Trim();
                    string AreaNameEN = txt_ANEN.Text.ToString().Trim();
                    string CoCd = comboCocd.SelectedValue.ToString().Trim();
                    if (areadal.AreaInsert(AreaCodeT0, AreaNameCH, AreaNameEN, CoCd) == 1)
                    {
                        MessageBox.Show("增加成功！");
                        txt_ACT0.Text = "";
                        txt_ANCH.Text = "";
                        txt_ANEN.Text = "";
                        comboCocd.SelectedValue = "请选择";
                        int result = logDal.OpertionLogInsert(userid, "AreaSetting添加操作", DateTime.Now.ToString(), "AreaSetting添加操作成功");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                        int result = logDal.OpertionLogInsert(userid, "AreaSetting添加操作", DateTime.Now.ToString(), "AreaSetting添加操作失败");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    this.dataGridView2.DataSource = areadal.AreaSelect();
                }

            }

        }
        //查
        /// <summary>
        /// 查询功能
        /// 修改人：ydx
        /// 修改时间：2014-07-21
        /// 修改目的：添加公司代码、分组1、分组2 查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_Select_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            try
            {
                string CoCd = "";
                string AreaCodeT0 = txt_SACT0.Text.ToString().Trim();
                string AreaNameCH = txt_SANCH.Text.ToString().Trim();
                string AreaNameEN = txt_SANEN.Text.ToString().Trim();
                if (cbCocd.SelectedValue.ToString().Trim() != "请选择")
                {
                    CoCd = cbCocd.SelectedValue.ToString().Trim();
                }
                this.dataGridView2.DataSource = areadal.AreaSelect(AreaCodeT0, AreaNameCH, AreaNameEN, CoCd);
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "AreaSetting查询操作", DateTime.Now.ToString(), "AreaSetting查询操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }

        public static int ID;
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            UpdateArea updarea = new UpdateArea();
            updarea.Owner = this;
            updarea.ShowDialog();
            this.dataGridView2.DataSource = areadal.AreaSelect();
        }
        /// <summary>
        /// 导出 按钮 
        /// 添加人：ydx
        /// 添加时间：2014-07-31
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_imp_Click(object sender, EventArgs e)
        {
            #region NPOI数据导出
            dt = (DataTable)dataGridView2.DataSource;
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("请先在该表中导入数据！");
                return;
            }
            else
            {
                ImportToExcel.Export(dt, "AreaSettingReport", "AreaSettingReport", "", DateTime.Now.ToString("yyyy-MM-dd"));
                int result = logDal.OpertionLogInsert(userid, "AreaSetting导出操作", DateTime.Now.ToString(), "AreaSetting导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            #endregion
           


            //if (imp.toExcel(dataGridView2, "Area Setting") == true)
            //{
            //    MessageBox.Show("导出成功！");
            //    int result = logDal.OpertionLogInsert(userid, "AreaSetting导出操作", DateTime.Now.ToString(), "AreaSetting导出成功");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请先在该表中导入数据！");
            //}
            //if (dataGridView2.RowCount - 1 != 0)
            //{
            //    SaveFileDialog saveFileDialog = new SaveFileDialog();
            //    saveFileDialog.Filter = "Execl   files   (*.xls)|*.xls";
            //    saveFileDialog.FilterIndex = 0;
            //    saveFileDialog.RestoreDirectory = true;
            //    saveFileDialog.CreatePrompt = true;
            //    saveFileDialog.Title = "导出Excel文件到";
            //    saveFileDialog.FileName = "Area Setting";
            //    saveFileDialog.ShowDialog();
            //    Stream myStream;
            //    myStream = saveFileDialog.OpenFile();
            //    StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(0));

            //    string str = "";
            //    try
            //    {
            //        //写标题     
            //        for (int i = 0; i < dataGridView2.Columns.Count; i++)
            //        {
            //            if (i > 0)
            //            {
            //                str += "\t";
            //            }
            //            str += dataGridView2.Columns[i].HeaderText;

            //        }

            //        sw.WriteLine(str);
            //        //写内容   
            //        for (int j = 0; j < dataGridView2.RowCount - 1; j++)
            //        {
            //            string tempStr = "";
            //            for (int k = 0; k < dataGridView2.Columns.Count; k++)
            //            {
            //                if (k > 0)
            //                {
            //                    tempStr += "\t";
            //                }
            //                tempStr += dataGridView2[k, j].Value.ToString();
            //            }
            //            sw.WriteLine(tempStr);
            //        }
            //        sw.Close();
            //        myStream.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //    finally
            //    {
            //        sw.Close();
            //        myStream.Close();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请先在该表中导入数据！");
            //}
        }
    }
}

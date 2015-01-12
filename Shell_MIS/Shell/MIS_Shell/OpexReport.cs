using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using DAL;
using MIS_Shell.CommExcel;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;


namespace MIS_Shell
{
    /// <summary>
    /// 添加时间:2014-07-26
    /// 添加人:ydx
    /// 添加目的：费用报告
    /// </summary>
    public partial class OpexReport : Form
    {
        OpexReportDAL importdal = new OpexReportDAL();
        DataTable dt = new DataTable();
        ImportToExcel importEx = new ImportToExcel();//导出事件
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        JVDal jvdal = new JVDal();//合资公司
        public string where = "";//查询条件
        public OpexReport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 导出按钮  2014-07-26
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_import_Click(object sender, EventArgs e)
        {
            //hxy 11-20日修改

            #region npoi 导出excel
            DataTable dt = (DataTable)dataGridView1.DataSource;
            string ReportType = comboTypeChild.GetItemText(comboTypeChild.Items[comboTypeChild.SelectedIndex]);
            string compeny = comboJV.GetItemText(comboJV.Items[comboJV.SelectedIndex]);
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("请先在该表中导入数据！");
                int result = logDal.OpertionLogInsert(userid, "OpexReport" + ReportType + "导出操作", DateTime.Now.ToString(), "OpexReport" + ReportType + "导出操作，先在该表中导入数据");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {
                if (ReportType=="部门油站/期间汇总")
                {
                    ReportType = "部门油站-期间汇总";
                }
                if (ReportType=="费用汇总按部门/油站")
                {
                    ReportType = "费用汇总按部门-油站";
                }
                if (ReportType=="费用明细按部门/油站")
                {
                    ReportType = "费用明细按部门-油站";
                }
                ImportToExcel.Export(dt, "OpexReport(" + ReportType + ")", "OpexReport(" + ReportType + ")", compeny, DateTime.Now.ToString());
                int result = logDal.OpertionLogInsert(userid, "OpexReport" + ReportType + "导出操作", DateTime.Now.ToString(), "OpexReport导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            #endregion         
            //#region

            //      if (importEx.toExcel(dataGridView1, "OPEX Report") == true)
            //{
            //    MessageBox.Show("导出成功！");
            //}
            //else
            //{
            //    MessageBox.Show("请先在该表中导入数据！");
            //}
            //#endregion
            

        }


        /// <summary>
        /// ydx  2014-07-26
        /// </summary>
        //private bool ExportExcel()
        //{
        //    bool flag = false;
        //    if (dataGridView1.RowCount - 1 != 0)
        //    {
        //        SaveFileDialog saveFileDialog = new SaveFileDialog();
        //        saveFileDialog.Filter = "Execl   files   (*.xls)|*.xls";
        //        saveFileDialog.FilterIndex = 0;
        //        saveFileDialog.RestoreDirectory = true;
        //        saveFileDialog.CreatePrompt = true;
        //        saveFileDialog.Title = "导出Excel文件到";
        //        saveFileDialog.FileName = "OPEX Report";
        //        saveFileDialog.ShowDialog();

        //        Stream myStream;
        //        myStream = saveFileDialog.OpenFile();
        //        StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(0));

        //        string str = "";
        //        try
        //        {
        //            //写标题     
        //            for (int i = 0; i < dataGridView1.Columns.Count; i++)
        //            {
        //                if (i > 0)
        //                {
        //                    str += "\t";
        //                }
        //                str += dataGridView1.Columns[i].HeaderText;

        //            }

        //            sw.WriteLine(str);
        //            //写内容   
        //            for (int j = 0; j < dataGridView1.RowCount - 1; j++)
        //            {
        //                string tempStr = "";

        //                for (int k = 0; k < dataGridView1.Columns.Count; k++)
        //                {
        //                    if (k > 0)
        //                    {
        //                        tempStr += "\t";
        //                    }
        //                    tempStr += dataGridView1[k, j].Value.ToString();
        //                }
        //                sw.WriteLine(tempStr);
        //            }
        //            sw.Close();
        //            myStream.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString());
        //        }
        //        finally
        //        {
        //            sw.Close();
        //            myStream.Close();
        //        }
        //        flag = true;
        //    }
        //    else
        //    {
        //        flag = false;

        //    }

        //    return flag;
        //}
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-08-01
        /// 添加目的：第三个mapping
        /// </summary>
        #region Mapping3
        private void bindDataThree()
        {
            string[] obj = new string[dt.Columns.Count];
            double[] dk = new double[dt.Columns.Count];
            double[] dm = new double[dt.Columns.Count];

            //统计所有在最后生成一行 总统计
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    if (k >= 2)
                    {
                        dm[k] += double.Parse(dt.Rows[i][k].ToString());
                    }
                }
            }
            obj[0] = "Total Opex";//总统计 字段
            for (int k = 2; k < dk.Length; k++)
            {
                obj[k] = dm[k].ToString();
            }
            DataRow df = dt.NewRow();
            df.ItemArray = obj;
            dt.Rows.InsertAt(df, dt.Rows.Count);
            //显示小数点后两位
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 2; j < dt.Columns.Count; j++)
                {

                    dt.Columns[j].ReadOnly = false;//聚合函数求出的值的列是只读的 此处必须处理成”可读“
                    dt.Rows[i][j] = double.Parse(dt.Rows[i][j].ToString()).ToString("0.00");
                }
            }
        }
        #endregion

        #region  Mapping 4,5
        /// <summary>
        ///数据转换  2014-07-26
        /// </summary>
        private void bindDataFour()
        {
            string[] obj = new string[dt.Columns.Count];
            double[] dk = new double[dt.Columns.Count];
            double[] dm = new double[dt.Columns.Count];
            string plline = dt.Rows[0]["PLLine"].ToString();

            //统计所有在最后生成一行 总统计
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    if (k >= 2)
                    {
                        dm[k] += double.Parse(dt.Rows[i][k].ToString());
                    }
                }
            }
            obj[0] = "Total Opex";//总统计 字段
            for (int k = 2; k < dk.Length; k++)
            {
                obj[k] = dm[k].ToString();
            }
            DataRow df = dt.NewRow();
            df.ItemArray = obj;

            //分类  统计
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (dt.Rows[i]["PLLine"].ToString() == plline)
                {
                    //生成新序列、
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        if (k >= 2)
                        {
                            dk[k] += double.Parse(dt.Rows[i][k].ToString());
                        }
                    }
                }
                else
                {
                    if (i <= dt.Rows.Count - 1)
                    {
                        plline = dt.Rows[i]["PLLine"].ToString();
                    }
                    obj[0] = dt.Rows[i - 1][0] + " Total";
                    for (int k = 2; k < dk.Length; k++)
                    {
                        obj[k] = dk[k].ToString();
                    }
                    DataRow dr = dt.NewRow();
                    dr.ItemArray = obj;
                    dt.Rows.InsertAt(dr, i);
                    for (int k = 2; k < dk.Length; k++)
                    {
                        dk[k] = 0.0;
                    }

                }

            }
            //判断最后一行是不是就属于 一行一类的情况
            if (dt.Rows[dt.Rows.Count - 1]["PLLine"].ToString() != dt.Rows[dt.Rows.Count - 2]["PLLine"].ToString())
            {
                obj[0] = dt.Rows[dt.Rows.Count - 1][0] + " Total";
                for (int k = 2; k < dk.Length; k++)
                {
                    obj[k] = dk[k].ToString();
                }
                DataRow dr = dt.NewRow();
                dr.ItemArray = obj;
                dt.Rows.InsertAt(dr, dt.Rows.Count);
                for (int k = 2; k < dk.Length; k++)
                {
                    dk[k] = 0.0;
                }

            }
            else
            {
                //不属于 一行一类  2014-11-06
                int p = dt.Rows.Count - 1;
                string type = dt.Rows[p]["PLLine"].ToString().Trim();
                for (int i = p; i > 0; i--)
                {
                    if (type == dt.Rows[p]["PLLine"].ToString().Trim())
                    {
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            if (k >= 2)
                            {
                                dm[k] += double.Parse(dt.Rows[i][k].ToString());
                            }
                        }
                    }
                }
                obj[0] = dt.Rows[dt.Rows.Count - 1][0] + "  Total";
                for (int k = 2; k < dk.Length; k++)
                {
                    obj[k] = dk[k].ToString();
                }
                DataRow dr = dt.NewRow();
                dr.ItemArray = obj;
                dt.Rows.InsertAt(dr, dt.Rows.Count);
                for (int k = 2; k < dk.Length; k++)
                {
                    dk[k] = 0.0;
                }
            }
            //算出的结果 增添到表格的最后一行
            dt.Rows.InsertAt(df, dt.Rows.Count);
            for (int k = 2; k < dk.Length; k++)
            {
                dm[k] = 0.0;
            }
            //显示小数点后两位
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 2; j < dt.Columns.Count; j++)
                {

                    dt.Columns[j].ReadOnly = false;//聚合函数求出的值的列是只读的 此处必须处理成”可读“
                    //dt.Rows[i][j] = double.Parse(dt.Rows[i][j].ToString()).ToString("0.00");
                    //string bb = double.Parse(dt.Rows[i][j].ToString()).ToString("n");
                    // dt.Rows[i][j] = double.Parse(dt.Rows[i][j].ToString()).ToString("n");
                    // string bbd=dt.Rows[i][j].ToString();
                    dt.Rows[i][j] = double.Parse(dt.Rows[i][j].ToString()).ToString();

                }
            }

        }

        #endregion
        /// <summary>
        ///数据转换  2014-07-26  
        /// </summary>
        #region Mapping 1  2
        private void bindData()
        {
            string[] obj = new string[dt.Columns.Count];
            double[] dk = new double[dt.Columns.Count];
            double[] dm = new double[dt.Columns.Count];
            string plline = dt.Rows[0]["PLLine"].ToString();

            //统计所有在最后生成一行 总统计
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    if (k >= 4)
                    {
                        dm[k] += double.Parse(dt.Rows[i][k].ToString());
                    }
                }
            }
            obj[0] = "Total Opex";//总统计 字段
            for (int k = 4; k < dk.Length; k++)
            {
                obj[k] = dm[k].ToString();
            }
            DataRow df = dt.NewRow();
            df.ItemArray = obj;

            //分类  统计
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (dt.Rows[i]["PLLine"].ToString() == plline)
                {
                    //生成新序列、
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        if (k >= 4)
                        {
                            dk[k] += double.Parse(dt.Rows[i][k].ToString());
                        }
                    }
                }
                else
                {
                    if (i <= dt.Rows.Count - 1)
                    {
                        plline = dt.Rows[i]["PLLine"].ToString();
                    }
                    obj[0] = dt.Rows[i - 1][0] + " Total";
                    for (int k = 4; k < dk.Length; k++)
                    {
                        obj[k] = dk[k].ToString();
                    }
                    DataRow dr = dt.NewRow();
                    dr.ItemArray = obj;
                    dt.Rows.InsertAt(dr, i);
                    for (int k = 4; k < dk.Length; k++)
                    {
                        dk[k] = 0.0;
                    }
                }
            }
            //判断最后一行是不是就属于 一行一类的情况
            if (dt.Rows[dt.Rows.Count - 1]["PLLine"].ToString() != dt.Rows[dt.Rows.Count - 2]["PLLine"].ToString())
            {
                obj[0] = dt.Rows[dt.Rows.Count - 1][0] + " Total";
                for (int k = 4; k < dk.Length; k++)
                {
                    obj[k] = dk[k].ToString();
                }
                DataRow dr = dt.NewRow();
                dr.ItemArray = obj;
                dt.Rows.InsertAt(dr, dt.Rows.Count);
                for (int k = 4; k < dk.Length; k++)
                {
                    dk[k] = 0.0;
                }

            }
            else
            {
                //不属于 一行一类  2014-11-06
                int p = dt.Rows.Count - 1;
                string type = dt.Rows[p]["PLLine"].ToString().Trim();
                for (int i = p; i > 0; i--)
                {
                    if (type == dt.Rows[p]["PLLine"].ToString().Trim())
                    {
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            if (k >= 4)
                            {
                                dm[k] += double.Parse(dt.Rows[i][k].ToString());
                            }
                        }
                    }
                }
                obj[0] = dt.Rows[dt.Rows.Count - 1][0] + "  Total";
                for (int k = 4; k < dk.Length; k++)
                {
                    obj[k] = dk[k].ToString();
                }
                DataRow dr = dt.NewRow();
                dr.ItemArray = obj;
                dt.Rows.InsertAt(dr, dt.Rows.Count);
                for (int k = 4; k < dk.Length; k++)
                {
                    dk[k] = 0.0;
                }
            }
            //算出的结果 增添到表格的最后一行
            dt.Rows.InsertAt(df, dt.Rows.Count);
            for (int k = 4; k < dk.Length; k++)
            {
                dm[k] = 0.0;
            }

            //显示小数点后两位
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 4; j < dt.Columns.Count; j++)
                {

                    dt.Columns[j].ReadOnly = false;//聚合函数求出的值的列是只读的 此处必须处理成”可读“
                    dt.Rows[i][j] = double.Parse(dt.Rows[i][j].ToString()).ToString("0.00");
                }
            }
        }
        #endregion
        /// <summary>
        /// 窗体加载   2014-07-26
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpexReport_Load(object sender, EventArgs e)
        {

            dataGridView1.Font = new Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //报告格式
            this.comboType.SelectedItem = "请选择";
            this.dateTimeStar.CustomFormat = "yyyy-MM";
            this.dateTimeEnd.CustomFormat = "yyyy-MM";

            this.comboJV.SelectedText = "请选择";
            DataTable dt = new DataTable();
            dt = jvdal.JVCocd();
            DataRow dr = dt.NewRow();
            dr["CoCd"] = "请选择";
            dr["cdNameEn"] = "请选择";
            dt.Rows.InsertAt(dr, 0);
            comboJV.DataSource = dt;
            comboJV.DisplayMember = "cdNameEn";
            comboJV.ValueMember = "CoCd";

            // dt = importdal.ImportOPEX();//获得数据在此datatable里
            // bindData();
            // dataGridView1.DataSource = dt;
        }

        public string cocd = "";
        /// <summary>
        /// 选择不同条件  2014-07-26
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string starTime = dateTimeStar.Text.ToString();
            //string endTime = dateTimeEnd.Text.ToString();
            //if(comboJV.Text=="请选择")
            //{
            //    dt = new DataTable();
            //    dataGridView1.DataSource = dt;
            //    MessageBox.Show("请选择条件");
            //}else if(comboJV.Text=="按部门")
            //{
            //    dt = importdal.ImportOPEX(starTime,endTime);
            //    if (dt.Rows.Count > 0)
            //    {
            //        bindData();
            //    }
            //    else
            //    {
            //        MessageBox.Show("没有数据");
            //    }
            //}else if(comboJV.Text=="按年份")
            //{
            //    dt = importdal.ImportOPE(starTime, endTime);
            //    if (dt.Rows.Count > 0)
            //    {
            //        bindData();
            //    }
            //    else
            //    {
            //        MessageBox.Show("没有数据");
            //    }

            //}else
            //{
            //    dt = importdal.ImportOPEX();//所有数据
            //    if (dt.Rows.Count > 0)
            //    {
            //        bindData();
            //    }
            //    else
            //    {

            //        MessageBox.Show("没有数据");
            //    }
            //}
            //dataGridView1.DataSource = dt;

        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-08-01
        /// 添加目的：添加报告格式  当选择不同报告格式 ，子格式的下拉列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboType.SelectedItem.ToString().Trim() != "System.Data.DataRowView" && comboType.SelectedItem.ToString().Trim() != "")
            {
                if (comboType.SelectedItem.ToString().Trim() == "费用汇总")//4,5 
                {
                    this.comboTypeChild.Items.Clear();
                    this.comboTypeChild.Items.AddRange(new object[] { comboType.SelectedItem.ToString().Trim() + "按期间", comboType.SelectedItem.ToString().Trim() + "按部门/油站" });
                    this.comboTypeChild.SelectedItem = "请选择";
                }
                else if (comboType.SelectedItem.ToString().Trim() == "费用明细")//1,2
                {
                    this.comboTypeChild.Items.Clear();
                    this.comboTypeChild.Items.AddRange(new object[] { comboType.SelectedItem.ToString().Trim() + "按期间", comboType.SelectedItem.ToString().Trim() + "按部门/油站" });
                    this.comboTypeChild.SelectedItem = "请选择";
                }
                else if (comboType.SelectedItem.ToString().Trim() == "部门油站/期间汇总")//3
                {
                    this.comboTypeChild.Items.Clear();
                    this.comboTypeChild.Items.AddRange(new object[] { "部门油站/期间汇总" });
                    this.comboTypeChild.SelectedItem = "请选择";
                }
                else
                {
                    this.comboTypeChild.Items.Clear();
                    this.comboTypeChild.Items.AddRange(new object[] { "请选择" });
                    this.comboTypeChild.SelectedItem = "请选择";
                }
            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-08-01
        /// 添加目的：子 报表格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboTypeChild_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-08-01
        /// 添加目的：子 报表格式
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Select_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            if (comboType.SelectedItem.ToString().Trim() != "System.Data.DataRowView" && comboType.SelectedItem.ToString().Trim() == "请选择")//不同类别的选择条件
            {
                MessageBox.Show("报表格式必须选择");
            }
            else
            {
                if (comboTypeChild.Text.ToString().Trim() == "请选择")//不同类别的选择条件
                {
                    MessageBox.Show("报表格式详细必须选择");
                }
                else
                {
                    if (comboJV.SelectedValue.ToString().Trim() != "请选择")//合资公司的搜索条件
                    {
                        if (MIS_T5.staticT5 == "" && MIS_T5.staticT8 == "")
                        {
                            where = " and tp.CoCd=''" + comboJV.SelectedValue.ToString().Trim() + "''";
                        }
                        else if (MIS_T5.staticT5 != "" && MIS_T5.staticT8 == "")
                        {
                            where = " and tp.CoCd=''" + comboJV.SelectedValue.ToString().Trim() + "'' and  tp.T5 in(" + MIS_T5.staticT5 + ") ";
                        }
                        else if (MIS_T5.staticT5 == "" && MIS_T5.staticT8 != "")
                        {
                            where = " and tp.CoCd=''" + comboJV.SelectedValue.ToString().Trim() + "''  and tp.T8 in(" + MIS_T5.staticT8 + ")  ";
                        }
                        else if (MIS_T5.staticT5 != "" && MIS_T5.staticT8 != "")
                        {
                            where = " and tp.CoCd=''" + comboJV.SelectedValue.ToString().Trim() + "'' and  tp.T5 in(" + MIS_T5.staticT5 + " )  and tp.T8 in(" + MIS_T5.staticT8 + ") ";
                        }
                    }
                    else
                    {
                        where = "请选择";//如果合资公司不选择，则显示所有合资公司的
                    }

                    string starTimes = dateTimeStar.Text.ToString().Trim();
                    string endTimes = dateTimeEnd.Text.ToString().Trim();
                    //将时间转换成三位的  月份
                    string starTime = starTimes.Substring(0, 5) + "0" + starTimes.Substring(5, 2);
                    string endTime = endTimes.Substring(0, 5) + "0" + endTimes.Substring(5, 2);
                    if (comboTypeChild.SelectedItem.ToString().Trim() == "费用汇总按期间")//4
                    {
                        dt = importdal.ImportOPETotalByDate(starTime, endTime, where);
                        if (dt.Rows.Count > 0)
                        {
                            bindDataFour();
                        }
                        else
                        {
                            MessageBox.Show("没有数据");
                        }
                    }
                    else if (comboTypeChild.SelectedItem.ToString().Trim() == "费用汇总按部门/油站")//5
                    {
                        dt = importdal.ImportOPETotalByDepart(starTime, endTime, where);
                        if (dt.Rows.Count > 0)
                        {
                            bindDataFour();
                        }
                        else
                        {
                            MessageBox.Show("没有数据");
                        }
                    }
                    else if (comboTypeChild.SelectedItem.ToString().Trim() == "费用明细按期间")//1
                    {
                        dt = importdal.ImportOPE(starTime, endTime, where);
                        if (dt.Rows.Count > 0)
                        {
                            bindData();
                        }
                        else
                        {
                            MessageBox.Show("没有数据");
                        }
                    }
                    else if (comboTypeChild.SelectedItem.ToString().Trim() == "费用明细按部门/油站")//2
                    {
                        dt = importdal.ImportOPEX(starTime, endTime, where);
                        if (dt.Rows.Count > 0)
                        {
                            bindData();
                        }
                        else
                        {
                            MessageBox.Show("没有数据");
                        }
                    }
                    else if (comboTypeChild.SelectedItem.ToString().Trim() == "部门油站/期间汇总")//3
                    {

                        if (where != "请选择")
                        {
                            if (MIS_T5.staticT5 == "" && MIS_T5.staticT8 == "")
                            {
                                where = " and tp.Cocd=''" + comboJV.SelectedValue.ToString().Trim() + "''  and jd.CoCd=tp.Cocd ";
                            }
                            else if (MIS_T5.staticT5 != "" && MIS_T5.staticT8 == "")
                            {
                                where = "  and tp.Cocd=''" + comboJV.SelectedValue.ToString().Trim() + "''  and jd.CoCd=tp.Cocd and  tp.T5 in(" + MIS_T5.staticT5 + ") ";
                            }
                            else if (MIS_T5.staticT5 == "" && MIS_T5.staticT8 != "")
                            {
                                where = " and tp.Cocd=''" + comboJV.SelectedValue.ToString().Trim() + "''  and jd.CoCd=tp.Cocd  and tp.T8 in(" + MIS_T5.staticT8 + ")  ";
                            }
                            else if (MIS_T5.staticT5 != "" && MIS_T5.staticT8 != "")
                            {
                                where = " and tp.Cocd=''" + comboJV.SelectedValue.ToString().Trim() + "'' and jd.CoCd=tp.Cocd and  tp.T5 in(" + MIS_T5.staticT5 + " )  and tp.T8 in(" + MIS_T5.staticT8 + ") ";
                            }
                        }
                        dt = importdal.ImportOPEThree(starTime, endTime, where);
                        if (dt.Rows.Count > 0)
                        {
                            bindDataThree();
                        }
                        else
                        {
                            MessageBox.Show("没有数据");
                        }
                    }
                    else
                    {
                        dt = importdal.ImportOPEX();//所有数据
                        if (dt.Rows.Count > 0)
                        {
                            bindData();
                        }
                        else
                        {
                            MessageBox.Show("没有数据");
                        }
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;
                    dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (comboTypeChild.SelectedItem.ToString().Trim() == "费用汇总按期间" || comboTypeChild.SelectedItem.ToString().Trim() == "部门油站/期间汇总")
                    {
                                                

                                dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                dataGridView1.DefaultCellStyle.Format = "N2";                          

                       
                    }

                    if (comboTypeChild.SelectedItem.ToString().Trim() == "费用明细按期间" || comboTypeChild.SelectedItem.ToString().Trim() == "部门油站/期间汇总")
                    {
                        dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView1.DefaultCellStyle.Format = "N2";   

                        //for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        //{
                        //    if (i >= 4)
                        //    {

                        //        dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        //        dataGridView1.DefaultCellStyle.Format = "N2";
                        //    }

                        //}
                    }
                    if (comboTypeChild.SelectedItem.ToString().Trim() == "费用汇总按部门/油站")
                    {
                        dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        //for (int i = 2; i < dataGridView1.Columns.Count; i++)
                        //{
                            

                        //        dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;           
                           

                        //}
                        dataGridView1.DefaultCellStyle.Format = "N2";

                    }
                    if (comboTypeChild.SelectedItem.ToString().Trim() == "费用明细按部门/油站")
                    {
                        dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                       // for (int i = 4; i < dataGridView1.Columns.Count; i++)
//{
                            
                               // dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                            

                        //}
                        dataGridView1.DefaultCellStyle.Format = "N2";

                    }


                }
            }
        }
        public static string staticCocd = "";
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-27
        /// 添加目的：选择不同合资公司 显示此合资公司下的所有T5，T8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboJV_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.comboJV.SelectedValue.ToString().Trim() != "System.Data.DataRowView" && comboJV.SelectedValue.ToString().Trim() != "请选择")//不同类别的选择条件
            {
                staticCocd = comboJV.SelectedValue.ToString().Trim();
                MIS_T5 t5 = new MIS_T5();
                t5.Owner = this;
                t5.ShowDialog();
                //DataTable dtCheck = tcodeDal.TCodeSelectT8(comboJV.SelectedValue.ToString().Trim());
                //if (dtCheck != null && dtCheck.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtCheck.Rows.Count; i++)
                //    {
                //        checkedListBox.Items.Add(dtCheck.Rows[i][0].ToString().Trim());
                //    }
                //}
            }
        }




    }
}

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

namespace MIS_Shell
{
    /// <summary>
    /// 添加人：ydx
    /// 添加时间：2014-09-26
    /// 添加目的：资产负债表
    /// </summary>
    public partial class BS : Form
    {
        public BS()
        {
            InitializeComponent();
        }
        JVDal jvdal = new JVDal();//合资公司下拉列表
        ImportDal importdal = new ImportDal();
        DataTable dt = new DataTable();
        TCodeDal tcodeDal = new TCodeDal();//T8下拉框
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        string cocd = "";
        string t8 = "";
        string AccountPeriod = "";
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-26
        /// 添加目的：资产负债表 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BS_Load(object sender, EventArgs e)
        {
            dataGridView1.Font = new Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Format = "N2";
            dataGridView1.DefaultCellStyle.Font = new Font("宋体", 10);
            comb_BS.SelectedItem = "请选择";//设置让类别下拉列表默认显示“请选择”
            dt = jvdal.JVSelectImport();
            DataRow dr = dt.NewRow();
            dr["CoCd"] = "请选择";
            dr["cb"] = "请选择";
            dt.Rows.InsertAt(dr, 0);
            this.cb_BS_company.DataSource = dt;
            cb_BS_company.DisplayMember = "cb";
            cb_BS_company.ValueMember = "CoCd";

        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-11-06
        /// 添加目的：生成最后一行统计,并且分类统计
        /// </summary>
        private void bindDataFour(DataTable dt)
        {
            string[] obj = new string[dt.Columns.Count];
            double[] dk = new double[dt.Columns.Count];
            double[] dm = new double[dt.Columns.Count];
            string plline = dt.Rows[0]["AccType"].ToString();
            //生成最后的统计行 和173行一起注释掉的代码
            ////统计所有在最后生成一行 总统计
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    for (int k = 0; k < dt.Columns.Count; k++)
            //    {
            //        if (k >= 2)
            //        {
            //            dm[k] += double.Parse(dt.Rows[i][k].ToString());
            //        }
            //    }
            //}
            //obj[0] = "总计";//总统计 字段
            //for (int k = 2; k < dk.Length; k++)
            //{
            //    obj[k] = dm[k].ToString();
            //}
            //DataRow df = dt.NewRow();
            //df.ItemArray = obj;
            //分类  统计  不能统计最后一类的  合计
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["AccType"].ToString().Trim() == plline)
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
                else if (dt.Rows[i]["AccType"].ToString().Trim() != plline)//不同时 添加 总计
                {
                    if (i <= dt.Rows.Count - 1)
                    {
                        plline = dt.Rows[i]["AccType"].ToString();
                    }
                    obj[0] = dt.Rows[i - 1][0] + "  Total";
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
            if (dt.Rows[dt.Rows.Count - 1]["AccType"].ToString().Trim() != dt.Rows[dt.Rows.Count - 2]["AccType"].ToString().Trim())
            {
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
            else
            {
                //不属于 一行一类  2014-11-06
                int p = dt.Rows.Count - 1;
                string type = dt.Rows[p]["AccType"].ToString().Trim();
                for (int i = p; i > 0; i--)
                {
                    if (type == dt.Rows[p]["AccType"].ToString().Trim())
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
            ////算出的结果 增添到表格的最后一行
            //dt.Rows.InsertAt(df, dt.Rows.Count);
            //for (int k = 2; k < dk.Length; k++)
            //{
            //    dm[k] = 0.0;
            //}

        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-11-11
        /// 添加目的：合并字段
        /// </summary>
        /// <param name="table"></param>
        private void fieldMerge(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["AccType"].ToString().Trim() != "" && table.Rows[i]["AccSubType"].ToString().Trim() == "")
                {
                    table.Rows[i]["AccSubType"] = table.Rows[i]["AccType"].ToString().Trim();
                }
            }
            table.Columns.Remove("AccType");
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-11-11
        /// 添加目的：生成 资产总计，负债合计，生成负债和股东权益合计 这三行数据
        /// </summary>
        /// <param name="table"></param>
        private void createField(DataTable table)
        {
            double[] dm = new double[dt.Columns.Count];
            string[] obj = new string[dt.Columns.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["AccSubType"].ToString().Trim() == "流动资产  Total")
                {
                    for (int k = 1; k < table.Columns.Count; k++)
                    {
                        dm[k] = double.Parse(table.Rows[i][k].ToString().Trim());
                    }
                }
                else if (table.Rows[i]["AccSubType"].ToString().Trim() == "非流动资产  Total")
                {
                    //统计出 资产合计 行
                    for (int j = 1; j < table.Columns.Count; j++)
                    {
                        dm[j] += double.Parse(table.Rows[i][j].ToString().Trim());
                        obj[j] = dm[j].ToString();
                    }
                    obj[0] = "资产总计";
                    DataRow dr = table.NewRow();
                    dr.ItemArray = obj;
                    table.Rows.InsertAt(dr, i + 1);
                    //清零
                    for (int j = 1; j < table.Columns.Count; j++)
                    {
                        dm[j] = 0;
                    }
                    i += 1;//因为生成“资产总计”行 所以此处要清空，再参与别的计算
                }
                else if (table.Rows[i]["AccSubType"].ToString().Trim() == "流动负债  Total")//流动负债  Total
                {
                    for (int k = 1; k < table.Columns.Count; k++)
                    {
                        dm[k] = double.Parse(table.Rows[i][k].ToString().Trim());
                    }
                }
                else if (table.Rows[i]["AccSubType"].ToString().Trim() == "非流动负债  Total")//非流动负债  Total
                {
                    //统计出 负债合计 行
                    for (int j = 1; j < table.Columns.Count; j++)
                    {
                        dm[j] += double.Parse(table.Rows[i][j].ToString().Trim());
                        obj[j] = dm[j].ToString();
                    }
                    obj[0] = "负债总计";
                    DataRow dr = table.NewRow();
                    dr.ItemArray = obj;
                    table.Rows.InsertAt(dr, i + 1);
                    //因为后面的  生成负债和股东权益合计 行需要用到此行（负债总计）的数据所以不用清零
                    i += 1;
                }
                //生成负债和股东权益合计   行 =负债总计(非流动负债  Total+流动负债  Total)+股东权益  Total
                else if (table.Rows[i]["AccSubType"].ToString().Trim() == "股东权益  Total" )//非流动负债  Total
                {
                    //统计出 负债合计 行
                    for (int j = 1; j < table.Columns.Count; j++)
                    {
                        dm[j] += double.Parse(table.Rows[i][j].ToString().Trim());
                        obj[j] = dm[j].ToString();
                    }
                    obj[0] = "负债和股东权益合计";
                    DataRow dr = table.NewRow();
                    dr.ItemArray = obj;
                    table.Rows.InsertAt(dr, i + 1);
                    //清零
                    for (int j = 1; j < table.Columns.Count; j++)
                    {
                        dm[j] = 0;
                    }
                    i += 1;
                }
            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-26
        /// 添加目的：修改表格显示 体现Total
        /// </summary>
        /// <param name="dt"></param>
        public void changeStyle(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i]["AccType"].ToString().Trim() != "" && dt.Rows[i]["AccSubType"].ToString().Trim() == "")
                    {
                        dt.Rows[i]["AccType"] = dt.Rows[i]["AccType"].ToString().Trim() + " Total";
                        dt.Rows[i]["AccSubType"] = dt.Rows[i]["AccType"].ToString().Trim();
                    }
                    else if (dt.Rows[i]["AccType"].ToString().Trim() == "" && dt.Rows[i]["AccSubType"].ToString().Trim() == "")
                    {
                        dt.Rows[i]["AccType"] = " Total";
                        dt.Rows[i]["AccSubType"] = " Total";
                    }
                }
            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-26
        /// 添加目的：合并Datatable 
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public DataTable MergeDataTable(DataTable dt1, DataTable dt2)
        {
            //定义dt的行数   
            int dtRowCount = 0;
            //dt的行数为dt1或dt2中行数最大的行数   
            if (dt1.Rows.Count > dt2.Rows.Count)
            {
                dtRowCount = dt1.Rows.Count;
            }
            else
            {
                dtRowCount = dt2.Rows.Count;
            }
            DataTable dt = new DataTable();
            //向dt中添加dt1的列名   
            for (int i = 0; i < dt1.Columns.Count; i++)
            {
                dt.Columns.Add(dt1.Columns[i].ColumnName);
            }
            //向dt中添加dt2的列名   
            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                if (dt2.Columns[i].ColumnName.ToString().Trim() == "AccType" || dt2.Columns[i].ColumnName.ToString().Trim() == "AccSubType")
                {
                    dt.Columns.Add(dt2.Columns[i].ColumnName + "2");
                }
                else
                {
                    dt.Columns.Add(dt2.Columns[i].ColumnName);
                }
            }
            #region hxy修改，给dt设置数据类型string转为double，否则最后datagridview设置千分位无效
            foreach (DataColumn item in dt.Columns)
            {
                if (item.ColumnName != "AccType2" && item.ColumnName != "AccSubType2" && item.ColumnName != "AccType" && item.ColumnName != "AccSubType")
                {
                    item.DataType = Type.GetType("System.Double");
                }
            }
            #endregion


            for (int i = 0; i < dtRowCount; i++)
            {
                DataRow row = dt.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    for (int k = 0; k < dt1.Columns.Count; k++) { if ((dt1.Rows.Count - 1) >= i) { row[k] = dt1.Rows[i].ItemArray[k]; } }
                    for (int k = 0; k < dt2.Columns.Count; k++) { if ((dt2.Rows.Count - 1) >= i) { row[dt1.Columns.Count + k] = dt2.Rows[i].ItemArray[k]; } }
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

     
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-26
        /// 添加目的：资产负债表 查询
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Select_Click(object sender, EventArgs e)
        {
            string _date = dtp_BS.Text.ToString();
            string[] One_date = _date.Split(new Char[] { '-' });
            string strCollected = "";
            AccountPeriod = One_date[0] + "-0" + One_date[1];
            if (cb_BS_company.SelectedValue.ToString().Trim() != "请选择")//合资公司
            {
                cocd = cb_BS_company.SelectedValue.ToString().Trim();
            }
            else
            {
                cocd = "";
            }
             #region t8数值
            //t8 数值
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                if (checkedListBox.GetItemChecked(i))
                {
                    if (strCollected == "")
                    {
                        strCollected = "'" + checkedListBox.GetItemText(checkedListBox.Items[i]) + "'";
                    }
                    else
                    {
                        strCollected = strCollected + ",'" + checkedListBox.GetItemText(checkedListBox.Items[i]) + "'";
                    }
                }
            }
            t8 = strCollected;
            #endregion

             #region 资产负债表 报告
            #region 选择报表类型为“请选择”给出提示
            if (comb_BS.SelectedItem.ToString().Trim() == "请选择")
            {
                MessageBox.Show("类别是必选项");
            }
            #endregion

            #region 资产负债表 类别为“差值”
            else if (comb_BS.SelectedItem.ToString().Trim() == "差值")
            {
               // string bb = cocd + "" + AccountPeriod + "" + t8;
                DataTable dataChu = importdal.bsSeleByProcMonthChu(cocd, AccountPeriod, t8);//年初
                DataTable dtCha = importdal.bsSeleByProcMin(cocd, AccountPeriod, t8);//所选日期上个月的累计，当月的月发生额，
                if (dtCha != null && dtCha.Rows.Count > 0)
                {

                    if (dataChu != null && dataChu.Rows.Count > 0)
                    {
                        dt = this.MergeDataTable(dataChu, dtCha);
                        dt.Columns.Remove("AccType2");
                        dt.Columns.Remove("AccSubType2");
                        //将资产负债表里 “非流动负债”，“流动负债”，“股东权益”类里的数据乘以（-1）
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 2; j < dt.Columns.Count; j++)
                            {
                                if (dt.Rows[i]["AccType"].ToString().Trim() == "非流动负债" || dt.Rows[i]["AccType"].ToString().Trim() == "流动负债" || dt.Rows[i]["AccType"].ToString().Trim() == "股东权益")
                                {
                                    dt.Columns[j].ReadOnly = false;
                                    dt.Rows[i][j] = double.Parse(dt.Rows[i][j].ToString().Trim()) * (-1);
                                }
                            }
                        }

                        //累计 加上期初值
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Columns[3].ReadOnly = false;
                            dt.Columns[4].ReadOnly = false;
                            dt.Rows[i][3] = double.Parse(dt.Rows[i][2].ToString()) + double.Parse(dt.Rows[i][3].ToString());
                            dt.Rows[i][4] = double.Parse(dt.Rows[i][2].ToString()) + double.Parse(dt.Rows[i][4].ToString());
                        }
                    }
                    //选择本月1月 显示上年12月的数值 如果表头出现‘000’的形式 要转换成上年 12月份
                    string[] Columu = dt.Columns[4].Caption.ToString().Split(new Char[] { ' ' });//将第三列的数据分割
                    string[] DateColumn = Columu[2].ToString().Split(new Char[] { '-' });//将yyyy-mmm形式的数据分割开
                    int year = 0;
                    if (DateColumn[1].ToString() == "000")//获取月份部分，如果为000  则需要修改该字段为 (yyyy-1)-"012"
                    {
                        year = int.Parse(DateColumn[0]) - 1;
                        dt.Columns[4].ColumnName = Columu[0] + " " + Columu[1] + " " + year + "-012";
                    }
                    bindDataFour(dt);
                    fieldMerge(dt);
                    createField(dt);

                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["年初余额"].Visible = false;//隐藏期初余额 列
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (i != 0)
                    {
                        dataGridView1.Columns[i].DefaultCellStyle.Format = "N2";
                        dataGridView1.Columns[i].DefaultCellStyle.Font = new Font("Arial", 10);
                        dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }

            }
            #endregion

            #region 资产负债表类别为“ 年初期末”
            else if (comb_BS.SelectedItem.ToString().Trim() == "年初期末")
            {
                // dt = importdal.bsSeleByProc(cocd, AccountPeriod, t8);
                DataTable dataChu = importdal.bsSeleByProcMonthChu(cocd, AccountPeriod, t8);//年初
                DataTable dataMid = importdal.bsSeleByProc(cocd, AccountPeriod, t8);//从当年开始到本月的累计
                if (dataChu != null && dataChu.Rows.Count > 0)
                {
                    if (dataMid != null && dataMid.Rows.Count > 0)
                    {
                        dt = this.MergeDataTable(dataChu, dataMid);
                        dt.Columns.Remove("AccType2");
                        dt.Columns.Remove("AccSubType2");
                        //将资产负债表里 “非流动负债”，“流动负债”，“股东权益”类里的数据乘以（-1）
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 2; j < dt.Columns.Count; j++)
                            {
                                if (dt.Rows[i]["AccType"].ToString().Trim() == "非流动负债" || dt.Rows[i]["AccType"].ToString().Trim() == "流动负债" || dt.Rows[i]["AccType"].ToString().Trim() == "股东权益")
                                {
                                    dt.Columns[j].ReadOnly = false;
                                    dt.Rows[i][j] = double.Parse(dt.Rows[i][j].ToString().Trim()) * (-1);
                                }
                            }
                        }

                        //本年累计+ 期初值
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                          
                            dt.Columns[3].ReadOnly = false;
                             dt.Rows[i][3] = double.Parse(dt.Rows[i][2].ToString()) + double.Parse(dt.Rows[i][3].ToString());
                           
                        }
                    }
                 
                    bindDataFour(dt);
                    fieldMerge(dt);
                    createField(dt);
                }

                dataGridView1.DataSource = null;//为解决 datagridview里数据字段显示错位
                dataGridView1.DataSource = dt;
                foreach (DataGridViewColumn item in dataGridView1.Columns)
                {
                    if (item.Name.Trim() == "年初余额" || item.Name.Trim() == "期末余额")
                    {
                        item.DefaultCellStyle.Format = "N2";
                        item.DefaultCellStyle.Font = new Font("Arial", 10);
                        item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                }
            }
            #endregion

            #region 资产负债表类别为“ 按月”
            else if (comb_BS.SelectedItem.ToString().Trim() == "按月")
            {
                DataTable dataChu = importdal.bsSeleByProcMonthChu(cocd, AccountPeriod, t8);//期初
                DataTable dataMonth = importdal.bsSeleByProcMonth(cocd, AccountPeriod, t8);//每月
                if (dataChu != null && dataMonth != null)
                {
                 
                    dt = this.MergeDataTable(dataChu, dataMonth);
                    dt.Columns.Remove("AccType2");
                    dt.Columns.Remove("AccSubType2");
                    //将资产负债表里 “非流动负债”，“流动负债”，“股东权益”类里的数据乘以（-1）
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 2; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["AccType"].ToString().Trim() == "非流动负债" || dt.Rows[i]["AccType"].ToString().Trim() == "流动负债" || dt.Rows[i]["AccType"].ToString().Trim() == "股东权益")
                            {
                                dt.Columns[j].ReadOnly = false;
                                dt.Rows[i][j] = double.Parse(dt.Rows[i][j].ToString().Trim()) * (-1);
                            }
                        }
                    }

                    //累计加  从存储过程里查出的数据都是各个月的  不是累计的 所以此处要做累加处理
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 2; j < dt.Columns.Count - 1; j++)
                        {
                            dt.Columns[j].ReadOnly = false;
                            dt.Rows[i][j + 1] = double.Parse(dt.Rows[i][j].ToString()) + double.Parse(dt.Rows[i][j + 1].ToString());
                        }
                    }
                    bindDataFour(dt);
                    fieldMerge(dt);
                    createField(dt);
                }
                //为解决 datagridview里数据字段显示错位
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;

                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].DefaultCellStyle.Format = "N2";
                    dataGridView1.Columns[i].DefaultCellStyle.Font = new Font("Arial", 10);
                    dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }


            }
            #endregion
        #endregion
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-26
        /// 添加目的：资产负债表  数据导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_import_Click(object sender, EventArgs e)
        {
            #region npoi 导出excel
            DataTable dt = (DataTable)dataGridView1.DataSource;
            string ReportType = comb_BS.GetItemText(comb_BS.Items[comb_BS.SelectedIndex]);
            string compeny = cb_BS_company.GetItemText(cb_BS_company.Items[cb_BS_company.SelectedIndex]);
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("请先在该表中导入数据！");
                int result = logDal.OpertionLogInsert(userid, "资产负债表" + ReportType + "导出操作", DateTime.Now.ToString(), "资产负债表" + ReportType + "导出操作，先在该表中导入数据");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {
                ImportToExcel.Export(dt, "资产负债表" + ReportType + "", "资产负债表(" + ReportType + ")", compeny, DateTime.Now.ToString());
                int result = logDal.OpertionLogInsert(userid, "资产负债表" + ReportType + "导出操作", DateTime.Now.ToString(), "资产负债表导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            #endregion
            //ImportToExcel imp = new ImportToExcel();
            //imp.toExcel(dataGridView1, "资产负债表");
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-26
        /// 合资公司 选项更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_BS_company_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cb_BS_company.SelectedValue.ToString().Trim() != "System.Data.DataRowView" && cb_BS_company.SelectedValue.ToString().Trim() != "请选择")//不同类别的选择条件
            {
                DataTable dtCheck = tcodeDal.TCodeSelectT8(cb_BS_company.SelectedValue.ToString().Trim());
                if (dtCheck != null && dtCheck.Rows.Count > 0)
                {
                    select_All.Checked = false;
                    checkedListBox.Items.Clear();
                    for (int i = 0; i < dtCheck.Rows.Count; i++)
                    {
                        checkedListBox.Items.Add(dtCheck.Rows[i][0].ToString().Trim());
                    }
                }
            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-26
        /// 添加目的：T8 索引更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_T8_SelectedValueChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        ///添加人：ydx
        ///添加时间：2014-10-27
        ///添加目的：T8全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (select_All.Checked)
            {
                for (int j = 0; j < checkedListBox.Items.Count; j++)
                {
                    checkedListBox.SetItemChecked(j, true);
                }
            }
            else
            {
                for (int j = 0; j < checkedListBox.Items.Count; j++)
                {
                    checkedListBox.SetItemChecked(j, false);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //李晓光做的 查询
            //this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            //this.button1.Enabled = false;
            //string _date = dtp.Text.ToString();
            //string[] One_date = _date.Split(new Char[] { '-' });
            //string AccountPeriod = One_date[0] + "-0" + One_date[1];
            //importdal.ImportInsert_tmp(AccountPeriod);
            //dataGridView1.DataSource = null;
            //dataGridView1.DataSource = importdal.ImportBSSelect();
            //importdal.ImportDelete_tmp();
            //this.Cursor = System.Windows.Forms.Cursors.Default;
            //this.button1.Enabled = true;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

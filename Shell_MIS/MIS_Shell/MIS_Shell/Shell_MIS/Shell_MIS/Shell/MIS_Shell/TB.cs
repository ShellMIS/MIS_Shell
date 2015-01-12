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
    /// 修改人：ydx
    /// 修改时间：2014-09-23
    /// 修改目的：试算平衡表
    /// </summary>
    public partial class TB : Form
    {
        JVDal jvdal = new JVDal();//合资公司下拉列表
        public TB()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        ImportDal importdal = new ImportDal();
        TCodeDal tcodeDal = new TCodeDal();//根据合资公司查T8
        public string t8 = "";
        
              int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        private void button1_Click(object sender, EventArgs e)
        {
            // importdal.ImportDelete_tmp();

            // importdal.ImportInsert_tmp(AccountPeriod);
            //if (cb_company.Text.ToString() == "合资公司")
            //{
            //    dataGridView1.DataSource = null;
            //    dataGridView1.DataSource = importdal.ImportTBSelect();
            //}
            //else if (cb_company.Text.ToString() == "SCL")
            //{
            //    dataGridView1.DataSource = null;
            //    dataGridView1.DataSource = importdal.ImportTBSelect_SCL();
            //}
            //else
            //{
            //    MessageBox.Show("请选择您想显示的公司！");
            //}
            //importdal.ImportDelete_tmp();
        }

        //private void ExpotExcel()
        //{
        //    if (dataGridView1.RowCount - 1 != 0)
        //    {
        //        SaveFileDialog saveFileDialog = new SaveFileDialog();
        //        saveFileDialog.Filter = "Execl   files   (*.xls)|*.xls";
        //        saveFileDialog.FilterIndex = 0;
        //        saveFileDialog.RestoreDirectory = true;
        //        saveFileDialog.CreatePrompt = true;
        //        saveFileDialog.Title = "导出Excel文件到";
        //        DateTime now = DateTime.Now;
        //        saveFileDialog.FileName = now.Year.ToString().PadLeft(2)
        //        + now.Month.ToString().PadLeft(2, '0')
        //        + now.Day.ToString().PadLeft(2, '0') + "-"
        //        + now.Hour.ToString().PadLeft(2, '0')
        //        + now.Minute.ToString().PadLeft(2, '0')
        //        + now.Second.ToString().PadLeft(2, '0');
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
        //    }
        //    else
        //    {
        //        MessageBox.Show("请先在该表中导入数据！");
        //    }
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            // ExpotExcel();
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-23
        /// 添加目的：给（合资公司）下拉列表赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_Load(object sender, EventArgs e)
        {
            this.comboType.SelectedItem = "请选择";//类别默认为请选择
            dt = jvdal.JVSelectImport();
            DataRow dr = dt.NewRow();
            dr["cb"] = "请选择";
            dr["CoCd"] = "请选择";
            dt.Rows.InsertAt(dr, 0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.cb_company.DataSource = dt;
                cb_company.DisplayMember = "cb";
                cb_company.ValueMember = "CoCd";
            }
            dataGridView1.Font = new Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);

        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-23
        /// 添加目的：试算平衡表 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Select_Click(object sender, EventArgs e)
        {

            string strCollected = string.Empty;//t8
            string _date = dtp.Text.ToString();
            string[] One_date = _date.Split('-');
            string endTi = endTime.Text.ToString();
            string[] end = endTi.Split('-');
            //string[] One_date = _date.Split(new Char[] { '-' });
            string AccountPeriodStar = One_date[0] + "-0" + One_date[1];
            string peridTimeEnd = end[0] + "-0" + end[1];
            string cocd = "";
            // string t8 = "";
            string t8 = "";
            if (cb_company.SelectedValue.ToString().Trim() != "请选择")
            {
                cocd = cb_company.SelectedValue.ToString().Trim();
                //增加T8查询条件
            }

            if (comboType.SelectedItem.ToString().Trim() == "请选择")
            {
                MessageBox.Show("类别必须选择");
            }
            else
            {
                //获得选中的T8
                for (int i = 0; i < checkedListBox.Items.Count; i++)
                {
                    if (checkedListBox.GetItemChecked(i))
                    {
                        if (strCollected == string.Empty)
                        {
                            strCollected = "'" + checkedListBox.GetItemText(checkedListBox.Items[i]) + "'";
                        }
                        else
                        {
                            strCollected = strCollected + ",'" + checkedListBox.GetItemText(checkedListBox.Items[i]) + "'";
                        }
                    }
                }
                if (strCollected == string.Empty)
                {
                    t8 = "";
                }
                else
                {
                    t8 = strCollected;
                }
                #region 替换后
                
                
                if (comboType.SelectedItem.ToString().Trim() == "Accountcode替换后")
                {
                    // MessageBox.Show("JV：" + cb_company.SelectedValue.ToString().Trim() + "Shijian:" + AccountPeriod);
                    dt = importdal.tbSeleByProcD(cocd, AccountPeriodStar, peridTimeEnd, t8);
                    //MessageBox.Show(""+dt.Rows.Count);
                    if (dt!=null)
                    {
                        string[] obj = new string[dt.Columns.Count];
                        // double[] dk = new double[dt.Columns.Count];
                        double[] dm = new double[dt.Columns.Count];
                        //统计所有在最后生成一行 总统计
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
                        //obj[0] = "Total Opex";//总统计 字段
                        //for (int k = 2; k < dt.Columns.Count; k++)
                        //{
                        //    obj[k] = dm[k].ToString();
                        //}
                        //DataRow df = dt.NewRow();
                        //df.ItemArray = obj;
                        //以下代码用来控制数据显示两位小数
                        if (dt.Rows.Count > 0)
                        {

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                for (int j = 2; j < dt.Columns.Count; j++)
                                {
                                    dt.Columns[j].ReadOnly = false;
                                    dt.Rows[i][j] = double.Parse(dt.Rows[i][j].ToString().Trim());

                                    if (j >= 2)
                                    {
                                        dm[j] += double.Parse(dt.Rows[i][j].ToString());
                                    }
                                }
                            }
                            //生成总统计行
                            for (int k = 2; k < dt.Columns.Count; k++)
                            {
                                obj[k] = dm[k].ToString();
                            }
                            DataRow df = dt.NewRow();
                            df.ItemArray = obj;
                            dt.Rows.InsertAt(df, dt.Rows.Count);
                            dt.Rows[dt.Rows.Count - 1][0] = "总数";
                        }
                     
                    }
                    if (dt == null || dt.Rows.Count <= 0)
                    {
                        MessageBox.Show("没有数据");
                    }
                    
                }
                #endregion
                #region 替换前和替换后


                else if (comboType.SelectedItem.ToString().Trim() == "Accountcode替换前和替换后")
                {
                    // MessageBox.Show("JV：" + cb_company.SelectedValue.ToString().Trim() + "Shijian:" + AccountPeriod);
                    dt = importdal.tbSeleByProcSD(cocd, AccountPeriodStar, peridTimeEnd, t8);
                    if (dt!=null)
                    {
                        string[] obj = new string[dt.Columns.Count];
                        // double[] dk = new double[dt.Columns.Count];
                        double[] dm = new double[dt.Columns.Count];
                        //MessageBox.Show(""+dt.Rows.Count);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                for (int j = 3; j < dt.Columns.Count; j++)
                                {
                                    dt.Columns[j].ReadOnly = false;
                                    dt.Rows[i][j] = double.Parse(dt.Rows[i][j].ToString().Trim());

                                    if (j >= 3)
                                    {
                                        dm[j] += double.Parse(dt.Rows[i][j].ToString());
                                    }
                                }
                            }

                            for (int k = 3; k < dt.Columns.Count; k++)
                            {
                                obj[k] = dm[k].ToString();
                            }
                            DataRow df = dt.NewRow();
                            df.ItemArray = obj;
                            dt.Rows.InsertAt(df, dt.Rows.Count);
                            dt.Rows[dt.Rows.Count - 1][0] = "总数";
                        }

                        int bb = dt.Rows.Count;
                    }
                  
                    if (dt == null || dt.Rows.Count <= 0)
                    {
                        MessageBox.Show("没有数据");
                    }
                }
                #endregion
                #region 千分位设置

                ////重新声明一个DataTable，dt1，然后把数据源的Table复制到dt1,注意先复制列，然后设置列的数据类型，最后填充数据
                //DataTable dt1 = new DataTable();
                //DataColumn dc;
                ////给dt1添加列
                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    dc = new DataColumn(dt.Columns[i].ColumnName);
                //    dt1.Columns.Add(dc);
                //}
                ////设置dt1列的数据类型
                //for (int i = 0; i < dt1.Columns.Count; i++)
                //{
                //    if (dt1.Columns[i].ToString().Trim() == "期间余额" || dt1.Columns[i].ToString().Trim() == "期间借方" || dt1.Columns[i].ToString().Trim() == "期间贷方" || dt1.Columns[i].ToString().Trim() == "期末余额" || dt1.Columns[i].ToString().Trim() == "期初余额")
                //    {
                //        dt1.Columns[i].DataType = Type.GetType("System.String");
                //    }

                //}
                //dt1 = dt.Copy();

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;

                dataGridView1.Font = new Font("Arial", 10);
                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
              
                #endregion

            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-23
        /// 添加目的：试算平衡表  数据导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_import_Click(object sender, EventArgs e)
        {
            #region npoi 导出excel
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("请先在该表中导入数据！");
                int result = logDal.OpertionLogInsert(userid, "试算平衡表导出操作", DateTime.Now.ToString(), "试算平衡表导出操作，先在该表中导入数据");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {
                ImportToExcel.Export(dt, "Trial Balance", "Trial Balance", "", DateTime.Now.ToString());
                int result = logDal.OpertionLogInsert(userid, "试算平衡表导出操作", DateTime.Now.ToString(), "试算平衡表导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            #endregion
            //ImportToExcel imp = new ImportToExcel();
            //imp.toExcel(dataGridView1, "试算平衡表");
        }
        /// <summary>
        /// 合资公司改变事件 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_company_SelectedValueChanged(object sender, EventArgs e)
        {
            //string bb = cb_company.SelectedValue.ToString();
            if (this.cb_company.SelectedValue.ToString().Trim() != "System.Data.DataRowView" && cb_company.SelectedValue.ToString().Trim() != "请选择")//不同类别的选择条件
            {
                DataTable dtCheck = tcodeDal.TCodeSelectT8(cb_company.SelectedValue.ToString().Trim());
                if (dtCheck != null && dtCheck.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCheck.Rows.Count; i++)
                    {
                        checkedListBox.Items.Add(dtCheck.Rows[i][0].ToString().Trim());

                    }
                }
            }
        }
        public static string accountCodeD;
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-20
        /// 添加目的：试算平衡表 数据追踪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            string accountCode = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
             string _date = dtp.Text.ToString();
            string[] One_date = _date.Split('-');
            string endTi = endTime.Text.ToString();
            string[] end = endTi.Split('-');       
            string AccountPeriodStar = One_date[0] + "-0" + One_date[1];
            string peridTimeEnd = end[0] + "-0" + end[1];

            accountCodeD += " and  AccountPeriod>='" + AccountPeriodStar + "' and AccountPeriod<='" + peridTimeEnd + "'";
            if (accountCode != "")
            {
                accountCodeD += " and AccountCodeD='" + accountCode + "'";
            }
            if (this.cb_company.SelectedValue.ToString().Trim() != "" && cb_company.SelectedValue.ToString().Trim() != "请选择")
            {
                accountCodeD += " and CoCd='" + cb_company.SelectedValue.ToString().Trim() + "'";
            }
            if (t8 != "")
            {
                accountCodeD += " and T8 in('" + t8 + "')";
            }
            DataListDB dat = new DataListDB();
            dat.Owner = this;
            dat.ShowDialog();

        }

        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-28
        /// 添加目的:T8全选 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSelectAll.Checked)
            {
                for (int i = 0; i < checkedListBox.Items.Count; i++)
                {
                    checkedListBox.SetItemChecked(i, true);
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

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || dataGridView1.Rows.Count <= 0) return;
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? string.Empty).ToString();
        }

      
       

        

       
    }
}

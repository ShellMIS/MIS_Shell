using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel=Microsoft.Office.Interop.Excel;
using System.IO;
using DAL;
using MIS_Shell.CommExcel;

namespace MIS_Shell
{
    public partial class MIS_Report : Form
    {
        public MIS_Report()
        {
            InitializeComponent();
        }
        ImportDal importdal = new ImportDal();
        CommBS commbs = new CommBS();
        CommPL commpl = new CommPL();
        CommKPI co = new CommKPI();
        DataTable dt = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            if (drop_report.Text.ToString() == "《试算平衡表》")
            {
                string start = com_star_year.Text.ToString() + '-' + com_start_mon.Text.ToString();
                string end = com_end_year.Text.ToString() + '-' + com_end_mon.Text.ToString();
                dataGridView1.DataSource = importdal.ImportTBSelect();
                ExpotExcel();
                MessageBox.Show("导出成功！");
            }
            else if (drop_report.Text.ToString() == "《资产负债表》")
            {
                string FileName = Application.StartupPath + "\\Report\\资产负债表Model.xls";
                string FileName1 = Application.StartupPath + "\\OutputReport\\资产负债表" + now.Year.ToString() + now.Month.ToString() + now.Day.ToString() + "-" + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString() + ".xls";
                commbs.MIS_BS(FileName, FileName1);
                MessageBox.Show("成功！");
            }
            else if (drop_report.Text.ToString() == "《损益表》")
            {
                string FileName = Application.StartupPath + "\\Report\\损益表Model.xls";
                string FileName1 = Application.StartupPath + "\\OutputReport\\损益表"+now.Year.ToString()+now.Month.ToString()+now.Day.ToString()+"-"+now.Hour.ToString()+now.Minute.ToString()+now.Second.ToString()+".xls";
                commpl.MIS_PL(FileName, FileName1);
                MessageBox.Show("成功！");
            }
            else if (drop_report.Text.ToString() == "《MIS—KPI》")
            {
                string FileName = Application.StartupPath + "\\Report\\MIS-KPI Model.xls";
                string FileName1 = Application.StartupPath + "\\OutputReport\\MIS-KPI"+now.Year.ToString()+now.Month.ToString()+now.Day.ToString()+"-"+now.Hour.ToString()+now.Minute.ToString()+now.Second.ToString()+".xls";
                co.MIS_OPEX(FileName,FileName1);
                MessageBox.Show("成功！");
            }
                ///就是这部分代码
                ///
            else if (drop_report.Text.ToString() == "《费用表》")
            {
                #region 
                DataTable dt = null;//importdal.ImportOPEX();//获得数据在此datatable里
                string []obj=new string[dt.Columns.Count];
                double []dk=new double[dt.Columns.Count];
                double[] dm = new double[dt.Columns.Count];
                string plline = dt.Rows[0]["PLLine"].ToString();

                //统计所有在最后生成一行 总统计
                for (int i = 0; i < dt.Rows.Count;i++ )
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
                for (int i = 0; i < dt.Rows.Count;i++ )
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
                        obj[0] = dt.Rows[i - 1][0] + "Total";
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
                    obj[0] = dt.Rows[dt.Rows.Count-1][0] + "Total";
                    for (int k = 4; k < dk.Length; k++)
                    {
                        obj[k] = dk[k].ToString();
                    }
                    DataRow dr = dt.NewRow();
                    dr.ItemArray = obj;
                    dt.Rows.InsertAt(dr,dt.Rows.Count);
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
              dataGridView1.DataSource = dt;
             
                //string FileName = Application.StartupPath + "\\Report\\损益表Model.xls";
                //string FileName1 = Application.StartupPath + "\\OutputReport\\损益表.xls";
                //dataGridView1.DataSource = importdal.ImportOPEX();
                //ExpotExcel();

                if (ExportExcel())
                {
                    MessageBox.Show("导出成功！");
                }
                else
                {
                    MessageBox.Show("请先在该表中导入数据！");
                }
                #endregion
            }
            else
            {
                MessageBox.Show("请选择需要导出的报表！");
            }
        }
        /// <summary>
        /// ydx 
        /// </summary>
        private bool ExportExcel()
        {
            bool flag = false;
            if (dataGridView1.RowCount - 1 != 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl   files   (*.xls)|*.xls";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "导出Excel文件到";

                DateTime now = DateTime.Now;
                saveFileDialog.FileName = now.Year.ToString().PadLeft(2)
                + now.Month.ToString().PadLeft(2, '0')
                + now.Day.ToString().PadLeft(2, '0') + "-"
                + now.Hour.ToString().PadLeft(2, '0')
                + now.Minute.ToString().PadLeft(2, '0')
                + now.Second.ToString().PadLeft(2, '0');

                saveFileDialog.ShowDialog();

                Stream myStream;
                myStream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(0));

                string str = "";
                try
                {
                    //写标题     
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        if (i > 0)
                        {
                            str += "\t";
                        }
                        str += dataGridView1.Columns[i].HeaderText;

                    }

                    sw.WriteLine(str);
                    //写内容   
                    for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                    {
                        string tempStr = "";

                        for (int k = 0; k < dataGridView1.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                tempStr += "\t";
                            }
                            tempStr += dataGridView1[k, j].Value.ToString();
                        }
                        sw.WriteLine(tempStr);
                    }
                    sw.Close();
                    myStream.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
                flag = true;
            }
            else
            {
                flag = false;
                
            }

            return flag;
        }
        private void ExpotExcel() {
            if (dataGridView1.RowCount - 1 != 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl   files   (*.xls)|*.xls";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "导出Excel文件到";
                
                DateTime now = DateTime.Now;
                saveFileDialog.FileName = now.Year.ToString().PadLeft(2)
                + now.Month.ToString().PadLeft(2, '0')
                + now.Day.ToString().PadLeft(2, '0') + "-"
                + now.Hour.ToString().PadLeft(2, '0')
                + now.Minute.ToString().PadLeft(2, '0')
                + now.Second.ToString().PadLeft(2, '0');
                saveFileDialog.ShowDialog();
                Stream myStream;
                myStream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(0));

                string str = "";
                try
                {
                    //写标题     
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        if (i > 0)
                        {
                            str += "\t";
                        }
                        str += dataGridView1.Columns[i].HeaderText;

                    }

                    sw.WriteLine(str);
                    //写内容   
                    for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                    {
                        string tempStr = "";
                        for (int k = 0; k < dataGridView1.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                tempStr += "\t";
                            }
                            tempStr += dataGridView1[k, j].Value.ToString();
                        }
                        sw.WriteLine(tempStr);
                    }
                    sw.Close();
                    myStream.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
            }
            else
            {
                MessageBox.Show("请先在该表中导入数据！");
            }     
        }

        private void drop_report_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

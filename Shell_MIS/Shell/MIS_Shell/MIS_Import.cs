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
using DBHelper;
using MIS_Shell.CommExcel;

namespace MIS_Shell
{
    public partial class MIS_Import : Form
    {
        public MIS_Import()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        ImportDal importdal = new ImportDal();
        JVDal jvdal = new JVDal();//合资公司 下拉列表
        JVDSMDCDal DepartDal = new JVDSMDCDal();//获取
        ImportToExcel ImpExcel = new ImportToExcel();//将查出的数据导入到Excel表格里
        OptionLogDAL logDal = new OptionLogDAL();//操作日志
        int UserID = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户ID
        DataTable dts = new DataTable();//存放临时数据
        DataTable dtCF = null;
        public int sum = 0;
        #region 浏览
        /// <summary>
        /// 浏览按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string fileName = fd.FileName;

                if (!File.Exists(fileName))
                {
                    MessageBox.Show("文件不存在或已经删除");
                }
                else
                {
                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                   
                    bind(fileName);
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                }
            }
        }
        #endregion
        /// <summary>
        /// 修改人：ydx
        /// 修改目的：数据包导入 浏览
        /// 修改时间：2014-09-18
        /// </summary>
        /// <param name="fileName"></param>
        private void bind(string fileName)
        {
           
            try
            {
                importdal.ImportDelete();
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.GetEncoding("ASCII")))
                    {
                        string line;
                        #region
                        while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                        {
                            if (line.Length > 40)
                            {
                                string Account_Code = line.Substring(0, 10).ToString();

                                string Blank_Column = line.Substring(10, 5).ToString();

                                string date1 = line.Substring(15, 7).ToString();
                                string date1_1 = date1.Substring(0, 4).ToString();
                                string date1_2 = date1.Substring(4, 3).ToString();
                                string Account_Period = date1_1 + "-" + date1_2;

                                string date2 = line.Substring(22, 8).ToString();
                                string date2_1 = date2.Substring(0, 4).ToString();
                                string date2_2 = date2.Substring(4, 2).ToString();
                                string date2_3 = date2.Substring(6, 2).ToString();
                                string Tran = date2_1 + "-" + date2_2 + "-" + date2_3;
                                DateTime Transaction_Date = DateTime.ParseExact(Tran, "yyyy-MM-dd", null);

                                string Blank_Column1 = line.Substring(30, 2).ToString();

                                string Record_Type = line.Substring(32, 1).ToString();

                                string Journal_Number = line.Substring(33, 7).ToString();

                                string Line_Number = line.Substring(40, 7).ToString();

                                string Am = line.Substring(47, 18).ToString();
                                double Amount = (line.Substring(65, 1).ToString() == "D") ? double.Parse(Am) / 1000 : double.Parse(Am) / 1000 * -1;

                                string DebitCredit_Marker = line.Substring(65, 1).ToString();

                                string Allocation_Indicator = line.Substring(66, 1).ToString();

                                string Journal_Type = line.Substring(67, 5).ToString();

                                string Journal_Source = line.Substring(72, 3).ToString();

                                string Blank_Column2 = line.Substring(75, 2).ToString();

                                string Transaction_Reference = line.Substring(77, 10).ToString();

                                string Blank_Column3 = line.Substring(87, 5).ToString();

                                //string Description = GetSubString(line, 92, 25);

                                string Entry_Date = line.Substring(117, 8);

                                string Entry_Period = line.Substring(125, 7);

                                string Due_Date = line.Substring(132, 8);

                                string Blank_Column4 = line.Substring(140, 6);

                                string PaymentAllocation_Ref = line.Substring(146, 9);

                                string PaymentAllocation_Date = line.Substring(155, 8);

                                string PaymentAllocation_Period = line.Substring(163, 7);

                                string Asset_Indicator = line.Substring(170, 1);

                                string Asset_Code = line.Substring(171, 10);

                                string Asset_Sub_Code = line.Substring(181, 5);

                                string Conversion_Code = line.Substring(186, 5);

                                string Conversion_Rate = line.Substring(191, 18);

                                string Other_Amount = line.Substring(209, 18);

                                string Other_Amount_Decimal_Places = line.Substring(227, 1);

                                string Cleardown_Sequence_Number = line.Substring(228, 5);

                                string Blank_Column5 = line.Substring(233, 4);

                                string Next_Period_Reversal = line.Substring(237, 1);

                                string Loss_or_Gain = line.Substring(238, 1);

                                string Rough_Book_Flag = line.Substring(239, 1);

                                string In_Use_Flag = line.Substring(240, 1);

                                string T0 = line.Substring(241, 15);
                                string T1 = line.Substring(256, 15);
                                string T2 = line.Substring(271, 15);
                                string T3 = line.Substring(286, 15);
                                string T4 = line.Substring(301, 15);
                                string T5 = line.Substring(316, 15);
                                string T6 = line.Substring(331, 15);
                                string T7 = line.Substring(346, 15);
                                string T8 = line.Substring(361, 15);
                                string T9 = line.Substring(376, 15);

                                string date3 = line.Substring(391, 8);
                                string date3_1 = date3.Substring(0, 4).ToString();
                                string date3_2 = date3.Substring(4, 2).ToString();
                                string date3_3 = date3.Substring(6, 2).ToString();
                                string pos = date3_1 + "-" + date3_2 + "-" + date3_3;
                                DateTime Posting_Date = DateTime.ParseExact(pos, "yyyy-MM-dd", null);

                                //dts.Columns["Id"].AutoIncrement = true;
                                //dts.Columns["Id"].AutoIncrementSeed = 1;
                                //dts.Columns["Id"].AutoIncrementStep = 1;
                                object[] ob = new object[] {null, Account_Code, Account_Period, Transaction_Date , Record_Type,  Journal_Number , Line_Number, Amount, DebitCredit_Marker, Allocation_Indicator, Journal_Type,
                                    Journal_Source, Transaction_Reference,null, Entry_Date, Entry_Period, Due_Date, PaymentAllocation_Ref, PaymentAllocation_Date, PaymentAllocation_Period, Asset_Indicator, Asset_Code,
                                    Asset_Sub_Code, Conversion_Code, Conversion_Rate, Other_Amount, Other_Amount_Decimal_Places, Cleardown_Sequence_Number, Next_Period_Reversal, Loss_or_Gain, Rough_Book_Flag,
                                    In_Use_Flag, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, Posting_Date,null,null,null,null,null};

                                dts.Rows.Add(ob);//存入临时表

                                //importdal.ImportInsert(Account_Code, Account_Period, Transaction_Date, Record_Type, Journal_Number, Line_Number, Amount, DebitCredit_Marker, Allocation_Indicator, Journal_Type,
                                //    Journal_Source, Transaction_Reference, Entry_Date, Entry_Period, Due_Date, PaymentAllocation_Ref, PaymentAllocation_Date, PaymentAllocation_Period, Asset_Indicator, Asset_Code,
                                //    Asset_Sub_Code, Conversion_Code, Conversion_Rate, Other_Amount, Other_Amount_Decimal_Places, Cleardown_Sequence_Number, Next_Period_Reversal, Loss_or_Gain, Rough_Book_Flag,
                                //    In_Use_Flag, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, Posting_Date);
                                //cb_for.Checked = true;
                            }
                        }
                        #endregion

                    }
                }
                DBHelper.SqlHelp.InsertTable(dts, "T_imp_raw_tmp");//将临时表批量插入数据库临时表
                dts.Clear();//插入临时表的同时删除内存中的表， 否则导入下月的数据时会同时导入上月的数据
                string _date = dtp.Text.ToString();
                string[] One_date = _date.Split(new Char[] { '-' });
                string AccountPeriod = One_date[0] + "-0" + One_date[1];//会计期间
                DataTable dtcompany = new DataTable();
                dtcompany = importdal.ImportCoCdSelect(cmb_company.SelectedValue.ToString().Trim());//选择合资公司
                DataTable dtDate = new DataTable();
                dtDate = importdal.ImportDateSelect(AccountPeriod);//会计期间
                DataTable dtRepeat = new DataTable();
                dtRepeat = importdal.ImportCoCdSelect(cmb_company.SelectedValue.ToString().Trim(), AccountPeriod);

                if (dtcompany.Rows[0][0].ToString().Trim() == "0")//合资公司
                {
                    this.but_Verify.Enabled = false;
                    this.but_jy.Enabled = false;

                    importdal.ImportDelete();
                    MessageBox.Show("选择公司与导入文件包不匹配，请重新选择文件包！");
                    this.textBox1.Clear();
                }
                else if (dtDate.Rows[0][0].ToString().Trim() == "0")//会计期间
                {
                    this.but_Verify.Enabled = false;
                    this.but_jy.Enabled = false;
                    importdal.ImportDelete();
                    MessageBox.Show("选择期间与导入文件包不匹配，请重新选择文件包！");
                    this.textBox1.Clear();
                }
                else if (dtRepeat.Rows[0][0].ToString().Trim() != "0")
                {
                    this.but_Verify.Enabled = false;
                    this.but_jy.Enabled = false;
                    importdal.ImportDelete();
                    MessageBox.Show("该合资公司在该期间内的包文件已经上传！");
                    this.textBox1.Clear();
                }
                else
                {
                    this.but_Verify.Enabled = true;//校验
                    this.but_jy.Enabled = false;//匹配
                    this.button1.Enabled = false;//浏览不可用
                    this.textBox1.Text = fileName.ToString();
                    MessageBox.Show("请验证！");
                }
            }
            catch (Exception err)
            {
                dts.Clear();
                importdal.ImportDelete();
                MessageBox.Show("操作失败！" + err.ToString());
            }
        }
        #region MyRegion
        /// <summary>
        /// 获取字符串中指定位置开始的指定长度的字符串，支持汉字英文混合 汉字为2字节计数
        /// </summary>
        /// <param name="strSub">输入中英混合字符串</param>
        /// <param name="start">开始截取的起始位置</param>
        /// <param name="length">要截取的字符串长度</param>
        /// <returns></returns>
        public static string GetSubString(string strSub, int start, int length)
        {
            string temp = strSub;
            int j = 0, k = 0, p = 0;

            CharEnumerator ce = temp.GetEnumerator();
            while (ce.MoveNext())
            {
                j += (ce.Current > 0 && ce.Current < 255) ? 1 : 2;

                if (j <= start)
                {
                    p++;
                }
                else
                {
                    if (j == GetLength(temp))
                    {
                        temp = temp.Substring(p, k + 1);
                        break;
                    }
                    if (j <= length + start)
                    {
                        k++;
                    }
                    else
                    {
                        temp = temp.Substring(p, k);
                        break;
                    }
                }
            }

            return temp;
        }

        /// <summary>
        /// 获取指定字符串长度，汉字以2字节计算
        /// </summary>
        /// <param name="aOrgStr">要统计的字符串</param>
        /// <returns></returns>
        private static int GetLength(String aOrgStr)
        {
            int intLen = aOrgStr.Length;
            int i;
            char[] chars = aOrgStr.ToCharArray();
            for (i = 0; i < chars.Length; i++)
            {
                if (System.Convert.ToInt32(chars[i]) > 255)
                {
                    intLen++;
                }
            }
            return intLen;
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                DataRow dr = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    //insertToSql(dr);
                }
                MessageBox.Show("导入成功！");
            }
            else
            {
                MessageBox.Show("没有数据！");
            }
        }
        #region 窗体加载
        private void MIS_Import_Load(object sender, EventArgs e)
        {
            this.btnDaoChu.Enabled = false;
            dataGridView2.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 10);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 10);
            dt = jvdal.JVSelectImport();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmb_company.DataSource = dt;
                cmb_company.DisplayMember = "cb";
                cmb_company.ValueMember = "CoCd";
            }

            dts = importdal.ImportSelect().Clone();//临时数据格式

        }
        #endregion
        #region 匹配
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-09-18
        /// 修改目的：完善数据包导入的 匹配功能
        /// 匹配
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_jy_Click(object sender, EventArgs e)
        {
            this.but_jy.Enabled = false;//匹配
            dt = importdal.ImportCheckSelect(cmb_company.SelectedValue.ToString().Trim());//替换AccountCode 和T5 ，T5为空的没有被替换
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.button1.Enabled = true;//l浏览
            DBHelper.SqlHelp.InsertTable(dt, "T_imp_raw");//将Datatable中的数批量导入到数据库的表中 ydx
           // importdal.ImportDelete();
            textBox1.Clear();
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.but_Verify.Enabled = false;//校验
            
            MessageBox.Show("匹配成功！");
        }
        #endregion
        #region 验证
        /// <summary>
        /// 验证
        /// 修改人：ydx
        /// 修改时间：2014-09-18
        /// 修改目的：数据包导入 验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_Verify_Click(object sender, EventArgs e)
        {
            //李晓光做的部分
            //this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            //this.button1.Enabled = false;
            //this.but_jy.Enabled = false;

            //string _date = dtp.Text.ToString();
            //string[] One_date = _date.Split(new Char[] { '-' });
            //string AccountPeriod = One_date[0] + "-0" + One_date[1];
            //DataTable dtVerify = new DataTable();
            //dtVerify = importdal.ImportCheckExits(AccountPeriod);
            //if (dtVerify.Rows.Count == 0)
            //{
            //    MessageBox.Show("验证通过，可以进行下一步匹配！");
            //}
            //else
            //{
            //    MessageBox.Show("验证未通过！");
            //    dataGridView2.DataSource = dtVerify;
            //    importdal.ImportDelete();
            //}
            //this.Cursor = System.Windows.Forms.Cursors.Default;
            //this.button1.Enabled = true;
            //this.but_jy.Enabled = true;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.button1.Enabled = false;
            this.but_jy.Enabled = false;

            string cocd = cmb_company.SelectedValue.ToString().Trim();//获取合资公司参数
           
            DataTable dtAccount = importdal.checkAccount(cocd);//验证AccountCode
            dtCF = dtAccount.Clone();
            if (dtAccount.Rows.Count > 0)
            {
                dtAccount.Rows[0][0] = "No:AC";
                dtCF.Merge(dtAccount);
            }
            #region 验证T
            try
            {
                DataTable dtT0 = importdal.checkT0(cocd);//验证T0
                if (dtT0.Rows.Count > 0)
                {
                    dtT0.Rows[0][0] = "No:T0";
                    dtCF.Merge(dtT0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("T0:" + ex.Message);
            }
            try
            {
                DataTable dtT3 = importdal.checkT3(cocd);//验证T3
                if (dtT3.Rows.Count > 0)
                {
                    dtT3.Rows[0][0] = "No:T3";
                    dtCF.Merge(dtT3);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("T3:" + ex.Message);
            }

            try
            {
                DataTable dtT5 = importdal.checkT5(cocd);//验证T5JV部分
                if (dtT5.Rows.Count > 0)
                {
                    dtT5.Rows[0][0] = "No:T5_JV";
                    dtCF.Merge(dtT5);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("T5:" + ex.Message);
            }

            try
            {
                DataTable dtT5st = importdal.checkT5Site(cocd);//验证T5Site部分
                if (dtT5st.Rows.Count > 0)
                {
                    dtT5st.Rows[0][0] = "No:T5_S";
                    dtCF.Merge(dtT5st);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("TT5_s:" + ex.Message);
            }
            try
            {
                DataTable dtT8 = importdal.checkT8(cocd);//验证t8
                if (dtT8.Rows.Count > 0)
                {
                    dtT8.Rows[0][0] = "No:T8";
                    dtCF.Merge(dtT8);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("T8:" + ex.Message);

            }

            try
            {

                DataTable dtT1 = importdal.checkT1(cocd);//验证T1
                if (dtT1.Rows.Count > 0)
                {
                    dtT1.Columns[0].ReadOnly = false;
                    dtT1.Rows[0][0] = "No:T1";
                    dtCF.Merge(dtT1);
                }
                //DataTable dtT1 = importdal.checkT1(cocd);//验证T1
                //if (dtT1.Rows.Count > 0)
                //{
                //    dtT1.Columns[1].ReadOnly = false;
                //    dtT1.Rows[0][1] = "No:T1";
                //    dtCF.Merge(dtT1);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("T1:" + ex.Message);

            }
            try
            {
                DataTable dtT2 = importdal.checkT2(cocd);//验证t2
                if (dtT2 != null && dtT2.Rows.Count > 0)
                {
                    dtT2.Columns[0].ReadOnly = false;
                    dtT2.Rows[0][0] = "No:T2";
                    dtCF.Merge(dtT2);
                }
                //DataTable dtT2 = importdal.checkT2(cocd);//验证t2
                //if (dtT2 != null && dtT2.Rows.Count > 0)
                //{
                //    dtT2.Columns[1].ReadOnly = false;
                //    dtT2.Rows[0][1] = "No:T2";
                //    dtCF.Merge(dtT2);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("T2:" + ex.Message);

            }
            #endregion


            if (dtCF.Rows.Count > 0)
            {
                importdal.ImportDelete();

                MessageBox.Show("没有通过验证，不符合的数据如页面所示！");
                btnDaoChu.Enabled = true;//导出按钮可用
                button1.Enabled = false;//浏览按钮不可用
                but_Verify.Enabled = false;//校验按钮不可用
                but_jy.Enabled = false;// 匹配按钮不可用
               dataGridView2.DataSource = null;
                dataGridView2.DataSource = dtCF;
              
                #region 操作日志记录
                int result = logDal.OpertionLogInsert(UserID, "Import 数据验证", DateTime.Now.ToString(), "未通过验证");
                if (result <= 0)
                {
                    MessageBox.Show("日志插入失败！");

                }
                #endregion
            }
            else
            {
                MessageBox.Show("验证通过，请匹配！");
                but_jy.Enabled = true;
                but_Verify.Enabled = false;
                this.button1.Enabled = false;

                #region 操作日志记录
                int result = logDal.OpertionLogInsert(UserID, "Import 数据验证", DateTime.Now.ToString(), "通过验证");
                if (result <= 0)
                {
                    MessageBox.Show("日志插入失败！");

                }
                #endregion
                
            }
            this.Cursor = System.Windows.Forms.Cursors.Default;
           // this.button1.Enabled = true;
            //this.but_jy.Enabled = true;
        }
        #endregion
        #region 导出
        private void Excel()
        {
            if (this.dataGridView2.RowCount - 1 != 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl   files   (*.xls)|*.xls";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "导出Excel文件到";
                saveFileDialog.FileName = "JV Setting";
                saveFileDialog.ShowDialog();
                Stream myStream;
                myStream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(0));
                string str = "";
                try
                {
                    //写标题     
                    for (int i = 0; i < dataGridView2.Columns.Count; i++)
                    {
                        if (i > 0)
                        {
                            str += "\t";
                        }
                        str += dataGridView2.Columns[i].HeaderText;
                    }
                    sw.WriteLine(str);
                    //写内容   
                    for (int j = 0; j < dataGridView2.RowCount - 1; j++)
                    {
                        string tempStr = "";
                        for (int k = 0; k < dataGridView2.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                tempStr += "\t";
                            }
                            tempStr += dataGridView2[k, j].Value.ToString();
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
                #region 操作日志记录
                int result = logDal.OpertionLogInsert(UserID, "Import 数据导出", DateTime.Now.ToString(), "请先在该表中导入数据");
                if (result <= 0)
                {
                    MessageBox.Show("日志插入失败！");

                }
                #endregion
            }
        }
        #endregion

        //使用npoi到处不符合的数据
        private void btnDaoChu_Click(object sender, EventArgs e)
        {
            #region npoi导出
            if (dtCF.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据可导出");
                int result = logDal.OpertionLogInsert(UserID, "数据包数据导出", DateTime.Now.ToString(), "数据包没有数据可导出");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {
                ImportToExcel.Export(dtCF, "数据包不符合的数据", "数据包不符合的数据", "", DateTime.Now.ToString());
               dtCF.Clear();
                int result = logDal.OpertionLogInsert(UserID, "数据包数据导出", DateTime.Now.ToString(), "数据包不符合的数据导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            #endregion
        }


    }
}

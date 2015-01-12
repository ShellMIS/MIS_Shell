using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using Microsoft.Office.Interop;
using DAL;
using MIS_Shell.CommExcel;
using System.Threading;
using System.Text.RegularExpressions;

namespace MIS_Shell
{

    public partial class BSSetting : Form
    {
        public BSSetting()
        {
            InitializeComponent();
            //System.Drawing.Rectangle rect = System.Windows.Forms.Screen.PrimaryScreen.Bounds;

            //int h = rect.Height; //高（像素）

            //int w = rect.Width;  //宽（像素）            

            //this.Size = new Size(w, h);
        }

        BSDal bsdal = new BSDal();
        DataTable dt = new DataTable();
        ImportToExcel imp = new ImportToExcel();//导出事件 2014-08-25ydx
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string fileName = fd.FileName;
                bind(fileName);
            }
        }
        private void bind(string fileName)
        {
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT *  FROM [BS mapping$]", strConn);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                dt = ds.Tables[0];
                this.txt_Path.Text = fileName.ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "BSSetting数据绑定", DateTime.Now.ToString(), "BSSetting数据绑定失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-08-25
        /// 修改目的：导入进行重复数据监测并将重复数据导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dtCh = null;
            if (dt.Rows.Count > 0)
            {
                dtCh = dt.Clone();
                DataRow dr = null;
                bool flag = true;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    if (bsdal.BSSelect(dr[0].ToString().Trim(), dr[1].ToString().Trim(), dr[2].ToString().Trim(), dr[3].ToString().Trim(), dr[4].ToString().Trim(), dr[5].ToString().Trim(), dr[6].ToString().Trim(), int.Parse(dr[7].ToString().Trim())).Rows.Count > 0)
                    {
                        dtCh.ImportRow(dr);
                        flag = false;
                        // break;
                    }
                }
                if (flag)
                {
                   // DBHelper.SqlHelp.InsertTable(dt, "T_BSSetting");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];

                        insertToSql(dr);
                    }
                    MessageBox.Show("导入成功");
                    int result = logDal.OpertionLogInsert(userid, "BSSetting导入操作", DateTime.Now.ToString(), "BSSetting数据导入成功");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("有重复数据，将重复数据保存到以下位置！");
                    if (dtCh.Rows.Count > 0)
                    {
                        //this.dataGrdCh.DataSource = dtCh;
                        if (imp.tableToExcel(dtCh, "BS mapping重复数据") == true)
                        {
                            MessageBox.Show("重复数据已导出！");
                            int result = logDal.OpertionLogInsert(userid, "BSSetting导入进行重复数据监测并将重复数据导出", DateTime.Now.ToString(), "BSSetting重复数据导出成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("导出有异常！");
                            int result = logDal.OpertionLogInsert(userid, "BSSetting导入进行重复数据监测并将重复数据导出", DateTime.Now.ToString(), "BSSetting重复数据导出异常");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }

                    }
                }
                this.dataGridView2.DataSource = bsdal.BSSelect();
            }
            else
            {
                MessageBox.Show("没有数据！");
                int result = logDal.OpertionLogInsert(userid, "BSSetting导入进行重复数据监测并将重复数据导出", DateTime.Now.ToString(), "BSSetting没有数据");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }

        private void insertToSql(DataRow dr)
        {
            string ReportType = dr["ReportType"].ToString();
            string AccGroup = dr["AccGroup"].ToString();
            string AccSubGroup = dr["AccSubGroup"].ToString();
            string AccType = dr["AccType"].ToString();
            string AccSubType = dr["AccSubType"].ToString();
            string AccountCode = dr["AccountCode"].ToString();
            string Account_Description = dr["Account_Description"].ToString();
            int sort = int.Parse(dr["SortField"].ToString().Trim());
            bsdal.BSInsert(ReportType, AccGroup, AccSubGroup, AccType, AccSubType, AccountCode, Account_Description, sort);
        }

        private void BSSetting_Load(object sender, EventArgs e)
        {
            BackgroundWorker getdata = new System.ComponentModel.BackgroundWorker();//定义一个backGroundWorker
            getdata.WorkerSupportsCancellation = true;//设置能否取消任务
            getdata.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_Combo_DoWork);//让backgroundWorker做的事
            getdata.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_Combo_RunWorkerCompleted);
            getdata.RunWorkerAsync();


            // this.dataGridView2.DataSource = bsdal.BSSelect();
            dataGridView2.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView2.Font = new System.Drawing.Font("Arial", 10);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);

            foreach (DataGridViewColumn item in dataGridView2.Columns)
            {
                if (item.Name.Trim() == "id" || item.Name.Trim() == "AccountCode" || item.Name.Trim() == "createdby" || item.Name.Trim() == "Createddate" || item.Name.Trim() == "Modifyby" || item.Name.Trim() == "Modifydate" || item.Name.Trim() == "orderby")
                {
                    item.DefaultCellStyle.Font = new Font("Arial", 10);
                }
                else
                {
                    item.DefaultCellStyle.Font = new Font("宋体", 10);
                }
            }


        }
        private void button1_Click(object sender, EventArgs e)
        {

            #region npoi导出
            DataTable dt = (DataTable)dataGridView2.DataSource;
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据可导出");
                int result = logDal.OpertionLogInsert(userid, "BSSetting数据导出操作", DateTime.Now.ToString(), "BSSetting没有数据可以导出");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {
                ImportToExcel.Export(dt, "BSSetting Report", "BSSetting Report", "", DateTime.Now.ToString());
                int result = logDal.OpertionLogInsert(userid, "BSSetting数据导出操作", DateTime.Now.ToString(), "BSSetting数据导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            #endregion
            #region 以前
            //if (imp.toExcel(dataGridView2, "BS mapping") == true)
            //{
            //    MessageBox.Show("导出成功");
            //    int result = logDal.OpertionLogInsert(userid, "BSSetting数据导出操作", DateTime.Now.ToString(), "BSSetting数据导出成功");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("没有数据可导出");
            //    int result = logDal.OpertionLogInsert(userid, "BSSetting数据导出操作", DateTime.Now.ToString(), "BSSetting没有数据可以导出");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
            #endregion

            //if (dataGridView2.RowCount - 1 != 0)
            //{
            //    SaveFileDialog saveFileDialog = new SaveFileDialog();
            //    saveFileDialog.Filter = "Execl   files   (*.xls)|*.xls";
            //    saveFileDialog.FilterIndex = 0;
            //    saveFileDialog.RestoreDirectory = true;
            //    saveFileDialog.CreatePrompt = true;
            //    saveFileDialog.Title = "导出Excel文件到";
            //    saveFileDialog.FileName = "BS mapping";
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

        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                dataGridView2.Rows[e.RowIndex].Selected = true;
            }
        }

        //private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    int row = dataGridView2.SelectedRows.Count;
        //    if (row == 0)
        //    {
        //        MessageBox.Show("没有选中任何行", "Error");
        //        return;
        //    }
        //    else if (MessageBox.Show("确认删除选中的" + row.ToString() + "条记录吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //    {
        //        for (int i = dataGridView2.SelectedRows.Count; i > 0; i--)
        //        {
        //            int rno = dataGridView2.SelectedRows[i - 1].Index;
        //            if (Convert.ToBoolean(dataGridView2.Rows[rno].Cells[0].Value) == true)//全选的时候 删除最后一行会报错 如果不添加这行代码
        //            {

        //                string id = dataGridView2.Rows[rno].Cells[0].Value.ToString();
        //                if (bsdal.BSDelete(id) == 1)
        //                {
        //                    dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[i - 1].Index);
        //                    int result = logDal.OpertionLogInsert(userid, "BSSetting删除操作", DateTime.Now.ToString(), "BSSetting删除成功");
        //                    if (result < 0)
        //                    {
        //                        MessageBox.Show("日志插入失败！");
        //                        return;
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("操作失败！");
        //                    int result = logDal.OpertionLogInsert(userid, "BSSetting删除操作", DateTime.Now.ToString(), "BSSetting删除操作失败");
        //                    if (result < 0)
        //                    {
        //                        MessageBox.Show("日志插入失败！");
        //                        return;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
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
                string id = "";
                for (int i = dataGridView2.SelectedRows.Count; i > 0; i--)
                {

                    int rno = dataGridView2.SelectedRows[i - 1].Index;
                    if (Convert.ToBoolean(dataGridView2.Rows[rno].Cells[0].Value) == true)//全选的时候 删除最后一行会报错 如果不添加这行代码
                    {

                        id += "'" + dataGridView2.Rows[rno].Cells[0].Value.ToString() + "'" + ',';
                        #region ydx删除代码
                        // id+=dataGridView2.Rows[rno].Cells[0].Value.ToString()+",";
                        //  id += "'+dataGridView2.Rows[rno].Cells[0].Value.ToString()+",";

                        //if (bsdal.BSDelete(id.Substring(0,id.Length-2)) == 1)
                        //{
                        //   dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[i - 1].Index);
                        //    #region 日志
                        //    int result = logDal.OpertionLogInsert(userid, "BSSetting删除操作", DateTime.Now.ToString(), "BSSetting删除成功");
                        //    if (result < 0)
                        //    {
                        //        MessageBox.Show("日志插入失败！");
                        //        return;
                        //    }
                        //    #endregion


                        //}
                        //else
                        //{
                        //    MessageBox.Show("操作失败！");
                        //    int result = logDal.OpertionLogInsert(userid, "BSSetting删除操作", DateTime.Now.ToString(), "BSSetting删除操作失败");
                        //    if (result < 0)
                        //    {
                        //        MessageBox.Show("日志插入失败！");
                        //        return;
                        //    }
                        //}
                        #endregion

                    }

                }
                #region hxy修改删除

                if (bsdal.BSDelete(id.Substring(1, id.Length - 3)) == 1)
                {
                    this.dataGridView2.DataSource = bsdal.BSSelect();
                    //dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[i - 1].Index);
                    #region 日志
                    int result = logDal.OpertionLogInsert(userid, "BSSetting删除操作", DateTime.Now.ToString(), "BSSetting删除成功");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                    #endregion


                }
                else
                {
                    MessageBox.Show("操作失败！");
                    int result = logDal.OpertionLogInsert(userid, "BSSetting删除操作", DateTime.Now.ToString(), "BSSetting删除操作失败");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }

                }
                #endregion


            }
        }
        private void But_Insert_Click(object sender, EventArgs e)
        {
            if (txt_ReportType.Text.ToString().Trim() == "" || txt_AccGroup.Text.ToString().Trim() == "" || txt_AccSubGroup.Text.ToString().Trim() == "" || txt_AccType.Text.ToString().Trim() == "" || txt_AccSubType.Text.ToString().Trim() == "" || txt_AccountCode.Text.ToString().Trim() == "" || SortField.Text.ToString().Trim() == "")
            {
                MessageBox.Show("有空白项，请填写完整");
            }
            else
            {
                string ReportType = this.txt_ReportType.Text.ToString().Trim();
                string AccGroup = this.txt_AccGroup.Text.ToString().Trim();
                string AccSubGroup = this.txt_AccSubGroup.Text.ToString().Trim();
                string AccType = this.txt_AccType.Text.ToString().Trim();
                string AccSubType = this.txt_AccSubType.Text.ToString().Trim();
                string AccountCode = this.txt_AccountCode.Text.ToString().Trim();
                string Account_Description = this.txt_AccountD.Text.ToString().Trim();
                int sort = int.Parse(SortField.Text.ToString().Trim());//排序

                if (bsdal.BSInsert(ReportType, AccGroup, AccSubGroup, AccType, AccSubType, AccountCode, Account_Description, sort) == 1)
                {
                    MessageBox.Show("增加成功！");
                    this.txt_ReportType.Text = "";
                    this.txt_AccGroup.Text = "";
                    this.txt_AccSubGroup.Text = "";
                    this.txt_AccType.Text = "";
                    this.txt_AccSubType.Text = "";
                    this.txt_AccountCode.Text = "";
                    this.txt_AccountD.Text = "";
                    int result = logDal.OpertionLogInsert(userid, "BSSetting添加操作", DateTime.Now.ToString(), "BSSetting添加成功");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("操作失败！");
                    int result = logDal.OpertionLogInsert(userid, "BSSetting添加操作", DateTime.Now.ToString(), "BSSetting添加操作失败");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
                dt = bsdal.BSSelect();
                this.dataGridView2.DataSource = dt;
            }

        }

        private void but_Select_Click(object sender, EventArgs e)
        {
            try
            {
                string ReportType = this.txt_SReportType.Text.ToString().Trim();
                string AccGroup = this.txt_SAccGroup.Text.ToString().Trim();
                string AccSubGroup = this.txt_SAccSubGroup.Text.ToString().Trim();
                string AccType = this.txt_SAccType.Text.ToString().Trim();
                string AccSubType = this.txt_SAccSubType.Text.ToString().Trim();
                string AccountCode = this.txt_SAccountCode.Text.ToString().Trim();
                string Account_Description = this.txt_SAccountD.Text.ToString().Trim();
                if (!Regex.IsMatch(sortFields.Text.ToString().Trim(), "^[1-9]d*$"))
                {
                    MessageBox.Show("sortField 需填写整数");
                    return;
                };
                int sort = int.Parse(sortFields.Text.ToString().Trim());


                this.dataGridView2.DataSource = bsdal.BSSelect(ReportType, AccGroup, AccSubGroup, AccType, AccSubType, AccountCode, Account_Description, sort);
                int result = logDal.OpertionLogInsert(userid, "BSSetting查询操作", DateTime.Now.ToString(), "BSSetting查询操作成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "BSSetting查询操作", DateTime.Now.ToString(), "BSSetting查询操作失败");
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
            UpdateBS updatebs = new UpdateBS();
            updatebs.Owner = this;
            updatebs.ShowDialog();
            this.dataGridView2.DataSource = bsdal.BSSelect();
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-08-01
        /// 添加目的：模版下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_down_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Excel模版文件到";
            saveFileDialog.FileName = "BS mapping.xlsx";
            saveFileDialog.ShowDialog();
            string downpath = Application.StartupPath + "\\Model\\BS mapping.xlsx";
            System.IO.File.Delete(saveFileDialog.FileName);
            System.IO.File.Copy(downpath, saveFileDialog.FileName);
            MessageBox.Show("下载模版成功！");
            int result = logDal.OpertionLogInsert(userid, "BSSetting导出excel模板文件操作", DateTime.Now.ToString(), "BSSetting导出excel模板文件成功");
            if (result < 0)
            {
                MessageBox.Show("日志插入失败！");
                return;
            }
        }


        private void backgroundWorker_Combo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.dataGridView2.DataSource = dt;
        }

        private void backgroundWorker_Combo_DoWork(object sender, DoWorkEventArgs e)
        {

            dt = bsdal.BSSelect();

        }


    }
}

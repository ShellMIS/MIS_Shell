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
    /// 修改人：ydx
    /// 修改时间：2014-08-25
    /// 修改目的：导入重复数据导出
    /// </summary>
    public partial class TCodeSetting : Form
    {
        public TCodeSetting()
        {
            InitializeComponent();
        }
        TCodeDal tcodedal = new TCodeDal();
        JVDal jvdal = new JVDal();
        DataTable dt = new DataTable();
        ImportToExcel imp = new ImportToExcel();//数据导出函数
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
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT *  FROM [TCode$]", strConn);
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
                int result = logDal.OpertionLogInsert(userid, "TCodeSetting绑定操作", DateTime.Now.ToString(), "TCodeSetting绑定操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-08-14
        /// 修改目的：重复数据导入问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            DataTable dtCh = null;
            if (dt.Rows.Count > 0)
            {
                dtCh = dt.Clone();
                DataRow dr = null;
                bool flag = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    if (tcodedal.TCodeSelect(dr[0].ToString().Trim(), dr[1].ToString().Trim(), dr[2].ToString().Trim(), dr[3].ToString().Trim()).Rows.Count > 0)
                    {
                        dtCh.ImportRow(dr);
                        flag = true;
                    }
                }
                if(flag)
                {
                    MessageBox.Show("有重复数据，将重复数据保存到以下位置！");
                    if (dtCh.Rows.Count > 0)
                    {
                        if (imp.tableToExcel(dtCh, "TCode Setting重复数据") == true)
                        {
                            MessageBox.Show("已经将重复数据导出！");
                            int result = logDal.OpertionLogInsert(userid, "TCodeSetting重复数据导入操作", DateTime.Now.ToString(), "TCodeSetting已经将重复数据导出");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("重复数据导出异常！");
                            int result = logDal.OpertionLogInsert(userid, "TCodeSetting重复数据导入操作", DateTime.Now.ToString(), "TCodeSetting重复数据导出异常");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                    }
                   // MessageBox.Show("已经存在此记录："+dr[0]+"->"+dr[1]+"->"+dr[2]+"->"+dr[3]); 
                }else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        insertToSql(dr);
                    }
                    MessageBox.Show("导入成功！");
                    int result = logDal.OpertionLogInsert(userid, "TCodeSetting重复数据导入操作", DateTime.Now.ToString(), "TCodeSetting重复数据导入成功");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
                this.dataGridView2.DataSource = tcodedal.TCodeSelect();

            }
            else
            {
                MessageBox.Show("没有数据！");
                int result = logDal.OpertionLogInsert(userid, "TCodeSetting重复数据导入操作", DateTime.Now.ToString(), "TCodeSetting没有数据");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            } 
        }
        private void insertToSql(DataRow dr)
        {
            string TcodeType = dr["Tcode Type"].ToString();
            string Tcode = dr["Tcode"].ToString();
            string TcodeName = dr["Tcode Name"].ToString();
            string CoCd = dr["CoCd"].ToString();
            tcodedal.TCodeInsert(TcodeType, Tcode, TcodeName,CoCd);
        }

        private void TCodeSetting_Load(object sender, EventArgs e)
        {
            this.dataGridView2.DataSource = tcodedal.TCodeSelect();
            dataGridView2.Font=new System.Drawing.Font("Arial",10);

            DataTable jv = new DataTable();
            jv = jvdal.JVSelectImport();
            for (int j = 0; j < jv.Rows.Count; j++)
            {
                cb_CoCd.DataSource = jv;
                cb_CoCd.DisplayMember = "cb";
                cb_CoCd.ValueMember = "CoCd";

                cb_Scocd.DataSource = jv;
                cb_Scocd.DisplayMember = "cb";
                cb_Scocd.ValueMember = "CoCd";
            }

            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                if (col.Name!="TcodeName")
                {
                    col.DefaultCellStyle.Font = new Font("Arial", 10);
                }
              
            }
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            
        }
        //导出数据到Excel文件
        private void button1_Click(object sender, EventArgs e)
        {
            #region 修改npoi导出
            DataTable dt = (DataTable)dataGridView2.DataSource;
            if (dt.Rows.Count<=0)
            {
                MessageBox.Show("没有数据可导出");
                int result = logDal.OpertionLogInsert(userid, "TCodeSetting导出数据到excel操作", DateTime.Now.ToString(), "没有数据可导出");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {
                string JVCompeny = cb_Scocd.GetItemText(cb_Scocd.Items[cb_Scocd.SelectedIndex]);
                ImportToExcel.Export(dt, "TCodeSetting Report", "TCodeSetting Report", JVCompeny, DateTime.Now.ToString());
                int result = logDal.OpertionLogInsert(userid, "TCodeSetting导出数据到excel操作", DateTime.Now.ToString(), "导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        
            #endregion
            #region 以前的导出
            //if (imp.toExcel(dataGridView2, "TCode Setting") == true)
            //{
            //    MessageBox.Show("导出成功");
            //    int result = logDal.OpertionLogInsert(userid, "TCodeSetting导出数据到excel操作", DateTime.Now.ToString(), "导出成功");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("没有数据可导出");
            //    int result = logDal.OpertionLogInsert(userid, "TCodeSetting导出数据到excel操作", DateTime.Now.ToString(), "没有数据可导出");
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

            //    saveFileDialog.FileName = "TCode Setting";

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
                        if (tcodedal.TCodeDelete(id) == 1)
                        {
                            dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[i - 1].Index);
                            int result = logDal.OpertionLogInsert(userid, "TCodeSetting删除操作", DateTime.Now.ToString(), "删除成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("操作失败！");
                            int result = logDal.OpertionLogInsert(userid, "TCodeSetting删除操作", DateTime.Now.ToString(), "操作失败");
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
        /// 修改人：ydx
        /// 修改时间：2014-08-14
        /// 修改目的：重复录入问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_Insert_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            if (txt_TT.Text.ToString().Trim() == "" || txt_Tcode.Text.ToString().Trim() == "" || txt_TN.Text.ToString().Trim()=="")
            {
                MessageBox.Show("有空白信息，请填写完整");
            }else
            {
                 string TcodeType = txt_TT.Text.ToString().Trim();
                string Tcode = txt_Tcode.Text.ToString().Trim();
                string TcodeName = txt_TN.Text.ToString().Trim();
                string CoCd = cb_CoCd.SelectedValue.ToString();
                DataTable dtExists = tcodedal.TCodeSelect(TcodeType, Tcode, TcodeName, CoCd);
                if(dtExists.Rows.Count>0)
                {
                    MessageBox.Show("已经存在此记录："+TcodeType+"->"+Tcode+"->"+TcodeName+"->"+CoCd);
                    int result = logDal.OpertionLogInsert(userid, "TCodeSetting重复录入操作", DateTime.Now.ToString(), "已经存在此记录");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }else
                {
                    if (tcodedal.TCodeInsert(TcodeType, Tcode, TcodeName, CoCd) == 1)
                    {
                        MessageBox.Show("增加成功！");
                        txt_TT.Text = "";
                        txt_Tcode.Text = "";
                        txt_TN.Text = "";
                        int result = logDal.OpertionLogInsert(userid, "TCodeSetting添加操作", DateTime.Now.ToString(), "增加成功");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                        int result = logDal.OpertionLogInsert(userid, "TCodeSetting添加操作", DateTime.Now.ToString(), "操作失败");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                }
                
                this.dataGridView2.DataSource = tcodedal.TCodeSelect();
            }
        }

        private void but_Select_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);

            try
            {
                string TcodeType = txt_STT.Text.ToString().Trim();
                string Tcode = txt_STcode.Text.ToString().Trim();
                string TcodeName = txt_STN.Text.ToString().Trim();
                string CoCd = cb_Scocd.SelectedValue.ToString();
  
                this.dataGridView2.DataSource = tcodedal.TCodeSelect(TcodeType, Tcode, TcodeName, CoCd);
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }
        }

        public static int ID;
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            UpdateTCode tcode = new UpdateTCode();
            tcode.Owner = this;
            tcode.ShowDialog();
            this.dataGridView2.DataSource = tcodedal.TCodeSelect();
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-08-01
        /// 添加目的：下载模版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_down_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Excel模版文件到";
            saveFileDialog.FileName = "TCode Setting.xlsx";
            saveFileDialog.ShowDialog();
            string downpath = Application.StartupPath + "\\Model\\TCode Setting.xlsx";
            System.IO.File.Delete(saveFileDialog.FileName);
            System.IO.File.Copy(downpath, saveFileDialog.FileName);
            MessageBox.Show("下载模版成功！");
            int result = logDal.OpertionLogInsert(userid, "TCodeSetting下载模版操作", DateTime.Now.ToString(), "下载模版成功");
            if (result < 0)
            {
                MessageBox.Show("日志插入失败！");
                return;
            }
        }

       
    }
}

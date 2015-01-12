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
using System.Net;
using MIS_Shell.CommExcel;

namespace MIS_Shell
{
    /// <summary>
    /// 修改人：ydx
    /// 修改时间：2014-08-25
    /// 修改目的：数据导入，导出
    /// </summary>
    public partial class JV_COASetting : Form
    {
        public JV_COASetting()
        {
            InitializeComponent();
        }
        JV_COADal jvcoadal = new JV_COADal();
        COADal coadal = new COADal();
        JVDal jvdal = new JVDal();
        DataTable dt = new DataTable();
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        ImportToExcel imp = new ImportToExcel();//数据导出 ydx  2014-08-25
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
            dt = null;
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT *  FROM [mapping$]", strConn);
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
                int result = logDal.OpertionLogInsert(userid, "JV_COASetting绑定数据", DateTime.Now.ToString(), "JV_COASetting绑定数据失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }

        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-08-15
        /// 修改目的：导入的过程中做重复数据判断
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
                bool flag = true;
                //为dt添加自增列id
                dt.Columns.Add("Id", typeof(int));
                dt.Columns["Id"].SetOrdinal(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][0] = i + 1;
                }
               // DataTable dtJvCoa = jvcoadal.JV_COASelect("");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    if (dr[0].ToString().Trim() != "" && dr[1].ToString().Trim() != "" && dr[2].ToString().Trim() != "" && dr[3].ToString().Trim() != "")
                    {
                        if (jvcoadal.JV_COASelects(dr[0].ToString().Trim(), dr[1].ToString().Trim(), dr[2].ToString().Trim(), dr[3].ToString().Trim()).Rows.Count > 0)
                        {
                            flag = false;
                            dtCh.ImportRow(dr);//吧重复数据填到内存表dtCH
                        }
                    }
                }

                bool bb = flag;
                int b = dtCh.Rows.Count;
                if (flag)
                {
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    if (dr[0].ToString().Trim() != "" && dr[1].ToString().Trim() != "" && dr[2].ToString().Trim() != "" && dr[3].ToString().Trim() != "")
                    //    {
                    //        dr = dt.Rows[i];
                    //        insertToSql(dr);                

                    //dt.Columns["Id"].AutoIncrement = true;
                    //dt.Columns["Id"].AutoIncrementSeed = 1;
                    //dt.Columns["Id"].AutoIncrementStep = 1;
                    DBHelper.SqlHelp.InsertTable(dt, "JV_COASetting");
                    //     }
                    //  }
                    MessageBox.Show("导入成功！");

                }
                else
                {
                    MessageBox.Show("有重复数据，将重复数据保存到以下位置！");
                    if (dtCh.Rows.Count > 0)
                    {
                        if (imp.tableToExcel(dtCh, "JV_COA Setting重复数据") == true)
                        {
                            MessageBox.Show("已经将重复数据导出！");
                            int result = logDal.OpertionLogInsert(userid, "JV_COASetting导出操作", DateTime.Now.ToString(), "JV_COASetting重复数据已导出");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("重复数据导出异常！");
                            int result = logDal.OpertionLogInsert(userid, "JV_COASetting导出操作", DateTime.Now.ToString(), "JV_COASetting导出有异常");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }

                    }
                    // MessageBox.Show("已经存在此记录："+dr[0]+"->"+dr[1]+"->"+dr[2]+"->"+dr[3]);
                }
                this.dataGridView2.DataSource = jvcoadal.JV_COASelect();
            }
            else
            {
                MessageBox.Show("没有数据！");
                int result = logDal.OpertionLogInsert(userid, "JV_COASetting导出操作", DateTime.Now.ToString(), "JV_COASetting没有数据");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
        private void insertToSql(DataRow dr)
        {
            string AccountCode = dr["AccountCode"].ToString();
            string Account_Description = dr["Account_Description"].ToString();
            string Account = dr["SCLAccountCode"].ToString();
            string CoCd = dr["CoCd"].ToString();
            jvcoadal.JV_COAInsert(AccountCode, Account_Description, Account, CoCd);
        }

        private void JV_COASetting_Load(object sender, EventArgs e)
        {
            //修改人：ydx
            //修改时间：2014-08-15
            //修改目的：accountcode
            dt = coadal.COASelectcb();
            DataRow dr = dt.NewRow();
            dr["cb"] = "请选择";
            dr["Account"] = "0";
            dt.Rows.InsertAt(dr, 0);
            cbAccountCode1.DataSource = dt;//添加
            cbAccountCode1.DisplayMember = "cb";
            cbAccountCode1.ValueMember = "Account";

            cb_AccountCode.DataSource = dt;//修改
            cb_AccountCode.DisplayMember = "cb";
            cb_AccountCode.ValueMember = "Account";

            //修改人：ydx
            //修改时间：2014-08-15
            //修改目的：cocd
            DataTable jv = new DataTable();
            jv = jvdal.JVSelectImport();
            dr = jv.NewRow();
            dr["cb"] = "请选择";
            dr["Cocd"] = "0";
            jv.Rows.InsertAt(dr, 0);
            cb_CoCd.DataSource = jv;//添加
            cb_CoCd.DisplayMember = "cb";
            cb_CoCd.ValueMember = "CoCd";

            cb_SCoCd.DataSource = jv;//修改
            cb_SCoCd.DisplayMember = "cb";
            cb_SCoCd.ValueMember = "CoCd";

            //解决打开此页面数据加载慢的问题
            // this.dataGridView2.DataSource = jvcoadal.JV_COASelect();
            // dataGridView2.Font = new System.Drawing.Font("Arial", 10);
            // dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            // dataGridView2.RowsDefaultCellStyle.Font = new System.Drawing.Font("Arial", 10);
            // dataGridView2.Columns["SCL_Account_Description"].DefaultCellStyle.Font = new System.Drawing.Font("宋体", 10);
            //dataGridView2.Columns["JV_Account_Description"].DefaultCellStyle.Font = new System.Drawing.Font("宋体", 10);

        }
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-08-25
        /// 修改目的：数据导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //hxy 修改
            DataTable dt = (DataTable)dataGridView2.DataSource;
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据可导出");
                int result = logDal.OpertionLogInsert(userid, "JV_COA Setting导出数据到excel操作", DateTime.Now.ToString(), "没有数据可导出");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {
                string compeny = cb_SCoCd.GetItemText(cb_SCoCd.Items[cb_SCoCd.SelectedIndex]);
                ImportToExcel.Export(dt, "JVCOASetting", "JV_COASettingReport", compeny, DateTime.Now.ToString());
                int result = logDal.OpertionLogInsert(userid, " JV_COA Setting导出数据到excel操作", DateTime.Now.ToString(), "导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }

            //if (imp.toExcel(dataGridView2, "JV_COA Setting") == true)
            //{
            //    MessageBox.Show("导出成功");
            //    int result = logDal.OpertionLogInsert(userid, " JV_COA Setting导出数据到excel操作", DateTime.Now.ToString(), "导出成功");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("没有数据可导出");
            //    int result = logDal.OpertionLogInsert(userid, "JV_COA Setting导出数据到excel操作", DateTime.Now.ToString(), "没有数据可导出");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            // }
            //if (dataGridView2.RowCount - 1 != 0)
            //{
            //    SaveFileDialog saveFileDialog = new SaveFileDialog();
            //    saveFileDialog.Filter = "Execl   files   (*.xls)|*.xls";
            //    saveFileDialog.FilterIndex = 0;
            //    saveFileDialog.RestoreDirectory = true;
            //    saveFileDialog.CreatePrompt = true;
            //    saveFileDialog.Title = "导出Excel文件到";

            //    saveFileDialog.FileName = "JV_COA Setting";

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
                        if (jvcoadal.JV_COADelete(id) == 1)
                        {
                            dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[i - 1].Index);
                            int result = logDal.OpertionLogInsert(userid, "JV_COASetting删除操作", DateTime.Now.ToString(), "删除成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("操作失败！");
                            int result = logDal.OpertionLogInsert(userid, "JV_COASetting删除操作", DateTime.Now.ToString(), "删除失败");
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
        /// 修改时间：2014-08-15
        /// 修改目的：添加无法完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_Insert_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            string AccountCode = txt_Account.Text.ToString().Trim();
            string Account_Description = txt_AccountD.Text.ToString().Trim();
            string Account = "";
            if (cbAccountCode1.SelectedValue != null && cbAccountCode1.SelectedIndex != 0)
            {
                Account = cbAccountCode1.SelectedValue.ToString();
            }
            string CoCd = "";
            if (cb_CoCd.SelectedValue != null && cb_CoCd.SelectedIndex != 0)
            {
                CoCd = cb_CoCd.SelectedValue.ToString();
            }


            if (AccountCode == "" || Account_Description == "" || CoCd == "")
            {
                MessageBox.Show("有空白项，请填写完整");
                return;
            }
            else
            {
                DataTable dtExists = jvcoadal.JV_COASelect(AccountCode, Account_Description, Account, CoCd);
                if (dtExists.Rows.Count > 0)
                {
                    MessageBox.Show("已经存在此记录" + AccountCode);
                    return;
                }
                else
                {
                    if (jvcoadal.JV_COAInsert(AccountCode, Account_Description, Account, CoCd) == 1)
                    {
                        MessageBox.Show("增加成功！");
                        int result = logDal.OpertionLogInsert(userid, "JV_COASetting增加操作", DateTime.Now.ToString(), "增加成功");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                        int result = logDal.OpertionLogInsert(userid, "JV_COASetting增加操作", DateTime.Now.ToString(), "操作失败");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }

                }
            }
            this.dataGridView2.DataSource = jvcoadal.JV_COASelect();
        }

        private void but_Select_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
                string AccountCode = txt_SAccount.Text.ToString().Trim();
                string Account_Description = txt_SAccountD.Text.ToString().Trim();
                string Account = "";
                if (cbAccountCode1.SelectedValue != null && cbAccountCode1.SelectedIndex != 0)
                {
                    Account = cbAccountCode1.SelectedValue.ToString();
                }
                string CoCd = "";
                if (cb_SCoCd.SelectedValue != null && cb_SCoCd.SelectedIndex != 0)
                {
                    CoCd = cb_SCoCd.SelectedValue.ToString();
                }

                this.dataGridView2.DataSource = jvcoadal.JV_COASelect(AccountCode, Account_Description, Account, CoCd);
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "JV_COASetting选择操作", DateTime.Now.ToString(), "操作失败");
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
            UpdateJV_COA updatejv_coa = new UpdateJV_COA();
            updatejv_coa.Owner = this;
            updatejv_coa.ShowDialog();
            this.dataGridView2.DataSource = jvcoadal.JV_COASelect();
        }

        private void DownLoad_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Excel模版文件到";
            saveFileDialog.FileName = "COA setting.xlsx";
            saveFileDialog.ShowDialog();
            string downpath = Application.StartupPath + "\\Model\\COA setting.xlsx";
            System.IO.File.Delete(saveFileDialog.FileName);
            System.IO.File.Copy(downpath, saveFileDialog.FileName);
            MessageBox.Show("导出成功！");
            int result = logDal.OpertionLogInsert(userid, "JV_COASetting下载模板", DateTime.Now.ToString(), "下载模板成功");
            if (result < 0)
            {
                MessageBox.Show("日志插入失败！");
                return;
            }
        }

        private void dataGridView2_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || dataGridView2.Rows.Count <= 0) return;
            dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? string.Empty).ToString();
        }


    }
}

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
    /// 修改时间：2014-08-15
    /// </summary>
    public partial class COASetting : Form
    {
        public COASetting()
        {
            InitializeComponent();
        }
        COADal coadal = new COADal();
        DataTable dt = new DataTable();
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        ImportToExcel imp = new ImportToExcel();//数据导出函数 2014-08-25 ydx


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
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT *  FROM [COA$] where [Account Type] is not null ", strConn);
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
                int result = logDal.OpertionLogInsert(userid, "COASetting绑定数据", DateTime.Now.ToString(), "COASetting绑定数据失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            DataTable dtCh = null;
            if (dt.Rows.Count > 0)
            {
                dtCh = dt.Clone();
                //DataRow dr = null;
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    dr = dt.Rows[i];
                //    insertToSql(dr);
                //}
                //MessageBox.Show("导入成功！");
                DataRow dr = null;
                bool flag = true;
                //查找重复记录
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    if (dr[0].ToString().Trim() == "" && dr[1].ToString().Trim() == "" && dr[2].ToString().Trim() == "" && dr[3].ToString().Trim() == "")
                    {

                    }
                    if (coadal.COASelect(dr[0].ToString().Trim(), dr[1].ToString().Trim(), dr[2].ToString().Trim(), dr[3].ToString().Trim(), "").Rows.Count > 0)
                    {
                        dtCh.ImportRow(dr);
                        flag = false;
                        // break;
                    }
                }
                if (flag)
                {
                    //没有重复记录的话，插入到数据库中
                    try
                    {
                       // DBHelper.SqlHelp.InsertTable(dt, "COASetting");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            insertToSql(dr);
                        }
                        MessageBox.Show("导入成功");
                        int result = logDal.OpertionLogInsert(userid, "COASetting导入操作", DateTime.Now.ToString(), "COASetting导入成功");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {

                        int result = logDal.OpertionLogInsert(userid, "COASetting导入操作", DateTime.Now.ToString(), "COASetting导入操作: " + ex.Message);
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }

                }
                else
                {
                    MessageBox.Show("有重复数据，将重复数据保存到以下位置！");
                    if (dtCh.Rows.Count > 0)
                    {
                        // this.dataGrdCh.DataSource = dtCh;
                        if (imp.tableToExcel(dtCh, "COA Setting重复数据") == true)
                        {
                            MessageBox.Show("重复数据已导出！");
                            int result = logDal.OpertionLogInsert(userid, "COASetting导出操作", DateTime.Now.ToString(), "COASetting重复数据已导出");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("导出有异常！");
                            int result = logDal.OpertionLogInsert(userid, "COASetting导出操作", DateTime.Now.ToString(), "COASetting导出有异常");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }

                    }
                }

                this.dataGridView2.DataSource = coadal.COASelect();
            }
            else
            {
                MessageBox.Show("没有数据！");
                int result = logDal.OpertionLogInsert(userid, "COASetting导出操作", DateTime.Now.ToString(), "COASetting没有数据");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
        private void insertToSql(DataRow dr)
        {
            string AccountType = dr["Account Type"].ToString();
            string Account = dr["AccountCode"].ToString();
            string Account_Description = dr["Account_Description"].ToString();
            string Status = dr["Status"].ToString();
            string Update = dr["Update"].ToString();
            coadal.COAInsert(AccountType, Account, Account_Description, Status, Update);
        }

        private void COASetting_Load(object sender, EventArgs e)
        {
            this.dataGridView2.DataSource = coadal.COASelect();
            //  this.dataGridView2.Font = new Font("Arial", 10);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(new Font("Arial", 10), FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn item in dataGridView2.Columns)
            {
                if (item.Name.Trim() != "Account_Description")
                {
                    item.DefaultCellStyle.Font = new Font("Arial", 10);

                }
                else
                {
                    item.DefaultCellStyle.Font = new Font("宋体", 10);
                }
            }


        }
        //导出数据到Excel文件
        private void button1_Click(object sender, EventArgs e)
        {
            #region npoi导出excel
            DataTable dt = (DataTable)dataGridView2.DataSource;
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据可导出");
                int result = logDal.OpertionLogInsert(userid, "COASetting导出数据到excel操作", DateTime.Now.ToString(), "没有数据可导出");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }

            }
            else
            {
                ImportToExcel.Export(dt, "COASetting Report", "COASetting Report", "", DateTime.Now.ToString());
                int result = logDal.OpertionLogInsert(userid, "COASetting导出数据到excel操作", DateTime.Now.ToString(), "导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            #endregion
            #region 以前的excel导出
            //if (imp.toExcel(dataGridView2, "COA Setting") == true)
            //{
            //    MessageBox.Show("导出成功");
            //    int result = logDal.OpertionLogInsert(userid, "COASetting导出数据到excel操作", DateTime.Now.ToString(), "导出成功");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("没有数据可导出");
            //    int result = logDal.OpertionLogInsert(userid, "COASetting导出数据到excel操作", DateTime.Now.ToString(), "没有数据可导出");
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

            //    saveFileDialog.FileName = "COA Setting";

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
                #region hxy 定义
                string id = "";
                bool deleteflag = false;
                #endregion

                for (int i = dataGridView2.SelectedRows.Count; i > 0; i--)
                {
                    int rno = dataGridView2.SelectedRows[i - 1].Index;
                    if (Convert.ToBoolean(dataGridView2.Rows[rno].Cells[0].Value) == true)//全选的时候 删除最后一行会报错 如果不添加这行代码
                    {

                        // id = dataGridView2.Rows[rno].Cells[0].Value.ToString();
                        #region hxy
                        id += "'" + dataGridView2.Rows[rno].Cells[0].Value.ToString() + "'" + ',';
                        #endregion
                    }
                }
                //hxy 删除
                #region MyRegion


                if (coadal.COADelete(id.Substring(1, id.Length - 3)) == 1)
                {
                    //  dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[i - 1].Index);
                    dataGridView2.DataSource = coadal.COASelect();
                    deleteflag = true;

                }
                else
                {
                    MessageBox.Show("操作失败！");
                    deleteflag = false;
                    int result = logDal.OpertionLogInsert(userid, "COASetting删除操作", DateTime.Now.ToString(), "删除失败");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
                if (deleteflag)
                {
                    int result = logDal.OpertionLogInsert(userid, "COASetting删除操作", DateTime.Now.ToString(), "删除成功");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-08-14
        /// 修改目的：重复录入，初始状态为“启用”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_Insert_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            if (txt_AT.Text.ToString().Trim() == "" || txt_Account.Text.ToString().Trim() == "" || txt_AccountD.Text.ToString().Trim() == "")
            {
                MessageBox.Show("有空白项，请填写完整");
            }
            else
            {
                string AccountType = txt_AT.Text.ToString().Trim();
                string Account = txt_Account.Text.ToString().Trim();
                string Account_Description = txt_AccountD.Text.ToString().Trim();
                string Status = "Active";
                string Update = DateTime.Now.ToString();
                if (coadal.COASelect(AccountType, Account, Account_Description, "", "").Rows.Count > 0)
                {
                    MessageBox.Show("已经存在此记录：" + AccountType + "->" + Account + "->" + Account_Description);
                }
                else
                {
                    if (coadal.COAInsert(AccountType, Account, Account_Description, Status, Update) == 1)
                    {
                        MessageBox.Show("增加成功！");
                        txt_AT.Text = "";
                        txt_Account.Text = "";
                        txt_AccountD.Text = "";
                        int result = logDal.OpertionLogInsert(userid, "COASetting增加操作", DateTime.Now.ToString(), "增加成功");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                        int result = logDal.OpertionLogInsert(userid, "COASetting增加操作", DateTime.Now.ToString(), "操作失败");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                }

                this.dataGridView2.DataSource = coadal.COASelect();

            }

        }

        private void but_Select_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            try
            {
                string AccountType = txt_SAT.Text.ToString().Trim();
                string Account = txt_SAccount.Text.ToString().Trim();
                string Account_Description = txt_SAccountD.Text.ToString().Trim();
                string Status = cb_SStatus.Text.ToString().Trim();
                string Update = txt_SUpdate.Text.ToString().Trim();
                this.dataGridView2.DataSource = coadal.COASelect(AccountType, Account, Account_Description, Status, Update);
                this.dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
                foreach (DataGridViewColumn item in dataGridView2.Columns)
                {
                    if (item.Name.Trim() != "Account_Description")
                    {
                        item.DefaultCellStyle.Font = new Font("Arial", 10);

                    }
                    else
                    {
                        item.DefaultCellStyle.Font = new Font("宋体", 10);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "COASetting选择操作", DateTime.Now.ToString(), "操作失败");
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
            UpdateCOA updatecoa = new UpdateCOA();
            updatecoa.Owner = this;
            updatecoa.ShowDialog();
            this.dataGridView2.DataSource = coadal.COASelect();
        }

        private void Down_Model_Click(object sender, EventArgs e)
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
            int result = logDal.OpertionLogInsert(userid, "COASetting下载模板", DateTime.Now.ToString(), "下载模板成功");
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

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
    /// 添加人：ydx
    /// 修改时间：2014-08-22
    /// 修改目的：数据重复导入
    /// </summary>
    public partial class PLDBSetting : Form
    {
        Pldb pd = new Pldb();
        DataTable dt = new DataTable();
        ImportToExcel importEx = new ImportToExcel();//数据导出函数 2014-08-25 ydx

        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志


        public PLDBSetting()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PLDBSetting_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = pd.PlDbSelect();
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.Font = new System.Drawing.Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);

          //  dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.DefaultCellStyle.Font = new Font("Arial", 10);
                if (item.Name.Trim().ToUpper() == "PLDB_ID")
                {
                    item.Width = 150;
                }
             
            }
        }
        /// <summary>
        /// 添加面板里的 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable pldbid = pd.PlDbSelect(int.Parse(textIDs.Text.ToString()));
                if (pldbid.Rows.Count > 0)
                {
                    MessageBox.Show("已经存在编号为" + pldbid.Rows[0]["PLDB_Id"] + "的ID了");
                }
                else
                {
                    if (txtItem.Text.ToString().Trim() == "" || textIDs.Text.ToString().Trim() == "")
                    {
                        MessageBox.Show("有空白项，请填写完整");

                    }
                    else
                    {
                        if (pd.PlDbInsert(int.Parse(this.textIDs.Text.ToString()), this.txtItem.Text.ToString().Trim()) == 1)
                        {
                            MessageBox.Show("添加成功");
                            txtItem.Text = "";
                            textIDs.Text = "";
                            int result = logDal.OpertionLogInsert(userid, "PLDBSetting添加操作", DateTime.Now.ToString(), "PLDBSetting添加操作成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("添加失败");
                            int result = logDal.OpertionLogInsert(userid, "PLDBSetting添加操作", DateTime.Now.ToString(), "PLDBSetting添加操作失败");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常" + ex.Message);
            }
            finally
            {
                this.dataGridView1.DataSource = pd.PlDbSelect();
            }
        }
        /// <summary>
        /// gridview鼠标释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除toolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.SelectedRows.Count;
            if (row == 0)
            {
                MessageBox.Show("没有选中任何行", "Error");
                return;
            }
            else if (MessageBox.Show("确认删除选中的" + row.ToString() + "条记录吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = dataGridView1.SelectedRows.Count; i > 0; i--)
                {
                    int rno = dataGridView1.SelectedRows[i - 1].Index;
                    if (Convert.ToBoolean(dataGridView1.Rows[rno].Cells[0].Value) == true)//全选的时候 删除最后一行会报错 如果不添加这行代码
                    {

                        int id = Convert.ToInt32(dataGridView1.Rows[rno].Cells[0].Value.ToString());
                        if (pd.PlDbDelete(id) == 1)
                        {
                            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);
                            int result = logDal.OpertionLogInsert(userid, "PLDBSetting删除操作", DateTime.Now.ToString(), "PLDBSetting删除操作成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("操作失败");
                            int result = logDal.OpertionLogInsert(userid, "PLDBSetting删除操作", DateTime.Now.ToString(), "PLDBSetting删除操作失败");
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
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textId.Text.ToString() == "" && this.textItem.Text.ToString() == "")
                {
                    dataGridView1.DataSource = pd.PlDbSelect();

                }
                else
                {
                    dataGridView1.DataSource = pd.PlDbSelect(this.textId.Text.ToString(), this.textItem.Text.ToString().Trim());
                    int result = logDal.OpertionLogInsert(userid, "PLDBSetting查询操作", DateTime.Now.ToString(), "PLDBSetting查询操作成功");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败！" + ex.ToString());
                int result = logDal.OpertionLogInsert(userid, "PLDBSetting查询操作", DateTime.Now.ToString(), "PLDBSetting查询操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
        /// <summary>
        /// 打开Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Path_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string fileName = fd.FileName;
                bind(fileName);
            }
        }
        /// <summary>
        /// 绑定指定文件 要导入、导出的文件
        /// </summary>
        private void bind(string fileName)
        {
            string strConn = " Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [PLDB Setting$]", strConn);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                dt = ds.Tables[0];
                this.textpath.Text = fileName.ToString().Trim();
                int result = logDal.OpertionLogInsert(userid, "PLDBSetting绑定指定文件操作", DateTime.Now.ToString(), "PLDBSetting绑定指定文件操作成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            catch (Exception ex)
            {
                int result = logDal.OpertionLogInsert(userid, "PLDBSetting绑定指定文件操作", DateTime.Now.ToString(), "PLDBSetting绑定指定文件操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
                MessageBox.Show("操作失败！" + ex.Message);
            }
        }
        private void insertToSql(DataRow dr)
        {
            pd.PlDbInsert(int.Parse(dr["id"].ToString()), dr["Item"].ToString());
        }
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            DataTable tableCh = dt.Clone();
            bool flag = true;
            if (dt.Rows.Count > 0)
            {
                DataRow dr = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    if (pd.PlDbSelect("", dr[1].ToString().Trim()).Rows.Count > 0)
                    {
                        flag = false;
                        DataRow drC = dt.Rows[i];
                        tableCh.ImportRow(dr);
                    }
                }
                if (flag)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        insertToSql(dr);
                    }
                    MessageBox.Show("导入成功！");
                    int result = logDal.OpertionLogInsert(userid, "PLDBSetting导入操作", DateTime.Now.ToString(), "PLDBSetting导入成功");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("有重复数据，将重复数据保存到以下位置！");
                    if (tableCh.Rows.Count > 0)
                    {
                        if (importEx.tableToExcel(tableCh, "PLDB Setting重复数据") == true)
                        {
                            MessageBox.Show("导出成功！");
                            int result = logDal.OpertionLogInsert(userid, "PLDBSetting将重复数据导出操作", DateTime.Now.ToString(), "PLDBSetting导出成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("请先在该表中导入数据！");
                        }

                    }
                }
                this.dataGridView1.DataSource = pd.PlDbSelect();
            }
            else
            {
                MessageBox.Show("没有数据！");
                int result = logDal.OpertionLogInsert(userid, "PLDBSetting导入操作", DateTime.Now.ToString(), "PLDBSetting没有数据可导入");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
        /// <summary>
        /// 导出按钮 导出到Excel表格里
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_chu_Click(object sender, EventArgs e)
        {
            #region npoi导出

            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count<=0)
            {
                    MessageBox.Show("请先在该表中导入数据！");
                int result = logDal.OpertionLogInsert(userid, "PLDBSetting导出操作", DateTime.Now.ToString(), "PLDBSetting导出操作，先在该表中导入数据");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {
                ImportToExcel.Export(dt, "PLDBSetting Report", "PLDBSetting Report", "", DateTime.Now.ToString());
                int result = logDal.OpertionLogInsert(userid, "PLDBSetting导出操作", DateTime.Now.ToString(), "PLDBSetting导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }




            #endregion
            //if (importEx.toExcel(dataGridView1, "PLDB Setting") == true)
            //{
            //    MessageBox.Show("导出成功！");
            //    int result = logDal.OpertionLogInsert(userid, "PLDBSetting导出操作", DateTime.Now.ToString(), "PLDBSetting导出成功");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请先在该表中导入数据！");
            //    int result = logDal.OpertionLogInsert(userid, "PLDBSetting导出操作", DateTime.Now.ToString(), "PLDBSetting导出操作，先在该表中导入数据");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
        }
        public static int ID;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            UpdatePLDB up = new UpdatePLDB();
            up.Owner = this;
            up.ShowDialog();
            dataGridView1.DataSource = pd.PlDbSelect();
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-08-01
        /// 添加目的：PLDBSetting 下载模版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DownLoad_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Excel模版文件到";
            saveFileDialog.FileName = "PLDB Setting.xlsx";
            saveFileDialog.ShowDialog();
            string downpath = Application.StartupPath + "\\Model\\PLDB Setting.xlsx";
            System.IO.File.Delete(saveFileDialog.FileName);
            System.IO.File.Copy(downpath, saveFileDialog.FileName);
            MessageBox.Show("下载模版成功！");
            int result = logDal.OpertionLogInsert(userid, "PLDBSetting导出Excel模板操作", DateTime.Now.ToString(), "PLDBSetting导出Excel模板操作成功");
            if (result < 0)
            {
                MessageBox.Show("日志插入失败！");
                return;
            }
        }

     

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using System.Data.OleDb;
using MIS_Shell.CommExcel;

namespace MIS_Shell
{
    public partial class T5_Settingcs : Form
    {
        DataTable dt = new DataTable();
        DataTable Exceldt = new DataTable();
        T5SettingDAL T5Settingdal = new T5SettingDAL();
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        ImportToExcel imp = new ImportToExcel();//数据导出函数 2014-08-25 ydx
        public static int ID;
        public T5_Settingcs()
        {
            InitializeComponent();
        }
        //页面加载
        private void T5_Settingcs_Load(object sender, EventArgs e)
        {
            dt = T5Settingdal.T5Select("");
            dataGridView1.DataSource = dt;
            dataGridView1.Font = new System.Drawing.Font("Arial", 10);

            dataGridView1.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("宋体", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 10);
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 10);

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
        }
        //查询
        private void but_Select_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            string scl_T5 = txtSclT5.Text.Trim();
            string deptnameCh = txtDeptNameCh.Text.Trim();
            string deptNamePinYin = txtDeptNamePinYIn.Text.Trim();
            string StrWhere = " and SCL_T5 like '%" + scl_T5 + "%' and DeptNameCH like '%" + deptnameCh + "%' and DeptNamePinYin like '%" + deptNamePinYin + "%'";
            dt = T5Settingdal.T5Select(StrWhere);
            dataGridView1.DataSource = dt;
        }
        //添加
        private void But_Insert_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            string scl_T5 = txt_t5.Text.Trim();
            string deptnameCh = txt_deptCH.Text.Trim();
            string deptNamePinYin = txt_deptPinYin.Text.Trim();
            if (scl_T5 == "" || deptnameCh == "" || deptNamePinYin == "")
            {
                MessageBox.Show("有空白项，请填写完整");
            }
            else
            {
                //插入操作
                string StrWhere = " and SCL_T5 like '%" + scl_T5 + "%' and DeptNameCH like '%" + deptnameCh + "%' and DeptNamePinYin like '%" + deptNamePinYin + "%'";
                dt = T5Settingdal.T5Select(StrWhere);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("已经存在此记录：" + scl_T5 + "->" + deptnameCh + "->" + deptNamePinYin);
                    int result = logDal.OpertionLogInsert(userid, "T5Setting增加操作", DateTime.Now.ToString(), "记录已经存在");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
                else
                {
                    if (T5Settingdal.Insert(scl_T5, deptnameCh, deptNamePinYin) == 1)
                    {
                        MessageBox.Show("增加成功");
                        txt_t5.Text = "";
                        txt_deptCH.Text = "";
                        txt_deptPinYin.Text = "";
                        int result = logDal.OpertionLogInsert(userid, "T5Setting增加操作", DateTime.Now.ToString(), "增加成功");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                        int result = logDal.OpertionLogInsert(userid, "T5Setting增加操作", DateTime.Now.ToString(), "操作失败");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    dataGridView1.DataSource = T5Settingdal.T5Select("");
                }
            }

        }
        //浏览按钮
        private void btnOpenExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string fileName = fd.FileName;
                bind(fileName);
            }
        }
        //绑定选择的excel
        private void bind(string fileName)
        {
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT *  FROM [T5$]", strConn);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                Exceldt = ds.Tables[0];
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
        //导出excel模板
        private void Down_Model_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Excel模版文件到";
            saveFileDialog.FileName = "T5 setting.xlsx";
            saveFileDialog.ShowDialog();
            string downpath = Application.StartupPath + "\\Model\\T5 setting.xlsx";
            System.IO.File.Delete(saveFileDialog.FileName);
            System.IO.File.Copy(downpath, saveFileDialog.FileName);
            MessageBox.Show("导出成功！");
            int result = logDal.OpertionLogInsert(userid, "T5Setting下载模板", DateTime.Now.ToString(), "下载模板成功");
            if (result < 0)
            {
                MessageBox.Show("日志插入失败！");
                return;
            }
        }
        private void insertToSql(DataRow dr)
        {
            string scl_t5 = dr["SCL_T5"].ToString();
            string deptnameCH = dr["DeptNameCH"].ToString();
            string deptNamePinYin = dr["DeptNamePinYin"].ToString();
            T5Settingdal.Insert(scl_t5, deptnameCH, deptNamePinYin);
        }
        //导入
        private void btnImprot_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            DataTable dtCh = null;
            if (Exceldt.Rows.Count > 0)
            {
                dtCh = Exceldt.Clone();
                //DataRow dr = null;
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    dr = dt.Rows[i];
                //    insertToSql(dr);
                //}
                //MessageBox.Show("导入成功！");
                DataRow dr = null;
                bool flag = true;
                for (int i = 0; i < Exceldt.Rows.Count; i++)
                {
                    dr = Exceldt.Rows[i];
                    string StrWhere = " and SCL_T5 like '%" + dr[0].ToString().Trim() + "%' and DeptNameCH like '%" + dr[1].ToString().Trim() + "%' and DeptNamePinYin like '%" + dr[2].ToString().Trim() + "%'";
                    if (T5Settingdal.T5Select(StrWhere).Rows.Count > 0)
                    {
                        dtCh.ImportRow(dr);
                        flag = false;
                        // break;
                    }
                }
                if (flag)
                {
                    //批量插入
                    try
                    {
                       // DBHelper.SqlHelp.InsertTable(Exceldt, "T_SCLT5");

                        for (int i = 0; i < Exceldt.Rows.Count; i++)
                        {
                            dr = Exceldt.Rows[i];
                            insertToSql(dr);
                        }
                        MessageBox.Show("导入成功");
                        int result = logDal.OpertionLogInsert(userid, "T5Setting导入操作", DateTime.Now.ToString(), "T5Setting导入成功");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {

                        int result = logDal.OpertionLogInsert(userid, "T5Setting导入操作", DateTime.Now.ToString(), "T5Setting导入成失败:"+ex.Message);
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
                        try
                        {
                            //导出操作
                            ImportToExcel.Export(dtCh, "T5 Setting重复数据", "T5 Setting重复数据", "", DateTime.Now.ToString());
                            #region 日志
                            int result = logDal.OpertionLogInsert(userid, "T5Setting导入操作", DateTime.Now.ToString(), "T5Setting重复数据已导出");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message);
                            int result = logDal.OpertionLogInsert(userid, "T5Setting导入操作", DateTime.Now.ToString(), "T5Setting导出有异常");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                            
                        }
                     
                        //if (imp.tableToExcel(dtCh, "T5 Setting重复数据") == true)
                        //{
                          //  MessageBox.Show("重复数据已导出！");
                          //  #region 日志
                            //int result = logDal.OpertionLogInsert(userid, "T5Setting导出操作", DateTime.Now.ToString(), "T5Setting重复数据已导出");
                            //if (result < 0)
                            //{
                            //    MessageBox.Show("日志插入失败！");
                            //    return;
                            //}
                            //#endregion
                    
                      //  }
                        //else
                        //{
                        //    MessageBox.Show("导出有异常！");
                        //    int result = logDal.OpertionLogInsert(userid, "T5Setting导出操作", DateTime.Now.ToString(), "T5Setting导出有异常");
                        //    if (result < 0)
                        //    {
                        //        MessageBox.Show("日志插入失败！");
                        //        return;
                        //    }
                        //}

                    }
                }

                this.dataGridView1.DataSource = T5Settingdal.T5Select("");
            }
            else
            {
                MessageBox.Show("没有数据！");
                int result = logDal.OpertionLogInsert(userid, "T5Setting导出操作", DateTime.Now.ToString(), "T5Setting没有数据");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
        //导出数据
        private void button1_Click(object sender, EventArgs e)
        {
            #region npoi导出excel
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据可导出");
                int result = logDal.OpertionLogInsert(userid, "T5Setting导出数据到excel操作", DateTime.Now.ToString(), "没有数据可导出");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }

            }
            else
            {
                ImportToExcel.Export(dt, "T5Setting Report", "T5Setting Report", "", DateTime.Now.ToString());
                int result = logDal.OpertionLogInsert(userid, "T5Setting导出数据到excel操作", DateTime.Now.ToString(), "导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            #endregion
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
 ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
 UpdateT5Setting t5 = new UpdateT5Setting();
            t5.Owner = this;
            t5.ShowDialog();
            this.dataGridView1.DataSource = T5Settingdal.T5Select("");
        }
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
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

                        string id = dataGridView1.Rows[rno].Cells[0].Value.ToString();
                        if (T5Settingdal.delete(id) == 1)
                        {
                            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);
                            int result = logDal.OpertionLogInsert(userid, "T5Setting删除操作", DateTime.Now.ToString(), "删除成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("操作失败！");
                            int result = logDal.OpertionLogInsert(userid, "T5Setting删除操作", DateTime.Now.ToString(), "删除失败");
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

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || dataGridView1.Rows.Count <= 0) return;
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? string.Empty).ToString();
        }

    }


}

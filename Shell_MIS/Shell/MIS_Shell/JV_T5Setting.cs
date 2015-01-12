using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using MIS_Shell.CommExcel;
using System.Data.OleDb;

namespace MIS_Shell
{
    public partial class JV_T5Setting : Form
    {
        JVT5SettingDAL jvT5dal = new JVT5SettingDAL();
        T5SettingDAL t5dal = new T5SettingDAL();
        DataTable dt = new DataTable();
        DataTable Exceldt = new DataTable();//excel中的数据
        public static int ID;

        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        ImportToExcel imp = new ImportToExcel();//数据导出函数 2014-08-25 ydx


        string StrWhere;
        public JV_T5Setting()
        {
            InitializeComponent();
        }

        private void JV_T5Setting_Load(object sender, EventArgs e)
        {
            //cocd绑定
            DataTable dt = jvT5dal.SelectCocd("");
            cb_CoCd.DataSource = dt;//添加
            cb_CoCd.DisplayMember = "cb";
            cb_CoCd.ValueMember = "CoCd";

            cb_Scocb.DataSource = dt;//修改
            cb_Scocb.DisplayMember = "cb";
            cb_Scocb.ValueMember = "CoCd";
            dataGridView1.DataSource = jvT5dal.JVT5Select("");
 
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.Font = new System.Drawing.Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
        }

        private void but_Select_Click(object sender, EventArgs e)
        {
            string t5 = txt_sT5.Text.Trim();
            string jvt5 = txt_sjvt5.Text.Trim();
            string cocd = cb_Scocb.SelectedValue.ToString();
            StrWhere = " and SCL_T5 like '%" + t5 + "%' and JV_T5 like '%" + jvt5 + "%' and CoCd like '%" + cocd + "%'";
            dt = jvT5dal.JVT5Select(StrWhere);
            dataGridView1.DataSource = dt;
        }

        private void But_Insert_Click(object sender, EventArgs e)
        {
            string t5 = txt_t5.Text.Trim();
            string jvt5 = txt_jvt5.Text.Trim();
            string cocd = cb_CoCd.SelectedValue.ToString();

            if (t5 == "" || jvt5 == "" || cocd == "")
            {
                MessageBox.Show("有空白项，请填写完整");
            }
            else
            {
                //判断所要添加的SCL_T%在T_SCLT5中是否存在，若不存在，不让添加
                StrWhere = " and SCL_T5 LIKE '%" + txt_t5.Text.Trim() + "%'";                
                if (t5dal.T5Select(StrWhere).Rows.Count <= 0)
                {
                    //表示不存在
                    MessageBox.Show("不存在该SCL_T5：" + txt_t5.Text.Trim() + "");
                }
                else
                {
                    //存在该SCL_T5，然后验证表中是否有相同的记录                    
                    StrWhere = " and SCL_T5 like '%" + t5 + "%' and JV_T5 like '%" + jvt5 + "%' and CoCd like '%" + cocd + "%'";
                    dt = jvT5dal.JVT5Select(StrWhere);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("已经存在此记录：" + t5 + "->" + jvt5 + "->" + cocd);
                        int result = logDal.OpertionLogInsert(userid, "JVT5Setting增加操作", DateTime.Now.ToString(), "记录已经存在");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    else
                    {
                        if (jvT5dal.Insert(t5, jvt5, cocd) == 1)
                        {
                            MessageBox.Show("增加成功");
                            txt_t5.Text = "";
                            txt_jvt5.Text = "";
                            cb_CoCd.Text = "";
                            int result = logDal.OpertionLogInsert(userid, "JVT5Setting增加操作", DateTime.Now.ToString(), "增加成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("操作失败！");
                            int result = logDal.OpertionLogInsert(userid, "JVT5Setting增加操作", DateTime.Now.ToString(), "操作失败");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        dataGridView1.DataSource = jvT5dal.JVT5Select("");
                    }
                }
            }
        }
        //选择上传文件
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
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT *  FROM [JV_T5$]", strConn);
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
                int result = logDal.OpertionLogInsert(userid, "JVT5Setting绑定数据", DateTime.Now.ToString(), "JVT5Setting绑定数据失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
        //下载模板
        private void Down_Model_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Excel模版文件到";
            saveFileDialog.FileName = "JVT5 setting.xlsx";
            saveFileDialog.ShowDialog();
            string downpath = Application.StartupPath + "\\Model\\JV_T5 setting.xlsx";
            System.IO.File.Delete(saveFileDialog.FileName);
            System.IO.File.Copy(downpath, saveFileDialog.FileName);
            MessageBox.Show("导出成功！");
            int result = logDal.OpertionLogInsert(userid, "JVT5Setting下载模板", DateTime.Now.ToString(), "下载模板成功");
            if (result < 0)
            {
                MessageBox.Show("日志插入失败！");
                return;
            }
        }
        //导入
        private void btnImprot_Click(object sender, EventArgs e)
        {
            DataTable dtCh = null;
            if (Exceldt.Rows.Count > 0)
            {
                dtCh = Exceldt.Clone();
                DataRow dr = null;
                bool flag = true;
                for (int i = 0; i < Exceldt.Rows.Count; i++)
                {
                    dr = Exceldt.Rows[i];

                    string strWhere = " and SCL_T5 LIKE '%" + dr[0].ToString().Trim() + "%'";
                    if (t5dal.T5Select(strWhere).Rows.Count <= 0)
                    {
                        //表示不存在
                        MessageBox.Show("不存在该SCL_T5：" + dr[0].ToString().Trim() + "");
                        return;
                    }

                    else
                    {
                        string StrWhere = " and SCL_T5 like '%" + dr[0].ToString().Trim() + "%' and JV_T5 like '%" + dr[1].ToString().Trim() + "%' and CoCd like '%" + dr[2].ToString().Trim() + "%'";
                        if (jvT5dal.JVT5Select(StrWhere).Rows.Count > 0)
                        {
                            dtCh.ImportRow(dr);
                            flag = false;
                            // break;
                        }
                    }
                }
                if (flag)
                {
                    for (int i = 0; i < Exceldt.Rows.Count; i++)
                    {
                        dr = Exceldt.Rows[i];
                        insertToSql(dr);
                    }
                    MessageBox.Show("导入成功");
                    int result = logDal.OpertionLogInsert(userid, "JVT5Setting导入操作", DateTime.Now.ToString(), "JVT5Setting导入成功");
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
                        // this.dataGrdCh.DataSource = dtCh;
                        if (imp.tableToExcel(dtCh, "JVT5 Setting重复数据") == true)
                        {
                            MessageBox.Show("重复数据已导出！");
                            int result = logDal.OpertionLogInsert(userid, "JVT5Setting导出重复数据操作", DateTime.Now.ToString(), "JVT5Setting重复数据已导出");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("导出有异常！");
                            int result = logDal.OpertionLogInsert(userid, "JVT5Setting导出操作", DateTime.Now.ToString(), "JVT5Setting导出有异常");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }

                    }
                }

                this.dataGridView1.DataSource = jvT5dal.JVT5Select("");
            }
            else
            {
                MessageBox.Show("没有数据！");
                int result = logDal.OpertionLogInsert(userid, "JVT5Setting导出操作", DateTime.Now.ToString(), "JVT5Setting没有数据");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
        //导出
        private void button1_Click(object sender, EventArgs e)
        {
            #region npoi导出excel
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据可导出");
                int result = logDal.OpertionLogInsert(userid, "JVT5Setting导出数据到excel操作", DateTime.Now.ToString(), "没有数据可导出");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }

            }
            else
            {
                ImportToExcel.Export(dt, "JVT5Setting Report", "JVT5Setting Report", "", DateTime.Now.ToString());
                int result = logDal.OpertionLogInsert(userid, "JVT5Setting导出数据到excel操作", DateTime.Now.ToString(), "导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            #endregion
        }
        private void insertToSql(DataRow dr)
        {
            string scl_t5 = dr["SCL_T5"].ToString();
            string jv_t5 = dr["JV_T5"].ToString();
            string cocd = dr["CoCd"].ToString();
            jvT5dal.Insert(scl_t5, jv_t5, cocd);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            UpdateJVT5 t5 = new UpdateJVT5();
            t5.Owner = this;
            t5.ShowDialog();
            this.dataGridView1.DataSource = jvT5dal.JVT5Select("");
        }
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }


        private void 删除ToolStripMenuItem_Click_1(object sender, EventArgs e)
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
                        if (jvT5dal.delete(id) == 1)
                        {
                            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);
                            int result = logDal.OpertionLogInsert(userid, "JVT5Setting删除操作", DateTime.Now.ToString(), "删除成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("操作失败！");
                            int result = logDal.OpertionLogInsert(userid, "JVT5Setting删除操作", DateTime.Now.ToString(), "删除失败");
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
    }
}

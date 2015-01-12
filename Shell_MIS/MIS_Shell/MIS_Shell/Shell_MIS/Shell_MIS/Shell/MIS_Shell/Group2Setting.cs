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
    /// 添加时间：2014-07-24
    /// 添加目的:二级分组处理
    /// </summary>
    public partial class Group2Setting : Form
    {   
        Group1Dal gr1Dal = new Group1Dal();
        Group2Dal gr2Dal = new Group2Dal();
        ImportToExcel imp = new ImportToExcel();//数据导出 2014-08-25 ydx
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        public Group2Setting()
        {
            InitializeComponent();
        }
       
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Group2Setting_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            //dt = gr1Dal.GroupSelect();
            dt = gr1Dal.GroupSelectIf();
            DataRow dr = dt.NewRow();
            dr["Id"]="0";
            dr["Group1Name"] = "请选择";
            dr["Status"] = "0";
            dt.Rows.InsertAt(dr, 0);
            this.comboGr1.DataSource = dt.DefaultView;
            comboGr1.DisplayMember = "Group1Name";
            comboGr1.ValueMember = "Id";

            //状态只有三种形式  启动  冻结  关闭   查询的时候默认为请选择
            this.comboState.SelectedItem = "请选择";
           
            //修改
            DataTable dts = gr1Dal.GroupSelectIf();
            DataRow drS = dts.NewRow();
            drS["Id"]="0";
            drS["Group1Name"] = "请选择";
            drS["Status"] = "0";
            dts.Rows.InsertAt(drS, 0);
            this.comboGr1Id.DataSource = dts.DefaultView;
            comboGr1Id.DisplayMember = "Group1Name";
            comboGr1Id.ValueMember = "Id";

            this.dataGridView2.DataSource =gr2Dal.GroupSelect();
            this.dataGridView2.Font = new Font("Arial", 10);
            this.dataGridView2.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_Insert_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            if (comboGr1.SelectedValue.ToString().Trim() == "0")
            {
                MessageBox.Show("请选择一级分组");
            }
            else if (this.txt_Group2.Text.ToString().Trim()== "")
            {
                MessageBox.Show("有空白信息，请填写完整");
            }
            else
            {
                string group1 = comboGr1.SelectedValue.ToString();
                string Group2Name = txt_Group2.Text.ToString().Trim();
                string State = "Active";
                DataTable deExis = gr2Dal.GroupSelect(Group2Name, "", group1);
                if (deExis.Rows.Count > 0)
                {
                    MessageBox.Show("此记录"+Group2Name+"已存在于此一级分组" );
                }
                else
                {
                    if (gr2Dal.GroupInsert(Group2Name, State,group1) == 1)
                    {
                        MessageBox.Show("增加成功！");
                        comboGr1.SelectedValue = "0";
                        txt_Group2.Text = "";
                        int result = logDal.OpertionLogInsert(userid, "Group2Setting添加操作", DateTime.Now.ToString(), "Group2Setting添加成功");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                        int result = logDal.OpertionLogInsert(userid, "Group2Setting添加操作", DateTime.Now.ToString(), "Group2Setting操作失败");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    this.dataGridView2.DataSource = gr2Dal.GroupSelect();
                }

            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_Select_Click_1(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            try
            {
                string Group2Name = texGr2.Text.ToString().Trim();
                string State = "";
                if(comboState.SelectedItem.ToString()!="请选择")
                {
                    State = comboState.SelectedItem.ToString();
                }

                string gr1id = "";
                string dd = comboGr1Id.SelectedValue.ToString();
                if (comboGr1Id.SelectedValue.ToString() == "0")
                {
                    this.dataGridView2.DataSource = gr2Dal.GroupSelect(State);
                }
                else
                {
                    gr1id = comboGr1Id.SelectedValue.ToString();
                    this.dataGridView2.DataSource = gr2Dal.GroupSelect(Group2Name, State, gr1id);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "Group2Setting查询操作", DateTime.Now.ToString(), "Group2Setting操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
               this.contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                dataGridView2.Rows[e.RowIndex].Selected = true;
            }
        }
        /// <summary>
        /// 弹出 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static int ID;
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            UpdateGroup2 updatagroup = new UpdateGroup2();
            updatagroup.Owner = this;
            updatagroup.ShowDialog();
            this.dataGridView2.DataSource = gr2Dal.GroupSelect();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
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

                        int id = Convert.ToInt32(dataGridView2.Rows[rno].Cells[0].Value.ToString());
                        if (gr2Dal.GroupDelete(id.ToString()) == 1)
                        {
                            dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[i - 1].Index);
                            int result = logDal.OpertionLogInsert(userid, "Group2Setting删除操作", DateTime.Now.ToString(), "Group2Setting删除成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("操作失败");
                            int result = logDal.OpertionLogInsert(userid, "Group2Setting删除操作", DateTime.Now.ToString(), "Group2Setting删除操作失败");
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
        /// 导出  
        /// 添加人：ydx
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_imp_Click(object sender, EventArgs e)
        {
            #region NPOI数据导出
            DataTable dt = (DataTable)dataGridView2.DataSource;
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据可导出");
                int result = logDal.OpertionLogInsert(userid, "Group2Setting导出操作", DateTime.Now.ToString(), "Group2Setting没有数据可导出");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {
                //导出
                ImportToExcel.Export(dt, "Group2SettingReport", "Group2SettingReport", "", DateTime.Now.ToString("yyyy-MM-dd"));
                int result = logDal.OpertionLogInsert(userid, "Group2Setting导出操作", DateTime.Now.ToString(), "Group2Setting导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            #endregion
           




            //if (imp.toExcel(dataGridView2, "Group2 Setting") == true)
            //{
            //    MessageBox.Show("导出成功");
            //    int result = logDal.OpertionLogInsert(userid, "Group2Setting导出操作", DateTime.Now.ToString(), "Group2Setting导出成功");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("没有数据可导出");
            //    int result = logDal.OpertionLogInsert(userid, "Group2Setting导出操作", DateTime.Now.ToString(), "Group2Setting没有数据可导出");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}

            //if (this.dataGridView2.RowCount - 1 != 0)
            //{
            //    SaveFileDialog saveFileDialog = new SaveFileDialog();
            //    saveFileDialog.Filter = "Execl   files   (*.xls)|*.xls";
            //    saveFileDialog.FilterIndex = 0;
            //    saveFileDialog.RestoreDirectory = true;
            //    saveFileDialog.CreatePrompt = true;
            //    saveFileDialog.Title = "导出Excel文件到";
            //    saveFileDialog.FileName = "Group2 Setting";
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

        private void dataGridView2_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || dataGridView2.Rows.Count <= 0) return;
            dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? string.Empty).ToString();
        }
    }
}

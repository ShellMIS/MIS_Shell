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
    /// 添加时间:2014-07-24
    /// 添加目的:一级分组处理
    /// </summary>
    public partial class GroupSetting : Form
    {
        public GroupSetting()
        {
            InitializeComponent();
        }
        Group1Dal groupDal = new Group1Dal();
        DataTable dt = new DataTable();
        ImportToExcel imp = new ImportToExcel();//ydx 2014-08-25 数据导出
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupSetting_Load(object sender, EventArgs e)
        {
            this.comboSta.SelectedItem = "请选择";
            this.dataGridView2.DataSource = this.groupDal.GroupSelect();

            this.dataGridView2.DefaultCellStyle.Font = new Font("Arial", 10);
            this.dataGridView2.Font = new Font("Arial", 10);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);

        }

        /// <summary>
        /// 选中移行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                dataGridView2.Rows[e.RowIndex].Selected = true;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        if (groupDal.GroupDelete(id) == 1)
                        {
                            dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[i - 1].Index);
                            int result = logDal.OpertionLogInsert(userid, "Group Setting删除操作", DateTime.Now.ToString(), "删除成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("操作失败！");
                            int result = logDal.OpertionLogInsert(userid, "Group Setting删除操作", DateTime.Now.ToString(), "操作失败");
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
        /// 添加  2014-07-24
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_Insert_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            if (this.txt_CoG1.Text.ToString().Trim() == "")
            {
                MessageBox.Show("有空白信息，请填写完整");
            }
            else
            {
                string Group1Name = txt_CoG1.Text.ToString().Trim();
                string State = "Active";
                DataTable deExis = groupDal.GroupSelect(Group1Name, "");
                if (deExis.Rows.Count > 0)
                {
                    MessageBox.Show("此记录已存在：" + Group1Name);
                }
                else
                {
                    if (groupDal.GroupInsert(Group1Name, State) == 1)
                    {
                        MessageBox.Show("增加成功！");
                        txt_CoG1.Text = "";
                        int result = logDal.OpertionLogInsert(userid, "Group Setting添加操作", DateTime.Now.ToString(), "添加成功");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                        int result = logDal.OpertionLogInsert(userid, "Group Setting添加操作", DateTime.Now.ToString(), "操作失败");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    this.dataGridView2.DataSource = groupDal.GroupSelect();
                }
            }
        }
        /// <summary>
        ///查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_Select_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            try
            {
                string Group1Name = txt_GroupName.Text.ToString().Trim();
                string State = "";
                string aa = comboSta.SelectedItem.ToString();
                if (comboSta.SelectedValue!=null)                    
                {
                    if (comboSta.SelectedValue.ToString()!="")
                    {
                        State = comboSta.SelectedItem.ToString();
                        this.dataGridView2.DataSource = groupDal.GroupSelect(Group1Name, State);
                    }
                   
                }
                else
                {
                    string strWhere = "and  Group1Name='" + Group1Name + "' and ";
                    for (int i = 1; i < comboSta.Items.Count; i++)
                    {
                       
                        strWhere += " [Status] = '"+comboSta.Items[i].ToString().Trim()+"' or ";
                     
                    }
                       this.dataGridView2.DataSource = groupDal.GroupSelect(strWhere.Substring(0,strWhere.Length-3));

                }
               
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "Group Setting查询操作", DateTime.Now.ToString(), "操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }

        public static int ID;
        /// <summary>
        ///  修改 对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            UpdateGroup updatagroup = new UpdateGroup();
            updatagroup.Owner = this;
            updatagroup.ShowDialog();
            this.dataGridView2.DataSource = groupDal.GroupSelect();
        }
        /// <summary>
        /// 导出按钮 修改人 ydx
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
                int result = logDal.OpertionLogInsert(userid, "Group Setting导出操作", DateTime.Now.ToString(), "Group Setting没有数据可导出");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {
                //导出
                ImportToExcel.Export(dt, "GroupSettingReport", "GroupSettingReport", "", DateTime.Now.ToString("yyyy-MM-dd"));
                int result = logDal.OpertionLogInsert(userid, "Group Setting导出操作", DateTime.Now.ToString(), "Group Setting导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            #endregion

            //#region 修改数据导出
            //if (imp.toExcel(dataGridView2, "Group1 Setting") == true)
            //{
            //    MessageBox.Show("导出成功");
            //    int result = logDal.OpertionLogInsert(userid, "Group Setting导出操作", DateTime.Now.ToString(), "Group Setting导出成功");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("没有数据可导出");
            //    int result = logDal.OpertionLogInsert(userid, "Group Setting导出操作", DateTime.Now.ToString(), "Group Setting没有数据可导出");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
            //#endregion

            //if (this.dataGridView2.RowCount - 1 != 0)
            //{
            //    SaveFileDialog saveFileDialog = new SaveFileDialog();
            //    saveFileDialog.Filter = "Execl   files   (*.xls)|*.xls";
            //    saveFileDialog.FilterIndex = 0;
            //    saveFileDialog.RestoreDirectory = true;
            //    saveFileDialog.CreatePrompt = true;
            //    saveFileDialog.Title = "导出Excel文件到";
            //    saveFileDialog.FileName = "Group1 Setting";
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

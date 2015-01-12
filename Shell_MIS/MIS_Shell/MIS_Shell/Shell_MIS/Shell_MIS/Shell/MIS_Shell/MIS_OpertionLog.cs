using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;

namespace MIS_Shell
{
    //添加人：ydx
    //添加目的:操作日志管理
    public partial class MIS_OpertionLog : Form
    {

        OptionLogDAL opDal = new OptionLogDAL();
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户ID
        OptionLogDAL logDal = new OptionLogDAL(); //日志
        public MIS_OpertionLog()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 查询日志信息 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            string uid = tUId.Text.ToString();
            string uName = tUUser.Text.ToString();
            string LogDesc = tUDesc.Text.ToString();
            string date = textDate.Text.ToString();
            dataGridView1.DataSource = opDal.OpertionLogSelect(uid, uName, LogDesc, date);
        }
        /// <summary>
        /// 窗体加载事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MIS_OpertionLog_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = opDal.OpertionLogSelect();
            dataGridView1.Columns[0].Visible = false;
            //this.dataGridView1.DefaultCellStyle.Font = new Font("宋体", 10);
            this.dataGridView1.Columns[0].DefaultCellStyle.Font = new Font("Arial", 10);
            this.dataGridView1.Columns[2].DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.Font = new System.Drawing.Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                bool flag = true, deleteResult = true;
                string id = "";
                for (int i = dataGridView1.SelectedRows.Count; i > 0; i--)
                {
                    int rno = dataGridView1.SelectedRows[i - 1].Index;

                    #region 批量删除，循环拼接id
                    id +="'"+ dataGridView1.Rows[rno].Cells[0].Value.ToString() + "'"+",";
                    #endregion
                    #region 一条一条的删除
                    //if (opDal.deleteOpertionLog(id)==1)
                    //{
                    //    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);
                    //    flag = true;
                    //}
                    //else
                    //{
                    //    flag = false;
                    //    MessageBox.Show("删除操作失败");
                    //    deleteResult = false;

                    //}
                    #endregion
                }

                if (opDal.deleteOpertionLog(id.Substring(1,id.Length-3)) == 1)
                {
                    MessageBox.Show("删除成功！");
                    this.dataGridView1.DataSource = opDal.OpertionLogSelect();
                }
                else
                {
                    deleteResult = false;
                }
                if (deleteResult)
                {
                    int result = logDal.OpertionLogInsert(userid, "Mis_OpertionLog删除日志操作", DateTime.Now.ToString(), "删除成功！");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
                else
                {
                    int result = logDal.OpertionLogInsert(userid, "Mis_OpertionLog删除日志操作", DateTime.Now.ToString(), "删除失败！");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }


            }
        }
        /// <summary>
        /// 单元格鼠标抬起选中一行或多行数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                this.contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
    }
}

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
    /// 修改时间：2014-07-31
    /// 修改内容：添加group1，group2 级联功能
    /// 修改人：ydx
    /// 修改时间：2014-08-25
    /// 修噶目的;数据导入。导出
    /// 
    /// 
    /// </summary>
    public partial class JVSetting : Form
    {
        public JVSetting()
        {
            InitializeComponent();
        }
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        JVDal jvdal = new JVDal();
        Group1Dal gr1Dal = new Group1Dal();
        Group2Dal gr2Dal = new Group2Dal();
        //    DataTable dt = new DataTable();
        DataTable dtAll = new DataTable();
        ImportToExcel imp = new ImportToExcel();//ydx  2014-08-25 数据导出
        private void JVSetting_Load(object sender, EventArgs e)
        {
            //添加
            //分组1
            DataTable dt = new DataTable();
            // dt = gr1Dal.GroupSelect();
            dt = gr1Dal.GroupSelectIf();
            DataRow dr = dt.NewRow();
            dr["Id"] = "0";
            dr["Group1Name"] = "请选择";
            dr["Status"] = "0";
            dt.Rows.InsertAt(dr, 0);
            //cbgroup1.DataSource = dt.DefaultView;
            cbgroup1.DataSource = dt;
            cbgroup1.DisplayMember = "Group1Name";
            cbgroup1.ValueMember = "Id";
            //分组2
            DataTable dt2 = new DataTable();
            dt2 = gr2Dal.GroupSe();
            DataRow dr2 = dt2.NewRow();
            dr2["Id"] = "0";
            dr2["Group2Name"] = "请选择";
            dt2.Rows.InsertAt(dr2, 0);
            cbgroup2.DataSource = dt2.DefaultView;
            cbgroup2.DisplayMember = "Group2Name";
            cbgroup2.ValueMember = "Id";
            //查看
            //分组1
            DataTable dt3 = new DataTable();
            //dt3 = gr1Dal.GroupSelect();
            dt3 = gr1Dal.GroupSelectIf();
            DataRow dr3 = dt3.NewRow();
            dr3["Id"] = "0";
            dr3["Group1Name"] = "请选择";
            dr3["Status"] = "0";
            dt3.Rows.InsertAt(dr3, 0);
            cg1.DataSource = dt3.DefaultView;
            cg1.DisplayMember = "Group1Name";
            cg1.ValueMember = "Id";
            //分组2
            DataTable dt4 = new DataTable();
            dt4 = gr2Dal.GroupSe();
            DataRow dr4 = dt4.NewRow();

            dr4["Id"] = "0";
            dr4["Group2Name"] = "请选择";
            dt4.Rows.InsertAt(dr4, 0);
            cg2.DataSource = dt4.DefaultView;
            cg2.DisplayMember = "Group2Name";
            cg2.ValueMember = "Id";

            dtAll = jvdal.JVSelect();
            if (dtAll.Rows.Count > 0)
            {
                for (int i = 0; i < dtAll.Rows.Count; i++)
                {
                    if (dtAll.Rows[i]["Share"].ToString() != "")
                    {
                        dtAll.Rows[i]["Share"] = dtAll.Rows[i]["Share"].ToString().Trim() + "%";
                    }
                }
            }

            this.dataGridView2.DataSource = dtAll;

            dataGridView2.Font = new Font("Arial", 10);
            dataGridView2.DefaultCellStyle.Font = new Font("Arial", 10);

            this.dataGridView2.Columns["CoNameCH"].DefaultCellStyle.Font = new Font("宋体", 10);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }
        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                contextMenuStrip2.Show(MousePosition.X, MousePosition.Y);
                dataGridView2.Rows[e.RowIndex].Selected = true;
            }
        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int row2 = dataGridView2.SelectedRows.Count;
            if (row2 == 0)
            {
                MessageBox.Show("没有选中任何行", "Error");
                return;
            }
            else if (MessageBox.Show("确认删除选中的" + row2.ToString() + "条记录吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = dataGridView2.SelectedRows.Count; i > 0; i--)
                {
                    int row = dataGridView2.SelectedRows[i - 1].Index;
                    if (Convert.ToBoolean(dataGridView2.Rows[row].Cells[0].Value) == true)//全选的时候 删除最后一行会报错 如果不添加这行代码
                    {

                        string id = dataGridView2.Rows[row].Cells[0].Value.ToString();
                        if (jvdal.JVDelete(id) == 1)
                        {
                            dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[i - 1].Index);
                            int result = logDal.OpertionLogInsert(userid, "JV Setting删除操作", DateTime.Now.ToString(), "JV Setting删除成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("操作失败！");
                            int result = logDal.OpertionLogInsert(userid, "JV Setting删除操作", DateTime.Now.ToString(), "JV Setting删除失败");
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
        /// 添加功能
        /// 修改人：ydx
        /// 修改时间：2014-07-21
        /// 修改目的：添加分组1，分组2条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_Insert_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);

            #region hxy 修改
            if (this.cbgroup1.SelectedValue.ToString().Trim() == "0" || this.cbgroup2.SelectedValue.ToString().Trim() == "0" || this.cbgroup1.SelectedValue.ToString() == "System.Data.DataRowView" || this.cbgroup2.SelectedValue.ToString() == "System.Data.DataRowView")
            {
                MessageBox.Show("请选择分组");
            }
            #endregion          
            else if (txt_CoCd.Text == "" || txt_CoNameCH.Text == "" || txt_CoNameEN.Text == "" || txt_Share.Text == "")
            {
                MessageBox.Show("有空白信息，填写完整！");
            }
            else
            {
                string CoCd = txt_CoCd.Text.ToString().Trim();
                string CoNameCH = txt_CoNameCH.Text.ToString().Trim();
                string CoNameEN = txt_CoNameEN.Text.ToString().Trim();
                string Share = txt_Share.Text.ToString().Trim();
                string group1 = "";
                string group2 = "";
                if (this.cbgroup1.SelectedValue.ToString().Trim() != "0")
                {
                    group1 = this.cbgroup1.SelectedValue.ToString().Trim();
                }
                if (this.cbgroup2.SelectedValue.ToString().Trim() != "0")
                {
                    group2 = this.cbgroup2.SelectedValue.ToString().Trim();
                }

                if (jvdal.JVInsert(CoCd, CoNameCH, CoNameEN, Share, group1, group2) == 1)
                {
                    MessageBox.Show("增加成功！");
                    txt_CoCd.Text = "";
                    txt_CoNameCH.Text = "";
                    txt_CoNameEN.Text = "";
                    txt_Share.Text = "";
                    this.cbgroup2.SelectedValue = "0";
                    this.cbgroup1.SelectedValue = "0";
                    int result = logDal.OpertionLogInsert(userid, "JV Setting添加操作", DateTime.Now.ToString(), "JV Setting添加成功");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("操作失败！");
                    int result = logDal.OpertionLogInsert(userid, "JV Setting添加操作", DateTime.Now.ToString(), "JV Setting添加失败");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }


                DataTable dtAll = jvdal.JVSelect();
                if (dtAll.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAll.Rows.Count; i++)
                    {
                        if (dtAll.Rows[i]["Share"].ToString() != "")
                        {
                            dtAll.Rows[i]["Share"] = dtAll.Rows[i]["Share"].ToString().Trim() + "%";
                        }
                    }
                }

                this.dataGridView2.DataSource = dtAll;
            }

        }


        private void but_Select_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            try
            {
                string CoCd = txt_SCoCd.Text.ToString().Trim();
                string CoNameCH = txt_SCoNameCH.Text.ToString().Trim();
                string CoNameEN = txt_SCoNameEN.Text.ToString().Trim();
                string Share = txt_SShare.Text.ToString().Trim();
                string grop1 = "";
                string grop2 = "";
                #region hxy 修改
                if (cg1.SelectedValue != null)
                {

                    if (cg1.SelectedValue.ToString() != "0" && cg1.SelectedValue.ToString() != "System.Data.DataRowView")
                    {
                        grop1 = this.cg1.SelectedValue.ToString().Trim();
                    }
                }
                if (cg2.SelectedValue != null)
                {

                    if (cg2.SelectedValue.ToString().Trim() != "0" && cg2.SelectedValue.ToString() != "System.Data.DataRowView")
                    {
                        grop2 = this.cg2.SelectedValue.ToString().Trim();
                    }
                }
                #endregion
               
                DataTable dtAll = jvdal.JVSelect(CoCd, CoNameCH, CoNameEN, Share, grop1, grop2);
                if (dtAll.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAll.Rows.Count; i++)
                    {
                        if (dtAll.Rows[i]["Share"].ToString() != "")
                        {
                            dtAll.Rows[i]["Share"] = dtAll.Rows[i]["Share"].ToString().Trim() + "%";
                        }
                    }
                }

                this.dataGridView2.DataSource = dtAll;


            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "JV Setting查询操作", DateTime.Now.ToString(), "JV Setting查询失败");
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
            UpdateJV updatajv = new UpdateJV();
            updatajv.Owner = this;
            updatajv.ShowDialog();
            DataTable dtAll = jvdal.JVSelect();
            if (dtAll.Rows.Count > 0)
            {
                for (int i = 0; i < dtAll.Rows.Count; i++)
                {
                    if (dtAll.Rows[i]["Share"].ToString() != "")
                    {
                        dtAll.Rows[i]["Share"] = dtAll.Rows[i]["Share"].ToString().Trim() + "%";
                    }
                }
            }

            this.dataGridView2.DataSource = dtAll;
            // this.dataGridView2.DataSource = jvdal.JVSelect();
        }
        /// <summary>
        /// 导出
        /// 添加人：ydx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_imp_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (dtAll.Rows.Count <= 0)
            {
                MessageBox.Show("没有数据可导出！");
                result = logDal.OpertionLogInsert(userid, "JV Setting导出操作", DateTime.Now.ToString(), "JV Setting导出失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
                return;
            }
            ImportToExcel.Export(dtAll, "JVSettingReport", "JVSettingReport", txt_CoNameCH.Text, DateTime.Now.ToString("yyyy-MM-dd"));

            //if (imp.toExcel(dataGridView2, "JV Setting") == true)
            //{
            //    MessageBox.Show("导出成功");
            result = logDal.OpertionLogInsert(userid, "JV Setting导出操作", DateTime.Now.ToString(), "JV Setting导出成功");
            if (result < 0)
            {
                MessageBox.Show("日志插入失败！");
                return;
            }
            //}
            //else
            //{
            //    MessageBox.Show("没有数据可导出");
            //    int result = logDal.OpertionLogInsert(userid, "JV Setting导出操作", DateTime.Now.ToString(), "JV Setting导出失败");
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
            //    saveFileDialog.FileName = "JV Setting";
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
        /// <summary>
        /// 当分组1发生改变时触发的事件
        /// 修改人：ydx
        /// 修改时间：2014-07-31
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbgroup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbgroup1.SelectedValue.ToString() != "System.Data.DataRowView" && cbgroup1.SelectedValue.ToString() != "0")//&& cg1.SelectedValue.ToString() != "0")
            {
                DataTable dts = gr2Dal.GroupSelectByGr1Id(int.Parse(cbgroup1.SelectedValue.ToString()));
                cbgroup2.DataSource = dts.DefaultView;
                cbgroup2.DisplayMember = "Group2Name";
                cbgroup2.ValueMember = "Id";
                #region hxy 修改
                DataRow dr4 = dts.NewRow();
                dr4["Id"] = "0";
                dr4["Group2Name"] = "请选择";
                dts.Rows.InsertAt(dr4, 0);

                #endregion
               

            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-08-01
        /// 添加目的：修改面板里的  一级分组，二级分组级联问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cg1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cg1.SelectedValue.ToString() != "System.Data.DataRowView" && cg1.SelectedValue.ToString() != "0")//&& cg1.SelectedValue.ToString() != "0")
            {
                DataTable dts = gr2Dal.GroupSelectByGr1Id(int.Parse(cg1.SelectedValue.ToString()));
                cg2.DataSource = dts.DefaultView;

                cg2.DisplayMember = "Group2Name";
                cg2.ValueMember = "Id";
                #region hxy 修改
                DataRow dr4 = dts.NewRow();
                dr4["Id"] = "0";
                dr4["Group2Name"] = "请选择";
                dts.Rows.InsertAt(dr4, 0);
                #endregion
              
            }
        }


    }
}

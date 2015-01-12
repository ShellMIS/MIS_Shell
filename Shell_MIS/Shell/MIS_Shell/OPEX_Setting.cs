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
    public partial class OPEX_Setting : Form
    {
        public OPEX_Setting()
        {
            InitializeComponent();
        }
        Pldb pd = new Pldb();
        OPEXDal op = new OPEXDal();
        DataTable dt = new DataTable();
        ImportToExcel imp = new ImportToExcel();//ydx 2014-08-25 数据导出
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志

        /// <summary>
        /// 绑定指定文件 要导入、导出的文件
        /// </summary>
        private void bind(string fileName)
        {
            string strConn = " Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [OPEX mapping$]", strConn);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                dt = ds.Tables[0];
                this.textPath.Text = fileName.ToString().Trim();
                int result = logDal.OpertionLogInsert(userid, "OPEX_Setting绑定指定文件操作", DateTime.Now.ToString(), "OPEX_Setting绑定指定文件操作成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败！" + ex.Message);
                int result = logDal.OpertionLogInsert(userid, "OPEX_Setting绑定指定文件操作", DateTime.Now.ToString(), "OPEX_Setting绑定指定文件操作失败");
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
        private void btn_open_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string fileName = fd.FileName;
                bind(fileName);
            }
        }
        /// <summary>
        ///导入的过程中循环插入数据
        /// </summary>
        /// <param name="dr"></param>
        private void insertToSql(DataRow dr)
        {

            op.OpexSettingInsert(dr["PLType"].ToString(), dr["PL Line"].ToString(), dr["OpexLine"].ToString(), dr["BudgetOwner"].ToString(), dr["AccountCode"].ToString(), dr["Account_Description"].ToString());
        }
        /// <summary>
        /// 导入按钮
        /// 修改人：ydx
        /// 修改时间：2014-08-25
        /// 修改目的：导入重复数据 导出到excel表格里
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_import_Click(object sender, EventArgs e)
        {
            bool flag = true;
            DataTable dtCh = null;//存储重复数据
            if (dt.Rows.Count > 0)
            {
                DataRow dr = null;
                DataTable dts = null;
                string PtypeLine = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    int ptype = int.Parse(dr["PLType"].ToString());
                    dts = pd.PlDbSelect(ptype);
                    if (dts.Rows.Count <= 0)
                    {
                        flag = false;
                        PtypeLine = dr["PLLine"].ToString();
                        break;
                    }
                }

                if (flag == true)
                {
                    bool flags = true;
                    dtCh = dt.Clone();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        dr = dt.Rows[i];
                        //for (int u = 0; u < dt.Columns.Count;u++ )
                        //{
                        //    string bb = dt.Columns[u].Caption;
                        //}
                        //string qw = dr["PLType"].ToString();
                        //string qw2 = dr["PL Line"].ToString();
                        //string qw3 = dr["OpexLine"].ToString();
                        //string qw4 = dr["BudgetOwner"].ToString();
                        //string qw5 = dr["AccountCode"].ToString();
                        //string qw6 = dr["Account_Description"].ToString();
                        if (op.OpexSettingSelect(dr["PLType"].ToString().Trim(), dr["PL Line"].ToString().Trim(), dr["OpexLine"].ToString().Trim(), dr["BudgetOwner"].ToString().Trim(), dr["AccountCode"].ToString().Trim(), dr["Account_Description"].ToString().Trim()).Rows.Count > 0)
                        {
                            dtCh.ImportRow(dr);
                            flags = false;


                            // break;
                        }

                    }
                    if (flags)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            insertToSql(dr);
                        }
                        MessageBox.Show("导入成功");
                        int result = logDal.OpertionLogInsert(userid, "OPEX_Setting导入重复数据 ", DateTime.Now.ToString(), "OPEX_Setting导入成功");
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
                            if (imp.tableToExcel(dtCh, "OPEX mapping重复数据") == true)
                            {
                                MessageBox.Show("已经将重复数据导出！");
                                int result = logDal.OpertionLogInsert(userid, "OPEX_Setting导入重复数据 ", DateTime.Now.ToString(), "OPEX_Setting已经将重复数据导出");
                                if (result < 0)
                                {
                                    MessageBox.Show("日志插入失败！");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("重复数据导出异常！");
                                int result = logDal.OpertionLogInsert(userid, "OPEX_Setting导入重复数据 ", DateTime.Now.ToString(), "OPEX_Setting重复数据导出异常");
                                if (result < 0)
                                {
                                    MessageBox.Show("日志插入失败！");
                                    return;
                                }
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("PLDB里没有:" + PtypeLine + "这项条目，因此此文件无法导入！");
                    int result = logDal.OpertionLogInsert(userid, "OPEX_Setting导入重复数据 ", DateTime.Now.ToString(), "OPEX_Setting导入操作，PLDB里没有:" + PtypeLine + "这项条目，因此此文件无法导入");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
                this.dataGridView1.DataSource = op.OpexSettingSelect();
            }
            else
            {
                MessageBox.Show("没有数据！");
                int result = logDal.OpertionLogInsert(userid, "OPEX_Setting导入数据 ", DateTime.Now.ToString(), "OPEX_Setting要导入的文件中没有数据");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btn_export_Click(object sender, EventArgs e)
        {
            #region npoi导出
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("请先在该表中导入数据！");
                int result = logDal.OpertionLogInsert(userid, "OPEX_Setting导出操作", DateTime.Now.ToString(), "OPEX_Setting导出操作，先在该表中导入数据");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {
                ImportToExcel.Export(dt, "OPEX_Setting Report", "OPEX_Setting Report", "", DateTime.Now.ToString());
                int result = logDal.OpertionLogInsert(userid, "OPEX_Setting导出操作", DateTime.Now.ToString(), "OPEX_Setting导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            #endregion

            #region  以前
            //if (imp.toExcel(dataGridView1, "OPEX mapping") == true)
            //{
            //    MessageBox.Show("导出成功");
            //    int result = logDal.OpertionLogInsert(userid, "OPEX_Setting导出数据 ", DateTime.Now.ToString(), "OPEX_Setting导出成功");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("没有数据可导出");
            //    int result = logDal.OpertionLogInsert(userid, "OPEX_Setting导出数据 ", DateTime.Now.ToString(), "OPEX_Setting没有数据可导出");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
            #endregion


        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OPEX_Setting_Load(object sender, EventArgs e)
        {
            //添加 
            DataTable dt = new DataTable();
            dt = pd.PlDbSelect();
            DataRow dr = dt.NewRow();
            dr["PIDB_Item"] = "请选择";
            dr["PLDB_Id"] = "-1";
            dt.Rows.InsertAt(dr, 0);
            this.comboPltype.DataSource = dt;
            comboPltype.DisplayMember = "PIDB_Item";
            comboPltype.ValueMember = "PLDB_Id";

            //修改
            DataTable dts = pd.PlDbSelect();
            DataRow drs = dts.NewRow();
            drs["PIDB_Item"] = "请选择";
            drs["PLDB_Id"] = "-1";
            dts.Rows.InsertAt(drs, 0);
            comboPLTy.DataSource = dt;
            comboPLTy.DisplayMember = "PIDB_Item";
            comboPLTy.ValueMember = "PLDB_Id";
            //dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 10);
            dataGridView1.DataSource = op.OpexSettingSelect();
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.Font = new System.Drawing.Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                if (item.Name.Trim().ToUpper() == "ACCOUNT_DESCRIPTION")
                {
                    item.DefaultCellStyle.Font = new Font("宋体", 10);
                }
                else
                {
                    item.DefaultCellStyle.Font = new Font("Arial", 10);
                }
            }

        }
        /// <summary>
        ///删除
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
                        if (op.deleteOPEXSetting(id) == 1)
                        {
                            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);
                            int result = logDal.OpertionLogInsert(userid, "OPEX_Setting删除操作 ", DateTime.Now.ToString(), "OPEX_Setting删除成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("操作失败");
                            int result = logDal.OpertionLogInsert(userid, "OPEX_Setting删除操作 ", DateTime.Now.ToString(), "OPEX_Setting删除成功");
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
        /// 增加  提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (comboPltype.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("请选择类别");
            }
            else if (textOpline.Text.ToString().Trim() == "" || textBuowner.Text.ToString().Trim() == "" || textAccountCode.Text.ToString().Trim() == "" || textAccountDes.Text.ToString().Trim() == "")
            {
                MessageBox.Show("有空白项，请填写完整");
            }
            else
            {
                string pltype = comboPltype.SelectedValue.ToString().Trim();
                string plline = comboPltype.SelectedText.ToString().Trim();
                string opexline = textOpline.Text.ToString().Trim();
                string budge = textBuowner.Text.ToString().Trim();
                string accountcode = textAccountCode.Text.ToString().Trim();
                string accdesc = textAccountDes.Text.ToString().Trim();
                DataTable dtExists = op.OpexSettingSelect(pltype, plline, opexline, budge, accountcode, accdesc);
                if (dtExists.Rows.Count > 0)
                {
                    MessageBox.Show("已经存在此记录" + plline);
                    int result = logDal.OpertionLogInsert(userid, "OPEX_Setting添加操作 ", DateTime.Now.ToString(), "OPEX_Setting已经存在此记录");
                    if (result < 0)
                    {
                        MessageBox.Show("日志插入失败！");
                        return;
                    }
                }
                else
                {

                    if (op.OpexSettingInsert(pltype, plline, opexline, budge, accountcode, accdesc) == 1)
                    {
                        MessageBox.Show("添加成功！");
                        comboPltype.SelectedValue = -1;
                        textOpline.Text = "";
                        textBuowner.Text = "";
                        textAccountCode.Text = "";
                        textAccountDes.Text = "";
                        int result = logDal.OpertionLogInsert(userid, "OPEX_Setting添加操作 ", DateTime.Now.ToString(), "OPEX_Setting添加成功");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }

                    }
                    else
                    {
                        MessageBox.Show("添加失败");
                        int result = logDal.OpertionLogInsert(userid, "OPEX_Setting添加操作 ", DateTime.Now.ToString(), "OPEX_Setting添加失败");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }

                    dataGridView1.DataSource = op.OpexSettingSelect();
                }
            }
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_select_Click(object sender, EventArgs e)
        {

            string pltype = "";
            string plline = "";
            if (comboPLTy.SelectedValue.ToString().Trim() != "-1")
            {
                pltype = comboPLTy.SelectedValue.ToString().Trim();
                plline = comboPLTy.SelectedText.ToString().Trim();
            }
            string opexline = textPline.Text.ToString();
            string bowner = textBOwner.Text.ToString();
            string aacode = textAcCode.Text.ToString();
            string accDesc = textAccDesc.Text.ToString();
            try
            {
                DataTable dt = null;
                if (pltype == "" && plline == "" && opexline == "" && bowner == "" && aacode == "" && accDesc == "")
                {
                    dt = op.OpexSettingSelect();
                }
                else
                {

                    dt = op.OpexSettingSelect(pltype, plline, opexline, bowner, aacode, accDesc);

                }
                dataGridView1.DataSource = dt;
                int result = logDal.OpertionLogInsert(userid, "OPEX_Setting查询操作 ", DateTime.Now.ToString(), "OPEX_Setting查询成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败！" + ex.Message);
                int result = logDal.OpertionLogInsert(userid, "OPEX_Setting查询操作 ", DateTime.Now.ToString(), "OPEX_Setting查询失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }

        /// <summary>
        /// 鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                this.contextMenuStrip.Show(MousePosition.X, MousePosition.Y);
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
        public static int ID;
        /// <summary>
        /// 双击 弹出修改页面  当修改完成又跳会本页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            UpdateOPEX up = new UpdateOPEX();
            up.Owner = this;
            up.ShowDialog();
            dataGridView1.DataSource = op.OpexSettingSelect();
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
            saveFileDialog.FileName = "OPEX mapping.xlsx";
            saveFileDialog.ShowDialog();
            string downpath = Application.StartupPath + "\\Model\\OPEX mapping.xlsx";
            System.IO.File.Delete(saveFileDialog.FileName);
            System.IO.File.Copy(downpath, saveFileDialog.FileName);
            MessageBox.Show("下载模版成功！");
            int result = logDal.OpertionLogInsert(userid, "OPEX_Setting导出excel模板操作 ", DateTime.Now.ToString(), "OPEX_Setting导出Excel模板成功");
            if (result < 0)
            {
                MessageBox.Show("日志插入失败！");
                return;
            }

        }






    }
}

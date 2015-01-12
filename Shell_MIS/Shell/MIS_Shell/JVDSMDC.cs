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
    /// 修改时间：2014-08-25
    /// 修改目的：导入，导出处理
    /// </summary>
    public partial class JVDSMDC : Form
    {
        public JVDSMDC()
        {
            InitializeComponent();
        }
        JVDSMDCDal jvdsmacdal = new JVDSMDCDal();
        DataTable dt = new DataTable();
        ImportToExcel imp = new ImportToExcel();//数据导出 2014-08-25 ydx
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        SCLJV_T5DAL sclJvT5Dal = new SCLJV_T5DAL();
        DataTable tableExis = null;//存储T5
        string cocd = "";
        /// <summary>
        /// 浏览文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT *  FROM [JV & Sites list$]", strConn);
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

            }
        }
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-08-18
        /// 修改目的：重复数据筛选
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
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    //if (dr[0].ToString().Trim() != "" && dr[1].ToString().Trim() != "" && dr[2].ToString().Trim() != "" && dr[3].ToString().Trim() != "" && dr[4].ToString().Trim() != "" && dr[6].ToString().Trim() != "")
                  //  {
                        DataTable dtExists = jvdsmacdal.JVDSMDCSelect(dr[0].ToString().Trim(), dr[1].ToString().Trim(), dr[2].ToString().Trim(), dr[3].ToString().Trim(), dr[4].ToString().Trim(), dr[5].ToString().Trim(), dr[6].ToString().Trim(), dr[7].ToString().Trim(), dr[8].ToString().Trim(), dr[9].ToString().Trim(), dr[10].ToString().Trim(), dr[11].ToString().Trim(), dr[12].ToString().Trim(), dr[13].ToString().Trim(), dr[14].ToString().Trim(), dr[15].ToString().Trim(), dr[16].ToString().Trim());//jvdsmacdal.JVDSMDCSelect(dr[0].ToString().Trim(),dr[1].ToString().Trim(),dr[2].ToString().Trim(),dr[3].ToString().Trim(),dr[4].ToString().Trim(),dr[5].ToString().Trim(),dr[6].ToString().Trim(),dr[7].ToString().Trim(),dr[8].ToString().Trim(),dr[9].ToString().Trim(),dr[10].ToString().Trim(),dr[11].ToString().Trim(),dr[12].ToString().Trim(),dr[13].ToString().Trim(),dr[14].ToString().Trim(),dr[15].ToString().Trim(),dr[16].ToString().Trim());
                        int dp = dtExists.Rows.Count;
                        if (dtExists.Rows.Count > 0)
                        {
                            dtCh.ImportRow(dr);
                            flag = false;
                            //break;
                        }
                    }

              //  }
                if (flag == false)
                {
                    MessageBox.Show("有重复数据，将重复数据保存到以下位置！");
                    if (dtCh.Rows.Count > 0)
                    {
                        if (imp.tableToExcel(dtCh, "Department Site Setting重复数据") == true)
                        {
                            MessageBox.Show("已经将重复数据导出！");
                            int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting重复数据筛选", DateTime.Now.ToString(), "JV department & Site Setting已经将重复数据导出");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("重复数据导出异常！");
                            int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting重复数据筛选", DateTime.Now.ToString(), "JV department & Site Setting重复数据导出异常");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                    }
                    // MessageBox.Show("有重复数据:" + dr[0].ToString().Trim() + "," + dr[1].ToString().Trim() + "," + dr[2].ToString().Trim() + "," + dr[3].ToString().Trim() + "," + dr[4].ToString().Trim() + "," + dr[5].ToString().Trim() + "," + dr[6].ToString().Trim() + "," + dr[7].ToString().Trim());

                }
                else
                {
                    cocd = dt.Rows[0]["CoCd"].ToString().Trim();//记录合资公司名称
                    //先将临时数据放到JVDSMDC_Temp里 
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        dr = dt.Rows[i];
                      
                        //if (dr[0].ToString().Trim() != "" && dr[1].ToString().Trim() != "" && dr[2].ToString().Trim() != "" && dr[3].ToString().Trim() != "" && dr[4].ToString().Trim() != "" && dr[6].ToString().Trim() != "")
                        //{
                            insertToSql(dr, "JVDSMDC_Temp");
                       // }
                    }
                    //在T_SCLJV_T5里不存在以下T5code
                    tableExis = sclJvT5Dal.ifExists(cocd);
                    if (tableExis != null && tableExis.Rows.Count > 0)
                    {
                        MessageBox.Show("需在表T_SCLJV_T5 里添加如下T5");
                        int aa = tableExis.Rows.Count;
                        this.dataGridView2.DataSource = tableExis;
                        sclJvT5Dal.ImportDelete();//先删除临时表  JVDSMDC_Temp
                    }
                    else
                    {

                        //DataTable tableT5Stan = sclJvT5Dal.FindSCL_JV_T5(cocd) ;//查出本公司的 jvt5对应的sclT5，以及中英文名称 
                        //if(tableT5Stan!=null&&tableT5Stan.Rows.Count>0)
                        //{
                        //    for (int i = 0; i < tableT5Stan.Rows.Count;i++ )
                        //    {
                        //        sclJvT5Dal.updateDep_TempT5code(tableT5Stan.Rows[i]["SCL_T5"].ToString().Trim(), tableT5Stan.Rows[i]["DeptNameCH"].ToString().Trim(), tableT5Stan.Rows[i]["DeptNamePinYin"].ToString().Trim(), tableT5Stan.Rows[i]["JV_T5"].ToString().Trim(), cocd);
                        //    }
                        //}
                        //DataTable tableAll = sclJvT5Dal.JVDSMDC_Temp();
                        // sclJvT5Dal.ImportDelete();//先删除临时表  JVDSMDC_Temp
                        DataTable tableTH = sclJvT5Dal.JVDSMDC_Temp(cocd);//替换后的jv部分
                        DataTable tableAll = sclJvT5Dal.JVDSMDC_TempAll(cocd);//site部分
                        tableTH.Merge(tableAll);
                        DBHelper.SqlHelp.InsertTable(tableTH, "JVDSMDC");
                        sclJvT5Dal.ImportDelete();//先删除临时表  JVDSMDC_Temp
                        MessageBox.Show("导入成功");
                        int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting重复数据筛选", DateTime.Now.ToString(), "JV department & Site Setting导入成功");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }

                    }
                }
                if (tableExis.Rows.Count <= 0)
                {
                    this.dataGridView2.DataSource = jvdsmacdal.JVDSMDCSelect();
                }
            }
            else
            {
                MessageBox.Show("没有数据！");
            }
        }
        private void insertToSql(DataRow dr, string type)
        {
            string DeptNameCH = dr["Dept Name CH"].ToString();
            string DeptNamePinyin = dr["Dept Name Pinyin"].ToString();
            string Nature = dr["Nature"].ToString();
            string HSC = dr["HSC"].ToString();
            string CoCd = dr["CoCd"].ToString();
            string T0Code = dr["T0Code"].ToString();
            string T3Code = dr["T3Code"].ToString();
            string T5Code = dr["T5Code"].ToString();
            string SiteOpenDate = dr["Site Open Date"].ToString();
            string SiteAging = dr["Site Aging"].ToString();
            string SiteStatus = dr["Site Status"].ToString();
            string TMCode = dr["TMCode"].ToString();
            string Acquired = dr["Acquired"].ToString();
            string Location = dr["Location"].ToString();
            string CRType = dr["CR Type"].ToString();
            string InvestmentType = dr["Investment Type"].ToString();
            string District = dr["District"].ToString();
            if (type == "JVDSMDC")
            {
                jvdsmacdal.JVDSMDCInsert(DeptNameCH, DeptNamePinyin, Nature, HSC, CoCd, T0Code, T3Code, T5Code, SiteOpenDate, SiteAging, SiteStatus, TMCode, Acquired, Location,
                   CRType, InvestmentType, District);
            }
            else if (type == "JVDSMDC_Temp")
            {
                jvdsmacdal.JVDSMDC_TempInsert(DeptNameCH, DeptNamePinyin, Nature, HSC, CoCd, T0Code, T3Code, T5Code, SiteOpenDate, SiteAging, SiteStatus, TMCode, Acquired, Location,
                    CRType, InvestmentType, District);
            }

        }

        private void JVDSMDC_Load(object sender, EventArgs e)
        {
            //黄晓艳 绑定cocd
            //【添加】下拉列表绑定
            DataTable tableAdd = new DataTable();
            tableAdd = jvdsmacdal.JVSelectCocd();
            DataRow dr = tableAdd.NewRow();
            dr["CoCd"] = "0";
            dr["cdNameEn"] = "请选择";
            tableAdd.Rows.InsertAt(dr, 0);

            cobCOcd.DataSource = tableAdd.DefaultView;
            cobCOcd.DisplayMember = "cdNameEn";
            cobCOcd.ValueMember = "CoCd";
            cobCOcd.SelectedItem = "请选择";


            comboCocd.DataSource = tableAdd.DefaultView;
            comboCocd.DisplayMember = "cdNameEn";
            comboCocd.ValueMember = "CoCd";
            comboCocd.SelectedItem = "请选择";


            this.dataGridView2.DataSource = jvdsmacdal.JVDSMDCSelect();
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridView2.Font = new Font("Arial", 10);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView2.RowsDefaultCellStyle.Font = new Font("Arial", 10);
            //dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);
            //dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dataGridView2.Columns["DeptNameCH"].DefaultCellStyle.Font = new Font("宋体", 10);
            //dataGridView2.Columns["TMCode"].DefaultCellStyle.Font = new Font("宋体", 10);
            //dataGridView2.Columns["District"].DefaultCellStyle.Font = new Font("宋体", 10);



        }
        //导出数据到Excel文件
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-08-25
        /// 修改目的：数据导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            #region npoi导出
            #region npoi 导出excel
            DataTable dt = (DataTable)dataGridView2.DataSource;
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("请先在该表中导入数据！");
                int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting导出操作", DateTime.Now.ToString(), "JV department & Site Setting导出操作，先在该表中导入数据");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {
                ImportToExcel.Export(dt, "JV department & Site Setting Report", "JV department & Site Setting Report", "", DateTime.Now.ToString());
                int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting导出操作", DateTime.Now.ToString(), "JV department & Site Setting导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            #endregion
            #endregion
            #region 以前导出
            //if (imp.toExcel(dataGridView2, "Department Site Setting") == true)
            //{
            //    MessageBox.Show("导出成功");
            //    int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting导出数据到Excel文件", DateTime.Now.ToString(), "JV department & Site Setting导出成功");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("没有数据可导出");
            //    int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting导出数据到Excel文件", DateTime.Now.ToString(), "JV department & Site Setting没有数据可导出");
            //    if (result < 0)
            //    {
            //        MessageBox.Show("日志插入失败！");
            //        return;
            //    }
            //}
            #endregion


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
                        if (jvdsmacdal.JVDSMDCDelete(id) == 1)
                        {
                            dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[i - 1].Index);
                            int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting删除操作", DateTime.Now.ToString(), "JV department & Site Setting删除成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("操作失败！");
                            int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting删除操作", DateTime.Now.ToString(), "JV department & Site Setting删除失败");
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
        /// 添加人：ydx
        /// 添加时间：2014-11-20
        /// 添加目的：添加的同时要替换 T5code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_Insert_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            string DeptNameCH = txt_DNCH.Text.ToString().Trim();
            string DeptNamePinyin = txt_DNP.Text.ToString().Trim();
            string Nature = txt_Nature.Text.ToString().Trim();
            string HSC = txt_HSC.Text.ToString().Trim();
            // string CoCd = txt_CoCd.Text.ToString().Trim();
            //黄晓艳修改
            string CoCd = cobCOcd.SelectedValue.ToString().Trim();
            if (CoCd == "0")
            {
                MessageBox.Show("请选择合资公司！");
                return;
            }
            string T0Code = txt_T0Code.Text.ToString().Trim();
            string T3Code = txt_T3Code.Text.ToString().Trim();
            string T5Code = txt_T5Code.Text.ToString().Trim();
            string SiteOpenDate = txt_SOD.Text.ToString().Trim();
            string SiteAging = txt_SA.Text.ToString().Trim();
            string SiteStatus = txt_SS.Text.ToString().Trim();
            string TMCode = txt_TMCode.Text.ToString().Trim();
            string Acquired = txt_Acquired.Text.ToString().Trim();
            string Location = txt_Location.Text.ToString().Trim();
            string CRType = txt_CRType.Text.ToString().Trim();
            string InvestmentType = txt_IT.Text.ToString().Trim();
            string District = txt_District.Text.ToString().Trim();
            if (this.txt_DNCH.Text.ToString().Trim() == "" || this.txt_DNP.Text.ToString().Trim() == "" || this.txt_Nature.Text.ToString().Trim() == "" || this.txt_HSC.Text.ToString().Trim() == ""
                     || this.txt_T0Code.Text.ToString().Trim() == "" || this.txt_T3Code.Text.ToString().Trim() == "" || this.txt_T5Code.Text.ToString().Trim() == ""
                || this.txt_SOD.Text.ToString().Trim() == "" || this.txt_SA.Text.ToString().Trim() == "" || this.txt_SS.Text.ToString().Trim() == "" || this.txt_TMCode.Text.ToString().Trim() == ""
                || this.txt_Acquired.Text.ToString().Trim() == "" || this.txt_Location.Text.ToString().Trim() == "" || this.txt_CRType.Text.ToString().Trim() == "" || this.txt_IT.Text.ToString().Trim() == ""
                || this.txt_District.Text.ToString().Trim() == "")
            {
                MessageBox.Show("有空白信息，请填写完整");
            }
            else
            {
                int depId = 0;
                DataTable dtExists = jvdsmacdal.JVDSMDCSelect(DeptNameCH, DeptNamePinyin, Nature, HSC, CoCd, T0Code, T3Code, T5Code, SiteOpenDate, SiteAging, SiteStatus, TMCode, Acquired, Location, CRType, InvestmentType, District);
                if (dtExists.Rows.Count > 0)
                {
                    MessageBox.Show("已经存在此油站");
                }
                else
                {

                    //验证T_SCLJV_T5里是否有 JV_T5
                    DataTable tableExis = sclJvT5Dal.ifExists(CoCd, T5Code);
                    if (tableExis == null || tableExis.Rows.Count <= 0)
                    {
                        MessageBox.Show("T_SCLJV_T5里没有此T5：" + T5Code);
                    }
                    else //已经存在
                    {
                        depId = jvdsmacdal.JVDSMDCInsert(DeptNameCH, DeptNamePinyin, Nature, HSC, CoCd, T0Code, T3Code, T5Code, SiteOpenDate, SiteAging, SiteStatus, TMCode, Acquired, Location,
                       CRType, InvestmentType, District);
                        if (depId > 0)
                        {

                            DataTable tableField = sclJvT5Dal.FieldSelect(depId, CoCd);//查出标准T5对应的数据行
                            //替换部门表里的  T5code ，英文缩写，中文名称
                            int updateT5 = sclJvT5Dal.updateDepT5code(depId, tableField.Rows[0]["SCL_T5"].ToString().Trim(), tableField.Rows[0]["DeptNameCH"].ToString().Trim(), tableField.Rows[0]["DeptNamePinYin"].ToString().Trim());
                            MessageBox.Show("增加成功！");
                            this.dataGridView2.DataSource = jvdsmacdal.JVDSMDCSelect();
                            txt_DNCH.Clear();
                            txt_DNP.Clear();
                            txt_Nature.Clear();
                            txt_HSC.Clear();
                            txt_T0Code.Clear();
                            txt_T3Code.Clear();
                            txt_T5Code.Clear();
                            txt_SOD.Clear();
                            txt_SA.Clear();
                            txt_SS.Clear();
                            txt_TMCode.Clear();
                            txt_Acquired.Clear();
                            txt_Location.Clear();
                            txt_CRType.Clear();
                            txt_IT.Clear();
                            txt_District.Clear();
                            #region 日志
                            int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting添加操作", DateTime.Now.ToString(), "JV department & Site Setting添加成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                            #endregion
                            dataGridView2.CurrentCell = dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[0];

                        }
                        else
                        {
                            MessageBox.Show("操作失败！");
                            #region 日志
                            int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting插入操作", DateTime.Now.ToString(), "JV department & Site Setting操作失败");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                            #endregion

                        }
                    }
                }

            }

        }

        private void but_Select_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
            try
            {
                string DeptNameCH = txt_SDNCH.Text.ToString().Trim();
                string DeptNamePinyin = txt_SDNP.Text.ToString().Trim();
                string Nature = txt_SNature.Text.ToString().Trim();
                string HSC = txt_SHSC.Text.ToString().Trim();
                //  string CoCd = txt_CoCd.Text.ToString().Trim();
                //黄晓艳修改
                string CoCd = comboCocd.SelectedValue.ToString().Trim();
                if (CoCd == "")
                {
                    MessageBox.Show("请选择合资公司！");
                    return;
                }
                string T0Code = txt_ST0Code.Text.ToString().Trim();
                string T3Code = txt_ST3Code.Text.ToString().Trim();
                string T5Code = txt_ST5Code.Text.ToString().Trim();
                string SiteOpenDate = txt_SSOD.Text.ToString().Trim();
                string SiteAging = txt_SSA.Text.ToString().Trim();
                string SiteStatus = txt_SSS.Text.ToString().Trim();
                string TMCode = txt_STMCode.Text.ToString().Trim();
                string Acquired = txt_SAcquired.Text.ToString().Trim();
                string Location = txt_SLocation.Text.ToString().Trim();
                string CRType = txt_SCRType.Text.ToString().Trim();
                string InvestmentType = txt_SIT.Text.ToString().Trim();
                string District = txt_SDistrict.Text.ToString().Trim();
                dt = jvdsmacdal.JVDSMDCSelect(DeptNameCH, DeptNamePinyin, Nature, HSC, CoCd, T0Code, T3Code, T5Code, SiteOpenDate, SiteAging, SiteStatus, TMCode, Acquired, Location, CRType,
                    InvestmentType, District);
                this.dataGridView2.DataSource = dt;
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting查询操作", DateTime.Now.ToString(), "JV department & Site Setting查询操作失败");
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
            UpdateJVDSMDC jvdsmdc = new UpdateJVDSMDC();
            jvdsmdc.Owner = this;
            jvdsmdc.ShowDialog();
            this.dataGridView2.DataSource = jvdsmacdal.JVDSMDCSelect();
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-08-01
        /// 添加目的：模版下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Excel模版文件到";
            saveFileDialog.FileName = "Department Site Setting.xlsx";
            saveFileDialog.ShowDialog();
            string downpath = Application.StartupPath + "\\Model\\Department Site Setting.xlsx";
            System.IO.File.Delete(saveFileDialog.FileName);
            System.IO.File.Copy(downpath, saveFileDialog.FileName);
            MessageBox.Show("下载模版成功！");
            int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting模板下载操作", DateTime.Now.ToString(), "JV department & Site Setting模板下载操作成功");
            if (result < 0)
            {
                MessageBox.Show("日志插入失败！");
                return;
            }
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            int r = 0;
            if (e.RowIndex < 0)
            {
                r = 0;
            }
            else
            {
                r = e.RowIndex;
            }

            dataGridView2.Rows[r].Cells[e.ColumnIndex].Style.Font = new Font("Arial", 10, FontStyle.Regular);

        }

    }
}

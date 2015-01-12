using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using System.IO;
using MIS_Shell.CommExcel;

namespace MIS_Shell
{
    /// <summary>
    /// 添加人：ydx
    /// 添加时间：2014-08-04
    /// 添加目的：kpi报表
    /// </summary>
    public partial class KPIReport : Form
    {
        string where = string.Empty;
        string whereS = string.Empty;
        public static string kpiStaticCocd = "";
        ImportDal importdal = new ImportDal();
        DataTable dtSite = new DataTable();//油站
        DataTable dt = new DataTable();//kpi数据
        KPIReportDAL kpiDal = new KPIReportDAL();
        KPIReportSDAL kpiSDal = new KPIReportSDAL();
        KPIFunctionDAL kpiFunDal = new KPIFunctionDAL();
        ImportToExcel imp = new ImportToExcel();//ydx 2014-08-25 数据导出
        TCodeDal tCodDal = new TCodeDal();//t8
        JVDal jvdal = new JVDal();//合资公司
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        public KPIReport()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-08-20
        /// 修改目的：导出数据格式处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KPIReport_Load(object sender, EventArgs e)
        {
            dateTimeStar.CustomFormat = "yyyy-MM";
            dateTimeEnd.CustomFormat = "yyyy-MM";
            //增加合资公司下拉列表  查询条件
            this.comboCocd.SelectedText = "请选择";
            DataTable dt = new DataTable();
            dt = jvdal.JVCocd();
            DataRow dr = dt.NewRow();
            dr["CoCd"] = "请选择";
            dr["cdNameEn"] = "请选择";
            dt.Rows.InsertAt(dr, 0);
            comboCocd.DataSource = dt;
            comboCocd.DisplayMember = "cdNameEn";
            comboCocd.ValueMember = "CoCd";
            //设置gridview样式
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.Font = new Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-08-07
        /// 添加目的：kpi导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_import_Click(object sender, EventArgs e)
        {

            #region npoi 导出excel
            DataTable dt = (DataTable)dataGridView1.DataSource;
            string compeny = comboCocd.GetItemText(comboCocd.Items[comboCocd.SelectedIndex]);
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("请先在该表中导入数据！");
                int result = logDal.OpertionLogInsert(userid, "KPIReport 导出操作", DateTime.Now.ToString(), "KPIReport导出操作，先在该表中导入数据");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            else
            {

                ImportToExcel.Export(dt, "KPIReport", "KPIReport", compeny, DateTime.Now.ToString());
                int result = logDal.OpertionLogInsert(userid, "KPIReport导出操作", DateTime.Now.ToString(), "KPIReport导出成功");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
            #endregion         

            //if (imp.toExcel(dataGridView1, "KPI Report") == true)
            //{
            //    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            //    MessageBox.Show("导出成功");
            //    this.Cursor = System.Windows.Forms.Cursors.Default;
            //}else
            //{
            //    MessageBox.Show("没有数据可导出");
            //}
        }
        /// <summary>
        /// 查询出的数据暂时放在此处
        /// 添加人：ydx
        /// 添加时间：2014-08-20
        /// 添加目的：查询  按钮触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            if (comboCocd.SelectedValue.ToString().Trim()=="请选择")
            {
                MessageBox.Show("请选择合资公司");
            }else
            {
                if (MIS_KPI_T5.staticT5 == "" && MIS_KPI_T5.staticT8 == "")
                {
                    where = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' ";
                    whereS = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' ";
                }
                else if (MIS_KPI_T5.staticT5 != "" && MIS_KPI_T5.staticT8 == "")
                {
                    where = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' and imp.T5 ( select dd.T5Code from JVDSMDC dd where dd.T5Code in(" + MIS_KPI_T5.staticT5 + ")) ";
                    whereS = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' ";
                }
                else if (MIS_KPI_T5.staticT5 == "" && MIS_KPI_T5.staticT8 != "")
                {
                    where = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' and imp.T8 in(" + MIS_KPI_T5.staticT8 + ") ";
                    whereS = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' ";
                }
                else if (MIS_KPI_T5.staticT5 != "" && MIS_KPI_T5.staticT8 != "")
                {
                    whereS = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' ";
                    where = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' and imp.T5 in( select dd.T5Code from JVDSMDC dd where dd.T5Code in(" + MIS_KPI_T5.staticT5 + ")) and imp.T8 in(" + MIS_KPI_T5.staticT8 + ") ";
                }
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                string starTimes = dateTimeStar.Text.ToString().Trim();
                string endTimes = dateTimeEnd.Text.ToString().Trim();
                //将时间转换成三位的  月份
                string star = starTimes.Substring(0, 5) + "0" + starTimes.Substring(5, 2);
                string end = endTimes.Substring(0, 5) + "0" + endTimes.Substring(5, 2);
                //油站开关 5类
                //第1类
                DataTable dtOne = kpiDal.getTotalSiteinOperation(star, end);
                if (dtOne != null && dtOne.Rows.Count > 0)
                {
                    dt = dtOne.Copy();
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    dt.DataSet.EnforceConstraints = false;
                }
                // 第2类
                DataTable dtTwo = kpiDal.getNoofTEMPORARYCLOSED(star, end);
                dt.Merge(dtTwo);
                //第3类
                DataTable dtThree = kpiDal.getNoofNewOpenedSite(star, end);
                dt.Merge(dtThree);
                //第4类
                DataTable dtFour = kpiDal.getNoofclosedSites(star, end);
                dt.Merge(dtFour);
                //第5类
                DataTable dtFive = kpiDal.getNoofNewSecuredSite(star, end);
                dt.Merge(dtFive);

                //第0类 OPERATIONAL DAYS  2014-08-21
                DataTable dt0 = kpiSDal.getOPERATIONALDAYS(star, end, whereS);
                //dt.Merge(dt0,false, MissingSchemaAction.Ignore);
                dt.Merge(dt0);
                //int a = dt0.Rows.Count;
                // 第一类2014-08-20
                //DataTable dt1 = kpiSDal.getVOLUMELITRES(star, end, where);
                DataTable dt1 = kpiSDal.getVOLUMELITRES(star, end, where);
                // dt.Merge(dt1, false, MissingSchemaAction.Ignore);
                dt.Merge(dt1);
                //int b = dt1.Rows.Count;
                //第二类数据 ATP 由第一类的Volume of FUEL除以OPERATIONAL DAYS乘以365获得
                DataTable dt2 = kpiSDal.getATP(star, end, where, whereS);//第二类数据 
                //dt.Merge(dt2, false, MissingSchemaAction.Ignore);
                dt.Merge(dt2);
                // int c = dt2.Rows.Count;
                // 2第三类 GROSS PROCEEDS OF SALES
                DataTable dt3 = kpiSDal.getGROSSPROCEEDSOFSALES(star, end, where);
                //dt.Merge(dt3, false, MissingSchemaAction.Ignore);
                dt.Merge(dt3);
                //  int d = dt3.Rows.Count;
                //foreach (DataRow dr in dt3.Rows)
                //{
                //    dt.ImportRow(dr);
                //}
                //第四类 GENERAL PRICE REDUCTIONS
                DataTable dt4 = kpiSDal.getGENERALPRICEREDUCTIONS(star, end, where);// kpiFunDal.changeStyle(kpiDal.getGENERALPRICEREDUCTIONS("2013-10", "2014-07"));
                // dt.Merge(dt4, false, MissingSchemaAction.Ignore);
                dt.Merge(dt4);
                //  int d4 = dt4.Rows.Count;
                //第五类 NET PROCEEDS OF SALES (Based on pump price)
                DataTable dt5 = kpiSDal.getNETPROCEEDSOFSALESBasedonpumpprice(star, end, where); //kpiFunDal.changeStyle(kpiDal.getNETPROCEEDSOFSALESBasedonpumpprice("2013-10", "2014-07"));
                //dt.Merge(dt5, false, MissingSchemaAction.Ignore);
                dt.Merge(dt5);
                // int d5 = dt5.Rows.Count;
                //第六类  COST OF GOOD SOLD
                DataTable dt6 = kpiSDal.getCOSTOFGOODSOLD(star, end, where);// kpiFunDal.changeStyle(kpiDal.getCOSTOFGOODSOLD("2013-10", "2014-07"));
                //dt.Merge(dt6, false, MissingSchemaAction.Ignore);
                dt.Merge(dt6);
                // int d6 = dt6.Rows.Count;
                // 2第七类 C1/C2 GROSS MARGIN 
                DataTable dt7 = kpiSDal.getC1C2GROSSMARGIN(star, end, where); //kpiSDal.getC1C2GROSSMARGIN("2013-10", "2014-07");
                //dt.Merge(dt7, false, MissingSchemaAction.Ignore);
                dt.Merge(dt7);
                // int d7 = dt7.Rows.Count;
                // 2第8类 C2 TO C3 COST
                DataTable dt8 = kpiSDal.getC2TOC3COST(star, end, where); //kpiSDal.getC1C2GROSSMARGIN("2013-10", "2014-07");
                //dt.Merge(dt8, false, MissingSchemaAction.Ignore);
                dt.Merge(dt8);
                // int d8 = dt8.Rows.Count;
                //第九类 C2 TO C3 COST BY CATEGORY
                DataTable dt9 = kpiSDal.getC2TOC3COSTBYCATEGORY(star, end, where);//kpiFunDal.changeStyles(kpiDal.getC2TOC3COSTBYCATEGORY("2013-10", "2014-07"));
                dt.Merge(dt9, false, MissingSchemaAction.Ignore);
                // 3 第10类 C3 GROSS MARGIN (UGM)
                DataTable dt10 = kpiSDal.getC3GROSSMARGIN_UGM(star, end, where);//kpiFunDal.changeStyles(kpiDal.getC2TOC3COSTBYCATEGORY("2013-10", "2014-07"));
                dt.Merge(dt10, false, MissingSchemaAction.Ignore);
                ////第11类 TOTAL SITE OPEX EXP.
                DataTable dt11 = kpiSDal.getTOTALSITEOPEXEXP(star, end, where);// kpiFunDal.changeStyles(kpiDal.getTOTALSITEOPEXEXP("2013-10", "2014-07"));
                dt.Merge(dt11, false, MissingSchemaAction.Ignore);
                // 第12类 TOTAL C4
                DataTable dt12 = kpiSDal.getTOTALC4(star, end, where);// kpiFunDal.changeStyles(kpiDal.getTOTALSITEOPEXEXP("2013-10", "2014-07"));
                dt.Merge(dt12, false, MissingSchemaAction.Ignore);
                //第13类  TOTAL JV/SCL OVERHEAD
                DataTable dt13 = kpiSDal.getTOTALJVSCLOVERHEAD(star, end, where); //kpiSDal.getTOTALJVSCLOVERHEAD(star, end);//kpiFunDal.changeStyles(kpiDal.getTOTALJVSCLOVERHEAD("2013-10", "2014-07"));
                dt.Merge(dt13, false, MissingSchemaAction.Ignore);
                //第14类  OTHER INCOME/EXPENSE
                DataTable dt14 = kpiSDal.getOTHERINCOMEeXPENSE(star, end, where); //kpiFunDal.changeStyles(kpiDal.getOTHERINCOMEeXPENSE("2013-10", "2014-07"));
                dt.Merge(dt14, false, MissingSchemaAction.Ignore);
                //第15类 C5 BUSINESS CONTRIBUTION （CA）
                DataTable dt15 = kpiSDal.getC5BUSINESSCONTRIBUTIONs(star, end, where);//kpiFunDal.changeStyle15(kpiDal.getC5BUSINESSCONTRIBUTION("2013-10", "2014-07"));
                dt.Merge(dt15, false, MissingSchemaAction.Ignore);
                //第16类1  FIN / HR / IT / LG  6*
                DataTable dt16 = kpiSDal.getFIN_HR_IT_LG(star, end, where);//kpiFunDal.changeStyle15(kpiDal.getC5BUSINESSCONTRIBUTION("2013-10", "2014-07"));
                dt.Merge(dt16, false, MissingSchemaAction.Ignore);
                // 第17类 C5++ BUSINESS CONTRIBUTION（）=CA BUSINESS CONTRIBUTION(16)+FIN / HR / IT / LG（16）
                DataTable dt17 = kpiSDal.getC5PlusBUSINESSCONTRIBUTION(star, end, where);
                dt.Merge(dt17, false, MissingSchemaAction.Ignore);
                //C9 NIBIT=第17类
                DataTable dtNibit = dt17.Copy();
                dtNibit.Rows[0][0] = "C9 NIBIT";
                dt.Merge(dtNibit, false, MissingSchemaAction.Ignore);
                //第18类1 TAX
                DataTable dt18 = kpiSDal.getTAX(star, end, where);//kpiFunDal.changeStyle15(kpiDal.getC5BUSINESSCONTRIBUTION("2013-10", "2014-07"));
                dt.Merge(dt18, false, MissingSchemaAction.Ignore);
                //C10 NIBIAT =C9 NIBIT+第18类 TAX
                DataTable CNibit = dtNibit.Copy();
                CNibit.Rows[0][0] = "C10 NIBIAT";
                for (int k = 1; k < CNibit.Columns.Count; k++)
                {
                    CNibit.Rows[0][k] = (double.Parse(CNibit.Rows[0][k].ToString().Trim()) + double.Parse(dt18.Rows[0][k].ToString().Trim())).ToString("0.00");
                }
                dt.Merge(CNibit, false, MissingSchemaAction.Ignore);
                //第20类 FINANCING COST
                DataTable dt20 = kpiSDal.getFINANCINGCOST(star, end, where);//kpiFunDal.changeStyle15(kpiDal.getC5BUSINESSCONTRIBUTION("2013-10", "2014-07"));
                dt.Merge(dt20, false, MissingSchemaAction.Ignore);
                //C10 NIAT=C10 NIBIAT+第20类
                DataTable NIAT = CNibit.Copy();
                NIAT.Rows[0][0] = "C10 NIAT";
                for (int k = 1; k < CNibit.Columns.Count; k++)
                {
                    NIAT.Rows[0][k] = (double.Parse(CNibit.Rows[0][k].ToString().Trim()) + double.Parse(dt20.Rows[0][k].ToString().Trim())).ToString("0.00");
                }
                dt.Merge(NIAT, false, MissingSchemaAction.Ignore);
                this.Cursor = System.Windows.Forms.Cursors.Default;
                this.dataGridView1.DataSource = dt;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (i!=0)
                    {
                         dataGridView1.Columns[i].DefaultCellStyle.Format = "N2";
                         dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-27
        /// 添加目的：合资公司选择项更改 触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboCocd_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.comboCocd.SelectedValue.ToString().Trim() != "System.Data.DataRowView" && comboCocd.SelectedValue.ToString().Trim() != "请选择")//不同类别的选择条件
            {
                kpiStaticCocd = comboCocd.SelectedValue.ToString().Trim();
                MIS_KPI_T5 t5 = new MIS_KPI_T5();
                t5.Owner = this;
                t5.ShowDialog();
            }
        }
    }
}

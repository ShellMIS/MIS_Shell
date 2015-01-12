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
        string kpiType = "";//按年，月，公司
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
        public static string cocds = "";
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

            this.comboboxgroup.Items.Clear();
            this.comboboxgroup.Items.AddRange(
                new object[] { "请选择", "年", "月", "公司" });
            this.comboboxgroup.SelectedIndex = 0;

            comboboxgroup.SelectedIndex = 0;

            //增加合资公司下拉列表  查询条件
           // this.comboCocd.SelectedText = "请选择";
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
        /// 将kpi报告结
        /// </summary>
        /// <param name="star"></param>
        /// <param name="end"></param>
        /// <param name="where"></param>
        /// <param name="whereS"></param>
        /// <returns></returns>
        public DataTable getTable(string star, string end, string where, string whereS, string kpiType)
        {
            DataTable dt = new DataTable();
            #region
            //油站开关 5类
            //第1类
            DataTable dtOne = kpiDal.getTotalSiteinOperation(star, end,kpiType,whereS);
            if (dtOne != null && dtOne.Rows.Count > 0)
            {
                dt = dtOne.Copy();
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                dt.DataSet.EnforceConstraints = false;
            }
            // 第2类
            DataTable dtTwo = kpiDal.getNoofTEMPORARYCLOSED(star, end,kpiType,whereS);
            dt.Merge(dtTwo);
            //第3类
            DataTable dtThree = kpiDal.getNoofNewOpenedSite(star, end, kpiType,whereS);
            dt.Merge(dtThree);
            //第4类
            DataTable dtFour = kpiDal.getNoofclosedSites(star, end, kpiType,whereS);
            dt.Merge(dtFour);
            //第5类
            DataTable dtFive = kpiDal.getNoofNewSecuredSite(star, end, kpiType,whereS);
            dt.Merge(dtFive);

            //第0类 OPERATIONAL DAYS  2014-08-21
            DataTable dt0 = kpiSDal.getOPERATIONALDAYS(star, end, whereS, kpiType);
            dt.Merge(dt0);
            //int a = dt0.Rows.Count;
            // 第一类2014-08-20
            //DataTable dt1 = kpiSDal.getVOLUMELITRES(star, end, where);
            DataTable dt1 = kpiSDal.getVOLUMELITRES(star, end, where, kpiType,whereS);
            dt.Merge(dt1);
            //int b = dt1.Rows.Count;
            //第二类数据 ATP 由第一类的Volume of FUEL除以OPERATIONAL DAYS乘以365获得
           // DataTable dt2 = kpiSDal.getATP(star, end, where, whereS);//第二类数据 
            DataTable dt2 = kpiSDal.getATP(dt0, dt1);//第二类数据
            dt.Merge(dt2);
            //第四类 GENERAL PRICE REDUCTIONS
            DataTable dt4 = kpiSDal.getGENERALPRICEREDUCTIONS(star, end, where, kpiType,whereS);// kpiFunDal.changeStyle(kpiDal.getGENERALPRICEREDUCTIONS("2013-10", "2014-07"));
            
            //第五类 NET PROCEEDS OF SALES (Based on pump price)
            DataTable dt5 = kpiSDal.getNETPROCEEDSOFSALESBasedonpumpprice(star, end, where, kpiType,whereS); //kpiFunDal.changeStyle(kpiDal.getNETPROCEEDSOFSALESBasedonpumpprice("2013-10", "2014-07"));
           
            // 2第三类 GROSS PROCEEDS OF SALES
            DataTable dt3 = kpiSDal.getGROSSPROCEEDSOFSALES(dt5,dt4);
           // DataTable dt3 = kpiSDal.getGROSSPROCEEDSOFSALES(star, end, where);
            dt.Merge(dt3);
            dt.Merge(dt4);
            dt.Merge(dt5);
            //第六类  COST OF GOOD SOLD
            DataTable dt6 = kpiSDal.getCOSTOFGOODSOLD(star, end, where, kpiType,whereS);// kpiFunDal.changeStyle(kpiDal.getCOSTOFGOODSOLD("2013-10", "2014-07"));
            dt.Merge(dt6);
            // 2第七类 C1/C2 GROSS MARGIN 
            DataTable dt7 = kpiSDal.getC1C2GROSSMARGIN(dt5,dt6); //kpiSDal.getC1C2GROSSMARGIN("2013-10", "2014-07");
           dt.Merge(dt7);
            //第九类 C2 TO C3 COST BY CATEGORY
           DataTable dt9 = kpiSDal.getC2TOC3COSTBYCATEGORY(star, end, where, kpiType,whereS);//kpiFunDal.changeStyles(kpiDal.getC2TOC3COSTBYCATEGORY("2013-10", "2014-07"));
            // 2第8类 C2 TO C3 COST
            DataTable dt8 = kpiSDal.getC2TOC3COST(dt1,dt9); 
            dt.Merge(dt8);
            // int d8 = dt8.Rows.Count;
            dt.Merge(dt9, false, MissingSchemaAction.Ignore);
            // 3 第10类 C3 GROSS MARGIN (UGM)
            DataTable dt10 = kpiSDal.getC3GROSSMARGIN_UGM(dt7,dt8);//kpiFunDal.changeStyles(kpiDal.getC2TOC3COSTBYCATEGORY("2013-10", "2014-07"));
            dt.Merge(dt10, false, MissingSchemaAction.Ignore);
            ////第11类 TOTAL SITE OPEX EXP.
            DataTable dt11 = kpiSDal.getTOTALSITEOPEXEXP(star, end, where, kpiType,whereS);// kpiFunDal.changeStyles(kpiDal.getTOTALSITEOPEXEXP("2013-10", "2014-07"));
            dt.Merge(dt11, false, MissingSchemaAction.Ignore);
            // 第12类 TOTAL C4
            DataTable dt12 = kpiSDal.getTOTALC4(dt10, dt11);// kpiFunDal.changeStyles(kpiDal.getTOTALSITEOPEXEXP("2013-10", "2014-07"));
            dt.Merge(dt12, false, MissingSchemaAction.Ignore);
            //第13类  TOTAL JV/SCL OVERHEAD
            DataTable dt13 = kpiSDal.getTOTALJVSCLOVERHEAD(star, end, where, kpiType,whereS); //kpiSDal.getTOTALJVSCLOVERHEAD(star, end);//kpiFunDal.changeStyles(kpiDal.getTOTALJVSCLOVERHEAD("2013-10", "2014-07"));
            dt.Merge(dt13, false, MissingSchemaAction.Ignore);
            //第14类  OTHER INCOME/EXPENSE
            DataTable dt14 = kpiSDal.getOTHERINCOMEeXPENSE(star, end, where, kpiType,whereS); //kpiFunDal.changeStyles(kpiDal.getOTHERINCOMEeXPENSE("2013-10", "2014-07"));
            dt.Merge(dt14, false, MissingSchemaAction.Ignore);
            //第15类 C5 BUSINESS CONTRIBUTION （CA）
            DataTable dt15 = kpiSDal.getC5BUSINESSCONTRIBUTIONs(dt12, dt13, dt14, star, end, where, kpiType,whereS);//kpiFunDal.changeStyle15(kpiDal.getC5BUSINESSCONTRIBUTION("2013-10", "2014-07"));
            dt.Merge(dt15, false, MissingSchemaAction.Ignore);
            //第16类1  FIN / HR / IT / LG  6*
            DataTable dt16 = kpiSDal.getFIN_HR_IT_LG(star, end, where, kpiType,whereS);//kpiFunDal.changeStyle15(kpiDal.getC5BUSINESSCONTRIBUTION("2013-10", "2014-07"));
            dt.Merge(dt16, false, MissingSchemaAction.Ignore);
            // 第17类 C5++ BUSINESS CONTRIBUTION（）=CA BUSINESS CONTRIBUTION(16)+FIN / HR / IT / LG（16）
            DataTable dt17 = kpiSDal.getC5PlusBUSINESSCONTRIBUTION(dt15, dt16);
            dt.Merge(dt17, false, MissingSchemaAction.Ignore);
            //C9 NIBIT=第17类
            DataTable dtNibit = dt17.Copy();
            dtNibit.Rows[0][0] = "C9 NIBIT";
            dt.Merge(dtNibit, false, MissingSchemaAction.Ignore);
            //第18类1 TAX
            DataTable dt18 = kpiSDal.getTAX(star, end, where, kpiType,whereS);//kpiFunDal.changeStyle15(kpiDal.getC5BUSINESSCONTRIBUTION("2013-10", "2014-07"));
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
            DataTable dt20 = kpiSDal.getFINANCINGCOST(star, end, where, kpiType,whereS);//kpiFunDal.changeStyle15(kpiDal.getC5BUSINESSCONTRIBUTION("2013-10", "2014-07"));
            dt.Merge(dt20, false, MissingSchemaAction.Ignore);
            //C10 NIAT=C10 NIBIAT+第20类
            DataTable NIAT = CNibit.Copy();
            NIAT.Rows[0][0] = "C10 NIAT";
            for (int k = 1; k < CNibit.Columns.Count; k++)
            {
                NIAT.Rows[0][k] = (double.Parse(CNibit.Rows[0][k].ToString().Trim()) + double.Parse(dt20.Rows[0][k].ToString().Trim())).ToString("0.00");
            }
            dt.Merge(NIAT, false, MissingSchemaAction.Ignore);
            //第21类
            #region 六行数据计算：1，Share； 2,Total AI To Shell； 3，NIBIT Shell； 4，Tax Shell； 5，FN Cost Shell；  6,C9/C10 
            //获得公司百分比
            DataTable share = CNibit.Clone();
            DataTable tableShare=kpiSDal.getShare(kpiType,whereS);
            DataRow drC9NIBIT=dtNibit.Rows[0];//dr181  C9 NIBIT
            DataRow drTAX = dt18.Rows[0];//dr182  TAX
            DataRow drC10NIBIAT=CNibit.Rows[0];//C10 NIBIAT
            DataRow dr184=dt20.Rows[0];//FINANCING COST
            DataRow drC10NIAT = NIAT.Rows[0];//C10 NIAT
            DataRow dr = share.NewRow();//公司百分比
            if (tableShare!=null&&tableShare.Rows.Count>0)
            {
                dr[0] = "Share";
                for (int j = 1; j < share.Columns.Count;j++ )
                {
                    if(kpiType=="CoCd")
                    {
                    dr[j] = tableShare.Rows[0][j];
                    }
                    else if (kpiType == "APYear" || kpiType == "AccountPeriod")
                    {
                    dr[j] = tableShare.Rows[0][0];
                    }
                    
                    
                }
                share.Rows.InsertAt(dr,0);//公司百分比 
            }
            DataRow drTotalAlToShell = share.NewRow();//Total Al To Shell行 =(181+182+184)*187

            DataRow drNIBITShell = share.NewRow();//NIBIT Shell =181*187187
            DataRow drTaxShell = share.NewRow();//Tax Shell=182*187
            DataRow drFNCostShell = share.NewRow();//FN Cost Shell=184*187
            DataRow drC9_C10=share.NewRow();//C9/C10  =181/183
            DataRow drTaxRate = share.NewRow();//drTaxRate=Tax/(C10NIAT-Tax)
            drTotalAlToShell[0]="Total Al To Shell";
            drNIBITShell[0] = "NIBIT Shell";
            drTaxShell[0] = "Tax Shell";
            drFNCostShell[0] = "FN Cost Shell";
            drC9_C10[0] = "C10/C9";
            drTaxRate[0] = "Tax Rate";
            for (int j = 1; j < share.Columns.Count;j++ )
            {
                drTotalAlToShell[j] = double.Parse(dr[j].ToString().Trim()) * (double.Parse(drC9NIBIT[j].ToString().Trim()) + double.Parse(drTAX[j].ToString().Trim()) + double.Parse(dr184[j].ToString().Trim())) / 100;
                drNIBITShell[j] = double.Parse(dr[j].ToString().Trim()) * double.Parse(drC9NIBIT[j].ToString().Trim()) / 100; //NIBIT Shell =drC9NIBIT/187
                drTaxShell[j] = double.Parse(dr[j].ToString().Trim()) * double.Parse(drTAX[j].ToString().Trim()) / 100; //Tax Shell=drTAX*187
                drFNCostShell[j] = double.Parse(dr[j].ToString().Trim()) * double.Parse(dr184[j].ToString().Trim()) / 100; //FN Cost Shell=184*187
                // drC9_C10[j] = double.Parse(dr181[j].ToString().Trim()) / double.Parse(dr183[j].ToString().Trim()); //C9/C10  =drC10NIBIAT/drC9NIBIT
                drC9_C10[j] = double.Parse(drC10NIAT[j].ToString().Trim()) / double.Parse(drC9NIBIT[j].ToString().Trim()); //C10/C9  =drC10NIAT/drC9NIBIT
                drTaxRate[j] = (double.Parse(drTAX[j].ToString().Trim()) / (double.Parse(drC9NIBIT[j].ToString().Trim()) - double.Parse(drTAX[j].ToString().Trim()))); // =/drTAX(ddrC10NIAT-drTAX)
                    
            }
            share.Rows.InsertAt(drTotalAlToShell,1);
            share.Rows.InsertAt(drNIBITShell,2);
            share.Rows.InsertAt(drTaxShell,3);
            share.Rows.InsertAt(drFNCostShell,4);
            share.Rows.InsertAt(drC9_C10,5);
            dt.Merge(share,false, MissingSchemaAction.Ignore);
            #endregion

            #region 第22类 //Unit C3类
            DataTable table22 = dt10.Copy();//Unit C3类
            
            table22.Rows[0][0]="Unit C3";
            for(int i=0;i<(table22.Rows.Count);i++)
            {
                for (int j = 1; j < table22.Columns.Count;j++ )
                {
                    if(table22.Rows[i][j].ToString().Trim()!="0"&&dt1.Rows[i][j].ToString().Trim()!="0")
                    {
                        table22.Rows[i][j] = double.Parse(table22.Rows[i][j].ToString().Trim()) / double.Parse(dt1.Rows[i][j].ToString().Trim());
               
                    }
                    else if (table22.Rows[i][j].ToString().Trim() == "0" && dt1.Rows[i][j].ToString().Trim() != "0")
                    {
                        table22.Rows[i][j] = 0;
                    }
                    else if (table22.Rows[i][j].ToString().Trim() == "0" && dt1.Rows[i][j].ToString().Trim() == "0")
                    {
                        table22.Rows[i][j] = 0;
                    }
               }
            }
            //后三行不显示
            for (int i = 0; i < 3;i++ )
            {
                table22.Rows.RemoveAt(table22.Rows.Count-1);
            }
            DataRow drTotalUnitJVOpex=table22.NewRow();
            drTotalUnitJVOpex[0] = "Total Unit JV Opex (cpl)";
            table22.Rows.InsertAt(drTotalUnitJVOpex,0);
            dt.Merge(table22,false, MissingSchemaAction.Ignore);
            #endregion
            #region 第23类 Unit C4，24类 "Unit Site Opex";
            //Unit C4
            DataRow drUnit4 = dt12.Rows[0]; //dt.NewRow();
            //drUnit4[0] = "Unit C4";
            //Unit Site Opex
            // DataRow  drUnitSiteOpex = dt11.Rows[0];
            DataRow[] drUnitSiteOpex = dt11.Select("type='TOTAL SITE OPEX EXP.'");
            DataRow[] drTotalJVSclOverHead = dt13.Select("type='Total JV/SCL Overhead'");//drTotalJVSclOverHead
            DataRow drFIN_HR_IT_LG = dt16.Rows[0];//drFIN_HR_IT_LG
            // drUnitSiteOpex[0] = "Unit Site Opex";
            //第一类的 第一行
            DataRow dt14Row = dt1.Rows[0];
            DataRow []drVolumeofFuel=dt1.Select("type='Volume of Fuel'");
            if (dt14Row != null)
            {
                DataRow drU4 = dt.NewRow();
                drU4[0] = "Unit C4";
                DataRow drOpex = dt.NewRow();
                drOpex[0] = "Unit Site Opex";
                DataRow drUnitJVOHAboveC5 = dt.NewRow();
                drUnitJVOHAboveC5[0] = "Unit JVOH Above C5 ";
                DataRow drUnitJVOHBelowC5 = dt.NewRow();
                drUnitJVOHBelowC5[0] = "Unit JVOH Below C5 ";
                for (int j = 1; j < dt.Columns.Count; j++)
                {

                    drU4[j] = double.Parse(drUnit4[j].ToString().Trim()) / double.Parse(dt14Row[j].ToString().Trim());
                    drOpex[j] = double.Parse(drUnitSiteOpex[0][j].ToString().Trim()) / double.Parse(dt14Row[j].ToString().Trim());
                    drUnitJVOHAboveC5[j] = double.Parse(drTotalJVSclOverHead[0][j].ToString().Trim()) / double.Parse(drVolumeofFuel[0][j].ToString().Trim());//Unit JVOH Above C5 =drTotalJVSclOverHead/drVolumeofFuel
                    drUnitJVOHBelowC5[j] = double.Parse(drFIN_HR_IT_LG[j].ToString().Trim()) / double.Parse(drVolumeofFuel[0][j].ToString().Trim());//Unit JVOH Below C5  =drFIN_HR_IT_LG/drVolumeofFuel
                }
                dt.Rows.InsertAt(drU4, dt.Rows.Count + 1);
                dt.Rows.InsertAt(drOpex, dt.Rows.Count + 2);
                dt.Rows.InsertAt(drUnitJVOHAboveC5, dt.Rows.Count + 3);
                dt.Rows.InsertAt(drUnitJVOHBelowC5, dt.Rows.Count + 4);
            }
            #endregion
            #region 第24类
            DataTable table24 = dt1.Copy();
            table24.Rows[0][0] = "Product Mix";//清空此数据行的数据
            for (int j = 1; j < table22.Columns.Count;j++ )
            {
                table24.Rows[0][j] =System.DBNull.Value;
            }
            DataRow dr15 = dt1.Rows[1];
            if (dr15 != null)
            {
                for (int i = 1; i < table24.Rows.Count; i++)
                {
                    for (int j = 1; j < table24.Columns.Count; j++)
                    {
                        if (table24.Rows[i][j].ToString().Trim() == "0")
                        {
                            table24.Rows[i][j] = 0; //double.Parse(table24.Rows[i][j].ToString().Trim()) / double.Parse(dr15[j].ToString().Trim());

                        }
                        else if (table24.Rows[i][j].ToString().Trim() != "0" && dr15[j].ToString().Trim() != "0")
                        {
                            table24.Rows[i][j] = double.Parse(table24.Rows[i][j].ToString().Trim()) / double.Parse(dr15[j].ToString().Trim());

                        }
                    }
                }
            }
            //后三行不显示
            for (int i = 0; i < 4; i++)
            {
                table24.Rows.RemoveAt(table24.Rows.Count - 1);
            }
            dt.Merge(table24, false, MissingSchemaAction.Ignore);
            #endregion

            #endregion
            return dt;
        }
        /// <summary>
        /// 查询出的数据暂时放在此处
        /// 添加人：ydx
        /// 添加时间：2014-08-20
        /// 添加目的：“查询”  按钮触发
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
                if ( comboboxgroup.SelectedItem != null)
                {
                    if(comboboxgroup.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择报表类型，年、月、合资公司！");
                    }
                    else{

                        where="";
                        whereS="";
                            //选择报表输出类型按：合资公司、年、月
                             if (comboboxgroup.SelectedItem.ToString().Trim().Contains("年"))
                            {
                                kpiType = "APYear";
                            }
                            else if (comboboxgroup.SelectedItem.ToString().Trim().Contains("月"))
                            {
                                kpiType = "AccountPeriod";
                            }
                            else if (comboboxgroup.SelectedItem.ToString().Trim().Contains("公司"))
                            {
                                kpiType = "CoCd";
                            }

                             //where 条件，所选择的合资公司+T5+T8
                             string cocdT5T8string = MIS_KPI_T5.cocdt5t8string;
                             //如果cocdT5T8string="" 说明用户没有选择t5和t8，则此时拼接的字符串为 CN13:null:null;CN16:null:null；
                             if (cocdT5T8string == "" || cocdT5T8string == "ull:;")
                             {
                                 cocdT5T8string = "";
                                 if (this.comboCocd.CheckedItems.Count > 0)
                                 {
                                     for (int i = 0; i < comboCocd.CheckedItems.Count; i++)
                                     {
                                         DataRowView dv = ((DataRowView)comboCocd.CheckedItems[i]);
                                         string cocd = "''"+dv["CoCd"].ToString().Trim()+"''";
                                         cocdT5T8string += cocd + ":null:null;";
                                     }
                                 }
                             }
                             //cocdT5T8string串：CN13:T8fj3g:T5Rool
                             while(cocdT5T8string!="")
                             {
                                 string smallCocd = "";
                                 string smallT5 = "";
                                 string smallT8 = "";
                                 string first = cocdT5T8string.Substring(0,cocdT5T8string.IndexOf(";"));
                                 smallCocd = first.Substring(0,first.IndexOf(":"));
                                 smallT8 = first.Substring(first.IndexOf(":")+1,first.LastIndexOf(":")-first.IndexOf(":")-1); //first.Substring(first.IndexOf(":")+1,first.LastIndexOf(":")-first.IndexOf(":"));
                                 smallT5 = first.Substring(first.LastIndexOf(":")+1);

                                 if(smallCocd!="" &&smallT5!="null"&&smallT8!="null")
                                 {
                                     whereS+=" (imp.CoCd="+smallCocd+") or ";
                                     where += " (imp.CoCd=" + smallCocd + " and imp.T5 in(select distinct dd.T5Code from JVDSMDC dd where dd.CoCd=" + smallCocd + " and dd.T5Code in(" + smallT5 + ")) and imp.T8 in(" + smallT8 + ") ) or";
                                 }else if(smallCocd!="" &&smallT5!="null"&&smallT8=="null")
                                 {
                                     whereS += " (imp.CoCd=" + smallCocd + ") or ";
                                     where += " (imp.CoCd=" + smallCocd + " and imp.T5 in(select distinct dd.T5Code from JVDSMDC dd where dd.CoCd=" + smallCocd + " and dd.T5Code in(" + smallT5 + ")) ) or";
                                }
                                 else if (smallCocd != "" && smallT5 == "null" && smallT8 != "null")
                                 {
                                     whereS += " (imp.CoCd=" + smallCocd + ") or ";
                                     where += " (imp.CoCd=" + smallCocd + "  and imp.T8 in(" + smallT8 + ")) or";
                                 }
                                 else if (smallCocd != "" && smallT5 == "null" && smallT8 == "null")
                                 {
                                     whereS += " (imp.CoCd=" + smallCocd + ") or ";
                                     where += " (imp.CoCd=" + smallCocd + ") or";
                                 }

                                 cocdT5T8string = cocdT5T8string.Substring(cocdT5T8string.IndexOf(";") + 1);//循环一次之后的cocdT5T8string
                                
                             }
                             //if (MIS_KPI_T5.staticT5 == "" && MIS_KPI_T5.staticT8 == "")
                             //{
                             //    where = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' ";
                             //    whereS = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' ";
                             //}
                             //else if (MIS_KPI_T5.staticT5 != "" && MIS_KPI_T5.staticT8 == "")
                             //{
                             //    where = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' and imp.T5 ( select dd.T5Code from JVDSMDC dd where dd.T5Code in(" + MIS_KPI_T5.staticT5 + ")) ";
                             //    whereS = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' ";
                             //}
                             //else if (MIS_KPI_T5.staticT5 == "" && MIS_KPI_T5.staticT8 != "")
                             //{
                             //    where = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' and imp.T8 in(" + MIS_KPI_T5.staticT8 + ") ";
                             //    whereS = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' ";
                             //}
                             //else if (MIS_KPI_T5.staticT5 != "" && MIS_KPI_T5.staticT8 != "")
                             //{
                             //    whereS = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' ";
                             //    where = " and imp.CoCd=''" + comboCocd.SelectedValue.ToString().Trim() + "'' and imp.T5 in( select dd.T5Code from JVDSMDC dd where dd.T5Code in(" + MIS_KPI_T5.staticT5 + ")) and imp.T8 in(" + MIS_KPI_T5.staticT8 + ") ";
                             //}

                            
                             this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                             string starTimes = dateTimeStar.Text.ToString().Trim();
                             string endTimes = dateTimeEnd.Text.ToString().Trim();
                             //将时间转换成三位的  月份
                             string star = starTimes.Substring(0, 5) + "0" + starTimes.Substring(5, 2);
                             string end = endTimes.Substring(0, 5) + "0" + endTimes.Substring(5, 2);

                        if(where !="")
                        {
                            where = where.Substring(0,where.Length-3);
                        }
                        if(whereS!="")
                        {
                            whereS = whereS.Substring(0,whereS.Length-3);
                        }

                             dt = getTable(star, end, where, whereS, kpiType);//调用KPI数据表

                             this.Cursor = System.Windows.Forms.Cursors.Default;
                             this.dataGridView1.DataSource = dt;
                             for (int i = 0; i < dataGridView1.Columns.Count; i++)
                             {
                                 if (i != 0)
                                 {
                                     dataGridView1.Columns[i].DefaultCellStyle.Format = "N2";
                                     dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                 }
                             }
                       
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

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || dataGridView1.Rows.Count <= 0) return;
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? string.Empty).ToString();
        }

        /// <summary>
        /// 选择合资公司 点击“确定”按钮
        /// 选择不同的合资公司弹出新的窗体显示此合资公司下的所有T5，T8
        /// 添加时间：2015-01-04
        /// 添加人：ydx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnT5andT8_Click(object sender, EventArgs e)
        {
            //获取选择的cocd       
            if (this.comboCocd.CheckedItems.Count > 0)
            {
                cocds = "";
                for (int i = 0; i < comboCocd.CheckedItems.Count; i++)
                {
                    DataRowView dv = ((DataRowView)comboCocd.CheckedItems[i]);
                    cocds += dv["CoCd"].ToString().Trim() + ",";
                }
                string[] charcocds = cocds.Substring(0, cocds.Length - 1).Split(',');
                string aa = "','";
                cocds = string.Join(aa, charcocds);
                MIS_KPI_T5 t5 = new MIS_KPI_T5();
                t5.Owner = this;
                t5.ShowDialog();
            }
        }
    }
}

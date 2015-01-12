﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DBHelper;

namespace DAL
{
    /// <summary>
    /// 添加人：ydx
    /// 添加时间：2014-08-20
    /// 添加目的：KPI最终格式
    /// </summary>
  public  class KPIReportSDAL
  {
      KPIReportDAL kpiDal = new KPIReportDAL();
      KPIFunctionDAL kpiFunDal = new KPIFunctionDAL();
      #region 第一次查出的数据 0,(0类不用获取类别，直接)1,4,5,6,9,11,13,14,15（获得类别时特殊）
      /// <summary>
      /// 获取第0类 OPERATIONAL DAYS
      /// </summary>
      /// <param name="star"></param>
      /// <param name="end"></param>
      /// <returns></returns>
      /// <summary>
      public DataTable getOPERATIONALDAYS(string star, string end,string where)
      {
          //将第一类的数据算出并且转换成只含有类别和数据的格式
          DataTable dt1 = kpiDal.getOPERATIONALDAYS(star, end,where);
          // 将最后算出的数据乘以（-1）
          dt1 = kpiFunDal.getLastData(dt1);
          return dt1;
      }
      /// 第一类  
      /// </summary>
      /// <param name="star"></param>
      /// <param name="end"></param>
      /// <returns></returns>
      public DataTable getVOLUMELITRES(string star,string end,string where)
      {
          //将第一类的数据算出并且转换成只含有类别和数据的格式
          DataTable dt1 = kpiFunDal.changeStyle(kpiDal.getVOLUMELITRES(star, end, where));
          // 获取第一类的类别 有三列数据
         // DataTable dt1Type = kpiDal.getTypes("VOLUME LITRES");
          // 将类别和数值合并 得出所有类别的数据 并且没有数据的默认为0
         // dt1 = kpiFunDal.getAllTypes(dt1Type, dt1);
          // 将最后算出的数据乘以（-1）
          dt1 = kpiFunDal.getLastData(dt1);
          return dt1;
      }
      /// <summary>
      /// 第4类  
      /// </summary>
      /// <param name="star"></param>
      /// <param name="end"></param>
      /// <returns></returns>
      public DataTable getGENERALPRICEREDUCTIONS(string star, string end,string where)
      {
          //第四类 GENERAL PRICE REDUCTIONS
          DataTable table4 = kpiFunDal.changeStyle(kpiDal.getGENERALPRICEREDUCTIONS(star, end,where));
          // 获取第4类的类别 有三列数据
         // DataTable dtType = kpiDal.getTypes("GENERAL PRICE REDUCTIONS");
          // 将类别和数值合并 得出所有类别的数据 并且没有数据的默认为0
        //  table4 = kpiFunDal.getAllTypes(dtType, table4);
          // 将最后算出的数据乘以（-1）
          table4 = kpiFunDal.getLastData(table4);
          return table4;
      }
      /// <summary>
      /// 第5类  
      /// </summary>
      /// <param name="star"></param>
      /// <param name="end"></param>
      /// <returns></returns>
      public DataTable getNETPROCEEDSOFSALESBasedonpumpprice(string star, string end,string where)
      {
          //第五类 NET PROCEEDS OF SALES (Based on pump price)
          DataTable table5 = kpiFunDal.changeStyle(kpiDal.getNETPROCEEDSOFSALESBasedonpumpprice(star, end,where));
          // 获取第五类的类别 有三列数据
         // DataTable dtType = kpiDal.getTypes("NET PROCEEDS OF SALES (Based on pump price)");
          // 将类别和数值合并 得出所有类别的数据 并且没有数据的默认为0
          //table5 = kpiFunDal.getAllTypes(dtType, table5);
          // 将最后算出的数据乘以（-1）

          //获取NET PROCEEDS OF SALES (Based on pump price)  下 NPS OF NFUEL  下 --- OTHERS的数据
          DataTable tableOther = kpiDal.getNETPROCEEDSOFSALESBasedonpumpprice_NFUEL_OTher(star, end, where);
          if(tableOther!=null&&tableOther.Rows.Count>0)
          {
              int RowNF = 0;
              int RowTotal=0;
              for (int i = 0; i < table5.Rows.Count;i++ )
              {
                  if (table5.Rows[i][0].ToString().Trim() == "Net Proceeds of Sales-NFuel")
                  {
                      RowNF = i;
                  }
                  else if (table5.Rows[i][0].ToString().Trim() == "Net Proceeds of Sales(Based on pump price)")
                  {
                      RowTotal = i;
                  }
              }
              //将 other里的字段单独查出来并附加到最后一行
              for (int i = 1; i < table5.Columns.Count; i++)
              {
                  table5.Columns[i].ReadOnly = false;
                  tableOther.Columns[i+2].ReadOnly = false;
                  //other 行
                  table5.Rows[table5.Rows.Count-1][i] =double.Parse( tableOther.Rows[0][i+2].ToString().Trim());
                  
                  //NPS OF NFUEL行  因为添加了Other行的数据 所以对应的上一层数据要发生变化
                  table5.Rows[RowNF][i] = double.Parse(table5.Rows[RowNF][i].ToString().Trim()) + double.Parse(tableOther.Rows[0][i + 2].ToString().Trim());
                  //NET PROCEEDS OF SALES (Based on pump price)行 因为添加了Other行的数据 所以对应的最上层数据要发生变化
                  table5.Rows[RowTotal][i] = double.Parse(table5.Rows[RowTotal][i].ToString().Trim()) + double.Parse(tableOther.Rows[0][i + 2].ToString().Trim());
              }
          }
          table5 = kpiFunDal.getLastData(table5);
          return table5;
      }
      /// <summary>
      /// 第6类   COST OF GOOD SOLD
      /// </summary>
      /// <param name="star"></param>
      /// <param name="end"></param>
      /// <returns></returns>
      public DataTable getCOSTOFGOODSOLD(string star, string end, string where)
      {
          //第6类 COST OF GOOD SOLD
          DataTable table6 = kpiFunDal.changeStyle(kpiDal.getCOSTOFGOODSOLD(star, end, where));
          // 获取第6类的类别 有三列数据
         // DataTable dtType = kpiDal.getTypes("COST OF GOOD SOLD");
          // 将类别和数值合并 得出所有类别的数据 并且没有数据的默认为0
         // table6 = kpiFunDal.getAllTypes(dtType, table6);
          // 将最后算出的数据乘以（-1）
          return  kpiFunDal.getLastData(table6);
      }
      /// <summary>
      /// 第9类  C2 TO C3 COST BY CATEGORY
      /// </summary>
      /// <param name="star"></param>
      /// <param name="end"></param>
      /// <returns></returns>
      public DataTable getC2TOC3COSTBYCATEGORY(string star, string end, string where)
      {

          //第9类 C2 TO C3 COST BY CATEGORY    kpiFunDal.changeStyles一定要注意
          DataTable table = kpiFunDal.changeStyles(kpiDal.getC2TOC3COSTBYCATEGORY(star, end, where));

       
          // 获取第9类的类别 有三列数据
         // DataTable dtType = kpiDal.getTypes("9");
          // 将类别和数值合并 得出所有类别的数据 并且没有数据的默认为0
         // table = kpiFunDal.getAllTypes(dtType, table);
          // 将最后算出的数据乘以（-1）
          table = kpiFunDal.getLastData(table);

          DataTable table_stacory = kpiDal.getC2TOC3COSTBYCATEGORY_storage(star, end, where);
          table_stacory = kpiFunDal.getLastData(table_stacory);
          for (int i = 0; i < table.Rows.Count;i++ )
          {
              int a = table.Rows.Count;
              int b = table_stacory.Rows.Count;
              if(table.Rows[i][0].ToString().Trim()==table_stacory.Rows[0][0].ToString().Trim())
              {
                  for (int j = 1; j < table.Columns.Count;j++ )
                  {
                      table.Rows[i][j] = table_stacory.Rows[0][j];
                      table.Rows[0][j] = double.Parse(table.Rows[0][j].ToString()) + double.Parse(table_stacory.Rows[0][j].ToString());
                  }
              }
              if (table.Rows[i][0].ToString().Trim() == table_stacory.Rows[1][0].ToString().Trim())
              {
                  for (int j = 1; j < table.Columns.Count; j++)
                  {
                      table.Rows[i][j] = table_stacory.Rows[1][j];
                      table.Rows[0][j] = double.Parse(table.Rows[0][j].ToString()) + double.Parse(table_stacory.Rows[1][j].ToString());
                  }
              }
          }
          return table;
      }
      /// <summary>
      /// 第11类  TOTAL SITE OPEX EXP.
      /// </summary>
      /// <param name="star"></param>
      /// <param name="end"></param>
      /// <returns></returns>
      public DataTable getTOTALSITEOPEXEXP(string star, string end, string where)
      {

          //第11类 TOTAL SITE OPEX EXP.
          DataTable table = kpiFunDal.changeStyles(kpiDal.getTOTALSITEOPEXEXP(star, end, where));
          // 获取第11类的类别 有三列数据
         // DataTable dtType = kpiDal.getTypes("TOTAL SITE OPEX EXP.");
          // 将类别和数值合并 得出所有类别的数据 并且没有数据的默认为0
         // table = kpiFunDal.getAllTypes(dtType, table);
          // 将最后算出的数据乘以（-1）
          //DataTable taTotalOpex = table.Copy();//因为每次更改table里的数据 对应taTotalOpex里的数据会跟随改变 只能重新copy一遍
          //DataTable tableAll = kpiDal.getTOTALSITEOPEXEXP_Other(star,end,where);//others类的数据
          //DataRow[] drTotalSiteOpex = taTotalOpex.Select(" Type='Total Site OPEX Exp.'");//在计算之前先记录table 里的 TOTAL SITE OPEX EXP.行
          //////第14类  获取第十四类 OTHER INCOME/EXPENSE 下的Total TMSLA下的TMSAL-Chinese partner，TMSAL-SCL
          ////DataTable tableTMS = kpiFunDal.getLastData(kpiFunDal.changeStyles(kpiDal.getOTHERINCOMEeXPENSE_TotalTMSLA(star, end, where)));
          ////DEPRECIATION SITE
          //DataTable tableDepSite = kpiDal.getC5BUSINESSCONTRIBUTION_SITE(star, end, where);
          //if(table!=null&&table.Rows.Count>0&&tableAll!=null&&tableAll.Rows.Count>0)
          //{
          //    for (int i = 0; i < table.Rows.Count;i++ )
          //    {
          //        if (table.Rows[i][0].ToString().Trim() == "Others")//先替换Other行里的数据
          //        {
          //            for (int j = 1; j < table.Columns.Count;j++ )
          //            {
          //               table.Columns[j].ReadOnly=false;
          //                //都不为空
          //             //  if (tableAll.Rows[0][j + 1].ToString().Trim() != "" && drTotalSiteOpex.Length>0)
          //                   if (tableAll.Rows[0][j + 1].ToString().Trim() != "")
          //                {
          //                    table.Rows[i][j] = double.Parse(tableAll.Rows[0][j + 1].ToString().Trim());
          //                //    table.Rows[i][j] = double.Parse(tableAll.Rows[0][j + 1].ToString().Trim())- double.Parse(drTotalSiteOpex[0][j].ToString().Trim()) - double.Parse(tableDepSite.Rows[0][j].ToString());
          //                }
          //            }
          //        }
          //        else if (table.Rows[i][0].ToString().Trim() == "Total Site OPEX Exp.")
          //        {
          //            for (int j = 1; j < table.Columns.Count; j++)
          //            {
          //                table.Columns[j].ReadOnly = false;
          //                table.Rows[i][j] = double.Parse(tableAll.Rows[0][j + 1].ToString().Trim()) + double.Parse(table.Rows[i][j].ToString());// -double.Parse(tableDepSite.Rows[0][j].ToString());
          //            }
          //        }
          //    }
          //}
          table = kpiFunDal.getLastData(table);
          return table;
      }
      /// <summary>
      /// 第13类  TOTAL JV/SCL OVERHEAD
      /// </summary>
      /// <param name="star"></param>
      /// <param name="end"></param>
      /// <returns></returns>
      public DataTable getTOTALJVSCLOVERHEAD(string star, string end, string where)
      {

          //第13类 TOTAL JV/SCL OVERHEAD
          DataTable table = kpiFunDal.changeStyles(kpiDal.getTOTALJVSCLOVERHEAD(star, end,where));
          // 获取第13类的类别 有三列数据
         // DataTable dtType = kpiDal.getTypes("13");
          //获取 第13类 TOTAL JV/SCL OVERHEAD  里的other类
          //TOTAL JV/SCL OVERHEAD
          //DataTable tableAll = kpiDal.getTOTALJVSCLOVERHEADOther(star, end,where);//others 类
          //DataTable tableTotalJV = table.Copy();//
          //DataRow[] dr = tableTotalJV.Select(" Type='Total JV/SCL Overhead'");
          ////DEPRECIATION JV
          //DataTable tableDepJV = kpiDal.getC5BUSINESSCONTRIBUTION_JV(star, end, where);

          //if (tableAll != null && tableAll.Rows.Count > 0&&table!=null&&table.Rows.Count>0)
          //{
          //    for (int i = 0; i < table.Rows.Count;i++ )
          //    {
          //        if (table.Rows[i][0].ToString().Trim().ToUpper() == "Total JV/SCL Overhead")
          //      {
          //          for (int j = 1; j < table.Columns.Count;j++ )
          //          {
          //              table.Columns[j].ReadOnly = false;
          //              table.Rows[i][j] =double.Parse( tableAll.Rows[0][j].ToString().Trim());// - double.Parse(tableDepJV.Rows[0][j].ToString());//减去JV部分
          //              //table.Rows[i][j] =double.Parse( tableAll.Rows[0][j].ToString().Trim()) - double.Parse(tableDepJV.Rows[0][j].ToString());//减去JV部分
          //          }
          //      }
                  //other里的数据不用再替换了
                  //else if (table.Rows[i][0].ToString().Trim() == "Others")
                  //{
                  //    for (int j = 1; j < table.Columns.Count; j++)
                  //    {
                  //        table.Columns[j].ReadOnly = false;
                  //        table.Rows[i][j] = double.Parse(tableAll.Rows[0][j].ToString().Trim());
                  //        //table.Rows[i][j] = double.Parse(tableAll.Rows[0][j].ToString().Trim()) - double.Parse(dr[0][j].ToString().Trim());//- double.Parse(tableDepJV.Rows[0][j].ToString());
                  //    }
                  //}
          //    }
          //}
          // 将类别和数值合并 得出所有类别的数据 并且没有数据的默认为0
         // tableOther = kpiFunDal.getAllTypes(dtType, tableOther);
          // 将最后算出的数据乘以（-1）
          table = kpiFunDal.getLastData(table);
          return table;
      }
      /// <summary>
      /// 第14类  OTHER INCOME/EXPENSE
      /// </summary>
      /// <param name="star"></param>
      /// <param name="end"></param>
      /// <returns></returns>
      public DataTable getOTHERINCOMEeXPENSE(string star, string end, string where)
      {
          //第14类  获取第十四类 OTHER INCOME/EXPENSE 下的Total TMSLA下的TMSAL-Chinese partner，TMSAL-SCL
          DataTable tableTMS =kpiFunDal.getLastData( kpiFunDal.changeStyles(kpiDal.getOTHERINCOMEeXPENSE_TotalTMSLA(star, end, where)));
          //第14类 OTHER INCOME/EXPENSE
          DataTable table = kpiFunDal.getLastData(kpiFunDal.changeStyles(kpiDal.getOTHERINCOMEeXPENSE(star, end, where)));
          DataView dv = table.DefaultView;
          dv.Sort = "type desc";
          DataTable dt2 = dv.ToTable(); 
         // table.Select(" order by Type desc");
          dt2.Merge(tableTMS);
          for (int i = 1; i < dt2.Rows.Count;i++ )
          {
              for (int j = 1; j < dt2.Columns.Count; j++)
              {
                  dt2.Rows[0][j]= (double.Parse(dt2.Rows[i][j].ToString()) + double.Parse(dt2.Rows[0][j].ToString())).ToString();
              }
          }

          // 获取第14类的类别 
          //DataTable dtType = kpiFunDal.changeStyles(kpiDal.getTypes("14"));
          // 将类别和数值合并 得出所有类别的数据 并且没有数据的默认为0
         // table = kpiFunDal.getAllTypes(dtType, table);
          // 将最后算出的数据乘以（-1）
         // table = (table);
          return dt2;
      }
      /// <summary>
      /// 第15类  C5 BUSINESS CONTRIBUTION （CA）
      /// </summary>
      /// <param name="star"></param>
      /// <param name="end"></param>
      /// <returns></returns>
      public DataTable getC5BUSINESSCONTRIBUTION(string star, string end, string where)
      {
          //第15类 C5 BUSINESS CONTRIBUTION （CA）
          DataTable table = kpiFunDal.changeStyle15(kpiDal.getC5BUSINESSCONTRIBUTION(star, end, where));
          // 获取第15类的类别 有三列数据
          //DataTable dtType = kpiDal.getTypes("15");
          // 将类别和数值合并 得出所有类别的数据 并且没有数据的默认为0
          //table = kpiFunDal.getAllTypes(dtType, table);
          // 将最后算出的数据乘以（-1）
          table = kpiFunDal.getLastData(table);
          return table;
      }
       /// <summary>
       /// 第16类1  FIN / HR / IT / LG 类  AccountCode:6* 和T5:21,23,30
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
      public DataTable getFIN_HR_IT_LG(string star, string end, string where)
       {
           //将第一类的数据算出并且转换成只含有类别和数据的格式
           DataTable dt116 = kpiDal.getFIN_HR_IT_LG(star, end, where);
           // 将最后算出的数据乘以（-1）
           dt116 = kpiFunDal.getLastData(dt116);
           return dt116;
       }
        /// <summary>
       /// 第18类1 TAX
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
      public DataTable getTAX(string star, string end,string where)
      {
          //将第一类的数据算出并且转换成只含有类别和数据的格式
          DataTable dt18 = kpiDal.getTAX(star, end,where);
          // 将最后算出的数据乘以（-1）
          dt18 = kpiFunDal.getLastData(dt18);
          return dt18;
      }
      /// <summary>
      /// 第20类  FINANCING COST
      /// </summary>
      /// <param name="star"></param>
      /// <param name="end"></param>
      /// <returns></returns>
      public DataTable getFINANCINGCOST(string star, string end, string where)
      {
          //将第一类的数据算出并且转换成只含有类别和数据的格式
          DataTable dt20 = kpiDal.getFINANCINGCOST(star, end, where);
          // 将最后算出的数据乘以（-1）
          dt20 = kpiFunDal.getLastData(dt20);
          return dt20;
      }
      #endregion

      #region 根据第一次出的结果 算出的结果集 2=1/0*365 3=5-4，7=5+6，8=9*1
      #region 第2类 第2类等于 第一类的Volume of FUEL/第o类的数值*365
      public DataTable getATP(string star, string end,string where,string whereS)
      {
          DataTable tableAtp = new DataTable();
          DataTable tableOperDay = getOPERATIONALDAYS(star, end, whereS);
          DataTable table1 = getVOLUMELITRES(star, end, whereS);
         
          tableAtp = tableOperDay.Clone();
          DataRow dr1 = tableAtp.NewRow();
          tableAtp.Rows.InsertAt(dr1,0);
          tableAtp.Rows[0][0] = "ATP";
          for (int i = 0; i < table1.Rows.Count; i++)//获得第一类的Volume of FUEL行
          {
              //Volume of Fuel
              if (table1.Rows[i][0].ToString().Trim().ToLower() == "volume of fuel")
              {
                if (tableOperDay!=null&&tableOperDay.Rows.Count > 0)
                  {
                      for (int j = 1; j < table1.Columns.Count;j++ )
                      {
                          tableAtp.Columns[j].ReadOnly = false;
                          tableAtp.Rows[0][j] = double.Parse(table1.Rows[i][j].ToString()) / double.Parse(tableOperDay.Rows[0][j].ToString()) * 365;
                      }
                  }
              }
          }
          return tableAtp;
      }
      #endregion
      #region 第三类3=5-4
      /// <summary>
      ///第三类 GROSS PROCEEDS OF SALES 
      /// </summary>
      public DataTable getGROSSPROCEEDSOFSALES(string star, string end, string where)
      {
          DataTable table = new DataTable();
          //第五类 NET PROCEEDS OF SALES (Based on pump price)
          DataTable table5 = getNETPROCEEDSOFSALESBasedonpumpprice(star, end, where);
          //第四类 GENERAL PRICE REDUCTIONS
          DataTable table4 = getGENERALPRICEREDUCTIONS(star, end, where);
          if(table4.Rows.Count>0&&table5.Rows.Count>0)
          {
              table = table5.Copy();
              for (int i = 0; i < table.Rows.Count;i++ )
              {
                  if (table.Rows[i][0].ToString().Trim() == "Net Proceeds of Sales-Fuel")
                  {
                     table.Rows[i][0]="GPR OF FUEL";
                  }
                  else if (table.Rows[i][0].ToString().Trim() == "Net Proceeds of Sales-NFuel")
                  {
                      table.Rows[i][0]="GPR OF NFUEL";
                  }
                  else if (table.Rows[i][0].ToString().Trim() == "Net Proceeds of Sales(Based on pump price)")
                  {
                      table.Rows[i][0] = "GROSS PROCEEDS OF SALES";
                  }
                  for (int j = 1; j < table.Columns.Count;j++ )
                  {
                      table.Columns[j].ReadOnly = false;
                      table.Rows[i][j] = (double.Parse(table.Rows[i][j].ToString().Trim()) - double.Parse(table4.Rows[i][j].ToString().Trim())).ToString("0.00") ;

                  }
              }
          }
          return table;
      }
      #endregion
      #region 第七类 7=5+6
      /// <summary>
      ///第七类 C1/C2 GROSS MARGIN 
      /// </summary>
      public DataTable getC1C2GROSSMARGIN(string star, string end, string where)
      {
          DataTable table = new DataTable();
          //第五类 NET PROCEEDS OF SALES (Based on pump price)
          DataTable table5 = getNETPROCEEDSOFSALESBasedonpumpprice(star, end, where); //kpiFunDal.changeStyle(kpiDal.getNETPROCEEDSOFSALESBasedonpumpprice(star, end));
          //第六类 COST OF GOOD SOLD  
          DataTable table6 = getCOSTOFGOODSOLD(star, end, where); //kpiFunDal.changeStyle(kpiDal.getCOSTOFGOODSOLD(star, end));
          if (table6.Rows.Count > 0 && table6.Rows.Count > 0)
          {
              table = table5.Copy();
              for (int i = 0; i < table.Rows.Count; i++)
              {
                  if (table.Rows[i][0].ToString().Trim() == "Net Proceeds of Sales-Fuel")
                  {
                      table.Rows[i][0] = "C2 MARGIN OF FUEL";
                  }
                  else if (table.Rows[i][0].ToString().Trim() == "Net Proceeds of Sales-NFuel")
                  {
                      table.Rows[i][0] = "C2 MARGIN OF NFUEL";
                  }
                  else if (table.Rows[i][0].ToString().Trim() == "Net Proceeds of Sales(Based on pump price)")
                  {
                      table.Rows[i][0] = "C1/C2 GROSS MARGIN";
                  }
                  for (int j = 1; j < table.Columns.Count; j++)
                  {
                      table.Columns[j].ReadOnly = false;
                      table.Rows[i][j] = (double.Parse(table.Rows[i][j].ToString().Trim()) + double.Parse(table6.Rows[i][j].ToString().Trim())).ToString("0.00");
                  }
              }
          }
          return table;
      }
      #endregion

      #region 第八类 8=9*1
      /// <summary>
      ///第八类 C2 TO C3 COST  
      ///添加人：ydx
      ///添加时间：2014-08-22
      /// </summary>
      public DataTable getC2TOC3COST(string star, string end, string where)
      {
          DataTable table = new DataTable();
          DataTable table1 = getVOLUMELITRES(star, end, where);//第1类 VOLUME LITRES
          DataTable table9 = getC2TOC3COSTBYCATEGORY(star, end, where);//第9类
         // DataRow drc113 = null;
        // double []  dr113=new double[table1.Columns.Count-1];
          DataRow drc114=null;
           //DataRow drc115=null;
           //DataRow drc116=null;
           //DataRow drc117=null;
           //DataRow drc118=null;
           if (table9.Rows.Count > 0 && table9.Rows.Count > 0)
          {
              table = table1.Copy();
             // table = table1.Clone();
              // c114 C2 TO C3 COST BY CATEGORY
              // c115 --- SECONDARY TRANSPORTATION
              // c116  --- STORAGE & HANDLING
              // c117  --- Others(Fuel Stock Loss/Additive/Quality Control Center/Other)
              // c118 CHECK
               //此循环用于分别给 c114到c118赋值
              for (int i = 0; i < table9.Rows.Count;i++ )
              {
                  string bbc = table9.Rows.Count.ToString();
                  string bcs = table9.Rows[i][0].ToString().Trim();
                  if (table9.Rows[i][0].ToString().Trim() == "C2 To C3 Cost By Category")
                  {
                      drc114 = table9.Rows[i];
                  }
                  //
                  //else if (table9.Rows[i][0].ToString().Trim() == "--- Secondary Transportation")
                  //{
                  //    drc115 = table9.Rows[i];
                  //}
                  //else if (table9.Rows[i][0].ToString().Trim() == "--- Storage & Handling")
                  //{
                  //    drc116 = table9.Rows[i];
                  //}
                  //else if (table9.Rows[i][0].ToString().Trim() == "--- Others(Quality Control Center/Other)")
                  //{
                  //    drc117 = table9.Rows[i];
                  //}
                  //else if (table9.Rows[i][0].ToString().Trim() == "CHECK")
                  //{
                  //    drc118 = table9.Rows[i];
                  //}
              }
               //此循环用于给dr15赋值
              DataRow dr = table1.NewRow();
              for (int i = 0; i < table1.Rows.Count; i++)
              {
                  if (table1.Rows[i][0].ToString().Trim() == "Volume of Fuel")
                  {
                      dr = table1.Rows[i];
                  }
              }
               
               //此循环用于改变标题
              for (int i = 0; i < table.Rows.Count; i++)
              {
                  //修改标题名称
                  if (table.Rows[i][0].ToString().Trim() == "Volume of Fuel")
                  {
                      table.Rows[i][0] = "C2 TO C3 COST FUEL";
                  }
                  else if (table.Rows[i][0].ToString().Trim() == "Volume of NFuel")
                  {
                      table.Rows[i][0] = "C2 TO C3 COST NFUEL";
                  }
                  else if (table.Rows[i][0].ToString().Trim() == "Volume Litres")
                  {
                      table.Rows[i][0] = "C2 TO C3 COST";
                  }
              }
              //此循环用于计算
              for (int i = 0; i < table.Rows.Count; i++)
              {
                  string bmbb = table.Rows[i][0].ToString();
                  //计算数值并填充
                  //for (int j = 1; j < table.Columns.Count; j++)
                  //{
                     
                      if (table.Rows[i][0].ToString().Trim() == "C2 TO C3 COST")
                      {
                          for (int j = 1; j < table.Columns.Count; j++)
                          {
                              table.Columns[j].ReadOnly = false;
                              table.Rows[i][j] = drc114[j].ToString().Trim();
                          }
                      }
                      else if (table.Rows[i][0].ToString().Trim() == "C2 TO C3 COST FUEL" && drc114 != null )
                      {
                          for (int j = 1; j < table.Columns.Count; j++)
                          {
                              table.Rows[i][j] = double.Parse(drc114[j].ToString().Trim()) * double.Parse(table.Rows[i][j].ToString()) / double.Parse(dr[j].ToString());
                          }
                      }
                      //c114*c16/C15
                      else if (table.Rows[i][0].ToString().Trim() == "--- AGO" && drc114 != null)
                      {
                          for (int j = 1; j < table.Columns.Count; j++)
                          {
                            
                              table.Rows[i][j] = double.Parse(drc114[j].ToString().Trim()) * double.Parse(table.Rows[i][j].ToString()) / double.Parse(dr[j].ToString());
                          }
                      }
                      //c115*c17/C15
                     // else if (table.Rows[i][0].ToString().Trim() == "--- Mogas 90" && drc115!= null)
                      else if (table.Rows[i][0].ToString().Trim() == "--- Mogas 90" && drc114 != null)
                      {
                          for (int j = 1; j < table.Columns.Count; j++)
                          {
                              //  string bb = table.Rows[i][j].ToString();
                              table.Rows[i][j] = double.Parse(drc114[j].ToString().Trim()) * double.Parse(table.Rows[i][j].ToString()) / double.Parse(dr[j].ToString());
                          }
                      }
                    
                      //c116*c18/C15  
                      //--- Mogas 92/93
                     // else if (table.Rows[i][0].ToString().Trim() == "--- Mogas 92/93")
                      else if (table.Rows[i][0].ToString().Trim() == "--- Mogas 92/93" && drc114 != null)
                      {
                          for (int j = 1; j < table.Columns.Count; j++)
                          {
                              table.Rows[i][j] = double.Parse(drc114[j].ToString().Trim()) * double.Parse(table.Rows[i][j].ToString().Trim()) / double.Parse(dr[j].ToString().Trim());
                          }
                      }
                   
                      //c117*c19/C15
                      //--- Mogas 95/97
                     // else if (table.Rows[i][0].ToString().Trim() == "--- Mogas 95/97" && drc117 != null)
                      else if (table.Rows[i][0].ToString().Trim() == "--- Mogas 95/97" && drc114 != null)
                      {
                          for (int j = 1; j < table.Columns.Count; j++)
                          {
                              table.Rows[i][j] = double.Parse(drc114[j].ToString().Trim()) * double.Parse(table.Rows[i][j].ToString()) / double.Parse(dr[j].ToString());
                          }
                      }
                      //c118*c20/C15
                     // else if (table.Rows[i][0].ToString().Trim() == "--- Mogas 98" && drc118 != null)
                      else if (table.Rows[i][0].ToString().Trim() == "--- Mogas 98" && drc114 != null)
                      {
                          for (int j = 1; j < table.Columns.Count; j++)
                          {
                              table.Rows[i][j] = double.Parse(drc114[j].ToString().Trim()) * double.Parse(table.Rows[i][j].ToString()) / double.Parse(dr[j].ToString());
                          }
                      }
                      else if (table.Rows[i][0].ToString().Trim() == "--- Others")
                      {
                          for (int j = 1; j < table.Columns.Count; j++)
                          {
                              table.Rows[i][j] = 0;// double.Parse(drc118[j].ToString().Trim()) * double.Parse(table.Rows[i][j].ToString()) / double.Parse(dr[j].ToString());
                          }
                      }
                      else if (table.Rows[i][0].ToString().Trim() == "--- V-POWER")
                      {
                       for (int j = 1; j < table.Columns.Count; j++)
                          {
                              table.Rows[i][j] =0;// double.Parse(drc118[j].ToString().Trim()) * double.Parse(table.Rows[i][j].ToString()) / double.Parse(dr[j].ToString());
                          }
                      }
                      else if (table.Rows[i][0].ToString().Trim() == "C2 TO C3 COST NFUEL")
                      {
                          for (int j = 1; j < table.Columns.Count; j++)
                          {
                              table.Rows[i][j] = 0;
                          }
                      }
                      else if (table.Rows[i][0].ToString().Trim() == "--- CR")
                      {
                          for (int j = 1; j < table.Columns.Count; j++)
                          {
                              table.Rows[i][j] = 0;
                          }
                      }
                      else if (table.Rows[i][0].ToString().Trim() == "--- Lubes")
                      {
                          for (int j = 1; j < table.Columns.Count; j++)
                          {
                              table.Rows[i][j] = 0;
                          }
                      }
                      else if (table.Rows[i][0].ToString().Trim() == "--- Others")
                      {
                          for (int j = 1; j < table.Columns.Count; j++)
                          {
                              table.Rows[i][j] = 0;
                          }
                      }
                      //table.Rows[i][j] = double.Parse(table.Rows[i][j].ToString()) + double.Parse(table6.Rows[i][j].ToString());
               //   }
              }
          }
          return table;
      }
      #endregion
      #endregion

      #region 根据第二次出的结果 算出的结果集 10=7+8
      /// <summary>
      /// 3 第10类 C3 GROSS MARGIN (UGM)
      /// </summary>
      public DataTable getC3GROSSMARGIN_UGM(string star, string end, string where)
      {
          DataTable table = new DataTable();
          //第7类 第七类 C1/C2 GROSS MARGIN 
          DataTable table7 = getC1C2GROSSMARGIN(star, end, where); //kpiFunDal.changeStyle(kpiDal.getNETPROCEEDSOFSALESBasedonpumpprice(star, end));
          //第八类 C2 TO C3 COST  
          DataTable table8 = getC2TOC3COST(star, end, where); //kpiFunDal.changeStyle(kpiDal.getCOSTOFGOODSOLD(star, end));
          if (table7.Rows.Count > 0 && table7.Rows.Count > 0)
          {
              table = table7.Copy();
              for (int i = 0; i < table.Rows.Count; i++)
              {
                  if (table.Rows[i][0].ToString().Trim() == "C2 MARGIN OF FUEL")
                  {
                      table.Rows[i][0] = "C3 GROSS MARGIN FUEL";
                  }
                  else if (table.Rows[i][0].ToString().Trim() == "C2 MARGIN OF NFUEL")
                  {
                      table.Rows[i][0] = "C3 GROSS MARGIN NFUEL";
                  }
                  else if (table.Rows[i][0].ToString().Trim() == "C1/C2 GROSS MARGIN")
                  {
                      table.Rows[i][0] = "C3 GROSS MARGIN (UGM)";
                  }
                  
                  for (int j = 1; j < table8.Columns.Count; j++)
                  {
                      table.Columns[j].ReadOnly = false;
                      string table10C=table.Rows[i][0].ToString().Trim();
                      string table8C=table8.Rows[i][0].ToString().Trim();
                      if (table8C==table10C||table10C == "C3 GROSS MARGIN (UGM)" && table8C == "C2 TO C3 COST" || table10C == "C3 GROSS MARGIN NFUEL" && table8C == "C2 TO C3 COST NFUEL" || table10C == "C3 GROSS MARGIN FUEL" && table8C == "C2 TO C3 COST FUEL")
                      {
                          table.Rows[i][j] = (double.Parse(table.Rows[i][j].ToString().Trim()) + double.Parse(table8.Rows[i][j].ToString().Trim())).ToString("0.00");
                     }
                      else
                      {
                          table.Rows[i][j] = (double.Parse(table.Rows[i][j].ToString().Trim())).ToString("0.00");
                      }
                     }
              }
          }
          return table;
      }
      #endregion

      #region 根据第三次出的结果 算出的结果集 12=10+11
      /// <summary>
      ///第12类 TOTAL C4
      ///TOTAL C4=以下两项的和
      ///C4 FUEL DIRECT CONTRIBUTION=C3 GROSS MARGIN FUEL（10） + TOTAL SITE OPEX EXP.（11）
      ///C4 NFUEL DIRECT CONTRIBUTION=C3 GROSS MARGIN NFUEL（10）
      ///
      /// </summary>
      public DataTable getTOTALC4(string star, string end, string where)
      {
          DataRow drFUEL = null;//10
          DataRow drNFUEL = null;//10
          DataRow drTOTALSITE = null;//11
          DataTable table12 = null;//定义新类别 12
          //12类  所有类别
          DataTable table = kpiFunDal.changeStyle12(kpiDal.getTypes("12"));
          //第10类 C3 GROSS MARGIN (UGM)
          DataTable table10 = getC3GROSSMARGIN_UGM(star, end, where);
          if(table10!=null&&table10.Rows.Count>0)
          {
              for (int i = 0; i < table10.Rows.Count;i++ )
              {
                  if (table10.Rows[i][0].ToString().Trim() == "C3 GROSS MARGIN FUEL")
                  {
                      drFUEL=table10.Rows[i];
                  }
                  else if (table10.Rows[i][0].ToString().Trim() == "C3 GROSS MARGIN NFUEL")
                  {
                    drNFUEL=table10.Rows[i];
                  }
              }
          }
          //第11类 TOTAL SITE OPEX EXP.
          DataTable table11 = getTOTALSITEOPEXEXP(star, end, where);
         if(table11!=null&&table11.Rows.Count>0)
         {
               for (int i = 0; i < table11.Rows.Count; i++)
              {
                  //Total Site OPEX Exp.
                  if (table11.Rows[i][0].ToString().Trim() == "Total Site OPEX Exp.")
                  {
                      drTOTALSITE = table11.Rows[i];
                  }
              }
         }
          //生成12类
          if(table10!=null&&table10.Rows.Count>0)
          {
              table12 = table10.Clone();
              for (int i = 0; i < table.Rows.Count;i++ )
              {
                  DataRow dr = table12.NewRow();
                  table12.Rows.Add(dr);
                  table12.Rows[i][0]=table.Rows[i][0].ToString().Trim();
                  string bic = table12.Rows[i][0].ToString();
              }
              //将12类的数字数值部分添加进去
              for (int k = 0; k < table12.Rows.Count;k++ )
              {
                  if (table12.Rows[k][0].ToString().Trim() == "C4 Fuel Direct Contribution")
                  {
                    for (int j = 1; j < table12.Columns.Count; j++)
                    {
                        table12.Rows[k][j] = (double.Parse(drFUEL[j].ToString().Trim()) + double.Parse(drTOTALSITE[j].ToString().Trim())).ToString("0.00");
                    }
                  }
                  else if (table12.Rows[k][0].ToString().Trim() == "C4 NFuel Direct Contribution")
                    {
                        for (int j = 1; j < table12.Columns.Count; j++)
                        {
                            table12.Rows[k][j] = drNFUEL[j].ToString().Trim();
                        }
                    }
                  else if (table12.Rows[k][0].ToString().Trim() == "Total C4")
                  {
                      for (int j = 1; j < table12.Columns.Count; j++)
                      {
                          table12.Rows[k][j] = (double.Parse(drFUEL[j].ToString().Trim()) + double.Parse(drNFUEL[j].ToString().Trim()) + double.Parse(drTOTALSITE[j].ToString().Trim())).ToString("0.00");
                      }
                  }
              }
          }
          return table12;
      }

       /// <summary>
      /// 第15类 16类
      /// 15类  C5 BUSINESS CONTRIBUTION=TOTAL C4(12) + TOTAL JV/SCL OVERHEAD(13) + OTHER INCOME/EXPENSE(14)
      /// 16类  CA BUSINESS CONTRIBUTION= C5 BUSINESS CONTRIBUTION （15类的和）
      /// </summary>
      public DataTable getC5BUSINESSCONTRIBUTIONs(string star, string end, string where)
      {
          //DEPRECIATION SITE
          DataTable tableDepSite = kpiDal.getC5BUSINESSCONTRIBUTION_SITE(star,end,where);

          //DEPRECIATION JV
          DataTable tableDepJV = kpiDal.getC5BUSINESSCONTRIBUTION_JV(star, end, where);

          //IMPAIREMENT
          DataTable tableIMP = kpiDal.getC5BUSINESSCONTRIBUTION_IMPAIREMENT(star, end, where);

          //P/L DISPOSAL 
          DataTable tablePL_DISPOSAL = kpiDal.getC5BUSINESSCONTRIBUTION_DISPOSAL(star, end, where);

          DataTable table15 = getC5BUSINESSCONTRIBUTION(star, end, where);//新定义的15类 完全由计算生成的
          DataTable table12 = getTOTALC4(star, end, where);//12类 TOTAL C4
          DataTable table13 = getTOTALJVSCLOVERHEAD(star, end, where);//13类 TOTAL JV/SCL OVERHEAD
          DataTable table14 = getOTHERINCOMEeXPENSE(star, end, where);//14类 OTHER INCOME/EXPENSE
          DataRow dr12=null;
          DataRow dr13=null;
          DataRow dr14=null;
          //首先清空 table15里的数据
          for (int i = 0; i < table15.Rows.Count;i++ )
          {
              for (int j = 1; j < table15.Columns.Count;j++ )
              {
                  table15.Rows[i][j] = 0;
              }
          }
          for(int i=0;i<table12.Rows.Count;i++)//12类
          {
              if (table12.Rows[i][0].ToString().Trim() == "Total C4")
            {
                dr12=table12.Rows[i];
            }
          }
           for(int i=0;i<table13.Rows.Count;i++)//13类
          {
              if (table13.Rows[i][0].ToString().Trim() == "Total JV/SCL Overhead")
            {
                dr13=table13.Rows[i];
            }
          }
           for(int i=0;i<table14.Rows.Count;i++)//14类
          {
            
              if (table14.Rows[i][0].ToString().Trim() == "Other Income/Expense")
            {
                dr14=table14.Rows[i];
            }
          }
          //循环替换 C5 BUSINESS CONTRIBUTION类下的 四个子类的数值部分
         if(table15!=null&&table15.Rows.Count>0)
         {
             for (int i = 0; i < table15.Rows.Count;i++ )
             {
                 //替换掉 DEPRECIATION SITE
                 if(tableDepSite!=null&&tableDepSite.Rows.Count>0)
                 {
                     if(table15.Rows[i][0].ToString().Trim()==tableDepSite.Rows[0][0].ToString().Trim())
                     {
                         for (int j = 1; j < table15.Columns.Count;j++ )
                         {
                             table15.Columns[j].ReadOnly = false;
                             table15.Rows[i][j] =double.Parse( tableDepSite.Rows[0][j].ToString().Trim())*(-1);
                         }
                     }
                 } //替换掉DEPRECIATION JV
                  if (tableDepJV != null && tableDepJV.Rows.Count > 0)
                 {
                     if (table15.Rows[i][0].ToString().Trim() == tableDepJV.Rows[0][0].ToString().Trim())
                     {
                         for (int j = 1; j < table15.Columns.Count; j++)
                         {
                             table15.Columns[j].ReadOnly = false;
                             table15.Rows[i][j] =double.Parse( tableDepJV.Rows[0][j].ToString().Trim())*(-1);
                         }
                     }
                 }// 替换掉 IMPAIREMENT
                  if (tableIMP != null && tableIMP.Rows.Count > 0)
                 {
                     if (table15.Rows[i][0].ToString().Trim() == tableIMP.Rows[0][0].ToString().Trim())
                     {
                         for (int j = 1; j < table15.Columns.Count; j++)
                         {
                             table15.Columns[j].ReadOnly = false;
                             table15.Rows[i][j] =double.Parse( tableIMP.Rows[0][j].ToString().Trim())*(-1);
                         }
                     }
                 }
                // 替换掉P/L DISPOSAL
                  if (tablePL_DISPOSAL != null && tablePL_DISPOSAL.Rows.Count > 0)
                 {
                     if (table15.Rows[i][0].ToString().Trim() == tablePL_DISPOSAL.Rows[0][0].ToString().Trim())
                     {
                         for (int j = 1; j < table15.Columns.Count; j++)
                         {
                             table15.Columns[j].ReadOnly = false;
                             table15.Rows[i][j] =double.Parse( tablePL_DISPOSAL.Rows[0][j].ToString().Trim())*(-1);
                         }
                     }
                 }
             }
         }

           double []rowData =new double[table15.Columns.Count];//算出16 类的结果
          string []obj=new string[table15.Columns.Count];//将结果放到obj对象中
          int bb = table15.Rows.Count;
           for (int i = 0; i < table15.Rows.Count;i++ )
           {
               
               if(table15.Rows[i][0].ToString().Trim()!="")
               {
                   if (table15.Rows[i][0].ToString().Trim() == "C5 BUSINESS CONTRIBUTION")
                   {
                       for (int j = 1; j < table15.Columns.Count; j++)
                       {
                           if(dr12[j]!=null&&dr14[j]!=null)
                           {
                               table15.Columns[j].ReadOnly = false;
                               table15.Rows[i][j] = (double.Parse(dr12[j].ToString().Trim()) + double.Parse(dr13[j].ToString().Trim()) + double.Parse(dr14[j].ToString().Trim())).ToString("0.00");
                       
                           } 
                       }
                   }
                   for (int j = 1; j < table15.Columns.Count; j++)//循环获得16类的 数值部分
                   {
                       if (table15.Rows[i][j] != null && table15.Rows[i][j].ToString()!="")
                       {
                        
                           rowData[j] += double.Parse(table15.Rows[i][j].ToString().Trim());

                       }
                   }
               }
           }
          //以下代码生成第16类 整行
          obj[0]="CA BUSINESS CONTRIBUTION";
          for (int k = 1; k < table15.Columns.Count;k++ )
          {
              obj[k]=rowData[k].ToString("0.00");
          }
          DataRow dr16 = table15.NewRow();
          dr16.ItemArray = obj;
          table15.Rows.InsertAt(dr16,table15.Rows.Count);
         return table15;
      }
      ///// <summary>
      ///// 第17类 C5++ BUSINESS CONTRIBUTION（）=CA BUSINESS CONTRIBUTION(16)+FIN / HR / IT / LG（16）
      ///// </summary>
      public DataTable getC5PlusBUSINESSCONTRIBUTION(string star, string end, string where)
      {
         DataTable table17=null;//新定义的17类 完全由计算生成的
         DataTable table15 = getC5BUSINESSCONTRIBUTIONs(star, end, where);//15类
          DataRow dr15=null;
          foreach(DataRow dr in table15.Rows)
          {
              if (dr[0].ToString().Trim() == "CA BUSINESS CONTRIBUTION") ;
              dr15 = dr;
          }
          DataTable table16 = getFIN_HR_IT_LG(star, end, where);//16类 
          table17 = table16.Copy();
          table17.Rows[0][0] = "C5++ BUSINESS CONTRIBUTION";
          for (int i = 1; i < table17.Columns.Count;i++ )
          {
              table17.Rows[0][i] = double.Parse(dr15[i].ToString().Trim()) + double.Parse(table16.Rows[0][i].ToString().Trim());
          }
          return table17;
      }
      #endregion
  }
}

using System;
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
    /// 添加时间：2014-08-08
    /// 添加目的：kpi报告数据
    /// </summary>
   public  class KPIReportDAL
   {



       /// <summary>
       /// 获取类别 适合1,4,5,6,9,11,12,13,14,15
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getTypes(string type)
       {
           string sql = "";
           if (type == "9")//9
           {
               sql = @"select kp.ReportGroup, kp.ReportType as ReportSubType from KPISetting kp group by rollup( kp.ReportGroup, kp.ReportType) having ReportGroup='C2 TO C3 COST BY CATEGORY' ";

           }
           else if (type == "12")//12类
           {
               sql = @"select kp.ReportGroup, kp.ReportType as Type  from KPISetting kp  group by rollup( kp.ReportGroup, kp.ReportType)  having ReportGroup='TOTAL C4' order by kp.ReportType ";
           }
           else if (type == "13")//13类
           {
               sql = @"select kp.ReportGroup, kp.ReportType as ReportSubType from KPISetting kp group by rollup( kp.ReportGroup, kp.ReportType) having ReportGroup='TOTAL JV/SCL OVERHEAD' ";
           }
           else if (type == "14")//14类
           {
               sql = @"select kp.ReportGroup, kp.ReportType as ReportSubType from KPISetting kp group by rollup( kp.ReportGroup, kp.ReportType) having ReportGroup='OTHER INCOME/EXPENSE' ";

           }
           else if (type == "15")//15类
           {
               sql = @"select kp.Item, kp.ReportType as ReportSubType from KPISetting kp group by rollup(kp.Item, kp.ReportType)having kp.Item='CA'";
           }
           else
           {
               sql = @" select kp.ReportGroup, kp.ReportType, kp.ReportSubType from KPISetting kp  group by rollup(kp.ReportGroup, kp.ReportType, kp.ReportSubType) having kp.ReportGroup ='" + type + "'";

           }
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("type");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       #region 类别上面的  油站开、关 管理页面
       /// <summary>
       /// 第一类   Total Site in Operation
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getTotalSiteinOperation(string star, string end,string kpiType,string where)
       {
           string sql = "declare @sql varchar(max) select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9))+''''+  ";
           sql += "  ' then [TotalSiteinOperation] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9)) ";
           sql += "  from [v_T_TotalSite] bb where 1=1 and bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           
           sql+=" GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)";


           sql += " select   @sql='select ''Total Site in Operation'' as  Type , '+@sql+' from [v_T_TotalSite] imp ";
           if (where != string.Empty)
           {
               sql +=" where "+ where;
           }
           sql += " '";
           sql += " EXEC(@sql)";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("operation");
           dt.Load(dr);
           dr.Close();
           return dt;
       }

       /// <summary>
       /// 第二类   No. of TEMPORARY CLOSED
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getNoofTEMPORARYCLOSED(string star, string end, string kpiType, string  where)
       {
           string sql = @"declare @sql varchar(max)  select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9))+''''+  ' then [TemporaryClosedSites] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9)) ";
           sql += "   from [v_T_TotalSite] bb where 1=1 and bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)";
           sql += "  select   @sql=' select ''No. of TEMPORARY CLOSED'' as  Type , '+@sql+' from v_T_TotalSite imp  ";
           if (where != string.Empty)
           {
               sql += " where " + where;
           }
           sql += " '";
           sql += " EXEC(@sql)";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("site");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 第三类   No. of New Opened Site
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getNoofNewOpenedSite(string star, string end,string kpiType,string where)
       {
           string sql = @"declare @sql varchar(max) select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9))+''''+   ' then [NewOpenedSites] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9))  ";
           sql += "   from [v_T_TotalSite] bb where 1=1 and bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)";
           sql += "  select   @sql=' select ''No. of New Opened Site'' as  Type , '+@sql+' from v_T_TotalSite imp  ";
           if (where != string.Empty)
           {
               sql += " where " + where;
           }
           sql += " '";
           sql += " EXEC(@sql)";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("oy");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 第四类   No. of closed Sites
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getNoofclosedSites(string star, string end,string kpiType,string where)
       {
           string sql = @"declare @sql varchar(max) select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9))+''''+  ' then [closedSites] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9)) ";
           sql += "  from [v_T_TotalSite] bb where 1=1 and bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)";
           sql += " select   @sql=' select ''No. of closed Sites'' as  Type , '+@sql+' from v_T_TotalSite imp  ";
           if (where != string.Empty)
           {
               sql += " where " + where;
           }
           sql += " '";
           sql += " EXEC(@sql)";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("ou");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 第五类   No. of New Secured Site 
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getNoofNewSecuredSite (string star, string end,string kpiType,string where)
       {
           string sql = @"declare @sql varchar(max) select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9))+''''+   ' then [NewSecuredSites] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9)) ";
           sql += "  from [v_T_TotalSite] bb where 1=1 and bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)";
           sql += " select   @sql=' select ''No. of New Secured Site'' as  Type , '+@sql+' from v_T_TotalSite imp  ";
           if (where != string.Empty)
           {
               sql += " where " + where;
           }
           sql += " '";
           sql += " EXEC(@sql)";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("oi");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       #endregion

       #region 第一次能查出的结果 0, 1，4，5，6，9，11，13，14，15  2014-08-08
       /// <summary>
       /// 获取第0类 OPERATIONAL DAYS
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getOPERATIONALDAYS(string star, string end,string where,string kpiType)
       {
  //         string sql = @"declare @sql varchar(max) select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+
//' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)) 
//from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) order by SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if(kpiType!="")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9)) ";
           
           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (where != string.Empty)
           {
               string whereS = where.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
          
           sql += "select @sql='select kp.ReportSubType as ''Type'','+@sql+' from KPISetting kp inner join v_imp_raw imp on imp.AccountCodeD=kp.AccountCode ";
           if(where !=string.Empty)
           {
               sql +=" and "+where;
           }
        sql+=   " and kp.ReportSubType=''OPERATIONAL DAYS'' group by  kp.ReportSubType'";
           sql += " EXEC(@sql)";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("ImportOP");
           dt.Load(dr);
           dr.Close();

           return dt;
       }
       /// <summary>
        /// 获取第一类 VOLUME LITRES
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getVOLUMELITRES(string star, string end, string where, string kpiType,string whereS)
       {
           string sql = @"declare @sql varchar(max) ";
           if(kpiType!="")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),"+kpiType+"),0,9)) ";
           
           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp","bb");
              whereS= whereS.Replace("''","'");
               sql += " and " + whereS;
           }
          sql+=" GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           sql += "select @sql=' select vv.ReportSubType as ''Type'',b.* from (	select kp.ReportGroup, kp.ReportType, kp.ReportSubType  from KPISetting kp group by kp.ReportGroup, kp.ReportType, kp.ReportSubType having kp.ReportGroup =''VOLUME LITRES'') vv full join ( select kp.ReportGroup, kp.ReportType, kp.ReportSubType,'+@sql+' from KPISetting kp inner join v_imp_raw imp on imp.AccountCodeD=kp.AccountCode   ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += " and kp.T2<>'''' and kp.ReportGroup=''VOLUME LITRES'' inner join KPITemp kt on kt.Id=kp.Id and imp.T2=kt.T2 group by rollup(kp.ReportGroup, kp.ReportType, kp.ReportSubType) )b on vv.ReportGroup=b.ReportGroup and vv.ReportType=b.ReportType and vv.ReportSubType=b.ReportSubType ' ";
           sql += " EXEC(@sql)";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("kpireport");
           dt.Load(dr);
           dr.Close();

           return dt;
       }
       /// <summary>
       /// 获取第四类 GENERAL PRICE REDUCTIONS
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getGENERALPRICEREDUCTIONS(string star, string end, string where, string kpiType,string whereS)
       {
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
          // sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           sql += " select @sql=' select vv.ReportSubType as ''Type'',b.* from (select kp.ReportGroup, kp.ReportType, kp.ReportSubType  from KPISetting kp group by kp.ReportGroup, kp.ReportType, kp.ReportSubType having kp.ReportGroup =''GENERAL PRICE REDUCTIONS'') vv full join ( select kp.ReportGroup, kp.ReportType, kp.ReportSubType,'+@sql+'  ";
           sql += " from KPISetting kp inner join v_imp_raw imp  ";
           sql += " on imp.AccountCodeD=kp.AccountCode  ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += " inner join KPITemp kt ";
           sql += " on kt.Id=kp.Id  ";
           sql += " and imp.T2=kt.T2 ";
           sql += "  and kp.T2<>'''' and kp.T5='''' ";
           sql += " and kp.ReportGroup=''GENERAL PRICE REDUCTIONS'' ";
           sql += " group by rollup(kp.ReportGroup, kp.ReportType, kp.ReportSubType))b on vv.ReportGroup=b.ReportGroup and vv.ReportType=b.ReportType and vv.ReportSubType=b.ReportSubType ' ";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("ImportO");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
     /// <summary>
       /// 获取第五类 NET PROCEEDS OF SALES (Based on pump price)
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getNETPROCEEDSOFSALESBasedonpumpprice(string star, string end, string where, string kpiType,string whereS)
       {
           //string sql = @"declare @sql varchar(max)";
           //sql+="select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql+=" ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql+=" from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += " select @sql=' select vv.ReportSubType as ''Type'',b.* from (select kp.ReportGroup, kp.ReportType, kp.ReportSubType  from KPISetting kp group by kp.ReportGroup, kp.ReportType, kp.ReportSubType having kp.ReportGroup =''Net Proceeds of Sales(Based on pump price)'') vv full join ( select kp.ReportGroup, kp.ReportType, kp.ReportSubType,'+@sql+'   ";
           sql += " from KPISetting kp inner join v_imp_raw imp  ";
           sql += " on imp.AccountCodeD=kp.AccountCode   ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql+=" inner join KPITemp kt ";
           sql += " on kt.Id=kp.Id  ";
           sql+=" and imp.T2=kt.T2 ";

           sql += "  and kp.T2<>'''' and kp.T5='''' ";
           sql += " and kp.ReportGroup=''Net Proceeds of Sales(Based on pump price)'' ";
           sql += " group by rollup(kp.ReportGroup, kp.ReportType, kp.ReportSubType))b on vv.ReportGroup=b.ReportGroup and vv.ReportType=b.ReportType and vv.ReportSubType=b.ReportSubType  ' ";
           sql+=" EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("ImportOPE");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 添加人：ydx
       /// 添加时间：2014-11-18
       /// 添加目的：NET PROCEEDS OF SALES (Based on pump price)下的 NPS OF NFUEL 下的 --- OTHERS
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <param name="where"></param>
       /// <returns></returns>
       public DataTable getNETPROCEEDSOFSALESBasedonpumpprice_NFUEL_OTher(string star, string end, string where,string kpiType,string whereS)
       {
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           sql += " select  @sql=' select kp.ReportGroup, kp.ReportType, kp.ReportSubType,'+@sql+'   ";
           sql += "  from KPISetting kp inner join v_imp_raw imp   ";
           sql += " on imp.AccountCodeD=kp.AccountCode   ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += " and kp.ReportGroup=''Net Proceeds of Sales(Based on pump price)''";
           sql += " and kp.ReportType=''Net Proceeds of Sales-NFuel'' ";
           sql += " and kp.ReportSubType=''--- Others'' ";
		 sql +=" group by kp.ReportGroup, kp.ReportType, kp.ReportSubType' ";
         sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("ImportOPE_other");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 获取第六类 COST OF GOOD SOLD 
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getCOSTOFGOODSOLD(string star, string end,string where,string kpiType,string whereS)
       {
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           sql += " select @sql=' select vv.ReportSubType as ''Type'',b.* from (select kp.ReportGroup, kp.ReportType, kp.ReportSubType  from KPISetting kp group by kp.ReportGroup, kp.ReportType, kp.ReportSubType having kp.ReportGroup =''COST OF GOOD SOLD'') vv full join ( select kp.ReportGroup, kp.ReportType, kp.ReportSubType,'+@sql+' ";
           sql += " from KPISetting kp inner join v_imp_raw imp  ";
           sql += " on imp.AccountCodeD=kp.AccountCode  ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += " inner join KPITemp kt ";
           sql += " on kt.Id=kp.Id  ";
           sql += " and kp.T2<>'''' ";
           sql += " and imp.T2=kt.T2 ";
           sql += " and kp.T5='''' ";
           sql += " and kp.ReportGroup=''COST OF GOOD SOLD'' ";
           sql += " group by rollup(kp.ReportGroup, kp.ReportType, kp.ReportSubType))b on vv.ReportGroup=b.ReportGroup and vv.ReportType=b.ReportType and vv.ReportSubType=b.ReportSubType  ' ";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("o");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
        /// <summary>
       /// 获取第九类 C2 TO C3 COST BY CATEGORY 
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getC2TOC3COSTBYCATEGORY(string star, string end,string where,string kpiType,string whereS)
       {
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += "  select @sql=' select vv.ReportGroup as ''VGroup'', vv.ReportType as ''Type'',b.* from (select kp.ReportGroup, kp.ReportType from KPISetting kp group by rollup( kp.ReportGroup, kp.ReportType) having kp.ReportGroup =''C2 TO C3 COST BY CATEGORY'') vv full join ( select kp.ReportGroup, kp.ReportType, '+@sql+' ";
           sql += " from KPISetting kp inner join v_imp_raw imp   ";
           sql += " on imp.AccountCodeD=kp.AccountCode ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
        
           sql += " and kp.ReportGroup=''C2 TO C3 COST BY CATEGORY'' ";
           sql += " group by rollup(kp.ReportGroup, kp.ReportType) )b on vv.ReportGroup=b.ReportGroup and vv.ReportType=b.ReportType' ";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("oph");
           dt.Load(dr);
           dr.Close();
           return dt;
       }

       /// <summary>
       /// 获取第九类 C2 TO C3 COST BY CATEGORY 下的 --- STORAGE & HANDLING和--- Others(Quality Control Center/Other)
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getC2TOC3COSTBYCATEGORY_storage(string star, string end, string where,string kpiType,string whereS)
       {
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += "  select @sql=' select  kp.ReportType Type, '+@sql+' ";
           sql += " from KPISetting kp inner join v_imp_raw imp   ";
           sql += " on imp.AccountCodeD like''6%'' ";
           sql += " and imp.T5=kp.T5 ";
           sql += " inner join KPITemp kt on kt.Id=kp.Id and kp.T5=kt.T5 ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += " and kp.ReportGroup=''C2 TO C3 COST BY CATEGORY'' ";
           sql += " group by kp.ReportGroup, kp.ReportType ' ";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("type");
           dt.Load(dr);
           dr.Close();
           return dt;
       }

       /// <summary>
       /// 获取第十一类 TOTAL SITE OPEX EXP. 
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getTOTALSITEOPEXEXP(string star, string end,string where,string kpiType,string whereS)
       {
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += " select @sql='select vv.ReportGroup as ''VGroup'', vv.ReportType as ''Type'',b.* from (select kp.ReportGroup, kp.ReportType from KPISetting kp group by kp.ReportGroup, kp.ReportType having kp.ReportGroup =''Total Site OPEX Exp.'') vv full join ( select kp.ReportGroup, kp.ReportType, '+@sql+'   ";
           sql += " from KPISetting kp inner join v_imp_raw imp   ";
           sql += "  on imp.AccountCodeD=kp.AccountCode  ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += "  and kp.T2='''' and kp.T5=''R*''   ";
           sql+="  and imp.T5 like''R%'' ";
           sql += "  and kp.ReportGroup=''Total Site OPEX Exp.'' ";
           sql += "  group by rollup(kp.ReportGroup, kp.ReportType) )b  on vv.ReportGroup=b.ReportGroup and vv.ReportType=b.ReportType' ";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("hj");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 添加人：ydx
       /// 添加时间：2014-11-18
       /// 添加目的：TOTAL SITE OPEX EXP.  6* 下的 Other 行
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <param name="where"></param>
       /// <returns></returns>
       public DataTable getTOTALSITEOPEXEXP_Other(string star, string end, string where,string kpiType,string whereS)
       {
          
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += "  select @sql='select kp.ReportGroup, kp.ReportType, '+@sql+'    ";
           sql += " from KPISetting kp inner join v_imp_raw imp  on   imp.AccountCodeD =kp.AccountCode   ";
           sql += "    ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += "  and kp.T5=''R*'' ";
           sql += "  and imp.T5 like''R%'' ";
           sql += "  and kp.ReportGroup=''Total Site OPEX Exp.''  and kp.ReportType=''Others'' ";
           sql += "   group by kp.ReportGroup, kp.ReportType ' ";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("other");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
    /// <summary>
       /// 获取第十三类 TOTAL JV/SCL OVERHEAD 
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getTOTALJVSCLOVERHEAD(string star, string end,string where,string kpiType,string whereS)
       {
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += " select @sql='select vv.ReportGroup as ''VGroup'', vv.ReportType as ''Type'',b.* from (select kp.ReportGroup, kp.ReportType from KPISetting kp group by kp.ReportGroup, kp.ReportType having  kp.ReportGroup =''TOTAL JV/SCL OVERHEAD'') vv full join ( select kp.ReportGroup, kp.ReportType, '+@sql+' ";
           sql += " from KPISetting kp inner join v_imp_raw imp   ";
           sql += " on imp.AccountCodeD=kp.AccountCode  ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += " inner join KPITemp kt  ";
           sql += " on kt.Id=kp.Id ";
           sql+=" and imp.T5=kt.T5 ";
           sql += "  and kp.ReportGroup=''TOTAL JV/SCL OVERHEAD'' ";
           sql += "  group by rollup(kp.ReportGroup, kp.ReportType))b on vv.ReportGroup=b.ReportGroup and vv.ReportType=b.ReportType' ";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("jh");
           dt.Load(dr);
           dr.Close();
           return dt;
       }

       /// <summary>
       /// 获取第十三类 TOTAL JV/SCL OVERHEAD 里的  other类
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getTOTALJVSCLOVERHEADOther(string star, string end,string where,string kpiType,string whereS)
       {
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += " select @sql='select kp.ReportType as Type,'+@sql+' ";
           sql += " from KPISetting kp inner join v_imp_raw imp   ";
           sql += " on   imp.AccountCodeD =kp.AccountCode  ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += " inner join KPITemp kt  ";
           sql += " on kt.Id=kp.Id ";
           sql += " and imp.T5=kt.T5 ";
           sql += "  and kp.ReportGroup=''Total JV/SCL Overhead'' ";
           sql += " and kp.ReportType=''Others'' ";
           sql += "  group by kp.ReportGroup, kp.ReportType' ";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("jhother");
           dt.Load(dr);
           dr.Close();
           return dt;
       }

       /// <summary>
       /// 获取第十四类 OTHER INCOME/EXPENSE 
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getOTHERINCOMEeXPENSE(string star, string end, string where,string kpiType,string whereS)
       {
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += " select @sql='select vv.ReportGroup as ''VGroup'', vv.ReportType as ''Type'',b.* from (select kp.ReportGroup, kp.ReportType from KPISetting kp group by rollup( kp.ReportGroup, kp.ReportType) having  kp.ReportGroup =''OTHER INCOME/EXPENSE'') vv full join ( select kp.ReportGroup, kp.ReportType, '+@sql+' ";
           sql += " from KPISetting kp inner join v_imp_raw imp ";
           sql += " on imp.AccountCodeD=kp.AccountCode  ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += " and kp.ReportGroup=''OTHER INCOME/EXPENSE'' ";
           sql += " group by kp.ReportGroup, kp.ReportType)b on vv.ReportGroup=b.ReportGroup and vv.ReportType=b.ReportType' ";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("fg");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 获取第十四类 OTHER INCOME/EXPENSE 下的Total TMSLA下的TMSAL-Chinese partner，TMSAL-SCL
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getOTHERINCOMEeXPENSE_TotalTMSLA(string star, string end,string where,string kpiType,string whereS)
       {
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += " select @sql='select vv.ReportGroup as ''VGroup'', vv.ReportType as ''Type'',b.* from (select kp.ReportGroup, kp.ReportType from KPISetting kp group by  kp.ReportGroup, kp.ReportType  having  kp.ReportGroup =''Total TMSLA'') vv full join ( select kp.ReportGroup, kp.ReportType, '+@sql+' ";
           sql += " from KPISetting kp inner join v_imp_raw imp ";
           sql += " on imp.AccountCodeD=kp.AccountCode  ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += " and kp.ReportGroup=''Total TMSLA'' ";
           sql += " group by kp.ReportGroup, kp.ReportType)b on vv.ReportGroup=b.ReportGroup and vv.ReportType=b.ReportType' ";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("fg");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 获取第十五类 C5 BUSINESS CONTRIBUTION （CA）
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getC5BUSINESSCONTRIBUTION(string star, string end,string where,string kpiType,string whereS)
       {
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += " select @sql='select vv.ReportType as ''Type'',b.* from (select kp.Item, kp.ReportType from KPISetting kp group by kp.Item, kp.ReportType having  kp.Item =''CA'') vv full join ( select kp.Item, kp.ReportType, '+@sql+' ";
           sql += " from KPISetting kp inner join v_imp_raw imp   ";
           sql += " on imp.AccountCodeD=kp.AccountCode ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += "  and kp.Item=''CA'' ";
           sql += "  group by rollup(kp.Item, kp.ReportType))b on vv.Item=b.Item and vv.ReportType=b.ReportType' ";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("ty");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 添加人：ydx
       /// 添加时间：2014-11-19
       /// 添加目的：C5 BUSINESS CONTRIBUTION 下的 DEPRECIATION SITE
       ///
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <param name="where"></param>
       /// <returns></returns>
       public DataTable getC5BUSINESSCONTRIBUTION_SITE(string star, string end, string where,string kpiType,string whereS)
       {
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += " select @sql='select  kp.ReportType, '+@sql+' ";
           sql += " from KPISetting kp inner join v_imp_raw imp   ";
           sql += " on imp.AccountCodeD=kp.AccountCode   and imp.T5 like (SUBSTRING(''R*'',1,1)+''%'')";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += "  and kp.Item=''CA'' and kp.ReportType=''DEPRECIATION SITE'' ";
           sql += "  group by kp.Item, kp.ReportType' ";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("ty");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 添加人：ydx
       /// 添加时间：2014-11-19
       /// 添加目的：C5 BUSINESS CONTRIBUTION 下的 DEPRECIATION JV
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <param name="where"></param>
       /// <returns></returns>
       public DataTable getC5BUSINESSCONTRIBUTION_JV(string star, string end, string where,string kpiType,string whereS)
       {
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += " select @sql='select  kp.ReportType, '+@sql+' ";
           sql += " from KPISetting kp inner join v_imp_raw imp   ";
           sql += " on imp.AccountCodeD=kp.AccountCode inner join KPITemp kt on kt.Id=kp.Id  and imp.T5 =kt.T5";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += "  and kp.Item=''CA'' and kp.ReportType=''DEPRECIATION JV'' ";
           sql += "  group by kp.Item, kp.ReportType' ";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("ty");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 添加人：ydx
       /// 添加时间：2014-11-19
       /// 添加目的：C5 BUSINESS CONTRIBUTION 下的 IMPAIREMENT
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <param name="where"></param>
       /// <returns></returns>
       public DataTable getC5BUSINESSCONTRIBUTION_IMPAIREMENT(string star, string end, string where,string kpiType,string whereS)
       {
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += " select @sql='select  kp.ReportType, '+@sql+' ";
           sql += " from KPISetting kp inner join v_imp_raw imp   ";
           sql += " on imp.AccountCodeD=kp.AccountCode inner join KPITemp kt on kt.Id=kp.Id ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += "  and kp.Item=''CA'' and kp.ReportType=''IMPAIREMENT'' ";
           sql += "  group by kp.Item, kp.ReportType' ";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("ty");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 添加人：ydx
       /// 添加时间：2014-11-19
       /// 添加目的：C5 BUSINESS CONTRIBUTION 下的 P/L DISPOSAL 
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <param name="where"></param>
       /// <returns></returns>
       public DataTable getC5BUSINESSCONTRIBUTION_DISPOSAL(string star, string end, string where,string kpiType,string whereS)
       {
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += " select @sql='select  kp.ReportType, '+@sql+' ";
           sql += " from KPISetting kp inner join v_imp_raw imp   ";
           sql += " on imp.AccountCodeD=kp.AccountCode inner join KPITemp kt on kt.Id=kp.Id  ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += "  and kp.Item=''CA'' and kp.ReportType=''P/L DISPOSAL '' ";
           sql += "  group by kp.Item, kp.ReportType' ";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("ty");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 第16类1  FIN / HR / IT / LG  6*  T5:21,23,30
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getFIN_HR_IT_LG(string star, string end,string where,string kpiType,string whereS)
       {

           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += " select @sql=' select kp.ReportType as ''Type'','+@sql+' from KPISetting kp inner join KPITemp kt on kp.Id=kt.Id inner join v_imp_raw imp on imp.T5 =kt.T5 and imp.AccountCodeD =kp.AccountCode  ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql += " and kp.ReportType=''FIN / HR / IT / LG'' group by  kp.ReportType'";
           sql += " EXEC(@sql) ";
          SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("ty16");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 第18类1 TAX
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getTAX(string star, string end,string where,string kpiType,string whereS)
       {
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) ";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += "select @sql=' select kp.ReportType as ''Type'','+@sql+' from KPISetting kp inner join v_imp_raw imp on imp.AccountCodeD=kp.AccountCode ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql+=" and kp.ReportType=''TAX'' group by  kp.ReportType'";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("ty18");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 第20类  FINANCING COST
       /// </summary>
       /// <param name="star"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public DataTable getFINANCINGCOST(string star, string end,string where,string kpiType,string whereS)
       {
           //string sql = @"declare @sql varchar(max)";
           //sql += "select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ";
           //sql += " ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))  ";
           //sql += " from [T_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "' ";
           //sql += " GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)";
           string sql = @"declare @sql varchar(max) ";
           if (kpiType != "")
           {
               sql += @" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9))+''''+' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)) ";

           }
           sql += " from [v_imp_raw] bb where 1=1 and  bb.AccountPeriod >='" + star + "' and  bb.AccountPeriod <='" + end + "'";
           if (whereS != string.Empty)
           {
               whereS = whereS.Replace("imp", "bb");
               whereS = whereS.Replace("''", "'");
               sql += " and " + whereS;
           }
           sql += " GROUP BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9)  ORDER BY SUBSTRING(CONVERT (varchar(10)," + kpiType + "),0,9) ";
           
           sql += "select @sql=' select kp.ReportGroup as ''Type'','+@sql+' from KPISetting kp inner join v_imp_raw imp on imp.AccountCodeD=kp.AccountCode ";
           if (where != string.Empty)
           {
               sql += " and " + where;
           }
           sql+=" and kp.ReportGroup=''FINANCING COST'' group by  kp.ReportGroup'";
           sql += " EXEC(@sql) ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("ty20");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 第21类 Share
       /// 添加人：ydx
       /// 添加时间：2014-12-31
       /// 添加目的：获得合资公司百分比
       /// </summary>
       /// <param name="cocd"></param>
       /// <returns></returns>
       public DataTable getJvShare(string kpiType,string where)
       {
           string sql = "";
           if(kpiType=="CoCd")
           {
               sql=@" declare @sql varchar(max) ";
               sql+=" select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),CoCd),0, ";
               sql += "  9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),CoCd),0,9))+''''+   ' then CONVERT(int,share) else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT  (varchar(10),CoCd),0,9))   ";
                sql+=" from [JVSetting] bb ";
                sql+="  where 1=1 ";
                 if (where != string.Empty)
                 {
                     where = where.Replace("imp","bb");
                     where = where.Replace("''","'");
                     sql += " and " + where;
                 }
                 sql+="  GROUP BY SUBSTRING(CONVERT (varchar(10),CoCd),0,9) order by SUBSTRING(CONVERT (varchar(10),CoCd),0,9) select   @sql='select ''Share'' as  Type , '+@sql+' from [JVSetting] imp ' exec(@sql)";
           }else 
           {
               sql = @" select  SUM(CONVERT(int,share)) from JVSetting imp ";
               if (where != string.Empty)
               {
                   where = where.Replace("''", "'");
                   sql += " where " + where;
               }
           }
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("ty20");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       #endregion
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBHelper;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 添加人：ydx
    /// 添加时间：2014-09-02
    /// 添加目的：费用报告数据导出
    /// </summary>
    public class OpexReportDAL
    {
        SqlHelp sqlHelp = new SqlHelp();
        #region 费用表
        //ydx  2014-06-30 费用表
        public DataTable ImportOPEX()
        {
            string sql = @"declare @sql varchar(max)
select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when [T5]=' +''''+LTRIM([T5])+''''+
' then [Amount] else 0 end) as '+QUOTENAME([T5]) 
from [T_imp_raw_tmp] WHERE 1=1 GROUP BY [T5] order by T5
SELECT @sql='select op.PLLine,op.OpexLine, tp.[AccountCode],op.Account_Description,'+@sql+',sum([Amount]) as [总数] from [T_imp_raw_tmp] tp inner join T_OPEXSetting op on tp.AccountCode=op.AccountCode  group by tp.AccountCode,op.PLLine,op.OpexLine, tp.[AccountCode],op.Account_Description order by op.PLLine asc'
EXEC(@sql)";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("ImportOPEX");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 有时间条件 按部门  2014-07-26  费用明细按部门  2
        /// </summary>
        /// <returns></returns>
        public DataTable ImportOPEX(string star, string end, string where)
        {
            string sql = @" declare @sql varchar(max)
select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when [T5]=' +''''+LTRIM([T5])+''''+
' then [Amount] else 0 end) as '+QUOTENAME([T5]+jd.DeptNamePinyin) 
from [T_imp_raw] bb,JVDSMDC jd WHERE T5<>''and jd.T5Code=bb.T5 and bb.AccountPeriod >='" + star + "' and bb.AccountPeriod<='" + end + "'  GROUP BY [T5],[T5]+jd.DeptNamePinyin order by T5 ";
            sql += " SELECT @sql='select op.PLLine,op.OpexLine, tp.[AccountCodeD],op.Account_Description,'+@sql+',sum([Amount]) as [Total] from [T_imp_raw] tp inner join T_OPEXSetting op on tp.AccountCodeD=op.AccountCode  and tp.AccountPeriod >=''" + star + "'' and tp.AccountPeriod<=''" + end + "'' ";
            if (where != "请选择")
            {
                sql += where;
            }
            sql += "  group by op.PLType, op.PLLine,op.OpexLine,tp.AccountCodeD ,op.Account_Description  order by op.PLType,tp.AccountCodeD'  ";
            //sql+=" group by tp.AccountCodeD,op.PLLine,op.OpexLine, tp.[AccountCodeD],op.Account_Description ' ";
            sql += " EXEC(@sql)";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("ImportOPEX");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 有时间条件 按年份  2014-07-26  费用明细按期间  1
        /// </summary>
        /// <returns></returns>
        public DataTable ImportOPE(string star, string end, string where)
        {
            string sql = @"  declare @sql varchar(max)
select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when [AccountPeriod]=' +''''+LTRIM([AccountPeriod])+''''+
' then [Amount] else 0 end) as '+QUOTENAME([AccountPeriod]) 
from [T_imp_raw] bb WHERE AccountPeriod<>'' and bb.AccountPeriod >='" + star + "' and bb.AccountPeriod<='" + end + "'  GROUP BY [AccountPeriod] order by AccountPeriod ";
            sql += "SELECT @sql='select op.PLLine,op.OpexLine,tp.[AccountCodeD],op.Account_Description,'+@sql+',sum([Amount]) as [Total] from [T_imp_raw] tp inner join T_OPEXSetting op on tp.AccountCodeD=op.AccountCode and tp.AccountPeriod >=''" + star + "'' and tp.AccountPeriod<=''" + end + "''";
            if (where != "请选择")
            {
                sql += where;
            }
            sql += "  group by op.PLType, op.PLLine,op.OpexLine ,tp.AccountCodeD, op.Account_Description order by op.PLType,tp.AccountCodeD  ' ";
            sql += "EXEC(@sql)";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("ImportOPEX");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 费用汇总按期间  4
        /// 添加人：ydx
        /// 添加时间：2014-08-01
        /// 添加目的：
        /// </summary>
        /// <param name="star">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="end">包含cocd和t5、t8的字符串</param>
        /// <returns></returns>
        public DataTable ImportOPETotalByDate(string star, string end, string CocdT5T8String, string Selectfields, string sortFields, string YearMonthQuarter)
        {
            #region ydx sql
            //            string sql = @"  declare @sql varchar(max)
            //select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when [AccountPeriod]=' +''''+LTRIM([AccountPeriod])+''''+
            //' then [Amount] else 0 end) as '+QUOTENAME([AccountPeriod]) 
            //from [T_imp_raw] bb WHERE AccountPeriod<>'' and bb.AccountPeriod >='" + star + "' and bb.AccountPeriod<='" + end + "'  GROUP BY [AccountPeriod] order by AccountPeriod ";
            //            sql += "SELECT @sql='select op.PLLine,op.OpexLine,'+@sql+',sum([Amount]) as [Total] from [T_imp_raw] tp inner join T_OPEXSetting op on tp.AccountCodeD=op.AccountCode and tp.AccountPeriod >=''" + star + "'' and tp.AccountPeriod<=''" + end + "''";
            //             if (where != "请选择")
            //            {
            //                sql += where;
            //            }
            //            sql+=" group by op.PLType,op.PLLine,op.OpexLine order by convert(int,op.PLType) asc' ";
            //            sql += "EXEC(@sql)";         
            //SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            //DataTable dt = new DataTable("ImportOPEX");
            //dt.Load(dr);
            //dr.Close();
            //return dt;
            #endregion
            #region hxy procedure cod可复选
            DataTable dt = null;
          //  SqlDataReader dataReader = null;
            SqlParameter[] parameters = {
                       new SqlParameter("@start", star),
                       new SqlParameter("@end", end),
                       new SqlParameter("@CocdT5T8String",CocdT5T8String ),
                       new SqlParameter("@Selectfields",Selectfields ),
                       new SqlParameter("@sortFields",sortFields ),
                       new SqlParameter("@YearMonthQuarter",YearMonthQuarter )};
            dt = sqlHelp.ExecuteQuery("OpexByPeriod", parameters, CommandType.StoredProcedure);
            return dt;
            //sqlHelp.RunProc("OpexByPeriod", parameters, out dataReader);
            //if (dataReader != null && dataReader.HasRows == true)
            //{
            //    dt = new DataTable();
            //    dt.Load(dataReader);
            //}
            //dataReader.Close();
            //return dt;
            #endregion

        }
        /// <summary>
        /// 费用汇总按部门 5
        /// 添加人：ydx
        /// 添加时间：2014-08-01
        /// 添加目的：
        /// </summary>
        /// <param name="star"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataTable ImportOPETotalByDepart(string star, string end, string CocdT5T8String, string Selectfields, string sortFields, int t5Site)
        {
            #region 以前的
            //            string sql = @" declare @sql varchar(max)
            //select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when [T5]=' +''''+LTRIM([T5])+''''+
            //' then [Amount] else 0 end) as '+QUOTENAME([T5]+jd.DeptNamePinyin) 
            //from [T_imp_raw] bb,JVDSMDC jd WHERE T5<>'' and jd.T5Code=bb.T5 and bb.AccountPeriod >='" + star + "' and bb.AccountPeriod<='" + end + "'  GROUP BY [T5],[T5]+jd.DeptNamePinyin  ";
            //            sql += "SELECT @sql='select op.PLLine,op.OpexLine,'+@sql+',sum([Amount]) as [Total] from [T_imp_raw] tp inner join T_OPEXSetting op on tp.AccountCodeD=op.AccountCode and tp.AccountPeriod >=''" + star + "'' and tp.AccountPeriod<=''" + end + "''";
            //            if (where != "请选择")
            //            {
            //                sql += where;
            //            }
            //            sql += " group by op.PLType,op.PLLine,op.OpexLine order by convert(int,op.PLType),op.OpexLine  asc ' ";
            //            sql += "EXEC(@sql)";
            //            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            //            DataTable dt = new DataTable("ImportOPEX");
            //            dt.Load(dr);
            //            dr.Close();
            //            return dt;
            #endregion


            #region hxy procedure cod可复选
            DataTable dt = null;
          //  SqlDataReader dataReader = null;
            SqlParameter[] parameters = {
                       new SqlParameter("@start", star),
                       new SqlParameter("@end", end),
                       new SqlParameter("@CocdT5T8String",CocdT5T8String ),
                       new SqlParameter("@Selectfields",Selectfields ),
                       new SqlParameter("@sortFields",sortFields ),
                           new SqlParameter("@t5Site",t5Site )};
           dt= sqlHelp.ExecuteQuery("OpexBySite", parameters, CommandType.StoredProcedure);
           return dt;
            //sqlHelp.RunProc("OpexBySite", parameters, out dataReader);
            //if (dataReader != null && dataReader.HasRows == true)
            //{
            //    dt = new DataTable();
            //    dt.Load(dataReader);
            //}
            //dataReader.Close();
            //return dt;
            #endregion
        }
        /// <summary>
        ///部门油站/期间汇总 3
        /// 添加人：ydx
        /// 添加时间：2014-08-01
        /// 添加目的：
        /// </summary>
        /// <param name="star"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataTable ImportOPEThree(string star, string end, string where)
        {
            //此段代码生成的T5不包含重复T5 去唯一的T5代码 影响报告的生成数据 （重复的T5代码只生成一个T5的数据值）
            //            string sql = @"  declare @sql varchar(max)
            //select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when [AccountPeriod]=' +''''+LTRIM([AccountPeriod])+''''+
            //' then [Amount] else 0 end) as '+QUOTENAME([AccountPeriod]) 
            //from [T_imp_raw] bb WHERE AccountPeriod<>'' and bb.AccountPeriod >='" + star + "' and bb.AccountPeriod<='" + end + "'  GROUP BY [AccountPeriod] order by AccountPeriod ";
            //            sql += "SELECT @sql='select tp.T5,jd.DeptNamePinyin,'+@sql+',sum([Amount]) as [Total] from [T_imp_raw] tp inner join T_OPEXSetting op on tp.AccountCodeD=op.AccountCode and tp.AccountPeriod >=''" + star + "'' and tp.AccountPeriod<=''" + end + "''";
            //            sql += " inner join (select distinct CoCd,T5Code, DeptNamePinyin from JVDSMDC ) jd on jd.T5Code=tp.T5 ";
            //            if (where != "请选择")
            //            {
            //                sql += where;
            //            }
            //            sql += " group by tp.T5,jd.DeptNamePinyin order by tp.T5 asc ' ";
            //            sql += "EXEC(@sql)";

            //此段代码生成的T5包含重复T5  影响报告的生成数据（重复的T5代码数据会生成两份）
            string sql = @"  declare @sql varchar(max)
select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when [AccountPeriod]=' +''''+LTRIM([AccountPeriod])+''''+
' then [Amount] else 0 end) as '+QUOTENAME([AccountPeriod]) 
from [T_imp_raw] bb WHERE AccountPeriod<>'' and bb.AccountPeriod >='" + star + "' and bb.AccountPeriod<='" + end + "'  GROUP BY [AccountPeriod] order by AccountPeriod ";
            sql += "SELECT @sql='select tp.T5,jd.DeptNamePinyin,'+@sql+',sum([Amount]) as [Total] from [T_imp_raw] tp inner join T_OPEXSetting op on tp.AccountCodeD=op.AccountCode and tp.AccountPeriod >=''" + star + "'' and tp.AccountPeriod<=''" + end + "''";
            sql += " inner join JVDSMDC jd on jd.T5Code=tp.T5 ";
            if (where != "请选择")
            {
                sql += where;
            }
            sql += " group by tp.T5,jd.DeptNamePinyin order by tp.T5 asc ' ";
            sql += "EXEC(@sql)";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("ImportOPEX");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //        /// <summary>
        //        /// 有时间条件 按部门  2014-07-26  费用明细按部门  2
        //        /// </summary>
        //        /// <returns></returns>
        //        public DataTable ImportOPEX(string star,string end)
        //        {
        //            string sql = @" declare @sql varchar(max)
        //select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when [T5]=' +''''+LTRIM([T5])+''''+
        //' then [Amount] else 0 end) as '+QUOTENAME([T5]+jd.DeptNamePinyin) 
        //from [T_imp_raw] bb,JVDSMDC jd WHERE T5<>''and jd.T5Code=bb.T5 and bb.AccountPeriod >='" + star + "' and bb.AccountPeriod<='" + end + "'  GROUP BY [T5],[T5]+jd.DeptNamePinyin order by T5 ";
        //            sql += "SELECT @sql='select op.PLLine,op.OpexLine, tp.[AccountCodeD],op.Account_Description,'+@sql+',sum([Amount]) as [Total] from [T_imp_raw] tp inner join T_OPEXSetting op on tp.AccountCodeD=op.AccountCode  and tp.AccountPeriod >=''" + star + "'' and tp.AccountPeriod<=''" + end + "'' inner join JV_COASetting  jvc  on jvc.SCLAccountCode=tp.AccountCodeD and jvc.AccountCode=tp.AccountCodeS group by tp.AccountCodeD,op.PLLine,op.OpexLine, tp.[AccountCodeD],op.Account_Description ' ";
        //            sql+="EXEC(@sql)";
        //            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
        //            DataTable dt = new DataTable("ImportOPEX");
        //            dt.Load(dr);
        //            dr.Close();
        //            return dt;
        //        }

        //        /// <summary>
        //        /// 有时间条件 按年份  2014-07-26  费用明细按期间  1
        //        /// </summary>
        //        /// <returns></returns>
        //        public DataTable ImportOPE(string star, string end)
        //        {
        //            string sql = @"  declare @sql varchar(max)
        //select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when [AccountPeriod]=' +''''+LTRIM([AccountPeriod])+''''+
        //' then [Amount] else 0 end) as '+QUOTENAME([AccountPeriod]) 
        //from [T_imp_raw] bb WHERE AccountPeriod<>'' and bb.AccountPeriod >='" + star + "' and bb.AccountPeriod<='" + end + "'  GROUP BY [AccountPeriod] order by AccountPeriod ";
        //            sql += "SELECT @sql='select op.PLLine,op.OpexLine,tp.[AccountCodeD],op.Account_Description,'+@sql+',sum([Amount]) as [Total] from [T_imp_raw] tp inner join T_OPEXSetting op on tp.AccountCodeD=op.AccountCode and tp.AccountPeriod >=''" + star + "'' and tp.AccountPeriod<=''" + end + "''  inner join JV_COASetting  jvc  on jvc.SCLAccountCode=tp.AccountCodeD and jvc.AccountCode=tp.AccountCodeS group by tp.AccountCodeD,op.PLLine,op.OpexLine, tp.[AccountCodeD],op.Account_Description ' ";
        //            sql += "EXEC(@sql)";
        //            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
        //            DataTable dt = new DataTable("ImportOPEX");
        //            dt.Load(dr);
        //            dr.Close();
        //            return dt;
        //        }
        //        /// <summary>
        //        /// 费用汇总按期间  4
        //        /// 添加人：ydx
        //        /// 添加时间：2014-08-01
        //        /// 添加目的：
        //        /// </summary>
        //        /// <param name="star"></param>
        //        /// <param name="end"></param>
        //        /// <returns></returns>
        //        public DataTable ImportOPETotalByDate(string star, string end)
        //        {
        //            string sql = @"  declare @sql varchar(max)
        //select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when [AccountPeriod]=' +''''+LTRIM([AccountPeriod])+''''+
        //' then [Amount] else 0 end) as '+QUOTENAME([AccountPeriod]) 
        //from [T_imp_raw] bb WHERE AccountPeriod<>'' and bb.AccountPeriod >='" + star + "' and bb.AccountPeriod<='" + end + "'  GROUP BY [AccountPeriod] order by AccountPeriod ";
        //            sql += "SELECT @sql='select op.PLLine,op.OpexLine,'+@sql+',sum([Amount]) as [Total] from [T_imp_raw] tp inner join T_OPEXSetting op on tp.AccountCodeD=op.AccountCode and tp.AccountPeriod >=''" + star + "'' and tp.AccountPeriod<=''" + end + "'' inner join JV_COASetting  jvc  on jvc.SCLAccountCode=tp.AccountCodeD and jvc.AccountCode=tp.AccountCodeS  group by op.PLType,op.PLLine,op.OpexLine order by convert(int,op.PLType) asc' ";
        //            sql += "EXEC(@sql)";
        //            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
        //            DataTable dt = new DataTable("ImportOPEX");
        //            dt.Load(dr);
        //            dr.Close();
        //            return dt;
        //        }

        //        /// <summary>
        //        /// 费用汇总按部门 5
        //        /// 添加人：ydx
        //        /// 添加时间：2014-08-01
        //        /// 添加目的：
        //        /// </summary>
        //        /// <param name="star"></param>
        //        /// <param name="end"></param>
        //        /// <returns></returns>
        //        public DataTable ImportOPETotalByDepart(string star, string end)
        //        {
        //            string sql = @" declare @sql varchar(max)
        //select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when [T5]=' +''''+LTRIM([T5])+''''+
        //' then [Amount] else 0 end) as '+QUOTENAME([T5]+jd.DeptNamePinyin) 
        //from [T_imp_raw] bb,JVDSMDC jd WHERE T5<>'' and jd.T5Code=bb.T5 and bb.AccountPeriod >='" + star + "' and bb.AccountPeriod<='" + end + "'  GROUP BY [T5],[T5]+jd.DeptNamePinyin order by T5 ";
        //            sql += "SELECT @sql='select op.PLLine,op.OpexLine,'+@sql+',sum([Amount]) as [Total] from [T_imp_raw] tp inner join T_OPEXSetting op on tp.AccountCodeD=op.AccountCode and tp.AccountPeriod >=''" + star + "'' and tp.AccountPeriod<=''" + end + "'' inner join JV_COASetting  jvc  on jvc.SCLAccountCode=tp.AccountCodeD and jvc.AccountCode=tp.AccountCodeS group by op.PLType,op.PLLine,op.OpexLine order by convert(int,op.PLType)  asc ' ";
        //            sql += "EXEC(@sql)";
        //            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
        //            DataTable dt = new DataTable("ImportOPEX");
        //            dt.Load(dr);
        //            dr.Close();
        //            return dt;
        //        }

        /// <summary>
        ///部门油站/期间汇总 3
        /// 添加人：ydx
        /// 添加时间：2014-08-01
        /// 添加目的：
        /// </summary>
        /// <param name="star"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        //        public DataTable ImportOPEThree(string star, string end)
        //        {
        //            string sql = @"  declare @sql varchar(max)
        //        select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when [AccountPeriod]=' +''''+LTRIM([AccountPeriod])+''''+
        //        ' then [Amount] else 0 end) as '+QUOTENAME([AccountPeriod]) 
        //        from [T_imp_raw] bb WHERE AccountPeriod<>'' and bb.AccountPeriod >='" + star + "' and bb.AccountPeriod<='" + end + "'  GROUP BY [AccountPeriod] order by AccountPeriod ";
        //            sql += "SELECT @sql='select tp.T5,jd.DeptNamePinyin,'+@sql+',sum([Amount]) as [Total] from [T_imp_raw] tp inner join T_OPEXSetting op on tp.AccountCodeD=op.AccountCode and tp.AccountPeriod >=''" + star + "'' and tp.AccountPeriod<=''" + end + "'' inner join JV_COASetting  jvc  on jvc.SCLAccountCode=tp.AccountCodeD and jvc.AccountCode=tp.AccountCodeS inner join JVDSMDC jd on jd.T5Code=tp.T5 group by tp.T5,jd.DeptNamePinyin order by tp.T5 asc ' ";
        //            sql += "EXEC(@sql)";
        //            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
        //            DataTable dt = new DataTable("ImportOPEX");
        //            dt.Load(dr);
        //            dr.Close();
        //            return dt;
        //        }
        #endregion
    }
}

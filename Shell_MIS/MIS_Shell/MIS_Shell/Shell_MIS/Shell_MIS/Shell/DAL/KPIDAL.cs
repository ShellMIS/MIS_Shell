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
    /// 添加时间：2014-07-28
    /// 添加目的：KPI数据字典管理
    /// </summary>
   public class KPIDAL
    {

       
       /// <summary>
       /// T2
       /// </summary>
       /// <returns></returns>
       public DataTable T2Select()
       {
           string sql = @"select T2 from T_imp_raw_tmp group by T2 having T2<>'' order by T2";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable table = new DataTable("opexSetting");
           table.Load(dr);
           dr.Close();
           return table;
       }
       /// <summary>
       /// T5
       /// </summary>
       /// <returns></returns>
       public DataTable T5Select()
       {
           string sql = @" select T5 from T_imp_raw_tmp group by T5 having T5<>'' order by T5";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable table = new DataTable("opexSetting");
           table.Load(dr);
           dr.Close();
           return table;
       }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="plType"></param>
        /// <param name="plLine"></param>
        /// <param name="opexLine"></param>
        /// <param name="budgetOwner"></param>
        /// <param name="accountCode"></param>
        /// <param name="account_Description"></param>
        /// <param name="createdBy"></param>
        /// <param name="createdDate"></param>
        /// <param name="modifyBy"></param>
        /// <param name="modifyDate"></param>
        /// <returns></returns>
       public int KPISettingInsert(string Item, string ItemDescription, string Code, string CodeDescription, string ReportGroup, string ReportType, string ReportSubType, string AccountCode,string T2, string T5)
        {
            string sql = "insert into KPISetting values('" + Item + "','" + ItemDescription + "','" + Code + "','" + CodeDescription + "','" + ReportGroup + "','" + ReportType + "','" + ReportSubType + "','" + AccountCode + "','" + T2 + "','" + T5 + "')select @@IDENTITY";
            return SqlHelp.sqlexecutereader(sql);
        }
       /// <summary>
       /// 添加人：ydx
       /// 添加时间：2014-08-07
       /// 添加目的：向kpi临时表中添加数据
       /// </summary>
       /// <param name="id"></param>
       /// <param name="t2"></param>
       /// <param name="t5"></param>
       /// <returns></returns>
       public int kpiTempInsert(string id,string t2,string t5)
       {
           string sql = "insert into KPITemp values('"+id+"','"+t2+"','"+t5+"')";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           dr.Close();
           return 1;
       }
       /// <summary>
       /// 添加人：ydx
       /// 添加时间：2014-08-07
       /// 添加目的：kpi临时表删除 与kpi表中相关的数据
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int deleteKPItemp(string id)
       {
           string sql = "delete from KPITemp where Id='" + id + "'";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           dr.Close();
           return 1;
       }
        /// <summary>
        /// 获取所有数据 T_OPEXSetting表中的
        /// </summary>
        /// <returns></returns>
       public DataTable KPISettingSelect()
        {
            string sql = @" select * from KPISetting order by Id";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable table = new DataTable("KPISetting");
            table.Load(dr);
            dr.Close();
            return table;
        }
        /// <summary>
        /// 根据编号查出一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public DataTable KPISettingSelect(int id)
        {
            string sql = @" select * from KPISetting where ID=" + id + " ";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("KPISetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 根据不同条件查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="plType"></param>
        /// <param name="plLine"></param>
        /// <param name="opexLine"></param>
        /// <param name="budgetOwner"></param>
        /// <param name="accountCode"></param>
        /// <param name="account_Description"></param>
        /// <param name="createdBy"></param>
        /// <param name="createdDate"></param>
        /// <param name="modifyBy"></param>
        /// <param name="modifyDate"></param>
        /// <returns></returns>
       public DataTable KPISettingSelect(string Item, string ItemDescription, string Code, string CodeDescription, string ReportGroup, string ReportType, string ReportSubType, string AccountCode,string T2, string T5)
       {
            string sql = @" select * from KPISetting where 1=1 ";

            if (!string.IsNullOrEmpty(Item))
            {
                sql += " and Item like'" + Item + "'";
            }
            if (!string.IsNullOrEmpty(ItemDescription))
            {
                sql += " and ItemDescription like'%" + ItemDescription + "%'";
            }
            if (!string.IsNullOrEmpty(Code))
            {
                sql += " and Code like'%" + Code + "%'";
            }
            if (!string.IsNullOrEmpty(CodeDescription))
            {
                sql += " and CodeDescription like'%" + CodeDescription + "%'";
            }
            if (!string.IsNullOrEmpty(ReportGroup))
            {
                sql += " and ReportGroup like'%" + ReportGroup + "%'";
            }
            if (!string.IsNullOrEmpty(ReportType))
            {
                sql += " and ReportType like'%" + ReportType + "%'";
            }

            if (!string.IsNullOrEmpty(ReportSubType))
            {
                sql += " and ReportSubType like'" + ReportSubType + "'";
            }
            if (!string.IsNullOrEmpty(AccountCode))
            {
                sql += " and AccountCode like'%" + AccountCode + "%'";
            }
            if (!string.IsNullOrEmpty(T2))
            {
                sql += " and T2 like'%" + T2 + "%'";
            }
            if (!string.IsNullOrEmpty(T5))
            {
                sql += " and T5 like'%" + T5 + "%'";
            }
            sql+=" order by Id";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("KPISetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="plType"></param>
        /// <param name="plLine"></param>
        /// <param name="opexLine"></param>
        /// <param name="budgetOwner"></param>
        /// <param name="accountCode"></param>
        /// <param name="account_Description"></param>
        /// <param name="createdBy"></param>
        /// <param name="createdDate"></param>
        /// <param name="modifyBy"></param>
        /// <param name="modifyDate"></param>
        /// <returns></returns>
       public int updateKPISetting(int id,string Item, string ItemDescription, string Code, string CodeDescription, string ReportGroup, string ReportType, string ReportSubType, string AccountCode,string T2, string T5)
       {
           string sql = "update KPISetting set Item='" + Item + "',ItemDescription='" + ItemDescription + "',Code='" + Code + "',CodeDescription='" + CodeDescription + "' ,ReportGroup='" + ReportGroup + "' ,ReportType='" + ReportType + "' ,ReportSubType='" + ReportSubType + "',AccountCode='" + AccountCode + "',T2='" + T2 + "',T5='" + T5 + "' where ID=" + id + "";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public int deleteKPISetting(int id)
        {
            string sql = "delete from KPISetting where Id=" + id + "";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }

       
    }
}

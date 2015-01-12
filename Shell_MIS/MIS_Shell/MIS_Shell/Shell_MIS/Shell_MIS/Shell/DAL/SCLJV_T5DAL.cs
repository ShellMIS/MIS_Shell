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
    /// 添加时间：2014-11-20
    /// 添加目的：合资公司与标准T5Code对照
    /// </summary>
   public class SCLJV_T5DAL
    {
       /// <summary>
       /// 删除部门临时表
       /// </summary>
       /// <returns></returns>
       public int ImportDelete()
       {
           string sql = "truncate table [JVDSMDC_Temp]";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           dr.Close();
           return 1;
       }
       /// <summary>
        ///JVDSMDC表 导入验证 查出JV部分
       /// </summary>
       /// <param name="cocd"></param>
       /// <returns></returns>
       public DataTable JVDSMDC_Temp(string cocd)
       {
           string sql = @"  select jv.id,scl.DeptNameCH as DeptNameCH,scl.DeptNamePinYin as DeptNamePinyin,jv.Nature,jv.HSC,jv.CoCd,jv.T0Code,jv.T3Code,scl.SCL_T5 as T5Code,jv.SiteOpenDate, jv.SiteAging,jv.SiteStatus,jv.TMCode,jv.Acquired,jv.Location,jv.CRType,jv.InvestmentType,jv.District from JVDSMDC_Temp jv  inner join T_SCLJV_T5 scljv on jv.T5Code=scljv.JV_T5 and jv.CoCd=scljv.CoCd  inner join T_SCLT5 scl on scl.SCL_T5=scljv.SCL_T5 and scljv.CoCd='" + cocd + "' and jv.Nature='JV'";
          
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable table = new DataTable("jvS");
           table.Load(dr);
           dr.Close();
           return table;
       }
       /// <summary>
       /// JVDSMDC里natur不为jv的
       /// </summary>
       /// <param name="cocd"></param>
       /// <returns></returns>
       public DataTable JVDSMDC_TempAll(string cocd)
       {
           string sql = @" select * from  JVDSMDC_Temp  where CoCd='" + cocd + "' and Nature='Site'";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable table = new DataTable("jvSite");
           table.Load(dr);
           dr.Close();
           return table;
       }
        /// <summary>
        /// 添加人:ydx
        /// 添加时间：2014-11-20
        /// 添加目的：（数据包导入验证）查看合资公司是否已有T5code
        /// </summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        /// 
        public DataTable ifExists(string cocd)
        {
            string sql = @"  select distinct * from JVDSMDC_Temp where Nature='JV' and  T5Code not in(select JV_T5 from T_SCLJV_T5 where CoCd='" + cocd + "') and CoCd='" + cocd + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable table = new DataTable("jvd");
            table.Load(dr);
            dr.Close();
            return table;
        }

       /// <summary>
        /// 添加，修改时 判断T_SCLJV_T5表里的T5是否存在
       /// </summary>
       /// <param name="cocd"></param>
       /// <param name="JVt5"></param>
       /// <returns></returns>
        public DataTable ifExists(string cocd,string JVt5)
        {
            string sql = @" select * from [T_SCLJV_T5] where CoCd='"+cocd+"' and JV_T5='"+JVt5+"'  ";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable table = new DataTable("jd");
            table.Load(dr);
            dr.Close();
            return table;
        }
      
       /// <summary>
       /// 先查出数据
       /// </summary>
       /// <param name="cocd"></param>
       /// <returns></returns>
        public DataTable FieldSelect(int id,string cocd)
        {
            string sql = @" select scljv.SCL_T5,scl.DeptNameCH,scl.DeptNamePinYin from T_SCLJV_T5 scljv inner join T_SCLT5 scl  on scljv.SCL_T5=scl.SCL_T5 and scljv.JV_T5 =(select T5Code from JVDSMDC where id='"+id+"') and CoCd='"+cocd+"'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable table = new DataTable("jvd");
            table.Load(dr);
            dr.Close();
            return table;
        }
        /// <summary>
        /// 添加目的：替换部门下的T5code
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int updateDepT5code(int deid, string sclT5, string depCh, string depNaPinyin)
        {
            string sql = @" update JVDSMDC  set T5Code='" + sclT5 + "',DeptNameCH='" + depCh + "',DeptNamePinyin='" + depNaPinyin + "'  where id='" + deid + "'  ";
            return SqlHelp.sqlexecutereader(sql);
        }




       // /// <summary>
       // /// 查出所有的 SCLT5和JVT5
       // /// </summary>
       // /// <param name="cocd"></param>
       // /// <returns></returns>
       // public DataTable FindSCL_JV_T5(string cocd)
       // {
       //     string sql = @"  select scl.SCL_T5,scl.DeptNameCH,scl.DeptNamePinYin,jv.JV_T5 from T_SCLJV_T5 jv inner join T_SCLT5 scl on scl.SCL_T5=jv.SCL_T5 and jv.CoCd='"+cocd+"'";
       //     SqlDataReader dr = SqlHelp.ExecuteReader(sql);
       //     DataTable table = new DataTable("scjv");
       //     table.Load(dr);
       //     dr.Close();
       //     return table;
       // }
       ///// <summary>
       ///// 在JVDSMDC 导入的时候替换掉T5
       ///// </summary>
       ///// <param name="sclT5"></param>
       ///// <param name="depCh"></param>
       ///// <param name="depNaPinyin"></param>
       ///// <param name="jvT5"></param>
       ///// <returns></returns>
       // public int updateDep_TempT5code(string sclT5, string depCh, string depNaPinyin,string jvT5,string cocd)
       // {
       //     string sql = @" update JVDSMDC_Temp  set T5Code='" + sclT5 + "',DeptNameCH='" + depCh + "', DeptNamePinyin='" + depNaPinyin + "'  where T5Code='" + jvT5 + "' and CoCd='"+cocd+"' ";
       //     return SqlHelp.sqlexecutereader(sql);
       // }


    }
}

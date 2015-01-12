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
   public class T5SiteDAL
    {

       public SqlHelp sqlhelper = new SqlHelp();
        /// <summary>
        /// 插入T5Site
        /// </summary>
        /// <param name="CoCd"></param>
        /// <param name="CoNameCH"></param>
        /// <param name="CoNameEN"></param>
        /// <param name="CoGroup1"></param>
        /// <returns></returns>
        public int T5Site_Insert(string t5old, string t5new, string cocd, string accountPerid)
        {
            int result = 0;
            string strSql = "insert into JVDSMDC";
            strSql += " values (";
            strSql += "@T5Old,@T5New,@Cocd,@AccountPerid)";
            SqlParameter[] parameters = {
             new SqlParameter("@T5Old",t5old),
             new SqlParameter("@T5New",t5new),
             new SqlParameter("@Cocd",cocd),
             new SqlParameter("@AccountPerid",accountPerid)
           };
            strSql += " select @@identity;";
            object c = SqlHelp.ExecuteScalar(strSql, parameters);
            result = int.Parse(c.ToString().Trim());
            return result;
        }
        /// <summary>
        /// 查询所有数据库里的内容
        /// </summary>
        /// <returns></returns>
        public DataTable T5SiteSelect(string cocd)
        {
            string sql = @"select t5.CoCd,t5.T5_OldSite,t5.T5_NewSite from T5_Site t5 where t5.CoCd='"+cocd+"' order by t5.CoCd,t5.T5_OldSite,t5.T5_NewSite ";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("t5");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBHelper;

namespace DAL
{
   public class JVT5SettingDAL
    {
       public DataTable SelectCocd(string StrWhere)
       {
           string sql = "select (CoCd+' '+CoNameEN)as cb,* from JVSetting where 1=1 ";
           if (StrWhere != "")
           {
               sql += StrWhere;
           }

           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("JVT5Setting");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       public DataTable JVT5Select(string StrWhere)
       {
           string sql = "select * from T_SCLJV_T5 where 1=1 ";
           if (StrWhere != "")
           {
               sql += StrWhere;
           }

           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("JVT5Setting");
           dt.Load(dr);
           dr.Close();
           return dt;
       }

       public int Insert(string SCL_T5, string JV_T5, string CoCd)
       {
           string sql = "insert into T_SCLJV_T5 values('" + SCL_T5 + "','" + JV_T5 + "','" + CoCd + "')";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           dr.Close();
           return 1;
       }

       public int Update(int ID, string SCL_T5, string JV_T5, string CoCd)
       {
           string sql = @"UPDATE T_SCLJV_T5 SET SCL_T5 = '" + SCL_T5 + "',JV_T5 = '" + JV_T5 + "' ,CoCd = '" + CoCd + "'";
           sql += " where ID='" + ID + "'";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           dr.Close();
           return 1;
       }

       public int delete(string ID)
       {
           string sql = @"delete from  T_SCLJV_T5 ";
           sql += " where ID=" + ID + "";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           dr.Close();
           return 1;
       }
    }
}

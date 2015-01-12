using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBHelper;
namespace DAL
{
    public class T5SettingDAL
    {

        //查询
        public DataTable T5Select(string StrWhere)
        {
            string sql = "select * from T_SCLT5 where 1=1 ";
            if (StrWhere != "")
            {
                sql += StrWhere;
            }

            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T5Setting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public int Insert(string SCL_T5, string DeptNameCH, string DeptNamePinYin)
        {
            string sql = "insert into T_SCLT5 values('" + SCL_T5 + "','" + DeptNameCH + "','" + DeptNamePinYin + "')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }

        public int Update(int ID, string SCL_T5, string DeptNameCH, string DeptNamePinYin)
        {
            string sql = @"UPDATE T_SCLT5 SET SCL_T5 = '" + SCL_T5 + "',DeptNameCH = '" + DeptNameCH + "' ,DeptNamePinYin = '" + DeptNamePinYin + "'";
            sql += " where ID='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }

        public int delete(string ID)
        {
            string sql = @"delete from  T_SCLT5 ";
            sql += " where ID=" + ID + "";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
       
    }
}

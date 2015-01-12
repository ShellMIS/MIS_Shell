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
   public class PrivilegeDAL
    {
        /// <summary>
        /// 获取所有数据 T_CF_ Privilege表
        /// </summary>
        /// <returns></returns>
        public DataTable PrivilegeSelect()
        {
            string sql = @"  select PrivilegeID,[Description] from [T_CF_ Privilege] order by Description ";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable table = new DataTable("privilege");
            table.Load(dr);
            dr.Close();
            return table;
        }
    }
}

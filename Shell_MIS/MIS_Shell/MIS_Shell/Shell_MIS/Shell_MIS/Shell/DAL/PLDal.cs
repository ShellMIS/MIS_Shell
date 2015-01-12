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
    public class PLDal
    {
        /// <summary>
        /// 在PLSetting中插入内容。有可能会插入创建人、时间、修改人、时间
        /// </summary>
        public int PLInsert(string AccGroup, string AccSubGroup, string AccType, string AccSubType, string AccountCode, string Account_Description)
        {
            string sql = @"INSERT INTO [T_PLSetting] ([AccGroup],[AccSubGroup],[AccType],[AccSubType],[AccountCode],[Account_Description])
     VALUES ('" + AccGroup + "','" + AccSubGroup + "','" + AccType + "','" + AccSubType + "','" + AccountCode + "','" + Account_Description + "')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 根据ID删除
        /// </summary>
        public int PLDelete(string id)
        {
            string sql = "delete from [T_PLSetting] where ID ='" + id + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 查询
        /// </summary>
        public DataTable PLSelect()
        {
            string sql = @"select * from [T_PLSetting]";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_PLSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable PLSelect(string AccGroup, string AccSubGroup, string AccType, string AccSubType, string AccountCode, string Account_Description)
        {
            string sql = @"select * from [T_PLSetting] where 1=1 ";
            if (!string.IsNullOrEmpty(AccGroup))
            {
                sql += "and AccGroup like '%" + AccGroup + "%'";
            }
            if (!string.IsNullOrEmpty(AccSubGroup))
            {
                sql += "and AccSubGroup like '%" + AccSubGroup + "%'";
            }
            if (!string.IsNullOrEmpty(AccType))
            {
                sql += "and AccType like '%" + AccType + "%'";
            }
            if (!string.IsNullOrEmpty(AccSubType))
            {
                sql += "and AccSubType like '%" + AccSubType + "%'";
            }
            if (!string.IsNullOrEmpty(AccountCode))
            {
                sql += "and AccountCode like '%" + AccountCode + "%'";
            }
            if (!string.IsNullOrEmpty(Account_Description))
            {
                sql += "and Account_Description like '%" + Account_Description + "%'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_PLSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable PLSelect(int id)
        {
            string sql = @"select * from [T_PLSetting] where id='" + id + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_PLSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public int PLUpdate(int id, string AccGroup, string AccSubGroup, string AccType, string AccSubType, string AccountCode, string Account_Description)
        {
            string sql = @"UPDATE [T_PLSetting] SET AccGroup = '" + AccGroup + "',AccSubGroup = '" + AccSubGroup + "' ,AccType = '" + AccType + "' ,AccSubType = '" + AccSubType
                    + "',AccountCode='" + AccountCode + "',Account_Description='" + Account_Description + "'  WHERE  ID='" + id + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
    }
}

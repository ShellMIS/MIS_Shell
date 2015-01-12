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
    public class BSDal
    {
        /// <summary>
        /// 在BSSetting中插入内容。有可能会插入创建人、时间、修改人、时间
        /// </summary>
        public int BSInsert(string ReportType, string AccGroup, string AccSubGroup, string AccType, string AccSubType, string AccountCode, string Account_Description,int sortField)
        {
            string sql = @"INSERT INTO [T_BSSetting] ([ReportType],[AccGroup],[AccSubGroup],[AccType],[AccSubType],[AccountCode],[Account_Description],[orderby])
     VALUES ('" + ReportType + "','" + AccGroup + "','" + AccSubGroup + "','" + AccType + "','" + AccSubType + "','" + AccountCode + "','" + Account_Description + "'," + sortField + ")";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 根据ID删除
        /// </summary>
        public int BSDelete(string id)
        {
            string sql = "delete from [T_BSSetting] where ID in ('" + id + "')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 查询
        /// </summary>
        public DataTable BSSelect()
        {
            string sql = @"select * from [T_BSSetting]";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_BSSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable BSSelect(string ReportType, string AccGroup, string AccSubGroup, string AccType, string AccSubType, string AccountCode, string Account_Description, int sortField)
        {
            string sql = @"select * from [T_BSSetting] where 1=1 ";
            if (!string.IsNullOrEmpty(ReportType))
            {
                sql += "and ReportType like '%" + ReportType + "%'";
            }
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

            if (!string.IsNullOrEmpty(sortField.ToString()))
            {
                sql += " and orderby like '%" + sortField + "%'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_BSSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable BSSelect(int id)
        {
            string sql = @"select * from [T_BSSetting] where id='"+id+"'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_BSSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 修改
        /// </summary>
        public int BSUpdate(int id,string ReportType, string AccGroup, string AccSubGroup, string AccType, string AccSubType, string AccountCode, string Account_Description,int sortFiel)
        {
            string sql = @"UPDATE [T_BSSetting] SET ReportType = '" + ReportType + "',AccGroup = '" + AccGroup + "' ,AccSubGroup = '" + AccSubGroup + "' ,AccType = '" + AccType
                    + "',AccSubType='" + AccSubType + "',AccountCode='" + AccountCode + "',Account_Description='" + Account_Description + "', orderby="+sortFiel+"  WHERE  ID='" + id + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }

    }
}

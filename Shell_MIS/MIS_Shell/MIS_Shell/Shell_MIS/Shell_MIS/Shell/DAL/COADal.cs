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
    public class COADal
    {
        /// <summary>
        /// 插入CoaSetting
        /// </summary>
        /// <param name="CoCd"></param>
        /// <param name="CoNameCH"></param>
        /// <param name="CoNameEN"></param>
        /// <param name="CoGroup1"></param>
        /// <returns></returns>
        public int COAInsert(string AccountType, string Account, string Account_Description, string Status, string Update)
        {
            string sql = "insert into COASetting values('" + AccountType + "','" + Account + "','" + Account_Description + "','" + Status + "','" + Update + "')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 查询所有数据库里的内容
        /// </summary>
        /// <returns></returns>
        public DataTable COASelect()
        {
            string sql = "select * from COASetting";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("COASetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable COASelectcb()
        {
            string sql = "select (Account+' '+Account_Description)as cb,Account from COASetting";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("COASetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable COASelect(string AccountType, string Account, string Account_Description, string Status, string Update)
        {
            string sql = @"select * from COASetting where 1=1 ";
            if (!string.IsNullOrEmpty(AccountType))
            {
                sql += "and AccountType like '%" + AccountType + "%'";
            }
            if (!string.IsNullOrEmpty(Account))
            {
                sql += "and Account like '%" + Account + "%'";
            }
            if (!string.IsNullOrEmpty(Account_Description))
            {
                sql += "and Account_Description like '%" + Account_Description + "%'";
            }
            if (!string.IsNullOrEmpty(Status))
            {
                sql += "and Status like '%" + Status + "%'";
            }
            if (!string.IsNullOrEmpty(Update))
            {
                sql += "and Update like '%" + Update + "%'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("COASetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable COASelect(int ID)
        {
            string sql = "select * from COASetting where id='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("COASetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 根据ID做删除
        /// hxy 修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int COADelete(string id)
        {
            string sql = "delete from COASetting where ID in('" + id + "')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }

        public int COAUpdate(int ID, string AccountType, string Account, string Account_Description, string Status, string Update)
        {
            string sql = @"UPDATE COASetting SET AccountType = '" + AccountType + "',Account = '" + Account + "' ,Account_Description = '" + Account_Description + "' ,Status = '" + Status
                    + "',[Update]='" + Update + "'  WHERE  ID='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
    }
}

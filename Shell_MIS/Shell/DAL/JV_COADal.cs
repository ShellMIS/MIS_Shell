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
    public class JV_COADal
    {
        /// <summary>
        /// 插入JV_COASetting
        /// </summary>
        /// <param name="CoCd"></param>
        /// <param name="CoNameCH"></param>
        /// <param name="CoNameEN"></param>
        /// <param name="CoGroup1"></param>
        /// <returns></returns>
        public int JV_COAInsert(string AccountCode, string Account_Description, string SCLAccountCode, string CoCd)
        {
            string sql = "insert into JV_COASetting values('" + AccountCode + "','" + Account_Description + "','" + SCLAccountCode + "','" + CoCd + "')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-08-15
        /// 修改目的：查询出的数据有问题
        /// 查询所有数据库里的内容
        /// </summary>
        /// <returns></returns>
        public DataTable JV_COASelect()
        {
           // string sql = @"select a.AccountCode,a.Account_Description,a.SCLAccountCode,b.Account_Description,a.CoCd from
 //JV_COASetting a left join COASetting b on a.SCLAccountCode=b.Account";
            string sql = @"select a.id, a.SCLAccountCode as 'SCL_AccountCode',b.Account_Description as 'SCL_Account_Description', a.AccountCode as 'JV_AccountCode',a.Account_Description as 'JV_Account_Description',a.CoCd 
from JV_COASetting a 
left join COASetting b 
on a.SCLAccountCode=b.Account";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("JV_COASetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-08-15
        /// 修改目的：重复插入判断有问题
        /// </summary>
        /// <param name="AccountCode"></param>
        /// <param name="Account_Description"></param>
        /// <param name="SCLAccountCode"></param>
        /// <param name="CoCd"></param>
        /// <returns></returns>
        public DataTable JV_COASelects(string AccountCode, string Account_Description, string SCLAccountCode, string CoCd)
        {
            string sql = @"select a.id, a.SCLAccountCode as 'SCL_AccountCode',b.Account_Description as 'SCL_Account_Description', a.AccountCode as 'JV_AccountCode',a.Account_Description as 'JV_Account_Description',a.CoCd from JV_COASetting a inner join COASetting b on a.SCLAccountCode=b.Account ";
            if (!string.IsNullOrEmpty(AccountCode))
            {
                sql += "and a.AccountCode = '" + AccountCode + "'";
            }
            if (!string.IsNullOrEmpty(Account_Description))
            {
                sql += "and a.Account_Description = '" + Account_Description + "'";
            }
            if (!string.IsNullOrEmpty(SCLAccountCode))
            {
                sql += "and a.SCLAccountCode = '" + SCLAccountCode + "'";
            }
            if (!string.IsNullOrEmpty(CoCd))
            {
                sql += "and a.CoCd = '" + CoCd + "'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("JV_COASetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-08-15
        /// 修改目的：重复插入判断有问题
        /// </summary>
        /// <param name="AccountCode"></param>
        /// <param name="Account_Description"></param>
        /// <param name="SCLAccountCode"></param>
        /// <param name="CoCd"></param>
        /// <returns></returns>
        public DataTable JV_COASelect(string AccountCode, string Account_Description, string SCLAccountCode, string CoCd)
        {
            string sql = @"select a.id, a.SCLAccountCode as 'SCL_AccountCode',b.Account_Description as 'SCL_Account_Description', a.AccountCode as 'JV_AccountCode',a.Account_Description as 'JV_Account_Description',a.CoCd from JV_COASetting a inner join COASetting b on a.SCLAccountCode=b.Account ";
            if (!string.IsNullOrEmpty(AccountCode))
            {
                sql += "and a.AccountCode like '%" + AccountCode + "%'";
            }
            if (!string.IsNullOrEmpty(Account_Description))
            {
                sql += "and a.Account_Description like '%" + Account_Description + "%'";
            }
            if (!string.IsNullOrEmpty(SCLAccountCode))
            {
                sql += "and a.SCLAccountCode like '%" + SCLAccountCode + "%'";
            }
            if (!string.IsNullOrEmpty(CoCd))
            {
                sql += "and a.CoCd like '%" + CoCd + "%'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("JV_COASetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-08-15
        /// 修改目的：根据编号查出一条数据报错
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataTable JV_COASelect(int ID)
        {
            string sql = "select * from JV_COASetting where Id='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("JV_COASetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 根据ID做删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int JV_COADelete(string id)
        {
            string sql = "delete from JV_COASetting where ID ='" + id + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }

        public int JV_COAUpdate(int ID, string AccountCode, string Account_Description, string SCLAccountCode, string CoCd)
        {
            string sql = @"UPDATE JV_COASetting SET AccountCode = '" + AccountCode + "' ,Account_Description = '" + Account_Description + "', SCLAccountCode='" + SCLAccountCode
                + "', CoCd='" + CoCd + "' WHERE  ID='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
    }
}

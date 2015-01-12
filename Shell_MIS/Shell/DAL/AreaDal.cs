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
    public class AreaDal
    {
        /// <summary>
        /// 插入AreaSetting
        /// </summary>
        /// <param name="CoCd"></param>
        /// <param name="CoNameCH"></param>
        /// <param name="CoNameEN"></param>
        /// <param name="CoGroup1"></param>
        /// <returns></returns>
        public int AreaInsert(string AreaCodeT0, string AreaNameCH, string AreaNameEN, string CoCd)
        {
            string sql = "insert into AreaSetting values('" + AreaCodeT0 + "','" + AreaNameCH + "','" + AreaNameEN + "','" + CoCd + "')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 查询所有数据库里的内容
        /// </summary>
        /// <returns></returns>
        public DataTable AreaSelect()
        {
            string sql = "select * from AreaSetting";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("AreaSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="AreaCodeT0"></param>
        /// <param name="AreaNameCH"></param>
        /// <param name="AreaNameEN"></param>
        /// <param name="CoCd"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable AreaSelect(string AreaCodeT0, string AreaNameCH, string AreaNameEN, string CoCd, string id)
        {
            string sql = @"select * from AreaSetting where  ID<>'" + id + "' ";
            if (!string.IsNullOrEmpty(AreaCodeT0))
            {
                sql += "and AreaCodeT0 like '%" + AreaCodeT0 + "%'";
            }
            if (!string.IsNullOrEmpty(AreaNameCH))
            {
                sql += "and AreaNameCH like '%" + AreaNameCH + "%'";
            }
            if (!string.IsNullOrEmpty(AreaNameEN))
            {
                sql += "and AreaNameEN like '%" + AreaNameEN + "%'";
            }
            if (!string.IsNullOrEmpty(CoCd))
            {
                sql += "and CoCd like '%" + CoCd + "%'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("AreaSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }


        public DataTable AreaSelect(string AreaCodeT0, string AreaNameCH, string AreaNameEN, string CoCd)
        {
            string sql = @"select * from AreaSetting where 1=1 ";
            if (!string.IsNullOrEmpty(AreaCodeT0))
            {
                sql += "and AreaCodeT0 like '%" + AreaCodeT0 + "%'";
            }
            if (!string.IsNullOrEmpty(AreaNameCH))
            {
                sql += "and AreaNameCH like '%" + AreaNameCH + "%'";
            }
            if (!string.IsNullOrEmpty(AreaNameEN))
            {
                sql += "and AreaNameEN like '%" + AreaNameEN + "%'";
            }
            if (!string.IsNullOrEmpty(CoCd))
            {
                sql += "and CoCd like '%" + CoCd + "%'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("AreaSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable AreaSelect(int id)
        {
            string sql = "select * from AreaSetting where ID='" + id + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("AreaSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 根据ID做删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AreaDelete(string id)
        {
            string sql = "delete from AreaSetting where ID ='" + id + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 修改
        /// </summary>
        public int AreaUpdate(int ID, string AreaCodeT0, string AreaNameCH, string AreaNameEN, string CoCd)
        {
            string sql = @"UPDATE AreaSetting SET AreaCodeT0 = '" + AreaCodeT0 + "',AreaNameCH = '" + AreaNameCH + "' ,AreaNameEN = '" + AreaNameEN + "' ,CoCd = '" + CoCd
                    + "'  WHERE  ID='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
       
    }
}

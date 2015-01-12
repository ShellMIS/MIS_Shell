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
    public class TCodeDal
    {
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-26
        /// 添加目的：查出不同合资公司下的T8
        /// </summary>
        /// <param name="Cocd"></param>
        /// <returns></returns>
        public DataTable TCodeSelectT8(string Cocd)
        {
            string sql = "select distinct Tcode from TCodeSetting where TcodeType='T8' and CoCd='"+Cocd+"' ";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("t8");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-04
        /// 添加目的：查询所有T8
        /// </summary>
        /// <returns></returns>
        public DataTable TCodeSelectT8()
        {
            string sql = "select distinct Tcode from TCodeSetting where TcodeType='T8'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("t8");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        /// <summary>
        /// 插入TCodeSetting
        /// </summary>
        /// <param name="CoCd"></param>
        /// <param name="CoNameCH"></param>
        /// <param name="CoNameEN"></param>
        /// <param name="CoGroup1"></param>
        /// <returns></returns>
        public int TCodeInsert(string TcodeType, string Tcode, string TcodeName,string CoCd)
        {
            string sql = "insert into TCodeSetting values('" + TcodeType + "','" + Tcode + "','" + TcodeName + "','" + CoCd + "')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 查询所有数据库里的内容
        /// </summary>
        /// <returns></returns>
        public DataTable TCodeSelect()
        {
            string sql = "select * from TCodeSetting";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("TCodeSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable TCodeSelect(string TcodeType, string Tcode, string TcodeName,string CoCd)
        {
            string sql = @"select * from TCodeSetting where 1=1 ";
            if (!string.IsNullOrEmpty(TcodeType))
            {
                sql += "and TcodeType like '%" + TcodeType + "%'";
            }
            if (!string.IsNullOrEmpty(Tcode))
            {
                sql += "and Tcode like '%" + Tcode + "%'";
            }
            if (!string.IsNullOrEmpty(TcodeName))
            {
                sql += "and TcodeName like '%" + TcodeName + "%'";
            }
            if (!string.IsNullOrEmpty(CoCd))
            {
                sql += "and CoCd like '%" + CoCd + "%'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("TCodeSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable TCodeSelect(int ID)
        {
            string sql = "select * from TCodeSetting where id='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("TCodeSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 根据ID做删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int TCodeDelete(string id)
        {
            string sql = "delete from TCodeSetting where ID ='" + id + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }

        public int TCodeUpdate(int ID,string TcodeType, string Tcode, string TcodeName,string CoCd)
        {
            string sql = @"UPDATE TCodeSetting SET TcodeType = '" + TcodeType + "',Tcode = '" + Tcode + "' ,TcodeName = '" + TcodeName + "',CoCd='" + CoCd + "'  WHERE  ID='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
    }
}

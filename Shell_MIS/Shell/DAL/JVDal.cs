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
    public class JVDal
    {
        /// <summary>
        /// 添加人：ydx
        /// 添加目的：下拉列表绑定公司代码
        /// 添加日期：2014-07-21
        /// </summary>
        /// <returns></returns>
        public DataTable JVCocd()
        {
            string sql = @"select (CoCd+' '+CoNameEN)as cdNameEn,CoCd  from JVSetting where CoCd<>''";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("cocd");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-07-21
        /// 修改目的：引入分组1、分组2
        /// </summary>
        /// <param name="CoCd"></param>
        /// <param name="CoNameCH"></param>
        /// <param name="CoNameEN"></param>
        /// <param name="Share"></param>
        /// <param name="JVCode"></param>
        /// <param name="JVGroup1"></param>
        /// <param name="JVGroup2"></param>
        /// <returns></returns>
        public int JVInsert(string CoCd, string CoNameCH, string CoNameEN, string Share, string JVGroup1, string JVGroup2)
        {
            string sql = "insert into JVSetting values('" + CoCd + "','" + CoNameCH + "','" + CoNameEN + "','" + Share + "','" + JVGroup1 + "','" + JVGroup2 + "')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 删除JVSetting一条语句
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int JVDelete(string id)
        {
            string sql = "delete from JVSetting where ID ='" + id + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 模糊查询
        /// 修改人:ydx
        /// 修改时间：2014-07-21
        /// 修改目的：添加分组1，分组2条件
        /// </summary>
        /// <param name="CoCd"></param>
        /// <param name="CoNameCH"></param>
        /// <param name="CoNameEN"></param>
        /// <param name="Share"></param>
        /// <returns></returns>
        public DataTable JVSelect(string CoCd, string CoNameCH, string CoNameEN, string Share, string JVGroup1, string JVGroup2)
        {
            string sql = @"select jv.Id,jv.CoCd,jv.CoNameCH,jv.CoNameEN,jv.Share,gr1.Group1Name,gr2.Group2Name from JVSetting jv,Group1Setting gr1,Group2Setting gr2 where jv.JVGroup1=gr1.Id and jv.JVGroup2=gr2.Id";
            if (!string.IsNullOrEmpty(CoCd))
            {
                sql += " and CoCd like '%" + CoCd + "%'";
            }
            if (!string.IsNullOrEmpty(CoNameCH))
            {
                sql += " and CoNameCH like '%" + CoNameCH + "%'";
            }
            if (!string.IsNullOrEmpty(CoNameEN))
            {
                sql += " and CoNameEN like '%" + CoNameEN + "%'";
            }
            if (!string.IsNullOrEmpty(Share))
            {
                sql += " and Share like '%" + Share + "%'";
            }
           if (!string.IsNullOrEmpty(JVGroup1))
            {
                sql += " and jv.JVGroup1 like'%" + JVGroup1 + "%'";
            }
            if (!string.IsNullOrEmpty(JVGroup2))
            {
                sql += " and jv.JVGroup2 like'%" + JVGroup2 + "%'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("JVSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable JVSelect()
        {
            string sql = @"select jv.Id,jv.CoCd,jv.CoNameCH,jv.CoNameEN,jv.Share,gr1.Group1Name,gr2.Group2Name from JVSetting jv,Group1Setting gr1,Group2Setting gr2 
where jv.JVGroup1=gr1.Id and jv.JVGroup2=gr2.Id";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("JVSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable JVSelectImport()
        {
            string sql = @"select (CoCd+' '+CoNameEN)as cb,* from JVSetting";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("JVSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable JVSelect(int ID)
        {
            string sql = @"select * from JVSetting where id='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("JVSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 修改
        /// 修改人：ydx
        /// 修改时间：2014-07-21
        /// 修改目的：修改分组1，分组2
        /// </summary>
        public int JVUpdate(int ID, string CoCd, string CoNameCH, string CoNameEN, string Share, string JVGroup1, string JVGroup2)
        {
           string sql = @"UPDATE JVSetting SET CoCd = '" + CoCd + "',CoNameCH = '" + CoNameCH + "' ,CoNameEN = '" + CoNameEN + "' ,Share = '" + Share + "',JVGroup1='" + JVGroup1 + "',JVGroup2='" + JVGroup2 + "'  WHERE  ID='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
    }
}

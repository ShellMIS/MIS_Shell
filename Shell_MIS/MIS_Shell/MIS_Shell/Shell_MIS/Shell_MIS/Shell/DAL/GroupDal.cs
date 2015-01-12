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
    public class GroupDal
    {

        /// <summary>
        /// 添加人：ydx
        /// 添加目的：分组1绑定到下拉列表
        /// 添加日期：2014-07-21
        /// </summary>
        /// <returns></returns>
        public DataTable Group1()
        {
            string sql = @"select distinct CoGroup1 from GroupSetting where CoGroup1<>''";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("gr1");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加目的：分组2绑定到下拉列表
        /// 添加日期：2014-07-21
        /// </summary>
        /// <returns></returns>
        public DataTable Group2()
        {
            string sql = @"select distinct CoGroup2 from GroupSetting where CoGroup2<>''";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("gr2");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 插入GroupSetting
        /// </summary>
        /// <param name="CoCd"></param>
        /// <param name="CoNameCH"></param>
        /// <param name="CoNameEN"></param>
        /// <param name="CoGroup1"></param>
        /// <returns></returns>
        public int GroupInsert(string CoCd, string CoNameCH, string CoNameEN, string CoGroup1,string CoGroup2)
        {
            string sql = "insert into groupsetting values('" + CoCd + "','" + CoNameCH + "','" + CoNameEN + "','" + CoGroup1 + "','" + CoGroup2 + "')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 查询group名称
        /// </summary>
        /// <returns></returns>
        public DataTable GroupSelectGroup()
        {
            string sql = @"select CoGroup1 from GroupSetting";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("GroupSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 查询所有数据库里的内容
        /// </summary>
        /// <returns></returns>
        public DataTable GroupSelect()
        {
            string sql = "select * from GroupSetting";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("GroupSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable GroupSelect(string CoCd, string CoNameCH, string CoNameEN, string CoGroup1, string CoGroup2)
        {
            string sql = @"select * from GroupSetting where 1=1 ";
            if (!string.IsNullOrEmpty(CoCd))
            {
                sql += "and CoCd like '%" + CoCd + "%'";
            }
            if (!string.IsNullOrEmpty(CoNameCH))
            {
                sql += "and CoNameCH like '%" + CoNameCH + "%'";
            }
            if (!string.IsNullOrEmpty(CoNameEN))
            {
                sql += "and CoNameEN like '%" + CoNameEN + "%'";
            }
            if (!string.IsNullOrEmpty(CoGroup1))
            {
                sql += "and CoGroup1 like '%" + CoGroup1 + "%'";
            }
            if (!string.IsNullOrEmpty(CoGroup2))
            {
                sql += "and CoGroup2 like '%" + CoGroup2 + "%'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("GroupSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable GroupSelect(int ID)
        {
            string sql = "select * from GroupSetting where id='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("GroupSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 根据ID做删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GroupDelete(string id)
        {
            string sql = "delete from GroupSetting where ID ='" + id + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 修改
        /// </summary>
        public int GroupUpdate(int ID,string CoCd, string CoNameCH, string CoNameEN, string CoGroup1, string CoGroup2)
        {
            string sql = @"UPDATE GroupSetting SET CoCd = '" + CoCd + "',CoNameCH = '" + CoNameCH + "' ,CoNameEN = '" + CoNameEN + "' ,CoGroup1 = '" + CoGroup1
                    + "',CoGroup2='" + CoGroup2 + "'  WHERE  ID='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
    }
}

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
    /// <summary>
    /// 添加人：ydx
    /// 添加日期：2014-07-22
    /// 添加目的：类别管理
    /// </summary>
   public class SiteCategoryDal
    { /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
       public int SiteCategoryInsert(string SiteCategory1, string SiteCategory2, string SiteCategoryName)
        {
            string sql = "insert into T_SiteCategorySettings values ('" + SiteCategory1 + "','" + SiteCategory2 + "','" + SiteCategoryName + "')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 查询所有数据PLDB中的
        /// </summary>
        /// <returns></returns>
       public DataTable SiteCategorySelect()
        {
            string sql = " select * from T_SiteCategorySettings";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("SiteCategorySetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 根据不同的查询条件获取数据
        /// </summary>
        /// <param name="plDb_id"></param>
        /// <param name="PlDb_Item"></param>
        /// <returns></returns>
       public DataTable SiteCategorySelect( string SiteCategory1, string SiteCategory2, string SiteCategoryName)
        {
            string sql = @" select * from T_SiteCategorySettings where 1=1 ";
           
            if (!string.IsNullOrEmpty(SiteCategory1))
            {
                sql += " and SiteCategory1 like'%" + SiteCategory1 + "%'";
            }
            if (!string.IsNullOrEmpty(SiteCategory2))
            {
                sql += " and SiteCategory2 like'%" + SiteCategory2 + "%'";
            }
            if (!string.IsNullOrEmpty(SiteCategoryName))
           {
               sql += " and SiteCategoryName like'%" + SiteCategoryName + "%'";
           }

            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("SiteCategorySetting");
            dt.Load(dr);
            dr.Close();
            return dt;

        }
        /// <summary>
        /// 根据编号获取数据
        /// </summary>
        /// <param name="PlDb_Id"></param>
        /// <returns></returns>
       public DataTable SiteCategorySelect(int id)
        {
            string sql = "select * from T_SiteCategorySettings where Id=" + id + "";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("siteSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 根据编号删除一条数据
        /// </summary>
        /// <param name="PlDb_Id"></param>
        /// <returns></returns>
       public int SiteCategoryDelete(int id)
        {
            string sql = "delete from T_SiteCategorySettings where Id=" + id + "";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }

       public int SiteCategoryUpdate(int id, string SiteCategory1, string SiteCategory2, string SiteCategoryName)
        {
            string sql = @"update T_SiteCategorySettings set SiteCategory1='" + SiteCategory1 + "',SiteCategory2='" + SiteCategory2 + "',SiteCategoryName='" + SiteCategoryName + "' where Id=" + id + "";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
    }
}

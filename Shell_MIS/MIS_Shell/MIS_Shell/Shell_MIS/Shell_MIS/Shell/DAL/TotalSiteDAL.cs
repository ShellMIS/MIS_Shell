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
    /// 添加人:ydx
    /// 添加时间：2014-08-28
    /// 添加目的：油站数量管理
    /// </summary>
   public class TotalSiteDAL
    {
        /// <summary>
        /// 插入TotalSite
        /// </summary>
        /// <param name="CoCd"></param>
        /// <param name="CoNameCH"></param>
        /// <param name="CoNameEN"></param>
        /// <param name="CoGroup1"></param>
        /// <returns></returns>
       public int TotalSiteInsert(string Period, string Cocd, string CoNameEN, string T3Code, int TotalSiteinOperation, int TemporaryClosedSites, int NewOpenedSites, int closedSites, int NewSecuredSites, int Unopened)
        {
            string sql = @"insert into T_TotalSite values('"+Period+"','"+Cocd+"','"+CoNameEN+"','"+T3Code+"',"+TotalSiteinOperation+","+TemporaryClosedSites+","+NewOpenedSites+","+closedSites+","+NewSecuredSites+","+Unopened+")";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 查询所有数据库里的内容
        /// </summary>
        /// <returns></returns>
        public DataTable TotalSiteSelect()
        {
            string sql = "select id as 'id' ,Period as 'Period' ,Cocd as 'Cocd',CoNameEN as 'CoName EN',T3Code as 'T3Code' ,TotalSiteinOperation as 'Total Site in Operation' ,TemporaryClosedSites as 'No. of Temporary Closed Sites',NewOpenedSites as 'No. of New Opened Sites',closedSites as 'No. of closed Sites',NewSecuredSites as 'No. of New Secured Sites',Unopened as 'Unopened' from T_TotalSite";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_TotalSiteSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        /// <summary>
        ///导入重复判断
        /// </summary>
        /// <param name="Period"></param>
        /// <param name="Cocd"></param>
        /// <param name="CoNameEN"></param>
        /// <param name="T3Code"></param>
        /// <param name="TotalSiteinOperation"></param>
        /// <param name="TemporaryClosedSites"></param>
        /// <param name="NewOpenedSites"></param>
        /// <param name="closedSites"></param>
        /// <param name="NewSecuredSites"></param>
        /// <param name="Unopened"></param>
        /// <returns></returns>
        //public DataTable TotalSiteSelect(string Period, string Cocd, string CoNameEN, string T3Code, string TotalSiteinOperation, string TemporaryClosedSites, string NewOpenedSites, string closedSites, string NewSecuredSites, string Unopened)
        public DataTable TotalSiteSelectS(string Period, string Cocd, string T3Code, string TotalSiteinOperation, string TemporaryClosedSites, string NewOpenedSites, string closedSites, string NewSecuredSites, string Unopened)
        {
            string sql = @"select id as 'id' ,Period as 'Period' ,Cocd as 'Cocd',CoNameEN as 'CoName EN',T3Code as 'T3Code' ,TotalSiteinOperation as 'Total Site in Operation' ,TemporaryClosedSites as 'No. of Temporary Closed Sites',NewOpenedSites as 'No. of New Opened Sites',closedSites as 'No. of closed Sites',NewSecuredSites as 'No. of New Secured Sites',Unopened as 'Unopened' from T_TotalSite where 1=1 ";
            if (!string.IsNullOrEmpty(Period))
            {
                sql += "and Period = '" + Period + "'";
            }
            if (!string.IsNullOrEmpty(Cocd))
            {
                sql += "and Cocd = '" + Cocd + "'";
            }
            //if (!string.IsNullOrEmpty(CoNameEN))
            //{
            //    sql += "and CoNameEN like '%" + CoNameEN + "%'";
            //}
            if (!string.IsNullOrEmpty(T3Code))
            {
                sql += "and T3Code = '" + T3Code + "'";
            }
            if (!string.IsNullOrEmpty(TotalSiteinOperation))
            {
                sql += "and TotalSiteinOperation = '" + TotalSiteinOperation + "'";
            }
            if (!string.IsNullOrEmpty(TemporaryClosedSites))
            {
                sql += "and TemporaryClosedSites = '" + TemporaryClosedSites + "'";
            }
            if (!string.IsNullOrEmpty(NewOpenedSites))
            {
                sql += "and NewOpenedSites = '" + NewOpenedSites + "'";
            }
            if (!string.IsNullOrEmpty(closedSites))
            {
                sql += "and closedSites = '" + closedSites + "'";
            }
            if (!string.IsNullOrEmpty(NewSecuredSites))
            {
                sql += "and NewSecuredSites = '" + NewSecuredSites + "'";
            }
            if (!string.IsNullOrEmpty(Unopened))
            {
                sql += "and Unopened = '" + Unopened + "'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_TotalSite");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="Period"></param>
       /// <param name="Cocd"></param>
       /// <param name="CoNameEN"></param>
       /// <param name="T3Code"></param>
       /// <param name="TotalSiteinOperation"></param>
       /// <param name="TemporaryClosedSites"></param>
       /// <param name="NewOpenedSites"></param>
       /// <param name="closedSites"></param>
       /// <param name="NewSecuredSites"></param>
       /// <param name="Unopened"></param>
       /// <returns></returns>
        //public DataTable TotalSiteSelect(string Period, string Cocd, string CoNameEN, string T3Code, string TotalSiteinOperation, string TemporaryClosedSites, string NewOpenedSites, string closedSites, string NewSecuredSites, string Unopened)
        public DataTable TotalSiteSelect(string Period, string Cocd,string T3Code, string TotalSiteinOperation, string TemporaryClosedSites, string NewOpenedSites, string closedSites, string NewSecuredSites, string Unopened)
        {
            string sql = @"select id as 'id' ,Period as 'Period' ,Cocd as 'Cocd',CoNameEN as 'CoName EN',T3Code as 'T3Code' ,TotalSiteinOperation as 'Total Site in Operation' ,TemporaryClosedSites as 'No. of Temporary Closed Sites',NewOpenedSites as 'No. of New Opened Sites',closedSites as 'No. of closed Sites',NewSecuredSites as 'No. of New Secured Sites',Unopened as 'Unopened' from T_TotalSite where 1=1 ";
            if (!string.IsNullOrEmpty(Period))
            {
                sql += "and Period like '%" + Period + "%'";
            }
            if (!string.IsNullOrEmpty(Cocd))
            {
                sql += "and Cocd like '%" + Cocd + "%'";
            }
            //if (!string.IsNullOrEmpty(CoNameEN))
            //{
            //    sql += "and CoNameEN like '%" + CoNameEN + "%'";
            //}
            if (!string.IsNullOrEmpty(T3Code))
            {
                sql += "and T3Code like '%" + T3Code + "%'";
            }
            if (!string.IsNullOrEmpty(TotalSiteinOperation))
            {
                sql += "and TotalSiteinOperation like '%" + TotalSiteinOperation + "%'";
            }
            if (!string.IsNullOrEmpty(TemporaryClosedSites))
            {
                sql += "and TemporaryClosedSites like '%" + TemporaryClosedSites + "%'";
            }
            if (!string.IsNullOrEmpty(NewOpenedSites))
            {
                sql += "and NewOpenedSites like '%" + NewOpenedSites + "%'";
            }
            if (!string.IsNullOrEmpty(closedSites))
            {
                sql += "and closedSites like '%" + closedSites + "%'";
            }
            if (!string.IsNullOrEmpty(NewSecuredSites))
            {
                sql += "and NewSecuredSites like '%" + NewSecuredSites + "%'";
            }
            if (!string.IsNullOrEmpty(Unopened))
            {
                sql += "and Unopened like '%" + Unopened + "%'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_TotalSite");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
       /// <summary>
       /// 根据编号查出一条记录
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
        public DataTable TotalSiteSelect(int ID)
        {
            string sql = "select * from T_TotalSite where id='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_TotalSite");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 根据ID做删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int TotalSiteDelete(string id)
        {
            string sql = "delete from T_TotalSite where id ='" + id + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
       /// <summary>
       /// 修改
       /// </summary>
       /// <param name="ID"></param>
       /// <param name="AccountType"></param>
       /// <param name="Account"></param>
       /// <param name="Account_Description"></param>
       /// <param name="Status"></param>
       /// <param name="Update"></param>
       /// <returns></returns>
        public int TotalSiteUpdate(int ID, string Period, string Cocd, string CoNameEN, string T3Code, string TotalSiteinOperation, string TemporaryClosedSites, string NewOpenedSites, string closedSites, string NewSecuredSites, string Unopened)
        {
            string sql = @"UPDATE T_TotalSite SET Period = '" + Period + "',Cocd = '" + Cocd + "' ,CoNameEN = '" + CoNameEN + "' ,T3Code = '" + T3Code
                    + "',TotalSiteinOperation='" + TotalSiteinOperation + "',TemporaryClosedSites='" + TemporaryClosedSites + "',NewOpenedSites='" + NewOpenedSites + "',closedSites='" + closedSites + "',NewSecuredSites='" + NewSecuredSites + "',Unopened='" + Unopened + "'  WHERE  id='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
    }
}

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
    public class Group2Dal
    {

        /// <summary>
        /// 查询所有数据库里的内容
        /// </summary>
        /// <returns></returns>
        public DataTable GroupSe()
        {
            string sql = "select Id,Group2Name from Group2Setting where Status ='Active'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("GroupSetting");
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
        public int GroupInsert(string Group2Name, string State, string Group1Id)
        {
            string sql = "insert into Group2Setting values('" + Group2Name + "','" + State + "'," + Group1Id + ")";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
       
        /// <summary>
        /// 查询所有数据库里的内容
        /// </summary>
        /// <returns></returns>
        public DataTable GroupSelect()
        {
            string sql = " select  g2.Id,g1.Group1Name,g1.Status,g2.Group2Name,g2.Status from Group2Setting as g2,Group1Setting g1  where g1.Id=g2.Group1Id";
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
        public DataTable GroupSelect(string state)
        {
            string sql = " select  g2.Id,g1.Status,g1.Group1Name,g1.Status,g2.Group2Name,g2.Status from Group2Setting as g2,Group1Setting g1  where g1.Id=g2.Group1Id";
            if (!string.IsNullOrEmpty(state))
            {
                sql += " and g2.State like '%" + state + "%'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("GroupSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable GroupSelect(string Group2Name, string State, string Group1Id)
        {
            string sql = @"select g2.Id, g1.Group1Name,g1.Status,g2.Group2Name,g2.Status from Group2Setting as g2,Group1Setting g1 where g1.Id=g2.Group1Id ";
             if (!string.IsNullOrEmpty(Group2Name))
            {
                sql += "and g2. Group2Name like '%" + Group2Name + "%'";
            }
            if (!string.IsNullOrEmpty(State))
            {
                sql += "and g2.Status like '%" + State + "%'";
            }
            if (!string.IsNullOrEmpty(Group1Id))
            {
                sql += " and g2.Group1Id like '%" + Group1Id + "%'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("GroupSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 根据id查分组2的一条数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataTable GroupSelect(int ID)
        {
            string sql = "select * from Group2Setting  where Id='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("GroupSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }



       /// <summary>
       /// 根据分组1编号查询分组1下的所有分组2 
       /// 修改人：ydx   select * 
       /// 2014年12月11日 hxy修改 
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
         public DataTable GroupSelectByGr1Id(int ID)
        {
            string sql = " select Id,Group2Name from Group2Setting where Group1Id="+ID+"";
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
            string sql = "delete from Group2Setting where Id ='" + id + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 修改
        /// </summary>
        public int GroupUpdate(int ID, string Group2Name, string State, string Group1Id)
        {
            string sql = @"UPDATE Group2Setting SET Group2Name = '" + Group2Name + "',Status = '" + State + "',Group1Id='" + Group1Id + "' WHERE  Id='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
    }
}

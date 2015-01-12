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
   public class Group1Dal
    {
        /// <summary>
        /// 插入GroupSetting
        /// </summary>
        /// <param name="CoCd"></param>
        /// <param name="CoNameCH"></param>
        /// <param name="CoNameEN"></param>
        /// <param name="CoGroup1"></param>
        /// <returns></returns>
       public int GroupInsert(string Group1Name, string State)
        {
            string sql = "insert into Group1Setting values('" + Group1Name + "','" + State + "')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
       /// <summary>
       /// 查询所有数据库里的内容
       /// </summary>
       /// <returns></returns>
       public DataTable GroupSelectIf()
       {
           string sql = "select * from Group1Setting where Status ='Active'";
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
            string sql = "select * from Group1Setting";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("GroupSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable GroupSelect(string Group1Name, string State)
        {
            string sql = @"select * from Group1Setting where 1=1 ";
            if (!string.IsNullOrEmpty(Group1Name))
            {
                sql += "and Group1Name like '%" + Group1Name + "%'";
            }
            if (!string.IsNullOrEmpty(State))
            {
                sql += "and Status like '%" + State + "%'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("GroupSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable GroupSelect(int ID)
        {
            string sql = "select * from Group1Setting where Id='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("GroupSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
       /// <summary>
       /// hxy 添加一个重载，在status不填写的时候，查询所有的
       /// </summary>
       /// <param name="strWhere"></param>
       /// <returns></returns>
        public DataTable GroupSelect(string strWhere)
        {
            string sql = @"select * from Group1Setting where 1=1 ";
            if (strWhere!="")
            {
                sql += strWhere;
            }
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
            string sql = "delete from Group1Setting where Id ='" + id + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 修改
        /// </summary>
        public int GroupUpdate(int ID, string Group1Name, string State)
        {
            string sql = @"UPDATE Group1Setting SET Group1Name = '" + Group1Name + "',Status = '" + State + "' WHERE  Id='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
    }
}

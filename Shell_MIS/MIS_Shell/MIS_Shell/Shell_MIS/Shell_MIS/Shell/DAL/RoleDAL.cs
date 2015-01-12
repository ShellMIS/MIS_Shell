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
    //添加人：ydx
    //目的：对角色表进行处理
   public class RoleDAL
    {
        /// <summary>
        /// 获取所有数据 T_CF_Role表
        /// </summary>
        /// <returns></returns>
        public DataTable RoleSelectCombox()
        {
            string sql = @" select RoleID,RoleName from T_CF_Role";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable table = new DataTable("roled");
            table.Load(dr);
            dr.Close();
            return table;
        }
       
        /// <summary>
        /// 获取所有数据 T_CF_Role表
        /// </summary>
        /// <returns></returns>
        public DataTable ifExistsRole(int roleId,string roleName)
        {
            string sql = @" select * from T_CF_Role where RoleID="+roleId+" and RoleName='"+roleName+"'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable table = new DataTable("roles");
            table.Load(dr);
            dr.Close();
            return table;
        }
        /// <summary>
        /// 获取所有数据 T_CF_Role表
        /// </summary>
        /// <returns></returns>
        public DataTable RoleSelect()
        {
            string sql = @" select * from T_CF_Role";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable table = new DataTable("role");
            table.Load(dr);
            dr.Close();
            return table;
        }
        /// <summary>
        /// 根据编号查出一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable RoleSelect(int id)
        {
            string sql = @"select * from T_CF_Role where RoleID="+id+" ";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("role");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        
    }
}

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
    /// 添加时间：2014-10-30
    /// 添加目的:角色，权限表
    /// </summary>
   public class RolePrivilegeDAL
    {


       /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-11-03
        /// 添加目的：获取所有权限
       /// </summary>
       /// <returns></returns>
       public DataTable PriTargeByRoleId()
        {
            string sql = @" select pri.PrivilegeTarget  from T_CF_RolePrivilege rolePri inner join [T_CF_ Privilege] pri on rolePri.PrivilegeId=pri.PrivilegeID ";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable table = new DataTable("prid");
            table.Load(dr);
            dr.Close();
            return table;
        }

        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-11-03
        /// 添加目的：根据角色编号 获取此角色下的权限模块id
        /// </summary>
        /// <returns></returns>
       // public DataTable PriTargeByRoleId(int roleId)
       public DataTable PriTargeByRoleId(string roleId)
        {
            //string sql = @" select pri.PrivilegeTarget from T_CF_RolePrivilege rolePri inner join [T_CF_ Privilege] pri on rolePri.PrivilegeId=pri.PrivilegeID and rolePri.RoleId=" + roleId + " order by pri.Description ";
            string sql = @" select pri.PrivilegeTarget from T_CF_RolePrivilege rolePri inner join [T_CF_ Privilege] pri on rolePri.PrivilegeId=pri.PrivilegeID and rolePri.RoleId in ("+roleId+") order by pri.Description ";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable table = new DataTable("prid");
            table.Load(dr);
            dr.Close();
            return table;
        }
       /// <summary>
       /// 添加人：ydx
       /// 添加时间：2014-11-03
       /// 添加目的：删除
       /// </summary>
       /// <param name="roleId"></param>
       /// <param name="privilegeId"></param>
       /// <param name="isVisible"></param>
       /// <returns></returns>
       public int rolePrivilegeDelete(int roleId)
       {
           string sql = @"delete from T_CF_RolePrivilege where RoleId="+roleId+" ";
           return SqlHelp.sqlexecutereader(sql);
       }
       /// <summary>
       /// 添加人：ydx
       /// 添加时间：2014-11-03
       /// 添加目的：添加
       /// </summary>
       /// <returns></returns>
       public int rolePrivilegeInsert(int roleId, int  privilegeId,string isVisible)
       {
           string sql = "insert into [T_CF_RolePrivilege] values(" + roleId + "," + privilegeId + ",'" + isVisible + "') select @@identity;";
           return SqlHelp.sqlexecutereader(sql);
       }
        /// <summary>
        /// 获取所有数据 T_CF_Role表
        /// </summary>
        /// <returns></returns>
        public DataTable IfExistsRoleAndPrivilege(int roleId,int privilegeId)
        {
            string sql = @" select COUNT(*) from T_CF_RolePrivilege where RoleId="+roleId+" and PrivilegeId="+privilegeId+"";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable table = new DataTable("exis");
            table.Load(dr);
            dr.Close();
            return table;
        }
        /// <summary>
        /// 获取所有数据 T_CF_Role表
        /// </summary>
        /// <returns></returns>
        public DataTable RolePrivilegeSelect(int roleId)
        {
            string sql = @" select * from T_CF_RolePrivilege where RoleId=" + roleId + "";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable table = new DataTable("rolePrivilege");
            table.Load(dr);
            dr.Close();
            return table;
        }
    }
}

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
   //目的：人员、角色关联的一个表
   public class UserAndRole
    {
       /// <summary>
       /// 添加人:ydx
       /// 添加时间：2014-11-11
       /// 添加目的：根据人员编号 查出角色编号
       /// </summary>
       /// <param name="userids"></param>
       /// <returns></returns>
       public DataTable getRoleIdsByUserId(int userId)
       {
           string sql = @" select RoleID  from T_CF_UserRole where UserID ="+userId+"";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable table = new DataTable("roleid");
           table.Load(dr);
           dr.Close();
           return table;
       }
       /// <summary>
       /// 删除此角色下的用户
       /// </summary>
       /// <param name="roleId"></param>
       /// <returns></returns>
       public int userAndRoleDelete(string roleId)
       {
           string sql = @"delete from T_CF_UserRole where RoleID="+roleId+"";
           return SqlHelp.sqlexecutereader(sql);
       }
       /// <summary>
       /// 添加人：ydx
       /// 添加时间：2014-11-03
       /// 添加目的：判断人员，角色是否已经存在
       /// </summary>
       /// <param name="roleId"></param>
       /// <param name="userId"></param>
       /// <returns></returns>
       public DataTable IfExistsRoleAndUser(int roleId, int userId)
       {
           string sql = @" select COUNT(*) from T_CF_UserRole where RoleId=" + roleId + " and UserID=" + userId + "";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable table = new DataTable("exis");
           table.Load(dr);
           dr.Close();
           return table;
       }
       /// <summary>
       /// 添加人：ydx
       /// 添加时间：2014-11-03
       /// 添加目的：根据角色编号，查出此角色下的所有用户
       /// </summary>
       /// <param name="roleId"></param>
       /// <returns></returns>
       public DataTable userIdByRoleId(int roleId)
       {
           string sql = @" select UserID from T_CF_UserRole where RoleID=" + roleId + "";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable table = new DataTable("userId");
           table.Load(dr);
           dr.Close();
           return table;
       }

       /// <summary>
       /// 添加 
       /// </summary>
       /// <param name="UserID"></param>
       /// <param name="RoleID"></param>
       /// <returns></returns>
       public int UandRInsert(string UserID, string RoleID,string CreatedBy,string CreatedDate,string ModifyBy,string ModifyDate)
       {
           string sql = @"insert into T_CF_UserRole values('" + UserID + "','" + RoleID + "','" + CreatedBy + "','" + CreatedDate + "','" + ModifyBy + "','" + ModifyDate + "') select @@identity;";
           return SqlHelp.sqlexecutereader(sql);
       }
       /// <summary>
       /// 修改 
       /// </summary>
       /// <param name="UserID"></param>
       /// <param name="RoleID"></param>
       /// <returns></returns>
       public int updateUandR(string UserID, string RoleID, string ModifyBy, string ModifyDate)
       {
           string sql = @" update T_CF_UserRole set RoleID='" + RoleID + "',ModifyBy='" + ModifyBy + "',ModifyDate='" + ModifyDate + "' where UserID='" + UserID + "'";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           dr.Close();
           return 1;
       }

       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int deleteUserAndRole(string id)
       {
           string sql = @"delete from T_CF_UserRole where UserID='" + id + "'";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           dr.Close();
           return 1;
       }
   }
}

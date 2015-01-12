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
    //添加目的:对人员进行处理
    public class UserDAL
    {

        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-11-03
        /// 添加目的：根据部门编号 查出此部门下的 所有人员
        /// </summary>
        /// <returns></returns>
        public DataTable userIdByDepId(int depid)
        {
            string sql = @"  select UserID,UserName+RealName as ur from T_CF_User where DeptID=" + depid + "";
            DataTable dt = SqlHelp.ExecuteDt(sql);
            return dt;
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-11-03
        /// 添加目的：下拉复选框
        /// </summary>
        /// <returns></returns>
        public DataTable UserListComb()
        {
            string sql = @"  select UserID,UserName+RealName as ur from T_CF_User";
            DataTable dt = SqlHelp.ExecuteDt(sql);
            return dt;
        }

        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-11-03
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataTable ExitUser(string UserName)
        {
            string sql = "select tu.UserID,tu.UserName,tu.UserPwd,tu.RealName,tu.DeptID,tu.PhoneNum1,tu.PhoneNum2,tu.Email,tr.RoleName,tr.RoleID  from T_CF_UserRole as tcu  inner join T_CF_User as tu on tu.UserID=tcu.UserID  inner join T_CF_Role as tr on tr.RoleID=tcu.RoleID  where tu.UserName='" + UserName + "'";
            DataTable dt = SqlHelp.ExecuteDt(sql);
            return dt;
        }
        /// <summary>
        /// 2014-07-14 修改ydx
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="UserPwd"></param>
        /// <returns></returns>
        //public DataTable ExitUser(string UserName, string UserPwd)
        //{
        //   string sql = "select top 1 tu.UserID,tu.UserName,tu.UserPwd,tu.RealName,tu.DeptID,tu.PhoneNum1,tu.PhoneNum2,tu.Email,tr.RoleName,tr.RoleID  from T_CF_UserRole as tcu  inner join T_CF_User as tu on tu.UserID=tcu.UserID  inner join T_CF_Role as tr on tr.RoleID=tcu.RoleID  where tu.UserName='"+UserName+"' and tu.UserPwd='"+UserPwd+"'";
        //    DataTable dt = SqlHelp.ExecuteDt(sql);
        //    return dt;
        //}
        //public DataTable ExitUser(string UserName, string UserPwd)
        //{
        //    string sql = "select count(*) from T_CF_User where UserName='" + UserName + "' and UserPwd='" + UserPwd + "'";
        //    DataTable dt = SqlHelp.ExecuteDt(sql);
        //    return dt;
        //}
        /// <summary>
        /// 添加人员同时返回此人的 编号 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="UserPwd"></param>
        /// <param name="DeptID"></param>
        /// <param name="RealName"></param>
        /// <param name="PhoneNum1"></param>
        /// <param name="PhoneNum2"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        public int UserInsert(string UserName, string UserPwd, string DeptID, string RealName, string PhoneNum1, string PhoneNum2, string Email, string CreatedBy, string CreatedDate, string ModifyBy, string ModifyDate)
        {
            string sql = "insert into [T_CF_User] values('" + UserName + "','" + UserPwd + "','" + DeptID + "','" + RealName + "','" + PhoneNum1 + "','" + PhoneNum2 + "','" + Email + "','" + CreatedBy + "','" + CreatedDate + "','" + ModifyBy + "','" + ModifyDate + "') select @@identity;";
            return  SqlHelp.sqlexecutereader(sql);
        }
        /// <summary>
        /// 根据编号查出一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable UserSelect(int id)
        {
            string sql = @"  select tu.UserID,tu.UserName,tu.UserPwd,tu.RealName,tu.DeptID,tu.PhoneNum1,tu.PhoneNum2,tu.Email,tr.RoleName,tr.RoleID  from T_CF_UserRole as tcu  inner join T_CF_User as tu on tu.UserID=tcu.UserID  inner join T_CF_Role as tr on tr.RoleID=tcu.RoleID  where tu.UserID=" + id + " ";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_CF_User");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 获取所有数据 T_CF_User表中的
        /// </summary>
        /// <returns></returns>
        public DataTable UserSelect()
        {
            string sql = @"  select tu.UserID,tu.UserName,tr.RoleName,tu.UserPwd,tu.RealName,de.DepartmentEnglish,de.DepartmentName,tu.PhoneNum1,tu.PhoneNum2,tu.Email,tr.RoleName,tu.CreatedBy,convert ( varchar(10),tu.CreatedDate,121) as CreatedDate ,tu.ModifyBy,convert ( varchar(10),tu.ModifyDate,121) as ModifyDate  from T_CF_UserRole as tcu inner join T_CF_User as tu on tu.UserID=tcu.UserID inner join T_CF_Role as tr on tr.RoleID=tcu.RoleID  inner join Department as de on de.ID=tu.DeptID";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable table = new DataTable("T_CF_User");
            table.Load(dr);
            dr.Close();
            return table;
        }
        /// <summary>
        /// 根据不同条件查询
        /// </summary>
        public DataTable UserSelect(string UserName, string UserPwd, string DeptID, string RealName, string PhoneNum1, string PhoneNum2, string Email,string RoleID)
        {
            string sql = @" select tu.UserID,tr.RoleName,tu.DeptID,tu.UserName,tu.UserPwd,tu.RealName,tu.PhoneNum1,tu.PhoneNum2,tu.Email,tr.RoleName,tu.CreatedBy,convert ( varchar(10),tu.CreatedDate,121) as CreatedDate ,tu.ModifyBy,convert ( varchar(10),tu.ModifyDate,121) as ModifyDate  from T_CF_UserRole as tcu inner join T_CF_User as tu on tu.UserID=tcu.UserID inner join T_CF_Role as tr on tr.RoleID=tcu.RoleID where 1=1 ";

            if (!string.IsNullOrEmpty(UserName))
            {
                sql += " and UserName like'%" + UserName + "%'";
            }
            if (!string.IsNullOrEmpty(UserPwd))
            {
                sql += " and UserPwd like'%" + UserPwd + "%'";
            }
            if (!string.IsNullOrEmpty(DeptID))
            {
                sql += " and DeptID like'%" + DeptID + "%'";
            }
            if (!string.IsNullOrEmpty(RealName))
            {
                sql += " and RealName like'%" + RealName + "%'";
            }
            if (!string.IsNullOrEmpty(PhoneNum1))
            {
                sql += " and tu.PhoneNum1 like'%" + PhoneNum1 + "%'";
            }
            if (!string.IsNullOrEmpty(PhoneNum2))
            {
                sql += " and PhoneNum2 like'%" + PhoneNum2 + "%'";
            }
            if (!string.IsNullOrEmpty(Email))
            {
                sql += " and Email like'%" + Email + "%'";
            }
            if (!string.IsNullOrEmpty(RoleID))
            {
                sql += " and tr.RoleID like'%" + RoleID + "%'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("tcu");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="UserName"></param>
        /// <param name="UserPwd"></param>
        /// <param name="DeptID"></param>
        /// <param name="RealName"></param>
        /// <param name="PhoneNum1"></param>
        /// <param name="PhoneNum2"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        public int updateUser(int UserID, string UserName, string UserPwd, string DeptID, string RealName, string PhoneNum1, string PhoneNum2, string Email, string ModifyBy, string ModifyDate)
        {
            string sql = "update T_CF_User set UserName='" + UserName + "',UserPwd='" + UserPwd + "',DeptID='" + DeptID + "',RealName='" + RealName + "',PhoneNum1='" + PhoneNum1 + "',PhoneNum2='" + PhoneNum2 + "',Email='" + Email + "',ModifyBy='" + ModifyBy + "',ModifyDate='" + ModifyDate + "' where UserID=" + UserID + "";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int deleteUser(int id)
        {
            string sql = "delete from [T_CF_User] where UserID=" + id + "";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 判断是否用户名重名
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsExsit(string username)
        {
            bool result = false;
            string sql = "select count(1) from T_CF_User where UserName='" + username + "'";
            int i = SqlHelp.ExecuteNonQuery(sql);
            if (i>0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 修改个人面膜
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int updateUserPwd(string pwd)
        {
            string sql = "update  [T_CF_User] set UserPwd=" + pwd + "";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBHelper;

namespace DAL
{
    public class DepartmentDAL
    {
        DataTable dt = new DataTable();
        SqlDataReader sdr;
        /// <summary>
        ///添加人：ydx
        ///添加时间：2014-11-13
        ///添加目的： 此查询结果 用于绑定部门列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDepartmentCombox()
        {

            string sql = @" select ID,DepartmentEnglish+' '+DepartmentName as 'Name' from [Department]";
            try
            {
                dt = SqlHelp.ExecuteDt(sql);
            }
            catch (Exception)
            {
                throw;

            }
            return dt;

        }
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="DepartmentName">部门名称</param>
        /// <param name="DepartmentEnglish">部门英文缩写</param>
        /// <returns></returns>
        public int InsertDepartment(string DepartmentName, string DepartmentEnglish)
        {
            int result = 0;
            string sql = "insert into Department values('" + DepartmentName + "','" + DepartmentEnglish + "')";
            try
            {
                result = SqlHelp.ExecuteSql(sql);
            }
            catch (Exception)
            {
                throw;

            }
            if (result>0)
            {
                result = 1;
            }
            return result;

        }
        /// <summary>
        /// 查询部门
        /// </summary>
        /// <returns></returns>
        public DataTable  GetDepartment( string StrWhere)
        {

            string sql = "SELECT [ID]       ,[DepartmentName] 部门名称      ,[DepartmentEnglish] 部门英文缩写  FROM [Shell-MIS].[dbo].[Department]  where 1=1";
            if (StrWhere!="")
            {
                sql += StrWhere;
            }
            sql += " order by ID Desc";
        
            try
            {
                dt = SqlHelp.ExecuteDt(sql);
            }
            catch (Exception)
            {
                throw;

            }
            return dt;

        }
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="ID">主键</param>
        /// <param name="DepartmentEnglish">英文缩写</param>
        /// <param name="DepartmentName">部门名称</param>
        /// <returns></returns>
        public int AlterDepartment(string ID,string DepartmentEnglish,string DepartmentName)
        {
            int result = 0;
            string sql = "update  Department set DepartmentName='" + DepartmentName + "' , DepartmentEnglish='" + DepartmentEnglish + "'  where ID='"+ID+"'";
            try
            {
                result = SqlHelp.ExecuteSql(sql);
            }
            catch (Exception)
            {
                throw;

            }
            if (result > 0)
            {
                result = 1;
            }
            return result;
        }

    }
}

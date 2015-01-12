using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBHelper;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    //添加人：ydx
    //添加目的:操作日志处理
   public class OptionLogDAL
    {

       /// <summary>
       /// 操作日志  添加 
       /// </summary>
       /// <param name="UserId"></param>
       /// <param name="OpertionDes"></param>
       /// <param name="OpertionDate"></param>
       /// <returns></returns>
       public int OpertionLogInsert(int UserId, string OpertionDes, string OpertionDate, string OpertionState)
        {
          // SqlConnection conn = new SqlConnection("Data Source=SHELL-A918E8DCD\\ORCL;Initial Catalog=Shell-MIS;pwd=12345678;User Id=sa;");
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Shell-MIS;pwd=12345678;User Id=sa;");
           conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select NewID() ";         
            object rowID =cmd.ExecuteScalar();
            Guid guid = new Guid(rowID.ToString());
            cmd.CommandText = "insert into [T_OpertionLog] values('" + guid + "'," + UserId + ",'" + OpertionDes + "','" + OpertionDate + "','" + OpertionState + "') ";
         return   cmd.ExecuteNonQuery();

        }
        /// <summary>
        /// 根据编号查出一条数据 操作日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable OpertionLogSelect(int id)
        {
            string sql = @" select ol.Id,ol.UserId,tu.UserName,ol.OpertionDes,CONVERT(varchar(19),ol.OpertionDate,121) as OpertionDate,ol.OpertionState from [T_OpertionLog] as ol inner join  [T_CF_User] as tu on tu.UserID=ol.UserId where ol.ID=" + id + "";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("opLog");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 获取所有数据 [T_OpertionLog]表中的 操作日志
        /// </summary>
        /// <returns></returns>
        public DataTable OpertionLogSelect()
        {
            string sql = @" select ol.Id, ol.UserId,tu.UserName,ol.OpertionDescription,CONVERT(varchar(19),ol.OpertionDate,121) as OpertionDate,ol.OpertionState from [T_OpertionLog] as ol inner join  [T_CF_User] as tu on tu.UserID=ol.UserId";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable table = new DataTable("opLog");
            table.Load(dr);
            dr.Close();
            return table;
        }
        /// <summary>
        /// 根据不同条件查询 操作日志
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserName"></param>
        /// <param name="OpertionDes"></param>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable OpertionLogSelect(string UserId,string UserName, string OpertionDes,string starTime,string endTime)
        {
            string sql = @" select ol.Id, ol.UserId,tu.UserName,ol.OpertionDes,CONVERT(varchar(19),ol.OpertionDate,121) as OpertionDate,ol.OpertionState from [T_OpertionLog] as ol inner join  [T_CF_User] as tu on tu.UserID=ol.UserId where 1=1 ";

            if (!string.IsNullOrEmpty(UserId.ToString()))
            {
                sql += " and ol.UserId like'%" + UserId + "%'";
            }
            if (!string.IsNullOrEmpty(UserName))
            {
                sql += " and tu.UserName like'%" + UserName + "%'";
            }
            if (!string.IsNullOrEmpty(OpertionDes))
            {
                sql += " and ol.OpertionDes like'%" + OpertionDes + "%'";
            }
            if (!string.IsNullOrEmpty(starTime))
            {
                sql += " and CONVERT(varchar(19),ol.OpertionDate,121) >'" + starTime + "'";
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                sql += " and CONVERT(varchar(19),ol.OpertionDate,121) <'" + endTime + "'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("opLog");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
       /// <summary>
       /// 根据不同条件查询 操作日志
       /// </summary>
       /// <param name="UserId"></param>
       /// <param name="UserName"></param>
       /// <param name="OpertionDes"></param>
       /// <param name="LogDate"></param>
       /// <returns></returns>
        public DataTable OpertionLogSelect(string UserId, string UserName, string OpertionDes, string LogDate)
        {
            string sql = @" select ol.Id, ol.UserId,tu.UserName,ol.OpertionDescription,CONVERT(varchar(19),ol.OpertionDate,121) as OpertionDate ,ol.OpertionState from [T_OpertionLog] as ol inner join  [T_CF_User] as tu on tu.UserID=ol.UserId where 1=1 ";
             if (!string.IsNullOrEmpty(UserId))
            {
                sql += " and ol.UserId like'%" + UserId + "%'";
            }
            if (!string.IsNullOrEmpty(UserName))
            {
                sql += " and tu.UserName like'%" + UserName + "%'";
            }
            if (!string.IsNullOrEmpty(OpertionDes))
            {
                sql += " and ol.[OpertionDescription] like'%" + OpertionDes + "%'";
            }
            if (!string.IsNullOrEmpty(LogDate))
            {
                sql += " and CONVERT(varchar(19),ol.OpertionDate,121) like'%" + LogDate + "%'";
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("opLog");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
       /// <summary>
       /// 删除 操作日志
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public int deleteOpertionLog(string id)
        {
            string sql = "delete from  [T_OpertionLog] where ID in ('"+id+"')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
    }
}

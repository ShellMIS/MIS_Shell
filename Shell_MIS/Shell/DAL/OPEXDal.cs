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
    /// 添加时间：2014-08-12
    /// 添加目的：费用报告 字典管理
    /// </summary>
   public class OPEXDal
    {
       
       /// <summary>
       /// 插入数据
       /// </summary>
       /// <param name="plType"></param>
       /// <param name="plLine"></param>
       /// <param name="opexLine"></param>
       /// <param name="budgetOwner"></param>
       /// <param name="accountCode"></param>
       /// <param name="account_Description"></param>
       /// <param name="createdBy"></param>
       /// <param name="createdDate"></param>
       /// <param name="modifyBy"></param>
       /// <param name="modifyDate"></param>
       /// <returns></returns>
       public int OpexSettingInsert(string plType,string plLine,string opexLine,string budgetOwner,string accountCode,string account_Description)
       {
            string sql = "insert into T_OPEXSetting (PLType,PLLine,OpexLine,BudgetOwner,AccountCode,Account_Description)values('"+plType+"','"+plLine+"','"+opexLine+"','"+budgetOwner+"','"+accountCode+"','"+account_Description+"')";
           SqlDataReader dr=SqlHelp.ExecuteReader(sql);
           dr.Close();
           return 1;
       }
       /// <summary>
       /// 获取所有数据 T_OPEXSetting表中的
       /// </summary>
       /// <returns></returns>
       public DataTable OpexSettingSelect()
       {
           string sql = @" select Id,PLType,PLLine,OpexLine,BudgetOwner,AccountCode,Account_Description from T_OPEXSetting";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable table = new DataTable("opexSetting");
           table.Load(dr);
           dr.Close();
           return table;
       }
       /// <summary>
       /// 根据编号查出一条数据
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public DataTable OpexSettingSelect(int id)
       {
           string sql = @" select Id,PLType,PLLine,OpexLine,BudgetOwner,AccountCode,Account_Description from T_OPEXSetting where ID=" + id + " ";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("opexSetting");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 根据不同条件查询
       /// </summary>
       /// <param name="id"></param>
       /// <param name="plType"></param>
       /// <param name="plLine"></param>
       /// <param name="opexLine"></param>
       /// <param name="budgetOwner"></param>
       /// <param name="accountCode"></param>
       /// <param name="account_Description"></param>
       /// <param name="createdBy"></param>
       /// <param name="createdDate"></param>
       /// <param name="modifyBy"></param>
       /// <param name="modifyDate"></param>
       /// <returns></returns>
       //public DataTable OpexSettingSelect(int id,string plType,string plLine,string opexLine,string budgetOwner,string accountCode,string account_Description,string createdBy,DateTime createdDate,string modifyBy,DateTime modifyDate)
       //{ 
       public DataTable OpexSettingSelect(string plType, string plLine, string opexLine, string budgetOwner, string accountCode, string account_Description)
       {
           string sql = @" select Id,PLType,PLLine,OpexLine,BudgetOwner,AccountCode,Account_Description from T_OPEXSetting where 1=1 ";
           // if (id>0)
           //{
           //    sql += " and ID="+id+"";
           //}
           if (!string.IsNullOrEmpty(plType))
           {
               sql += " and plType like'"+plType+"'";
           }
           if (!string.IsNullOrEmpty(plLine))
           {
               sql += " and plLine like'%" + plLine + "%'";
           }
           if (!string.IsNullOrEmpty(opexLine))
           {
               sql += " and OpexLine like'%" + opexLine + "%'";
           }
           if (!string.IsNullOrEmpty(budgetOwner))
           {
               sql += " and budgetOwner like'%" + budgetOwner + "%'";
           }
           if (!string.IsNullOrEmpty(accountCode))
           {
               sql += " and AccountCode like'%" + accountCode + "%'";
           }
           if (!string.IsNullOrEmpty(account_Description))
           {
               sql += " and Account_Description like'%" + account_Description + "%'";
           }
         

           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("opexSetting");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 修改
       /// </summary>
       /// <param name="id"></param>
       /// <param name="plType"></param>
       /// <param name="plLine"></param>
       /// <param name="opexLine"></param>
       /// <param name="budgetOwner"></param>
       /// <param name="accountCode"></param>
       /// <param name="account_Description"></param>
       /// <param name="createdBy"></param>
       /// <param name="createdDate"></param>
       /// <param name="modifyBy"></param>
       /// <param name="modifyDate"></param>
       /// <returns></returns>
       public int updateOPEXSetting(int id, string plType, string plLine, string opexLine, string budgetOwner, string accountCode, string account_Description)
       {
           string sql = "update T_OPEXSetting set PLType='"+plType+"',PLLine='"+plLine+"',OpexLine='"+opexLine+"',BudgetOwner='"+budgetOwner+"' ,AccountCode='"+accountCode+"' ,Account_Description='"+account_Description+"' where ID="+id+"";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           dr.Close();
           return 1;
       }
       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int deleteOPEXSetting(int id)
       {
           string sql = "delete from T_OPEXSetting where ID="+id+"";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           dr.Close();
           return 1;
       }

    }
}

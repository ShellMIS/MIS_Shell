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
   public class Pldb
    {
       /// <summary>
       /// 插入数据
       /// </summary>
       /// <param name="item"></param>
       /// <returns></returns>
      
      // public int PlDbInsert(string item,int id)
       public int PlDbInsert(int id, string item)
       {
           //string sql = "insert into PLDB values ("+id+",'"+item+"')";
           string sql = "insert into PLDB values ("+id+",'" + item + "')";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           dr.Close();
           return 1;
       }
       /// <summary>
       /// 查询所有数据PLDB中的
       /// </summary>
       /// <returns></returns>
       public DataTable PlDbSelect()
       {
           string sql = " select * from PLDB";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("PLDBSetting");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 根据不同的查询条件获取数据
       /// </summary>
       /// <param name="plDb_id"></param>
       /// <param name="PlDb_Item"></param>
       /// <returns></returns>
       public DataTable PlDbSelect(string plDb_id, string PlDb_Item)
       {
           string sql = @" select * from PLDB where 1=1 ";
           if (!string.IsNullOrEmpty(plDb_id))
           {
               sql += " and PLDB_Id like '%"+plDb_id+"%'";
           }
           if(!string .IsNullOrEmpty(PlDb_Item))
           {
               sql += " and PIDB_Item like'%"+PlDb_Item+"%'";
           }

           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("PLDBSetting");
           dt.Load(dr);
           dr.Close();
           return dt;

       }
       /// <summary>
       /// 根据编号获取数据
       /// </summary>
       /// <param name="PlDb_Id"></param>
       /// <returns></returns>
       public DataTable PlDbSelect(int PlDb_Id)
       {
           string sql = "select * from PLDB where PLDB_Id="+PlDb_Id+"";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           DataTable dt = new DataTable("PLDBSetting");
           dt.Load(dr);
           dr.Close();
           return dt;
       }
       /// <summary>
       /// 根据编号删除一条数据
       /// </summary>
       /// <param name="PlDb_Id"></param>
       /// <returns></returns>
       public int PlDbDelete(int PlDb_Id)
       {
           string sql = "delete from PLDB where PLDB_Id="+PlDb_Id+"";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           dr.Close();
           return 1;
       }

       public int PLDBUpdate(int PLDB_id,string PLDB_item)
       {
           string sql = @"update PLDB set PIDB_Item='"+PLDB_item+"' where PLDB_Id="+PLDB_id+"";
           SqlDataReader dr = SqlHelp.ExecuteReader(sql);
           dr.Close();
           return 1;
       }
    }
}

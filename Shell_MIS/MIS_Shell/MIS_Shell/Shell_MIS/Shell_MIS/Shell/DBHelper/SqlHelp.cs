using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DBHelper
{
    public class SqlHelp
    {
        #region 通用方法

        // 数据连接池
        private SqlConnection con;
        /// <summary>
        /// 返回数据库连接字符串
        /// </summary>
        /// <returns></returns>
        //public static string connStr = "Data Source=.;Initial Catalog=Shell-MIS;User ID=sa;Password=12345678";
        //public static readonly string connectionString = ConfigurationManager.ConnectionStrings["dbcn"].ConnectionString;
        public static String GetSqlConnection()
        {
            // String conn = "Data Source=.;Initial Catalog=Shell-MIS;User Id=sa;Password=12345678;";
            //  String conn = "Data Source=SHELL-A918E8DCD\\ORCL;Initial Catalog=Shell-MIS;User Id=sa;Password=12345678;MultipleActiveResultSets=true;";
           String conn = "Data Source=.;Initial Catalog=Shell-MIS;pwd=12345678;User Id=sa;";
        //    String conn = "Data Source=.;Initial Catalog=Shell-MIS;pwd=12345678;User Id=sa; Pooling=true;MAX Pool Size=1024;Min Pool Size=1;Connection Lifetime=60";
          
            return conn;
        }
        #endregion
        #region 执行sql字符串
        /// <summary>
        /// 将datatable中的数据批量插入数据库
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="TabelName"></param>
        /// <param name="dtColum"></param>
        // public void InsertTable(DataTable dt, string TabelName, DataColumnCollection dtColum)
        public static void InsertTable(DataTable dt, string TabelName)
        {
            SqlConnection conn = new SqlConnection(GetSqlConnection());
            conn.Open();
            //声明SqlBulkCopy ,using释放非托管资源
            using (SqlBulkCopy sqlBC = new SqlBulkCopy(conn))
            {
                //一次批量的插入的数据量
                sqlBC.BatchSize = 1000;
                //超时之前操作完成所允许的秒数，如果超时则事务不会提交 ，数据将回滚，所有已复制的行都会从目标表中移除
                sqlBC.BulkCopyTimeout = 60;

                //設定 NotifyAfter 属性，以便在每插入10000 条数据时，呼叫相应事件。 
                sqlBC.NotifyAfter = 10000;
                // sqlBC.SqlRowsCopied += new SqlRowsCopiedEventHandler(OnSqlRowsCopied);

                //设置要批量写入的表  数据库中的表
                sqlBC.DestinationTableName = TabelName;
                //自定义的datatable和数据库的字段进行对应
                //sqlBC.ColumnMappings.Add("id", "tel");
                //sqlBC.ColumnMappings.Add("name", "neirong");
                //for (int i = 0; i < dtColum.Count; i++)
                //{
                //    sqlBC.ColumnMappings.Add(dt.Columns[i].ColumnName.ToString(), dtColum[i].ColumnName.ToString());
                //}
                //批量写入
                sqlBC.WriteToServer(dt);
            }
            conn.Dispose();

        }
        /// <summary>
        /// 添加人：ydx
        /// 添加目的：插入一条数据的同时返回主键值 ydx
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int sqlexecutereader(string sql)
        {
            int newID = 0;
            String ConnStr = GetSqlConnection();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                SqlCommand myComm = new SqlCommand(sql, conn);
                conn.Open();
                newID = Convert.ToInt32(myComm.ExecuteScalar());
                conn.Close();
            }
            return newID;
        }
        /// <summary>
        /// 执行不带参数的SQL语句 
        /// </summary>
        /// <param name="Sqlstr"></param>
        /// <returns></returns>
        public static int ExecuteSql(String Sqlstr)
        {
            String ConnStr = GetSqlConnection();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = Sqlstr;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return 1;
            }
        }
        /// <summary>
        /// 执行带参数的SQL语句
        /// </summary>
        /// <param name="Sqlstr">SQL语句</param>
        /// <param name="param">参数对象数组</param>
        /// <returns></returns>
        public static int ExecuteSql(String Sqlstr, SqlParameter[] param)
        {
            String ConnStr = GetSqlConnection();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = Sqlstr;
                cmd.Parameters.AddRange(param);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return 1;
            }
        }
        /// <summary>
        /// 返回DataReader
        /// </summary>
        /// <param name="Sqlstr"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(String Sqlstr)
        {
            String ConnStr = GetSqlConnection();
            SqlConnection conn = new SqlConnection(ConnStr);//返回DataReader时，是不可以用using()的
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 180;
                cmd.Connection = conn;
                cmd.CommandText = Sqlstr;
                conn.Open();
                return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);//关闭关联的Connection
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 执行SQL语句并返回数据表
        /// </summary>
        /// <param name="Sqlstr">SQL语句</param>
        /// <returns></returns>
        public static DataTable ExecuteDt(String Sqlstr)
        {
            String ConnStr = GetSqlConnection();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(Sqlstr, conn))
                {
                    DataTable dt = new DataTable();
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                    return dt;
                }
            }
        }
        /// <summary> 
        /// 执行SQL语句并返回DataSet 
        /// </summary> 
        /// <param name="Sqlstr">SQL语句</param> 
        /// <returns></returns> 
        public static DataSet ExecuteDs(String Sqlstr)
        {
            String ConnStr = GetSqlConnection();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(Sqlstr, conn))
                {
                    DataSet ds = new DataSet();
                    conn.Open();
                    da.Fill(ds);
                    conn.Close();
                    return ds;
                }
            }
        }
        #endregion
        #region 操作存储过程
        /// <summary>
        /// 运行存储过程（已重载）
        /// </summary>
        /// <param name="procName">存储过程名字</param>
        /// <returns>存储过程返回值</returns>
        public int RunProc(string procName)
        {
            SqlCommand cmd = CreateCommand(procName, null);
            cmd.ExecuteNonQuery();
            this.Close();
            return (int)cmd.Parameters["ReturnValue"].Value;
        }
        /// <summary>
        /// 运行存储过程(已重载) 
        /// </summary>
        /// <param name="procName">存储过程的名字</param>
        /// <param name="prams">存储过程的输入参数列表</param>
        /// <returns>存储过程的返回值</returns>
        public int RunProc(string procName, SqlParameter[] prams)
        {
            SqlCommand cmd = CreateCommand(procName, prams);
            cmd.ExecuteNonQuery();
            this.Close();
            return (int)cmd.Parameters[0].Value;
        }
        /// <summary>
        /// 运行存储过程（已重载）
        /// </summary>
        /// <param name="procName">存储过程的名字</param>
        /// <param name="dataReader">结果集</param>
        public void RunProc(string procName, out SqlDataReader dataReader)
        {
            SqlCommand cmd = CreateCommand(procName, null);
            //dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            dataReader = cmd.ExecuteReader();


        }
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-09-23
        /// 修改目的：执行存储过程带输入参数，并返回结果集
        /// 运行存储过程（已重载）
        /// </summary>
        /// <param name="procName">存储过程的名字</param>
        /// <param name="prams">存储过程的输入参数列表</param>
        /// <param name="dataReader">结果集</param>
        public void RunProc(string procName, SqlParameter[] prams, out SqlDataReader dataReader)
        {
            SqlCommand cmd = CreateCommand(procName, prams);
            cmd.CommandTimeout = 999;
            //dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            dataReader = cmd.ExecuteReader();
        }
        /// <summary>
        /// 黄晓艳添加的重载方法
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="prams"></param>
        /// <param name="dataReader"></param>
        public SqlDataReader RunProcT2(string procName, SqlParameter[] prams)
        {
            SqlCommand cmd = CreateCommand(procName, prams);
            cmd.CommandTimeout = 999;
            //dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            SqlDataReader dataReader = cmd.ExecuteReader();
            return dataReader;
        }

        /// <summary> 
        /// 创建Command对象用于访问存储过程 
        /// </summary> 
        /// <param name="procName">存储过程的名字</param> 
        /// <param name="prams">存储过程的输入参数列表</param> 
        /// <returns>Command对象</returns> 
        private SqlCommand CreateCommand(string procName, SqlParameter[] prams)
        {
            // 确定连接是打开的 
            Open();
            //command = new SqlCommand( sprocName, new SqlConnection( ConfigManager.DALConnectionString ) ); 
            SqlCommand cmd = new SqlCommand(procName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // 添加存储过程的输入参数列表 
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                    cmd.Parameters.Add(parameter);
            }
            // 返回Command对象 
            return cmd;
        }
        /// <summary> 
        /// 创建输入参数 
        /// </summary> 
        /// <param name="ParamName">参数名</param> 
        /// <param name="DbType">参数类型</param> 
        /// <param name="Size">参数大小</param> 
        /// <param name="Value">参数值</param> 
        /// <returns>新参数对象</returns> 
        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }
        /// <summary> 
        /// 创建输出参数 
        /// </summary> 
        /// <param name="ParamName">参数名</param> 
        /// <param name="DbType">参数类型</param> 
        /// <param name="Size">参数大小</param> 
        /// <returns>新参数对象</returns> 
        public SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }
        /// <summary> 
        /// 创建存储过程参数 
        /// </summary> 
        /// <param name="ParamName">参数名</param> 
        /// <param name="DbType">参数类型</param> 
        /// <param name="Size">参数大小</param> 
        /// <param name="Direction">参数的方向(输入/输出)</param> 
        /// <param name="Value">参数值</param> 
        /// <returns>新参数对象</returns> 
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;
            if (Size > 0)
            {
                param = new SqlParameter(ParamName, DbType, Size);
            }
            else
            {
                param = new SqlParameter(ParamName, DbType);
            }
            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
            {
                param.Value = Value;
            }
            return param;
        }
        #endregion
        #region 数据库连接和关闭
        /// <summary> 
        /// 打开连接池 
        /// </summary> 
        private void Open()
        {
            // 打开连接池 
            if (con == null)
            {
                //这里不仅需要using System.Configuration;还要在引用目录里添加 
                con = new SqlConnection(GetSqlConnection());
                con.Open();
            }
        }
        /// <summary> 
        /// 关闭连接池 
        /// </summary> 
        public void Close()
        {
            if (con != null)
                con.Close();
        }
        /// <summary> 
        /// 释放连接池 
        /// </summary> 
        public void Dispose()
        {
            // 确定连接已关闭 
            if (con != null)
            {
                con.Dispose();
                con = null;
            }
        }
        #endregion
        /// <summary>
        /// 黄晓艳添加执行增删改的方法
        /// </summary>
        /// <param name="Sqlstr"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(String Sqlstr)
        {
            int result = 0;
            String ConnStr = GetSqlConnection();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = Sqlstr;
                conn.Open();
                result = (int)cmd.ExecuteScalar();
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="sql">执行的sql</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql, params SqlParameter[] param)
        {
            using (SqlConnection conn = new SqlConnection(GetSqlConnection()))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sql, conn))
                {
                    if (param != null)
                    {
                        sda.SelectCommand.Parameters.AddRange(param);
                    }
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] param)
        {
            object result;
            using (SqlConnection conn = new SqlConnection(GetSqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    conn.Open();
                    result = cmd.ExecuteScalar();
                    return result;
                }
            }

        }
        /// <summary>
        /// 获取sqldatareader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql, params SqlParameter[] param)
        {
            using (SqlConnection conn = new SqlConnection(GetSqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    //cmd.Connection = conn;
                    //cmd.CommandText = Sqlstr;
                    //conn.Open();
                    //return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);//关闭关联的Connection

                    conn.Open();
                    cmd.Parameters.AddRange(param);
                    //conn.Close();
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }
            }




        }

        public DataTable ExecuteQuery(string sql, SqlParameter[] paras, CommandType ct)
        {
            using (SqlConnection conn = new SqlConnection(GetSqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DataTable dt = new DataTable();
                    cmd.Parameters.AddRange(paras);
                    conn.Open();
                    cmd.CommandType = ct; using (SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        dt.Load(sdr);
                    }
                    conn.Close();
                    return dt;
                }
            }
        }

    }
}

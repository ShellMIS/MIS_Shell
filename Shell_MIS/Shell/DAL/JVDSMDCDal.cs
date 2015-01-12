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
    public class JVDSMDCDal
    {
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-27
        /// 添加目的：根据公司代码查出此公司下的所有部门
        /// </summary>
        /// <param name="cocd"></param>
        /// <returns></returns>
        public DataTable getT5CodeByCocd(string cocd)
        {
            string sql = "select dep.DeptNameCH,dep.T5Code  from JVDSMDC dep where   dep.CoCd='"+cocd+"'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("jvdep");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-27
        /// 添加目的：根据公司代码查出此公司下的所有部门
        /// </summary>
        /// <param name="cocd"></param>
        /// <returns></returns>
        public DataTable getT5AndJvCodeByCocd(string cocd)
        {
            string sql = "select dep.DeptNameCH,dep.T5Code  from JVDSMDC dep where  dep.CoCd='" + cocd + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("jvanddep");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 插入部门表
        /// </summary>
        /// <param name="CoCd"></param>
        /// <param name="CoNameCH"></param>
        /// <param name="CoNameEN"></param>
        /// <param name="CoGroup1"></param>
        /// <returns></returns>
        //public int JVDSMDCInsert(string DeptNameCH, string DeptNamePinyin, string Nature, string HSC, string CoCd, string T0Code, string T5Code, string T3Code, string SiteOpenDate, string SiteAging,
        //    string SiteStatus, string TMCode, string Acquired, string Location, string CRType, string InvestmentType, string District)
        //{
        //    string sql = @"insert into JVDSMDC values('" + DeptNameCH + "','" + DeptNamePinyin + "','" + Nature + "','" + HSC + "','"
        //        + CoCd + "','" + T0Code + "','" + T5Code + "','" + T3Code + "','" + SiteOpenDate + "','" + SiteAging + "','" + SiteStatus + "','" + TMCode + "','"
        //        + Acquired + "','" + Location + "','" + CRType + "','" + InvestmentType + "','" + District + "') select @@identity;";
        //    return SqlHelp.sqlexecutereader(sql);
        //}
        

        /// <summary>
        /// sql注入
        /// </summary>
        /// <param name="DeptNameCH"></param>
        /// <param name="DeptNamePinyin"></param>
        /// <param name="Nature"></param>
        /// <param name="HSC"></param>
        /// <param name="CoCd"></param>
        /// <param name="T0Code"></param>
        /// <param name="T5Code"></param>
        /// <param name="T3Code"></param>
        /// <param name="SiteOpenDate"></param>
        /// <param name="SiteAging"></param>
        /// <param name="SiteStatus"></param>
        /// <param name="TMCode"></param>
        /// <param name="Acquired"></param>
        /// <param name="Location"></param>
        /// <param name="CRType"></param>
        /// <param name="InvestmentType"></param>
        /// <param name="District"></param>
        /// <returns></returns>
  public int JVDSMDCInsert(string DeptNameCH, string DeptNamePinyin, string Nature, string HSC, string CoCd, string T0Code, string T5Code, string T3Code, string SiteOpenDate, string SiteAging,
            string SiteStatus, string TMCode, string Acquired, string Location, string CRType, string InvestmentType, string District)
        {
            //string sql = @"insert into JVDSMDC values('" + DeptNameCH + "','" + DeptNamePinyin + "','" + Nature + "','" + HSC + "','"
            //    + CoCd + "','" + T0Code + "','" + T5Code + "','" + T3Code + "','" + SiteOpenDate + "','" + SiteAging + "','" + SiteStatus + "','" + TMCode + "','"
            //    + Acquired + "','" + Location + "','" + CRType + "','" + InvestmentType + "','" + District + "') select @@identity;";
            //return SqlHelp.sqlexecutereader(sql);
            int result = 0;
            string strSql = "insert into JVDSMDC";
            strSql += " values (";
            strSql += "@DeptNameCH,@DeptNamePinyin,@Nature,@HSC,@CoCd,@T0Code,@T5Code,@T3Code,@SiteOpenDate,@SiteAging,@SiteStatus,@TMCode,@Acquired,@Location,@CRType,@InvestmentType,@District)";

            SqlParameter[] parameters = {
             new SqlParameter("@DeptNameCH",DeptNameCH),
             new SqlParameter("@DeptNamePinyin",DeptNamePinyin),
             new SqlParameter("@Nature",Nature),
             new SqlParameter("@HSC",HSC),
             new SqlParameter("@Cocd",CoCd),
             new SqlParameter("@T0Code",T0Code),
             new SqlParameter("@T5Code",T5Code),
             new SqlParameter("@T3Code",T3Code),
             new SqlParameter("@SiteOpenDate",SiteOpenDate),
             new SqlParameter("@SiteAging",SiteAging),
             new SqlParameter("@SiteStatus",SiteStatus),
             new SqlParameter("@TMCode",TMCode),
             new SqlParameter("@Acquired",Acquired),
             new SqlParameter("@Location",Location),
             new SqlParameter("@CRType",CRType),
             new SqlParameter("@InvestmentType",InvestmentType),
             new SqlParameter("@District",District)};
            strSql += " select @@identity;";
            object c = SqlHelp.ExecuteScalar(strSql, parameters);
            result = int.Parse(c.ToString());
            return result;
        }


        public int JVDSMDC_TempInsert(string DeptNameCH, string DeptNamePinyin, string Nature, string HSC, string CoCd, string T0Code, string T5Code, string T3Code, string SiteOpenDate, string SiteAging,
            string SiteStatus, string TMCode, string Acquired, string Location, string CRType, string InvestmentType, string District)
        {
            int result = 0;
            string strSql = "insert into JVDSMDC";
            strSql += " values (";
            strSql += "@DeptNameCH,@DeptNamePinyin,@Nature,@HSC,@CoCd,@T0Code,@T5Code,@T3Code,@SiteOpenDate,@SiteAging,@SiteStatus,@TMCode,@Acquired,@Location,@CRType,@InvestmentType,@District)";

            SqlParameter[] parameters = {
             new SqlParameter("@DeptNameCH",DeptNameCH),
             new SqlParameter("@DeptNamePinyin",DeptNamePinyin),
             new SqlParameter("@Nature",Nature),
             new SqlParameter("@HSC",HSC),
             new SqlParameter("@Cocd",CoCd),
             new SqlParameter("@T0Code",T0Code),
             new SqlParameter("@T5Code",T5Code),
             new SqlParameter("@T3Code",T3Code),
             new SqlParameter("@SiteOpenDate",SiteOpenDate),
             new SqlParameter("@SiteAging",SiteAging),
             new SqlParameter("@SiteStatus",SiteStatus),
             new SqlParameter("@TMCode",TMCode),
             new SqlParameter("@Acquired",Acquired),
             new SqlParameter("@Location",Location),
             new SqlParameter("@CRType",CRType),
             new SqlParameter("@InvestmentType",InvestmentType),
             new SqlParameter("@District",District)};
            strSql += " select @@identity;";
            object c = SqlHelp.ExecuteScalar(strSql, parameters);
            result = int.Parse(c.ToString());
            return result;
            //string sql = @"insert into JVDSMDC_Temp values('" + DeptNameCH + "','" + DeptNamePinyin + "','" + Nature + "','" + HSC + "','"
            //    + CoCd + "','" + T0Code + "','" + T5Code + "','" + T3Code + "','" + SiteOpenDate + "','" + SiteAging + "','" + SiteStatus + "','" + TMCode + "','"
            //    + Acquired + "','" + Location + "','" + CRType + "','" + InvestmentType + "','" + District + "') select @@identity;";
            //return SqlHelp.sqlexecutereader(sql);
        }
        /// <summary>
        /// 查询所有数据库里的内容
        /// </summary>
        /// <returns></returns>
        public DataTable JVDSMDCSelect()
        {
            string sql = "select * from JVDSMDC";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("JVDSMDC");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// hxy sql 注入
        /// </summary>
        /// <param name="DeptNameCH"></param>
        /// <param name="DeptNamePinyin"></param>
        /// <param name="Nature"></param>
        /// <param name="HSC"></param>
        /// <param name="CoCd"></param>
        /// <param name="T0Code"></param>
        /// <param name="T3Code"></param>
        /// <param name="T5Code"></param>
        /// <param name="SiteOpenDate"></param>
        /// <param name="SiteAging"></param>
        /// <param name="SiteStatus"></param>
        /// <param name="TMCode"></param>
        /// <param name="Acquired"></param>
        /// <param name="Location"></param>
        /// <param name="CRType"></param>
        /// <param name="InvestmentType"></param>
        /// <param name="District"></param>
        /// <param name="PageWhere"></param>
        /// <returns></returns>

        public DataTable JVDSMDCSelect(string DeptNameCH, string DeptNamePinyin, string Nature, string HSC, string CoCd, string T0Code, string T3Code, string T5Code, string SiteOpenDate, string SiteAging,
         string SiteStatus, string TMCode, string Acquired, string Location, string CRType, string InvestmentType, string District)
        {
            SqlParameter[] parameters = new SqlParameter[17];
            string sql = @"select * from (select ROW_NUMBER() over( order by id) 序号,* from JVDSMDC  where 1=1 ";
            using (SqlConnection conn = new SqlConnection(SqlHelp.GetSqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    #region 参数
                    if (!string.IsNullOrEmpty(DeptNameCH))
                    {
                        sql += " and DeptNameCH like @DeptNameCH ";
                        SqlParameter sp1 = new SqlParameter("@DeptNameCH", DeptNameCH);
                        cmd.Parameters.Add(sp1);
                    }
                    if (!string.IsNullOrEmpty(DeptNamePinyin))
                    {
                        sql += " and DeptNamePinyin like @DeptNamePinyin";
                        SqlParameter sp2 = new SqlParameter("@DeptNamePinyin", DeptNamePinyin);
                        cmd.Parameters.Add(sp2);
                    }
                    if (!string.IsNullOrEmpty(Nature))
                    {
                        sql += " and Nature like  @Nature";
                        SqlParameter sp3 = new SqlParameter("@Nature", Nature);
                        cmd.Parameters.Add(sp3);
                    }
                    if (!string.IsNullOrEmpty(HSC))
                    {
                        sql += " and HSC like @HSC ";
                        SqlParameter sp4 = new SqlParameter("@HSC", HSC);
                        cmd.Parameters.Add(sp4);
                    }
                    if (!string.IsNullOrEmpty(CoCd))
                    {
                        sql += " and CoCd like @Cocd";
                        SqlParameter sp3 = new SqlParameter("@CoCd", CoCd);
                        cmd.Parameters.Add(sp3);
                    }
                    if (!string.IsNullOrEmpty(T0Code))
                    {
                        sql += " and T0Code like @T0Code";
                        SqlParameter sp3 = new SqlParameter("@T0Code", T0Code);
                        cmd.Parameters.Add(sp3);
                    }
                    if (!string.IsNullOrEmpty(T5Code))
                    {
                        sql += " and T5Code like @T5Code";
                        SqlParameter sp3 = new SqlParameter("@T5Code", T5Code);
                        cmd.Parameters.Add(sp3);
                    }
                    if (!string.IsNullOrEmpty(T3Code))
                    {
                        sql += " and T3Code like @T3Code";
                        SqlParameter sp3 = new SqlParameter("@T3Code", T3Code);
                        cmd.Parameters.Add(sp3);
                    }
                    if (!string.IsNullOrEmpty(SiteOpenDate))
                    {
                        sql += " and SiteOpenDate like @SiteOpenDate";
                        SqlParameter sp3 = new SqlParameter("@SiteOpenDate", SiteOpenDate);
                        cmd.Parameters.Add(sp3);
                    }
                    if (!string.IsNullOrEmpty(SiteAging))
                    {
                        sql += " and SiteAging like  @SiteAging";
                        SqlParameter sp3 = new SqlParameter("@SiteAging", SiteAging);
                        cmd.Parameters.Add(sp3);
                    }
                    if (!string.IsNullOrEmpty(SiteStatus))
                    {
                        sql += " and SiteStatus like @SiteStatus";
                        SqlParameter sp3 = new SqlParameter("@SiteStatus", SiteStatus);
                        cmd.Parameters.Add(sp3);
                    }
                    if (!string.IsNullOrEmpty(TMCode))
                    {
                        sql += " and TMCode like @TMCode";
                        SqlParameter sp3 = new SqlParameter("@TMCode", TMCode);
                        cmd.Parameters.Add(sp3);
                    }
                    if (!string.IsNullOrEmpty(Acquired))
                    {
                        sql += " and Acquired like @Acquired";
                        SqlParameter sp3 = new SqlParameter("@Acquired", Acquired);
                        cmd.Parameters.Add(sp3);
                    }
                    if (!string.IsNullOrEmpty(Location))
                    {
                        sql += " and Location like @Location";
                        SqlParameter sp3 = new SqlParameter("@Location", Location);
                        cmd.Parameters.Add(sp3);
                    }
                    if (!string.IsNullOrEmpty(CRType))
                    {
                        sql += " and CRType like @CRType";
                        SqlParameter sp3 = new SqlParameter("@CRType", CRType);
                        cmd.Parameters.Add(sp3);
                    }
                    if (!string.IsNullOrEmpty(InvestmentType))
                    {
                        sql += " and InvestmentType like @InvestmentType";
                        SqlParameter sp3 = new SqlParameter("@InvestmentType", InvestmentType);
                        cmd.Parameters.Add(sp3);
                    }
                    if (!string.IsNullOrEmpty(District))
                    {
                        sql += " and District like @District";
                        SqlParameter sp3 = new SqlParameter("@District", District);
                        cmd.Parameters.Add(sp3);
                    }
                    sql += " )  as bb";
                
                    #endregion
                    cmd.CommandText = sql;
                    SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    DataTable dt = new DataTable("JVDSMDC");
                    dt.Load(dr);
                    dr.Close();
                    return dt;
                }
            }
        }
        //public DataTable JVDSMDCSelect(string DeptNameCH, string DeptNamePinyin, string Nature, string HSC, string CoCd, string T0Code, string T3Code, string T5Code, string SiteOpenDate, string SiteAging,
        //    string SiteStatus, string TMCode, string Acquired, string Location, string CRType, string InvestmentType, string District)
        //{
        //    string sql = @"select * from JVDSMDC where 1=1 ";
        //    if (!string.IsNullOrEmpty(DeptNameCH))
        //    {
        //        sql += " and DeptNameCH like '%" + DeptNameCH + "%' ";
        //    }
        //    if (!string.IsNullOrEmpty(DeptNamePinyin))
        //    {
        //        sql += " and DeptNamePinyin like '%" + DeptNamePinyin + "%' ";
        //    }
        //    if (!string.IsNullOrEmpty(Nature))
        //    {
        //        sql += " and Nature like '%" + Nature + "%' ";
        //    }
        //    if (!string.IsNullOrEmpty(HSC))
        //    {
        //        sql += " and HSC like '%" + HSC + "%' ";
        //    }
        //    if (!string.IsNullOrEmpty(CoCd))
        //    {
        //        sql += " and CoCd like '%" + CoCd + "%' ";
        //    }
        //    if (!string.IsNullOrEmpty(T0Code))
        //    {
        //        sql += " and T0Code like '%" + T0Code + "%' ";
        //    }
        //    if (!string.IsNullOrEmpty(T5Code))
        //    {
        //        sql += " and T5Code like '%" + T5Code + "%' ";
        //    }
        //    if (!string.IsNullOrEmpty(T3Code))
        //    {
        //        sql += "and T3Code like '%" + T3Code + "%'";
        //    }
        //    if (!string.IsNullOrEmpty(SiteOpenDate))
        //    {
        //        sql += "and SiteOpenDate like '%" + SiteOpenDate + "%'";
        //    }
        //    if (!string.IsNullOrEmpty(SiteAging))
        //    {
        //        sql += "and SiteAging like '%" + SiteAging + "%'";
        //    }
        //    if (!string.IsNullOrEmpty(SiteStatus))
        //    {
        //        sql += "and SiteStatus like '%" + SiteStatus + "%'";
        //    }
        //    if (!string.IsNullOrEmpty(TMCode))
        //    {
        //        sql += "and TMCode like '%" + TMCode + "%'";
        //    }
        //    if (!string.IsNullOrEmpty(Acquired))
        //    {
        //        sql += "and Acquired like '%" + Acquired + "%'";
        //    }
        //    if (!string.IsNullOrEmpty(Location))
        //    {
        //        sql += "and Location like '%" + Location + "%'";
        //    }
        //    if (!string.IsNullOrEmpty(CRType))
        //    {
        //        sql += "and CRType like '%" + CRType + "%'";
        //    }
        //    if (!string.IsNullOrEmpty(InvestmentType))
        //    {
        //        sql += "and InvestmentType like '%" + InvestmentType + "%'";
        //    }
        //    if (!string.IsNullOrEmpty(District))
        //    {
        //        sql += "and District like '%" + District + "%'";
        //    }
        //    SqlDataReader dr = SqlHelp.ExecuteReader(sql);
        //    DataTable dt = new DataTable("JVDSMDC");
        //    dt.Load(dr);
        //    dr.Close();
        //    return dt;
        //}




        public DataTable JVDSMDCSelect(int ID)
        {
            string sql = "select * from JVDSMDC where id='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("JVDSMDC");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 获取T3code
        /// </summary>
        /// <param name="CoCd"></param>
        /// <returns></returns>
        public DataTable JVSelectCompany(string CoCd)
        {
            string sql = @"select T3Code from JVDSMDC where CoCd='" + CoCd + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("JVSetting");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //public DataTable JVSelectCompany(string CoCd)
        //{
        //    string sql = @"select * from JVDSMDC where CoCd='" + CoCd + "'";
        //    SqlDataReader dr = SqlHelp.ExecuteReader(sql);
        //    DataTable dt = new DataTable("JVSetting");
        //    dt.Load(dr);
        //    dr.Close();
        //    return dt;
        //}
        /// <summary>
        /// 根据ID做删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int JVDSMDCDelete(string id)
        {
            string sql = "delete from JVDSMDC where ID ='" + id + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 修改
        /// </summary>
        public int JVDSMDCUpdate(int ID,string DeptNameCH, string DeptNamePinyin, string Nature, string HSC, string CoCd, string T0Code, string T3Code, string T5Code, string SiteOpenDate, string SiteAging,
           string SiteStatus, string TMCode, string Acquired, string Location, string CRType, string InvestmentType, string District)
        {
            string sql = @"UPDATE JVDSMDC SET DeptNameCH = '" + DeptNameCH + "',DeptNamePinyin = '" + DeptNamePinyin + "' ,Nature = '" + Nature + "' ,HSC = '" + HSC
                    + "',CoCd='" + CoCd + "', T0Code='" + T0Code + "', T3Code='" + T3Code + "',T5Code='" + T5Code + "',SiteOpenDate='" + SiteOpenDate + "',SiteAging='" + SiteAging + "',SiteStatus='" + SiteStatus
                    + "',TMCode='" + TMCode + "',Acquired='" + Acquired + "',Location='" + Location + "',CRType='" + CRType + "',InvestmentType='" + InvestmentType + "',District='" + District
                    + "' WHERE  ID='" + ID + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }

        /// <summary>
        /// 黄晓艳 cocd下拉框绑定
        /// </summary>
        /// <returns></returns>
        public DataTable JVSelectCocd()
        {
            string sql = "select (CoCd+' '+CoNameEN)as cdNameEn,CoCd  from JVSetting where CoCd<>'' ";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("COCD");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
    }
}

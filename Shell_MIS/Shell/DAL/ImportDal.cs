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
    public class ImportDal
    {

        public DBHelper.SqlHelp sqlHelp = new SqlHelp();
        //增加
        public int ImportInsert(string AccountCode, string AccountPeriod, DateTime TransactionDate, string RecordType, string JournalNumber, string LineNumber, double Amount, string DCMarker,
            string AllocationIndicator, string JournalType, string JournalSource, string TransactionReference, string EntryDate, string EntryPeriod, string DueDate, string PaymentAllocationRef,
            string PaymentAllocationDate, string PaymentAllocationPeriod, string AssetIndicator, string AssetCode, string AssetSub_Code, string Conversion_Code, string ConversionRate, string OtherAmount,
            string OtherAmountDecimalPlaces, string CleardownSequenceNumber, string NextPeriodReversal, string LossorGain, string RoughBookFlag, string InUseFlag, string T0, string T1, string T2, string T3,
            string T4, string T5, string T6, string T7, string T8, string T9, DateTime PostingDate)
        {
            string sql = @"insert into T_imp_raw_tmp ([AccountCode],[AccountPeriod],[TransactionDate],[RecordType],[JournalNumber],[LineNumber],[Amount],[DCMarker],[AllocationIndicator],[JournalType]
           ,[JournalSource],[TransactionReference],[EntryDate],[EntryPeriod],[DueDate],[PaymentAllocationRef],[PaymentAllocationDate],[PaymentAllocationPeriod],[AssetIndicator],[AssetCode]
           ,[AssetSub_Code],[Conversion_Code],[ConversionRate],[OtherAmount],[OtherAmountDecimalPlaces],[CleardownSequenceNumber],[NextPeriodReversal],[LossorGain],[RoughBookFlag],[InUseFlag]
           ,[T0],[T1],[T2],[T3],[T4],[T5],[T6],[T7],[T8],[T9],[PostingDate])
            values('" + AccountCode + "','" + AccountPeriod + "','" + TransactionDate + "','" + RecordType + "','" + JournalNumber + "','" + LineNumber
                + "','" + Amount + "','" + DCMarker + "','" + AllocationIndicator + "','" + JournalType + "','" + JournalSource + "','" + TransactionReference + "','" + EntryDate + "','" + EntryPeriod
                + "','" + DueDate + "','" + PaymentAllocationRef + "','" + PaymentAllocationDate + "','" + PaymentAllocationPeriod + "','" + AssetIndicator + "','" + AssetCode + "','" + AssetSub_Code
                + "','" + Conversion_Code + "','" + ConversionRate + "','" + OtherAmount + "','" + OtherAmountDecimalPlaces + "','" + CleardownSequenceNumber + "','" + NextPeriodReversal
                + "','" + LossorGain + "','" + RoughBookFlag + "','" + InUseFlag + "','" + T0 + "','" + T1 + "','" + T2 + "','" + T3 + "','" + T4 + "','" + T5 + "','" + T6 + "','" + T7 + "','" + T8 + "','" + T9
                + "','" + PostingDate + "')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        //校验后增加
        public int ImportCheckInsert(string AccountCodeS, string AccountCodeD, string AccountPeriod, DateTime TransactionDate, string RecordType, string JournalNumber, string LineNumber, double Amount,
            string DCMarker, string AllocationIndicator, string JournalType, string JournalSource, string TransactionReference, string EntryDate, string EntryPeriod, string DueDate,
            string PaymentAllocationRef, string PaymentAllocationDate, string PaymentAllocationPeriod, string AssetIndicator, string AssetCode, string AssetSub_Code, string Conversion_Code,
            string ConversionRate, string OtherAmount, string OtherAmountDecimalPlaces, string CleardownSequenceNumber, string NextPeriodReversal, string LossorGain, string RoughBookFlag,
            string InUseFlag, string T0, string T1, string T2, string T3, string T4, string T5, string T6, string T7, string T8, string T9, DateTime PostingDate, string CoCd)
        {
            string sql = @"INSERT INTO [Shell-MIS].[dbo].[T_imp_raw]
           ([AccountCodeS],[AccountCodeD],[AccountPeriod],[TransactionDate],[RecordType],[JournalNumber],[LineNumber],[Amount],[DCMarker],[AllocationIndicator],[JournalType],[JournalSource]
           ,[TransactionReference],[EntryDate],[EntryPeriod],[DueDate],[PaymentAllocationRef],[PaymentAllocationDate],[PaymentAllocationPeriod],[AssetIndicator],[AssetCode]
           ,[AssetSub_Code],[Conversion_Code],[ConversionRate],[OtherAmount],[OtherAmountDecimalPlaces],[CleardownSequenceNumber],[NextPeriodReversal],[LossorGain],[RoughBookFlag]
           ,[InUseFlag],[T0],[T1],[T2],[T3],[T4],[T5],[T6],[T7],[T8],[T9],[PostingDate],[CoCd])
            values('" + AccountCodeS + "','" + AccountCodeD + "','" + AccountPeriod + "','" + TransactionDate + "','" + RecordType + "','" + JournalNumber + "','" + LineNumber
                + "','" + Amount + "','" + DCMarker + "','" + AllocationIndicator + "','" + JournalType + "','" + JournalSource + "','" + TransactionReference + "','" + EntryDate + "','"
                + EntryPeriod + "','" + DueDate + "','" + PaymentAllocationRef + "','" + PaymentAllocationDate + "','" + PaymentAllocationPeriod + "','" + AssetIndicator + "','" + AssetCode + "','"
                + AssetSub_Code + "','" + Conversion_Code + "','" + ConversionRate + "','" + OtherAmount + "','" + OtherAmountDecimalPlaces + "','" + CleardownSequenceNumber + "','"
                + NextPeriodReversal + "','" + LossorGain + "','" + RoughBookFlag + "','" + InUseFlag + "','" + T0 + "','" + T1 + "','" + T2 + "','" + T3 + "','" + T4 + "','" + T5 + "','" + T6 + "','" + T7 + "','"
                + T8 + "','" + T9 + "','" + PostingDate + "','" + CoCd + "')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        #region  校验

        /// <summary>
        /// 验证AccountCode   返回0继续验证，否则导出数据，并添加一行说明是AccountCode不符合
        /// 添加人：ydx
        /// 添加时间：2014-09-18
        /// </summary>
        /// <param name="cocd"></param>
        /// <returns></returns>
        public DataTable checkAccount(string cocd)
        {
            string sql = @"select * from T_imp_raw_tmp  where AccountCode not in(select jvcoa.AccountCode from JV_COASetting jvcoa where jvcoa.CoCd='" + cocd + "' )";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("checkAccount");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-10
        /// 添加目的：验证T0
        /// </summary>
        /// <param name="cocd"></param>
        /// <returns></returns>
        public DataTable checkT0(string cocd)
        {
            string sql = @"	select * from T_imp_raw_tmp where T0  not in(select AreaCodeT0 from AreaSetting where CoCd='" + cocd + "' )and T0<>''";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("checkT0");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-10
        /// 添加目的：验证T3
        /// </summary>
        /// <param name="cocd"></param>
        /// <returns></returns>

        public DataTable checkT3(string cocd)
        {
            string sql = @"	select * from T_imp_raw_tmp where T3  not in(select T3Code from JVDSMDC where CoCd='" + cocd + "' )and T3<>''";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("checkT3");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-10
        /// 添加目的：验证t5
        /// </summary>
        /// <param name="cocd"></param>
        /// <returns></returns>

        public DataTable checkT5(string cocd)
        {
            string sql = @" select * from T_imp_raw_tmp  where T5  not in( select scl.JV_T5 from JVDSMDC jv left join T_SCLJV_T5 scl on jv.T5Code=scl.SCL_T5 and  jv.CoCd=scl.CoCd and scl.CoCd='" + cocd + "' ) and T5<>''";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("checkT5");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 添加人：ydx
        /// 验证T5Site部分
        /// </summary>
        /// <param name="cocd"></param>
        /// <returns></returns>
        public DataTable checkT5Site(string cocd)
        {
            string sql = @" select * from T_imp_raw_tmp  where T5  not in( select jv.T5Code from JVDSMDC jv where jv.CoCd='" + cocd + "' ) and T5<>'' and T5 like'R%'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("checkT5Site");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-18
        /// 添加目的：验证T8
        ///验证T8 返回0则继续验证，否则导出数据 ，并添加一行说明是T8不符合
        /// </summary>
        /// <param name="cocd"></param>
        /// <returns></returns>
        public DataTable checkT8(string cocd)
        {
            string sql = @"	select * from T_imp_raw_tmp where  T8 not in(select Tcode from TCodeSetting where TcodeType='T8' and CoCd='" + cocd + "') and T8<>''";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("checkT8");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable checkT1(string cocd)
        {
            string sql = @"  select * from T_imp_raw_tmp where T1<>'' ";
            sql += " and  T1 not in(select Tcode from TCodeSetting where TcodeType='T1' and CoCd='" + cocd + "') ";
            sql += " and AccountCode like '5101%'  ";
            sql += " union ";
            sql += " select * from T_imp_raw_tmp ";
            sql += " where T1<>''  ";
            sql += " and  T1 not in(select Tcode from TCodeSetting where TcodeType='T1' and CoCd='" + cocd + "')  ";
            sql += " and AccountCode like '5401%' ";
            sql += "  union ";
            sql += " select * from T_imp_raw_tmp ";
            sql += " where T1<>''  ";
            sql += " and  T1 not in(select Tcode from TCodeSetting where TcodeType='T1' and CoCd='" + cocd + "') ";
            sql += " and AccountCode like 'Q5%'  ";

            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("checkT1");
            dt.Load(dr);
            dr.Close();
            return dt;


        }

        public DataTable checkT2(string cocd)
        {
            string sql = @"  select * from T_imp_raw_tmp ";
            sql += "  where T2<>'' and T2 not in(select Tcode from TCodeSetting where TcodeType='T2' and CoCd='" + cocd + "') ";
            sql += " and AccountCode like '5101%'   ";
            sql += "	union   ";
            sql += "		select * from T_imp_raw_tmp   ";
            sql += "  where T2<>'' and T2 not in(select Tcode from TCodeSetting where TcodeType='T2' and CoCd='" + cocd + "')   ";
            sql += " and AccountCode like '5401%'  ";
            sql += "	 union  ";
            sql += "	select * from T_imp_raw_tmp   ";
            sql += " where T2<>'' and T2 not in(select Tcode from TCodeSetting where TcodeType='T2' and CoCd='" + cocd + "')  ";
            sql += " and AccountCode like 'Q5%'  ";

            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("checkT2");
            dt.Load(dr);
            dr.Close();
            return dt;


        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-18
        /// 添加目的：
        /// --验证T1  返回0行则继续验证，否则导出数据 ，并添加一行说明是T1不符合
        /// </summary>
        /// <param name="cocd"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        //public DataTable checkT1(string cocd)
        //{
        //    DataTable dt = null;
        //    SqlDataReader dataReader = null;
        //    SqlParameter[] sp = { 
        //                           sqlHelp.MakeInParam("@Cocd",SqlDbType.VarChar,50,cocd)
        //                            };

        //    sqlHelp.RunProc("ProCheckT1", sp, out dataReader);
        //    if (dataReader != null && dataReader.HasRows == true)
        //    {
        //        dt = new DataTable();
        //        dt.Load(dataReader);
        //    }
        //    else
        //    {
        //        string ba = "";
        //    }

        //    dataReader.Close();
        //    return dt;

        //    //string sql = @"select * from T_imp_raw_tmp  where  T1 not in(select Tcode from TCodeSetting where TcodeType='T1' and CoCd='"+cocd+"') and T1<>''";
        //    //SqlDataReader dr = SqlHelp.ExecuteReader(sql);
        //    //DataTable dt = new DataTable("checkT1");
        //    //dt.Load(dr);
        //    //dr.Close();
        //    //return dt;
        //}

        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-18
        /// 添加目的：验证T2
        /// --验证T2 返回0则继续验证，否则导出数据 ，并添加一行说明是T2不符合
        /// </summary>
        /// <param name="cocd"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        //public DataTable checkT2(string cocd)
        //{
        //    DataTable dt = null;

        //    SqlParameter[] sp = { 
        //                           sqlHelp.MakeInParam("@Cocd",SqlDbType.VarChar,50,cocd)
        //                            };

        //    SqlDataReader dataReaderT2 = sqlHelp.RunProcT2("ProCheckT2", sp);
        //    if (dataReaderT2 != null && dataReaderT2.HasRows == true)
        //    {
        //        dt = new DataTable();
        //        dt.Load(dataReaderT2);

        //    }
        //    dataReaderT2.Close();
        //    return dt;

        //    //string sql = @"	select * from T_imp_raw_tmp  where  T2 not in(select Tcode from TCodeSetting where TcodeType='T2' and CoCd='" + cocd + "') and T2<>''";
        //    //SqlDataReader dr = SqlHelp.ExecuteReader(sql);
        //    //DataTable dt = new DataTable("checkT2");
        //    //dt.Load(dr);
        //    //dr.Close();
        //    //return dt;
        //}
        #endregion
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-18
        /// 添加目的：匹配，将T_imp_raw 表里的AccountCodeS和AccountCodeD进行匹配
        /// </summary>
        /// <param name="cocd"></param>
        /// <returns></returns>
        public DataTable ImportCheckSelect(string cocd)
        {
            string sql = @" select a.copartershipID,a.AccountCode as AccountCodeS,b.SCLAccountCode as AccountCodeD,a.AccountPeriod 
			,a.TransactionDate,a.RecordType,a.JournalNumber,a.LineNumber,a.Amount,a.DCMarker,a.AllocationIndicator,a.JournalType,a.JournalSource,
			a.TransactionReference,a.Description,a.EntryDate,a.EntryPeriod,a.DueDate,a.PaymentAllocationRef,a.PaymentAllocationDate,a.PaymentAllocationPeriod,
            a.AssetIndicator,a.AssetCode,a.AssetSub_Code,a.Conversion_Code,a.ConversionRate,a.OtherAmount,a.OtherAmountDecimalPlaces,
            a.CleardownSequenceNumber,a.NextPeriodReversal,a.LossorGain,a.RoughBookFlag,a.InUseFlag,a.T0,a.T1,a.T2,a.T3,
            a.T4,scl.SCL_T5 as 'T5',a.T6,a.T7,a.T8,a.T9,a.PostingDate,a.UpdateOrderBalInd,a.AllocationinProgressMarker,
            a.JournalHoldReference,a.OperatorID,a.BudgetCheckAccount,'' as'createdby',null as'Createddate',
            '" + cocd + "' as 'CoCd'  from T_imp_raw_tmp a  inner join JV_COASetting b   on a.AccountCode=b.AccountCode  inner join T_SCLJV_T5 scl on scl.JV_T5=a.T5 and b.CoCd=scl.CoCd  and scl.CoCd='" + cocd + "' and a.T5 not like 'R%' ";

            sql += @" union  select  a.copartershipID,a.AccountCode as AccountCodeS,b.SCLAccountCode as AccountCodeD,a.AccountPeriod 
			,a.TransactionDate,a.RecordType,a.JournalNumber,a.LineNumber,a.Amount,a.DCMarker,a.AllocationIndicator,a.JournalType,a.JournalSource,
			a.TransactionReference,a.Description,a.EntryDate,a.EntryPeriod,a.DueDate,a.PaymentAllocationRef,a.PaymentAllocationDate,a.PaymentAllocationPeriod,
            a.AssetIndicator,a.AssetCode,a.AssetSub_Code,a.Conversion_Code,a.ConversionRate,a.OtherAmount,a.OtherAmountDecimalPlaces,
            a.CleardownSequenceNumber,a.NextPeriodReversal,a.LossorGain,a.RoughBookFlag,a.InUseFlag,a.T0,a.T1,a.T2,a.T3,
            a.T4,a.T5,a.T6,a.T7,a.T8,a.T9,a.PostingDate,a.UpdateOrderBalInd,a.AllocationinProgressMarker,
            a.JournalHoldReference,a.OperatorID,a.BudgetCheckAccount,'' as'createdby',null as'Createddate',
            '" + cocd + "' as 'CoCd' from T_imp_raw_tmp a  inner join JV_COASetting b  on a.AccountCode=b.AccountCode and  b.CoCd='" + cocd + "' and a.T5  like 'R%' ";
            sql += @" union 
              select  a.copartershipID,a.AccountCode as AccountCodeS,b.SCLAccountCode as AccountCodeD,a.AccountPeriod 
			,a.TransactionDate,a.RecordType,a.JournalNumber,a.LineNumber,a.Amount,a.DCMarker,a.AllocationIndicator,a.JournalType,a.JournalSource,
			a.TransactionReference,a.Description,a.EntryDate,a.EntryPeriod,a.DueDate,a.PaymentAllocationRef,a.PaymentAllocationDate,a.PaymentAllocationPeriod,
            a.AssetIndicator,a.AssetCode,a.AssetSub_Code,a.Conversion_Code,a.ConversionRate,a.OtherAmount,a.OtherAmountDecimalPlaces,
            a.CleardownSequenceNumber,a.NextPeriodReversal,a.LossorGain,a.RoughBookFlag,a.InUseFlag,a.T0,a.T1,a.T2,a.T3,
            a.T4,a.T5,a.T6,a.T7,a.T8,a.T9,a.PostingDate,a.UpdateOrderBalInd,a.AllocationinProgressMarker,
            a.JournalHoldReference,a.OperatorID,a.BudgetCheckAccount,'' as'createdby',null as'Createddate',
            '" + cocd + "' as 'CoCd' from T_imp_raw_tmp a  inner join JV_COASetting b    on a.AccountCode=b.AccountCode and  b.CoCd='" + cocd + "' and a.T5=''";


            //            string sql = @" select a.id, a.copartershipID,a.AccountCode as AccountCodeS,b.SCLAccountCode as AccountCodeD,a.AccountPeriod 
            //			,a.TransactionDate,a.RecordType,a.JournalNumber,a.LineNumber,a.Amount,a.DCMarker,a.AllocationIndicator,a.JournalType,a.JournalSource,
            //			a.TransactionReference,a.Description,a.EntryDate,a.EntryPeriod,a.DueDate,a.PaymentAllocationRef,a.PaymentAllocationDate,a.PaymentAllocationPeriod,
            //            a.AssetIndicator,a.AssetCode,a.AssetSub_Code,a.Conversion_Code,a.ConversionRate,a.OtherAmount,a.OtherAmountDecimalPlaces,
            //            a.CleardownSequenceNumber,a.NextPeriodReversal,a.LossorGain,a.RoughBookFlag,a.InUseFlag,a.T0,a.T1,a.T2,a.T3,
            //            a.T4,a.T5,a.T6,a.T6,a.T8,a.T9,a.PostingDate,a.UpdateOrderBalInd,a.AllocationinProgressMarker,
            //            a.JournalHoldReference,a.OperatorID,a.BudgetCheckAccount,'' as'createdby',null as'Createddate',
            //            '" + cocd + "' as 'CoCd'from T_imp_raw_tmp a left join JV_COASetting b  on a.AccountCode=b.AccountCode and b.CoCd='" + cocd + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("check");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportCheckExits(string AccountPeriod)
        {
            string sql = @"select AccountCode,T0,T1,T2,T3,T5,T8 from T_imp_raw_tmp 
where AccountCode not in (select AccountCode from JV_COASetting) or T8 not in(
select Tcode from TCodeSetting where TcodeType='T0' or TcodeType='T1' or TcodeType='T2' or TcodeType='T3' 
or TcodeType='T5' or TcodeType='T8') ";
            if (!string.IsNullOrEmpty(AccountPeriod))
            {
                sql += "and AccountPeriod ='" + AccountPeriod + "'";
            }
            //if (!string.IsNullOrEmpty(T3))
            //{
            //    sql += "and T3 ='" + T3 + "'";
            //}
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_imp_raw_tmp");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加目的：此合资公司下的此部门下的T5code替换后 ，替换前
        /// </summary>
        /// <param name="cocd"></param>
        /// <returns></returns>
        public DataTable yanT5(string cocd)
        {
            string sql = @" select jv.T5Code ,scl.JV_T5  from JVDSMDC jv join T_SCLJV_T5 scl on jv.T5Code=scl.SCL_T5 and jv.CoCd=scl.CoCd and jv.CoCd='" + cocd + "' and jv.Nature='JV'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_imp_rawT5");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加目的：替换
        /// </summary>
        /// <param name="T5Before"></param>
        /// <param name="T5After"></param>
        /// <returns></returns>
        public int changeT5(string T5Before, string T5After)
        {
            string sql = @" update T_imp_raw_tmp set T5='" + T5After + "' where T5='" + T5Before + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        //查询
        public DataTable ImportSelect()
        {
            string sql = @"select * from T_imp_raw_tmp";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_imp_raw_tmp");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 修改人：ydx
        /// 修改时间：2014-09-23
        /// 数据包导入 浏览 时验证会计期间
        /// </summary>
        /// <param name="AccountPeriod"></param>
        /// <returns></returns>
        public DataTable ImportDateSelect(string AccountPeriod)
        {
            string sql = @"select count(*) from T_imp_raw_tmp where  AccountPeriod='" + AccountPeriod + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_imp_raw_tmp");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 修改人：ydx
        ///数据包导入 浏览时 验证合资公司
        /// </summary>
        /// <param name="CoCd"></param>
        /// <returns></returns>
        public DataTable ImportCoCdSelect(string CoCd)
        {
          //  string sql = @"select count(*) from T_imp_raw_tmp a , (select * from JVDSMDC where CoCd='" + CoCd
              //  + "')b where a.T3=b.T3Code";
                string sql = @"select count(*) from T_imp_raw_tmp a , (select * from JVDSMDC where CoCd='" + CoCd
                   + "')b where a.T3=b.T3Code   ";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_imp_raw_tmp");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 修改人：ydx
        ///数据包导入 浏览时 验证是否已经导入此公司、此期间的数据
        /// </summary>
        /// <param name="CoCd"></param>
        /// <param name="AccountPeriod"></param>
        /// <returns></returns>
        public DataTable ImportCoCdSelect(string CoCd, string AccountPeriod)
        {
            string sql = @"select count(*) from T_imp_raw a , (select * from JVDSMDC where CoCd='" + CoCd
                + "')b where a.T3=b.T3Code and a.AccountPeriod='" + AccountPeriod + "' and a.CoCd=b.CoCd";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_imp_raw_tmp");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 此方法不能删除,程序中有用到
        /// 删除临时表
        /// </summary>
        /// <returns></returns>
        public int ImportDelete()
        {
            string sql = "truncate table T_imp_raw_tmp";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-20
        /// 添加目的：试算平衡表 数据追踪
        /// </summary>
        /// <param name="CoCd"></param>
        /// <param name="AccountPeriod"></param>
        /// <returns></returns>
        public DataTable dataList(string where)
        {
            string sql = @"select * from T_imp_raw where 1=1";
            if (where != "")
            {
                sql += where;
            }
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("datalist");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-23
        /// 添加目的：调用 查询试算平衡表的存储过程 只查出替换后的会计代码
        /// </summary>
        /// <param name="cocd"></param>
        /// <param name="accountPerid"></param>
        /// <returns></returns>
        public DataTable tbSeleByProcD(string cocd, string peridStar, string accountPerid, string t8)
        {
            DataTable dt = null;
            SqlDataReader dataReader = null;
            SqlParameter[] sp = { 
                                   sqlHelp.MakeInParam("@Cocd",SqlDbType.VarChar,50,cocd),
                                   sqlHelp.MakeInParam("@AccountPeridStar",SqlDbType.VarChar,20,peridStar),
                                   sqlHelp.MakeInParam("@AccountPerid",SqlDbType.VarChar,20,accountPerid),
                                   sqlHelp.MakeInParam("@T8",SqlDbType.VarChar,100,t8)
                                };

            sqlHelp.RunProc("ProTB_aD", sp, out dataReader);
            if (dataReader != null && dataReader.HasRows == true)
            {
                dt = new DataTable();
                dt.Load(dataReader);
            }

            dataReader.Close();
            return dt;
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-09-23
        /// 添加目的：调用 查询试算平衡表的存储过程  查出替换前和替换后的会计代码
        /// </summary>
        /// <param name="cocd"></param>
        /// <param name="accountPerid"></param>
        /// <returns></returns>
        public DataTable tbSeleByProcSD(string cocd, string peridStar, string accountPerid, string t8)
        {
            DataTable dt = null;
            SqlDataReader dataReader = null;
            SqlParameter[] sp = { 
                                   sqlHelp.MakeInParam("@Cocd",SqlDbType.VarChar,50,cocd),
                                  sqlHelp.MakeInParam("@AccountPeridStar",SqlDbType.VarChar,20,peridStar),
                                   sqlHelp.MakeInParam("@AccountPerid",SqlDbType.VarChar,20,accountPerid),
                                     sqlHelp.MakeInParam("@T8",SqlDbType.VarChar,100,t8)
                                };

            sqlHelp.RunProc("ProTB_aSD", sp, out dataReader);
            if (dataReader != null && dataReader.HasRows == true)
            {
                dt = new DataTable();
                dt.Load(dataReader);
            }

            dataReader.Close();
            return dt;
        }
        /// <summary>
        /// 添加人:ydx
        ///添加时间：2014-09-26
        ///添加目的：资产负债表 两月的差
        /// </summary>
        /// <param name="cocd"></param>
        /// <param name="accountPerid"></param>
        /// <returns></returns>
        public DataTable bsSeleByProcMin(string cocd, string accountPerid, string t8code)
        {
            DataTable dt = null;
            SqlDataReader dataReader = null;
            SqlParameter[] sp = { 
                                   sqlHelp.MakeInParam("@Cocd",SqlDbType.VarChar,10,cocd),
                                   sqlHelp.MakeInParam("@AccountPerid",SqlDbType.VarChar,20,accountPerid),
                                   sqlHelp.MakeInParam("@T8",SqlDbType.VarChar,200,t8code)
                                };

            sqlHelp.RunProc("ProcBs_Min", sp, out dataReader);
            if (dataReader != null && dataReader.HasRows == true)
            {
                dt = new DataTable();
                dt.Load(dataReader);
            }

            dataReader.Close();
            return dt;
        }

        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-08
        /// 添加目的：资产负债表详细
        /// </summary>
        /// <param name="cocd"></param>
        /// <param name="accountPerid"></param>
        /// <param name="t8code"></param>
        /// <returns></returns>
        public DataTable bsSeleByProc(string cocd, string accountPerid, string t8code)
        {
            DataTable dt = null;
            SqlDataReader dataReader = null;
            SqlParameter[] sp = { 
                                   sqlHelp.MakeInParam("@Cocd",SqlDbType.VarChar,10,cocd),
                                   sqlHelp.MakeInParam("@AccountPerid",SqlDbType.VarChar,20,accountPerid),
                                   sqlHelp.MakeInParam("@T8",SqlDbType.VarChar,200,t8code)
                                };
            sqlHelp.RunProc("ProTest", sp, out dataReader);
            //sqlHelp.RunProc("ProcBsT8", sp, out dataReader);
            if (dataReader != null && dataReader.HasRows == true)
            {
                dt = new DataTable();
                dt.Load(dataReader);
            }
            dataReader.Close();
            return dt;
        }


        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-26
        /// 添加目的：资产负债表 按月输出 期初余额
        /// </summary>
        /// <param name="cocd"></param>
        /// <param name="accountPerid"></param>
        /// <param name="t8code"></param>
        /// <returns></returns>
        public DataTable bsSeleByProcMonthChu(string cocd, string accountPerid, string t8code)
        {
            DataTable dt = null;
            SqlDataReader dataReader = null;
            SqlParameter[] sp = { 
                                   sqlHelp.MakeInParam("@Cocd",SqlDbType.VarChar,10,cocd),
                                   sqlHelp.MakeInParam("@AccountPerid",SqlDbType.VarChar,20,accountPerid),
                                   sqlHelp.MakeInParam("@T8",SqlDbType.VarChar,200,t8code)
                                };

            sqlHelp.RunProc("ProcBsMonthChu", sp, out dataReader);
            if (dataReader != null && dataReader.HasRows == true)
            {
                dt = new DataTable();
                dt.Load(dataReader);
            }

            dataReader.Close();
            return dt;
        }

        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-26
        /// 添加目的：资产负债表 按月输出 每月余额
        /// </summary>
        /// <param name="cocd"></param>
        /// <param name="accountPerid"></param>
        /// <param name="t8code"></param>
        /// <returns></returns>
        public DataTable bsSeleByProcMonth(string cocd, string accountPerid, string t8code)
        {
            DataTable dt = null;
            SqlDataReader dataReader = null;
            SqlParameter[] sp = { 
                                   sqlHelp.MakeInParam("@Cocd",SqlDbType.VarChar,10,cocd),
                                   sqlHelp.MakeInParam("@AccountPerid",SqlDbType.VarChar,20,accountPerid),
                                   sqlHelp.MakeInParam("@T8",SqlDbType.VarChar,200,t8code)
                                };

            sqlHelp.RunProc("ProcBsMonth", sp, out dataReader);
            if (dataReader != null && dataReader.HasRows == true)
            {
                dt = new DataTable();
                dt.Load(dataReader);
            }

            dataReader.Close();
            return dt;
        }
        /// <summary>
        /// 李晓光 做的
        /// 试算平衡表----合资公司
        /// </summary>
        /// <returns></returns>
        public DataTable ImportTBSelect()
        {
            string sql = @"select s.AccountCodeS,s.Account_Description,s.Accumulated_balance,s.periods,s.Debit,s.Credit,
isnull(s.Accumulated_balance+s.periods,0)as Accumulated from(
select distinct AccountCodeS,Account_Description,(1-1) as Accumulated_balance,isnull(periods,0)as periods,
isnull(Debit,0)as Debit,isnull(Credit,0)as Credit from View_CD
where AccountCodeS not in(select AccountCode from T_TB where [date]='2013012' ) union all 
select AccountCode,Account_Description,Accumulated_balance,periods,Debit,Credit from TB)s
order by s.AccountCodeS";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_imp_raw_tmp");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 李晓光 做的
        /// SCL
        /// </summary>
        /// <returns></returns>
        public DataTable ImportTBSelect_SCL()
        {
            string sql = @"select coa.Account,coa.Account_Description,sum(tb.Accumulated_balance)as Accumulated_balance,sum(tb.periods) as periods,sum(tb.Debit)as Debit,
sum(tb.Credit)as Credit,sum(tb.Accumulated)as Accumulated from 
(select a.AccountCode,b.Account_Description,b.Account from JV_COASetting a,COASetting b
where a.SCLAccountCode=b.Account) coa,
(select s.AccountCodeS,s.Account_Description,s.Accumulated_balance,s.periods,s.Debit,s.Credit,
isnull(s.Accumulated_balance+s.periods,0)as Accumulated from(
select distinct AccountCodeS,Account_Description,(1-1) as Accumulated_balance,isnull(periods,0)as periods,
isnull(Debit,0)as Debit,isnull(Credit,0)as Credit from View_CD
where AccountCodeS not in(select AccountCode from T_TB where [date]='2013012' ) union all 
select AccountCode,Account_Description,Accumulated_balance,periods,Debit,Credit from TB)s 
) tb where coa.AccountCode=tb.AccountCodeS group by coa.Account,coa.Account_Description
order by coa.Account";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_imp_raw_tmp");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        /// <summary>
        /// 暂时不用
        /// 李晓光 做的
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public int ImportInsert_tmp(string date)
        {
            string sql = @"insert into T_imp_raw_tb
select * from T_imp_raw where AccountPeriod='" + date + "'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        /// <summary>
        /// 暂时不用
        /// 李晓光 做的
        /// </summary>
        /// <returns></returns>
        public int ImportDelete_tmp()
        {
            string sql = "truncate table T_imp_raw_tb";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            dr.Close();
            return 1;
        }
        public DataTable ImportBSSelect()
        {
            string sql = @"select c.AccSubType,isnull(sum(t.Accumulated_balance),0)as Accumulated_balance,isnull(sum(t.Accumulated),0)as Accumulated
from
(select isnull(b.AccountCode,0)as AccountCode, a.AccSubType from (select 
	case
	when AccountCode = ''
	then '0'
	else AccountCode
	end as AccountCode,AccSubType 
from T_BSSetting) a left join JV_COASetting b on a.AccountCode=b.SCLAccountCode) c left join View_STB t 
on c.AccountCode=t.AccountCodeS
group by c.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("T_imp_raw_tb");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        #region 资产负债表
        #region 资产下的流动资产
        public DataTable ImportSelect_reportBS_hbzj()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%货币资金%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_jyxjrzc()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType  like '%交易性金融资产%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_yspj()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%应收票据%'  group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_yszk()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%应收账款%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_yfzk()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%预付账款%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_ysgl()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%应收股利%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_yslx()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%应收利息%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_qtysk()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%其它应收款%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_ch()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%存货%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_xhxswzc()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%消耗性生物资产%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_dtfy()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType = '待摊费用' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_ynndqdfldzc()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%一年内到期的非流动资产%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_qtldzc()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%其他流动资产%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        #endregion
        #region 资产下的非流动资产
        public DataTable ImportSelect_reportBS_kgcsjrzc()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%可供出售金融资产%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_cyzdqtz()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%期投资%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_tzxfdc()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%投资性房地产%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_cqgqtz()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%长期股权投资%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_cqysk()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%长期应收款%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_gdzc()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%固定资产%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_zjgc()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%在建工程%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_gcwz()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%工程物资%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_gdzcql()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%固定资产清理%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_scxswzc()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%生产性生物资产%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_yqzc()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%油气资产%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_wxzc()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%无形资产%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_kfzc()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%开发支出%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_sy()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%商誉%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_cqdtfy()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%长期待摊费用%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_dysdszc()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%递延所得税资产%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_qtfldzc()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%其他非流动资产%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        #endregion
        #region 负债和所有者权益（或股东权益）下的流动负债
        public DataTable ImportSelect_reportBS_dqjk()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%短期借款%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_jyxjrfz()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%交易性金融负债%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_yfpj()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%应付票据%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_ldfzyfzk()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%应付帐款%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_ldfzyszk()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%预收帐款%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_ldfzyfgz()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%应付工资%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_ldfzyjsj()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%应交税金%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_ldfzyflx()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%应付利息%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_ldfzyfgl()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%应付股利%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_ldfzqtyfk()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%其它应付款%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_ldfzqtfy()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%预提费用%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_ldfzyjfz()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%预计负债%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_ynndqdcqzqtz()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%一年内到期的长期债权投资%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_qtcqfz()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%其他长期负债%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        #endregion
        #region 负债和所有者权益（或股东权益）下的非流动负债
        public DataTable ImportSelect_reportBS_cqjk()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%长期借款%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_yfzq()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%应付债券%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_cqyfk()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%长期应付款%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_zxyfk()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%专项应付款%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_dysdsfz()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%递延所得税负债%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_tfldfz()
        {
            string sql = @"select a.AccType,a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%其他非流动负债%' group by a.AccType,a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        #endregion
        #region 负债和所有者权益（或股东权益）下的所有者权益（或股东权益）
        public DataTable ImportSelect_reportBS_sszb()
        {
            string sql = @"select sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%实收资本%' ";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_zbgj()
        {
            string sql = @"select a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%资本公积%' group by a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_yygj()
        {
            string sql = @"select a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%盈余公积%' group by a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_wfplr()
        {
            string sql = @"select a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%未分配利润%' group by a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportBS_jkcg()
        {
            string sql = @"select a.AccSubType,sum(b.Amount)as 'money' from T_BSSetting a,T_imp_raw_tmp b 
where a.AccSubType like '%减：库存股%' group by a.AccSubType";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        #endregion
        #endregion
        #region 损益表
        public DataTable ImportSelect_reportPL_sybyysr()
        {
            string sql = @"select sum(a.Accumulated_balance) as 'money',a.date,b.AccSubGroup from T_TB a,
(SELECT [AccGroup],[AccSubGroup],[AccType],[AccSubType],[AccountCode],[Account_Description],[createdby],[Createddate],[Modifyby],[Modifydate] FROM [Shell-MIS].[dbo].[T_PLSetting]
  where AccSubGroup like '%营业收入%') b where a.AccountCode=b.AccountCode group by b.AccSubGroup,a.date";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportPL_sybyycb()
        {
            string sql = @"select sum(a.Accumulated_balance) as 'money',a.date,b.AccSubGroup from T_TB a,
(SELECT [AccGroup],[AccSubGroup],[AccType],[AccSubType],[AccountCode],[Account_Description],[createdby],[Createddate],[Modifyby],[Modifydate] FROM [Shell-MIS].[dbo].[T_PLSetting]
  where AccSubGroup like '%营业成本%') b where a.AccountCode=b.AccountCode group by b.AccSubGroup,a.date";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportPL_sybyysf()
        {
            string sql = @"select sum(a.Accumulated_balance) as 'money',a.date,b.AccSubGroup from T_TB a,
(SELECT [AccGroup],[AccSubGroup],[AccType],[AccSubType],[AccountCode],[Account_Description],[createdby],[Createddate],[Modifyby],[Modifydate] FROM [Shell-MIS].[dbo].[T_PLSetting]
  where AccSubGroup like '%营业税费%') b where a.AccountCode=b.AccountCode group by b.AccSubGroup,a.date";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportPL_sybxsfy()
        {
            string sql = @"select sum(a.Accumulated_balance) as 'money',a.date,b.AccSubGroup from T_TB a,
(SELECT [AccGroup],[AccSubGroup],[AccType],[AccSubType],[AccountCode],[Account_Description],[createdby],[Createddate],[Modifyby],[Modifydate] FROM [Shell-MIS].[dbo].[T_PLSetting]
  where AccSubGroup like '%销售费用%') b where a.AccountCode=b.AccountCode group by b.AccSubGroup,a.date";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportPL_sybglfy()
        {
            string sql = @"select sum(a.Accumulated_balance) as 'money',a.date,b.AccSubGroup from T_TB a,
(SELECT [AccGroup],[AccSubGroup],[AccType],[AccSubType],[AccountCode],[Account_Description],[createdby],[Createddate],[Modifyby],[Modifydate] FROM [Shell-MIS].[dbo].[T_PLSetting]
  where AccSubGroup like '%管理费用%') b where a.AccountCode=b.AccountCode group by b.AccSubGroup,a.date";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportPL_sybgcwfy()
        {
            string sql = @"select sum(a.Accumulated_balance) as 'money',a.date,b.AccSubGroup from T_TB a,
(SELECT [AccGroup],[AccSubGroup],[AccType],[AccSubType],[AccountCode],[Account_Description],[createdby],[Createddate],[Modifyby],[Modifydate] FROM [Shell-MIS].[dbo].[T_PLSetting]
  where AccSubGroup like '%财务费用%') b where a.AccountCode=b.AccountCode group by b.AccSubGroup,a.date";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportPL_sybgzcjzss()
        {
            string sql = @"select sum(a.Accumulated_balance) as 'money',a.date,b.AccSubGroup from T_TB a,
(SELECT [AccGroup],[AccSubGroup],[AccType],[AccSubType],[AccountCode],[Account_Description],[createdby],[Createddate],[Modifyby],[Modifydate] FROM [Shell-MIS].[dbo].[T_PLSetting]
  where AccSubGroup like '%资产减值损失%') b where a.AccountCode=b.AccountCode group by b.AccSubGroup,a.date";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportPL_sybggyjzbdjsy()
        {
            string sql = @"select sum(a.Accumulated_balance) as 'money',a.date,b.AccSubGroup from T_TB a,
(SELECT [AccGroup],[AccSubGroup],[AccType],[AccSubType],[AccountCode],[Account_Description],[createdby],[Createddate],[Modifyby],[Modifydate] FROM [Shell-MIS].[dbo].[T_PLSetting]
  where AccSubGroup like '%公允价值变动净收益%') b where a.AccountCode=b.AccountCode group by b.AccSubGroup,a.date";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportPL_sybgtzjsy()
        {
            string sql = @"select sum(a.Accumulated_balance) as 'money',a.date,b.AccSubGroup from T_TB a,
(SELECT [AccGroup],[AccSubGroup],[AccType],[AccSubType],[AccountCode],[Account_Description],[createdby],[Createddate],[Modifyby],[Modifydate] FROM [Shell-MIS].[dbo].[T_PLSetting]
  where AccSubGroup like '%投资净收益%') b where a.AccountCode=b.AccountCode group by b.AccSubGroup,a.date";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportPL_sybgyywsr()
        {
            string sql = @"select sum(a.Accumulated_balance) as 'money',a.date,b.AccSubGroup from T_TB a,
(SELECT [AccGroup],[AccSubGroup],[AccType],[AccSubType],[AccountCode],[Account_Description],[createdby],[Createddate],[Modifyby],[Modifydate] FROM [Shell-MIS].[dbo].[T_PLSetting]
  where AccSubGroup like '%营业外收入%') b where a.AccountCode=b.AccountCode group by b.AccSubGroup,a.date";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportPL_sybgyywzc()
        {
            string sql = @"select sum(a.Accumulated_balance) as 'money',a.date,b.AccSubGroup from T_TB a,
(SELECT [AccGroup],[AccSubGroup],[AccType],[AccSubType],[AccountCode],[Account_Description],[createdby],[Createddate],[Modifyby],[Modifydate] FROM [Shell-MIS].[dbo].[T_PLSetting]
  where AccSubGroup like '%营业外支出%') b where a.AccountCode=b.AccountCode group by b.AccSubGroup,a.date";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable ImportSelect_reportPL_sybgfldzcczjss()
        {
            string sql = @"select sum(a.Accumulated_balance) as 'money',a.date,b.AccSubGroup from T_TB a,
(SELECT [AccGroup],[AccSubGroup],[AccType],[AccSubType],[AccountCode],[Account_Description],[createdby],[Createddate],[Modifyby],[Modifydate] FROM [Shell-MIS].[dbo].[T_PLSetting]
  where AccSubGroup like '%非流动资产处置净损失%') b where a.AccountCode=b.AccountCode group by b.AccSubGroup,a.date";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        #endregion

        #region MIS_Kpi  2014-07-08 ydx
        /// <summary>
        /// GROSS PROCEEDS OF SALES  30行
        /// </summary>
        /// <returns></returns>
        public DataTable import_16()
        {
            string sql = @"select SUM(Amount) as 'money' from [Shell-MIS].[dbo].[T_imp_raw_tmp]  where SUBSTRING(EntryDate,0,7)='201401'and  AccountCode ='Q51010101'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //25行
        public DataTable import_25()
        {
            string sql = @"select SUM(Amount) as 'money' from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and  AccountCode ='Q51010103'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //46行
        public DataTable import_46()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode ='51014101'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //54行
        public DataTable import_54()
        {
            string sql = @"select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp]  where SUBSTRING(EntryDate,0,7)='201401'and AccountCode ='51014104'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //55行
        public DataTable import_55()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode ='51014103'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //60行
        public DataTable import_60()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode ='51010101'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //68 行
        public DataTable import_68()
        {
            string sql = @"select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp]  where SUBSTRING(EntryDate,0,7)='201401'and AccountCode ='51010104'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //69 行
        public DataTable import_69()
        {
            string sql = @"select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp]  where SUBSTRING(EntryDate,0,7)='201401'and AccountCode ='51010103'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //70行
        public DataTable import_70()
        {
            string sql = @"select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp]  where SUBSTRING(EntryDate,0,7)='201401'and AccountCode ='51010105'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //74行
        public DataTable import_74()
        {
            string sql = @"select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode ='54010101'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //82 行
        public DataTable import_82()
        {
            string sql = @"select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp]  where SUBSTRING(EntryDate,0,7)='201401'and AccountCode ='54010104'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //83 行
        public DataTable import_83()
        {
            string sql = @"select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode ='54010103'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //84 行
        public DataTable import_84()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode ='54010205'";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //115 行
        public DataTable import_115()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61004031','61004032')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //116 行
        public DataTable import_116()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61001011','6100403202','61003056','6100403299')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //117 行
        public DataTable import_117()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('5401010101','5401010102')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        //RENTAL 135行
        public DataTable import_135()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61007001','61002001','61002002','61004051','61004052','61007002')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        //MANPOWER  136行
        public DataTable import_136()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61001006','61001004','61001038','61001005','61001023','61001001','61001002','61001003','61001007','61001022','61001009','61001010','61001021','61001031','61001032','61001033','61001034','61001035','61001036','61001037','61001039','61001040','61001041')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //SP&A  137行
        public DataTable import_137()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61005001','6100500101','6100500102','6100500103','6100500104','61005004','6100500401','6100500402','6100500403','6100500404','6100500405')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //SITE HSSE  138行
        public DataTable import_138()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61003006','61002031','61002032','61003031','61003032','6100303210','6100303220','6100303230','61003033','61003034','61003035','61003036','61003039','61003040','61003045')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //REPAIR & MAINTENANCE  139行
        public DataTable import_139()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('6100300150','6100300310','61003001','6100301610','61003014','6100300320','61003016','6100300830','6100300210','6100300211','6100300212','6100300160','61003004','6100300410','61003002','6100300820','6100300910','6100300420','6100300810','61003003','61003010','6100300840','61003009','6100301630','6100300110','6100301620','6100300170','6100300140','6100300120','6100300124','6100300123','6100300122','6100300121','6100300125','6100300850','61003008','6100300920','6100300220','61003005','6100300130','61003013','6100300226','6100300224','6100300225','6100300222','6100300223','6100300221')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        //STAMP DUTY & PROPERTY TAX   140行
        public DataTable import_140()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61006003','61006002','61006001')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //UTILITY  141行
        public DataTable import_141()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp]  where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('6100200350','6100200340','6100200320','6100200330','6100200310','61003042','61003043','61003041','61002003','6100200360')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //IT  142行 
        public DataTable import_142()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('6100301130','61003015','61003011','61003012','61002008','6100301120','6100301110')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //RBA  143行
        public DataTable import_143()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp]  where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('6100502301','6100502302','6100502303','6100502304','61005025')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }


        //RENTAL  151行
        public DataTable import_151()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61002001','61002002')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        // MANPOWER  152行
        public DataTable import_152()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp]  where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61001006','61001004','61001038','61001005','61001023','61001001','61001002','61001003','61001007','61001022','61001009','61001010','61001021','61001031','61001032','61001033','61001034','61001035','61001036','61001037','61001039','61001040','61001041')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //SP&A  153行
        public DataTable import_153()
        {
            string sql = @"  select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61005003','6100500301','6100500302','61005011','6100501101','6100501102','6100501103','61005002','6100500201','6100500202')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //TRAVEL & ENTER.   154行
        public DataTable import_154()
        {
            string sql = @"  select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61004001','61002015','61002011','61002012','61002013', '61002016','61002009','61004003','61004002','61004004')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //HSSE  155行
        public DataTable import_155()
        {
            string sql = @"select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61003006','61002031','61002032','61003031','61003032','6100303210','6100303220','6100303230','61003033','61003034','61003035','61003036','61003039','61003040')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        //PROFESSIONALS  156行
        public DataTable import_156()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61006031','61006023','61006021','61006032','61006025', '61006011','61004007','61006022','61006012')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //   TMSLA-CHINESE PARTNER  160
        public DataTable import_160()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61006042','61005022')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        //  TMSLA-SCL   161行
        public DataTable import_161()
        {
            string sql = @"select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp]  where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61006041','61005021')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        //NON-OPERATING   162行
        public DataTable import_162()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('51020101','51020102','51020201','51020202','51020203','51020290','51020301','51029001','54060301','54060401','54069001')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //DEPRECIATION SITE  166行
        public DataTable import_166()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp]where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61007024','61007030','61007031','61007011','61007012','61007013','61007014','61007015','61007016','61007017','61007018','61007019','61007041','61007042')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //DEPRECIATION JV  167行
        public DataTable import_167()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('61007024','61007030','61007031','61007011','61007012','61007013','61007014','61007015','61007016','61007017','61007018','61007019','61007041','61007042')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //IMPAIREMENT 168行
        public DataTable import_168()
        {
            string sql = @" select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode='55038101' ";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //P/L DISPOSAL   169行
        public DataTable import_169()
        {
            string sql = @"select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp]  where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('53010201','56010701','51011201','51011202','51011290','52030101','53010101','53010102','53010103','53010104','53010105','53010106','53010107','53010108','53010111','53010112','53010113','53010114','53010115','53010116','53010117','53010118','53010202','53010205','53010301','53010390','53010501','53010502','53010801','53010802','53019001','54011201','54011202','54011290','54020101','54020102','54020103','54020190','54020501','56010101','56010102','56010103','56010104','56010105','56010106','56010107','56010108','56010111','56010112','56010113','56010114','56010115','56010116','56010117','56010118','56010201','56010290','56010401','56010402','56010501','56010601','56010702','56010705','56010790','56010801','56010802','56010901','56010902','56011001','56011002','56011003','56011004','56011005','56011006','56011007','56011008','56011009','56011301','56011302','56011801','56011802','56011901','56011902','56011990','58010101','58010102','58010103','58010104','58010105','53010203','53010204','56010703','56010704','53010602','53010603','56011101','56011102','56011201','56011202') ";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //TAX  182行

        public DataTable import_182()
        {
            string sql = @"select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp]  where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('57010101','57010102')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //FINANCING COST  184行

        public DataTable import_184()
        {
            string sql = @"select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp] where SUBSTRING(EntryDate,0,7)='201401'and AccountCode in('55030101','55030102','55030111','55030112','55030201','55030202','55030211','55030212','55030601','55030602','55030603','55030611','55030301','55030302','55030303','55030304','55030305','55030306','55030401','55030402','55030403','55030404','55030405','55030406')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        //144,157,173行
        public DataTable import_144_157_173()
        {
            string sql = @"select SUM(Amount) from [Shell-MIS].[dbo].[T_imp_raw_tmp]  where SUBSTRING(EntryDate,0,7)='201401'and  AccountCode like('6%')";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        #endregion

        #region

        /// <summary>
        /// kpi导出
        /// 添加人：ydx
        /// 添加时间:2014-08-07
        /// </summary>
        /// <returns></returns>
        public DataTable kpiReport()
        {
            string sql = @"declare @sql varchar(max) select  @sql=isnull(@sql+','+CHAR(15),'')+'sum(case when  SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)=' +''''+LTRIM(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9))+''''+ ' then [Amount] else 0 end) as '+QUOTENAME(SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9)) from [T_imp_raw]  where 1=1  GROUP BY SUBSTRING(CONVERT (varchar(10),AccountPeriod),0,9) select @sql='select kp.ReportGroup, kp.ReportType, kp.ReportSubType,'+@sql+' from KPISetting kp inner join T_imp_raw imp on imp.AccountCodeD=kp.AccountCode inner join KPITemp kt on kt.Id=kp.Id and kp.T2<>'''' and imp.T2=kt.T2 and kp.T5='''' group by rollup(kp.ReportGroup, kp.ReportType, kp.ReportSubType) union all select null, kp.ReportGroup, kp.ReportType,'+@sql+' from KPISetting kp inner join T_imp_raw imp on imp.AccountCodeD=kp.AccountCode and kp.T2='''' and kp.T5='''' group by rollup(kp.ReportGroup, kp.ReportType) union all select null, kp.ReportGroup, kp.ReportType,'+@sql+' from KPISetting kp inner join T_imp_raw imp on imp.AccountCodeD=kp.AccountCode and kp.T2='''' and kp.T5=''R*'' and imp.T5 like''R%'' group by rollup(kp.ReportGroup, kp.ReportType) union all select null, kp.ReportGroup, kp.ReportType,'+@sql+' from KPISetting kp inner join T_imp_raw imp on imp.AccountCodeD=kp.AccountCode inner join KPITemp kt on kt.Id=kp.Id and kp.T2='''' and kp.T5<>''R*'' and imp.T5=kt.T5 group by rollup(kp.ReportGroup, kp.ReportType) union all select null, kp.ReportGroup, kp.ReportType,'+@sql+'  from KPISetting kp , T_imp_raw imp  , KPITemp kt where kt.Id=kp.Id and kp.T2='''' and kp.T5<>'''' and kp.AccountCode =''6*'' and imp.AccountCodeD like''6%'' and imp.T5=kt.T5 group by kp.ReportGroup, kp.ReportType union all select null, kp.ReportGroup, kp.ReportType,'+@sql+' from KPISetting kp , T_imp_raw imp where kp.T2='''' and kp.AccountCode =''6*'' and kp.T5=''R*'' and imp.AccountCodeD like''6%'' and imp.T5 like''R%'' group by kp.ReportGroup, kp.ReportType  ' print @sql EXEC(@sql)";
            SqlDataReader dr = SqlHelp.ExecuteReader(sql);
            DataTable dt = new DataTable("ImportOPEX");
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        #endregion

    }
}

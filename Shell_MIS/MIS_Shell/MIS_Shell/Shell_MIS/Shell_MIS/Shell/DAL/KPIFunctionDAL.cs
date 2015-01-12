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
    /// 添加时间：2014-08-21
    /// 添加目的：一些改变查出数据格式的函数定义
    /// </summary>
  public  class KPIFunctionDAL
    {
      KPIReportDAL kpiDal = new KPIReportDAL();
        #region 第一个步骤 改变table的格式 有多列变成一列(汉字+数据)
                //0,
        /// <summary>
        /// 改变table的格式  转换成最终格式 除去特别格式的
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable changeStyle(DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["Type"].ToString() == "" && table.Rows[i]["ReportGroup"].ToString() != "" && table.Rows[i]["ReportType"].ToString() == "")
                    {
                        table.Columns["Type"].ReadOnly = false;
                        table.Rows[i]["Type"] = table.Rows[i]["ReportGroup"].ToString();
                    }
                    else if (table.Rows[i]["Type"].ToString() == "" && table.Rows[i]["ReportGroup"].ToString() != "" && table.Rows[i]["ReportType"].ToString() != "")
                    {
                        table.Columns["Type"].ReadOnly = false;
                        table.Rows[i]["Type"] = table.Rows[i]["ReportType"].ToString();

                    }
                    else if (table.Rows[i]["Type"].ToString() == "" && table.Rows[i]["ReportGroup"].ToString() == "" && table.Rows[i]["ReportType"].ToString() == "")
                    {
                        table.Rows[i].Delete();
                    }
                   
                }
                table.AcceptChanges();
            }
            table.Columns.Remove("ReportGroup");
            table.Columns.Remove("ReportType");
            table.Columns.Remove("ReportSubType");
            return table;
        }
        /// <summary>
        /// 改变table的格式  转换成最终格式 适合 9，11，12,13，14
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable changeStyles(DataTable table)
        {
            

            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["Type"].ToString() == "" && table.Rows[i]["ReportGroup"].ToString() != "")
                    {
                        table.Columns["Type"].ReadOnly = false;
                        table.Rows[i]["Type"] = table.Rows[i]["ReportGroup"].ToString();
                    }
                    else if (table.Rows[i]["Type"].ToString() == "" && table.Rows[i]["ReportGroup"].ToString() == "" && table.Rows[i]["VGroup"].ToString() == "")
                    {
                        table.Rows[i].Delete();
                    }
                    else if (table.Rows[i]["Type"].ToString() == "" && table.Rows[i]["ReportGroup"].ToString() == "" && table.Rows[i]["ReportType"].ToString() == "" && table.Rows[i]["VGroup"].ToString() != "")
                    {
                         table.Columns["Type"].ReadOnly = false;
                         table.Rows[i]["Type"] = table.Rows[i]["VGroup"].ToString();
                    }
                }
                table.AcceptChanges();
            }
            table.Columns.Remove("ReportGroup");
            table.Columns.Remove("ReportType");
            table.Columns.Remove("VGroup");
            return table;
        }
        /// <summary>
        /// 改变table的格式  转换成最终格式 适合12
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable changeStyle12(DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {

                    if (table.Rows[i]["Type"].ToString() == "" && table.Rows[i]["ReportGroup"].ToString() != "")
                    {
                        table.Columns["Type"].ReadOnly = false;
                        table.Rows[i]["Type"] = table.Rows[i]["ReportGroup"].ToString();
                    }
                }
                table.AcceptChanges();
            }
            table.Columns.Remove("ReportGroup");
            return table;
        }
        /// <summary>
        /// 改变table的格式  转换成最终格式 适合15
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable changeStyle15(DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["Type"].ToString() == "" && table.Rows[i]["ReportType"].ToString() == "" && table.Rows[i]["Item"].ToString() != "")
                    {
                        table.Columns["Type"].ReadOnly = false;
                       // table.Rows[i]["Type"] = table.Rows[i]["Item"].ToString();
                        table.Columns["Type"].MaxLength = 200;
                        table.Rows[i]["Type"] = "C5 BUSINESS CONTRIBUTION";//table.Rows[i]["Item"].ToString();
                    }
                    else if (table.Rows[i]["Type"].ToString() == "" && table.Rows[i]["ReportType"].ToString() == "" && table.Rows[i]["Item"].ToString()=="")
                    {
                          table.Rows[i].Delete();
                    }
                }
              table.AcceptChanges();
            }
            table.Columns.Remove("ReportType");
            table.Columns.Remove("Item");
            return table;
        }
        #endregion

        #region 第二个步骤 将每类中包含的所有类别都显示  没有数据的以0填充
        public DataTable getAllTypes(DataTable dtAllType, DataTable dtData)
        {
            if (dtAllType != null && dtAllType.Rows.Count > 0 && dtData != null && dtData.Rows.Count == 0)
            {
                for (int i = 0; i < dtAllType.Rows.Count; i++)
                {
                    string type = dtAllType.Rows[i]["ReportSubType"].ToString().Trim();
                    bool flag = true;
                    if (flag)
                    {
                        DataRow dr = dtData.NewRow();
                        dr["ReportSubType"] = type;
                        for (int k = 1; k < dtData.Columns.Count; k++)
                        {
                            dr[k] = "0";
                        }
                        dtData.Rows.InsertAt(dr, i);
                    }
                }
            }
           else if (dtAllType != null && dtAllType.Rows.Count > 0 && dtData != null&&dtData.Rows.Count>0)
            {
                for (int i = 0; i < dtAllType.Rows.Count; i++)
                {
                    string type = dtAllType.Rows[i]["ReportSubType"].ToString().Trim();
                    bool flag = true;
                    for (int j = 0; j < dtData.Rows.Count; j++)
                    {
                        if (type == dtData.Rows[j]["ReportSubType"].ToString().Trim())
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        DataRow dr = dtData.NewRow();
                        dr["ReportSubType"] = type;
                        for (int k = 1; k < dtData.Columns.Count; k++)
                        {
                            dr[k] = "0";
                        }
                        dtData.Rows.InsertAt(dr, i);
                    }
                }
            }  
            return dtData;
        }
        #endregion
        #region 第三步骤 将所算出的数据乘以（-1）
      
        //所有查出的数值 乘以（-1）
          public DataTable getLastData(DataTable dt1)
          {
                if(dt1.Rows.Count>0)
                {
                    for (int i = 0; i < dt1.Rows.Count;i++ )
                    {
                        for (int j = 1; j < dt1.Columns.Count;j++ )
                        {
                            dt1.Columns[j].ReadOnly = false;
                            if (dt1.Rows[i][j].ToString().Trim() == "" || dt1.Rows[i][j]==null)
                            {
                                dt1.Rows[i][j] = 0.00;
                            }
                           else if (dt1.Rows[i][j].ToString().Trim() != "0")
                            {
                                double bb = double.Parse(dt1.Rows[i][j].ToString().Trim()) * (-1);
                                dt1.Rows[i][j] = bb.ToString("0.00");
                            }
                        }
                    }
                    
                }
              return dt1;
          }
        #endregion
    }
}

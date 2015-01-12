using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;

namespace MIS_Shell
{
    /// <summary>
    /// 添加人：ydx
    /// 添加时间：2014-10-27
    /// 添加目的：kpi报表里的 T5，T8查询条件
    /// </summary>
    public partial class MIS_KPI_T5 : Form
    {
        JVDSMDCDal DepartmentDal = new JVDSMDCDal();//t5
        TCodeDal tcodeDal = new TCodeDal();//t8
        public static string staticT5 = "";//选定的T5
        public static string staticT8 = "";//选定的T8
        public static string cocdt5t8string = ""; //处理后的字符串
        public MIS_KPI_T5()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MIS_KPI_T5_Load(object sender, EventArgs e)
        {

            string cocds = KPIReport.cocds;//kpi报表里选择的合资公司
            DataTable dtCheckT5 = DepartmentDal.getT5CodeByCocd(cocds);
            //T5复选框 
           // DataTable dtCheckT5 = DepartmentDal.getT5AndJvCodeByCocd(KPIReport.kpiStaticCocd);
            checkedListBoxT5.DataSource = dtCheckT5;//T5
            checkedListBoxT5.ValueMember = "CocdT5Code";
            checkedListBoxT5.DisplayMember = "DeptNameCH";
            //T8复选框
            DataTable dtCheck = tcodeDal.TCodeSelectT8(cocds);
            if (dtCheck.Rows.Count > 0)//T8
            {
                for (int i = 0; i < dtCheck.Rows.Count; i++)
                {
                    checkedListBoxT8.Items.Add(dtCheck.Rows[i][0].ToString().Trim());
                }
            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-27
        /// 添加目的：t5全选
        /// t5 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_all_CheckedChanged(object sender, EventArgs e)
        {
            if (select_all.Checked)
            {
                for (int j = 0; j < checkedListBoxT5.Items.Count; j++)
                {
                    checkedListBoxT5.SetItemChecked(j, true);
                }
            }
            else
            {
                for (int j = 0; j < checkedListBoxT5.Items.Count; j++)
                {
                    checkedListBoxT5.SetItemChecked(j, false);
                }
            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-27
        /// 添加目的：t8全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T8CheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (T8CheckAll.Checked)
            {
                for (int j = 0; j < checkedListBoxT8.Items.Count; j++)
                {
                    checkedListBoxT8.SetItemChecked(j, true);
                }
            }
            else
            {
                for (int j = 0; j < checkedListBoxT8.Items.Count; j++)
                {
                    checkedListBoxT8.SetItemChecked(j, false);
                }
            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-27
        /// 添加目的:确定 按钮 触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            string strCollectedT5 = "";
            string strCollectedT8 = "";
            for (int i = 0; i < checkedListBoxT5.CheckedItems.Count; i++)
            {
                DataRowView dv = ((DataRowView)checkedListBoxT5.CheckedItems[i]);
                if (strCollectedT5 == "")
                {
                    strCollectedT5 = "''" + dv["CocdT5Code"].ToString().Trim() + "''";
                }
                else
                {
                    strCollectedT5 = strCollectedT5 + ",''" + dv["CocdT5Code"].ToString().Trim() + "''";
                }
            }
            staticT5 = strCollectedT5;
            for (int i = 0; i < checkedListBoxT8.Items.Count; i++)
            {
                if (checkedListBoxT8.GetItemChecked(i))
                {
                    if (strCollectedT8 == "")
                    {
                        strCollectedT8 = "''" + checkedListBoxT8.GetItemText(checkedListBoxT8.Items[i]) + "''";
                    }
                    else
                    {
                        strCollectedT8 = strCollectedT8 + ",''" + checkedListBoxT8.GetItemText(checkedListBoxT8.Items[i]) + "''";
                    }
                }
            }
            staticT8 = strCollectedT8;

            // 处理cocd和t5  变成cn13:20,21,32;cn16:234,234
            #region 处理成这种格式的字符串 cocd:t5:t8;

            string[] t8string = staticT8.Split(',');

            string T5 = "";
            string t8 = "";
            string cocd = "";
            cocdt5t8string = "";
            if (staticT5 != "")
            {
                string[] t5string = staticT5.Split(',');
                for (int i = 0; i < t5string.Length; i++)
                {
                    cocd = t5string[i].Substring(0, t5string[i].IndexOf('-'));//cn16
                    T5 = "''" + t5string[i].Substring(t5string[i].IndexOf('-') + 1);//T5 R60989
                    //如果不包含该cocd 则添加cocd
                    if (!cocdt5t8string.Contains(cocd))
                    {
                        cocdt5t8string += ";" + cocd + "'':";
                        cocdt5t8string += T5 + ",";
                    }
                    else
                    {
                        cocdt5t8string += T5 + ",";
                    }
                }
              //cocdt5t8string=cocdt5t8string.Replace(",;", ";"); 
             }
            else
            {
                cocdt5t8string += "null:";
            }


            string cocdT8 = "";
            string cocdT82 = "";
            if (staticT8 != "")
            {
                cocdT82 = t8string[0].Substring(0, t8string[0].IndexOf('-'))+"''";//cn12
            }
            #region  拼接t8
            if (staticT8 != "")
            {
                for (int j = 0; j < t8string.Length; j++)
                {
                    cocdT8 = t8string[j].Substring(0, t8string[j].IndexOf('-'))+"''";//t8串中的cocd ：cn12
                    if (cocdt5t8string.Contains(cocdT8))//如果里面有cn12
                    {
                        if (cocdT82 == cocdT8)
                        {
                            cocdT82 = cocdT8;//都是cn12，则继续拼接
                            t8 += "''" + t8string[j].Substring(t8string[j].IndexOf('-') + 1) + ",";//T8 20
                            if (j == t8string.Length - 1)
                            {
                                t8 = t8.Substring(0, t8.Length - 1) + ":";
                                int cocdindex = cocdt5t8string.IndexOf(cocdT82 + ":");
                                cocdt5t8string = cocdt5t8string.Insert(cocdindex + (cocdT82.Length + 1), t8);
                                cocdT82 = cocdT8;
                                t8 = "";
                            }

                        }
                        else
                        {
                            //如果循环下一次不是cn12，则把刚才拼接的cn12的t8插入到cn12:后面
                            t8 = t8.Substring(0, t8.Length - 1) + ":";
                            int cocdindex = cocdt5t8string.IndexOf(cocdT82 + ":");
                            cocdt5t8string = cocdt5t8string.Insert(cocdindex + (cocdT82.Length + 1), t8);
                            cocdT82 = cocdT8;
                            t8 = "";
                            t8 += "''" + t8string[j].Substring(t8string[j].IndexOf('-') + 1) + "'',";//T8 20
                        }
                    }
                    else
                    {
                        cocdt5t8string += ";" + cocdT8 + ":";
                        if (t8 != "")
                        {
                            cocdt5t8string += "null";
                            t8 = t8.Substring(0, t8.Length - 1) + ":";//把最后的逗号改为：
                            int cocdindex = cocdt5t8string.IndexOf(cocdT82 + ":");
                            cocdt5t8string = cocdt5t8string.Insert(cocdindex + (cocdT82.Length + 1), t8);
                            t8 = "";
                            t8 += "''" + t8string[j].Substring(t8string[j].IndexOf('-') + 1) + "'',";//T8 20
                            cocdT82 = cocdT8;
                        }
                        else
                        {
                            t8 += "''" + t8string[j].Substring(t8string[j].IndexOf('-') + 1) + "'',";//T8 20
                            cocdt5t8string += "null";
                        }
                    }
                }

            }
            else
            {
                //没选择t8
                if (staticT5 != "")
                {
                    string[] t5string = staticT5.Split(',');
                    string cocd2 = "";
                    for (int i = 0; i < t5string.Length; i++)
                    {
                        cocd = t5string[i].Substring(0, t5string[i].IndexOf('-'))+"''";//cn16
                        //cocd2 = cocd;//cocd2存储循环的上一次的值
                        if (cocd2 != cocd)
                        {
                            cocdt5t8string = cocdt5t8string.Insert(cocdt5t8string.IndexOf(cocd + ":") + (cocd.Length + 1), "null:");
                            cocd2 = cocd;//cocd2存储循环的上一次的值
                        }

                    }
                }

            }
            #endregion

            cocdt5t8string = cocdt5t8string.Substring(1, cocdt5t8string.Length - 1);
            if (cocdt5t8string.IndexOf(';') > 0)
            {
                cocdt5t8string = cocdt5t8string.Remove(cocdt5t8string.IndexOf(';') - 1, 1) + ";";
            }
            else
            {
                cocdt5t8string += ";";
            }
           
            cocdt5t8string = cocdt5t8string.Replace(",;",";");
            cocdt5t8string = cocdt5t8string.Replace("''''","''");
            #endregion

            this.Close();
            this.Close();
        }
       
        
    }
}

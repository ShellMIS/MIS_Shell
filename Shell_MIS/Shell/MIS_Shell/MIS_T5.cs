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
    /// 添加目的：费用报表里的 T5，T8查询条件
    /// </summary>
    public partial class MIS_T5 : Form
    {
        JVDSMDCDal DepartmentDal = new JVDSMDCDal();//t5
        TCodeDal tcodeDal = new TCodeDal();//t8
        public static  string staticT5 = "";//选定的T5
        public static  string staticT8 = "";//选定的T8
        public MIS_T5()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-27
        /// 添加目的：根据选择的合资公司显示此公司下的T5，T8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MIS_T5_Load(object sender, EventArgs e)
        {
               //T5复选框 
              DataTable  dtCheckT5 = DepartmentDal.getT5CodeByCocd(OpexReport.staticCocd);
              checkedListBoxT5.DataSource = dtCheckT5;//T5
              checkedListBoxT5.ValueMember = "T5Code";
              checkedListBoxT5.DisplayMember = "DeptNameCH";
              //T8复选框
              DataTable dtCheck = tcodeDal.TCodeSelectT8(OpexReport.staticCocd);
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
        /// 添加目的：全选 复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_all_CheckedChanged(object sender, EventArgs e)
        {
            if(select_all.Checked)
            {
                for (int j = 0; j < checkedListBoxT5.Items.Count;j++ )
                {
                    checkedListBoxT5.SetItemChecked(j,true);
                }
            }else
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
        /// 添加目的：t8复选框上的 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T8CheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if(T8CheckAll.Checked)
            {
                for (int j = 0; j < checkedListBoxT8.Items.Count;j++ )
                {
                    checkedListBoxT8.SetItemChecked(j,true);
                }
            }else
            {
                for (int j = 0; j < checkedListBoxT8.Items.Count;j++ )
                {
                    checkedListBoxT8.SetItemChecked(j,false);
                }
            }
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-10-27
        /// 添加目的:确定按钮 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click(object sender, EventArgs e)
        {
            string strCollectedT5 = "";
            string strCollectedT8 = "";
            for (int i = 0; i < checkedListBoxT5.CheckedItems.Count; i++)
            {
                DataRowView dv = ((DataRowView)checkedListBoxT5.CheckedItems[i]);
                if (strCollectedT5 == "")
                {
                    strCollectedT5 = "''" + dv["T5Code"].ToString().Trim() + "''";
                }
                else
                {
                    strCollectedT5 = strCollectedT5 + ",''" + dv["T5Code"].ToString().Trim() + "''";
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
            this.Close();
        }
    }
}

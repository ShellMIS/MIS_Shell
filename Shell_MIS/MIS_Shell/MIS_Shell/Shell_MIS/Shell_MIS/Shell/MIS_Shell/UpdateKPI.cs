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
    /// 添加时间：2014-07-28
    /// 添加目的：kPI字典修改
    /// </summary>
    public partial class UpdateKPI : Form
    {
        public UpdateKPI()
        {
            InitializeComponent();
        }

        KPIDAL kPIDal = new KPIDAL();
        DataTable dt = new DataTable();
        /// <summary>
        /// 窗体加载 2014-07-28
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateKPI_Load(object sender, EventArgs e)
        {
            //T1
            //this.comboT1U.DataSource = kPIDal.T1Select();
            //comboT1U.DisplayMember = "T1";
            //comboT1U.ValueMember = "T1";
            ////T2
            //this.comboT2U.DataSource = kPIDal.T2Select();
            //comboT2U.DisplayMember = "T2";
            //comboT2U.ValueMember = "T2";
            ////T5
            //this.comboT5U.DataSource = kPIDal.T5Select();
            //comboT5U.DisplayMember = "T5";
            //comboT5U.ValueMember = "T5";

            int ID = KPISetting.ID;
            try
            {

                dt = kPIDal.KPISettingSelect(ID);
                this.txtId.Text = ID.ToString();
                this.textT2U.Text = dt.Rows[0]["T2"].ToString();
                this.textT5U.Text = dt.Rows[0]["T5"].ToString();
                this.txtItemU.Text = dt.Rows[0]["Item"].ToString();
                this.txtItemDesc.Text = dt.Rows[0]["ItemDescription"].ToString();
                this.txtCodeU.Text = dt.Rows[0]["Code"].ToString();
                this.txtCodeDescU.Text = dt.Rows[0]["CodeDescription"].ToString();
                this.txtReportGroupU.Text = dt.Rows[0]["ReportGroup"].ToString();
                this.txtReportTypeU.Text = dt.Rows[0]["ReportType"].ToString();
                this.txtReportSubU.Text = dt.Rows[0]["ReportSubType"].ToString();
                this.txtAccountCodeU.Text = dt.Rows[0]["AccountCode"].ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }
        }
        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_up_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = KPISetting.ID;
                //string T1 = comboT1U.SelectedValue.ToString().Trim();
                //string T2 = comboT2U.SelectedValue.ToString().Trim();
                //string T5 = comboT5U.SelectedValue.ToString().Trim();
                string T2 = this.textT2U.Text.ToString().Trim();
                string T5 = this.textT5U.Text.ToString().Trim();
                 string item=this.txtItemU.Text.ToString().Trim();
                 string itemDesc = this.txtItemDesc.Text.ToString().Trim();
                 string code = this.txtCodeU.Text.ToString().Trim();
                 string codeDesc = this.txtCodeDescU.Text.ToString().Trim();
                 string RGroup = this.txtReportGroupU.Text.ToString().Trim();
                 string RType = this.txtReportTypeU.Text.ToString().Trim();
                 string RSubType = this.txtReportSubU.Text.ToString().Trim();
                 string accountCode = this.txtAccountCodeU.Text.ToString().Trim();
               
                     if (kPIDal.updateKPISetting(ID, item, itemDesc, code, codeDesc, RGroup, RType, RSubType, accountCode,T2,T5) == 1)
                    {
                        int b = kPIDal.deleteKPItemp(ID.ToString().Trim()); //删除字表的对应项 
                        
                             if (T2 != "" && T5 == "")
                             {
                                 //获得t2
                                 string[] arr = T2.Split(',');
                                 for (int i = 0; i < arr.Length; i++)
                                 {
                                     if (kPIDal.kpiTempInsert(ID.ToString().Trim(), arr[i].ToString().Trim(), "") != 1)
                                     {
                                         MessageBox.Show("有异常");
                                     }
                                 }
                             }
                             else if (T2 == "" && T5 != "" && T5 != "R*")
                             {
                                 //获得t5
                                 string[] strT5 = T5.Split(',');
                                 for (int i = 0; i < strT5.Length; i++)
                                 {
                                     if (kPIDal.kpiTempInsert(ID.ToString().Trim(), "", strT5[i].ToString().Trim()) != 1)
                                     {
                                         MessageBox.Show("有异常");
                                     }
                                 }

                             }
                         
                        MessageBox.Show("修改成功！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                    }
                
                //}
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }

        }
    }
}

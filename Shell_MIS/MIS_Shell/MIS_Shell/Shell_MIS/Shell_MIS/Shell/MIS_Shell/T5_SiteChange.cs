using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using System.Net;
using MIS_Shell.CommExcel;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

namespace MIS_Shell
{
    public partial class T5_SiteChange : Form
    {
        public T5_SiteChange()
        {
            InitializeComponent();
        }

        JVDal jvdal = new JVDal();
        JVDSMDCDal jvDep = new JVDSMDCDal();
        T5SiteDAL siteDal = new T5SiteDAL();
        OptionLogDAL optionDal = new OptionLogDAL();
        int UserID = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户ID
        
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：
        /// 窗体加载绑定合资公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T5_SiteChange_Load(object sender, EventArgs e)
        {
            //合资公司下拉列表
            DataTable jv = new DataTable();
            jv = jvdal.JVSelectImport();
            DataRow dr = jv.NewRow();
            dr["cb"] = "请选择";
            dr["Cocd"] = "0";
            jv.Rows.InsertAt(dr,0);
            this.cb_Cocd.DataSource = jv;//添加
            this.cb_Cocd.DisplayMember = "cb";
            this.cb_Cocd.ValueMember = "CoCd";


        }

       /// <summary>
       ///添加人：ydx
       ///添加时间：2014-12-29
       ///添加目的：合资公司下拉列表发生改变时 显示此公司下的T5（Site部分）
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void cb_Cocd_SelectedValueChanged(object sender, EventArgs e)
        {

            if (this.cb_Cocd.SelectedValue.ToString().Trim() != "System.Data.DataRowView" && this.cb_Cocd.SelectedValue.ToString().Trim() != "0")
            {
                
                    //获得此合资公司下的T5（Site部分）
                    DataTable table = jvDep.getT5SiteByCocd(cb_Cocd.SelectedValue.ToString().Trim());
                    DataRow dr = table.NewRow();
                    dr["DeptNameCH"] = "请选择";
                    dr["T5Code"] = "0";
                    table.Rows.InsertAt(dr, 0);
                    this.cb_OldSite.DataSource = table;//添加
                    this.cb_OldSite.DisplayMember = "DeptNameCH";
                    this.cb_OldSite.ValueMember = "T5Code";

            }
        }
        /// <summary>
        /// 添加
        /// 添加时间：2014-12-29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_Insert_Click(object sender, EventArgs e)
        {
            string cocd = cb_Cocd.SelectedValue.ToString().Trim();
           // string oldT5 = cb_OldSite.SelectedValue.ToString().Trim();//
            string oldT5="";
            if (cb_OldSite.SelectedValue == null)
            {
                oldT5 = cb_OldSite.Text.ToString().Trim();//不是下拉的、是手写的

            }
            else
            {
                cb_OldSite.SelectedValue.ToString().Trim();
            }

            string newT5 = txt_NewT5.Text.ToString().Trim();
            if (cocd == "0")
            {
                MessageBox.Show("合资公司必填");
            }
            else
            {
                if (oldT5 == "0"||oldT5=="")
                {
                    MessageBox.Show("Old_Site必填,或者手动输入");
                }
                else
                {
                    if (newT5 == "")
                    {
                        MessageBox.Show("New_Site必填");
                    }
                    else
                    {
                        try
                        {
                            int insertId = siteDal.T5Site_Insert(oldT5, newT5, cocd, "2014-001");
                            if (insertId != 0)
                            {

                                int result =optionDal.OpertionLogInsert(UserID, "T5替换页面管理_添加OldT5:"+oldT5+",NewT5:"+newT5, DateTime.Now.ToString(), "添加成功！");
                                if (result <= 0)
                                {
                                    MessageBox.Show("T5替换，日志插入失败！");

                                }
                                MessageBox.Show("T5替换，添加成功");
                                dataGridView2.DataSource = siteDal.T5SiteSelect(cocd);
                            }
                            else
                            {
                                int result = optionDal.OpertionLogInsert(UserID, "T5替换页面管理_添加OldT5:" + oldT5 + ",NewT5:" + newT5, DateTime.Now.ToString(), "添加失败！");
                                if (result <= 0)
                                {
                                    MessageBox.Show("T5替换，日志插入失败！");
                                }
                                MessageBox.Show("T5替换，添加失败");
                            }
                        }catch(Exception ex)
                        {
                            int result = optionDal.OpertionLogInsert(UserID, "T5替换页面管理_添加OldT5:" + oldT5 + ",NewT5:" + newT5, DateTime.Now.ToString(), "添加异常！");
                            if (result <= 0)
                            {
                                MessageBox.Show("T5替换，日志插入失败！");
                            }
                            MessageBox.Show("T5New,T5Old映射关系添加异常");
                        }
                    }
                }
            }

        }

       
    }
}

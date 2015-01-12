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
    /// 添加目的：试算平衡表历史数据导入
    /// </summary>
    public partial class DataImport : Form
    {
        public DataImport()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        JVDal jvdal = new JVDal();//合资公司下拉列表
        private void DataImport_Load(object sender, EventArgs e)
        {
            dt = jvdal.JVSelectImport();
            DataRow dr = dt.NewRow();
            dr["cb"] = "请选择";
            dr["CoCd"] = "请选择";
            dt.Rows.InsertAt(dr, 0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.combo_Company.DataSource = dt;
                combo_Company.DisplayMember = "cb";
                combo_Company.ValueMember = "CoCd";
            }
        }
        /// <summary>
        ///添加人：ydx
        ///添加时间：2014-10-20
        ///添加目的：试算平衡表历史数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}

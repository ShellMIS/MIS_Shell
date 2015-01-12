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
    /// 添加时间：2014-10-20
    /// 添加目的：试算平衡表 数据追踪
    /// </summary>
    public partial class DataListDB : Form
    {
        public DataListDB()
        {
            InitializeComponent();
        }
        DataTable table = new DataTable();
        ImportDal import = new ImportDal();
        private void DataListDB_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            table = import.dataList(TB.accountCodeD);
            dataGridView1.DataSource = table;
            TB.accountCodeD = "";
            dataGridView1.Font = new Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);

        }
    }
}

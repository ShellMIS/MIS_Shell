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
    public partial class DepartmentManager : Form
    {
        DepartmentDAL deparDal = new DepartmentDAL();
        DataTable dt = new DataTable();
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());
        OptionLogDAL logDal = new OptionLogDAL();
        public static DepartmentManager _DepartmentManager;
        public static string ID;

        public DepartmentManager()
        {
            InitializeComponent();


        }
        //添加操作
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataGridDepart.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridDepart.Font, FontStyle.Bold);
            string Name = txtDepartmentName.Text.Trim();
            string EnglishName = txtDepartmentEnglish.Text.Trim();

            if (Name == "" || EnglishName == "")
            {
                MessageBox.Show("有空白项，请填写完整！");
                return;
            }
            DataTable dt = deparDal.GetDepartment(" and DepartmentName='" + Name + "'");
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("已经存在该部门！");
                return;
            }
            else
            {
                int result = deparDal.InsertDepartment(Name, EnglishName);
                if (result == 1)
                {
                    lbmsg.Text = "添加部门成功！";
                    lbmsg.ForeColor = System.Drawing.Color.Green;
                    DataGridDepart.DataSource = DepartmentDataSource();
                    int addDepartResult = logDal.OpertionLogInsert(userid, "添加部门", DateTime.Now.ToString(), "添加部门成功！");
                    if (addDepartResult < 0)
                    {
                        MessageBox.Show("插入日志失败！");
                        return;
                    }
                }
                else
                {
                    lbmsg.Text = "添加部门失败！";
                    lbmsg.ForeColor = System.Drawing.Color.Red;
                    int addDepartResult = logDal.OpertionLogInsert(userid, "添加部门", DateTime.Now.ToString(), "添加部门失败！");
                    if (addDepartResult < 0)
                    {
                        MessageBox.Show("插入日志失败！");
                    }
                    return;
                }
            }
            txtDepartmentName.Text = "";
            txtDepartmentEnglish.Text = "";

        }

        //列表数据源
        private DataTable DepartmentDataSource()
        {
            dt = deparDal.GetDepartment("");
            return dt;
        }

        //双击修改
        private void DataGridDepart_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DepartmentAlter departAlter = new DepartmentAlter();
            ID = DataGridDepart.CurrentRow.Cells[0].Value.ToString();
            departAlter.ShowDialog();
            departAlter.Owner = this;
            DataGridDepart.DataSource = DepartmentDataSource();
            lbmsg.Text = "";

        }

        private void DepartmentManager_Load(object sender, EventArgs e)
        {
            DataGridDepart.DataSource = DepartmentDataSource();
            lbmsg.Text = "";
            DataGridDepart.DefaultCellStyle.Font = new Font("宋体", 10);
            DataGridDepart.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            DataGridDepart.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridDepart.Font, FontStyle.Bold);

        }
        //查询
        private void btnSelect_Click(object sender, EventArgs e)
        {
            lbmsg.Text = "";
            DataGridDepart.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridDepart.Font, FontStyle.Bold);
            string Name = txtSName.Text.Trim();
            string EnglishName = txtSEnglish.Text.Trim();
            if (cbSelect.Checked)
            {
                     dt = deparDal.GetDepartment(" and DepartmentName = '" + Name + "' and DepartmentEnglish = '" + EnglishName + "'");
            }
            else
            {
                dt = deparDal.GetDepartment(" and DepartmentName like '%" + Name + "%' and DepartmentEnglish like '%" + EnglishName + "%'");
            }
         
            DataGridDepart.DataSource = dt;
        }

        private void DataGridDepart_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex < 0 || e.RowIndex < 0 || DataGridDepart.Rows.Count <= 0) return;
            DataGridDepart.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = (DataGridDepart.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? string.Empty).ToString();
        
        }




    }
}

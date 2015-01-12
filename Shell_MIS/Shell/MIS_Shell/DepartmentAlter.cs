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
    public partial class DepartmentAlter : Form
    {
        DepartmentDAL departDal = new DepartmentDAL();
        string Id;
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());
        OptionLogDAL logDal = new OptionLogDAL();
        public DepartmentAlter()
        {
            InitializeComponent();
        }

        private void DepartmentAlter_Load(object sender, EventArgs e)
        {
            //获取双击的时候的ID
            Id = DepartmentManager.ID;
            string strWhere = " and ID='" + Id + "'";
            DataTable dt = departDal.GetDepartment(strWhere);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string DeName = dt.Rows[0]["部门名称"].ToString();
                    txtDepartName.Text = DeName;
                    string English = dt.Rows[0]["部门英文缩写"].ToString();
                    txtEnglish.Text = English;
                }
            }
        }

        //修改
        private void btnAlter_Click(object sender, EventArgs e)
        {
            string departmentName = txtDepartName.Text.Trim();
            string departmentEnglish = txtEnglish.Text.Trim();
            int result = 0;
            try
            {
                result = departDal.AlterDepartment(Id, departmentEnglish, departmentName);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }
            if (result>0)
            {
                lbmsg.Text = "修改成功!";
                lbmsg.ForeColor = System.Drawing.Color.Green;
                int UpdateDepartResult = logDal.OpertionLogInsert(userid, "修改部门信息", DateTime.Now.ToString(), "修改部门信息成功！");
                if (UpdateDepartResult < 0)
                {
                    MessageBox.Show("插入日志失败！");
                }
                this.Close();
            }
            else
            {
                lbmsg.Text = "修改失败!";
                lbmsg.ForeColor = System.Drawing.Color.Red;
                lbmsg.ForeColor = System.Drawing.Color.Green;
                int UpdateDepartResult = logDal.OpertionLogInsert(userid, "修改部门信息", DateTime.Now.ToString(), "修改部门信息失败！");
                if (UpdateDepartResult < 0)
                {
                    MessageBox.Show("插入日志失败！");
                    return;
                }
            }
        }
    }
}

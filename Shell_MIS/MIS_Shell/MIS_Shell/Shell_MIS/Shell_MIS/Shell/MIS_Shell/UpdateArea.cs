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
    public partial class UpdateArea : Form
    {
        public UpdateArea()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        AreaDal areadal = new AreaDal();
        JVDal jd = new JVDal();
        private void UpdateArea_Load(object sender, EventArgs e)
        {
            cCocd.DataSource = jd.JVCocd();
            cCocd.DisplayMember = "cdNameEn";
            cCocd.ValueMember = "CoCd";
            try
            {
                int ID = AreaSetting.ID;
                dt = areadal.AreaSelect(ID);
                this.txt_ID.Text = ID.ToString();
                this.txt_ACT0.Text = dt.Rows[0]["AreaCodeT0"].ToString();
                this.txt_ANCH.Text = dt.Rows[0]["AreaNameCH"].ToString();
                this.txt_ANEN.Text = dt.Rows[0]["AreaNameEN"].ToString();
                this.cCocd.SelectedValue = dt.Rows[0]["CoCd"].ToString();

            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = AreaSetting.ID;
                string AreaCodeT0 = this.txt_ACT0.Text.ToString().Trim();
                string AreaNameCH = this.txt_ANCH.Text.ToString().Trim();
                string AreaNameEN = this.txt_ANEN.Text.ToString().Trim();
                string CoCd = this.cCocd.SelectedValue.ToString().Trim();
                DataTable dtExists = areadal.AreaSelect(AreaCodeT0, AreaNameCH, AreaNameEN, CoCd, ID.ToString());
                if (dtExists.Rows.Count > 0)
                {
                    MessageBox.Show("已经存在此记录：" + AreaCodeT0);
                }
                else
                {
                    if (areadal.AreaUpdate(ID, AreaCodeT0, AreaNameCH, AreaNameEN, CoCd) == 1)
                    {
                        MessageBox.Show("修改成功！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                    }
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }
        }
    }
}

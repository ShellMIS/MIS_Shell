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
    public partial class UpdateSiteCategory : Form
    {
        
        DataTable dt = new DataTable();
        SiteCategoryDal siteDal = new SiteCategoryDal();
        public UpdateSiteCategory()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 修改按钮提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Up_Click(object sender, EventArgs e)
        {
            try
            {
                int id = MIS_SiteCategotySetting.ID;
                DataTable dtExis = siteDal.SiteCategorySelect(this.textSites1.Text.ToString().Trim(), this.textSite2.Text.ToString().Trim(), this.textSiteName.Text.ToString().Trim());
                if(dtExis.Rows.Count>0)
                {
                    MessageBox.Show("已经存在此记录:"+textSites1.Text);
                }else
                {
                    if (siteDal.SiteCategoryUpdate(id, this.textSites1.Text.ToString().Trim(), this.textSite2.Text.ToString().Trim(), this.textSiteName.Text.ToString().Trim()) == 1)
                    {
                        MessageBox.Show("修改成功");
                        this.textSiteId.Text = "";
                        this.textSiteId.ReadOnly = true;
                        this.textSites1.Text = "";
                        this.textSite2.Text = "";
                        this.textSiteName.Text = "";
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("修改失败！");
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败！" + ex.Message);
            }
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateSiteCategory_Load(object sender, EventArgs e)
        {
            try
            {
                int id = MIS_SiteCategotySetting.ID;
                dt = siteDal.SiteCategorySelect(id);
                this.textSiteId.Text = id.ToString();
                this.textSiteId.ReadOnly = true;
                this.textSites1.Text = dt.Rows[0]["SiteCategory1"].ToString();
                this.textSite2.Text = dt.Rows[0]["SiteCategory2"].ToString();
                this.textSiteName.Text = dt.Rows[0]["SiteCategoryName"].ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show("操作错误" + ex.Message);
            }
        }
    }
}

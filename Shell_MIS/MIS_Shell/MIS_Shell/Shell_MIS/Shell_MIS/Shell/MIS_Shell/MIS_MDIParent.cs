using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using DAL;

namespace MIS_Shell
{
    public partial class MIS_MDIParent : Form
    {
        private int childFormNumber = 0;
        RolePrivilegeDAL rolePriDal = new RolePrivilegeDAL();
        UserAndRole URoleDal = new UserAndRole();
        int UserID = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());
        public MIS_MDIParent()
        {
            InitializeComponent();

            系统背景 xtbj = new 系统背景();
            xtbj.MdiParent = this;
            xtbj.WindowState = FormWindowState.Maximized;
            xtbj.Dock = DockStyle.Fill;
            xtbj.Text = "壳牌MIS系统";
            xtbj.Show();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }
        /// <summary>
        /// 状态栏 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }
        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }
        private void txtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }
        private void htmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //MIS_Report misreport = new MIS_Report();
            //misreport.MdiParent = this;
            //misreport.WindowState = FormWindowState.Maximized;
            //misreport.Text = "报表功能";
            //misreport.Show();
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加目的：合资公司管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JVsetting_Click(object sender, EventArgs e)
        {
            JVSetting jvsetting = new JVSetting();
            jvsetting.MdiParent = this;
            jvsetting.WindowState = FormWindowState.Normal;
            jvsetting.Text = "JVsetting";
            jvsetting.Dock = DockStyle.Fill;
            jvsetting.Show();
         
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加目的：一级分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Groupsetting_Click(object sender, EventArgs e)
        {
            GroupSetting groupsetting = new GroupSetting();
            groupsetting.MdiParent = this;
            groupsetting.WindowState = FormWindowState.Maximized;
            groupsetting.Text = "groupsetting";
            groupsetting.Dock = DockStyle.Fill;
            groupsetting.Show();
       
        }
        private void areaSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AreaSetting areasetting = new AreaSetting();
            areasetting.MdiParent = this;
            areasetting.WindowState = FormWindowState.Maximized;
            areasetting.Text = "areasetting";
            areasetting.Dock = DockStyle.Fill;
            areasetting.Show();
          
        }
        private void jVDepartmentSiteMasterDataCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JVDSMDC jvdsmdc = new JVDSMDC();
            jvdsmdc.MdiParent = this;
            jvdsmdc.WindowState = FormWindowState.Maximized;
            jvdsmdc.Text = "jVDepartmentSiteMasterDataCollectionToolStripMenuItem";
            jvdsmdc.Dock = DockStyle.Fill;
            jvdsmdc.Show();

        }
        private void tCodeSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TCodeSetting tcodesetting = new TCodeSetting();
            tcodesetting.MdiParent = this;
            tcodesetting.WindowState = FormWindowState.Maximized;
            tcodesetting.Text = "tcodesetting";
            tcodesetting.Dock = DockStyle.Fill;
            tcodesetting.Show();
        }
        private void cOASettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            COASetting coasetting = new COASetting();
            coasetting.MdiParent = this;
            coasetting.WindowState = FormWindowState.Maximized;
            coasetting.Text = "coasetting";
            coasetting.Dock = DockStyle.Fill;
            coasetting.Show();
        }
        private void bSSettingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BSSetting bssetting = new BSSetting();
            bssetting.MdiParent = this;
            bssetting.WindowState = FormWindowState.Maximized;
            bssetting.Text = "bs setting";
            bssetting.Dock = DockStyle.Fill;
            bssetting.Show();
      
        }
        private void pLSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PLSetting plsetting = new PLSetting();
            plsetting.MdiParent = this;
            plsetting.WindowState = FormWindowState.Maximized;
            plsetting.Text = "bs setting";
            plsetting.Show();
        }
        /// <summary>
        /// PLDB 
        /// ydx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pLDBSettingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PLDBSetting pldbsetting = new PLDBSetting();
            pldbsetting.MdiParent = this;
            pldbsetting.WindowState = FormWindowState.Maximized;
            pldbsetting.Text = "PLDB setting";
            pldbsetting.Dock = DockStyle.Fill;
            pldbsetting.Show();
        }
        /// <summary>
        /// OPEXSetting 
        /// ydx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oPEXSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OPEX_Setting opexSetting = new OPEX_Setting();
            opexSetting.MdiParent = this;
            opexSetting.WindowState = FormWindowState.Maximized;
            opexSetting.Text = "OPEX setting";
            opexSetting.Dock = DockStyle.Fill;
            opexSetting.Show();
        }
        /// <summary>
        /// 控制所有页面不显示 最大化、最小化 
        /// ydx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (e.Item.Text.Length == 0 || e.Item.Text == "还原(&R)" || e.Item.Text == "最小化(&N)")
            {
                e.Item.Visible = false;
            }
        }

        private void 数据包导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MIS_Import misimport = new MIS_Import();
            misimport.MdiParent = this;
            misimport.WindowState = FormWindowState.Maximized;
            misimport.Text = "导入功能";
            misimport.Dock = DockStyle.Fill;
            misimport.Show();
        }
        //用户组管理页面  ydx
        private void 用户组管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MIS_RoleManager mum = new MIS_RoleManager();
            mum.MdiParent = this;
            mum.WindowState = FormWindowState.Maximized;
            mum.Text = "角色管理";
            mum.Dock = DockStyle.Fill;
            mum.Show();
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-11-04
        /// 添加目的：获取菜单栏里的 菜单项
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private ToolStripMenuItem GetControl(string name)
        {
            object o = this.GetType().GetField(name, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            return ((ToolStripMenuItem)o);
        }
        /// <summary>
        /// 判断是普通用户还是管理员  不同权限显示的页面有区别 
        /// ydx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MIS_MDIParent_Load(object sender, EventArgs e)
        {
            //在加载之前先把所有菜单的可见性都设置成 不可见
            DataTable tableAll = new DataTable();
            tableAll = rolePriDal.PriTargeByRoleId();
            if (tableAll != null && tableAll.Rows.Count > 0)
            {
                for (int j = 0; j < tableAll.Rows.Count; j++)
                {
                    if (tableAll.Rows[j][0].ToString().Trim()!="")
                    {
                        ToolStripMenuItem mi = this.GetControl(tableAll.Rows[j][0].ToString().Trim());
                        mi.Visible = false;
                    }
                    
                }
            }
            //根据实际的角色来控制所有菜单的可见性
            DataTable table = new DataTable();
            DataTable roleIds = URoleDal.getRoleIdsByUserId(MIS_Login.userId);//一个用户可能是多个角色
            string rolIds = "";
            if (roleIds != null && roleIds.Rows.Count > 0)
            {
                for (int i = 0; i < roleIds.Rows.Count; i++)
                {
                    if (rolIds == "")
                    {
                        rolIds = roleIds.Rows[i][0].ToString().Trim();
                    }
                    else
                    {
                        rolIds += "," + roleIds.Rows[i][0].ToString().Trim();
                    }
                }

            }
            table = rolePriDal.PriTargeByRoleId(rolIds);//根据角色查出此角色下的所有权限
            if (table != null && table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i][0].ToString().Trim()!="")
                    {
                        ToolStripMenuItem mi = this.GetControl(table.Rows[i][0].ToString().Trim());
                        mi.Visible = true;
                    }
                  
                }
            }
        }
        /// <summary>
        /// 数据库备份、还原
        /// ydx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 数据备份恢复ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MIS_BackDatabase backDatabase = new MIS_BackDatabase();
            backDatabase.MdiParent = this;
            backDatabase.WindowState = FormWindowState.Maximized;
            backDatabase.Text = "数据库备份、还原";
            backDatabase.Dock = DockStyle.Fill;
            backDatabase.Show();
        }
        /// <summary>
        /// 操作日志  ydx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 操作日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MIS_OpertionLog MOpertionLog = new MIS_OpertionLog();
            MOpertionLog.MdiParent = this;
            MOpertionLog.WindowState = FormWindowState.Maximized;
            MOpertionLog.Text = "操作日志管理";
            MOpertionLog.Dock = DockStyle.Fill;
            MOpertionLog.Show();
        }

        private void jVCOASettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JV_COASetting jvcoasetting = new JV_COASetting();
            jvcoasetting.MdiParent = this;
            jvcoasetting.WindowState = FormWindowState.Maximized;
            jvcoasetting.Text = "合资公司COAsetting";
            jvcoasetting.Dock = DockStyle.Fill;
            jvcoasetting.Show();
        }
        /// <summary>
        /// 分类管理 2014-07-23
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void siteCategorySettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MIS_SiteCategotySetting siteCategory = new MIS_SiteCategotySetting();
            siteCategory.MdiParent = this;
            siteCategory.WindowState = FormWindowState.Maximized;
            siteCategory.Text = "油站分类SiteCategorySetting";
            siteCategory.Dock = DockStyle.Fill;
            siteCategory.Show();
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-07-22
        /// 添加目的：二级分组管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void group2SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Group2Setting group2setting = new Group2Setting();
            group2setting.MdiParent = this;
            group2setting.WindowState = FormWindowState.Maximized;
            group2setting.Text = "组别2级分类";
            group2setting.Dock = DockStyle.Fill;
            group2setting.Show();
        }
        /// <summary>
        /// 费用报告
        ///添加人 ydx
        ///添加时间：2014-07-26
        ///添加目的：费用报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 费用报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpexReport opexReport = new OpexReport();
            opexReport.MdiParent = this;
            opexReport.WindowState = FormWindowState.Maximized;
            opexReport.Text = "费用报告";
            opexReport.Dock = DockStyle.Fill;
            opexReport.Show();
        }
        /// <summary>
        /// 添加人:ydx
        /// 添加目的:试算平衡表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 试算平衡表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TB tb = new TB();
            tb.MdiParent = this;
            tb.WindowState = FormWindowState.Maximized;
            tb.Text = "试算平衡表";
            tb.Dock = DockStyle.Fill;
            tb.Show();
        }
        /// <summary>
        /// KPISetting 
        /// 2014-07-28
        /// ydx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kPISettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KPISetting kpi = new KPISetting();
            kpi.MdiParent = this;
            kpi.WindowState = FormWindowState.Maximized;
            kpi.Text = "KPI";
            kpi.Dock = DockStyle.Fill;
            kpi.Show();
        }
        /// <summary>
        /// 添加人：ydx
        ///添加目的：资产负债表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 资产负债表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BS bs = new BS();
            bs.MdiParent = this;
            bs.WindowState = FormWindowState.Maximized;
            bs.Text = "资产负债表";
            bs.Dock = DockStyle.Fill;
            bs.Show();
        }
        /// <summary>
        /// 报表：kpi报告 
        /// 添加时间：2014-08-04
        /// 添加人：ydx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kPI报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KPIReport kpi = new KPIReport();
            kpi.MdiParent = this;
            kpi.WindowState = FormWindowState.Maximized;
            kpi.Text = "KPI报告";
            kpi.Dock = DockStyle.Fill;
            kpi.Show();
        }
        /// <summary>
        /// 退出系统  
        /// 添加人：ydx
        /// 添加时间：2014-08-12
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-08-28
        /// 添加目的：油站 开、关  管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void totalSitesNumberToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TotalSiteSetting totalSite = new TotalSiteSetting();
            totalSite.MdiParent = this;
            totalSite.WindowState = FormWindowState.Maximized;
            totalSite.Text = "Total Site Number";
            totalSite.Dock = DockStyle.Fill;
            totalSite.Show();
        }
        private void 固定报告导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 数据管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 权限管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrivilegeManage_Click(object sender, EventArgs e)
        {
            MIS_AllocationRights totalSite = new MIS_AllocationRights();
            totalSite.MdiParent = this;
            totalSite.WindowState = FormWindowState.Maximized;
            totalSite.Text = "权限管理";
            totalSite.Dock = DockStyle.Fill;
            totalSite.Show();
        }
        /// <summary>
        /// 添加人：ydx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 人员管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MIS_UsersManager userManager = new MIS_UsersManager();
            userManager.MdiParent = this;
            userManager.WindowState = FormWindowState.Maximized;
            userManager.Text = "人员管理";
         //   userManager.Dock = DockStyle.Fill;
            userManager.Show();
        }
        /// <summary>
        /// 添加人黄晓艳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 部门管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DepartmentManager departmentManager = new DepartmentManager();
            departmentManager.MdiParent = this;
            departmentManager.WindowState = FormWindowState.Maximized;
            departmentManager.Text = "部门管理";
            departmentManager.Dock = DockStyle.Fill;
            departmentManager.Show();
        }

        private void 预算导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 自定义报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 添加人：ydx
        /// 壳牌中国的T5标准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void t5SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            T5_Settingcs T5setting = new T5_Settingcs();
            T5setting.MdiParent = this;
            T5setting.WindowState = FormWindowState.Maximized;
            T5setting.Text = "T5Setting";
            T5setting.Dock = DockStyle.Fill;
            T5setting.Show();
        }
        /// <summary>
        /// 添加人：ydx
        /// 各合资公司的 T5标准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jVT5SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JV_T5Setting JVT5setting = new JV_T5Setting();
            JVT5setting.MdiParent = this;
            JVT5setting.WindowState = FormWindowState.Maximized;
            JVT5setting.Text = "JVT5Setting";
            JVT5setting.Dock = DockStyle.Fill;
            JVT5setting.Show();
        }
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-12-30
        /// 添加目的：T5Site替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void t5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            T5_SiteChange siteChange = new T5_SiteChange();
            siteChange.MdiParent = this;
            siteChange.WindowState = FormWindowState.Maximized;
            siteChange.Text = "T5 SiteReplace";
            siteChange.Dock = DockStyle.Fill;
            siteChange.Show();

        }

        private void 个人密码修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlterPwd alterpwd = new AlterPwd();
            alterpwd.MdiParent = this;
            alterpwd.WindowState = FormWindowState.Maximized;
            alterpwd.Text = "修改个人密码";
            alterpwd.Dock = DockStyle.Fill;
            alterpwd.Show();
        }

      

    

       
    }
}

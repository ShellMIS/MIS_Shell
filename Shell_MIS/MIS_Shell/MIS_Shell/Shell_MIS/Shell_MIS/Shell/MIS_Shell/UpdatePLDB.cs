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
    public partial class UpdatePLDB : Form
    {

        public UpdatePLDB()
        {
            InitializeComponent();
        }
        Pldb pd = new Pldb();
        DataTable dt = new DataTable();
        /// <summary>
        /// 提交按钮 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Up_Click(object sender, EventArgs e)
        {
            try
            {
                int id = PLDBSetting.ID;
               if(pd.PLDBUpdate(id,textItem.Text.ToString())==1)
               {
                   MessageBox.Show("修改成功");
                   this.Close();
               }else
               {
               MessageBox.Show("修改失败！");
               }
            }
            catch(Exception ex)
            {
                MessageBox.Show("操作失败！"+ex.Message);
            }
        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdatePLDB_Load(object sender, EventArgs e)
        {
            try 
            {
                int id = PLDBSetting.ID;
                dt = pd.PlDbSelect(id);
                this.textId.Text = id.ToString();
                this.textId.ReadOnly=true;
                this.textItem.Text = dt.Rows[0]["PIDB_Item"].ToString();
               
                
            }catch(Exception ex)
            {
                MessageBox.Show("操作错误"+ex.Message);
            }
        }

        
    }
}

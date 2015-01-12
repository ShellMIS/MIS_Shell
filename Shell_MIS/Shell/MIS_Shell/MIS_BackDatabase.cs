using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using DAL;


namespace MIS_Shell
{
    //添加人:ydx
    //添加目的: 完成数据库备份、还原功能
    public partial class MIS_BackDatabase : Form
    {
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        public MIS_BackDatabase()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 备份 数据库按钮点击事件 ydx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            DateTime now=DateTime.Now;
            string filePath = "";
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "(*.bak)|*.bak";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {

                System.IO.FileInfo fileInf = new System.IO.FileInfo(saveFile.FileName);
                if(fileInf.Extension.ToLower()==".bak")//输入文件后缀的形式
                {
                    filePath = saveFile.FileName.Substring(0, saveFile.FileName.Length - 4) + now.Year + now.Month + now.Hour + now.Minute + now.Second + ".bak";

                }
                //else if (fileInf.Extension.ToLower() == "")//只输入文件名的形式
                //{
                //    filePath = sf.FileName + now.Year + now.Month + now.Hour + now.Minute + now.Second + ".bak";

                //}
                else
                {
                    filePath = "";
                }
               if(filePath=="")
               {
                   MessageBox.Show("输入文件名");
               }else
               {
                   SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Shell-MIS;User Id=sa;Password=12345678;");
                   SqlCommand cmdBK = new SqlCommand();
                   cmdBK.CommandType = CommandType.Text;
                   cmdBK.Connection = conn;
                   cmdBK.CommandText = @"USE master; backup database [Shell-MIS] to disk='" + filePath + "'" + " with init";
                   try
                   {
                       conn.Open();
                       cmdBK.ExecuteNonQuery();
                       MessageBox.Show("备份数据库成功！");
                       int result = logDal.OpertionLogInsert(userid, "数据库备份操作", DateTime.Now.ToString(), "备份数据库成功");
                       if (result < 0)
                       {
                           MessageBox.Show("日志插入失败！");
                           return;
                       }

                   }
                   catch (Exception ex)
                   {
                       conn.Close();
                       MessageBox.Show("备份数据库异常，请联系开发人员" + ex.Message);
                       int result = logDal.OpertionLogInsert(userid, "数据库备份操作", DateTime.Now.ToString(), "备份数据库异常");
                       if (result < 0)
                       {
                           MessageBox.Show("日志插入失败！");
                           return;
                       }
                   }
                   finally
                   {
                       conn.Close();
                   }
               }
            }
        }
        /// <summary>
        /// 还原  数据库 ydx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestore_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            lMessage.ForeColor = Color.Red;
            this.lMessage.Text = "还原操作比较慢,请不要乱动鼠标,耐心等一下,还原成功会弹出对话框！";
            DateTime now = DateTime.Now;
            string filePath = "";
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "(*.bak)|*.bak";
            if (openFile.ShowDialog() == DialogResult.OK)
            {

                System.IO.FileInfo fileInf = new System.IO.FileInfo(openFile.FileName);
                if (fileInf.Extension.ToLower() == ".bak")//输入文件后缀的形式
                {
                    filePath = openFile.FileName;
                  
                }
                if (filePath == "")
                {
                    MessageBox.Show("文件名输入有问题！");
                }
                else
                {
                    SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Shell-MIS;User Id=sa;Password=12345678;");
                    SqlCommand cmdBK = new SqlCommand();
                    cmdBK.CommandType = CommandType.Text;
                    cmdBK.Connection = conn;
                    string sql = string.Format("use master;ALTER DATABASE [Shell-MIS] SET OFFLINE WITH ROLLBACK AFTER 0 ;declare @s varchar(8000);select @s=isnull(@s,'')+' kill '+rtrim(spID) from master..sysprocesses where dbid=db_id('{0}');select @s;exec(@s) ;RESTORE DATABASE {1} FROM DISK = N'{2}' with replace", "[Shell-MIS]", "[Shell-MIS]", filePath);
                    cmdBK.CommandText = sql;
                    try
                    {
                        conn.Open();
                        cmdBK.ExecuteNonQuery();
                        MessageBox.Show("还原数据库成功！");
                        this.Cursor = System.Windows.Forms.Cursors.Default;
                        int result = logDal.OpertionLogInsert(userid, "还原数据库操作", DateTime.Now.ToString(), "还原数据库成功");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show("还原数据库异常，请联系开发人员" + ex.Message);
                        int result = logDal.OpertionLogInsert(userid, "还原数据库操作", DateTime.Now.ToString(), "还原数据库异常");
                        if (result < 0)
                        {
                            MessageBox.Show("日志插入失败！");
                            return;
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
        /// <summary>
        ///窗体加载事件 开始就设置消息label用红色字体显示 ydx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MIS_BackDatabase_Load(object sender, EventArgs e)
        {
            lMessage.Text = "";
            lMessage.ForeColor = Color.Red;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using Microsoft.Office.Interop;
using DAL;

namespace MIS_Shell
{
    public partial class PLSetting : Form
    {
        public PLSetting()
        {
            InitializeComponent();
        }
        PLDal pldal = new PLDal();
        DataTable dt = new DataTable();

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string fileName = fd.FileName;
                bind(fileName);
            }
        }
        private void bind(string fileName)
        {
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT *  FROM [PL mapping$]", strConn);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                dt = ds.Tables[0];
                this.txt_Path.Text = fileName.ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                DataRow dr = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    insertToSql(dr);
                }
                MessageBox.Show("导入成功！");
                this.dataGridView2.DataSource = pldal.PLSelect();
            }
            else
            {
                MessageBox.Show("没有数据！");
            }
        }
        private void insertToSql(DataRow dr)
        {
            string AccGroup = dr["AccGroup"].ToString();
            string AccSubGroup = dr["AccSubGroup"].ToString();
            string AccType = dr["AccType"].ToString();
            string AccSubType = dr["AccSubType"].ToString();
            string AccountCode = dr["AccountCode"].ToString();
            string Account_Description = dr["Account_Description"].ToString();
            pldal.PLInsert(AccGroup, AccSubGroup, AccType, AccSubType, AccountCode, Account_Description);
        }

        private void PLSetting_Load(object sender, EventArgs e)
        {
            this.dataGridView2.DataSource = pldal.PLSelect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.RowCount - 1 != 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl   files   (*.xls)|*.xls";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "导出Excel文件到";

                DateTime now = DateTime.Now;
                saveFileDialog.FileName = now.Year.ToString().PadLeft(2)
                + now.Month.ToString().PadLeft(2, '0')
                + now.Day.ToString().PadLeft(2, '0') + "-"
                + now.Hour.ToString().PadLeft(2, '0')
                + now.Minute.ToString().PadLeft(2, '0')
                + now.Second.ToString().PadLeft(2, '0');

                saveFileDialog.ShowDialog();

                Stream myStream;
                myStream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(0));

                string str = "";
                try
                {
                    //写标题     
                    for (int i = 0; i < dataGridView2.Columns.Count; i++)
                    {
                        if (i > 0)
                        {
                            str += "\t";
                        }
                        str += dataGridView2.Columns[i].HeaderText;

                    }

                    sw.WriteLine(str);
                    //写内容   
                    for (int j = 0; j < dataGridView2.RowCount - 1; j++)
                    {
                        string tempStr = "";
                        for (int k = 0; k < dataGridView2.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                tempStr += "\t";
                            }
                            tempStr += dataGridView2[k, j].Value.ToString();
                        }
                        sw.WriteLine(tempStr);
                    }
                    sw.Close();
                    myStream.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
            }
            else
            {
                MessageBox.Show("请先在该表中导入数据！");
            } 
        }

        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                dataGridView2.Rows[e.RowIndex].Selected = true;
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row = dataGridView2.SelectedRows.Count;
            if (row == 0)
            {
                MessageBox.Show("没有选中任何行", "Error");
                return;
            }
            else if (MessageBox.Show("确认删除选中的" + row.ToString() + "条记录吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = dataGridView2.SelectedRows.Count; i > 0; i--)
                {
                    int rno = dataGridView2.SelectedRows[i - 1].Index;
                    if (Convert.ToBoolean(dataGridView2.Rows[rno].Cells[0].Value) == true)//全选的时候 删除最后一行会报错 如果不添加这行代码
                    {

                        string id = dataGridView2.Rows[rno].Cells[0].Value.ToString();
                        if (pldal.PLDelete(id) == 1)
                        {
                            dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[i - 1].Index);
                        }
                        else
                        {
                            MessageBox.Show("操作失败！");
                        }
                    }
                }
            }
        }

        private void But_Insert_Click(object sender, EventArgs e)
        {
            if (txt_AccGroup.Text.ToString().Trim() == "" || txt_AccSubGroup.Text.ToString().Trim() == "" || txt_AccType.Text.ToString().Trim() == "" || txt_AccSubType.Text.ToString().Trim() == "" || txt_AccountCode.Text.ToString().Trim() == "" || txt_AccountD.Text.ToString().Trim()=="")
            {
                MessageBox.Show("有空白项，请填写完整");
            }else
            {
                string AccGroup = this.txt_AccGroup.Text.ToString().Trim();
                string AccSubGroup = this.txt_AccSubGroup.Text.ToString().Trim();
                string AccType = this.txt_AccType.Text.ToString().Trim();
                string AccSubType = this.txt_AccSubType.Text.ToString().Trim();
                string AccountCode = this.txt_AccountCode.Text.ToString().Trim();
                string Account_Description = this.txt_AccountD.Text.ToString().Trim();
                DataTable dtExists = pldal.PLSelect(AccGroup, AccSubGroup, AccType, AccSubType, AccountCode, Account_Description);
                if(dtExists.Rows.Count>0)
                {
                    MessageBox.Show("已存在此记录："+AccGroup);
                }else
                {
                    if (pldal.PLInsert(AccGroup, AccSubGroup, AccType, AccSubType, AccountCode, Account_Description) == 1)
                {
                    MessageBox.Show("增加成功！");
                    txt_AccGroup.Text = "";
                    txt_AccSubGroup.Text = "";
                    txt_AccType.Text = "";
                    txt_AccSubType.Text = "";
                    txt_AccountCode.Text = "";
                    txt_AccountD.Text = "";

                }
                else
                {
                    MessageBox.Show("操作失败！");
                }
                this.dataGridView2.DataSource = pldal.PLSelect();
                }

                
            }
            
        }

        private void but_Select_Click(object sender, EventArgs e)
        {
            try
            {
                string AccGroup = this.txt_SAccGroup.Text.ToString().Trim();
                string AccSubGroup = this.txt_SAccSubGroup.Text.ToString().Trim();
                string AccType = this.txt_SAccType.Text.ToString().Trim();
                string AccSubType = this.txt_SAccSubType.Text.ToString().Trim();
                string AccountCode = this.txt_SAccountCode.Text.ToString().Trim();
                string Account_Description = this.txt_SAccountD.Text.ToString().Trim();
                this.dataGridView2.DataSource = pldal.PLSelect(AccGroup, AccSubGroup, AccType, AccSubType, AccountCode, Account_Description);
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }
        }
        public static int ID;

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            UpdatePL updatepl = new UpdatePL();
            updatepl.Owner = this;
            updatepl.ShowDialog();
            this.dataGridView2.DataSource = pldal.PLSelect();
        }

    }
}

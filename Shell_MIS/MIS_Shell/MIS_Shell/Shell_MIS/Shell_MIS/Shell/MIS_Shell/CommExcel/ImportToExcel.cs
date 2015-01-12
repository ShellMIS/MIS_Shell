using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using DAL;
using System.Diagnostics;//线程

using NPOI.HSSF.UserModel;
using NPOI.HPSF;//offic

namespace MIS_Shell.CommExcel
{
    /// <summary>
    /// 添加人：ydx
    /// 添加时间：2014-08-29
    /// 添加目的：导出类
    /// </summary>
    public class ImportToExcel
    {
        #region 冬侠
        #region tableToExcelStream
        public bool tableToExcelStream(System.Data.DataTable dTable, string fileName)
        {
            bool flag = false;
            if (dTable.Rows.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl   files   (*.xls)|*.xls";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "导出Excel文件到";
                saveFileDialog.FileName = fileName;
                saveFileDialog.ShowDialog();
                Stream myStream;
                myStream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(0));
                string str = "";
                try
                {
                    //写标题     
                    for (int i = 0; i < dTable.Columns.Count; i++)
                    {
                        if (i > 0)
                        {
                            str += "\t";
                        }
                        str += dTable.Columns[i].Caption.ToString().Trim();//获取列名称

                    }
                    sw.WriteLine(str);
                    //写内容   
                    for (int j = 0; j < dTable.Rows.Count; j++)
                    {
                        string tempStr = "";
                        for (int k = 0; k < dTable.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                tempStr += "\t";
                            }
                            tempStr += dTable.Rows[j][k].ToString().Trim();
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
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }
        #endregion

        #region gridviewToExcel
        /// <summary>
        /// 添加人：ydx
        /// 添加时间：2014-08-25
        /// 添加目的：将dataGridView里的数据导出到excel表格
        /// </summary>
        /// <param name="dGrid"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool toExcel(DataGridView dataGrid1, string fileName)
        {

            bool flag = false;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，可能计算机未安装Excel");
                // return;
            }
            //創建Excel對象
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            // Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            //for (int sheetcount = 0; sheetcount < 2; sheetcount++)//循环根据自己需要的sheet的数目这里是两个
            //{
            if (worksheet == null)
            {
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
            }
            else
            {
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.Add(Type.Missing, worksheet, 1, Type.Missing);
            }
            Microsoft.Office.Interop.Excel.Range range = null;
            //if (sheetcount == 0)
            //{
            long totalCount = dataGrid1.Rows.Count;
            long rowRead = 0;
            float percent = 0;
            worksheet.Name = fileName;//第一个sheet在Excel中显示的名称
            ////写入标题
            for (int i = 0; i < dataGrid1.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dataGrid1.Columns[i].HeaderText;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                range.Interior.ColorIndex = 15;//背景颜色
                range.Font.Bold = true;//粗体
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//居中
                //加边框
                range.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, null);
                range.ColumnWidth = 4.63;//设置列宽
                range.EntireColumn.AutoFit();//自动调整列宽
                //r1.EntireRow.AutoFit();//自动调整行高
            }

            //写入内容
            for (int r = 0; r < dataGrid1.Rows.Count - 1; r++)
            {
                for (int i = 0; i < dataGrid1.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dataGrid1.Rows[r].Cells[i].Value.ToString();
                    range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[r + 2, i + 1];
                    range.Font.Size = 9;//字体大小
                    //加边框
                    range.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, null);
                    range.EntireColumn.AutoFit();//自动调整列宽
                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
            }
            range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            if (dataGrid1.Columns.Count > 1)
            {
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            }
            //下面是将Excel存储在服务器上指定的路径与存储的名称
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl   files   (*.xls)|*.xls";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "导出Excel文件到";
                saveFileDialog.FileName = fileName;
                saveFileDialog.ShowDialog();
                string filePath = saveFileDialog.FileName;
                string version;//版本号
                int formNum;//格式
                version = xlApp.Version;
                if (Convert.ToDouble(version) < 12)//You use Excel 97-2003
                {
                    formNum = -4143;
                }
                else//you use excel 2007 or later
                {
                    formNum = 56;
                }
                workbook.SaveAs(filePath, formNum);
                flag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成Excel附件过程中出现异常，详细信息如：" + ex.ToString());
            }
            //我们导出Excel的时候会在进程中调用Excel进程，导出之后必须强制杀掉进程            
            try
            {
                xlApp.Quit();
                GC.Collect(); //回收资源 
                //KillProcess("Excel"); //关闭进程
                Process[] processes = Process.GetProcesses();
                foreach (Process process in processes)
                {
                    if (process.ProcessName == "EXCEL")
                    {
                        if (string.IsNullOrEmpty(process.MainWindowTitle))
                        {
                            process.Kill();
                        }
                    }
                }
                //System.Threading.Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete Excel Process Error:" + ex.Message);
            }
            return flag;
        }
        #endregion

        #region DataTable导出Excel
        public bool tableToExcel(System.Data.DataTable dt, string fileName)
        {
            bool flag = false;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，可能计算机未安装Excel");
                // return;
            }
            //創建Excel對象
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            // Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            //for (int sheetcount = 0; sheetcount < 2; sheetcount++)//循环根据自己需要的sheet的数目这里是两个
            //{
            if (worksheet == null)
            {
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
            }
            else
            {
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.Add(Type.Missing, worksheet, 1, Type.Missing);
            }
            Microsoft.Office.Interop.Excel.Range range = null;
            //if (sheetcount == 0)
            //{
            long totalCount = dt.Rows.Count;
            long rowRead = 0;
            float percent = 0;
            worksheet.Name = fileName;//第一个sheet在Excel中显示的名称
            ////写入标题
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                range.Interior.ColorIndex = 15;//背景颜色
                range.Font.Bold = true;//粗体
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//居中
                //加边框
                range.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, null);
                range.ColumnWidth = 4.63;//设置列宽
                range.EntireColumn.AutoFit();//自动调整列宽
                //r1.EntireRow.AutoFit();//自动调整行高
            }
            //写入内容
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i];
                    range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[r + 2, i + 1];
                    range.Font.Size = 9;//字体大小
                    //加边框
                    range.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, null);
                    range.EntireColumn.AutoFit();//自动调整列宽
                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
            }
            range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            if (dt.Columns.Count > 1)
            {
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            }
            //}
            //}
            //下面是将Excel存储在服务器上指定的路径与存储的名称
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl   files   (*.xls)|*.xls";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "导出Excel文件到";
                saveFileDialog.FileName = fileName;
                saveFileDialog.ShowDialog();
                string filePath = saveFileDialog.FileName;
                string version;//版本号
                int formNum;//格式
                version = xlApp.Version;
                if (Convert.ToDouble(version) < 12)//You use Excel 97-2003
                {
                    formNum = -4143;
                }
                else//you use excel 2007 or later
                {
                    formNum = 56;
                }
                //注释部分是把excel导出到指定位置
                //string tPath = System.AppDomain.CurrentDomain.BaseDirectory;
                //if (!Directory.Exists(tPath + "Excel"))
                //{
                //    Directory.CreateDirectory(tPath + "Excel");
                //}
                // workbook.SaveCopyAs(tPath + "Excel" + "\\" + System.DateTime.Today.ToString("yyyyMMdd") + fileName + "测试.xls");
                workbook.SaveAs(filePath, formNum);
                flag = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("生成Excel附件過程中出現異常，詳細信息如：" + ex.ToString());
            }
            //我们导出Excel的时候会在进程中调用Excel进程，导出之后必须强制杀掉进程            
            try
            {
                Process[] processes = Process.GetProcesses();
                foreach (Process process in processes)
                {
                    if (process.ProcessName == "EXCEL")
                    {
                        if (string.IsNullOrEmpty(process.MainWindowTitle))
                        {
                            process.Kill();
                        }
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete Excel Process Error:" + ex.Message);
            }
            return flag;
        }
        #endregion
        #endregion
        #region 黄晓艳 npoi数据导出到Excel 2014/11/20
        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="DefaultFileName">默认的文件名</param>
        /// <param name="JVCompeny">合资公司</param>
        /// <param name="datetime">导出的日期</param>
        public static void Export(DataTable dtSource, string strHeaderText, string DefaultFileName, string JVCompeny, string datetime)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel文件|*.xls";
            sfd.FileName = DefaultFileName;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string Filename = sfd.FileName;
                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();
                #region 右击文件 属性信息
                {
                    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                    dsi.Company = "NPOI";
                    workbook.DocumentSummaryInformation = dsi;

                    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                    si.Author = "文件作者信息"; //填加xls文件作者信息
                    si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
                    si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
                    si.Comments = "作者信息"; //填加xls文件作者信息
                    si.Title = "标题信息"; //填加xls文件标题信息
                    si.Subject = "主题信息";//填加文件主题信息
                    si.CreateDateTime = DateTime.Now;
                    workbook.SummaryInformation = si;
                }
                #endregion
                HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
                dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
                //取得列宽 
                int[] arrColWidth = new int[dtSource.Columns.Count];
                foreach (DataColumn item in dtSource.Columns)
                {
                    arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                }
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    for (int j = 0; j < dtSource.Columns.Count; j++)
                    {
                        int intTemp=0;
                        try
                        {
                            if (dtSource.Rows[i].RowState!=DataRowState.Deleted)
                            {
                                intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                            }
                          
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message);
                            return;
                        }

                        if (intTemp > arrColWidth[j])
                        {
                            arrColWidth[j] = intTemp;
                        }
                    }
                }
                int rowIndex = 0;
                foreach (DataRow row in dtSource.Rows)
                {
                    #region 新建表，填充表头，填充列头，样式
                    if (rowIndex == 65535 || rowIndex == 0)
                    {
                        if (rowIndex != 0)
                        {
                            sheet = (HSSFSheet)workbook.CreateSheet();
                        }

                        #region 表头及样式
                        {
                            //excel表头
                            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                            headerRow.HeightInPoints = 25;
                            headerRow.CreateCell(0).SetCellValue(strHeaderText);
                            //第一行标题
                            HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                            headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                            HSSFFont font = (HSSFFont)workbook.CreateFont();
                            font.FontHeightInPoints = 20;
                            font.Boldweight = short.MaxValue;
                            headStyle.SetFont(font);
                            headerRow.GetCell(0).CellStyle = headStyle;
                            sheet.AddMergedRegion(new NPOI.SS.Util.Region(0, 0, 0, dtSource.Columns.Count - 1));                 //合并单元格

                            //第二行合资公司
                            headerRow = (HSSFRow)sheet.CreateRow(1);
                            headerRow.HeightInPoints = 20;
                            HSSFFont font1 = (HSSFFont)workbook.CreateFont();
                            font1.FontHeightInPoints = 10;
                            font1.Boldweight = short.MaxValue;
                            headStyle.SetFont(font1);
                            headerRow.CreateCell(0).SetCellValue(JVCompeny);
                            headerRow.GetCell(0).CellStyle = headStyle;

                            sheet.AddMergedRegion(new NPOI.SS.Util.Region(1, 0, 1, dtSource.Columns.Count - 1));
                            //第三行日期
                            headerRow = (HSSFRow)sheet.CreateRow(2);
                            headerRow.HeightInPoints = 20;
                            HSSFFont font2 = (HSSFFont)workbook.CreateFont();
                            font2.FontHeightInPoints = 10;
                            font2.Boldweight = short.MaxValue;
                            headStyle.SetFont(font2);

                            headerRow.CreateCell(0).SetCellValue(datetime);
                            headerRow.GetCell(0).CellStyle = headStyle;
                            sheet.AddMergedRegion(new NPOI.SS.Util.Region(2, 0, 2, dtSource.Columns.Count - 1));

                            //headerRow.Dispose();
                        }
                        #endregion


                        #region 列头及样式
                        {
                            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(3);
                            HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                            headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                            HSSFFont font = (HSSFFont)workbook.CreateFont();
                            font.FontHeightInPoints = 10;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);
                            foreach (DataColumn column in dtSource.Columns)
                            {
                                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                                headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                                //设置列宽
                                sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                            }
                            //headerRow.Dispose();
                        }
                        #endregion

                        rowIndex = 4;
                    }
                    #endregion
                    #region 填充内容
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    HSSFCellStyle hssfstyle = (HSSFCellStyle)workbook.CreateCellStyle();
                    foreach (DataColumn column in dtSource.Columns)
                    {
                        HSSFCell newCell = (HSSFCell)dataRow.CreateCell(column.Ordinal);
                        string drValue = "";
                        if (row.RowState!=DataRowState.Deleted)
                        {
                             drValue = row[column].ToString();
                        }
                        
                        #region 数据类型
                        switch (column.DataType.ToString())
                        {
                            case "System.String"://字符串类型
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型
                                if (drValue != "")
                                {
                                    DateTime dateV;
                                    DateTime.TryParse(drValue, out dateV);
                                    newCell.SetCellValue(dateV);
                                }

                                newCell.CellStyle = dateStyle;//格式化显示
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型

                                hssfstyle.DataFormat = format.GetFormat("¥#,##0");
                                newCell.CellStyle = hssfstyle;
                                break;

                            case "System.Double":
                                hssfstyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0.00");
                                double doubV = 0.00;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                newCell.CellStyle = hssfstyle;
                                break;
                            case "System.DBNull"://空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }
                        #endregion
                        

                    }
                    #endregion

                    rowIndex++;
                }
                using (FileStream ms = new FileStream(Filename, FileMode.OpenOrCreate))
                {
                    try
                    {
                        workbook.Write(ms);
                        ms.Flush();
                        ms.Position = 0;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }

                    // sheet.Dispose();
                    //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet

                }
                MessageBox.Show("导出成功!");
            }

        }
        #endregion


    }
}

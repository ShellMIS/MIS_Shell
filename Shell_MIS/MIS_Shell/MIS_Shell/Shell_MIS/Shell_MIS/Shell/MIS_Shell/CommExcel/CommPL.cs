using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using DAL;
using DBHelper;

namespace MIS_Shell.CommExcel
{
    public class CommPL
    {


        ImportDal importdal = new ImportDal();

        DataTable dt = new DataTable();

        public void MIS_PL(string FileName, string FileName1)
        {
            Excel.Application app = new Excel.Application();//引用Excel对象
            Excel.Workbooks wbs = app.Workbooks;//创建Excel工作薄 
            Excel.Workbook wb = wbs.Open(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Excel.Worksheet s = wb.Worksheets["Sheet1"];
            #region
            dt = importdal.ImportSelect_reportPL_sybyysr();
            if (dt.Rows.Count != 0)
            {
                s.Cells[9, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[9, 3] = dt.Rows[1]["money"].ToString();
            }
            else
            {
                s.Cells[9, 3] = "0";
                s.Cells[9, 4] = "0";
            }
            dt = importdal.ImportSelect_reportPL_sybyycb();
            if (dt.Rows.Count != 0)
            {
                s.Cells[10, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[10, 3] = dt.Rows[1]["money"].ToString();
            }
            else
            {
                s.Cells[10, 3] = "0";
                s.Cells[10, 4] = "0";
            }
            dt = importdal.ImportSelect_reportPL_sybyysf();
            if (dt.Rows.Count != 0)
            {
                s.Cells[11, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[11, 3] = dt.Rows[1]["money"].ToString();
            }
            else
            {
                s.Cells[11, 3] = "0";
                s.Cells[11, 4] = "0";
            }
            dt = importdal.ImportSelect_reportPL_sybxsfy();
            if (dt.Rows.Count != 0)
            {
                s.Cells[12, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[12, 3] = dt.Rows[1]["money"].ToString();
            }
            else
            {
                s.Cells[12, 3] = "0";
                s.Cells[12, 4] = "0";
            }
            dt = importdal.ImportSelect_reportPL_sybglfy();
            if (dt.Rows.Count != 0)
            {
                s.Cells[13, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[13, 3] = dt.Rows[1]["money"].ToString();
            }
            else
            {
                s.Cells[13, 3] = "0";
                s.Cells[13, 4] = "0";
            }
            dt = importdal.ImportSelect_reportPL_sybgcwfy();
            if (dt.Rows.Count != 0)
            {
                s.Cells[14, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[14, 3] = dt.Rows[1]["money"].ToString();
            }
            else
            {
                s.Cells[14, 3] = "0";
                s.Cells[14, 4] = "0";
            }
            dt = importdal.ImportSelect_reportPL_sybgzcjzss();
            if (dt.Rows.Count != 0)
            {
                s.Cells[15, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[15, 3] = dt.Rows[1]["money"].ToString();
            }
            else
            {
                s.Cells[15, 3] = "0";
                s.Cells[15, 4] = "0";
            }
            dt = importdal.ImportSelect_reportPL_sybggyjzbdjsy();
            if (dt.Rows.Count != 0)
            {
                s.Cells[16, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[16, 3] = dt.Rows[1]["money"].ToString();
            }
            else
            {
                s.Cells[16, 3] = "0";
                s.Cells[16, 4] = "0";
            }
            dt = importdal.ImportSelect_reportPL_sybgtzjsy();
            if (dt.Rows.Count != 0)
            {
                s.Cells[17, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[17, 3] = dt.Rows[1]["money"].ToString();
            }
            else
            {
                s.Cells[17, 3] = "0";
                s.Cells[17, 4] = "0";
            }
            dt = importdal.ImportSelect_reportPL_sybgyywsr();
            if (dt.Rows.Count != 0)
            {
                s.Cells[19, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[19, 3] = dt.Rows[1]["money"].ToString();
            }
            else
            {
                s.Cells[19, 3] = "0";
                s.Cells[19, 4] = "0";
            }
            dt = importdal.ImportSelect_reportPL_sybgyywzc();
            if (dt.Rows.Count != 0)
            {
                s.Cells[20, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[20, 3] = dt.Rows[1]["money"].ToString();
            }
            else
            {
                s.Cells[20, 3] = "0";
                s.Cells[20, 4] = "0";
            }
            dt = importdal.ImportSelect_reportPL_sybgfldzcczjss();
            if (dt.Rows.Count != 0)
            {
                s.Cells[21, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[21, 3] = dt.Rows[1]["money"].ToString();
            }
            else
            {
                s.Cells[21, 3] = "0";
                s.Cells[21, 4] = "0";
            }
            #endregion
            wb.SaveCopyAs(FileName1);//保存工作簿
            wb.Close(false, Type.Missing, Type.Missing);
            app.Quit();//结束excel对象
            wb = null;
            wbs = null;
            app = null;
            GC.Collect();//垃圾收集
        }
    }
}

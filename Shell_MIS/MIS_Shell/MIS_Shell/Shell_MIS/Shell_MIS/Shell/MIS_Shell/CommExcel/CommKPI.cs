using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using DAL;

namespace MIS_Shell.CommExcel
{
    public class CommKPI
    {
        ImportDal importdal = new ImportDal();

        DataTable dt = new DataTable();
        public void MIS_OPEX(string FileName, string FileName1)
        {
            Excel.Application app = new Excel.Application();//引用Excel对象
            Excel.Workbooks wbs = app.Workbooks;//创建Excel工作薄 
            Excel.Workbook wb = wbs.Open(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Excel.Worksheet s = wb.Worksheets["JV"];
            #region Mis_KPI  2014-07-08 ydx
            #region 16-26行
            dt = importdal.import_16();
            if (dt.Rows.Count != 0)
            {
                s.Cells[16, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[17, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[18, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[19, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[20, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[21, 4] = dt.Rows[0]["money"].ToString();
                s.Cells[22, 4] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[16, 4] = 0;
                s.Cells[17, 4] = 0;
                s.Cells[18, 4] =0;
                s.Cells[19, 4] = 0;
                s.Cells[20, 4] = 0;
                s.Cells[21, 4] = 0;
                s.Cells[22, 4] = 0;
            }

            dt = importdal.import_25();
            s.Cells[24, 4] = 0;
            s.Cells[26, 4] =0;
            if (dt.Rows.Count != 0)
            {
                s.Cells[25, 4] = dt.Rows[0]["money"].ToString();
               
            }
            else
            {
                s.Cells[25, 4] = 0;
              
            }

            #endregion
            #region 46-52行 54  55
            dt = importdal.import_46();
            if (dt.Rows.Count != 0)
            {
                s.Cells[46, 4] = dt.Rows[0][0].ToString();
                s.Cells[47, 4] = dt.Rows[0][0].ToString();
                s.Cells[48, 4] = dt.Rows[0][0].ToString();
                s.Cells[49, 4] = dt.Rows[0][0].ToString();
                s.Cells[50, 4] = dt.Rows[0][0].ToString();
                s.Cells[51, 4] = dt.Rows[0][0].ToString();
                s.Cells[52, 4] = dt.Rows[0][0].ToString();
            }
            else
            {
                s.Cells[46, 4] =0;
                s.Cells[47, 4] = 0;
                s.Cells[48, 4] = 0;
                s.Cells[49, 4] = 0;
                s.Cells[50, 4] = 0;
                s.Cells[51, 4] = 0;
                s.Cells[52, 4] = 0;
            }
            dt = importdal.import_54();
         
            if (dt.Rows.Count != 0)
            {
                s.Cells[54, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[54, 4] = 0;

            }
            dt = importdal.import_55();

            if (dt.Rows.Count != 0)
            {
                s.Cells[55, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[55, 4] = 0;

            }
            #endregion
            #region 60-66  68  69  70行
            dt = importdal.import_60();
            if (dt.Rows.Count != 0)
            {
                s.Cells[60, 4] = dt.Rows[0][0].ToString();
                s.Cells[61, 4] = dt.Rows[0][0].ToString();
                s.Cells[62, 4] = dt.Rows[0][0].ToString();
                s.Cells[63, 4] = dt.Rows[0][0].ToString();
                s.Cells[64, 4] = dt.Rows[0][0].ToString();
                s.Cells[65, 4] = dt.Rows[0][0].ToString();
                s.Cells[66, 4] = dt.Rows[0][0].ToString();
            }
            else
            {
                s.Cells[60, 4] = 0;
                s.Cells[61, 4] = 0;
                s.Cells[62, 4] = 0;
                s.Cells[63, 4] = 0;
                s.Cells[64, 4] = 0;
                s.Cells[65, 4] = 0;
                s.Cells[66, 4] = 0;
            }
            dt = importdal.import_68();

            if (dt.Rows.Count != 0)
            {
                s.Cells[68, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[68, 4] = 0;

            }
            dt = importdal.import_69();

            if (dt.Rows.Count != 0)
            {
                s.Cells[69, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[69, 4] = 0;

            }
            dt = importdal.import_70();

            if (dt.Rows.Count != 0)
            {
                s.Cells[70, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[70, 4] = 0;

            }
            #endregion
            #region 74-80  82  83  84行
            dt = importdal.import_74();
            if (dt.Rows.Count != 0)
            {
                s.Cells[74, 4] = dt.Rows[0][0].ToString();
                s.Cells[75, 4] = dt.Rows[0][0].ToString();
                s.Cells[76, 4] = dt.Rows[0][0].ToString();
                s.Cells[77, 4] = dt.Rows[0][0].ToString();
                s.Cells[78, 4] = dt.Rows[0][0].ToString();
                s.Cells[79, 4] = dt.Rows[0][0].ToString();
                s.Cells[80, 4] = dt.Rows[0][0].ToString();
            }
            else
            {
                s.Cells[74, 4] = 0;
                s.Cells[75, 4] = 0;
                s.Cells[76, 4] = 0;
                s.Cells[77, 4] =0;
                s.Cells[78, 4] = 0;
                s.Cells[79, 4] =0;
                s.Cells[80, 4] = 0;
            }
            dt = importdal.import_82();

            if (dt.Rows.Count != 0)
            {
                s.Cells[82, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[82, 4] = 0;

            }
            dt = importdal.import_83();

            if (dt.Rows.Count != 0)
            {
                s.Cells[83, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[83, 4] = 0;

            }
            dt = importdal.import_84();

            if (dt.Rows.Count != 0)
            {
                s.Cells[84, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[84, 4] = 0;

            }
            #endregion
            #region 115,116,117行
            dt = importdal.import_115();

            if (dt.Rows.Count != 0)
            {
                s.Cells[115, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[115, 4] = 0;

            }
            dt = importdal.import_116();

            if (dt.Rows.Count != 0)
            {
                s.Cells[116, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[116, 4] = 0;

            }
            dt = importdal.import_117();

            if (dt.Rows.Count != 0)
            {
                s.Cells[117, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[117, 4] = 0;

            }
            #endregion
            #region 135-143行
            dt = importdal.import_135();

            if (dt.Rows.Count != 0)
            {
                s.Cells[135, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[135, 4] = 0;

            }
            dt = importdal.import_136();

            if (dt.Rows.Count != 0)
            {
                s.Cells[136, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[136, 4] = 0;

            }
            dt = importdal.import_137();

            if (dt.Rows.Count != 0)
            {
                s.Cells[137, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[137, 4] = 0;

            }
            dt = importdal.import_138();

            if (dt.Rows.Count != 0)
            {
                s.Cells[138, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[138, 4] = 0;

            }
            dt = importdal.import_139();

            if (dt.Rows.Count != 0)
            {
                s.Cells[139, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[139, 4] = 0;

            }
            dt = importdal.import_140();

            if (dt.Rows.Count != 0)
            {
                s.Cells[140, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[140, 4] = 0;

            }
            dt = importdal.import_141();

            if (dt.Rows.Count != 0)
            {
                s.Cells[141, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[141, 4] = 0;

            }
            dt = importdal.import_142();

            if (dt.Rows.Count != 0)
            {
                s.Cells[142, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[142, 4] = 0;

            }
            dt = importdal.import_143();

            if (dt.Rows.Count != 0)
            {
                s.Cells[143, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[143, 4] = 0;

            }
            #endregion
            #region 151-156行
            dt = importdal.import_151();

            if (dt.Rows.Count != 0)
            {
                s.Cells[151, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[151, 4] = 0;

            }
            dt = importdal.import_152();

            if (dt.Rows.Count != 0)
            {
                s.Cells[152, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[152, 4] = 0;

            }
            dt = importdal.import_153();

            if (dt.Rows.Count != 0)
            {
                s.Cells[153, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[153, 4] = 0;

            }
            dt = importdal.import_154();

            if (dt.Rows.Count != 0)
            {
                s.Cells[154, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[154, 4] = 0;

            }
            dt = importdal.import_155();

            if (dt.Rows.Count != 0)
            {
                s.Cells[155, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[155, 4] = 0;

            }
            dt = importdal.import_156();

            if (dt.Rows.Count != 0)
            {
                s.Cells[156, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[156, 4] = 0;

            }
            #endregion
            #region 160-162行
            dt = importdal.import_160();

            if (dt.Rows.Count != 0)
            {
                s.Cells[160, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[160, 4] = 0;

            }
            dt = importdal.import_161();

            if (dt.Rows.Count != 0)
            {
                s.Cells[161, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[161, 4] = 0;

            }
            dt = importdal.import_162();

            if (dt.Rows.Count != 0)
            {
                s.Cells[162, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[162, 4] = 0;

            }
            #endregion
            #region 166-169
            dt = importdal.import_166();

            if (dt.Rows.Count != 0)
            {
                s.Cells[166, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[166, 4] = 0;

            }
            dt = importdal.import_167();

            if (dt.Rows.Count != 0)
            {
                s.Cells[167, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[167, 4] = 0;

            }
            dt = importdal.import_168();

            if (dt.Rows.Count != 0)
            {
                s.Cells[168, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[168, 4] = 0;

            }
            dt = importdal.import_169();

            if (dt.Rows.Count != 0)
            {
                s.Cells[169, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[169, 4] = 0;

            }
            #endregion
            #region  182,184 行
            dt = importdal.import_182();

            if (dt.Rows.Count != 0)
            {
                s.Cells[182, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[182, 4] = 0;

            }
           
            dt = importdal.import_184();

            if (dt.Rows.Count != 0)
            {
                s.Cells[184, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[184, 4] = 0;

            }
            #endregion
            #region 144,157,173 行
            dt = importdal.import_144_157_173();

            if (dt.Rows.Count != 0)
            {
                s.Cells[144, 4] = dt.Rows[0][0].ToString();
                s.Cells[157, 4] = dt.Rows[0][0].ToString();
                s.Cells[173, 4] = dt.Rows[0][0].ToString();

            }
            else
            {
                s.Cells[144, 4] = 0;
                s.Cells[157, 4] = 0;
                s.Cells[173, 4] = 0;
            }
        #endregion
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

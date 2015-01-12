using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using DAL;

namespace MIS_Shell.CommExcel
{
    public class CommBS
    {
        ImportDal importdal = new ImportDal();

        DataTable dt = new DataTable();

        public void MIS_BS(string FileName, string FileName1)
        {
            Excel.Application app = new Excel.Application();//引用Excel对象
            Excel.Workbooks wbs = app.Workbooks;//创建Excel工作薄 
            Excel.Workbook wb = wbs.Open(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Excel.Worksheet s = wb.Worksheets["Sheet1"];
            #region 资产负债表
            #region 资产下的流动资产
            dt = importdal.ImportSelect_reportBS_hbzj();
            if (dt.Rows.Count != 0)
            {
                s.Cells[9, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[9, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_jyxjrzc();
            if (dt.Rows.Count != 0)
            {
                s.Cells[10, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[10, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_yspj();
            if (dt.Rows.Count != 0)
            {
                s.Cells[11, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[11, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_yszk();
            if (dt.Rows.Count != 0)
            {
                s.Cells[12, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[12, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_yfzk();
            if (dt.Rows.Count != 0)
            {
                s.Cells[13, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[13, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_ysgl();
            if (dt.Rows.Count != 0)
            {
                s.Cells[14, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[14, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_yslx();
            if (dt.Rows.Count != 0)
            {
                s.Cells[15, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[15, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_qtysk();
            if (dt.Rows.Count != 0)
            {
                s.Cells[16, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[16, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_ch();
            if (dt.Rows.Count != 0)
            {
                s.Cells[17, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[17, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_xhxswzc();
            if (dt.Rows.Count != 0)
            {
                s.Cells[18, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[18, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_dtfy();
            if (dt.Rows.Count != 0)
            {
                s.Cells[19, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[19, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_ynndqdfldzc();
            if (dt.Rows.Count != 0)
            {
                s.Cells[20, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[20, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_qtldzc();
            if (dt.Rows.Count != 0)
            {
                s.Cells[21, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[21, 3] = "0";
            }
            #endregion
            #region 资产下的非流动资产
            dt = importdal.ImportSelect_reportBS_kgcsjrzc();
            if (dt.Rows.Count != 0)
            {
                s.Cells[24, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[24, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_cyzdqtz();
            if (dt.Rows.Count != 0)
            {
                s.Cells[25, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[25, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_tzxfdc();
            if (dt.Rows.Count != 0)
            {
                s.Cells[26, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[26, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_cqgqtz();
            if (dt.Rows.Count != 0)
            {
                s.Cells[27, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[27, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_cqysk();
            if (dt.Rows.Count != 0)
            {
                s.Cells[28, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[28, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_gdzc();
            if (dt.Rows.Count != 0)
            {
                s.Cells[29, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[29, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_zjgc();
            if (dt.Rows.Count != 0)
            {
                s.Cells[30, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[30, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_gcwz();
            if (dt.Rows.Count != 0)
            {
                s.Cells[31, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[31, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_gdzcql();
            if (dt.Rows.Count != 0)
            {
                s.Cells[32, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[32, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_scxswzc();
            if (dt.Rows.Count != 0)
            {
                s.Cells[33, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[33, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_yqzc();
            if (dt.Rows.Count != 0)
            {
                s.Cells[34, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[34, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_wxzc();
            if (dt.Rows.Count != 0)
            {
                s.Cells[35, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[35, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_kfzc();
            if (dt.Rows.Count != 0)
            {
                s.Cells[36, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[36, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_sy();
            if (dt.Rows.Count != 0)
            {
                s.Cells[37, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[37, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_cqdtfy();
            if (dt.Rows.Count != 0)
            {
                s.Cells[38, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[38, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_dysdszc();
            if (dt.Rows.Count != 0)
            {
                s.Cells[39, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[39, 3] = "0";
            }
            dt = importdal.ImportSelect_reportBS_qtfldzc();
            if (dt.Rows.Count != 0)
            {
                s.Cells[40, 3] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[40, 3] = "0";
            }
            #endregion
            #region 负债和所有者权益（或股东权益）下的流动负债
            dt = importdal.ImportSelect_reportBS_dqjk();
            if (dt.Rows.Count != 0)
            {
                s.Cells[9, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[9, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_jyxjrfz();
            if (dt.Rows.Count != 0)
            {
                s.Cells[10, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[10, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_yfpj();
            if (dt.Rows.Count != 0)
            {
                s.Cells[11, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[11, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_ldfzyfzk();
            if (dt.Rows.Count != 0)
            {
                s.Cells[12, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[12, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_ldfzyszk();
            if (dt.Rows.Count != 0)
            {
                s.Cells[13, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[13, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_ldfzyfgz();
            if (dt.Rows.Count != 0)
            {
                s.Cells[14, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[14, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_ldfzyjsj();
            if (dt.Rows.Count != 0)
            {
                s.Cells[15, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[15, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_ldfzyflx();
            if (dt.Rows.Count != 0)
            {
                s.Cells[16, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[16, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_ldfzyfgl();
            if (dt.Rows.Count != 0)
            {
                s.Cells[17, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[17, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_ldfzqtyfk();
            if (dt.Rows.Count != 0)
            {
                s.Cells[18, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[18, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_ldfzqtfy();
            if (dt.Rows.Count != 0)
            {
                s.Cells[19, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[19, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_ldfzyjfz();
            if (dt.Rows.Count != 0)
            {
                s.Cells[20, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[20, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_ynndqdcqzqtz();
            if (dt.Rows.Count != 0)
            {
                s.Cells[21, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[21, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_qtcqfz();
            if (dt.Rows.Count != 0)
            {
                s.Cells[22, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[22 , 7] = "0";
            }
            #endregion
            #region 负债和所有者权益（或股东权益）下的非流动负债
            dt = importdal.ImportSelect_reportBS_cqjk();
            if (dt.Rows.Count != 0)
            {
                s.Cells[25, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[25, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_yfzq();
            if (dt.Rows.Count != 0)
            {
                s.Cells[26, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[26, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_cqyfk();
            if (dt.Rows.Count != 0)
            {
                s.Cells[27, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[27, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_zxyfk();
            if (dt.Rows.Count != 0)
            {
                s.Cells[28, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[28, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_dysdsfz();
            if (dt.Rows.Count != 0)
            {
                s.Cells[29, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[29, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_tfldfz();
            if (dt.Rows.Count != 0)
            {
                s.Cells[30, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[30, 7] = "0";
            }
            #endregion
            #region 负债和所有者权益（或股东权益）下的所有者权益（或股东权益）
            dt = importdal.ImportSelect_reportBS_sszb();
            if (dt.Rows.Count != 0)
            {
                s.Cells[34, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[34, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_zbgj();
            if (dt.Rows.Count != 0)
            {
                s.Cells[35, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[35, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_yygj();
            if (dt.Rows.Count != 0)
            {
                s.Cells[36, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[36, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_wfplr();
            if (dt.Rows.Count != 0)
            {
                s.Cells[37, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[37, 7] = "0";
            }
            dt = importdal.ImportSelect_reportBS_jkcg();
            if (dt.Rows.Count != 0)
            {
                s.Cells[38, 7] = dt.Rows[0]["money"].ToString();
            }
            else
            {
                s.Cells[38, 7] = "0";
            }
            #endregion
            #endregion
            //MessageBox.Show("成功！");
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

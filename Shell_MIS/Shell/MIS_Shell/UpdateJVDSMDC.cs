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
    public partial class UpdateJVDSMDC : Form
    {
        public UpdateJVDSMDC()
        {
            InitializeComponent();
        }
        DataTable dt=new DataTable();
        int userid = int.Parse(MIS_Login.dt.Rows[0]["UserID"].ToString());//用户id
        OptionLogDAL logDal = new OptionLogDAL();//日志
        JVDSMDCDal jvdsmdcdal = new JVDSMDCDal();
        SCLJV_T5DAL sclJvT5Dal = new SCLJV_T5DAL();
        private void UpdateJVDSMDC_Load(object sender, EventArgs e)
        {
            try
            {
                int ID = JVDSMDC.ID;
                dt = jvdsmdcdal.JVDSMDCSelect(ID);
                this.txt_ID.Text = ID.ToString();
                this.txt_DNCH.Text = dt.Rows[0]["DeptNameCH"].ToString();
                this.txt_DNP.Text = dt.Rows[0]["DeptNamePinyin"].ToString();
                this.txt_Nature.Text = dt.Rows[0]["Nature"].ToString();
                this.txt_HSC.Text = dt.Rows[0]["HSC"].ToString();
                this.txt_CoCd.Text = dt.Rows[0]["CoCd"].ToString();
                this.txt_T0Code.Text = dt.Rows[0]["T0Code"].ToString();
                this.txt_T3Code.Text = dt.Rows[0]["T3Code"].ToString();
                this.txt_T5Code.Text = dt.Rows[0]["T5Code"].ToString();
                this.txt_SOD.Text = dt.Rows[0]["SiteOpenDate"].ToString();
                this.txt_SA.Text = dt.Rows[0]["SiteAging"].ToString();
                this.txt_SS.Text = dt.Rows[0]["SiteStatus"].ToString();
                this.txt_TMCode.Text = dt.Rows[0]["TMCode"].ToString();
                this.txt_Acquired.Text = dt.Rows[0]["Acquired"].ToString();
                this.txt_Location.Text = dt.Rows[0]["Location"].ToString();
                this.txt_CRType.Text = dt.Rows[0]["CRType"].ToString();
                this.txt_IT.Text = dt.Rows[0]["InvestmentType"].ToString();
                this.txt_District.Text = dt.Rows[0]["District"].ToString();
              
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting修改操作", DateTime.Now.ToString(), "JV department & Site Setting操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = JVDSMDC.ID;
                string DeptNameCH = txt_DNCH.Text.ToString().Trim();
                string DeptNamePinyin = txt_DNP.Text.ToString().Trim();
                string Nature = txt_Nature.Text.ToString().Trim();
                string HSC = txt_HSC.Text.ToString().Trim();
                string CoCd = txt_CoCd.Text.ToString().Trim();
                string T0Code = txt_T0Code.Text.ToString().Trim();
                string T3Code = txt_T3Code.Text.ToString().Trim();
                string T5Code = txt_T5Code.Text.ToString().Trim();
                string SiteOpenDate = txt_SOD.Text.ToString().Trim();
                string SiteAging = txt_SA.Text.ToString().Trim();
                string SiteStatus = txt_SS.Text.ToString().Trim();
                string TMCode = txt_TMCode.Text.ToString().Trim();
                string Acquired = txt_Acquired.Text.ToString().Trim();
                string Location = txt_Location.Text.ToString().Trim();
                string CRType = txt_CRType.Text.ToString().Trim();
                string InvestmentType = txt_IT.Text.ToString().Trim();
                string District = txt_District.Text.ToString().Trim();
                  //验证T_SCLJV_T5里是否有 JV_T5
                DataTable tableExis = sclJvT5Dal.ifExists(CoCd,T5Code);
                if (tableExis == null || tableExis.Rows.Count <= 0)
                {
                    MessageBox.Show("T_SCLJV_T5里没有以下T5Code:" + T5Code);
                }
                else //已经存在
                {
                        if (jvdsmdcdal.JVDSMDCUpdate(ID, DeptNameCH, DeptNamePinyin, Nature, HSC, CoCd, T0Code, T3Code, T5Code, SiteOpenDate, SiteAging, SiteStatus, TMCode, Acquired, Location,
                        CRType, InvestmentType, District) == 1)
                        {
                            DataTable tableField = sclJvT5Dal.FieldSelect(JVDSMDC.ID, CoCd);//查出标准T5对应的数据行
                            if (tableField.Rows.Count<=0)
                            {
                                MessageBox.Show("标准T5中没有该T5");
                                return;
                            }
                            
                            //替换T5code ，英文缩写，中文名称
                            int updateT5 = sclJvT5Dal.updateDepT5code(JVDSMDC.ID, tableField.Rows[0]["SCL_T5"].ToString().Trim(), tableField.Rows[0]["DeptNameCH"].ToString().Trim(), tableField.Rows[0]["DeptNamePinYin"].ToString().Trim());


                            MessageBox.Show("修改成功！");
                            int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting修改操作", DateTime.Now.ToString(), "JV department & Site Setting修改成功");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("操作失败！");
                            int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting修改操作", DateTime.Now.ToString(), "JV department & Site Setting操作失败");
                            if (result < 0)
                            {
                                MessageBox.Show("日志插入失败！");
                                return;
                            }
                        }
                 }
               
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
                int result = logDal.OpertionLogInsert(userid, "JV department & Site Setting修改操作", DateTime.Now.ToString(), "JV department & Site Setting操作失败");
                if (result < 0)
                {
                    MessageBox.Show("日志插入失败！");
                    return;
                }
            }
        }
    }
}

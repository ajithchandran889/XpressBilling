using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XpressBilling.Account
{
    public partial class PopupRptSQ : System.Web.UI.Page
    {
        string location = string.Empty;
        string quatation = string.Empty;
        string companyCode = string.Empty;
        int SqId = 0;

        #region Page_Load
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            location = Request.QueryString["location"];
            quatation = Request.QueryString["quotation"];
            companyCode = Request.QueryString["comapnycode"];
            SqId = Request.QueryString["Id"] == "" ? 0 : Convert.ToInt32(Request.QueryString["Id"]);


            if (!IsPostBack)
            {
                DataTable dt = XBDataProvider.PopupRptSQ.GetSQReportBySQNO(quatation);

                if (dt != null && dt.Rows.Count > 0)
                {
                    Country.Text = dt.Rows[0]["TextH1"].ToString();
                    Name.Text = dt.Rows[0]["TextL1"].ToString();
                    TextBox1.Text = dt.Rows[0]["TextL2"].ToString();
                    TextBox2.Text = dt.Rows[0]["TextL3"].ToString();
                    TextBox3.Text = dt.Rows[0]["TextL4"].ToString();
                }
            }
        }
        #endregion Page_Load

        #region SaveClick
        /// <summary>
        /// SaveClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveClick(object sender, EventArgs e)
        {
            int retunValue = 0;

            string TextH1 = Country.Text;
            string TextL1 = Name.Text;
            string TextL2 = TextBox1.Text;
            string TextL3 = TextBox2.Text;
            string TextL4 = TextBox3.Text;
            retunValue = XBDataProvider.PopupRptSQ.SaveSQReport(companyCode, location, quatation, SqId, TextH1, TextL1, TextL2, TextL3, TextL4);
        }
        #endregion  SaveClick
    }
}
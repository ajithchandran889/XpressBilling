using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Globalization;
using System.Web.Security;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace XpressBilling.Account
{
    public partial class SalesQuote : System.Web.UI.Page
    {
        #region Page_Load
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }

                getsavedsalesinvoiceparameters(XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name));

                CompanyCode.Value = Session["CompanyCode"].ToString();
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

            bool header = HeaderCB.Checked;
            bool declaration = DeclarationCB.Checked;
            bool footer = FooterCB.Checked;
            int reportID = Convert.ToInt32(Report.SelectedValue.ToString());
            int copies = Convert.ToInt32(NoOfCopies.Text.Trim());
            //string headerText = Header.Text.Trim();
            //string declarationText = Declaration.Text.Trim();
            //string footerText = Footer.Text.Trim();
            string headerText = Request.Form[Header.UniqueID];
            string declarationText = Request.Form[Declaration.UniqueID];
            string footerText = Request.Form[Footer.UniqueID];
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            AccountDetails objAccountDetails = json_serializer.Deserialize<AccountDetails>(AccNoid.Value.Replace("__type", "type"));

            retunValue = XBDataProvider.RptSalesQuote.SaveRptSalesQuote(CompanyCode.Value, header, declaration, footer, reportID, copies,
             headerText, declarationText, footerText, objAccountDetails.AccountNo, objAccountDetails.name, objAccountDetails.BankCode,
             objAccountDetails.BankName, objAccountDetails.Branch, objAccountDetails.IFSC, objAccountDetails.IBAN,"");//#TBD
            if (retunValue == 1)
            {
                SaveSuccess.Visible = true;
                //UpdateSuccess.Visible = true;
                failure.Visible = false;
                getsavedsalesinvoiceparameters(XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name));
            }
            else
            {
                SaveSuccess.Visible = false;
                //UpdateSuccess.Visible = false;
                failure.Visible = true;
            }
        }
        #endregion  SaveClick
        private void getsavedsalesinvoiceparameters(string hdncompanyCode)
        {
            DataTable dtTable = XBDataProvider.RptSalesQuote.GetSalesQuoteParameters(hdncompanyCode);
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {

                Header.Text = dtTable.Rows[i]["HeaderText"].ToString();
                Declaration.Text = dtTable.Rows[i]["DeclarationText"].ToString();
                Footer.Text = dtTable.Rows[i]["FooterText"].ToString();
                NoOfCopies.Text = dtTable.Rows[i]["Copies"].ToString();
                AccountDetails objAccountDetails = new AccountDetails();
                txtAccNoid.Text = dtTable.Rows[i]["AccountNo"].ToString();
                objAccountDetails.AccountNo = dtTable.Rows[i]["AccountNo"].ToString();
                objAccountDetails.name = dtTable.Rows[i]["AccountName"].ToString();
                objAccountDetails.BankCode = dtTable.Rows[i]["BankCode"].ToString();
                objAccountDetails.BankName = dtTable.Rows[i]["BankName"].ToString();
                objAccountDetails.Branch = dtTable.Rows[i]["Branch"].ToString();
                objAccountDetails.IFSC = dtTable.Rows[i]["IFSC"].ToString();
                objAccountDetails.IBAN = dtTable.Rows[i]["IBAN"].ToString();
                lblBankCode.InnerText = dtTable.Rows[i]["BankCode"].ToString();
                if (dtTable.Rows[i]["Header"].ToString() == "True")
                {
                    HeaderCB.Checked = true;
                    Header.ReadOnly = false;
                }
                else
                {
                    HeaderCB.Checked = false;
                    Header.ReadOnly = true;
                }
                if (dtTable.Rows[i]["Declaration"].ToString() == "True")
                {
                    DeclarationCB.Checked = true;
                    Declaration.ReadOnly = false;
                }

                else
                {
                    DeclarationCB.Checked = false;
                    Declaration.ReadOnly = true;
                }
                if (dtTable.Rows[i]["Footer"].ToString() == "True")
                {
                    FooterCB.Checked = true;
                    Footer.ReadOnly = false;
                }
                else
                {
                    FooterCB.Checked = false;
                    Footer.ReadOnly = true;
                }
                if (BankCB.Checked == true)
                {

                    txtAccNoid.ReadOnly = false;
                }
                else
                {
                    txtAccNoid.ReadOnly = true;
                }

            }
        }

        [WebMethod]
        public static List<AccountDetails> GetAccountDetails(string companyCode)
        {
            List<AccountDetails> result = new List<AccountDetails>();
            try
            {

                DataTable dtTable = XBDataProvider.RptSalesQuote.GetAllActiveAcoountNo(companyCode);
                //Session["BPDetails"] = dtTable;

                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    AccountDetails AccountDetails = new AccountDetails();
                    AccountDetails.AccountNo = dtTable.Rows[i]["AccountNo"].ToString();
                    AccountDetails.name = dtTable.Rows[i]["Name"].ToString();
                    AccountDetails.Branch = dtTable.Rows[i]["Branch"].ToString();
                    AccountDetails.BankCode = dtTable.Rows[i]["BankCode"].ToString();
                    AccountDetails.AccountType = dtTable.Rows[i]["AccountType"].ToString();
                    AccountDetails.IBAN = dtTable.Rows[i]["IBAN"].ToString();
                    AccountDetails.IFSC = dtTable.Rows[i]["IFSC"].ToString();
                    AccountDetails.SWIFT = dtTable.Rows[i]["SWIFT"].ToString();
                    AccountDetails.MICR = dtTable.Rows[i]["MICR"].ToString();
                    AccountDetails.BankName = dtTable.Rows[i]["BankName"].ToString();
                    AccountDetails.CompanyCode = dtTable.Rows[i]["CompanyCode"].ToString();
                    result.Add(AccountDetails);
                }

            }
            catch (Exception e)
            {

            }


            return result;
        }
        public class AccountDetails
        {
            public string AccountNo { get; set; }
            public string name { get; set; }
            public string Branch { get; set; }
            public string BankCode { get; set; }
            public string AccountType { get; set; }
            public string IBAN { get; set; }
            public string IFSC { get; set; }
            public string SWIFT { get; set; }
            public string MICR { get; set; }
            public string BankName { get; set; }
            public string CompanyCode { get; set; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class PaymentMode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadPaymentModeList();
            }
            
        }

        protected void PaymentModePageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListPaymentMode.PageIndex = e.NewPageIndex;
            LoadPaymentModeList();
        }
        private void LoadPaymentModeList()
        {
            ListPaymentMode.DataSource = XBDataProvider.PaymentMode.GetAllPaymentMode();
            ListPaymentMode.DataBind();
        }
        protected void listPaymentModeDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in ListPaymentMode.Rows)
            {
                DropDownList ddlCompanyUser = gvRow.FindControl("PaymentModeDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlCompanyUser != null && hfSelectedValue != null)
                {
                    
                    ddlCompanyUser.SelectedValue = hfSelectedValue.Value;
                }
            }
        }

        protected void PaymentModeDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int companyId = Convert.ToInt32(ddl.Attributes["IdPaymentMode"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.PaymentMode.ActivatePaymentMode(companyId);
                }
                else
                {
                    XBDataProvider.PaymentMode.DeActivatePaymentMode(companyId);
                }
            }
            catch(Exception ex)
            {

            }
            
        }

        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in ListPaymentMode.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");  
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.PaymentMode.DeletePaymentModes(ids);
            LoadPaymentModeList();   
        }
    }
}
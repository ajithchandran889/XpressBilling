using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XBDataProvider;

namespace XpressBilling.Account
{
    public partial class Currency : System.Web.UI.Page
    {
        //protected void btnSaveCurrencyClick(object sender, EventArgs e)
        //{
        //    int currencyId = XBDataProvider.Currency.SaveCurrency(inputCurrency.Value,inputCurrency.Value,inputName.Value,
        //                                                            inputDecimal.Value,inputDecimal.Value,User.Identity.Name,
        //                                                            inputDate.Text, inputStatus.Value);
        //    listCurrency.DataSource = XBDataProvider.Currency.GetAllCurrencies();
        //    listCurrency.DataBind();
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCurrencyList();
            }
        }

        protected void listCurrencyPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listCurrency.PageIndex = e.NewPageIndex;
            LoadCurrencyList();
        }
        private void LoadCurrencyList()
        {
            listCurrency.DataSource = XBDataProvider.Currency.GetAllCurrencies();
            listCurrency.DataBind();
        }
        protected void CurrencyUserDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in listCurrency.Rows)
            {
                DropDownList ddlListUser = gvRow.FindControl("CurrencyStatusDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlListUser != null && hfSelectedValue != null)
                {
                    ddlListUser.SelectedValue = hfSelectedValue.Value;
                }
            }
        }

        protected void CurrencyStatusDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int currencyId = Convert.ToInt32(ddl.Attributes["currencyId"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.Currency.ActivateCurrency(currencyId);
                }
                else
                {
                    XBDataProvider.Currency.DeActivateCurrency(currencyId);
                }
                 LoadCurrencyList();
            }
            
            catch (Exception ex)
            {

            }

        }
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in listCurrency.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.Currency.DeleteCurrency(ids);
            LoadCurrencyList();
        }
    }
}
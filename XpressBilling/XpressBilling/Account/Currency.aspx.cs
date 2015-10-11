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
        protected void Page_Load(object sender, EventArgs e)
        {
            listCurrency.DataSource = XBDataProvider.Currency.GetAllCurrencies();
            listCurrency.DataBind();
        }

        protected void btnSaveCurrencyClick(object sender, EventArgs e)
        {
            int currencyId = XBDataProvider.Currency.SaveCurrency(inputCurrency.Value,inputCurrency.Value,inputName.Value,
                                                                    inputDecimal.Value,inputDecimal.Value,User.Identity.Name,
                                                                    inputDate.Text, inputStatus.Value);
            listCurrency.DataSource = XBDataProvider.Currency.GetAllCurrencies();
            listCurrency.DataBind();
        }
    }
}
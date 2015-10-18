﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class BankCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadBankCodeList();
        }

        protected void listBankCodePageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listBankCode.PageIndex = e.NewPageIndex;
            LoadBankCodeList();
        }
        private void LoadBankCodeList()
        {
            listBankCode.DataSource = XBDataProvider.BankCode.GetBankCode();
            listBankCode.DataBind();
        }
    }
}
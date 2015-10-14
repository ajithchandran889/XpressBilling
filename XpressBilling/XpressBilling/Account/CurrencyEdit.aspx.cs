﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class CurrencyEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null)
                {
                    DataTable currencyDetails = XBDataProvider.Currency.GetCurrencyById(id);
                    if (currencyDetails.Rows.Count > 0)
                    {
                        SetCurrencyDetails(currencyDetails);

                    }
                }
                else
                {
                    CurrencyId.Value = "0";
                }
            }

        }

        public void SetCurrencyDetails(DataTable currencyDetails)
        {
            DataRow row = currencyDetails.Rows[0];
            Company.Text = row["CompanyCode"].ToString();
            Company.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            Currency.Text = row["CurrencyCode"].ToString(); ;
            Decimal.Text = row["Decimal"].ToString();
            UserName.Text = row["Reference"].ToString();
            CurrencyId.Value = row["ID"].ToString();

        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                if (CurrencyId.Value != "0")
                {
                    XBDataProvider.Currency.UpdateCurrency(CurrencyId.Value, Company.Text, Currency.Text, Name.Text, Decimal.Text, UserName.Text, User.Identity.Name, DateTime.Today, true);
                }
                else
                {
                    XBDataProvider.Currency.SaveCurrency(Company.Text, Currency.Text, Name.Text, Decimal.Text, UserName.Text, User.Identity.Name, DateTime.Today, true);
                    ClearInputs(Page.Controls);
                }


            }
            catch (Exception ex)
            {

            }

            //Label lblMsg = this.Master.FindControl("Message") as Label;
            //lblMsg.Text = "Company added successfully";
            //lblMsg.Visible = true;
        }
        private void ClearInputs(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                else if (ctrl is DropDownList)
                    ((DropDownList)ctrl).ClearSelection();

                ClearInputs(ctrl.Controls);
            }
        }
    }
}
using System;
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
                if (id != null && id != 0)
                {
                    DataTable currencyDetails = XBDataProvider.Currency.GetCurrencyById(id);
                    if (currencyDetails.Rows.Count > 0)
                    {
                        SetCurrencyDetails(currencyDetails);

                    }
                }
                else
                {
                    lbldate.Visible = false;
                    lblstatus.Visible = false;
                    lblusername.Visible = false;
                    UserName.Visible = false;
                    Date.Visible = false;
                    ddlStatus.Visible = false;
                    CurrencyId.Value = "0";
                }
            }
            //else
            //{
            //    lbldate.Visible = false;
            //    lblstatus.Visible = false;
            //    lblusername.Visible = false;
            //    UserName.Visible = false;
            //    Date.Visible = false;
            //    ddlStatus.Visible = false;
            //    CurrencyId.Value = "0";
            //}

        }

        public void SetCurrencyDetails(DataTable currencyDetails)
        {
            DataRow row = currencyDetails.Rows[0];
            //Company.Text = row["CompanyCode"].ToString();
            //Company.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            Currency.Text = row["CurrencyCode"].ToString();
            Currency.ReadOnly = true;
            Decimal.Text = row["Decimal"].ToString();
            CurrencyId.Value = row["ID"].ToString();
            UserName.Text = row["CreatedBy"].ToString();
            UserName.ReadOnly = true;
            Date.Text = row["CreatedDate"].ToString();
            Date.ReadOnly = true;
            ddlStatus.SelectedValue = row["Status"].ToString();
            CurrencyId.Value = row["ID"].ToString();
        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                int status = 0;
                bool statusup;
                    if (ddlStatus.SelectedValue == "0")
                        statusup = false;
                    else
                        statusup = true;
                if (CurrencyId.Value != "0" && CurrencyId.Value!=null)
                {
                    status = XBDataProvider.Currency.UpdateCurrency(CurrencyId.Value, Name.Text, Decimal.Text, User.Identity.Name, User.Identity.Name, DateTime.Today, statusup);
                  if (status!=-1)
                  {
                      Message.Text = "Successfully updated";
                  }
                  else
                  {
                      Message.Text = "Oops..Something went wrong.Please try again";
                  }
                }
                else
                {
                    status = XBDataProvider.Currency.SaveCurrency(Session["CompanyCode"].ToString(), Currency.Text, Name.Text, Decimal.Text, User.Identity.Name, User.Identity.Name, DateTime.Today, true);
                    ClearInputs(Page.Controls);
                    if (status != -1)
                    {
                        Message.Text = "Successfully added";
                    }
                    else
                    {
                        Message.Text = "Oops..Something went wrong.Please try again";
                    }
                    ClearInputs(Page.Controls);
                }


            }
            catch (Exception ex)
            {

            }

           
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
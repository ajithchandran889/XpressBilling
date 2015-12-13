using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XpressBilling.Account
{
    public partial class CountryEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id!=0)
                {
                    DataTable countryDetails = XBDataProvider.Country.GetCountryById(id);
                    if (countryDetails.Rows.Count > 0)
                    {
                        SetcountryDetails(countryDetails);
                    }
                }
                else
                {
                    lbldate.Visible = false;
                    lblstatus.Visible = false;
                    lblusername.Visible = false;
                    UserName.Visible = false;
                    CreatedDate.Visible = false;
                    ddlStatus.Visible = false;
                    CountryId.Value = "0";                    
                }
            }
        }

        public void SetcountryDetails(DataTable countryDetails)
        {
            DataRow row = countryDetails.Rows[0];
            Country.Text = row["CountryCode"].ToString();
            Country.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            UserName.Text = row["Reference"].ToString();
            UserName.ReadOnly = true;
            CreatedDate.Text = Convert.ToDateTime(row["CreatedDate"]).ToString("MM'/'dd'/'yyyy");
            CreatedDate.ReadOnly = true;           
            ddlStatus.SelectedValue = row["Status"].ToString();
            CountryId.Value = row["ID"].ToString();

        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                int msgstatus = 0;
                hdncompanycode.Value = Session["CompanyCode"].ToString();
                if (CountryId.Value != "0" && CountryId.Value != null)
                {
                    bool status;
                    if (ddlStatus.SelectedValue == "0")
                        status = false;
                    else
                        status = true;
                    msgstatus = XBDataProvider.Country.UpdateCountry(Convert.ToInt32(CountryId.Value), Name.Text, User.Identity.Name, status);
                    if (msgstatus != -1)
                    {
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = true;
                        failure.Visible = false;
                    }
                    else
                    {
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = false;
                        failure.Visible = true;
                    }
                }
                else
                {
                    msgstatus = XBDataProvider.Country.SaveCountry(hdncompanycode.Value, Country.Text, Name.Text, User.Identity.Name, User.Identity.Name, DateTime.Today, true);
                    ClearInputs(Page.Controls);
                    if (msgstatus == 1)
                    {
                        SaveSuccess.Visible = true;
                        UpdateSuccess.Visible = false;
                        failure.Visible = false;
                        alreadyexist.Visible = false;
                    }
                    else if (msgstatus == -1)
                    {
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = false;
                        failure.Visible = false;
                        alreadyexist.Visible = true;
                    }
                    else
                    {
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = false;
                        failure.Visible = true;
                        alreadyexist.Visible = false;
                    }
                }

                
            }
            catch (Exception ex)
            {
                SaveSuccess.Visible = false;
                UpdateSuccess.Visible = false;
                failure.Visible = true;
                alreadyexist.Visible = false;
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
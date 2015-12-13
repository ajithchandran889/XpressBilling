using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XpressBilling.Account
{
    public partial class EditCity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                DataTable dtCity = XBDataProvider.City.GetAllActiveCountryCodes(Session["CompanyCode"].ToString());

                ddlCountry.DataSource = dtCity;
                ddlCountry.DataValueField = "CountryCode";
                ddlCountry.DataTextField = "Name";
                ddlCountry.DataBind();
                ListItem item = new ListItem();
                item.Text = "Select Country";
                item.Value = "0";
                ddlCountry.Items.Insert(0, item);
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    DataTable CityDetails = XBDataProvider.City.GetCityById(id);
                    if (CityDetails.Rows.Count > 0)
                    {
                        SetCityDetails(CityDetails);
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
                    CityId.Value = "0";
                }
            }
        }

        public void SetCityDetails(DataTable CityDetails)
        {
            DataRow row = CityDetails.Rows[0];
            City.Text = row["CityCode"].ToString();
            City.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            ddlCountry.SelectedValue = row["CountryCode"].ToString();
            UserName.Text = row["CreatedBy"].ToString();
            UserName.ReadOnly = true;
            Date.Text = row["CreatedDate"].ToString();
            Date.ReadOnly = true;
            ddlStatus.SelectedValue = row["Status"].ToString();
            CityId.Value = row["ID"].ToString();

        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                int msgstatus = 0;
                hdncompanycode.Value = Session["CompanyCode"].ToString();
                if (CityId.Value != "0" && CityId.Value != null)
                {
                    bool status;
                    if (ddlStatus.SelectedValue == "0")
                        status = false;
                    else
                        status = true;
                    msgstatus = XBDataProvider.City.UpdateCity(Convert.ToInt32(CityId.Value), Name.Text, ddlCountry.SelectedValue, User.Identity.Name, status);
                    if (msgstatus == 1)
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
                    string reference = "";
                    msgstatus = XBDataProvider.City.SaveCity(hdncompanycode.Value, City.Text, Name.Text, ddlCountry.SelectedValue, reference, User.Identity.Name, true);
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
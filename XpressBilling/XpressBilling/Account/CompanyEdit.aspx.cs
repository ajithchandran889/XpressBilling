

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using XBDataProvider;

namespace XpressBilling.Account
{
    public partial class CompanyEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                FormationDate.Text = Convert.ToDateTime(DateTime.Now).ToString("MM'/'dd'/'yyyy");
                DataTable dtCountries = XBDataProvider.Country.GetCountries(Session["CompanyCode"].ToString());
                Country.DataSource = dtCountries;
                Country.DataValueField = "CountryCode";
                Country.DataTextField = "name";
                Country.DataBind();
                ListItem item = new ListItem();
                item.Text = "Select Country";
                item.Value = "";
                Country.Items.Insert(0, item);

                DataTable dtCurrency = XBDataProvider.Currency.GetAllActiveCurrencies(Session["CompanyCode"].ToString());

                ddlCurrency.DataSource = dtCurrency;
                ddlCurrency.DataValueField = "CurrencyCode";
                ddlCurrency.DataTextField = "Name";
                ddlCurrency.DataBind();
                ListItem itemcurrency = new ListItem();
                itemcurrency.Text = "Select Currency";
                itemcurrency.Value = "";
                ddlCurrency.Items.Insert(0, itemcurrency);
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    DataTable companyDetails = XBDataProvider.Company.GetCompanyById(id);
                    if (companyDetails.Rows.Count > 0)
                    {
                        DataTable dtTable = XBDataProvider.City.GetCitiesByCompany(Session["CompanyCode"].ToString());
                        City.DataSource = dtTable;
                        City.DataValueField = "CityCode";
                        City.DataTextField = "Name";
                        City.DataBind();
                        SetCompanyDetails(companyDetails);
                    }
                }
                else
                {
                    CompanyId.Value = "0";
                    lblstatus.Visible = false;
                    ddlStatus.Visible = false;
                    FormationDate.Text = Convert.ToDateTime(DateTime.Now).ToString("MM'/'dd'/'yyyy");
                    FormationDate.ReadOnly = true;
                }
            }

        }

        public void SetCompanyDetails(DataTable companyDetails)
        {
            DataRow row = companyDetails.Rows[0];
            AddCompany.Text = row["CompanyCode"].ToString();
            AddCompany.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            //DateTime formationDate = DateTime.ParseExact(FormationDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            string formationDate = Convert.ToDateTime(row["FormationDate"]).ToString("MM'/'dd'/'yyyy");
            FormationDate.Text = formationDate.ToString();
            TIN.Text = row["TaxId"].ToString();
            RegistrationNo.Text = row["RegistrationNumber"].ToString();
            PAN.Text = row["PermanantAccountNo"].ToString();
            Phone.Text = row["Phone"].ToString();
            Phone.ReadOnly = true;
            Mobile.Text = row["Mobile"].ToString();
            Mobile.ReadOnly = true;
            Fax.Text = row["Fax"].ToString();
            Fax.ReadOnly = true;
            Email.Text = row["Email"].ToString();
            Email.ReadOnly = true;
            Web.Text = row["Web"].ToString();
            Web.ReadOnly = true;
            ContactPerson.Text = row["ContactPerson"].ToString();
            ContactPerson.ReadOnly = true;
            Designation.Text = row["Designation"].ToString();
            Designation.ReadOnly = true;
            Address1.Text = row["Address1"].ToString();
            Address1.ReadOnly = true;
            Address2.Text = row["Address2"].ToString();
            Address2.ReadOnly = true;
            ddlCurrency.SelectedValue = row["CurrencyCode"].ToString();
            City.SelectedValue = row["City"].ToString();
            City.Enabled = false;
            Area.Text = row["Area"].ToString();
            Area.ReadOnly = true;
            State.Text = row["State"].ToString();
            State.ReadOnly = true;
            Country.SelectedValue = row["Country"].ToString();
            Country.Enabled = false;
            Zip.Text = row["ZipCode"].ToString();
            Zip.ReadOnly = true;
            Note.Text = row["Note"].ToString();
            CompanyId.Value = row["ID"].ToString();
            ddlStatus.SelectedValue = row["Status"].ToString();
            if (row["Logo"].ToString() == null || row["Logo"].ToString()=="")
            {
                imgPreview.ImageUrl = "/Images/user/preview.png";
            }
            else
                imgPreview.ImageUrl = HttpUtility.HtmlDecode(row["Logo"].ToString());
            if (row["IsInitial"].ToString() == "True")
            {
                ContactPerson.Attributes.Add("class", ContactPerson.Attributes["class"].ToString().Replace("required", ""));
                Designation.Attributes.Add("class", Designation.Attributes["class"].ToString().Replace("required", ""));
                Mobile.Attributes.Add("class", Mobile.Attributes["class"].ToString().Replace("required", ""));
                Email.Attributes.Add("class", Email.Attributes["class"].ToString().Replace("required", ""));
                Address1.Attributes.Add("class", Address1.Attributes["class"].ToString().Replace("required", ""));
                Address2.Attributes.Add("class", Address2.Attributes["class"].ToString().Replace("required", ""));
                Area.Attributes.Add("class", Area.Attributes["class"].ToString().Replace("required", ""));
                Zip.Attributes.Add("class", Zip.Attributes["class"].ToString().Replace("required", ""));
                State.Attributes.Add("class", State.Attributes["class"].ToString().Replace("required", ""));
            }

        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                string path = "";
                string absolutePath = "";
                if (inputUpload.HasFile)
                {
                    string folderPath = "~/Images/Company/"+AddCompany.Text+"/Logo/";
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    if (!System.IO.Directory.Exists(Server.MapPath("~") + "/Images/Company/" + AddCompany.Text + "/Logo/"))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~") + "/Images/Company/" + AddCompany.Text + "/Logo/");
                    }
                    path = Server.MapPath(folderPath) + AddCompany.Text + "_logo_" + timestamp + Path.GetExtension(inputUpload.FileName);
                    absolutePath = folderPath + AddCompany.Text + "_logo_" + timestamp + Path.GetExtension(inputUpload.FileName); ;
                    imgPreview.ImageUrl = absolutePath;
                    inputUpload.SaveAs(path);
                }
                bool status = false;
                bool dbstatus;
                if (ddlStatus.SelectedValue == "0")
                    dbstatus = false;
                else
                    dbstatus = true;
                DateTime formationDate = DateTime.ParseExact(FormationDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                if (CompanyId.Value != "0" && CompanyId.Value != "")
                {
                    status = XBDataProvider.Company.UpdateCompany(CompanyId.Value, Name.Text, PAN.Text, formationDate, TIN.Text, RegistrationNo.Text, absolutePath, Note.Text, User.Identity.Name, dbstatus, ddlCurrency.SelectedValue);
                    if (status)
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
                    int retunValue = 0;
                    dbstatus = true;
                    retunValue = XBDataProvider.Company.SaveCompany(AddCompany.Text, Name.Text, PAN.Text, formationDate, TIN.Text, RegistrationNo.Text, ContactPerson.Text, absolutePath, Note.Text, true, "", User.Identity.Name,
                                                                     Phone.Text, Mobile.Text, Email.Text, Web.Text, Designation.Text, Address1.Text, Address2.Text, Request.Form[City.UniqueID], Area.Text,Convert.ToInt32(Zip.Text), Country.SelectedValue, State.Text, Fax.Text, dbstatus, ddlCurrency.SelectedValue);

                    if (retunValue == 1)
                    {
                        ClearInputs(Page.Controls);
                        SaveSuccess.Visible = true;
                        UpdateSuccess.Visible = false;
                        failure.Visible = false;
                        alreadyexist.Visible = false;
                    }
                    else if (retunValue == -1)
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

        [WebMethod]
        public static List<city> GetCities(string countryCode)
        {
            List<city> result = new List<city>();
            try
            {
                DataTable dtTable = XBDataProvider.City.GetCities(countryCode);
                DataRow row = null;
                for (int index = 0; index < dtTable.Rows.Count; index++)
                {
                    row = dtTable.Rows[index];
                    city city = new city();
                    city.cityCode = row["CityCode"].ToString();
                    city.cityName = row["Name"].ToString();
                    result.Add(city);
                }
            }
            catch (Exception e)
            {

            }


            return result;
        }

        [WebMethod]
        public static List<string> GatAllContacts(string companyCode)
        {
            List<string> result = new List<string>();
            DataTable dtTable = XBDataProvider.Contact.GetAllContactCode(companyCode);
            DataRow row = null;
            for (int index = 0; index < dtTable.Rows.Count; index++)
            {
                row = dtTable.Rows[index];
                result.Add(row["Contact"].ToString());
            }
            return result;
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
            imgPreview.ImageUrl = "/Images/user/preview.png";
        }
    }

    public class city
    {
        public string cityName { get; set; }
        public string cityCode { get; set; }
    }
}
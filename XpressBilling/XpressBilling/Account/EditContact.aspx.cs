using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using XBDataProvider;

namespace XpressBilling.Account
{
    public partial class EditAccount : System.Web.UI.Page
    {
         protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                DataTable dtCountries = XBDataProvider.Country.GetCountries(Session["CompanyCode"].ToString());
                Country.DataSource = dtCountries;
                Country.DataValueField = "CountryCode";
                Country.DataTextField = "name";
                Country.DataBind();
                ListItem item = new ListItem();
                item.Text = "Select Country";
                item.Value = "";
                Country.Items.Insert(0, item);
                //DataTable dtCompanies = XBDataProvider.Company.GetAllActiveCompany();
                //ddlCompany.DataSource = dtCompanies;
                //ddlCompany.DataValueField = "CompanyCode";
                //ddlCompany.DataTextField = "Name";
                //ddlCompany.DataBind();
                //ListItem itemcompany = new ListItem();
                //itemcompany.Text = "Select Company";
                //itemcompany.Value = "";
                //ddlCompany.Items.Insert(0, itemcompany);
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    DataTable contactDetails = XBDataProvider.Contact.GetContactCodeById(id);
                    if (contactDetails.Rows.Count > 0)
                    { DataTable dtTable = XBDataProvider.City.GetCitiesByCompany(Session["CompanyCode"].ToString());
                        City.DataSource = dtTable;
                        City.DataValueField = "CityCode";
                        City.DataTextField = "Name";
                        City.DataBind();
                        SetContactDetails(contactDetails);
                    }
                }
                else
                {
                    getinitialloadingContact();                     
                }
            }

        }
         public void getinitialloadingContact()
         {
             ContactId.Value = "0";
             DataTable GetLastcontactCode = XBDataProvider.Contact.GetLastCreateContactCode();
             if (GetLastcontactCode.Rows.Count > 0)
             {
                 DataRow row = GetLastcontactCode.Rows[0];
                 Contact.Text = (Convert.ToInt32(row["Contact"]) + 1).ToString();
             }
             else
             {
                 Random rnd = new Random();
                 string addnewContactCode = rnd.Next(1000000000, 1000000001).ToString();
                 Contact.Text = addnewContactCode;
             }
             lblusername.Visible = false;
             Username.Visible = false;
             lblDate.Visible = false;
             Date.Visible = false;
             ddlStatus.Visible = false;
             lblstatus.Visible = false;
             Username.Text = User.Identity.Name;
             Date.Text = Convert.ToDateTime(DateTime.Now).ToString("MM'/'dd'/'yyyy");
         }
        public void SetContactDetails(DataTable companyDetails)
        {
            DataRow row = companyDetails.Rows[0];
            Contact.Text = row["Contact"].ToString();
            Contact.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            string formationDate = Convert.ToDateTime(row["CreatedDate"]).ToString("MM/dd/yyyy");
            Date.Text = formationDate;
            Username.Text = row["UpdatedBy"].ToString();
            Phone.Text = row["Phone"].ToString();
            Mobile.Text = row["Mobile"].ToString();
            Fax.Text = row["Fax"].ToString();
            Email.Text = row["Email"].ToString();
            Web.Text = row["Web"].ToString();
            Designation.Text = row["Designation"].ToString();
            Address1.Text = row["Address1"].ToString();
            Address2.Text = row["Address2"].ToString();
            City.SelectedValue = row["CityCode"].ToString();
            Area.Text = row["Area"].ToString();
            State.Text = row["State"].ToString();
            Country.SelectedValue = row["CountryCode"].ToString();
            txtcompany.Text = row["CompanyName"].ToString();
            //ddlCompany.SelectedValue = row["CompanyCode"].ToString();
            //ddlCompany.Enabled = false;
            Zip.Text = row["ZipPostalCode"].ToString();
            ContactId.Value = row["ID"].ToString();
            ddlStatus.SelectedValue = row["Status"].ToString();
        }
        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                hdncompanycode.Value = Session["CompanyCode"].ToString();
                //int zipcode = Convert.ToInt16(Zip.Text);
                bool status = false;
                if (ddlStatus.SelectedValue == "0")
                    status = false;
                else
                    status = true;
                string note = "";
                string errmsg = "";
                if (ContactId.Value != "0" && ContactId.Value != "")
                {
                    status = XBDataProvider.Contact.UpdateContact(ContactId.Value.ToString(), Name.Text, Designation.Text, Phone.Text, Mobile.Text, Fax.Text, Email.Text, Web.Text, Address1.Text, Address2.Text, Request.Form[City.UniqueID], Area.Text, State.Text, Country.SelectedValue, Convert.ToInt32(Zip.Text), User.Identity.Name, status, txtcompany.Text);
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
                    retunValue = XBDataProvider.Contact.SaveContact(Contact.Text, Name.Text, hdncompanycode.Value, Designation.Text, txtcompany.Text, Phone.Text, Mobile.Text, Fax.Text, Email.Text, Web.Text, Address1.Text, Address2.Text, Request.Form[City.UniqueID], Area.Text, State.Text, Country.SelectedValue, Convert.ToInt32(Zip.Text), true, note, errmsg, User.Identity.Name);
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
        public static List<Citycontact> GetCities(string countryCode)
        {
            List<Citycontact> result = new List<Citycontact>();
            try
            {
                DataTable dtTable = XBDataProvider.City.GetCities(countryCode);
                DataRow row = null;
                for (int index = 0; index < dtTable.Rows.Count; index++)
                {
                    row = dtTable.Rows[index];
                    Citycontact Citycontact = new Citycontact();
                    Citycontact.CityCode = row["CityCode"].ToString();
                    Citycontact.Name = row["Name"].ToString();
                    result.Add(Citycontact);
                }
            }
            catch(Exception e)
            {

            }
            return result;
        }

        //[WebMethod]
        //public static List<string> GatAllContacts()
        //{
        //    List<string> result = new List<string>();
        //    DataTable dtTable = XBDataProvider.Contact.GetAllContactCode();
        //    DataRow row = null;
        //    for (int index = 0; index < dtTable.Rows.Count; index++)
        //    {
        //        row = dtTable.Rows[index];
        //        result.Add(row["Contact"].ToString());
        //    }
        //    return result;
        //}

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
            getinitialloadingContact();  
        }
    }

    public class Citycontact
    {
        public string Name { get; set; }
        public string CityCode { get; set; }
    }
}
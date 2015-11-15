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
                DataTable dtCountries = XBDataProvider.Country.GetCountries();
                Country.DataSource = dtCountries;
                Country.DataValueField = "CountryCode";
                Country.DataTextField = "name";
                Country.DataBind();
                ListItem item = new ListItem();
                item.Text = "Select Country";
                item.Value = "";
                Country.Items.Insert(0, item);
                DataTable dtCompanies = XBDataProvider.Company.GetAllActiveCompany();
                ddlCompany.DataSource = dtCompanies;
                ddlCompany.DataValueField = "CompanyCode";
                ddlCompany.DataTextField = "Name";
                ddlCompany.DataBind();
                ListItem itemcompany = new ListItem();
                itemcompany.Text = "Select Company";
                itemcompany.Value = "";
                ddlCompany.Items.Insert(0, itemcompany);
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null)
                {
                    DataTable contactDetails = XBDataProvider.Contact.GetContactCodeById(id);
                    if (contactDetails.Rows.Count > 0)
                    {
                        SetContactDetails(contactDetails);
                    }
                }
                else
                {
                    ContactId.Value = "0";
                }
            }

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
            Citycontact.SelectedValue = row["CityCode"].ToString();
            Area.Text = row["Area"].ToString();
            State.Text = row["State"].ToString();
            Country.SelectedValue = row["CountryCode"].ToString();
            ddlCompany.SelectedValue = row["CompanyCode"].ToString();
            ddlCompany.Enabled = false;
            Zip.Text = row["ZipPostalCode"].ToString();
            ContactId.Value = row["ID"].ToString();
        }
        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                int zipcode = Convert.ToInt16(Zip.Text);
                bool status = false;
                string note = "";
                string errmsg = "";
                if (ContactId.Value != "0" && ContactId.Value != "")
                {
                    status = XBDataProvider.Contact.UpdateContact(ContactId.Value.ToString(), Name.Text, Designation.Text, Phone.Text, Mobile.Text, Fax.Text, Email.Text, Web.Text, Address1.Text, Address2.Text, Citycontact.SelectedValue, Area.Text, State.Text, Country.SelectedValue, Convert.ToInt32(zipcode), true, User.Identity.Name);
                    if (status)
                    {
                        lblMsg.InnerText = "Successfully updated";
                    }
                    else
                    {
                        lblMsg.InnerText = "Oops..Something went wrong.Please try again";
                    }
                }
                else
                {
                    int retunValue = 0;                    
                    retunValue = XBDataProvider.Contact.SaveContact(Contact.Text, Name.Text, ddlCompany.SelectedValue.ToString(), Designation.Text, ddlCompany.SelectedItem.ToString(), Phone.Text, Mobile.Text, Fax.Text, Email.Text, Web.Text, Address1.Text, Address2.Text, Citycontact.SelectedValue, Area.Text, State.Text, Country.SelectedValue,Convert.ToInt32( zipcode), true, note, errmsg, User.Identity.Name);
                    if (retunValue >= 1)
                    {
                        ClearInputs(Page.Controls);
                        lblMsg.InnerText = "Successfully added";
                    }
                    else if (retunValue == -1)
                    {
                        lblMsg.InnerText = "Contact already exist";
                    }
                    else
                    {
                        lblMsg.InnerText = "Oops..Something went wrong.Please try again";
                    }                    
                }
            }
            catch (Exception ex)
            {

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
        }
    }

    public class Citycontact
    {
        public string Name { get; set; }
        public string CityCode { get; set; }
    }
}
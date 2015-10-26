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
            Phone.Text = row["Phone"].ToString();
           // Phone.ReadOnly = true;
            Mobile.Text = row["Mobile"].ToString();
            //Mobile.ReadOnly = true;
            Fax.Text = row["Fax"].ToString();
            //Fax.ReadOnly = true;
            Email.Text = row["Email"].ToString();
            //Email.ReadOnly = true;
            Web.Text = row["Web"].ToString();
            //Web.ReadOnly = true;
            Designation.Text = row["Designation"].ToString();
            //Designation.ReadOnly = true;
            Address1.Text = row["Address1"].ToString();
            //Address1.ReadOnly = true;
            Address2.Text = row["Address2"].ToString();
            //Address2.ReadOnly = true;
            Citycontact.SelectedValue = row["CityCode"].ToString();
           // Citycontact.Enabled = false;
            Area.Text = row["Area"].ToString();
           // Area.ReadOnly = true;
            State.Text = row["State"].ToString();
            //State.ReadOnly = true;
            Country.SelectedValue = row["CountryCode"].ToString();
            ddlCompany.SelectedValue = row["CompanyCode"].ToString();
            //Country.Enabled = false;
            Zip.Text = row["ZipPostalCode"].ToString();
           // Zip.ReadOnly = true;
            ContactId.Value = row["ID"].ToString();
        }
        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {               
                bool status = false;
                if (CompanyId.Value != "0" && CompanyId.Value != "")
                {
                    status = XBDataProvider.Company.UpdateCompany(ContactId.Value, Name.Text, User.Identity.Name);
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
                    retunValue = XBDataProvider.Company.SaveCompany(ddlCompany.Text, Name.Text, true, "", User.Identity.Name,
                                                                     Phone.Text, Mobile.Text, Email.Text, Web.Text, Designation.Text, Address1.Text, Address2.Text, Citycontact.SelectedValue, Area.Text, Zip.Text, Country.SelectedValue, State.Text, Fax.Text);
                    if (retunValue>=1)
                    {
                        ClearInputs(Page.Controls);
                        lblMsg.InnerText = "Successfully added";
                    }
                    else if(retunValue==-1)
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
        public static List<citypages> GetCities(string countryCode)
        {
            List<citypages> result = new List<citypages>();
            try
            {
                DataTable dtTable = XBDataProvider.City.GetCities(countryCode);
                DataRow row = null;
                for (int index = 0; index < dtTable.Rows.Count; index++)
                {
                    row = dtTable.Rows[index];
                    citypages citypages = new citypages();
                    citypages.cityCodethis = row["CityCode"].ToString();
                    citypages.cityNamethis = row["Name"].ToString();
                    result.Add(citypages);
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

    public class citypages
    {
        public string cityNamethis { get; set; }
        public string cityCodethis { get; set; }
    }
}
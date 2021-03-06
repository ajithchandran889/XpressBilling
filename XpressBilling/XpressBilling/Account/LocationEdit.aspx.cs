﻿using System;
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
    public partial class LocationEdit : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        DataTable dtCountries = XBDataProvider.Country.GetCountries();

        //        Country.DataSource = dtCountries;
        //        Country.DataValueField = "Id";
        //        Country.DataTextField = "name";
        //        Country.DataBind();
        //        ListItem item = new ListItem();
        //        item.Text = "Select Country";
        //        item.Value = "0";
        //        Country.Items.Insert(0, item);
        //        int id = Convert.ToInt32(Request.QueryString["Id"]);
        //        if (id != null)
        //        {
        //            DataTable LocationDetails = XBDataProvider.Location.GetLocationById(id);
        //            if (LocationDetails.Rows.Count > 0)
        //            {
        //                SetLocationDetails(LocationDetails);

        //            }

        //        }
        //        else
        //        {
        //            CompanyId.Value = "0";
        //        }
        //    }

        //}

        //public void SetLocationDetails(DataTable LocationDetails)
        //{
        //    DataRow row = LocationDetails.Rows[0];
        //    Location.Text = row["LocationCode"].ToString();
        //    Location.ReadOnly = true;
        //    Name.Text = row["Name"].ToString();
        //    string formationDate = Convert.ToDateTime(row["FormationDate"]).ToString("MM/dd/yyyy");
        //    FormationDate.Text = formationDate;
        //    TIN.Text = row["TaxId"].ToString();

        //    txtregno.Text = row["RegistrationNumber"].ToString();
        //    PAN.Text = row["PermanantAccountNo"].ToString();
        //    Phone.Text = row["Phone"].ToString();
        //    Mobile.Text = row["Mobile"].ToString();
        //    Fax.Text = row["Fax"].ToString();
        //    Email.Text = row["Email"].ToString();
        //    Web.Text = row["Web"].ToString();
        //    ContactPerson.Text = row["contactpersonname"].ToString();
        //    Designation.Text = row["Designation"].ToString();
        //    Address1.Text = row["Address1"].ToString();
        //    Address2.Text = row["Address2"].ToString();
        //    City.Text = row["cityname"].ToString();
        //    Area.Text = row["Area"].ToString();
        //    State.Text = row["State"].ToString();
        //    Country.SelectedValue = row["countryname"].ToString();
        //    Zip.Text = row["ZipPostalCode"].ToString();
        //    Note.Text = row["Note"].ToString();
        //    LocationId.Value = row["LocationCode"].ToString();
        //    CompanyId.Value = row["CompanyCode"].ToString();
        //     ContactId.Value = row["Contact"].ToString();
        //}
        //protected void SaveClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //Session["CompanyCode"].ToString();
        //        string path = "";
        //        if (logoUpload.HasFile)
        //        {
        //            string filename = Path.GetFileName(logoUpload.FileName);
        //            path = Server.MapPath("~/Images/logo/") + filename;
        //            logoUpload.SaveAs(path);
        //        }   
        //        if (LocationId.Value != "0" && LocationId.Value != "")
        //        {
        //            XBDataProvider.Location.UpdateLocation(LocationId.Value, Name.Text, PAN.Text, FormationDate.Text, TIN.Text, txtregno.Text, Phone.Text, path, Note.Text, true, "", User.Identity.Name,
        //                                                           Phone.Text, Mobile.Text, Email.Text, Web.Text, ContactPerson.Text, Designation.Text, Address1.Text, Address2.Text, City.Text, Area.Text, Zip.Text, Country.Text, State.Text, Fax.Text);
        //        }
        //        else
        //        {
        //            XBDataProvider.Location.SaveLocation(Location.Text, Name.Text, PAN.Text, FormationDate.Text, TIN.Text, txtregno.Text, Phone.Text, path, Note.Text, true, "", User.Identity.Name,
        //                                                            Phone.Text, Mobile.Text, Email.Text, Web.Text, ContactPerson.Text, Designation.Text, Address1.Text, Address2.Text, City.Text, Area.Text, Zip.Text, Country.Text, State.Text, Fax.Text);
        //            ClearInputs(Page.Controls);
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    //Label lblMsg = this.Master.FindControl("Message") as Label;
        //    //lblMsg.Text = "Company added successfully";
        //    //lblMsg.Visible = true;
        //}
        //private void ClearInputs(ControlCollection ctrls)
        //{
        //    foreach (Control ctrl in ctrls)
        //    {
        //        if (ctrl is TextBox)
        //            ((TextBox)ctrl).Text = string.Empty;
        //        else if (ctrl is DropDownList)
        //            ((DropDownList)ctrl).ClearSelection();

        //        ClearInputs(ctrl.Controls);
        //    }
        //}

        // }
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
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    DataTable companyDetails = XBDataProvider.Location.GetLocationById(id);
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
                    PageStatus.Value = "create";
                    CompanyId.Value = "0";
                    lblstatus.Visible = false;
                    ddlStatus.Visible = false;
                    FormationDate.Text = Convert.ToDateTime(DateTime.Now).ToString("MM'/'dd'/'yyyy");
                    //FormationDate.ReadOnly = true;
                }
            }

        }

        public void SetCompanyDetails(DataTable companyDetails)
        {
            PageStatus.Value = "edit";
            DataRow row = companyDetails.Rows[0];
            LocationCode.Text = row["LocationCode"].ToString();
            LocationCode.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            string formationDate = Convert.ToDateTime(row["FormationDate"]).ToString("MM'/'dd'/'yyyy");
            FormationDate.Text = formationDate;
            FormationDate.ReadOnly = true;
            TIN.Text = row["TaxId"].ToString();
            RegistrationNo.Text = row["RegistrationNumber"].ToString();
            PAN.Text = row["PermanentAccountNo"].ToString();
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
            LocationId.Value = row["LocationCode"].ToString();
            CompanyId.Value = row["CompanyCode"].ToString();
            ContactId.Value = row["ContactCode"].ToString();
            ddlStatus.SelectedValue = row["Status"].ToString();
            if (row["Logo"].ToString() == null || row["Logo"].ToString()=="")
            {
                imgPreview.ImageUrl = "/Images/user/preview.png";
            }
            else
                imgPreview.ImageUrl = HttpUtility.HtmlDecode(row["Logo"].ToString());
        }
        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                string path = "";
                string absolutePath = "";
                
                if (inputUpload.HasFile)
                {
                    string folderPath = "~/Images/Company/" + Session["CompanyCode"].ToString() + "/Location/";
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    if (!System.IO.Directory.Exists(Server.MapPath("~") + "/Images/Company/" + Session["CompanyCode"].ToString() + "/Location/"))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~") + "/Images/Company/" + Session["CompanyCode"].ToString() + "/Location/");
                    }
                    path = Server.MapPath(folderPath) + LocationCode.Text + "_location_" + timestamp + Path.GetExtension(inputUpload.FileName);
                    absolutePath = folderPath + LocationCode.Text + "_location_" + timestamp + Path.GetExtension(inputUpload.FileName); ;
                    imgPreview.ImageUrl = absolutePath;
                    inputUpload.SaveAs(path);
                }
                DateTime formationDate = DateTime.ParseExact(Request.Form[FormationDate.UniqueID], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                bool status = false;
                bool dbstatus;
                if (ddlStatus.SelectedValue == "0")
                    dbstatus = false;
                else
                    dbstatus = true;
                if (LocationId.Value != "0" && LocationId.Value != "")
                {
                    status = XBDataProvider.Location.UpdateLocation(LocationId.Value, Name.Text, PAN.Text, formationDate, TIN.Text, RegistrationNo.Text,  absolutePath, Note.Text, User.Identity.Name, dbstatus);
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
                    dbstatus = true;
                    int retunValue = 0;
                    // Unique code generation for Location 
                    Random rnd = new Random();
                    string addContactCode = string.Concat('L', LocationCode.Text.Trim(), rnd.Next(100000000, 999999999).ToString());
                    addContactCode = addContactCode.Substring(0, 10);
                    retunValue = XBDataProvider.Location.SaveLocation(Session["CompanyCode"].ToString(), LocationCode.Text, Name.Text, PAN.Text, formationDate, TIN.Text, RegistrationNo.Text, addContactCode, path, Note.Text, "", User.Identity.Name,
                                                                    Phone.Text, Mobile.Text, Email.Text, Web.Text, ContactPerson.Text, Designation.Text, Address1.Text, Address2.Text, Request.Form[City.UniqueID], Area.Text, Convert.ToInt32(Zip.Text), Country.Text, State.Text, Fax.Text, dbstatus);
                    if (retunValue == 1)
                    {
                        ClearInputs(Page.Controls);
                        SaveSuccess.Visible = true;
                        UpdateSuccess.Visible = false;
                        failure.Visible = false;
                        alreadyexist.Visible = false;
                        FormationDate.Text = Convert.ToDateTime(DateTime.Now).ToString("MM'/'dd'/'yyyy");
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
                //if (CompanyId.Value != "0" && CompanyId.Value != "")
                //{
                //    status = XBDataProvider.Company.UpdateCompany(CompanyId.Value, Name.Text, PAN.Text, FormationDate.Text, TIN.Text, RegistrationNo.Text, absolutePath, Note.Text, User.Identity.Name);
                //    if (status)
                //    {
                //        lblMsg.InnerText = "Successfully updated";
                //    }
                //    else
                //    {
                //        lblMsg.InnerText = "Oops..Something went wrong.Please try again";
                //    }
                //}
                //else
                //{
                //    int retunValue = 0;
                //    retunValue = XBDataProvider.Company.SaveCompany(Location.Text, Name.Text, PAN.Text, FormationDate.Text, TIN.Text, RegistrationNo.Text, ContactPerson.Text, absolutePath, Note.Text, true, "", User.Identity.Name,
                //                                                     Phone.Text, Mobile.Text, Email.Text, Web.Text, Designation.Text, Address1.Text, Address2.Text, City.SelectedValue, Area.Text, Zip.Text, Country.SelectedValue, State.Text, Fax.Text);
                //    if (retunValue >= 1)
                //    {
                //        ClearInputs(Page.Controls);
                //        lblMsg.InnerText = "Successfully added";
                //    }
                //    else if (retunValue == -1)
                //    {
                //        lblMsg.InnerText = "Location Code already exist";
                //    }
                //    else
                //    {
                //        lblMsg.InnerText = "Oops..Something went wrong.Please try again";
                //    }

                //}


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
        public static List<cityLocation> GetCities(string countryCode)
        {
            List<cityLocation> result = new List<cityLocation>();
            try
            {
                DataTable dtTable = XBDataProvider.City.GetCities(countryCode);
                DataRow row = null;
                for (int index = 0; index < dtTable.Rows.Count; index++)
                {
                    row = dtTable.Rows[index];
                    cityLocation cityLocation = new cityLocation();
                    cityLocation.cityCode = row["CityCode"].ToString();
                    cityLocation.cityName = row["Name"].ToString();
                    result.Add(cityLocation);
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

    public class cityLocation
    {
        public string cityName { get; set; }
        public string cityCode { get; set; }
    }
}
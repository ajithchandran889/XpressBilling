﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XBDataProvider;



namespace XpressBilling.Account
{
    public partial class LocationEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtCountries = XBDataProvider.Country.GetCountries();

                Country.DataSource = dtCountries;
                Country.DataValueField = "name";
                Country.DataTextField = "name";
                Country.DataBind();
                ListItem item = new ListItem();
                item.Text = "Select Country";
                item.Value = "0";
                Country.Items.Insert(0, item);
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null)
                {
                    DataTable LocationDetails = XBDataProvider.Location.GetLocationById(id);
                    if (LocationDetails.Rows.Count > 0)
                    {
                        SetLocationDetails(LocationDetails);

                    }
                    
                }
                else
                {
                    CompanyId.Value = "0";
                }
            }

        }

        public void SetLocationDetails(DataTable LocationDetails)
        {
            DataRow row = LocationDetails.Rows[0];
            Location.Text = row["LocationCode"].ToString();
            Location.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            string formationDate = Convert.ToDateTime(row["FormationDate"]).ToString("MM/dd/yyyy");
            FormationDate.Text = formationDate;
            TIN.Text = row["TaxId"].ToString();

            txtregno.Text = row["RegistrationNumber"].ToString();
            PAN.Text = row["PermanantAccountNo"].ToString();
            Phone.Text = row["Phone"].ToString();
            Mobile.Text = row["Mobile"].ToString();
            Fax.Text = row["Fax"].ToString();
            Email.Text = row["Email"].ToString();
            Web.Text = row["Web"].ToString();
            ContactPerson.Text = row["contactpersonname"].ToString();
            Designation.Text = row["Designation"].ToString();
            Address1.Text = row["Address1"].ToString();
            Address2.Text = row["Address2"].ToString();
            City.Text = row["cityname"].ToString();
            Area.Text = row["Area"].ToString();
            State.Text = row["State"].ToString();
            Country.SelectedValue = row["countryname"].ToString();
            Zip.Text = row["ZipPostalCode"].ToString();
            Note.Text = row["Note"].ToString();
            LocationId.Value = row["LocationCode"].ToString();
            CompanyId.Value = row["CompanyCode"].ToString();
             ContactId.Value = row["Contact"].ToString();
        }
        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                string path = "";
                if (logoUpload.HasFile)
                {
                    string filename = Path.GetFileName(logoUpload.FileName);
                    path = Server.MapPath("~/Images/logo/") + filename;
                    logoUpload.SaveAs(path);
                }   
                if (LocationId.Value != "0" && LocationId.Value != "")
                {
                    XBDataProvider.Location.UpdateLocation(LocationId.Value, Name.Text, PAN.Text, FormationDate.Text, TIN.Text, txtregno.Text, Phone.Text, path, Note.Text, true, "", User.Identity.Name,
                                                                   Phone.Text, Mobile.Text, Email.Text, Web.Text, ContactPerson.Text, Designation.Text, Address1.Text, Address2.Text, City.Text, Area.Text, Zip.Text, Country.Text, State.Text, Fax.Text);
                }
                else
                {
                    XBDataProvider.Location.SaveLocation(Location.Text, Name.Text, PAN.Text, FormationDate.Text, TIN.Text, txtregno.Text, Phone.Text, path, Note.Text, true, "", User.Identity.Name,
                                                                    Phone.Text, Mobile.Text, Email.Text, Web.Text, ContactPerson.Text, Designation.Text, Address1.Text, Address2.Text, City.Text, Area.Text, Zip.Text, Country.Text, State.Text, Fax.Text);
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
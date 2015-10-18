﻿

using System;
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
    public partial class CompanyEdit : System.Web.UI.Page
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
                    DataTable companyDetails = XBDataProvider.Company.GetCompanyById(id);
                    if (companyDetails.Rows.Count > 0)
                    {
                        SetCompanyDetails(companyDetails);

                    }
                }
                else
                {
                    CompanyId.Value = "0";
                }
            }

        }

        public void SetCompanyDetails(DataTable companyDetails)
        {
            DataRow row = companyDetails.Rows[0];
            Company.Text = row["CompanyCode"].ToString();
            Company.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            string formationDate = Convert.ToDateTime(row["FormationDate"]).ToString("MM/dd/yyyy");
            FormationDate.Text = formationDate;
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
            City.Text = row["City"].ToString();
            City.ReadOnly = true;
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

        }
        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                string path = "";
                string absolutePath = "";
                if (logoUpload.HasFile)
                {
                    string filename = Path.GetFileName(logoUpload.FileName);
                    path = Server.MapPath("~/Images/user/") + filename;
                    absolutePath = "/Images/logo/" + filename;
                    logoUpload.SaveAs(path);
                }
                bool status = false;
                if (CompanyId.Value != "0" && CompanyId.Value != "")
                {
                    status = XBDataProvider.Company.UpdateCompany(CompanyId.Value, Name.Text, PAN.Text, FormationDate.Text, TIN.Text, RegistrationNo.Text, absolutePath, Note.Text, User.Identity.Name);
                    if (status)
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
                    status = XBDataProvider.Company.SaveCompany(Company.Text, Name.Text, PAN.Text, FormationDate.Text, TIN.Text, RegistrationNo.Text, ContactPerson.Text, absolutePath, Note.Text, true, "", User.Identity.Name,
                                                                     Phone.Text, Mobile.Text, Email.Text, Web.Text, Designation.Text, Address1.Text, Address2.Text, City.Text, Area.Text, Zip.Text, Country.Text, State.Text, Fax.Text);
                    if (status)
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
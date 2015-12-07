using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class BussinessPartnerEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
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
                if (id != null && id!=0)
                { 
                    DataTable BPDetails = XBDataProvider.BussinessPartner.GetBPById(id);
                    if (BPDetails.Rows.Count > 0)
                    {
                        DataTable dtTable = XBDataProvider.City.GetCitiesByCompany(Session["CompanyCode"].ToString());
                        City.DataSource = dtTable;
                        City.DataValueField = "CityCode";
                        City.DataTextField = "Name";
                        City.DataBind();
                        SetBPDetails(BPDetails);

                    }
                }
                else
                {
                    ddlStatus.SelectedValue = "1";
                    ddlStatus.Enabled = false;
                    BPId.Value = "0";
                }
            }

        }

        public void SetBPDetails(DataTable BPDetails)
        {
            
            DataRow row = BPDetails.Rows[0];
            BussinessPartner.Text = row["BusinessPartnerCode"].ToString();
            BussinessPartner.ReadOnly = true;
            BusinessPartnerType.SelectedValue = row["BusinessPartnerType"].ToString();
            BusinessPartnerType.Enabled = false;
            if (row["BusinessPartnerType"].ToString()=="0")
            {
                OrderType_0.SelectedValue = row["OrderType"].ToString();
                OrderType_1.Attributes.Add("class", "hideElement");
            }
            else if (row["BusinessPartnerType"].ToString() == "1")
            {
                OrderType_1.SelectedValue = row["OrderType"].ToString();
                OrderType_0.Attributes.Add("class", "hideElement");
            }
            Discount.Text = row["Discount"].ToString();
            CreditLimit.Text = row["CreditLimit"].ToString();
            Tin.Text = row["TaxId1"].ToString();
            Cst.Text = row["TaxId2"].ToString();
            Name.Text = row["Name"].ToString();
            Name.ReadOnly = true;
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
            BPId.Value = row["ID"].ToString();
            ddlStatus.SelectedValue = row["Status"].ToString();

        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                int orderType = 0;
                if (BusinessPartnerType.SelectedValue == "0")
                {
                    orderType = Convert.ToInt32(OrderType_0.SelectedValue);
                }
                else
                {
                    orderType = Convert.ToInt32(OrderType_1.SelectedValue);
                }
                bool status = false;
                if (BPId.Value != "0" && BPId.Value != "")
                {
                    status = XBDataProvider.BussinessPartner.UpdateBP(BPId.Value, Convert.ToInt32(Discount.Text), Convert.ToInt32(CreditLimit.Text), Tin.Text, Cst.Text, Note.Text, User.Identity.Name, Convert.ToInt32(ddlStatus.SelectedValue), orderType);
                    if (status)
                    {
                        //Message.Text 
                        lblMsg.InnerText = "Successfully updated";
                    }
                    else
                    {
                        //Message.Text 
                        lblMsg.InnerText = "Oops..Something went wrong.Please try again";
                    }
                }
                else
                {
                    
                    int retunValue = 0;
                    retunValue = XBDataProvider.BussinessPartner.SaveBP(Session["CompanyCode"].ToString(), BussinessPartner.Text, Name.Text,Convert.ToInt32(BusinessPartnerType.SelectedValue), orderType,Convert.ToInt32(Discount.Text), Convert.ToInt32(CreditLimit.Text), ContactPerson.Text, Tin.Text, Cst.Text, Note.Text,  User.Identity.Name,
                                                                     Phone.Text, Mobile.Text, Email.Text, Web.Text, Designation.Text, Address1.Text, Address2.Text,Request.Form[City.UniqueID], Area.Text, Zip.Text, Country.SelectedValue, State.Text, Fax.Text);
                    if (retunValue >= 1)
                    {
                        ClearInputs(Page.Controls);
                        Message.Text = "Successfully added";
                    }
                    //else if (retunValue == -1)
                    //{
                    //    Message.Text = "Bussiness Partner already exist";
                    //}
                    else
                    {
                        //Message.Text 
                        lblMsg.InnerText = "Oops..Something went wrong.Please try again";
                    }

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace XpressBilling.Account
{
    public partial class EditManufacturer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                DataTable dtBusinessPartner = XBDataProvider.Manufacturer.GetAllActiveBusinessPartner();

                ddlBusinessPartner.DataSource = dtBusinessPartner;
                ddlBusinessPartner.DataValueField = "ID";
                ddlBusinessPartner.DataTextField = "Name";
                ddlBusinessPartner.DataBind();
                ListItem item = new ListItem();
                item.Text = "Select BusinessPartner";
                item.Value = "0";
                ddlBusinessPartner.Items.Insert(0, item);
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    DataTable ManufacturerDetails = XBDataProvider.Manufacturer.GetManufacturerById(id);
                    if (ManufacturerDetails.Rows.Count > 0)
                    {
                        SetManufacturerDetails(ManufacturerDetails);
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
                    ManufacturerId.Value = "0";
                }
            }
        }

        public void SetManufacturerDetails(DataTable ManufacturerDetails)
        {
            DataRow row = ManufacturerDetails.Rows[0];
            Manufacturer.Text = row["ManufacturerCode"].ToString();
            Manufacturer.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            ddlBusinessPartner.SelectedValue = row["BusinessPartnerCode"].ToString();
            UserName.Text = row["CreatedBy"].ToString();
            UserName.ReadOnly = true;
            Date.Text = Convert.ToDateTime(row["CreatedDate"]).ToString("MM'/'dd'/'yyyy"); //row["CreatedDate"].ToString();
            Date.ReadOnly = true;
            ddlStatus.SelectedValue = row["Status"].ToString();
            ManufacturerId.Value = row["ID"].ToString();

        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                int msgstatus = 0;
                hdncompanycode.Value = Session["CompanyCode"].ToString();
                if (ManufacturerId.Value != "0" && ManufacturerId.Value != null)
                {
                    bool status;
                    if (ddlStatus.SelectedValue == "0")
                        status = false;
                    else
                        status = true;
                    msgstatus = XBDataProvider.Manufacturer.UpdateManufacturer(Convert.ToInt32(ManufacturerId.Value), Name.Text, ddlBusinessPartner.SelectedValue, User.Identity.Name, status);
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
                    msgstatus = XBDataProvider.Manufacturer.SaveManufacturer(hdncompanycode.Value, Manufacturer.Text, Name.Text, ddlBusinessPartner.SelectedValue, reference, User.Identity.Name, true);
                    if (msgstatus == 1)
                    {
                        ClearInputs(Page.Controls);
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
                    //if (msgstatus == 1)
                    //{
                    //    lblMsg.InnerText = "Successfully added";
                    //}
                    //else
                    //{
                    //    lblMsg.InnerText = "Oops..Something went wrong.Please try again";
                    //}
                    //ClearInputs(Page.Controls);
                }

            }
            catch (Exception ex)
            {
                SaveSuccess.Visible = false;
                UpdateSuccess.Visible = false;
                failure.Visible = true;
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
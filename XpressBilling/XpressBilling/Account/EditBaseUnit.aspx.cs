using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XpressBilling.Account
{
    public partial class EditBaseUnit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    DataTable BaseUnitDetails = XBDataProvider.BaseUnit.GetBaseUnitById(id);
                    if (BaseUnitDetails.Rows.Count > 0)
                    {
                        SetBaseUnitDetails(BaseUnitDetails);
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
                    TaxId.Value = "0";
                }
            }
        }

        public void SetBaseUnitDetails(DataTable BaseUnitDetails)
        {
            DataRow row = BaseUnitDetails.Rows[0];
            BaseUnit.Text = row["BaseUnitCode"].ToString();
            BaseUnit.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            UserName.Text = row["Reference"].ToString();
            UserName.ReadOnly = true;
            Date.Text = row["CreatedDate"].ToString();
            Date.ReadOnly = true;
            ddlStatus.SelectedValue = row["Status"].ToString();
            TaxId.Value = row["ID"].ToString();

        }

        protected void SaveClick(object sender, EventArgs e)
        {
            int msgstatus = 0;
            try
            {
                hdncompanycode.Value = Session["CompanyCode"].ToString();
                if (TaxId.Value != "0" && TaxId.Value != null)
                {
                    bool status;
                    if (ddlStatus.SelectedValue == "0")
                        status = false;
                    else
                        status = true;
                    msgstatus = XBDataProvider.BaseUnit.UpdateBaseUnit(Convert.ToInt32(TaxId.Value), Name.Text, User.Identity.Name, status);
                    if (msgstatus == 1)
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
                    msgstatus = XBDataProvider.BaseUnit.SaveBaseUnit(hdncompanycode.Value, BaseUnit.Text, Name.Text, User.Identity.Name, User.Identity.Name, DateTime.Today, true);
                    if (msgstatus == 1)
                    {
                        lblMsg.InnerText = "Successfully added";
                    }
                    else
                    {
                        lblMsg.InnerText = "Oops..Something went wrong.Please try again";
                    }
                    ClearInputs(Page.Controls);
                }               
            }
            catch (Exception ex)
            {

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
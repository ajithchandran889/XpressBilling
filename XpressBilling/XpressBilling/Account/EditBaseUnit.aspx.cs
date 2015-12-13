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
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
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
                    CreatedDate.Visible = false;
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
            CreatedDate.Text = Convert.ToDateTime(row["CreatedDate"]).ToString("MM'/'dd'/'yyyy");
            CreatedDate.ReadOnly = true;
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
                    if (msgstatus != -1)
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
                    msgstatus = XBDataProvider.BaseUnit.SaveBaseUnit(hdncompanycode.Value, BaseUnit.Text, Name.Text, User.Identity.Name, User.Identity.Name, DateTime.Today, true);
                    
                    ClearInputs(Page.Controls);
                    if (msgstatus == 1)
                    {
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
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
    public partial class EditEmployeeMaster : System.Web.UI.Page
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
                    DataTable EmployeeDetails = XBDataProvider.Employee.GetEmployeeById(id);
                    if (EmployeeDetails.Rows.Count > 0)
                    {
                        SetEmployeeDetails(EmployeeDetails);
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
                    EmployeeId.Value = "0";
                }
            }
        }

        public void SetEmployeeDetails(DataTable EmployeeDetails)
        {
            DataRow row = EmployeeDetails.Rows[0];
            Employee.Text = row["EmployeeCode"].ToString();
            Employee.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            DOJ.Text = Convert.ToDateTime(row["DateofJoining"]).ToString("MM'/'dd'/'yyyy");
            UserName.Text = row["CreatedBy"].ToString();
            UserName.ReadOnly = true;
            Date.Text = Convert.ToDateTime(row["CreatedDate"]).ToString("MM'/'dd'/'yyyy");
            //row["CreatedDate"].ToString(); 
            Date.ReadOnly = true;
            ddlStatus.SelectedValue = row["Status"].ToString();
            EmployeeId.Value = row["ID"].ToString();

        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                int msgstatus = 0;
                hdncompanycode.Value = Session["CompanyCode"].ToString();
                if (EmployeeId.Value != "0" && EmployeeId.Value != null)
                {
                    bool status;
                    if (ddlStatus.SelectedValue == "0")
                        status = false;
                    else
                        status = true;
                    msgstatus = XBDataProvider.Employee.UpdateEmployee(Convert.ToInt32(EmployeeId.Value), Name.Text, User.Identity.Name, status);
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
                    msgstatus = XBDataProvider.Employee.SaveEmployee(hdncompanycode.Value, Employee.Text, Name.Text, DOJ.Text, reference, User.Identity.Name, true);
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
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
            DataTable dtCountries = XBDataProvider.Country.GetCountries();
            Country.DataSource = dtCountries;
            Country.DataValueField = "name";
            Country.DataTextField = "name";
            Country.DataBind();
            ListItem item=new ListItem();
            item.Text = "Select Country";
            item.Value = "0";
            Country.Items.Insert(0, item);
        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                string filename = Path.GetFileName(logoUpload.FileName);
                string path = Server.MapPath("~/Images/logo/") + filename;
                logoUpload.SaveAs(path);
                int companyId = XBDataProvider.Company.SaveCompany(Company.Text, Name.Text, PAN.Text, FormationDate.Text, TIN.Text, TIN.Text, Phone.Text, path, Note.Text, true, "", User.Identity.Name,
                                                                    Phone.Text, Mobile.Text, Email.Text, Web.Text, ContactPerson.Text, Designation.Text, Address1.Text, Address2.Text, City.Text, Area.Text, Zip.Text,Country.Text);
                ClearInputs(Page.Controls);
            }
            catch(Exception ex)
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
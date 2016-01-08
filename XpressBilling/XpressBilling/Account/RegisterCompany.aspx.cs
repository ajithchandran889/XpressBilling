using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class RegisterCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            FormationDate.Text = Convert.ToDateTime(DateTime.Now).ToString("MM'/'dd'/'yyyy");
        }

        protected void SaveCompanyClick(object sender, EventArgs e)
        {
           FormationDate.Text= Convert.ToDateTime(DateTime.Now).ToString("MM'/'dd'/'yyyy");
            bool status = false;
            // Unique code generation for Company 
            Random rnd = new Random();
            string AddContactCode = string.Concat('C', RgstCompany.Text.Trim(), rnd.Next(100000000, 999999999).ToString());
            AddContactCode = AddContactCode.Substring(0, 10);
            status = XBDataProvider.Company.SaveCompanyInitail(RgstCompany.Text, Name.Text, PAN.Text, FormationDate.Text, TIN.Text, RegistrationNo.Text, Note.Text, true, AddContactCode);
            if(status)
            {
                Response.Redirect("Register");
            }
        }

       
    }
}
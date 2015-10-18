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
            

        }

        protected void SaveCompanyClick(object sender, EventArgs e)
        {
            bool status = false;
            status = XBDataProvider.Company.SaveCompanyInitail(Company.Text, Name.Text, PAN.Text, FormationDate.Text, TIN.Text, RegistrationNo.Text, Note.Text, true);
            if(status)
            {
                Response.Redirect("Register");
            }
        }

       
    }
}
using System;
using System.Data;
using System.IO;

namespace XpressBilling.Account
{
    public partial class PrintSalesInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            crViewer.DisplayGroupTree = false;
            crViewer.HasPrintButton = true;
            crViewer.PrintMode = CrystalDecisions.Web.PrintMode.Pdf;
            //crViewer.HasRefreshButton = true;
            crViewer.HasCrystalLogo = false;
            crViewer.HasToggleGroupTreeButton = false;

            string locationCode = Request.QueryString["lc"] != null ? Request.QueryString["lc"].ToString() : "";
            string businessPartnerCode = Request.QueryString["bpc"] != null ? Request.QueryString["bpc"].ToString() : "";
            string salesOrderNo = Request.QueryString["son"] != null ? Request.QueryString["son"].ToString() : "";
            string companyCode = Request.QueryString["cc"] != null ? Request.QueryString["cc"].ToString() : "";

            BindReport(locationCode, businessPartnerCode, salesOrderNo, companyCode);
        }

        private void BindReport(string locationCode, string businessPartnerCode, string salesOrderNo, string companyCode)
        {
            //DataSet objDataSet = XBDataProvider.SalesInvoicePrint.GetSalesInvoicePrintData("Bng", "L101", "SOnum", "Techen");
            DataSet objDataSet = XBDataProvider.SalesInvoicePrint.GetSalesInvoicePrintData(locationCode, businessPartnerCode, salesOrderNo, companyCode);

            #region Comment this after adding LOGO table from DB
            DataTable dtLogo = new DataTable("Logo");
            dtLogo.Columns.Add(new DataColumn("Image"));
            dtLogo.Columns.Add(new DataColumn("Path"));
            DataRow dr = dtLogo.NewRow();
            dr["Path"] = @"E:\XpressBilling\XpressBilling2\XpressBilling\XpressBilling\Images\iser_logo.jpg";
            //try
            //{
            //    FileStream fs = new FileStream(@"E:\XpressBilling\XpressBilling2\XpressBilling\XpressBilling\Images\cnclogo.png",
            //               System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //    byte[] Image = new byte[fs.Length];
            //    fs.Read(Image, 0, Convert.ToInt32(fs.Length));
            //    fs.Close();
            //    dr["Image"] = Image;
            //}
            //catch (Exception ex)
            //{
            //    //Response.Write("<font color=red>" + ex.Message + "</font>");
            //}
            dtLogo.Rows.Add(dr);
            objDataSet.Tables.Add(dtLogo); 
            #endregion

            if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0].Rows.Count > 0)
            {
                crViewer.Visible = true;
                spanNoRecords.Visible = false;
                this.crSource.Report.FileName = "PrintSalesInvoice_CR.rpt";
                this.crSource.ReportDocument.SetDataSource(objDataSet);
                this.crSource.ReportDocument.Refresh();
            }
            else
            {
                spanNoRecords.Visible = true;
                spanNoRecords.InnerText = "No records found.";
                crViewer.Visible = false; ;
            }
        }
    }
}
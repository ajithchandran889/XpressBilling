using System;
using System.Data;

namespace XpressBilling.Account
{
    public partial class PrintSalesQuote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            crViewer.DisplayGroupTree = false;
            crViewer.HasPrintButton = true;
            //crViewer.PrintMode = CrystalDecisions.Web.PrintMode.Pdf;
            crViewer.HasRefreshButton = false;
            crViewer.HasCrystalLogo = false;
            crViewer.HasToggleGroupTreeButton = false;
            BindReport();
        }

        private void BindReport()
        {
            DataSet objDataSet = XBDataProvider.SalesQuotationPrint.GetSalesQuotationPrintData("Bng", "L101", "C0001", "Techen"); ;

            if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0].Rows.Count > 0)
            {
                crViewer.Visible = true;
                spanNoRecords.Visible = false;
                this.crSource.Report.FileName = "PrintSalesQuote_CR.rpt";
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
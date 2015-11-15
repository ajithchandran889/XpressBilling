using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class PriceBookEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string lastDocNumber=XBDataProvider.PriceBook.GetDocumentNumber();
                if(lastDocNumber==null || lastDocNumber =="")
                {
                    lastDocNumber="0";
                }
                else
                {
                    lastDocNumber=lastDocNumber.Replace('D',' ');
                }
                LastDocumentNumber.Value = lastDocNumber;
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    DataTable priceBookDetails = XBDataProvider.PriceBook.GetPriceBookById(id);
                    if (priceBookDetails.Rows.Count > 0)
                    {
                        SetPriceBookDetails(priceBookDetails);

                    }
                }
                else
                {
                    PriceBookId.Value = "0";
                    Currency.Text = XBDataProvider.Currency.GetCurrencyCodeByCompany(Session["CompanyCode"].ToString()); ;
                    CreatedUser.Visible = false;
                    Date.Visible = false;
                    lblDate.Visible = false;
                    lblUser.Visible = false;
                    PriceBookDetail.Visible = false;
                    SaveDetail.Visible = false;
                    CancelPriceBook2.Visible = false;
                }
                
                SetInitialRow();
            }
        }

        private void AddNewRowToGrid()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox box1 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[0].FindControl("Item");
                        TextBox box2 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[1].FindControl("SupplierBarcode");
                        //TextBox box3 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[2].FindControl("Description");
                        TextBox box4 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[2].FindControl("CurrencyCode");
                        TextBox box5 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[3].FindControl("MRP");
                        TextBox box6 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[4].FindControl("Price");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["Item"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["SupplierBarcode"] = box2.Text;
                        //dtCurrentTable.Rows[i - 1]["Description"] = box3.Text;
                        dtCurrentTable.Rows[i - 1]["CurrencyCode"] = box4.Text;
                        dtCurrentTable.Rows[i - 1]["MRP"] = box5.Text;
                        dtCurrentTable.Rows[i - 1]["Price"] = box6.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    PriceBookDetail.DataSource = dtCurrentTable;
                    PriceBookDetail.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox box1 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[0].FindControl("Item");
                        TextBox box2 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[1].FindControl("SupplierBarcode");
                        //TextBox box3 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[2].FindControl("Description");
                        TextBox box4 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[2].FindControl("CurrencyCode");
                        TextBox box5 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[3].FindControl("MRP");
                        TextBox box6 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[4].FindControl("Price");

                        box1.Text = dt.Rows[i]["Item"].ToString();
                        box2.Text = dt.Rows[i]["SupplierBarcode"].ToString();
                        //box3.Text = dt.Rows[i]["Description"].ToString();
                        box4.Text = dt.Rows[i]["CurrencyCode"].ToString();
                        box5.Text = dt.Rows[i]["MRP"].ToString();
                        box6.Text = dt.Rows[i]["Price"].ToString();

                        rowIndex++;
                    }
                }
            }
        }

        protected void ButtonAddClick(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        public void SetPriceBookDetails(DataTable priceBookDetails)
        {
            DataRow row = priceBookDetails.Rows[0];
            PriceBookId.Value = row["ID"].ToString();
            Type.SelectedValue = row["PriceBookType"].ToString();
            if(row["PriceBookType"].ToString()=="0")
            {
                OrderType_0.SelectedValue = row["OrderType"].ToString();
                OrderType_1.Visible = false;
                OrderType_0.Enabled = false;
            }
            else if (row["PriceBookType"].ToString() == "1")
            {
                OrderType_0.Visible = false;
                OrderType_1.SelectedValue = row["OrderType"].ToString();
                OrderType_1.Enabled = false;
            }
            Currency.Text = row["CurrencyCode"].ToString();
            PriceBookDocNo.Text = row["DocumentNo"].ToString();
            Date.Text = row["DocumentDate"].ToString();
            CreatedUser.Text = row["CreatedBy"].ToString();
            savePriceBook.Visible = false;
            cancelPriceBook.Visible = false;
            PriceBookDetail.Visible = true;
            SaveDetail.Visible = true;
            CancelPriceBook2.Visible = true;
            Type.Enabled = false;
            
        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("Item", typeof(string)));
            dt.Columns.Add(new DataColumn("SupplierBarcode", typeof(string)));
            //dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("CurrencyCode", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));
            dt.Columns.Add(new DataColumn("Price", typeof(string)));
            dr = dt.NewRow();
            dr["Item"] = string.Empty; ;
            dr["SupplierBarcode"] = string.Empty;
            //dr["Description"] = string.Empty;
            dr["CurrencyCode"] = string.Empty;
            dr["MRP"] = string.Empty;
            dr["Price"] = string.Empty;
            dt.Rows.Add(dr);
            //dr = dt.NewRow();

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            PriceBookDetail.DataSource = dt;
            PriceBookDetail.DataBind();
        }
        protected void savePriceBookClick(object sender, EventArgs e)
        {
            string newDocumentNumber="D"+(Convert.ToInt32(LastDocumentNumber.Value)+1);
            int orderType=0;
            if(Type.SelectedValue=="0")
            {
                orderType=Convert.ToInt32(OrderType_0.SelectedValue);
            }
            else
            {
                orderType=Convert.ToInt32(OrderType_1.SelectedValue);
            }
            int id=XBDataProvider.PriceBook.SavePriceBookMaster(Session["CompanyCode"].ToString(),newDocumentNumber,Convert.ToInt32(Type.SelectedValue),orderType,Currency.Text,User.Identity.Name);
            if(id>0)
            {
                savePriceBook.Visible = false;
                cancelPriceBook.Visible = false;
                PriceBookDetail.Visible = true;
                SaveDetail.Visible = true;
                CancelPriceBook2.Visible = true;
                Type.Enabled = false;
                OrderType_0.Enabled = false;
                OrderType_1.Enabled = false;
                PriceBookDocNo.Text = newDocumentNumber;
                PriceBookId.Value = id.ToString();
            }
            
        }
        protected void savePriceBookClickDetails(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("CompanyCode", typeof(string)));
            dt.Columns.Add(new DataColumn("PriceBookMstID", typeof(int)));
            dt.Columns.Add(new DataColumn("DocumentNo", typeof(string)));
            dt.Columns.Add(new DataColumn("DocumentDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("PriceType", typeof(int)));
            dt.Columns.Add(new DataColumn("OrderType", typeof(int)));
            dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
            dt.Columns.Add(new DataColumn("SupplierBarcode", typeof(string)));
            dt.Columns.Add(new DataColumn("CurrencyCode", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(int)));
            dt.Columns.Add(new DataColumn("Price", typeof(int)));
            dt.Columns.Add(new DataColumn("Reference", typeof(string)));
            dt.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
            dt.Columns.Add(new DataColumn("UpdatedBy", typeof(string)));
            dt.Columns.Add(new DataColumn("CreatedDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("UpdatedDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("ApprovalStatus", typeof(int)));
            dt.Columns.Add(new DataColumn("Status", typeof(int)));
            int i = 0;
            foreach (GridViewRow row in PriceBookDetail.Rows)
            {
                dr = dt.NewRow();
                dr["CompanyCode"] = Session["CompanyCode"].ToString();
                dr["PriceBookMstID"] =Convert.ToInt32(PriceBookId.Value);
                dr["DocumentNo"] = PriceBookDocNo.Text;
                dr["DocumentDate"] =DateTime.Now.Date;
                dr["PriceType"] = Type.SelectedValue;
                if(Type.SelectedValue=="0")
                {
                    dr["OrderType"] = Convert.ToInt32(OrderType_0.SelectedValue);
                }
                else
                {
                    dr["OrderType"] = Convert.ToInt32(OrderType_1.SelectedValue);
                }
                dr["ItemCode"] = (TextBox)PriceBookDetail.Rows[i].Cells[0].FindControl("Item");
                dr["SupplierBarcode"] = (TextBox)PriceBookDetail.Rows[i].Cells[1].FindControl("SupplierBarcode");
                dr["CurrencyCode"] = (TextBox)PriceBookDetail.Rows[i].Cells[2].FindControl("CurrencyCode");
                dr["MRP"] = (TextBox)PriceBookDetail.Rows[i].Cells[3].FindControl("MRP");
                dr["Price"] = (TextBox)PriceBookDetail.Rows[i].Cells[4].FindControl("Price");
                dr["Reference"] = null;
                dr["CreatedBy"] = User.Identity.Name;
                dr["UpdatedBy"] = User.Identity.Name;
                dr["CreatedDate"] =DateTime.Now.Date;
                dr["UpdatedDate"] = DateTime.Now.Date;
                dr["ApprovalStatus"] = 0;
                dr["Status"] = 1;
                i++;
            }
        }
    }
}
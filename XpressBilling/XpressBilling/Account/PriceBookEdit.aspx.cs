using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
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
                    CreatedDate.Visible = false;
                    lblDate.Visible = false;
                    lblUser.Visible = false;
                   // PriceBookDetail.Visible = false;
                    SaveDetail.Visible = false;
                    CancelPriceBook2.Visible = false;
                    lblSearch.Visible = false;
                    txtSearch.Visible = false;
                    btnSearch.Visible = false;
                    PriceBookDetail.Visible = false;
                    //SetInitialRow();
                }
                
                
            }
        }


        public void SetPriceBookDetails(DataTable priceBookDetails)
        {
            try
            {
                DataRow row = priceBookDetails.Rows[0];
                PriceBookId.Value = row["ID"].ToString();
                Type.SelectedValue = row["PriceType"].ToString();
                if (row["PriceType"].ToString() == "0")
                {
                    OrderType_1.Visible = false;
                    OrderType_0.Visible = true;
                    OrderType_0.SelectedValue = row["OrderType"].ToString();
                    
                    OrderType_0.Enabled = false;
                }
                else if (row["PriceType"].ToString() == "1")
                {
                    OrderType_0.Visible = false;
                    OrderType_1.Visible = true;
                    OrderType_1.SelectedValue = row["OrderType"].ToString();
                    OrderType_1.Style.Add("display", "block");
                    OrderType_1.Enabled = false;
                }
                Currency.Text = row["CurrencyCode"].ToString();
                PriceBookDocNo.Text = row["DocumentNo"].ToString();
                CreatedDate.Text = Convert.ToDateTime(row["DocumentDate"]).ToString("MM'/'dd'/'yyyy");
                CreatedUser.Text = row["CreatedBy"].ToString();
                savePriceBook.Visible = false;
                cancelPriceBook.Visible = false;
                //PriceBookDetail.Visible = true;
                SaveDetail.Visible = true;
                CancelPriceBook2.Visible = true;
                Type.Enabled = false;
                SetPriceBookDetailsChildGrid();
            }
            catch (Exception e)
            {

            }


        }

        public void SetPriceBookDetailsChildGrid()
        {
            DataTable dt=new DataTable();
            if(txtSearch.Text=="")
            {
                dt = XBDataProvider.PriceBook.GetPriceBookDtlById(Convert.ToInt32(PriceBookId.Value));
            }
            else
            {
                dt = XBDataProvider.PriceBook.GetPriceBookDtlByIdAndSearchKey(Convert.ToInt32(PriceBookId.Value),txtSearch.Text);
                
            }
            if(dt.Rows.Count>0)
            {
                PriceBookDetail.DataSource = dt;
                PriceBookDetail.DataBind();
                return;
            }
            //SetInitialRow();
            
        }

        private void AddNewRowToGrid()
        {
            try
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
                            TextBox box1 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[0].FindControl("ItemCode");
                            TextBox box2 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[1].FindControl("SupplierBarcode");
                            //TextBox box3 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[2].FindControl("Description");
                            TextBox box4 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[2].FindControl("CurrencyCode");
                            TextBox box5 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[3].FindControl("MRP");
                            TextBox box6 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[4].FindControl("Price");

                            drCurrentRow = dtCurrentTable.NewRow();
                            dtCurrentTable.Rows[i - 1]["ID"] = PriceBookDetail.DataKeys[rowIndex]["ID"]; ;
                            dtCurrentTable.Rows[i - 1]["ItemCode"] = box1.Text;
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
            catch(Exception e)
            {

            }
            
        }

        [WebMethod]
        public static List<ItemMasteDetails> GetItemMasters(string companyCode)
        {
            List<ItemMasteDetails> result = new List<ItemMasteDetails>();
            try
            {
                DataTable dtTable = XBDataProvider.ItemMaster.GetItemMasters(companyCode);
                DataRow row = null;
                for (int index = 0; index < dtTable.Rows.Count; index++)
                {
                    row = dtTable.Rows[index];
                    ItemMasteDetails itemMasteDetails = new ItemMasteDetails();
                    itemMasteDetails.code = row["ItemCode"].ToString();
                    itemMasteDetails.name = row["Name"].ToString();
                    itemMasteDetails.BaseUnitCode = row["BaseUnitCode"].ToString();
                    itemMasteDetails.supplierBarcode = row["SupplierBarcode"].ToString();
                    itemMasteDetails.mrp = Convert.ToInt32(row["MRP"].ToString());
                    itemMasteDetails.retailPrice = Convert.ToInt32(row["RetailPrice"].ToString());
                    itemMasteDetails.TaxCode = row["TaxCode"].ToString();
                    itemMasteDetails.TaxPer = Convert.ToInt32(row["TaxPercentage"].ToString());
                    itemMasteDetails.Qnty=row["Qnty"].ToString()!=""?Convert.ToInt32(row["Qnty"].ToString()):0;
                    result.Add(itemMasteDetails);
                }
            }
            catch (Exception e)
            {

            }


            return result;
        }

        private void SetPreviousData()
        {
            try
            {
                int rowIndex = 0;
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        PriceBookDetail.DataSource = dt;
                        PriceBookDetail.DataBind();
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    TextBox box1 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[0].FindControl("Item");
                        //    TextBox box2 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[1].FindControl("SupplierBarcode");
                        //    //TextBox box3 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[2].FindControl("Description");
                        //    TextBox box4 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[2].FindControl("CurrencyCode");
                        //    TextBox box5 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[3].FindControl("MRP");
                        //    TextBox box6 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[4].FindControl("Price");

                        //    box1.Text = dt.Rows[i]["Item"].ToString();
                        //    box2.Text = dt.Rows[i]["SupplierBarcode"].ToString();
                        //    //box3.Text = dt.Rows[i]["Description"].ToString();
                        //    box4.Text = dt.Rows[i]["CurrencyCode"].ToString();
                        //    box5.Text = dt.Rows[i]["MRP"].ToString();
                        //    box6.Text = dt.Rows[i]["Price"].ToString();

                        //    rowIndex++;
                        //}
                    }
                }
            }
            catch(Exception e)
            {

            }
            
        }

        protected void ButtonAddClick(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        private void SetInitialRow()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
                dt.Columns.Add(new DataColumn("SupplierBarcode", typeof(string)));
                //dt.Columns.Add(new DataColumn("Description", typeof(string)));
                dt.Columns.Add(new DataColumn("CurrencyCode", typeof(string)));
                dt.Columns.Add(new DataColumn("MRP", typeof(string)));
                dt.Columns.Add(new DataColumn("Price", typeof(string)));
                for (int i = 0; i <= 20;i++ )
                {
                    dr = dt.NewRow();
                    dr["ID"] = "0";
                    dr["ItemCode"] = string.Empty;
                    dr["SupplierBarcode"] = string.Empty;
                    //dr["Description"] = string.Empty;
                    dr["CurrencyCode"] = string.Empty;
                    dr["MRP"] = string.Empty;
                    dr["Price"] = string.Empty;
                    dt.Rows.Add(dr);
                }
                
                //dr = dt.NewRow();

                //Store the DataTable in ViewState
                ViewState["CurrentTable"] = dt;

                PriceBookDetail.DataSource = dt;
                PriceBookDetail.DataBind();
            }
            catch(Exception e)
            {

            }
            
        }

        protected void savePriceBookClick(object sender, EventArgs e)
        {
            try
            {
                string newDocumentNumber = "D" + (Convert.ToInt32(LastDocumentNumber.Value) + 1);
                int orderType = 0;
                if (Type.SelectedValue == "0")
                {
                    orderType = Convert.ToInt32(OrderType_0.SelectedValue);
                }
                else
                {
                    orderType = Convert.ToInt32(OrderType_1.SelectedValue);
                }
                int id = XBDataProvider.PriceBook.SavePriceBookMaster(Session["CompanyCode"].ToString(), newDocumentNumber, Convert.ToInt32(Type.SelectedValue), orderType, Currency.Text, User.Identity.Name);
                if (id > 0)
                {
                    savePriceBook.Visible = false;
                    cancelPriceBook.Visible = false;
                    //PriceBookDetail.Visible = true;
                    SaveDetail.Visible = true;
                    CancelPriceBook2.Visible = true;
                    Type.Enabled = false;
                    OrderType_0.Enabled = false;
                    OrderType_1.Enabled = false;
                    PriceBookDocNo.Text = newDocumentNumber;
                    PriceBookId.Value = id.ToString();
                    PriceBookDetail.Visible = true;
                    SetInitialRow();
                }
            }
            catch(Exception ex)
            {

            }
            
            
        }

        protected void savePriceBookClickDetails(object sender, EventArgs e)
        {
            try
            {
               
                    DataTable dt = new DataTable();
                    DataRow dr = null;
                    dt.Columns.Add(new DataColumn("ID", typeof(int)));
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
                        TextBox box1 = (TextBox)PriceBookDetail.Rows[i].Cells[0].FindControl("ItemCode");
                        TextBox box2 = (TextBox)PriceBookDetail.Rows[i].Cells[1].FindControl("SupplierBarcode");
                        //TextBox box3 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[2].FindControl("Description");
                        TextBox box4 = (TextBox)PriceBookDetail.Rows[i].Cells[2].FindControl("CurrencyCode");
                        TextBox box5 = (TextBox)PriceBookDetail.Rows[i].Cells[3].FindControl("MRP");
                        TextBox box6 = (TextBox)PriceBookDetail.Rows[i].Cells[4].FindControl("Price");
                        if (box1.Text != "" && box1.Text.Length!=0)
                        {
                            dr = dt.NewRow();
                            dr["ID"] = PriceBookDetail.DataKeys[i]["ID"];
                            dr["CompanyCode"] = Session["CompanyCode"].ToString();
                            dr["PriceBookMstID"] = Convert.ToInt32(PriceBookId.Value);
                            dr["DocumentNo"] = PriceBookDocNo.Text;
                            dr["DocumentDate"] = DateTime.Now.Date;
                            dr["PriceType"] = Type.SelectedValue;
                            if (Type.SelectedValue == "0")
                            {
                                dr["OrderType"] = Convert.ToInt32(OrderType_0.SelectedValue);
                            }
                            else
                            {
                                dr["OrderType"] = Convert.ToInt32(OrderType_1.SelectedValue);
                            }
                            dr["ItemCode"] = box1.Text;
                            dr["SupplierBarcode"] = box2.Text;
                            dr["CurrencyCode"] = box4.Text;
                            dr["MRP"] = box5.Text;
                            dr["Price"] = box6.Text;
                            dr["Reference"] = null;
                            dr["CreatedBy"] = User.Identity.Name;
                            dr["UpdatedBy"] = User.Identity.Name;
                            dr["CreatedDate"] = DateTime.Now.Date;
                            dr["UpdatedDate"] = DateTime.Now.Date;
                            dr["ApprovalStatus"] = 0;
                            dr["Status"] = 1;
                            dt.Rows.Add(dr);
                            i++;
                        }
                        
                    }
                    if (dt.Rows.Count > 0)
                    {
                        XBDataProvider.PriceBook.SavePriceBookMasterDetail(dt);
                    }
                    SetPriceBookDetailsChildGrid();
                
            }
            catch(Exception ex)
            {

            }
           
        }

        protected void BtnSearchClick(object sender, EventArgs e)
        {
            SetPriceBookDetailsChildGrid();
        }

        protected void PriceBookDetailRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int id = Convert.ToInt32(PriceBookDetail.DataKeys[e.Row.RowIndex]["ID"]);
                if (id > 0)
                {
                    TextBox box1 = (TextBox)PriceBookDetail.Rows[e.Row.RowIndex].Cells[0].FindControl("ItemCode");
                    box1.ReadOnly = true;
                }
            }
        }
    }

    public class ItemMasteDetails
    {
        public string code { get; set; }
        public string name { get; set; }
        public string supplierBarcode { get; set; }
        public int mrp { get; set; }
        public int retailPrice { get; set; }
        public string BaseUnitCode { get; set; }
        public string TaxCode { get; set; }
        public int TaxPer { get; set; }
        public int Qnty { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class StokeEntryEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                CompanyCode.Value = Session["CompanyCode"].ToString();
                DataTable dtLocation = XBDataProvider.Location.GetAllLocations(Session["CompanyCode"].ToString());
                Location.DataSource = dtLocation;
                Location.DataValueField = "LocationCode";
                Location.DataTextField = "Name";
                Location.DataBind();
                DataTable dtUsers = XBDataProvider.User.GetAllUsersByCompany(Session["CompanyCode"].ToString());
                CreatedUser.DataSource = dtUsers;
                CreatedUser.DataValueField = "UserName";
                CreatedUser.DataTextField = "UserName";
                CreatedUser.DataBind();
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    DataTable stockEntryDetails = XBDataProvider.StockEntry.GetStockEntryById(id);
                    if (stockEntryDetails.Rows.Count > 0)
                    {
                        SetStockEntryDetails(stockEntryDetails);
                        PageStatus.Value = "edit";
                    }
                }
                else
                {
                    #region Document No Generation
                    DataRow row = null;
                    DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetStockEntryCashCreditSequenceDetails(Session["CompanyCode"].ToString());
                    for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                    {
                        row = dtTableSequenceDetails.Rows[i];
                        string sequenceNo =XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(),Convert.ToInt32(row["lastSequenceNo"]),Convert.ToInt32(row["Digits"]));
                        if (row["OrderType"].ToString() == "Addition")
                        {
                            Document.Text = sequenceNo;
                            AdditionSequenceNo.Value = sequenceNo;
                            AdditionSequenceNoID.Value = row["ID"].ToString();
                        }
                        else if (row["OrderType"].ToString() == "Deduction")
                        {
                            DeductionSequenceNo.Value = sequenceNo;
                            DeductionSequenceNoID.Value = row["ID"].ToString();
                        }
                        else if (row["OrderType"].ToString() == "Opening")
                        {
                            OpeningSequenceNo.Value = sequenceNo;
                            OpeningSequenceNoID.Value = row["ID"].ToString();
                        }
                        Document.ReadOnly = true;

                    }
                    #endregion
                    #region default values
                    PageStatus.Value = "create";
                    Date.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");
                    DataTable dtTable = XBDataProvider.Company.GetCompanyDetailsByCode(Session["CompanyCode"].ToString());
                    DataRow rowCompany = dtTable.Rows[0];
                    Currency.Text = rowCompany["CurrencyCode"].ToString();
                    Location.SelectedValue = rowCompany["LocationCode"].ToString();
                    CreatedUser.SelectedValue = User.Identity.Name;
                    gridDetails.Visible = false;
                    #endregion
                }
            }
        }

        public void SetStockEntryDetails(DataTable stockEntryDetails)
        {
            try
            {
                SaveBtn.Visible = false;
                CancelBtn.Visible = false;
                btnSaveDtl.Visible = true;
                btnConvertStockRegister.Visible = true;
                btnPrint.Visible = true;
                DataRow row = stockEntryDetails.Rows[0];
                StokeEntryMstId.Value = row["ID"].ToString();
                AdjustmentType.SelectedValue = row["TransactionType"].ToString();
                AdjustmentType.Enabled = false;
                Document.Text = row["DocumentNo"].ToString();
                Document.ReadOnly = true;
                Status.SelectedValue = row["Status"].ToString();
                Status.Enabled = false;
                Location.SelectedValue = row["LocationCode"].ToString();
                Location.Enabled = false;
                Reference.Text = row["Reference"].ToString();
                Reference.ReadOnly = true;
                Date.Text = Convert.ToDateTime(row["DocumentDate"]).ToString("MM'/'dd'/'yyyy");
                Date.ReadOnly = true;
                CreatedUser.Text = row["CreatedBy"].ToString();
                CreatedUser.Enabled = false;
                Amount.Text = Convert.ToDecimal(row["Amount"]).ToString("0.00");
                Amount.ReadOnly = true;
                Currency.Text = row["Currency"].ToString();
                Currency.ReadOnly = true;
                
                SetStockEntryChildGrid();
                int i = 0;
                if (row["Status"].ToString() == "2")
                {
                    btnConvertStockRegister.Visible = false;
                    btnSaveDtl.Visible = false;
                    foreach (GridViewRow gvr in StockEntryDetail.Rows)
                    {
                        TextBox box2 = (TextBox)StockEntryDetail.Rows[i].Cells[1].FindControl("Item");
                        TextBox box3 = (TextBox)StockEntryDetail.Rows[i].Cells[2].FindControl("Name");
                        TextBox box4 = (TextBox)StockEntryDetail.Rows[i].Cells[3].FindControl("SERate");
                        TextBox box5 = (TextBox)StockEntryDetail.Rows[i].Cells[4].FindControl("SEQuantity");
                        TextBox box6 = (TextBox)StockEntryDetail.Rows[i].Cells[5].FindControl("Unit");
                        TextBox box7 = (TextBox)StockEntryDetail.Rows[i].Cells[6].FindControl("SEAmount");
                        box2.Enabled = false;
                        box3.Enabled = false;
                        box4.Enabled = false;
                        box5.Enabled = false;
                        box6.Enabled = false;
                        box7.Enabled = false;
                        i++;
                    }
                }
                else if (row["Status"].ToString() == "1")
                {
                    foreach (GridViewRow gvr in StockEntryDetail.Rows)
                    {
                        TextBox box2 = (TextBox)StockEntryDetail.Rows[i].Cells[1].FindControl("Item");
                        TextBox box3 = (TextBox)StockEntryDetail.Rows[i].Cells[2].FindControl("Name");
                        TextBox box6 = (TextBox)StockEntryDetail.Rows[i].Cells[5].FindControl("Unit");
                        TextBox box7 = (TextBox)StockEntryDetail.Rows[i].Cells[6].FindControl("SEAmount");
                        box2.Enabled = false;
                        box3.Enabled = false;
                        box6.Enabled = false;
                        box7.Enabled = false;
                        i++;
                    }
                }
            }
            catch (Exception e)
            {

            }


        }

        private void SetInitialRows()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("Rate", typeof(float)));
                dt.Columns.Add(new DataColumn("Qty", typeof(int)));
                dt.Columns.Add(new DataColumn("Amount", typeof(float)));
                dt.Columns.Add(new DataColumn("BaseUnitCode", typeof(string)));
                for (int i = 0; i < 1; i++)
                {
                    dr = dt.NewRow();
                    dr["ID"] = "0";
                    dr["ItemCode"] = string.Empty;
                    dr["ItemName"] = string.Empty;
                    dr["Rate"] = DBNull.Value;
                    dr["Qty"] = DBNull.Value;
                    dr["Amount"] = DBNull.Value;
                    dr["BaseUnitCode"] = string.Empty;
                    dt.Rows.Add(dr);
                }

                //dr = dt.NewRow();

                //Store the DataTable in ViewState
                ViewState["CurrentTable"] = dt;

                StockEntryDetail.DataSource = dt;
                StockEntryDetail.DataBind();
            }
            catch (Exception e)
            {

            }

        }

        protected void SaveBtnClick(object sender, EventArgs e)
        {
            try
            {
                #region Save
                int returnValue = 0;
                int selectedSequenceId = 0;
                if (AdjustmentType.SelectedValue == "0")
                {
                    selectedSequenceId = Convert.ToInt32(AdditionSequenceNoID.Value);
                }
                else if (AdjustmentType.SelectedValue == "1")
                {
                    selectedSequenceId = Convert.ToInt32(DeductionSequenceNoID.Value);
                }
                else if (AdjustmentType.SelectedValue == "2")
                {
                    selectedSequenceId = Convert.ToInt32(OpeningSequenceNoID.Value);
                }
                DateTime date = DateTime.ParseExact(Date.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                float amount = 0;
                if (Amount.Text!="")
                {
                    amount = float.Parse(Amount.Text, CultureInfo.InvariantCulture.NumberFormat);
                }
                returnValue = XBDataProvider.StockEntry.SaveSE(Session["CompanyCode"].ToString(),Convert.ToInt32(AdjustmentType.SelectedValue), Document.Text, 1, date, Location.SelectedValue, CreatedUser.SelectedValue,
                    User.Identity.Name, Reference.Text, amount, Currency.Text, selectedSequenceId);
                if (returnValue > 0)
                {
                    PageStatus.Value = "creating";
                    StokeEntryMstId.Value = returnValue.ToString();
                    gridDetails.Visible = true;
                    SaveBtn.Visible = false;
                    CancelBtn.Visible = false;
                    SetInitialRows();
                    SaveSuccess.Visible = true;
                    failure.Visible = false;
                }
                else
                {
                    failure.Visible = true;
                }
                #endregion
            }
            catch (Exception ex)
            {

            }

        }

        public void SetStockEntryChildGrid()
        {
            DataTable dt = new DataTable();
            dt = XBDataProvider.StockEntry.GetStockEntryDtlById(Convert.ToInt32(StokeEntryMstId.Value));

            if (dt.Rows.Count > 0)
            {
                StockEntryDetail.DataSource = dt;
                StockEntryDetail.DataBind();
                int i = 0;
                foreach (GridViewRow gvr in StockEntryDetail.Rows)
                {
                    TextBox box2 = (TextBox)StockEntryDetail.Rows[i].Cells[1].FindControl("Item");
                    TextBox box3 = (TextBox)StockEntryDetail.Rows[i].Cells[2].FindControl("Name");
                    TextBox box4 = (TextBox)StockEntryDetail.Rows[i].Cells[3].FindControl("SERate");
                    TextBox box5 = (TextBox)StockEntryDetail.Rows[i].Cells[4].FindControl("SEQuantity");
                    TextBox box6 = (TextBox)StockEntryDetail.Rows[i].Cells[5].FindControl("Unit");
                    TextBox box7 = (TextBox)StockEntryDetail.Rows[i].Cells[6].FindControl("SEAmount");
                    
                    if (Status.SelectedValue=="1")
                    {
                        box2.Enabled = false;
                        box3.Enabled = false;
                        box6.Enabled = false;
                        box7.Enabled = false;
                    }
                    else if (Status.SelectedValue == "2")
                    {
                        box2.Enabled = false;
                        box3.Enabled = false;
                        box4.Enabled = false;
                        box5.Enabled = false;
                        box6.Enabled = false;
                        box7.Enabled = false;
                        
                    }
                    i++;
                }
            }
        }

        protected void SaveBtnDetailClick(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("CompanyCode", typeof(string)));
                dt.Columns.Add(new DataColumn("LocationCode", typeof(string)));
                dt.Columns.Add(new DataColumn("StockAdjustmentMstId", typeof(int)));
                dt.Columns.Add(new DataColumn("Pos", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("BaseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Qty", typeof(int)));
                dt.Columns.Add(new DataColumn("Currency", typeof(string)));
                dt.Columns.Add(new DataColumn("Rate", typeof(float)));
                dt.Columns.Add(new DataColumn("Amount", typeof(float)));
                dt.Columns.Add(new DataColumn("Reference", typeof(string)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));
                dt.Columns.Add(new DataColumn("ErrorMsg", typeof(string)));
                dt.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
                dt.Columns.Add(new DataColumn("UpdatedBy", typeof(string)));
                dt.Columns.Add(new DataColumn("CreatedDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("UpdatedDate", typeof(DateTime)));
                int i = 0;
                if (PageStatus.Value == "creating")
                {
                    TextBox box2 = (TextBox)StockEntryDetail.Rows[i].Cells[1].FindControl("Item");
                    TextBox box3 = (TextBox)StockEntryDetail.Rows[i].Cells[2].FindControl("Name");
                    TextBox box4 = (TextBox)StockEntryDetail.Rows[i].Cells[3].FindControl("SERate");
                    TextBox box5 = (TextBox)StockEntryDetail.Rows[i].Cells[4].FindControl("SEQuantity");
                    TextBox box6 = (TextBox)StockEntryDetail.Rows[i].Cells[5].FindControl("Unit");
                    TextBox box7 = (TextBox)StockEntryDetail.Rows[i].Cells[6].FindControl("SEAmount");
                    string[] Items = Request.Form["Item"].Split(',');
                    string[] Names = Request.Form["Name"].Split(',');
                    string[] SERates = Request.Form["SERate"].Split(',');
                    string[] SEQuantitys = Request.Form["SEQuantity"].Split(',');
                    string[] Units = Request.Form["Unit"].Split(',');
                    string[] SEAmounts = Request.Form["SEAmount"].Split(',');
                    if (box2.Text != "" && box2.Text.Length != 0)
                    {
                        dr = dt.NewRow();
                        if (string.IsNullOrEmpty(StockEntryDetail.DataKeys[i]["ID"].ToString()))
                        {
                            dr["ID"] = DBNull.Value;
                        }
                        else
                        {
                            dr["ID"] = Convert.ToInt32(StockEntryDetail.DataKeys[i]["ID"]);
                        }

                        dr["CompanyCode"] = Session["CompanyCode"].ToString();
                        dr["LocationCode"] = Location.Text;
                        dr["StockAdjustmentMstId"] = Convert.ToInt32(StokeEntryMstId.Value);
                        dr["Pos"] = 0;
                        dr["ItemCode"] = box2.Text;
                        dr["ItemName"] = box3.Text;
                        dr["BaseUnitCode"] = box6.Text;
                        dr["Qty"] = Convert.ToInt32(box5.Text);
                        dr["Currency"] = "";
                        dr["Rate"] = float.Parse(box4.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["Amount"] = float.Parse(box7.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["Status"] = 1;
                        dr["Reference"] = "";
                        dr["ErrorMsg"] = null;
                        dr["CreatedBy"] = User.Identity.Name;
                        dr["UpdatedBy"] = User.Identity.Name;
                        dr["CreatedDate"] = DateTime.Now.Date;
                        dr["UpdatedDate"] = DateTime.Now.Date;
                        dt.Rows.Add(dr);
                    }
                    for (int k = 0; k < Items.Length; k++)
                    {
                        dr = dt.NewRow();
                        dr["ID"] = DBNull.Value;
                        dr["CompanyCode"] = Session["CompanyCode"].ToString();
                        dr["LocationCode"] = Location.Text;
                        dr["StockAdjustmentMstId"] = Convert.ToInt32(StokeEntryMstId.Value);
                        dr["Pos"] = 0;
                        dr["ItemCode"] = Items[k];
                        dr["ItemName"] = Names[k];
                        dr["BaseUnitCode"] = Units[k];
                        dr["Qty"] = Convert.ToInt32(SEQuantitys[k]);
                        dr["Currency"] = "";
                        dr["Rate"] = float.Parse(SERates[k], CultureInfo.InvariantCulture.NumberFormat);
                        dr["Amount"] = float.Parse(SEAmounts[k], CultureInfo.InvariantCulture.NumberFormat);
                        dr["Status"] = 1;
                        dr["Reference"] = "";
                        dr["ErrorMsg"] = null;
                        dr["CreatedBy"] = User.Identity.Name;
                        dr["UpdatedBy"] = User.Identity.Name;
                        dr["CreatedDate"] = DateTime.Now.Date;
                        dr["UpdatedDate"] = DateTime.Now.Date;
                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    foreach (GridViewRow row in StockEntryDetail.Rows)
                    {
                        TextBox box2 = (TextBox)StockEntryDetail.Rows[i].Cells[1].FindControl("Item");
                        TextBox box3 = (TextBox)StockEntryDetail.Rows[i].Cells[2].FindControl("Name");
                        TextBox box4 = (TextBox)StockEntryDetail.Rows[i].Cells[3].FindControl("SERate");
                        TextBox box5 = (TextBox)StockEntryDetail.Rows[i].Cells[4].FindControl("SEQuantity");
                        TextBox box6 = (TextBox)StockEntryDetail.Rows[i].Cells[5].FindControl("Unit");
                        TextBox box7 = (TextBox)StockEntryDetail.Rows[i].Cells[6].FindControl("SEAmount");
                        if (box2.Text != "" && box2.Text.Length != 0)
                        {
                            dr = dt.NewRow();
                            if (string.IsNullOrEmpty(StockEntryDetail.DataKeys[i]["ID"].ToString()))
                            {
                                dr["ID"] = DBNull.Value;
                            }
                            else
                            {
                                dr["ID"] = Convert.ToInt32(StockEntryDetail.DataKeys[i]["ID"]);
                            }

                            dr["CompanyCode"] = Session["CompanyCode"].ToString();
                            dr["LocationCode"] = Location.Text;
                            dr["StockAdjustmentMstId"] = Convert.ToInt32(StokeEntryMstId.Value);
                            dr["Pos"] = 0;
                            dr["ItemCode"] = box2.Text;
                            dr["ItemName"] = box3.Text;
                            dr["BaseUnitCode"] = box6.Text;
                            dr["Qty"] = Convert.ToInt32(box5.Text);
                            dr["Currency"] = "";
                            dr["Rate"] = float.Parse(box4.Text, CultureInfo.InvariantCulture.NumberFormat);
                            dr["Amount"] = float.Parse(box7.Text, CultureInfo.InvariantCulture.NumberFormat);
                            dr["Status"] = 1;
                            dr["Reference"] = "";
                            dr["ErrorMsg"] = null;
                            dr["CreatedBy"] = User.Identity.Name;
                            dr["UpdatedBy"] = User.Identity.Name;
                            dr["CreatedDate"] = DateTime.Now.Date;
                            dr["UpdatedDate"] = DateTime.Now.Date;
                            dt.Rows.Add(dr);
                            i++;
                        }

                    }
                }
                
                if (dt.Rows.Count > 0)
                {
                    XBDataProvider.StockEntry.SaveSEDetail(Convert.ToInt32(StokeEntryMstId.Value), float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat), User.Identity.Name, dt);
                     btnConvertStockRegister.Visible = true;
                     btnPrint.Visible = true;
                     Amount.Text = Request.Form[Amount.UniqueID].ToString();
                     Status.SelectedValue = "1";
                     SetStockEntryChildGrid();
                     PageStatus.Value = "edit";
                     SaveSuccess.Visible = false;
                     UpdateSuccess.Visible = true;
                     failure.Visible = false;
                    
                        
                }
                
            }
            catch(Exception ex)
            {
                SaveSuccess.Visible = false;
                UpdateSuccess.Visible = false;
                failure.Visible = true;
            }
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
                        TextBox box2 = (TextBox)StockEntryDetail.Rows[i].Cells[1].FindControl("Item");
                        TextBox box3 = (TextBox)StockEntryDetail.Rows[i].Cells[2].FindControl("Name");
                        TextBox box4 = (TextBox)StockEntryDetail.Rows[i].Cells[3].FindControl("SERate");
                        TextBox box5 = (TextBox)StockEntryDetail.Rows[i].Cells[4].FindControl("SEQuantity");
                        TextBox box6 = (TextBox)StockEntryDetail.Rows[i].Cells[5].FindControl("Unit");
                        TextBox box7 = (TextBox)StockEntryDetail.Rows[i].Cells[6].FindControl("SEAmount");
                        box2.Text = dt.Rows[i]["ItemCode"].ToString();
                        box3.Text = dt.Rows[i]["ItemName"].ToString();
                        box4.Text = dt.Rows[i]["Rate"].ToString();
                        box5.Text = dt.Rows[i]["Qty"].ToString();
                        box6.Text = dt.Rows[i]["BaseUnitCode"].ToString();
                        box7.Text = dt.Rows[i]["Amount"].ToString();

                        rowIndex++;
                    }
                }
            }

        }

        private void AddNewRowToGrid()
        {
            int rowIndex = 0;
            Amount.Text = Request.Form[Amount.UniqueID].ToString();
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox box2 = (TextBox)StockEntryDetail.Rows[i].Cells[1].FindControl("Item");
                        TextBox box3 = (TextBox)StockEntryDetail.Rows[i].Cells[2].FindControl("Name");
                        TextBox box4 = (TextBox)StockEntryDetail.Rows[i].Cells[3].FindControl("SERate");
                        TextBox box5 = (TextBox)StockEntryDetail.Rows[i].Cells[4].FindControl("SEQuantity");
                        TextBox box6 = (TextBox)StockEntryDetail.Rows[i].Cells[5].FindControl("Unit");
                        TextBox box7 = (TextBox)StockEntryDetail.Rows[i].Cells[6].FindControl("SEAmount");


                        //drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i]["ID"] = Convert.ToInt32(StockEntryDetail.DataKeys[i]["ID"]); ;
                        dtCurrentTable.Rows[i]["ItemCode"] = box2.Text;
                        dtCurrentTable.Rows[i]["ItemName"] = box3.Text;
                        if (box4.Text != "")
                        {
                            dtCurrentTable.Rows[i]["Rate"] = box4.Text;
                        }
                        if (box5.Text != "")
                        {
                            dtCurrentTable.Rows[i]["Qty"] = box5.Text;
                        }
                        dtCurrentTable.Rows[i]["BaseUnitCode"] = box6.Text;
                        if (box7.Text != "")
                        {
                            dtCurrentTable.Rows[i]["Amount"] = box7.Text;
                        }
                        rowIndex++;
                    }
                    for (int j = 0; j < 5; j++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;

                    StockEntryDetail.DataSource = dtCurrentTable;
                    StockEntryDetail.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }

        protected void AddNewRowClick(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        protected void btnConvertStockRegisterClick(object sender, EventArgs e)
        {
            if (XBDataProvider.StockEntry.ConvertToStockRegister(Convert.ToInt32(StokeEntryMstId.Value)))
            {
                Status.SelectedValue = "2";
                int i = 0;
                foreach (GridViewRow gvr in StockEntryDetail.Rows)
                {
                    TextBox box2 = (TextBox)StockEntryDetail.Rows[i].Cells[1].FindControl("Item");
                    TextBox box3 = (TextBox)StockEntryDetail.Rows[i].Cells[2].FindControl("Name");
                    TextBox box4 = (TextBox)StockEntryDetail.Rows[i].Cells[3].FindControl("SERate");
                    TextBox box5 = (TextBox)StockEntryDetail.Rows[i].Cells[4].FindControl("SEQuantity");
                    TextBox box6 = (TextBox)StockEntryDetail.Rows[i].Cells[5].FindControl("Unit");
                    TextBox box7 = (TextBox)StockEntryDetail.Rows[i].Cells[6].FindControl("SEAmount");
                    box2.Enabled = false;
                    box3.Enabled = false;
                    box4.Enabled = false;
                    box5.Enabled = false;
                    box6.Enabled = false;
                    box7.Enabled = false;
                    i++;
                }
                SaveSuccess.Visible = false;
                UpdateSuccess.Visible = false;
                FinalizeSuccess.Visible = true;
                failure.Visible = false;
                btnSaveDtl.Visible = false;
                btnConvertStockRegister.Visible = false;
            }
            else
            {
                failure.Visible = true;
                FinalizeSuccess.Visible = false;
                SaveSuccess.Visible = false;
                UpdateSuccess.Visible = false;
            }
        }

    }
}
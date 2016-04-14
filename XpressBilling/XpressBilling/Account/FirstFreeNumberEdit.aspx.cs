using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class FirstFreeNumberEdit : System.Web.UI.Page
    {
        DataTable dtLocation = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                string lastFirstFreeNumberNumber = XBDataProvider.FirstFreeNumber.GetDocumentNumber();
                if (lastFirstFreeNumberNumber == null || lastFirstFreeNumberNumber == "")
                {
                    lastFirstFreeNumberNumber = "0";
                }
                else
                {
                    lastFirstFreeNumberNumber = lastFirstFreeNumberNumber.Replace('F', ' ');
                }
                LastFirstFreeNumber.Value = lastFirstFreeNumberNumber;
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }

                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    DataTable firstFreeDetails = XBDataProvider.FirstFreeNumber.GetFirstFreeById(id);
                    if (firstFreeDetails.Rows.Count > 0)
                    {
                        SetFirstFreeDetails(firstFreeDetails);

                    }
                }
                else
                {
                    int prevType = XBDataProvider.FirstFreeNumber.GetFirstTimeSelectedType(Session["CompanyCode"].ToString());
                    if (prevType != -1)
                    {
                        Type.SelectedValue = prevType.ToString();
                        Type.Enabled = false;
                        string[] items = new string[] { "Sales Quotation", "Sales Order", "Manual Invoice", "Sales Return", "Purchase Order", "Stock Adjustment", "Material Issue", "Sales Invoice", "Goods Receipt", "Purchase Return", "Stock Transfer", "Receipt" };
                        DataRow row = null;
                        DataTable dtTable = XBDataProvider.FirstFreeNumber.GetOrderTypeExceptAddedItems(Session["CompanyCode"].ToString());

                        if (dtTable.Rows.Count > 0)
                        {
                            Transaction.Items.Clear();
                            ListItem emptyItem = new ListItem();
                            emptyItem.Value = "";
                            emptyItem.Text = "--Select one--";
                            Transaction.Items.Add(emptyItem);
                            bool flag = true;
                            int value = 0;
                            foreach (string item in items)
                            {
                                flag = true;
                                for (int i = 0; i < dtTable.Rows.Count; i++)
                                {
                                    row = dtTable.Rows[i];
                                    if (value.ToString() == row["Transactions"].ToString())
                                    {
                                        flag = false;
                                    }
                                }
                                if (flag)
                                {
                                    ListItem list = new ListItem();
                                    list.Value = value.ToString();
                                    list.Text = item;
                                    Transaction.Items.Add(list);
                                }
                                value++;
                            }
                        }
                    }
                    FirstFreeNumberId.Value = "0"; ;
                    CreatedUser.Visible = false;
                    CreatedDate.Visible = false;
                    lblDate.Visible = false;
                    lblUser.Visible = false;
                    FirstFreeDetail.Visible = false;

                }
            }
        }

        public void SetFirstFreeDetails(DataTable firstFreeDetails)
        {
            try
            {
                DataRow row = firstFreeDetails.Rows[0];
                FirstFreeNumberId.Value = row["ID"].ToString();
                Type.SelectedValue = row["SeqType"].ToString();
                Type.Enabled = false;
                NumberGroup.Text = row["DocumentNo"].ToString();
                Transaction.SelectedValue = row["Transactions"].ToString();
                Transaction.Enabled = false;
                Reference.Text = row["Reference"].ToString();
                CreatedDate.Text = Convert.ToDateTime(row["DocumentDate"]).ToString("MM'/'dd'/'yyyy");
                CreatedUser.Text = row["CreatedBy"].ToString();
                SetFirstFreeDetailsChildGrid();
            }
            catch (Exception e)
            {

            }


        }

        private DataTable SetInitialRowWithAgainstManualOptions(DataTable dtTemp = null)
        {
            DataTable dt = new DataTable();
            try
            {

                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("OrderType", typeof(string)));
                dt.Columns.Add(new DataColumn("Digits", typeof(int)));
                dt.Columns.Add(new DataColumn("EnterpriseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Prefix", typeof(string)));
                //dt.Columns.Add(new DataColumn("Description", typeof(string)));
                dt.Columns.Add(new DataColumn("SequenceNo", typeof(int)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));
                if (Type.SelectedValue == "0")
                {
                    dr = dt.NewRow();
                    dr["OrderType"] = "Against Order";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["OrderType"] = "Manual Return Order";
                    dt.Rows.Add(dr);
                }
                else
                {
                    if (dtTemp == null)
                        dtLocation = XBDataProvider.Location.GetAllLocations(Session["CompanyCode"].ToString());
                    else
                        dtLocation = dtTemp;
                    for (int i = 0; i < dtLocation.Rows.Count; i++)
                    {
                        DataRow row = dtLocation.Rows[i];
                        dr = dt.NewRow();
                        dr["OrderType"] = "Against Order";
                        dr["EnterpriseUnitCode"] = row["LocationCode"].ToString();
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["OrderType"] = "Manual Return Order";
                        dr["EnterpriseUnitCode"] = row["LocationCode"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        private DataTable SetInitialRowWithCashCreditOptions(DataTable dtTemp = null)
        {
            DataTable dt = new DataTable();
            try
            {

                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("OrderType", typeof(string)));
                dt.Columns.Add(new DataColumn("Digits", typeof(int)));
                dt.Columns.Add(new DataColumn("EnterpriseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Prefix", typeof(string)));
                //dt.Columns.Add(new DataColumn("Description", typeof(string)));
                dt.Columns.Add(new DataColumn("SequenceNo", typeof(int)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));


                if (Type.SelectedValue == "0")
                {
                    dr = dt.NewRow();
                    dr["OrderType"] = "Cash";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["OrderType"] = "Credit";
                    dt.Rows.Add(dr);
                }
                else
                {
                    if (dtTemp == null)
                        dtLocation = XBDataProvider.Location.GetAllLocations(Session["CompanyCode"].ToString());
                    else
                        dtLocation = dtTemp;
                    for (int i = 0; i < dtLocation.Rows.Count; i++)
                    {
                        DataRow row = dtLocation.Rows[i];
                        dr = dt.NewRow();
                        dr["EnterpriseUnitCode"] = row["LocationCode"].ToString();
                        dr["OrderType"] = "Cash";
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["EnterpriseUnitCode"] = row["LocationCode"].ToString();
                        dr["OrderType"] = "Credit";
                        dt.Rows.Add(dr);
                    }
                }

                //FirstFreeDetail.DataSource = dt;
                //FirstFreeDetail.DataBind();
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        private DataTable SetInitialRowWithAddtionDeductionOpeningOptions(DataTable dtTemp = null)
        {
            DataTable dt = new DataTable();
            try
            {

                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("OrderType", typeof(string)));
                dt.Columns.Add(new DataColumn("EnterpriseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Digits", typeof(int)));
                dt.Columns.Add(new DataColumn("Prefix", typeof(string)));
                //dt.Columns.Add(new DataColumn("Description", typeof(string)));
                dt.Columns.Add(new DataColumn("SequenceNo", typeof(int)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));

                if (Type.SelectedValue == "0")
                {
                    dr = dt.NewRow();
                    dr["OrderType"] = "Addition";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["OrderType"] = "Deduction";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["OrderType"] = "Opening";
                    dt.Rows.Add(dr);
                }
                else
                {
                    if (dtTemp == null)
                        dtLocation = XBDataProvider.Location.GetAllLocations(Session["CompanyCode"].ToString());
                    else
                        dtLocation = dtTemp;
                    for (int i = 0; i < dtLocation.Rows.Count; i++)
                    {
                        DataRow row = dtLocation.Rows[i];
                        dr = dt.NewRow();
                        dr["OrderType"] = "Addition";
                        dr["EnterpriseUnitCode"] = row["LocationCode"].ToString();
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["OrderType"] = "Deduction";
                        dr["EnterpriseUnitCode"] = row["LocationCode"].ToString();
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["OrderType"] = "Opening";
                        dr["EnterpriseUnitCode"] = row["LocationCode"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
                //FirstFreeDetail.DataSource = dt;
                //FirstFreeDetail.DataBind();
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        private DataTable SetInitialRowWithLocalImportOptions(DataTable dtTemp = null)
        {
            DataTable dt = new DataTable();
            try
            {

                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("OrderType", typeof(string)));
                dt.Columns.Add(new DataColumn("Digits", typeof(int)));
                dt.Columns.Add(new DataColumn("EnterpriseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Prefix", typeof(string)));
                //dt.Columns.Add(new DataColumn("Description", typeof(string)));
                dt.Columns.Add(new DataColumn("SequenceNo", typeof(int)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));


                if (Type.SelectedValue == "0")
                {
                    dr = dt.NewRow();
                    dr["OrderType"] = "Local";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["OrderType"] = "Import";
                    dt.Rows.Add(dr);
                }
                else
                {
                    if (dtTemp == null)
                        dtLocation = XBDataProvider.Location.GetAllLocations(Session["CompanyCode"].ToString());
                    else
                        dtLocation = dtTemp;
                    for (int i = 0; i < dtLocation.Rows.Count; i++)
                    {
                        DataRow row = dtLocation.Rows[i];

                        dr = dt.NewRow();
                        dr["EnterpriseUnitCode"] = row["LocationCode"].ToString();
                        dr["OrderType"] = "Local";
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["EnterpriseUnitCode"] = row["LocationCode"].ToString();
                        dr["OrderType"] = "Import";
                        dt.Rows.Add(dr);
                    }
                }
                //FirstFreeDetail.DataSource = dt;
                //FirstFreeDetail.DataBind();
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        private DataTable SetInitialRowWithManualGoodsOptions(DataTable dtTemp = null)
        {
            DataTable dt = new DataTable();
            try
            {

                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("OrderType", typeof(string)));
                dt.Columns.Add(new DataColumn("Digits", typeof(int)));
                dt.Columns.Add(new DataColumn("EnterpriseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Prefix", typeof(string)));
                //dt.Columns.Add(new DataColumn("Description", typeof(string)));
                dt.Columns.Add(new DataColumn("SequenceNo", typeof(int)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));


                if (Type.SelectedValue == "0")
                {
                    dr = dt.NewRow();
                    dr["OrderType"] = "Manual Goods Receipt";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["OrderType"] = "Goods Receipt Auto";
                    dt.Rows.Add(dr);
                }
                else
                {
                    if (dtTemp == null)
                        dtLocation = XBDataProvider.Location.GetAllLocations(Session["CompanyCode"].ToString());
                    else
                        dtLocation = dtTemp;
                    for (int i = 0; i < dtLocation.Rows.Count; i++)
                    {
                        DataRow row = dtLocation.Rows[i];

                        dr = dt.NewRow();
                        dr["OrderType"] = "Manual Goods Receipt";
                        dr["EnterpriseUnitCode"] = row["LocationCode"].ToString();
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["OrderType"] = "Goods Receipt Auto";
                        dr["EnterpriseUnitCode"] = row["LocationCode"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
                //FirstFreeDetail.DataSource = dt;
                //FirstFreeDetail.DataBind();
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        private DataTable SetInitialRowWithReceiptOptions(DataTable dtTemp = null)
        {
            DataTable dt = new DataTable();
            try
            {
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("OrderType", typeof(string)));
                dt.Columns.Add(new DataColumn("Digits", typeof(int)));
                dt.Columns.Add(new DataColumn("EnterpriseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Prefix", typeof(string)));
                //dt.Columns.Add(new DataColumn("Description", typeof(string)));
                dt.Columns.Add(new DataColumn("SequenceNo", typeof(int)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));


                if (Type.SelectedValue == "0")
                {
                    dr = dt.NewRow();
                    dr["OrderType"] = "Receipt Against Invoice";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["OrderType"] = "Advance Receipt";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["OrderType"] = "Advance Allocation";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["OrderType"] = "Credit Note";
                    dt.Rows.Add(dr);
                }
                else
                {
                    if (dtTemp == null)
                        dtLocation = XBDataProvider.Location.GetAllLocations(Session["CompanyCode"].ToString());
                    else
                        dtLocation = dtTemp;
                    for (int i = 0; i < dtLocation.Rows.Count; i++)
                    {
                        DataRow row = dtLocation.Rows[i];

                        dr = dt.NewRow();
                        dr["OrderType"] = "Receipt Against Invoice";
                        dr["EnterpriseUnitCode"] = row["LocationCode"].ToString();
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["OrderType"] = "Advance Receipt";
                        dr["EnterpriseUnitCode"] = row["LocationCode"].ToString();
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["OrderType"] = "Advance Allocation";
                        dr["EnterpriseUnitCode"] = row["LocationCode"].ToString();
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["OrderType"] = "Credit Note";
                        dr["EnterpriseUnitCode"] = row["LocationCode"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
                //FirstFreeDetail.DataSource = dt;
                //FirstFreeDetail.DataBind();
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        protected void saveFirstFreeNumber(object sender, EventArgs e)
        {
            try
            {
                string newDocumentNumber = "F" + (Convert.ToInt32(LastFirstFreeNumber.Value) + 1);
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("CompanyCode", typeof(string)));
                dt.Columns.Add(new DataColumn("FirstFreeNumberMstID", typeof(int)));
                dt.Columns.Add(new DataColumn("DocumentNo", typeof(string)));
                dt.Columns.Add(new DataColumn("DocumentDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("OrderType", typeof(string)));
                dt.Columns.Add(new DataColumn("EnterpriseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Digits", typeof(int)));
                dt.Columns.Add(new DataColumn("Prefix", typeof(string)));
                dt.Columns.Add(new DataColumn("SequenceNo", typeof(int)));
                dt.Columns.Add(new DataColumn("Reference", typeof(string)));
                dt.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
                dt.Columns.Add(new DataColumn("UpdatedBy", typeof(string)));
                dt.Columns.Add(new DataColumn("CreatedDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("UpdatedDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));
                int i = 0;
                foreach (GridViewRow row in FirstFreeDetail.Rows)
                {
                    TextBox box1 = (TextBox)FirstFreeDetail.Rows[i].Cells[0].FindControl("OrderType");
                    TextBox box2 = (TextBox)FirstFreeDetail.Rows[i].Cells[1].FindControl("EnterpriseUnit");
                    TextBox box3 = (TextBox)FirstFreeDetail.Rows[i].Cells[2].FindControl("NoOfDigits");
                    TextBox box4 = (TextBox)FirstFreeDetail.Rows[i].Cells[3].FindControl("Prefix");
                    TextBox box5 = (TextBox)FirstFreeDetail.Rows[i].Cells[4].FindControl("SequenceNumber");
                    if (box1.Text != "" && box1.Text.Length != 0)
                    {
                        dr = dt.NewRow();
                        dr["ID"] = FirstFreeDetail.DataKeys[i]["ID"];
                        dr["CompanyCode"] = Session["CompanyCode"].ToString();
                        dr["FirstFreeNumberMstID"] = Convert.ToInt32(FirstFreeNumberId.Value);
                        dr["DocumentNo"] = newDocumentNumber;
                        dr["DocumentDate"] = DateTime.Now.Date;
                        if (Type.SelectedValue != "0")
                        {
                            dr["EnterpriseUnitCode"] = box2.Text;
                        }
                        else
                        {
                            dr["EnterpriseUnitCode"] = DBNull.Value;
                        }

                        dr["OrderType"] = box1.Text;
                        dr["Digits"] = box3.Text;
                        dr["Prefix"] = box4.Text;
                        dr["SequenceNo"] = box5.Text;
                        dr["Reference"] = null;
                        dr["CreatedBy"] = User.Identity.Name;
                        dr["UpdatedBy"] = User.Identity.Name;
                        dr["CreatedDate"] = DateTime.Now.Date;
                        dr["UpdatedDate"] = DateTime.Now.Date;
                        dr["Status"] = 1;
                        dt.Rows.Add(dr);
                        i++;
                    }

                }
                if (FirstFreeNumberId.Value != "0")
                {
                    XBDataProvider.FirstFreeNumber.SaveFirstFreeMasterDetail(dt);
                    failureMessage.Visible = false;
                    SaveSuccess.Visible = true;
                }
                else
                {
                    int id = XBDataProvider.FirstFreeNumber.SaveFirstFreeMaster(Session["CompanyCode"].ToString(), newDocumentNumber, Convert.ToInt32(Type.SelectedValue), Convert.ToInt32(Transaction.SelectedValue), Reference.Text, User.Identity.Name, dt);
                    if (id > 0)
                    {
                        Type.Enabled = false;
                        Transaction.Enabled = false;
                        NumberGroup.Text = newDocumentNumber;
                        FirstFreeNumberId.Value = id.ToString();
                        SaveSuccess.Visible = true;
                        failureMessage.Visible = false;
                        SetFirstFreeDetailsChildGrid();
                    }
                    else
                    {
                        failureMessage.Visible = true;
                        SaveSuccess.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                failureMessage.Visible = true;
                SaveSuccess.Visible = false;
            }


        }

        public void SetFirstFreeDetailsChildGrid()
        {
            DataTable dt = new DataTable();
            dt = XBDataProvider.FirstFreeNumber.GetFirstFreeDtlById(Convert.ToInt32(FirstFreeNumberId.Value));

            if (dt.Rows.Count > 0)
            {
                if (Type.SelectedValue == "1")
                {
                    DataTable dtTemp = new DataTable();
                    DataTable dtLocations = XBDataProvider.FirstFreeNumber.GetFirstFreeNewLocations(Convert.ToInt32(FirstFreeNumberId.Value));
                    if (Transaction.SelectedValue == "0" || Transaction.SelectedValue == "1" || Transaction.SelectedValue == "2" || Transaction.SelectedValue == "7")
                    {
                        dtTemp = SetInitialRowWithCashCreditOptions(dtLocations);
                    }
                    else if (Transaction.SelectedValue == "3" || Transaction.SelectedValue == "6")
                    {
                        dtTemp = SetInitialRowWithAgainstManualOptions(dtLocations);
                    }
                    else if (Transaction.SelectedValue == "5")
                    {
                        dtTemp = SetInitialRowWithAddtionDeductionOpeningOptions(dtLocations);
                    }
                    else if (Transaction.SelectedValue == "4")
                    {
                        dtTemp = SetInitialRowWithLocalImportOptions(dtLocations);
                    }
                    else if (Transaction.SelectedValue == "8")
                    {
                        dtTemp = SetInitialRowWithManualGoodsOptions(dtLocations);
                    }
                    else if (Transaction.SelectedValue == "11")
                    {
                        dtTemp = SetInitialRowWithReceiptOptions(dtLocations);
                    }
                    DataRow dr = null;
                    DataRow row = null;
                    for(int i=0;i<dtTemp.Rows.Count;i++)
                    {
                        dr = dt.NewRow();
                        row = dtTemp.Rows[i];
                        dr["OrderType"] = row["OrderType"].ToString();
                        dr["EnterpriseUnitCode"] = row["EnterpriseUnitCode"].ToString();
                        dt.Rows.Add(dr);
                    }
                }

                FirstFreeDetail.DataSource = dt;
                FirstFreeDetail.DataBind();

            }
            //SetInitialRow();
            return;
        }

        protected void listFirstFreeNumberDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in FirstFreeDetail.Rows)
            {
                DropDownList ddlFirstFree = gvRow.FindControl("FirstFreeDtlStatus") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlFirstFree != null && hfSelectedValue != null)
                {

                    ddlFirstFree.SelectedValue = hfSelectedValue.Value;
                }
            }
        }

        protected void FirstFreeDetailPreRender(object sender, EventArgs e)
        {
            if (Type.SelectedValue == "0")
            {
                FirstFreeDetail.Columns[1].Visible = false;
            }
            else
                FirstFreeDetail.Columns[1].Visible = true;
        }

        protected void TypeSelectedIndexChanged(object sender, EventArgs e)
        {
            FirstFreeDetail.Visible = false;
            string[] items = new string[] { "Sales Quotation", "Sales Order", "Manual Invoice", "Sales Return", "Purchase Order", "Stock Adjustment", "Material Issue", "Sales Invoice", "Goods Receipt", "Purchase Return", "Stock Transfer", "Receipt" };
            DataRow row = null;
            DataTable dtTable = XBDataProvider.FirstFreeNumber.GetOrderTypeExceptAddedItems(Session["CompanyCode"].ToString());

            if (dtTable.Rows.Count > 0)
            {
                Transaction.Items.Clear();
                bool flag = true;
                int value = 0;
                foreach (string item in items)
                {
                    flag = true;
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        row = dtTable.Rows[i];
                        if (value.ToString() == row["Transactions"].ToString())
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        ListItem list = new ListItem();
                        list.Value = value.ToString();
                        list.Text = item;
                        Transaction.Items.Add(list);
                    }
                    value++;
                }
                if (Transaction.SelectedValue != "")
                {
                    DataTable dt = new DataTable();
                    FirstFreeDetail.Visible = true;
                    if (Transaction.SelectedValue == "0" || Transaction.SelectedValue == "1" || Transaction.SelectedValue == "2" || Transaction.SelectedValue == "7")
                    {
                        dt = SetInitialRowWithCashCreditOptions();
                    }
                    else if (Transaction.SelectedValue == "3" || Transaction.SelectedValue == "6")
                    {
                        dt = SetInitialRowWithAgainstManualOptions();
                    }
                    else if (Transaction.SelectedValue == "5")
                    {
                        dt = SetInitialRowWithAddtionDeductionOpeningOptions();
                    }
                    else if (Transaction.SelectedValue == "4")
                    {
                        dt = SetInitialRowWithLocalImportOptions();
                    }
                    else if (Transaction.SelectedValue == "8")
                    {
                        dt = SetInitialRowWithManualGoodsOptions();
                    }
                    else if (Transaction.SelectedValue == "11")
                    {
                        dt = SetInitialRowWithReceiptOptions();
                    }
                    FirstFreeDetail.DataSource = dt;
                    FirstFreeDetail.DataBind();
                }
            }
        }

        protected void TransactionSelectedIndexChanged(object sender, EventArgs e)
        {
            FirstFreeDetail.Visible = true;
            DataTable dt = new DataTable();
            if (Transaction.SelectedValue == "0" || Transaction.SelectedValue == "1" || Transaction.SelectedValue == "2" || Transaction.SelectedValue == "7")
            {
                dt = SetInitialRowWithCashCreditOptions();
            }
            else if (Transaction.SelectedValue == "3" || Transaction.SelectedValue == "6")
            {
                dt = SetInitialRowWithAgainstManualOptions();
            }
            else if (Transaction.SelectedValue == "5")
            {
                dt = SetInitialRowWithAddtionDeductionOpeningOptions();
            }
            else if (Transaction.SelectedValue == "4")
            {
                dt = SetInitialRowWithLocalImportOptions();
            }
            else if (Transaction.SelectedValue == "8")
            {
                dt = SetInitialRowWithManualGoodsOptions();
            }
            else if (Transaction.SelectedValue == "11")
            {
                dt = SetInitialRowWithReceiptOptions();
            }
            FirstFreeDetail.DataSource = dt;
            FirstFreeDetail.DataBind();
        }
    }
}
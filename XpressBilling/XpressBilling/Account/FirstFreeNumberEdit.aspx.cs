﻿using System;
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
                    FirstFreeNumberId.Value = "0"; ;
                    CreatedUser.Visible = false;
                    CreatedDate.Visible = false;
                    lblDate.Visible = false;
                    lblUser.Visible = false;
                    FirstFreeDetail.Visible = false;
                    SaveFirstFreeDetails.Visible = false;
                    CancelFirstFreeDetails.Visible = false;
                    if (Type.SelectedValue == "0")
                    {
                        DataRow row = null;
                        DataTable dtTable = XBDataProvider.FirstFreeNumber.GetOrderTypeExceptAddedItems(Session["CompanyCode"].ToString());
                        string[] items = new string[] { "Sales Quotation", "Sales Order", "Manual Invoice", "Sales Return", "Purchase Order", "Stock Adjustment", "Material Issue", "Sales Invoice" };
                        if(dtTable.Rows.Count>0 && Type.SelectedValue=="0")
                        {
                            Transaction.Items.Clear();
                            bool flag = true;
                            int value=0;
                            foreach(string item in items)
                            {
                                flag = true;
                                for(int i=0;i<dtTable.Rows.Count;i++)
                                {
                                    row = dtTable.Rows[i];
                                    if (value.ToString() == row["Transactions"].ToString())
                                    {
                                        flag = false;
                                    }
                                }
                                if(flag)
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
                //Reference.Enabled = false;
                CreatedDate.Text =  Convert.ToDateTime(row["DocumentDate"]).ToString("MM'/'dd'/'yyyy");
                CreatedUser.Text = row["CreatedBy"].ToString();
                saveFirstFreeNumberBtn.Visible = false;
                cancelFirstFreeNumber.Visible = false;
                //PriceBookDetail.Visible = true;
                //SaveDetail.Visible = true;
                //CancelPriceBook2.Visible = true;
                //Type.Enabled = false;
                SetFirstFreeDetailsChildGrid();
            }
            catch (Exception e)
            {

            }


        }

        private void SetInitialRowWithAgainstManualOptions()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("OrderType", typeof(string)));
                dt.Columns.Add(new DataColumn("Digits", typeof(int)));
                dt.Columns.Add(new DataColumn("EnterpriseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Prefix", typeof(string)));
                //dt.Columns.Add(new DataColumn("Description", typeof(string)));
                dt.Columns.Add(new DataColumn("SequenceNo", typeof(int)));
                dt.Columns.Add(new DataColumn("Defaults", typeof(int)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));
                dr = dt.NewRow();
                dr["OrderType"] = "Against Order";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["OrderType"] = "Manual Return Order";
                dt.Rows.Add(dr);

                FirstFreeDetail.DataSource = dt;
                FirstFreeDetail.DataBind();
            }
            catch (Exception e)
            {

            }

        }

        private void SetInitialRowWithCashCreditOptions()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("OrderType", typeof(string)));
                dt.Columns.Add(new DataColumn("Digits", typeof(int)));
                dt.Columns.Add(new DataColumn("EnterpriseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Prefix", typeof(string)));
                //dt.Columns.Add(new DataColumn("Description", typeof(string)));
                dt.Columns.Add(new DataColumn("SequenceNo", typeof(int)));
                dt.Columns.Add(new DataColumn("Defaults", typeof(int)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));

                dr = dt.NewRow();
                dr["OrderType"] = "Cash";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["OrderType"] = "Credit";
                dt.Rows.Add(dr);

                FirstFreeDetail.DataSource = dt;
                FirstFreeDetail.DataBind();
            }
            catch (Exception e)
            {

            }

        }

        private void SetInitialRowWithAddtionDeductionOpeningOptions()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("OrderType", typeof(string)));
                dt.Columns.Add(new DataColumn("EnterpriseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Digits", typeof(int)));
                dt.Columns.Add(new DataColumn("Prefix", typeof(string)));
                //dt.Columns.Add(new DataColumn("Description", typeof(string)));
                dt.Columns.Add(new DataColumn("SequenceNo", typeof(int)));
                dt.Columns.Add(new DataColumn("Defaults", typeof(int)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));
                dr = dt.NewRow();
                dr["OrderType"] = "Addition";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["OrderType"] = "Deduction";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["OrderType"] = "Opening";
                dt.Rows.Add(dr);

                FirstFreeDetail.DataSource = dt;
                FirstFreeDetail.DataBind();
            }
            catch (Exception e)
            {

            }

        }

        protected void saveFirstFreeNumber(object sender, EventArgs e)
        {
            try
            {
                string newDocumentNumber = "F" + (Convert.ToInt32(LastFirstFreeNumber.Value) + 1);
                int id = XBDataProvider.FirstFreeNumber.SaveFirstFreeMaster(Session["CompanyCode"].ToString(), newDocumentNumber, Convert.ToInt32(Type.SelectedValue), Convert.ToInt32(Transaction.SelectedValue), Reference.Text, User.Identity.Name);
                if (id > 0)
                {
                    Type.Enabled = false;
                    Transaction.Enabled = false;
                    saveFirstFreeNumberBtn.Visible = false;
                    cancelFirstFreeNumber.Visible = false;
                    SaveFirstFreeDetails.Visible = true;
                    CancelFirstFreeDetails.Visible = true;
                    NumberGroup.Text = newDocumentNumber;
                    FirstFreeNumberId.Value = id.ToString();
                    FirstFreeDetail.Visible = true;
                    if (Transaction.SelectedValue == "0" || Transaction.SelectedValue == "1" || Transaction.SelectedValue == "2" || Transaction.SelectedValue == "4")
                    {
                        SetInitialRowWithCashCreditOptions();
                    }
                    else if (Transaction.SelectedValue == "3" || Transaction.SelectedValue == "6")
                    {
                        SetInitialRowWithAgainstManualOptions();
                    }
                    else if (Transaction.SelectedValue == "5")
                    {
                        SetInitialRowWithAddtionDeductionOpeningOptions();
                    }
                    
                }
            }
            catch (Exception ex)
            {

            }


        }

        public void SetFirstFreeDetailsChildGrid()
        {
            DataTable dt = new DataTable();
            dt = XBDataProvider.FirstFreeNumber.GetFirstFreeDtlById(Convert.ToInt32(FirstFreeNumberId.Value));
            
            if (dt.Rows.Count > 0)
            {
                FirstFreeDetail.DataSource = dt;
                FirstFreeDetail.DataBind();
                return;
            }
            //SetInitialRow();

        }

        protected void SaveFirstFreeNumberDetails(object sender, EventArgs e)
        {
            try
            {
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
                dt.Columns.Add(new DataColumn("Defaults", typeof(int)));
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
                    TextBox box6 = (TextBox)FirstFreeDetail.Rows[i].Cells[5].FindControl("Default");
                    DropDownList ddl = (DropDownList)FirstFreeDetail.Rows[i].Cells[6].FindControl("FirstFreeDtlStatus");
                    if (box1.Text != "" && box1.Text.Length != 0)
                    {
                        dr = dt.NewRow();
                        dr["ID"] = FirstFreeDetail.DataKeys[i]["ID"];
                        dr["CompanyCode"] = Session["CompanyCode"].ToString();
                        dr["FirstFreeNumberMstID"] = Convert.ToInt32(FirstFreeNumberId.Value);
                        dr["DocumentNo"] = NumberGroup.Text;
                        dr["DocumentDate"] = DateTime.Now.Date;
                        if (Type.SelectedValue!="0")
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
                        dr["Defaults"] = box6.Text;
                        dr["Reference"] = null;
                        dr["CreatedBy"] = User.Identity.Name;
                        dr["UpdatedBy"] = User.Identity.Name;
                        dr["CreatedDate"] = DateTime.Now.Date;
                        dr["UpdatedDate"] = DateTime.Now.Date;
                        dr["Status"] = ddl.SelectedValue;
                        dt.Rows.Add(dr);
                        i++;
                    }

                }
                if (dt.Rows.Count > 0)
                {
                    XBDataProvider.FirstFreeNumber.SaveFirstFreeMasterDetail(dt);
                }
                SetFirstFreeDetailsChildGrid();
            }
            catch (Exception ex)
            {

            }


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
            string[] items = new string[] { "Sales Quotation", "Sales Order", "Manual Invoice", "Sales Return", "Purchase Order", "Stock Adjustment", "Material Issue" };
            if (Type.SelectedValue == "0")
            {
                DataRow row = null;
                DataTable dtTable = XBDataProvider.FirstFreeNumber.GetOrderTypeExceptAddedItems(Session["CompanyCode"].ToString());
                
                if (dtTable.Rows.Count > 0 && Type.SelectedValue == "0")
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
                }
            }
            else
            {
                int value = 0;
                foreach (string item in items)
                {
                    ListItem list = new ListItem();
                    list.Value = value.ToString();
                    list.Text = item;
                    Transaction.Items.Add(list);
                    value++;
                }
            }
        }
    }
}
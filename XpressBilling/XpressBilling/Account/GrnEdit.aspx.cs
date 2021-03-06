﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class GrnEdit : System.Web.UI.Page
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
                
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    DataTable grnDetails = XBDataProvider.GRN.GetGRNById(id);
                    if (grnDetails.Rows.Count > 0)
                    {
                        SetGRNDetails(grnDetails);
                        PageStatus.Value = "edit";
                    }
                }
                else
                {
                    PageStatus.Value = "create";
                    CreatedDate.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");
                    CreatedUser.Text = User.Identity.Name;
                    DataRow row = null;
                    //DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetGRNSequenceDetails(Session["CompanyCode"].ToString());
                    //for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                    //{
                    //    row = dtTableSequenceDetails.Rows[i];
                    //    string sequenceNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));

                    //    if (row["OrderType"].ToString() == "Goods Receipt Auto")
                    //    {
                    //        GoodsReceipt.Text = sequenceNo;
                    //        AutoSequenceNo.Value = sequenceNo;
                    //        AutoSequenceNoID.Value = row["ID"].ToString();
                    //    }
                    //    else if (row["OrderType"].ToString() == "Manual Goods Receipt")
                    //    {
                    //        ManualSequenceNo.Value = sequenceNo;
                    //        ManualSequenceNoID.Value = row["ID"].ToString();
                    //    }
                    //}
                    gridDetails.Visible = false;

                    //DataTable dtGRNSuppliers = XBDataProvider.GRN.GetSupplierCodesFromPurchaseOrder(Session["CompanyCode"].ToString());
                   
                    //SupplierId.Items.Clear();
                    //for (int i = 0; i < dtGRNSuppliers.Rows.Count; i++)
                    //{
                    //    row = dtGRNSuppliers.Rows[i];
                    //    ListItem list = new ListItem();
                    //    list.Value = row["BussinessPartnerCode"].ToString();
                    //    list.Text = row["BussinessPartnerCode"].ToString();
                    //    SupplierId.Items.Add(list);
                    //}
                    //ListItem item = new ListItem();
                    //item.Text = "Select Supplier";
                    //item.Value = string.Empty;
                    //SupplierId.Items.Insert(0, item);

                }
            }
        }

        public void SetGRNDetails(DataTable grnDetails)
        {
            try
            {
                btnPrint.Visible = true;
                DataRow row = grnDetails.Rows[0];
                GRNId.Value = row["ID"].ToString();
                GRNType.SelectedValue = row["OrderType"].ToString();
                GRNType.Enabled = false;
                GoodsReceipt.Text = row["GoodsReceiptNo"].ToString();
                Status.SelectedValue = row["Status"].ToString();
                SupplierIdGrn.Text = row["BussinessPartnerCode"].ToString();
                SupplierIdGrn.ReadOnly = true;
                Name.Text = row["Name"].ToString();
                Reference.Text = row["Reference"].ToString();
                PurchaseOrderText.Text = row["PurchaseOrderNo"].ToString();
                PurchaseOrderText.Visible = true;
                PurchaseOrder.Visible = false;
                CreatedUser.Text = row["CreatedBy"].ToString();
                PackingSlip.Text = row["PackingSlip"].ToString();
                PackingSlip.ReadOnly = true;
                TotalQty.Text = row["TotalQty"].ToString();
                CreatedDate.Text = Convert.ToDateTime(row["GoodsReceiptDate"]).ToString("MM'/'dd'/'yyyy");
                CreatedDate.ReadOnly = true;
                Location.Text = row["Location"].ToString();
                Location.ReadOnly = true;
                LocationHidden.Value = row["LocationCode"].ToString();
                if (Status.SelectedValue == "2")
                {
                    btnConverGRN.Visible = false;
                    btnSaveDtl.Visible = false;
                }
                else
                {
                    btnConverGRN.Visible = true;
                }
                SetGRNChildGrid(row["PurchaseOrderNo"].ToString());
            }
            catch (Exception e)
            {

            }
        }

        public void SetGRNChildGrid(string selectedPO)
        {
            DataTable dt = new DataTable();
            DataTable dtDetails = XBDataProvider.GRN.GetGRNDtl(selectedPO);
            if (dtDetails.Rows.Count > 0)
            {
                GRNId.Value = dtDetails.Rows[0]["GoodsReceiptMstId"].ToString();
                GRNDetail.DataSource = dtDetails;
                GRNDetail.DataBind();
                gridDetails.Visible = true;
                cancelGrn.Visible = false;
            }

        }

        //protected void SupplierIdSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataRow row = null;
        //        string selectedBP = SupplierId.SelectedValue;
        //        if (selectedBP!=string.Empty)
        //        {
        //            DataTable dtDetails = XBDataProvider.GRN.GetSupplierDetails(selectedBP);
        //            if (dtDetails.Rows.Count > 0)
        //            {
        //                Name.Text = dtDetails.Rows[0]["Name"].ToString();
        //                Name.ReadOnly = true;
        //                Location.Text = dtDetails.Rows[0]["LocationCode"].ToString();
        //                Location.ReadOnly = true;
        //            }
        //            for (int i = 0; i < dtDetails.Rows.Count; i++)
        //            {
        //                row = dtDetails.Rows[i];
        //                ListItem list = new ListItem();
        //                list.Value = row["PurchaseOrderNo"].ToString();
        //                list.Text = row["PurchaseOrderNo"].ToString();
        //                PurchaseOrder.Items.Add(list);
        //            }
        //            ListItem item = new ListItem();
        //            item.Text = "Select Purchase Order";
        //            item.Value = string.Empty;
        //            PurchaseOrder.Items.Insert(0, item);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        protected void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = null;
                string selectedBP = selectedSupplier.Value;
                SupplierIdGrn.Text = selectedBP;
                if (selectedBP != string.Empty)
                {
                    DataTable dtDetails = XBDataProvider.GRN.GetSupplierDetails(selectedBP);
                    if (dtDetails.Rows.Count > 0)
                    {
                        Name.Text = dtDetails.Rows[0]["Name"].ToString();
                        Name.ReadOnly = true;
                        Location.Text = dtDetails.Rows[0]["LocationCode"].ToString();
                    }
                    for (int i = 0; i < dtDetails.Rows.Count; i++)
                    {
                        row = dtDetails.Rows[i];
                        ListItem list = new ListItem();
                        list.Value = row["PurchaseOrderNo"].ToString();
                        list.Text = row["PurchaseOrderNo"].ToString();
                        PurchaseOrder.Items.Add(list);
                    }
                    ListItem item = new ListItem();
                    item.Text = "Select Purchase Order";
                    item.Value = string.Empty;
                    PurchaseOrder.Items.Insert(0, item);
                    DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetGRNSequenceDetails(CompanyCode.Value);
                    for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                    {
                        row = dtTableSequenceDetails.Rows[i];
                        if (row["SeqType"].ToString() == "1" && row["OrderType"].ToString() == GRNType.SelectedItem.Text && row["EnterpriseUnitCode"].ToString() == Location.Text)
                        {
                            GoodsReceipt.Text = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                            GrnSequenceNoID.Value = row["ID"].ToString();
                        }
                        else if (row["SeqType"].ToString() == "0" && row["OrderType"].ToString() == GRNType.SelectedItem.Text)
                        {
                            GoodsReceipt.Text = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                            GrnSequenceNoID.Value = row["ID"].ToString();
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {

            }
        }
        protected void PurchaseOrderSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedPO = PurchaseOrder.SelectedValue;
                if (selectedPO != string.Empty)
                {
                    SetGRNChildGrid(selectedPO);
                    cancelGrn.Visible = false;
                    PackingSlip.Attributes.Add("class", "form-control required");
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void SaveBtnDetailClick(object sender, EventArgs e)
        {
            try
            {
                
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("GoodsReceiptMstId", typeof(int)));
                dt.Columns.Add(new DataColumn("GoodsReceiptNo", typeof(string)));
                dt.Columns.Add(new DataColumn("GoodsReceiptDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("ReceivedQty", typeof(int)));
                int i = 0;
                DateTime date = DateTime.ParseExact(CreatedDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                foreach (GridViewRow row in GRNDetail.Rows)
                {
                    TextBox box3 = (TextBox)GRNDetail.Rows[i].Cells[5].FindControl("ReceivedQuantity");
                    dr = dt.NewRow();
                    dr["ID"] = Convert.ToInt32(GRNDetail.DataKeys[i]["ID"]);
                        
                    dr["GoodsReceiptMstId"] = Convert.ToInt32(GRNId.Value);
                    dr["GoodsReceiptNo"] = Request.Form[GoodsReceipt.UniqueID];
                    dr["GoodsReceiptDate"] = date;
                    dr["ReceivedQty"] = box3.Text;
                    dt.Rows.Add(dr);
                    i++;

                }
                if (PageStatus.Value=="create")
                {
                    int selectedSequenceID = Convert.ToInt32(GrnSequenceNoID.Value);
                   if(XBDataProvider.GRN.SaveGRNDetail(Convert.ToInt32(GRNId.Value),Request.Form[GoodsReceipt.UniqueID], date, PackingSlip.Text, Convert.ToInt32(Request.Form[TotalQty.UniqueID]), Reference.Text, Convert.ToInt32(GRNType.SelectedValue), dt, selectedSequenceID))
                   {
                       btnConverGRN.Visible = true;
                       btnPrint.Visible = true;
                       PageStatus.Value = "edit";
                       Status.SelectedValue = "1";
                       Location.ReadOnly = true;
                       SupplierIdGrn.ReadOnly = true;
                       GRNType.Enabled = false;
                       PurchaseOrder.Enabled = false;
                       PackingSlip.ReadOnly = true;
                       SetGRNChildGrid(Request.Form[PurchaseOrderText.UniqueID]);
                   }
                    
                }
                else
                {
                    if(XBDataProvider.GRN.UpdateGRNDetail(Convert.ToInt32(GRNId.Value), Convert.ToInt32(Request.Form[TotalQty.UniqueID]), Reference.Text, dt))
                    {
                        SetGRNChildGrid(Request.Form[PurchaseOrderText.UniqueID]);
                        GRNType.Enabled = false;
                        PurchaseOrder.Enabled = false;
                        PackingSlip.ReadOnly = true;
                    }
                }
               

            }
            catch (Exception ex)
            {

            }
        }

        protected void BtnConvertGRNClick(object sender, EventArgs e)
        {
            int i = 0;
            int purchaseOrderStatus = 5;
            foreach (GridViewRow row in GRNDetail.Rows)
            {
                TextBox box3 = (TextBox)GRNDetail.Rows[i].Cells[5].FindControl("ReceivedQuantity");
                HiddenField hdn = (HiddenField)GRNDetail.Rows[i].Cells[5].FindControl("prevRecvdQnty");
                if(Convert.ToInt32(box3.Text)!=Convert.ToInt32(hdn.Value))
                {
                    purchaseOrderStatus = 4;
                    break;
                }
                i++;
            }
            if (XBDataProvider.GRN.FinalizeGrn(Convert.ToInt32(GRNId.Value),purchaseOrderStatus))
            {
                Status.SelectedValue = 2.ToString();
                btnConverGRN.Visible = false;
                btnSaveDtl.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "window.print();", true);
            }
        }

        [WebMethod]
        public static List<FirstFreeDetails> GetFirstFreerDetails(string companyCode)
        {
            List<FirstFreeDetails> result = new List<FirstFreeDetails>();
            try
            {
                DataRow row = null;
                DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetGRNSequenceDetails(companyCode);
                for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                {
                    row = dtTableSequenceDetails.Rows[i];
                    FirstFreeDetails firstFreeDetails = new FirstFreeDetails();
                    string sequenceNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                    firstFreeDetails.id = row["ID"].ToString();
                    firstFreeDetails.sequenceNumber = sequenceNo;
                    firstFreeDetails.seqType = row["SeqType"].ToString();
                    firstFreeDetails.orderType = row["OrderType"].ToString() == "Goods Receipt Auto" ? "0" : "1";
                    firstFreeDetails.enterpriseUnitCode = row["EnterpriseUnitCode"].ToString();
                    result.Add(firstFreeDetails);
                }

            }
            catch (Exception e)
            {

            }


            return result;
        }

        protected void GRNTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if(Location.Text!="")
            {
                DataRow row = null;
                DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetGRNSequenceDetails(CompanyCode.Value);
                for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                {
                    row = dtTableSequenceDetails.Rows[i];
                    if (row["SeqType"].ToString() == "1" && row["OrderType"].ToString() == GRNType.SelectedItem.Text && row["EnterpriseUnitCode"].ToString() == Location.Text)
                    {
                        GoodsReceipt.Text = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                        GrnSequenceNoID.Value = row["ID"].ToString();
                    }
                    else if (row["SeqType"].ToString() == "0" && row["OrderType"].ToString() == GRNType.SelectedItem.Text)
                    {
                        GoodsReceipt.Text = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                        GrnSequenceNoID.Value = row["ID"].ToString();
                    }
                }
            }
            
        }
    }
}
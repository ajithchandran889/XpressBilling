using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class ItemMasterEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["CompanyCode"]==null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                getitemgroup();
                getmanufacturer();
                getbaseunit();
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    DataTable itemMasterDetails = XBDataProvider.ItemMaster.GetItemMasterById(id);
                    if (itemMasterDetails.Rows.Count > 0)
                    {
                        SetItemMasterDetails(itemMasterDetails);
                    }
                }
                else
                {
                    ItemMasterId.Value = "0";
                    CreatedUser.Visible = false;
                    CreatedDate.Visible = false;
                    lblDate.Visible = false;
                    lblUser.Visible = false;
                    ddlStatus.Visible = false;
                    lblstatus.Visible = false;
                   
                }
            }
        }
        public void getitemgroup()
        {
            DataTable dtitemgroup = XBDataProvider.ItemMaster.GetAllActiveItemGroup(Session["CompanyCode"].ToString());

                ddlItemGroup.DataSource = dtitemgroup;
                ddlItemGroup.DataValueField = "ItemGroupCode";
                ddlItemGroup.DataTextField = "Name";
                ddlItemGroup.DataBind();
                ListItem item = new ListItem();
                item.Text = "Select ItemGroup";
                item.Value = "0";
                ddlItemGroup.Items.Insert(0, item);
        }
        public void getmanufacturer()
        {
            DataTable dtmanufacturer = XBDataProvider.ItemMaster.GetAllActiveTManufacturer(Session["CompanyCode"].ToString());

            ddlManufacturer.DataSource = dtmanufacturer;
            ddlManufacturer.DataValueField = "ManufacturerCode";
            ddlManufacturer.DataTextField = "Name";
            ddlManufacturer.DataBind();
            ListItem item = new ListItem();
            item.Text = "Select Manufacturer";
            item.Value = "0";
            ddlManufacturer.Items.Insert(0, item);
        }
        public void getbaseunit()
        {
            DataTable dtbaseunit = XBDataProvider.ItemMaster.GetAllActiveTBaseUnit(Session["CompanyCode"].ToString());

            ddlBaseUnit.DataSource = dtbaseunit;
            ddlBaseUnit.DataValueField = "BaseUnitCode";
            ddlBaseUnit.DataTextField = "Name";
            ddlBaseUnit.DataBind();
            ListItem item = new ListItem();
            item.Text = "Select BaseUnit";
            item.Value = "0";
            ddlBaseUnit.Items.Insert(0, item);
        }
        public void SetItemMasterDetails(DataTable itemMasterDetails)
        {
            DataRow row = itemMasterDetails.Rows[0];
            ItemMasterId.Value = row["ID"].ToString();
            ItemCode.Text = row["ItemCode"].ToString();
            ItemCode.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            ItemType.SelectedValue = row["ItemType"].ToString();
            ItemType.Enabled = false;
            SupplierBarcode.Text = row["SupplierBarcode"].ToString();
            CreatedUser.Text = row["CreatedBy"].ToString();
            CreatedUser.ReadOnly = true;
            CreatedDate.Text = Convert.ToDateTime(row["CreatedDate"]).ToString("MM'/'dd'/'yyyy");
            CreatedDate.ReadOnly = true;
            SearchKey.Text = row["SearchKey"].ToString();
            ddlItemGroup.SelectedValue = row["ItemGroupCode"].ToString();
            InventoryValuation.SelectedValue = row["InventoryValuation"].ToString();
            InventoryValuation.Enabled = false;
            ddlManufacturer.SelectedValue = row["ManufacturerCode"].ToString();
            ddlBaseUnit.SelectedValue = row["BaseUnitCode"].ToString();
            ddlBaseUnit.Enabled = false;
            ddlStatus.SelectedValue = row["Status"].ToString();
            MRP.Text = row["MRP"].ToString();
            SafetStock.Text = row["SafetyStock"].ToString();
            ReorderQty.Text = row["ReorderQty"].ToString();
            RetailPrice.Text = row["RetailPrice"].ToString();
            PurchasePrice.Text = row["PurchasePrice"].ToString();
            Cost.Text = row["ItemCost"].ToString();
        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                int msgstatus = 0;
                bool statusflag;
                if (ddlStatus.SelectedValue == "0")
                    statusflag = false;
                else
                    statusflag = true;
                bool status = false;
                if (ItemMasterId.Value != "0" && ItemMasterId.Value != "")
                {
                    status = XBDataProvider.ItemMaster.UpdateItemMaster(ItemMasterId.Value, Name.Text, SupplierBarcode.Text, SearchKey.Text, ddlItemGroup.SelectedValue, ddlManufacturer.SelectedValue, Convert.ToInt32(MRP.Text), Convert.ToInt32(RetailPrice.Text)
                               , Convert.ToInt32(PurchasePrice.Text), Convert.ToInt32(Cost.Text), Convert.ToInt32(InventoryValuation.SelectedValue), Convert.ToInt32(SafetStock.Text), Convert.ToInt32(ReorderQty.Text), User.Identity.Name, statusflag);
                    if (status)
                    {
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = true;
                        failure.Visible = false;
                    }
                    else
                    {
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = false;
                        failure.Visible = true;
                    }
                }
                else
                {
                    msgstatus = XBDataProvider.ItemMaster.SaveItemMaster(Session["CompanyCode"].ToString(), ItemCode.Text, Name.Text, Convert.ToInt32(ItemType.SelectedValue), SupplierBarcode.Text, SearchKey.Text,
                                                                        ddlItemGroup.SelectedValue.ToString(), ddlManufacturer.SelectedValue.ToString(), ddlBaseUnit.SelectedValue.ToString(), Convert.ToInt32(MRP.Text), Convert.ToInt32(RetailPrice.Text)
                                                                        ,Convert.ToInt32(PurchasePrice.Text),Convert.ToInt32(Cost.Text),Convert.ToInt32(InventoryValuation.SelectedValue),Convert.ToInt32(SafetStock.Text),Convert.ToInt32(ReorderQty.Text),User.Identity.Name);
                    ClearInputs(Page.Controls);
                    if (msgstatus == 1)
                    {
                        SaveSuccess.Visible = true;
                        UpdateSuccess.Visible = false;
                        failure.Visible = false;
                        alreadyexist.Visible = false;
                    }
                    else if (msgstatus == -1)
                    {
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = false;
                        failure.Visible = false;
                        alreadyexist.Visible = true;
                    }
                    else
                    {
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = false;
                        failure.Visible = true;
                        alreadyexist.Visible = false;
                    }
                }


            }
            catch (Exception ex)
            {
                SaveSuccess.Visible = false;
                UpdateSuccess.Visible = false;
                failure.Visible = true;
                alreadyexist.Visible = false;
            }
            
        }

        private void ClearInputs(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                else if (ctrl is DropDownList)
                    ((DropDownList)ctrl).ClearSelection();

                ClearInputs(ctrl.Controls);
            }
        }
    }
}
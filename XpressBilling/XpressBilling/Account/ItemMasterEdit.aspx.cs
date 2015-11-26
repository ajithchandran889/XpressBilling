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
                }
            }
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
            ItemGroup.Text = row["ItemGroupCode"].ToString();
            InventoryValuation.SelectedValue = row["InventoryValuation"].ToString();
            InventoryValuation.Enabled = false;
            Manufacturer.Text = row["ManufacturerCode"].ToString();
            BaseUnit.Text = row["BaseUnitCode"].ToString();
            BaseUnit.ReadOnly = true;
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
                bool status = false;
                if (ItemMasterId.Value != "0" && ItemMasterId.Value != "")
                {
                    status = XBDataProvider.ItemMaster.UpdateItemMaster(ItemMasterId.Value, Name.Text, SupplierBarcode.Text, SearchKey.Text, ItemGroup.Text, Manufacturer.Text,Convert.ToInt32(MRP.Text),Convert.ToInt32(RetailPrice.Text)
                               ,Convert.ToInt32(PurchasePrice.Text), Convert.ToInt32(Cost.Text), Convert.ToInt32(InventoryValuation.SelectedValue), Convert.ToInt32(SafetStock.Text),Convert.ToInt32(ReorderQty.Text), User.Identity.Name);
                    if (status)
                    {
                        lblMsg.InnerText = "Successfully updated";
                    }
                    else
                    {
                        lblMsg.InnerText = "Oops..Something went wrong.Please try again";
                    }
                }
                else
                {
                    status = XBDataProvider.ItemMaster.SaveItemMaster(Session["CompanyCode"].ToString(),ItemCode.Text,Name.Text,Convert.ToInt32(ItemType.SelectedValue),SupplierBarcode.Text,SearchKey.Text,
                                                                        ItemGroup.Text,Manufacturer.Text,BaseUnit.Text,Convert.ToInt32(MRP.Text),Convert.ToInt32(RetailPrice.Text)
                                                                        ,Convert.ToInt32(PurchasePrice.Text),Convert.ToInt32(Cost.Text),Convert.ToInt32(InventoryValuation.SelectedValue),Convert.ToInt32(SafetStock.Text),Convert.ToInt32(ReorderQty.Text),User.Identity.Name);
                    if (status)
                    {
                        ClearInputs(Page.Controls);
                        lblMsg.InnerText = "Successfully added";
                    }
                    else
                    {
                        lblMsg.InnerText = "Oops..Something went wrong.Please try again";
                    }

                }


            }
            catch (Exception ex)
            {

            }

            //Label lblMsg = this.Master.FindControl("Message") as Label;
            //lblMsg.Text = "Company added successfully";
            //lblMsg.Visible = true;
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
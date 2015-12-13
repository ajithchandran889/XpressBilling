<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemMasterEdit.aspx.cs" Inherits="XpressBilling.Account.ItemMasterEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                 <div id="SaveSuccess" visible="false" class="alert alert-success" role="alert" runat="server">
                    <span runat="server"><img src="~/Images/like.png" alt="" runat="server" />	</span>
                    Saved Successfully
                </div>
                <div id="UpdateSuccess" visible="false" class="alert alert-success" role="alert" runat="server">
                    <span runat="server"><img src="~/Images/like.png" alt="" runat="server" />	</span>
                    Updated Successfully
                </div>
                <div id="failure" visible="false" class="alert alert-danger" role="alert" runat="server">
                    <span id="failureMessage" runat="server">Sorry,Something went wrong!</span>
                </div>
                <div id="alreadyexist" visible="false" class="alert alert-danger" role="alert" runat="server">
                    <span id="alreadyexistmsg" runat="server">Code Already Exists</span>
                </div>
                <div class="page-header">Item Master Details</div>
                <div class="form-group">
                    <label for="ItemCode" class="control-label col-xs-2 col-md-2">Item</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="ItemCode" name="ItemCode" class="form-control required" placeholder="Item" ClientIDMode="Static"></asp:TextBox>
                    </div><div class="col-xs-10 col-md-2"></div>
                    <label id="lblUser" for="CreatedUser" runat="server" class="control-label col-xs-2 col-md-2">User</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="CreatedUser" class="form-control required" placeholder="User" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>

                </div>
                <div class="form-group">
                    <label for="Name" class="control-label col-xs-2 col-md-2">Name</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Name" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                        
                    </div><div class="col-xs-10 col-md-2"></div>
                    <label id="lblDate" for="Date" runat="server" class="control-label col-xs-2 col-md-2">Date</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="CreatedDate" class="form-control required" placeholder="Date" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                </div>
                 <div class="form-group">
                    <label for="ItemType" class="control-label col-xs-2 col-md-2">Item type</label>
                    <div class="col-xs-10 col-md-2">
                       <asp:DropDownList runat="server" ID="ItemType" class="form-control required" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Cost"></asp:ListItem>
                           <asp:ListItem Value="1" Text="Purchase"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                     <div class="col-xs-10 col-md-2"></div>
                    <label for="Status" runat="server" id="lblstatus" class="control-label col-xs-2 col-md-2">Status</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control required" ID="ddlStatus" ClientIDMode="Static">
                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="SupplierBarcode" class="control-label col-xs-2 col-md-2">Supplier Barcode</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="SupplierBarcode" class="form-control" placeholder="Supplier Barcode" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                </div>
                <div class="form-group">
                    <label for="SearchKey" class="control-label col-xs-2 col-md-2">Search Key</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="SearchKey" class="form-control" placeholder="Search Key" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                </div>
                <div class="form-group">
                   
                </div>
                <div class="form-group">
                    <label  class="control-label col-xs-2 col-md-2">Attributes</label>
                    <label class="control-label col-xs-2 col-md-2"></label><label class="control-label col-xs-2 col-md-2"></label>
                    <label  class="control-label col-xs-2 col-md-2">General Information</label>                   
                </div>
                <div class="form-group">
                   
                </div>
                <div class="form-group">

                    <label for="ItemGroup" class="control-label col-xs-2 col-md-2">Item Group</label>
                    <div class="col-xs-10 col-md-2">
                        <%--<asp:TextBox runat="server" ID="ItemGroup" class="form-control required" placeholder="Item Group" ClientIDMode="Static"></asp:TextBox>--%>
                        <asp:DropDownList runat="server" class="form-control required" ID="ddlItemGroup" ClientIDMode="Static">                            
                        </asp:DropDownList>
                    </div><div class="col-xs-10 col-md-2"></div>
                    <label for="InventoryValuation" class="control-label col-xs-2 col-md-2">Inventory Valuation</label>                    
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" ID="InventoryValuation" class="form-control required" ClientIDMode="Static" Enabled="false">
                            <asp:ListItem Value="0" Text="LIFO"></asp:ListItem>
                            <asp:ListItem Value="1" Text="FIFO" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="MAUC"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    
                </div>
                <div class="form-group">
                    <label for="Manufacturer" class="control-label col-xs-2 col-md-2">Manufacturer</label>
                    <div class="col-xs-10 col-md-2">
                        <%--<asp:TextBox runat="server" ID="Manufacturer" class="form-control required" placeholder="Manufacturer" ClientIDMode="Static"></asp:TextBox>--%>
                       <asp:DropDownList runat="server" class="form-control required" ID="ddlManufacturer" ClientIDMode="Static">                            
                        </asp:DropDownList>
                    </div> 
                    <div class="col-xs-10 col-md-2"></div>
                    <label for="SafetStock" class="control-label col-xs-2 col-md-2">Safety Stock</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="SafetStock" class="form-control required txtNumeric" placeholder="Safet Stock" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                </div>

                <div class="form-group">
                    <label for="BaseUnit" class="control-label col-xs-2 col-md-2">Base Unit</label>
                    <div class="col-xs-10 col-md-2">
                        <%--<asp:TextBox runat="server" ID="BaseUnit" class="form-control required" placeholder="Base Unit" ClientIDMode="Static"></asp:TextBox>--%>
                        <asp:DropDownList runat="server" class="form-control required" ID="ddlBaseUnit" ClientIDMode="Static">                            
                        </asp:DropDownList>
                    </div>
                     <div class="col-xs-10 col-md-2"></div>
                    <label for="ReorderQty" class="control-label col-xs-2 col-md-2">Reorder Quantity</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="ReorderQty"  class="form-control required txtNumeric" placeholder="Reorder Qty" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                </div>

                <div class="form-group">
                    <label for="MRP" class="control-label col-xs-2 col-md-2">MRP</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="MRP" class="form-control required txtNumeric" placeholder="MRP" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label for="RetailPrice" class="control-label col-xs-2 col-md-2">Retail Price</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="RetailPrice" class="form-control required txtNumeric" placeholder="Retail Price" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label for="PurchasePrice" class="control-label col-xs-2 col-md-2">Purchase Price</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="PurchasePrice" class="form-control required txtNumeric" placeholder="PurchasePrice" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                 <div class="form-group">
                    <label for="Cost" class="control-label col-xs-2 col-md-2">Cost</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Cost" class="form-control required txtNumeric" placeholder="Cost" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:HiddenField ID="ItemMasterId" runat="server" />
                    <div class="col-xs-10 col-md-8">
                       <a href="/Account/ItemMaster.aspx" class="btn btn-primary pull-left">Cancel</a> <asp:Button ID="saveItemMaster" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="SaveClick" />
                </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemMaster.aspx.cs" Inherits="XpressBilling.Account.ItemMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Item Master
                </div>
                 <div class="col-xs-10 col-md-8" runat="server" id="filterArea">
                        <div class="form-group">  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">ItemCode</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="ItemCodeSearch" class="form-control" placeholder="ItemCode" ClientIDMode="Static" onkeyup="SearchGrid('ItemCodeSearch', 'ListItemMaster')"></asp:TextBox>
                            </div>  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">Name</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="NameSearch" class="form-control" placeholder="Name" ClientIDMode="Static" onkeyup="SearchGrid('NameSearch', 'ListItemMaster')"></asp:TextBox>
                            </div>  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">ItemGroupCode</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="ItemGroupCodeSearch" class="form-control" placeholder="IGroupCode" ClientIDMode="Static" onkeyup="SearchGrid('ItemGroupCodeSearch', 'ListItemMaster')"></asp:TextBox>
                            </div>                                                                                                  
                        </div>                       
                    </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right"> 
              <span class="icon-wrap pull-left"> <a href="ItemMasterEdit"><i class="glyphicon glyphicon-plus" style="color:white;"></i></a></span>
               <span class="icon-wrap pull-left"> <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server" ><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton> </span>
                    </div>
                    <asp:GridView ID="ListItemMaster" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="ItemMasterPageIndexChanging" PageSize="20" AutoGenerateColumns="false" EmptyDataText="There are no records" OnDataBound="ItemMasterModeDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="ItemMasterEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                            <asp:BoundField DataField="SupplierBarcode" HeaderText="Supplier Barcode"></asp:BoundField>
                            <asp:BoundField DataField="BaseUnitCode" HeaderText="Unit Of Measure"></asp:BoundField>
                            <asp:TemplateField HeaderText="Item Type">
                                <ItemTemplate><%# Eval("ItemType").ToString()=="0" ? "Cost" : "Purchase" %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ItemGroupCode" HeaderText="Item Group"></asp:BoundField>
                            <asp:BoundField DataField="MRP" HeaderText="MRP"></asp:BoundField>
                            <asp:BoundField DataField="RetailPrice" HeaderText="Retail Price"></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("Status").ToString()=="0" ? "InActive":"Active" %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>  
                                <ItemTemplate>  
                                    <asp:CheckBox ID="chkDel"  runat="server" />  
                                    <asp:HiddenField ID="selectedId" runat="server" Value='<%# Bind("ID") %>' />
                                </ItemTemplate>  
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
        </div>
</asp:Content>

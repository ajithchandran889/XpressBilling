<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemMaster.aspx.cs" Inherits="XpressBilling.Account.ItemMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Item Master
                <div class="pull-right">
                    <span class="icon-wrap pull-left"> <a href="ItemMasterEdit"><i class="glyphicon glyphicon-plus "></i></a></span>
                    <span class="icon-wrap pull-left"><asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server" ><i class="glyphicon glyphicon-trash"></i></asp:LinkButton> </span>
                </div>
                </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2>List View</h2>
                    </div>
                    <asp:GridView ID="ListItemMaster" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="ItemMasterPageIndexChanging" PageSize="3" AutoGenerateColumns="false" OnDataBound="ItemMasterModeDataBound">
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
                                <ItemTemplate>
                                   <asp:dropdownlist id="ItemMasterDdl" IdItemMaster='<%# Eval("ID") %>' AutoPostBack="true" runat="server" OnSelectedIndexChanged="ItemMasterDdlSelectedIndexChanged">
                                        <asp:listitem value="1" text="active"></asp:listitem>
                                        <asp:listitem value="0" text="inactive"></asp:listitem>
                                    </asp:dropdownlist>
                                    <asp:HiddenField ID="selectedvalue" runat="server" Value='<%# Bind("Status") %>' />
                                </ItemTemplate>
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
</asp:Content>

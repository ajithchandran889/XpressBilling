<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PriceBookEdit.aspx.cs" Inherits="XpressBilling.Account.PriceBookEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <div class="page-header">Add Price Book Details</div>
                <div class="form-group">
                    <div class="form-group">
                        <label class="control-label col-xs-12 col-sm-4 col-md-2">Type</label>
                        <div class="col-xs-12 col-sm-8 col-md-2">
                            <asp:DropDownList runat="server" ID="Type" class="form-control required" ClientIDMode="Static">
                                <asp:ListItem Value="0" Text="Cost"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Purchase"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <label class="control-label col-xs-12 col-sm-4 col-md-2">Price Book</label>
                        <div class="col-xs-12 col-sm-8 col-md-2">
                            <asp:TextBox runat="server" ID="PriceBookDocNo" class="form-control" ReadOnly="true" placeholder="Price Book" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-xs-12 col-sm-4 col-md-2">Order Type</label>
                        <div class="col-xs-12 col-sm-8 col-md-2">
                            <asp:DropDownList runat="server" ID="OrderType_0" class="form-control required" ClientIDMode="Static">
                                <asp:ListItem Value="0" Text="Local"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Import"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList runat="server" ID="OrderType_1" class="form-control required" Style="display: none" ClientIDMode="Static">
                                <asp:ListItem Value="0" Text="Cash"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Credit"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <label id="lblUser" runat="server" class="control-label col-xs-12 col-sm-4 col-md-2">Date</label>
                        <div class="col-xs-12 col-sm-8 col-md-2">
                            <asp:TextBox runat="server" ID="CreatedDate" class="form-control required" ReadOnly="true" placeholder="Date" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-xs-12 col-sm-4 col-md-2">Currency</label>
                        <div class="col-xs-12 col-sm-8 col-md-2">
                            <asp:TextBox runat="server" ID="Currency" ReadOnly="true" class="form-control required" placeholder="Currrency" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <label id="lblDate" runat="server" class="control-label col-xs-12 col-sm-4 col-md-2">User</label>
                        <div class="col-xs-12 col-sm-8 col-md-2">
                            <asp:TextBox runat="server" ID="CreatedUser" ReadOnly="true" class="form-control required" placeholder="User" ClientIDMode="Static"></asp:TextBox>
                        </div>

                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-10 col-md-8">
                        <asp:HiddenField ID="PriceBookId" runat="server" />
                        <asp:HiddenField ID="LastDocumentNumber" runat="server" />
                        <asp:Button ID="savePriceBook" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right" Text="Save" OnClick="savePriceBookClick" />
                        <a id="cancelPriceBook" href="/Account/PriceBook.aspx" runat="server" class="btn btn-primary pull-right">Cancel</a>
                    </div>
                </div>
            </div>
            <asp:Label ID="lblSearch" runat="server">Search:</asp:Label>
            &nbsp;&nbsp;
            <asp:TextBox ID="txtSearch" runat="server" CssClass="txt"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="BtnSearchClick" />
            <asp:GridView ID="PriceBookDetail" runat="server" ShowFooter="true" AutoGenerateColumns="false" DataKeyNames="ID">
                <Columns>
                    <asp:TemplateField HeaderText="Item">
                        <ItemTemplate>
                            <asp:TextBox ID="ItemCode" class="form-control ItemCode" ClientIDMode="Static" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supplier Barcode">
                        <ItemTemplate>
                            <asp:TextBox ID="SupplierBarcode" class="form-control SupplierBarcode" ClientIDMode="Static" runat="server" Text='<%# Bind("SupplierBarcode") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                         <asp:TextBox ID="Description" class="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Currency">
                        <ItemTemplate>
                            <asp:TextBox ID="CurrencyCode" class="form-control CurrencyCode" ClientIDMode="Static" runat="server" Text='<%# Bind("CurrencyCode") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MRP">
                        <ItemTemplate>
                            <asp:TextBox ID="MRP" class="form-control" ClientIDMode="Static" runat="server" Text='<%# Bind("MRP") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price">
                        <ItemTemplate>
                            <asp:TextBox ID="Price" class="form-control" ClientIDMode="Static" runat="server" Text='<%# Bind("Price") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div class="form-group">
                <div class="col-xs-10 col-md-8">
                    <asp:Button ID="SaveDetail" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right" Text="Save" OnClick="savePriceBookClickDetails" />
                    <a id="CancelPriceBook2" href="/Account/PriceBook.aspx" runat="server" class="btn btn-primary pull-right">Cancel</a>
                </div>
            </div>
            <br />
            <div style="display: none;">
                <asp:Button ID="ButtonAdd" ClientIDMode="Static" runat="server" OnClick="ButtonAddClick" Text="Add New Row" />
            </div>
        </div>
    </div>
</asp:Content>

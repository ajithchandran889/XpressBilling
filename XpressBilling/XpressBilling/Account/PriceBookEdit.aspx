﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PriceBookEdit.aspx.cs" Inherits="XpressBilling.Account.PriceBookEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <div id="SaveSuccess" visible="false" class="alert alert-success" role="alert" runat="server">
                    <span runat="server"><img src="~/Images/like.png" alt="" runat="server" />	</span>
                    Saved Successfully
                </div>
                <div id="failure" visible="false" class="alert alert-danger" role="alert" runat="server">
                    <span id="failureMessage" runat="server">Sorry,Something went wrong!</span>
                </div>
                <div class="page-header">Price Book Details</div>
                <div class="form-group">
                    <div class="form-group">
                        <label class="control-label col-xs-12 col-sm-4 col-md-2">Type</label>
                        <div class="col-xs-12 col-sm-8 col-md-2">
                            <asp:DropDownList runat="server" ID="Type" class="form-control required" ClientIDMode="Static">
                                <asp:ListItem Value="0" Text="Sales"></asp:ListItem>
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
                                <asp:ListItem Value="0" Text="Cash"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Credit"></asp:ListItem>
                            </asp:DropDownList>
                             <asp:DropDownList runat="server" ID="OrderType_1" class="form-control required" Style="display: none" ClientIDMode="Static">
                                <asp:ListItem Value="0" Text="Local"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Import"></asp:ListItem>
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
                            <%--<asp:TextBox runat="server" ID="Currency" ReadOnly="true" class="form-control" placeholder="Currrency" ClientIDMode="Static"></asp:TextBox>--%>
                        <asp:DropDownList runat="server" class="form-control required" ID="ddlCurrency" ClientIDMode="Static">
                        </asp:DropDownList>
                        </div>
                        <label id="lblDate" runat="server" class="control-label col-xs-12 col-sm-4 col-md-2">User</label>
                        <div class="col-xs-12 col-sm-8 col-md-2">
                            <asp:TextBox runat="server" ID="CreatedUser" ReadOnly="true" class="form-control required" placeholder="User" ClientIDMode="Static"></asp:TextBox>
                        </div>

                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-10 col-md-8">
                        <asp:HiddenField ID="PageStatus" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="rowCount" runat="server" ClientIDMode="Static" Value="1" />
                        <asp:HiddenField ID="PriceBookId" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="LastDocumentNumber" runat="server" />
                        <a id="cancelPriceBook" href="/Account/PriceBook.aspx" runat="server" class="btn btn-primary pull-left">Cancel</a><asp:Button ID="savePriceBook" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="savePriceBookClick" />
                        
                    </div>
                </div>
                <hr>
            </div>
           
            <%--<div class="form-group">--%>
                    <div class="col-xs-10 col-md-8">
                         
            <%--<asp:Label ID="lblSearch" runat="server">Search:</asp:Label>
            &nbsp;&nbsp;--%>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="txt"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="BtnSearchClick" />
                        </div>
            <asp:Panel runat="server" ID="gridDetails">
                <div class="col-xs-12 col-sm-12 col-md-12">
            <div class="grid_wrapper">
                
                    <h2  class="page-header">Transaction</h2>
                    
               
                <asp:GridView ID="PriceBookDetail"  class="table table-fix" ClientIDMode="Static" runat="server" ShowFooter="true" AutoGenerateColumns="false" DataKeyNames="ID"
                    OnRowDataBound="PriceBookDetailRowDataBound">
                     <RowStyle CssClass="Odd" />
                     <AlternatingRowStyle CssClass="Even" />
                    <Columns><%--<asp:TemplateField HeaderText="">
                            <ItemTemplate>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                               
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="No:">
                                    <ItemTemplate>
                                        <asp:Label ID="indexIcrement" runat="server" Text='<%# Container.DataItemIndex + 1 %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item">
                            <ItemTemplate>
                                <asp:TextBox ID="ItemCode" class="form-control ItemCode required" ClientIDMode="Static" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supplier Barcode">
                            <ItemTemplate>
                                <asp:TextBox ID="SupplierBarcode" class="form-control SupplierBarcode required" ClientIDMode="Static" runat="server" Text='<%# Bind("SupplierBarcode") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                             <asp:TextBox ID="Description" class="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Currency">
                            <ItemTemplate>
                                <asp:TextBox ID="CurrencyCode" class="form-control CurrencyCode required" ClientIDMode="Static" runat="server" Text='<%# Bind("CurrencyCode") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MRP">
                            <ItemTemplate>
                                <asp:TextBox ID="MRP" class="form-control required" ClientIDMode="Static" runat="server" Text='<%#Eval("MRP","{0:n}")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:TextBox ID="Price" class="form-control priceBookPricetxt required" ClientIDMode="Static" runat="server" Text='<%#Eval("Price","{0:n}")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
                    </div>
                </asp:Panel>
            <div class="form-group">
                <div class="col-xs-10 col-md-8">
                    <a id="CancelPriceBook2" href="/Account/PriceBook.aspx" runat="server" class="btn btn-primary pull-left">Cancel</a>
                    <asp:Button ID="SaveDetail" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="savePriceBookClickDetails" />
                </div>
            </div>
            <br />
            <div style="display: none;">
                <asp:Button ID="ButtonAdd" ClientIDMode="Static" runat="server" OnClick="ButtonAddClick" Text="Add New Row" />
            </div>
    </div>
    </div>
    <asp:HiddenField ID="CompanyCode" runat="server" ClientIDMode="Static" />
</asp:Content>

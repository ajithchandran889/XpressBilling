﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalesQuotation.aspx.cs" Inherits="XpressBilling.Account.SalesQuotation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                     <span style="float:left;">
                    Sales Quotation               </span>
                    <span style="float:right;">
                    Filtering    
                    <input type="checkbox" id="chkFilter" title="Search" onclick="javascript:fnShowSearch(this);" /></span>
                    <div style="clear:both;"></div>   
                </div>
                <div class="col-xs-10 col-md-12" runat="server" id="filterArea" style="display:none;">
                        <div class="form-group">  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">SalesQuotationNo</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="SalesQuotationNoSearch" class="form-control" placeholder="SQ No" ClientIDMode="Static" onkeyup="SearchGrid('SalesQuotationNoSearch', 'ListSalesQuotation')"></asp:TextBox>
                            </div>                              
                            <label class="control-label col-xs-12 col-sm-4 col-md-1">SalesMan</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="SalesManNameSearch" class="form-control" placeholder="SalesMan" ClientIDMode="Static" onkeyup="SearchGrid('SalesManNameSearch', 'ListSalesQuotation')"></asp:TextBox>
                            </div>  
                                                                                                
                        </div>                       
                    </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right">
                            <span class="icon-wrap pull-left"><a href="SQEdit"><i class="glyphicon glyphicon-plus" style="color: white;"></i></a></span>
                            <%--<span class="icon-wrap pull-left">
                                <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server"><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton>
                            </span>--%>
                        </div>
                    </div>
                    <asp:GridView ID="ListSalesQuotation" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="SalesQuotationPageIndexChanging" PageSize="20" AutoGenerateColumns="false" EmptyDataText="There are no Records">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="SQEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="SalesQuotationNo" HeaderText="Sales Quotation"></asp:BoundField>
                            <asp:BoundField DataField="SalesManName" HeaderText="Sales Man"></asp:BoundField>
                            <asp:BoundField DataField="SalesQuotationDate" HeaderText="Sales Quotation Date"  DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                            <asp:TemplateField HeaderText="Order Type">
                                <ItemTemplate><%# Eval("OrderType").ToString()=="0" ? "Cash" : "Credit" %></ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="OrderType">
                                <ItemTemplate><%# Eval("SeqType").ToString()=="0" ? (Eval("OrderType").ToString()=="0" ? "Local":"Import"):(Eval("OrderType").ToString()=="0" ? "Cash":"Credit") %></ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="CreatedBy" HeaderText="User"></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("Status").ToString()=="0" ? "Free" :(Eval("Status").ToString()=="1"?"Open":"Finalized")  %></ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField>  
                                <ItemTemplate>  
                                    <asp:CheckBox ID="chkDel"  runat="server" />  
                                    <asp:HiddenField ID="selectedId" runat="server" Value='<%# Bind("ID") %>' />
                                </ItemTemplate>  
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

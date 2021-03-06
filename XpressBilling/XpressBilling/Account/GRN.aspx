﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GRN.aspx.cs" Inherits="XpressBilling.Account.GRN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                   <span style="float:left;">
                    GRN               </span>
                    <span style="float:right;">
                    Filtering    
                    <input type="checkbox" id="chkFilter" title="Search" onclick="javascript:fnShowSearch(this);" /></span>
                    <div style="clear:both;"></div>   
                </div>
                <div class="col-xs-10 col-md-12" runat="server" id="filterArea" style="display:none;">
                        <div class="form-group">  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">GRN</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="GoodsReceiptNoSearch" class="form-control" placeholder="GRN" ClientIDMode="Static" onkeyup="SearchGrid('GoodsReceiptNoSearch', 'ListGRN')"></asp:TextBox>
                            </div>  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">GR Date</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="GoodsReceiptDateSearch" class="form-control" placeholder="GR Date" ClientIDMode="Static" onkeyup="SearchGrid('GoodsReceiptDateSearch', 'ListGRN')"></asp:TextBox>
                            </div>  
                             <label class="control-label col-xs-12 col-sm-4 col-md-2">BPCode</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="BPCodeSearch" class="form-control" placeholder="BPCode" ClientIDMode="Static" onkeyup="SearchGrid('BPCodeSearch', 'ListGRN')"></asp:TextBox>
                            </div>  
                                                           
                        </div>                       
                    </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right">
                            <span class="icon-wrap pull-left"><a href="GrnEdit"><i class="glyphicon glyphicon-plus" style="color: white;"></i></a></span>
                            <%--<span class="icon-wrap pull-left">
                                <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server"><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton>
                            </span>--%>
                        </div>
                    </div>
                    <asp:GridView ID="ListGRN" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="GRNPageIndexChanging" PageSize="20" AutoGenerateColumns="false" EmptyDataText="There are no Records">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="GrnEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="GoodsReceiptNo" HeaderText="Goods Receipt"></asp:BoundField>
                            <asp:BoundField DataField="GoodsReceiptDate" HeaderText="Date"  DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                            <asp:BoundField DataField="LocationName" HeaderText="Location"></asp:BoundField>
                            <asp:BoundField DataField="BpName" HeaderText="Supplier"></asp:BoundField>
                            <asp:BoundField DataField="Reference" HeaderText="Reference"></asp:BoundField>
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

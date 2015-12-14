﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BussinessPartner.aspx.cs" Inherits="XpressBilling.Account.BussinessPartner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Bussiness Partner       
                </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right">
                            <span class="icon-wrap pull-left"><a href="BussinessPartnerEdit"><i class="glyphicon glyphicon-plus" style="color: white;"></i></a></span>
                            <span class="icon-wrap pull-left">
                                <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server"><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton>
                            </span>
                        </div>
                    </div>
                    <asp:GridView ID="ListBussnessPartner" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="BussinessPartnerPageIndexChanging" PageSize="20" AutoGenerateColumns="false" OnDataBound="listBussnessPartnerDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="BussinessPartnerEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="BusinessPartnerCode" HeaderText="Bussiness Partner"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate><%# Eval("BusinessPartnerType").ToString()=="0" ? "Supplier" : "Customer" %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person"></asp:BoundField>
                            <asp:BoundField DataField="Mobile" HeaderText="Mobile"></asp:BoundField>
                            <asp:BoundField DataField="City" HeaderText="City"></asp:BoundField>
                            <asp:BoundField DataField="Country" HeaderText="Country"></asp:BoundField>
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
</asp:Content>

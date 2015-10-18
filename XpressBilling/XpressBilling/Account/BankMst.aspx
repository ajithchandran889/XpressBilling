﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BankMst.aspx.cs" Inherits="XpressBilling.Account.BankMst" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Bank Master               
                </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2>List View</h2>
                        <div class="pull-right"> 
              <span class="icon-wrap pull-left"> <a href="EditBankMst"><i class="glyphicon glyphicon-plus "></i></a></span>
               <span class="icon-wrap pull-left"> <i class="glyphicon glyphicon-trash"></i></span>
              </div>
                    </div>
                    <asp:GridView ID="listBankMst" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="listBankMstPageIndexChanging" PageSize="2" AutoGenerateColumns="false">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="EditBankMst?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                           <asp:BoundField DataField="BankCode" HeaderText="BankCode" Visible="false"></asp:BoundField>
                             <asp:BoundField DataField="AccountNo" HeaderText="Account Number"></asp:BoundField>
                            <asp:BoundField DataField="AccountType" HeaderText="Account Type"></asp:BoundField>   
                            <asp:BoundField DataField="BankName" HeaderText="BANK"></asp:BoundField>                          
                            <asp:BoundField DataField="Branch" HeaderText="Branch"></asp:BoundField>
                            <asp:BoundField DataField="IBAN" HeaderText="IBAN"></asp:BoundField>
                             <asp:BoundField DataField="IFSC" HeaderText="IFSC"></asp:BoundField>                           
                            <asp:BoundField DataField="SWIFT" HeaderText="SWIFT"></asp:BoundField>
                            <asp:BoundField DataField="MICR" HeaderText="MICR"></asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
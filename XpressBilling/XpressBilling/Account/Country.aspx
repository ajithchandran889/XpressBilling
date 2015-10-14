<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Country.aspx.cs" Inherits="XpressBilling.Account.Country" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Country
                <div class="pull-right"><a href="CountryEdit">ADD</a></div>
                </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2>List View</h2>
                        <div class="pull-right"> 
              <span class="icon-wrap pull-left"> <a href="CountryEdit"><i class="glyphicon glyphicon-plus "></i></a></span>
               <span class="icon-wrap pull-left"> <i class="glyphicon glyphicon-trash"></i></span>
              </div>
                    </div>
                    <asp:GridView ID="listCurrency" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="listCurrencyPageIndexChanging" PageSize="2" AutoGenerateColumns="false">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="CountryEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="CountryCode" HeaderText="Country"></asp:BoundField>                            
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                            <asp:BoundField DataField="Reference" HeaderText="User"></asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

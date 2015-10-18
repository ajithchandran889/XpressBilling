<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="Currency.aspx.cs" Inherits="XpressBilling.Account.Currency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Currency
                <div class="pull-right">
                    <span class="icon-wrap pull-left"> <a href="CurrencyEdit"><i class="glyphicon glyphicon-plus "></i></a></span>
                </div>
                </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2>List View</h2>
                    </div>
                    <asp:GridView ID="listCurrency" runat="server" CssClass="table" AllowPaging="true"  OnDataBound="CurrencyUserDataBound"
                        OnPageIndexChanging="listCurrencyPageIndexChanging" PageSize="2" AutoGenerateColumns="false">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="CurrencyEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="CompanyCode" HeaderText="Company" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="CurrencyCode" HeaderText="Currency"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                            <asp:BoundField DataField="Decimal" HeaderText="Decimal"></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                   <%-- <asp:Label runat="server" Text='<%# Convert.ToBoolean(Eval("Status"))?"Active":"InActive" %>'></asp:Label>--%>
                                    <asp:dropdownlist id="CurrencyStatusDdl" currencyId='<%# Eval("ID") %>' AutoPostBack="true" runat="server" OnSelectedIndexChanged="CurrencyStatusDdlSelectedIndexChanged">
                                        <asp:listitem value="1" text="active"></asp:listitem>
                                        <asp:listitem value="0" text="inactive"></asp:listitem>
                                    </asp:dropdownlist>
                                    <asp:HiddenField ID="selectedvalue" runat="server" Value='<%# Bind("Status") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="XpressBilling.Account.Company" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
          <div class="page-content">
       <div class="row content-holder">
          <div class="col-sm-12 col-md-12">
            <div class="page-header">Company
                <div class="pull-right"><a href="CompanyEdit">ADD</a></div>
            </div>
            <div class="grid_wrapper">
              <div class="grid_header">
                <h2>List View</h2>
              </div>
              <asp:GridView ID="listCompany" runat="server" CssClass="table" AllowPaging="true" 
                  OnPageIndexChanging="listCompanyPageIndexChanging" PageSize="2" AutoGenerateColumns="false">
                  <PagerStyle HorizontalAlign="Right" />
                   <columns>
                        <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="CompanyEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil"/>
                        <asp:boundfield DataField="CompanyCode" HeaderText="Company"></asp:boundfield>
                        <asp:boundfield DataField="Name" HeaderText="Name"></asp:boundfield>
                        <asp:boundfield DataField="ContactPerson" HeaderText="Contact Person"></asp:boundfield>
                        <asp:boundfield DataField="Mobile" HeaderText="Mobile"></asp:boundfield>
                        <asp:boundfield DataField="City" HeaderText="City"></asp:boundfield>
                        <asp:boundfield DataField="Country" HeaderText="Country"></asp:boundfield>
                        <asp:boundfield DataField="ZipCode" HeaderText="Zip/Postal Code" ItemStyle-Width="80"></asp:boundfield>
                        <asp:boundfield DataField="Email" HeaderText="Email" ItemStyle-Width="150"></asp:boundfield></columns>
              </asp:GridView>
            </div>
            
            
          </div>
        </div>
      </div>
</asp:Content>

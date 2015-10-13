<%@ Page Title=""  Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Location.aspx.cs" Inherits="XpressBilling.Account.Location" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
          <div class="page-content">
       <div class="row content-holder">
          <div class="col-sm-12 col-md-12">
            <div class="page-header">Location
                <%--<div class="pull-right"><a href="LocationEdit">ADD</a></div>--%>
            </div>
            <div class="grid_wrapper">
              <div class="grid_header">
                <h2 class="pull-left">List View</h2><div class="pull-right">
              <span class="icon-wrap pull-left"> <a href="LocationEdit"><i class="glyphicon glyphicon-plus "></i></a></span>
               <span class="icon-wrap pull-left"> <i class="glyphicon glyphicon-trash"></i></span>
              </div>
              </div>
              <asp:GridView ID="listLocation" runat="server" CssClass="table" CellPadding="0" CellSpacing="0" AllowPaging="true" 
                  OnPageIndexChanging="listLocationPageIndexChanging" PageSize="2" AutoGenerateColumns="false" EmptyDataText="There are no Locations">
                  <PagerStyle HorizontalAlign="Right" />
                   <columns>
                        <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="LocationEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil"/>
                        <asp:boundfield DataField="LocationCode" HeaderText="Location"></asp:boundfield>
                        <asp:boundfield DataField="Name" HeaderText="Name"></asp:boundfield>
                        <asp:boundfield DataField="ContactCode" HeaderText="Contact Person"></asp:boundfield>
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

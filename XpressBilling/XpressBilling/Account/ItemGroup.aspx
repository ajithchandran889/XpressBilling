<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemGroup.aspx.cs" Inherits="XpressBilling.Account.ItemGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Item Group              
                </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2>List View</h2>
                        <div class="pull-right"> 
              <span class="icon-wrap pull-left"> <a href="EditItemGroup"><i class="glyphicon glyphicon-plus "></i></a></span>
               <span class="icon-wrap pull-left"> <i class="glyphicon glyphicon-trash"></i></span>
              </div>
                    </div>
                    <asp:GridView ID="listItemGroup" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="listItemGroupPageIndexChanging" PageSize="2" AutoGenerateColumns="false">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="EditItemGroup?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="ItemGroupCode" HeaderText="Tax"></asp:BoundField>                            
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>  
                            <asp:BoundField DataField="TaxCodeName" HeaderText="TaxCode"></asp:BoundField> 
                            <asp:BoundField DataField="CessCode" HeaderText="CessCode"></asp:BoundField> 
                            <asp:BoundField DataField="CreatedBy" HeaderText="User"></asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="XpressBilling.Account.Invoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
           <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Sales Invoice               
                </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right">
                            <span class="icon-wrap pull-left"><a href="InvoiceEdit"><i class="glyphicon glyphicon-plus" style="color: white;"></i></a></span>
                            <span class="icon-wrap pull-left">
                                <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server"><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton>
                            </span>
                        </div>
                    </div>
                    <asp:GridView ID="ListInvoice" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="InvoicePageIndexChanging" PageSize="20" AutoGenerateColumns="false"  EmptyDataText="There are no Records">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="InvoiceEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="SalesOrderNo" HeaderText="Invoice"></asp:BoundField>
                            <asp:BoundField DataField="SalesOrderDate" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                            <asp:BoundField DataField="LocationCode" HeaderText="Location"></asp:BoundField>
                            <asp:BoundField DataField="Customer" HeaderText="Customer"></asp:BoundField>
                            <asp:BoundField DataField="BPTelephone" HeaderText="Telephone"></asp:BoundField>
                            <asp:BoundField DataField="OrderAmount" HeaderText="Amount" DataFormatString="{0:n}"></asp:BoundField>
                            <asp:BoundField DataField="Reference" HeaderText="Reference"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Sales Man"></asp:BoundField>
                             <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("Status").ToString()=="0" ? "Free" :(Eval("Status").ToString()=="1"?"Open":"Finalized") %></ItemTemplate>
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

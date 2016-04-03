<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Receipt.aspx.cs" Inherits="XpressBilling.Account.Receipt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
           <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Receipt            
                </div>
                
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right">
                            <span class="icon-wrap pull-left"><a href="ReceiptEdit"><i class="glyphicon glyphicon-plus" style="color: white;"></i></a></span>
                            <span class="icon-wrap pull-left">
                                <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server"><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton>
                            </span>
                        </div>
                    </div>
                    <asp:GridView ID="ListReceipt" runat="server" CssClass="table" AllowPaging="true" OnRowDataBound="ListReceiptRowDataBound"
                        OnPageIndexChanging="ReceiptPageIndexChanging" PageSize="20" AutoGenerateColumns="false"  EmptyDataText="There are no Records">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="ReceiptEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="DocumentNo" HeaderText="Receipt"></asp:BoundField>
                            <asp:BoundField DataField="DocumentDate" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                            <asp:TemplateField HeaderText="Transaction Type">
                                <ItemTemplate><%# Eval("TransactionType").ToString()=="0" ? "Receipt Against Invoice" :(Eval("TransactionType").ToString()=="1"?"Advance Receipt":(Eval("TransactionType").ToString()=="2"?"Advance Allocation":"Credit Note")) %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="BusinessPartnerName" HeaderText="Business Partner"></asp:BoundField>
                            <asp:BoundField DataField="Location" HeaderText="Location"></asp:BoundField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="Amount" Text='<%# Bind("Amount") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Reference" HeaderText="Reference"></asp:BoundField>
                            <asp:BoundField DataField="Cashier" HeaderText="Cashier"></asp:BoundField>
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
                    <asp:HiddenField ID="currencyDecimal" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrder.aspx.cs" Inherits="XpressBilling.Account.PurchaseOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Purchase Order              
                </div>
                 <div class="col-xs-10 col-md-8" runat="server" id="filterArea">
                        <div class="form-group">  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">PurchaseOrderNo</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="PurchaseOrderNoSearch" class="form-control" placeholder="PO No" ClientIDMode="Static" onkeyup="SearchGrid('PurchaseOrderNoSearch', 'ListPurchaseOrder')"></asp:TextBox>
                            </div>                              
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">Buyer</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="BuyerSearch" class="form-control" placeholder="Buyer" ClientIDMode="Static" onkeyup="SearchGrid('BuyerSearch', 'ListPurchaseOrder')"></asp:TextBox>
                            </div>  
                             <label class="control-label col-xs-12 col-sm-4 col-md-2">Telephone</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="TelephoneSearch" class="form-control" placeholder="Telephone" ClientIDMode="Static" onkeyup="SearchGrid('TelephoneSearch', 'ListPurchaseOrder')"></asp:TextBox>
                            </div>                                                                     
                        </div>                       
                    </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right">
                            <span class="icon-wrap pull-left"><a href="PurchaseOrderEdit"><i class="glyphicon glyphicon-plus" style="color: white;"></i></a></span>
                            <span class="icon-wrap pull-left">
                                <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server"><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton>
                            </span>
                        </div>
                    </div>
                    <asp:GridView ID="ListPurchaseOrder" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="PurchaseOrderPageIndexChanging" PageSize="20" AutoGenerateColumns="false"   EmptyDataText="There are no Records">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="PurchaseOrderEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="PurchaseOrderNo" HeaderText="Purchase Order"></asp:BoundField>
                            <asp:BoundField DataField="PurchaseOrderDate" HeaderText="Date"  DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                            <asp:BoundField DataField="LocationCode" HeaderText="Location"></asp:BoundField>
                            <asp:BoundField DataField="Buyer" HeaderText="Buyer"></asp:BoundField>
                            <asp:BoundField DataField="Telephone" HeaderText="Telephone "></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("Status").ToString()=="0" ? "Free" :(Eval("Status").ToString()=="1"?"Open":(Eval("Status").ToString()=="2"?"Finalized":(Eval("Status").ToString()=="4"?"Partially Received":(Eval("Status").ToString()=="5"?"Closed":"Error"))))  %></ItemTemplate>
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

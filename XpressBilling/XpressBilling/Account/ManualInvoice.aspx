<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManualInvoice.aspx.cs" Inherits="XpressBilling.Account.ManualInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
           <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                     <span style="float:left;">
                    Manual Invoice               </span>
                    <span style="float:right;">
                    Filtering    
                    <input type="checkbox" id="chkFilter" title="Search" onclick="javascript:fnShowSearch(this);" /></span>
                    <div style="clear:both;"></div>   
                </div>
                <div class="col-xs-10 col-md-12" runat="server" id="filterArea" style="display:none;">
                        <div class="form-group">  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">SalesOrderNo</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="SalesOrderNoSearch" class="form-control" placeholder="SO No" ClientIDMode="Static" onkeyup="SearchGrid('SalesOrderNoSearch', 'ListManualInvoice')"></asp:TextBox>
                            </div>  
                            <label class="control-label col-xs-12 col-sm-4 col-md-1">Sales Man</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="NameSearch" class="form-control" placeholder="Name" ClientIDMode="Static" onkeyup="SearchGrid('NameSearch', 'ListManualInvoice')"></asp:TextBox>
                            </div>   
                            <label class="control-label col-xs-12 col-sm-4 col-md-1">Customer</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="CustomerSearch" class="form-control" placeholder="Customer" ClientIDMode="Static" onkeyup="SearchGrid('CustomerSearch', 'ListManualInvoice')"></asp:TextBox>
                            </div>  
                             <label class="control-label col-xs-12 col-sm-4 col-md-1">Telephone</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="TelephoneSearch" class="form-control" placeholder="Telephone" ClientIDMode="Static" onkeyup="SearchGrid('TelephoneSearch', 'ListManualInvoice')"></asp:TextBox>
                            </div>                                                                     
                        </div>                       
                    </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right">
                            <span class="icon-wrap pull-left"><a href="ManualInvoiceEdit"><i class="glyphicon glyphicon-plus" style="color: white;"></i></a></span>
                            <%--<span class="icon-wrap pull-left">
                                <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server"><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton>
                            </span>--%>
                        </div>
                    </div>
                    <asp:GridView ID="ListManualInvoice" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="ManualInvoicePageIndexChanging" PageSize="20" EmptyDataText="There are no Records" AutoGenerateColumns="false">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="ManualInvoiceEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="SalesOrderNo" HeaderText="Invoice"></asp:BoundField>
                            <asp:BoundField DataField="SalesOrderDate" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                            <asp:BoundField DataField="SalesMan" HeaderText="Sales Man"></asp:BoundField>
                            <asp:BoundField DataField="LocationName" HeaderText="Location"></asp:BoundField>
                            <asp:BoundField DataField="Customer" HeaderText="Customer"></asp:BoundField>
                            <asp:BoundField DataField="Telephone" HeaderText="Telephone"></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("Status").ToString()=="0" ? "Free" :(Eval("Status").ToString()=="1"?"Open":"Paid")  %></ItemTemplate>
                            </asp:TemplateField>
                            
                            <%--<asp:TemplateField>  
                                <ItemTemplate>  
                                    <asp:CheckBox ID="chkDel"  runat="server" />  
                                    <asp:HiddenField ID="selectedId" runat="server" Value='<%# Bind("ID") %>' />
                                </ItemTemplate>  
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

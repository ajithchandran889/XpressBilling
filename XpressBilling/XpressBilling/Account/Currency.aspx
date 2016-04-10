<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="Currency.aspx.cs" Inherits="XpressBilling.Account.Currency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    <span style="float:left;">
                    Currency               </span>
                    <span style="float:right;">
                    Filtering    
                    <input type="checkbox" id="chkFilter" title="Search" onclick="javascript:fnShowSearch(this);" /></span>
                    <div style="clear:both;"></div>   
                </div>
                <div class="col-xs-10 col-md-8" runat="server" id="filterArea" style="display:none;">
                        <div class="form-group">  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">CurrencyCode</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="CurrencyCodeSearch" class="form-control" placeholder="CurrencyCode" ClientIDMode="Static" onkeyup="SearchGrid('CurrencyCodeSearch', 'listCurrency')"></asp:TextBox>
                            </div>  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">Name</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="NameSearch" class="form-control" placeholder="Name" ClientIDMode="Static" onkeyup="SearchGrid('NameSearch', 'listCurrency')"></asp:TextBox>
                            </div>                                                                     
                        </div>                       
                    </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                         <h2 class="pull-left">List View</h2>
                        <div class="pull-right"> 
                        <span class="icon-wrap pull-left"> <a href="CurrencyEdit"><i class="glyphicon glyphicon-plus" style="color:white;"></i></a></span>
               <%--<span class="icon-wrap pull-left"> <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server" ><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton> </span>--%>
                    </div></div>
                    <asp:GridView ID="listCurrency" runat="server" CssClass="table" AllowPaging="true" EmptyDataText="There are no Records"  OnDataBound="CurrencyUserDataBound"
                        OnPageIndexChanging="listCurrencyPageIndexChanging" PageSize="20" AutoGenerateColumns="false">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="CurrencyEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="CompanyCode" HeaderText="Company" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="CurrencyCode" HeaderText="Currency"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                            <asp:BoundField DataField="Decimal" HeaderText="Decimal"></asp:BoundField>                           
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("Status").ToString()=="0" ? "InActive":"Active" %></ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField>  
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

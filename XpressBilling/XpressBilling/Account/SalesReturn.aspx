<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalesReturn.aspx.cs" Inherits="XpressBilling.Account.SalesReturn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Sales Return               
                </div>
                <div class="col-xs-10 col-md-8" runat="server" id="filterArea">
                        <div class="form-group">  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">SalesReturnNo</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="SalesReturnNoSearch" class="form-control" placeholder="SR No" ClientIDMode="Static" onkeyup="SearchGrid('SalesReturnNoSearch', 'ListSalesReturn')"></asp:TextBox>
                            </div>                              
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">SalesMan</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="SalesManNameSearch" class="form-control" placeholder="SalesMan" ClientIDMode="Static" onkeyup="SearchGrid('SalesManNameSearch', 'ListSalesReturn')"></asp:TextBox>
                            </div> 
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">Locationcode</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="LocationcodeSearch" class="form-control" placeholder="Locationcode" ClientIDMode="Static" onkeyup="SearchGrid('LocationcodeSearch', 'ListSalesReturn')"></asp:TextBox>
                            </div> 
                                                                                                
                        </div>                       
                    </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right">
                            <span class="icon-wrap pull-left"><a href="SalesReturnEdit"><i class="glyphicon glyphicon-plus" style="color: white;"></i></a></span>
                            <span class="icon-wrap pull-left">
                                <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server"><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton>
                            </span>
                        </div>
                    </div>
                    <asp:GridView ID="ListSalesReturn" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="SalesReturnPageIndexChanging" PageSize="20" AutoGenerateColumns="false"  EmptyDataText="There are no Records">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="SalesReturnEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="SalesReturnNo" HeaderText="Sales Return"></asp:BoundField>
                            <asp:BoundField DataField="SalesReturnDate" HeaderText="Date"  DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                            <asp:BoundField DataField="Locationcode" HeaderText="Location"></asp:BoundField>
                            <asp:BoundField DataField="BusinessPartnerCode" HeaderText="Supplier"></asp:BoundField>
                            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:n}"></asp:BoundField>
                            <asp:BoundField DataField="SalesMan" HeaderText="Sales Man"></asp:BoundField>
                            <asp:BoundField DataField="Reference" HeaderText="Reference"></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("Status").ToString()=="0" ? "Free" :(Eval("Status").ToString()=="1"?"Open":"Finalized")  %></ItemTemplate>
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

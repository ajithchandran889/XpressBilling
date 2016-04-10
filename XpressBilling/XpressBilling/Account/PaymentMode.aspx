<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentMode.aspx.cs" Inherits="XpressBilling.Account.PaymentMode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                     <span style="float:left;">
                    Payment Mode               </span>
                    <span style="float:right;">
                    Filtering    
                    <input type="checkbox" id="chkFilter" title="Search" onclick="javascript:fnShowSearch(this);" /></span>
                    <div style="clear:both;"></div>   
                </div>
                <div class="col-xs-10 col-md-8" runat="server" id="filterArea" style="display:none;">
                        <div class="form-group">  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">PaymentMode</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="PaymentModeSearch" class="form-control" placeholder="PaymentMode" ClientIDMode="Static" onkeyup="SearchGrid('PaymentModeSearch', 'ListPaymentMode')"></asp:TextBox>
                            </div>  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">Name</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="NameSearch" class="form-control" placeholder="Name" ClientIDMode="Static" onkeyup="SearchGrid('NameSearch', 'ListPaymentMode')"></asp:TextBox>
                            </div>  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">BankCode</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="BankCodeSearch" class="form-control" placeholder="BankCode" ClientIDMode="Static" onkeyup="SearchGrid('BankCodeSearch', 'ListPaymentMode')"></asp:TextBox>
                            </div>      
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">Acc No</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="AccNoSearch" class="form-control" placeholder="AccNo" ClientIDMode="Static" onkeyup="SearchGrid('AccNoSearch', 'ListPaymentMode')"></asp:TextBox>
                            </div>                                                                 
                        </div>                       
                    </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right"> 
              <span class="icon-wrap pull-left"> <a href="PaymentModeEdit"><i class="glyphicon glyphicon-plus" style="color:white;"></i></a></span>
               <%--<span class="icon-wrap pull-left"> <asp:LinkButton ID="LinkButton1" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server" ><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton> </span>--%>
              </div>
                    </div>
                    <asp:GridView ID="ListPaymentMode" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="PaymentModePageIndexChanging" PageSize="20" AutoGenerateColumns="false" EmptyDataText="There are no Paymentmodes" OnDataBound="listPaymentModeDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="PaymentMode" DataNavigateUrlFormatString="PaymentModeEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                            <asp:TemplateField HeaderText="Transactions">
                                <ItemTemplate><%# Eval("Transactions").ToString()=="0" ? "Cash":Eval("Transactions").ToString()=="1" ? "Bank":Eval("Transactions").ToString()=="2" ? "Discount":Eval("Transactions").ToString()=="3" ? "Round-off":Eval("Transactions").ToString()=="4" ? "Credit note":"" %></ItemTemplate>
                            </asp:TemplateField>                             
                            <asp:BoundField DataField="AccName" HeaderText="Account"></asp:BoundField>
                            <asp:BoundField DataField="BankName" HeaderText="Bank"></asp:BoundField>
                           <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("Status").ToString()=="0" ? "InActive":"Active" %></ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField>  
                                <ItemTemplate>  
                                    <asp:CheckBox ID="chkDel"  runat="server" />  
                                    <asp:HiddenField ID="selectedId" runat="server" Value='<%# Bind("PaymentMode") %>' />
                                </ItemTemplate>  
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

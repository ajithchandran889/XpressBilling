<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentMode.aspx.cs" Inherits="XpressBilling.Account.PaymentMode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Payment Mode
                <div class="pull-right">
                    <span class="icon-wrap pull-left"> <a href="PaymentModeEdit"><i class="glyphicon glyphicon-plus "></i></a></span>
                    <span class="icon-wrap pull-left"><asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server" ><i class="glyphicon glyphicon-trash"></i></asp:LinkButton> </span>
                </div>
                </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2>List View</h2>
                    </div>
                    <asp:GridView ID="ListPaymentMode" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="PaymentModePageIndexChanging" PageSize="20" AutoGenerateColumns="false" OnDataBound="listPaymentModeDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="PaymentMode" DataNavigateUrlFormatString="PaymentModeEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                            <asp:BoundField DataField="AccountNo" HeaderText="Account"></asp:BoundField>
                            <asp:BoundField DataField="BankCode" HeaderText="Bank Code"></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                   <asp:dropdownlist id="PaymentModeDdl" IdPaymentMode='<%# Eval("PaymentMode") %>' AutoPostBack="true" runat="server" OnSelectedIndexChanged="PaymentModeDdlSelectedIndexChanged">
                                        <asp:listitem value="1" text="active"></asp:listitem>
                                        <asp:listitem value="0" text="inactive"></asp:listitem>
                                    </asp:dropdownlist>
                                    <asp:HiddenField ID="selectedvalue" runat="server" Value='<%# Bind("Status") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>  
                                <ItemTemplate>  
                                    <asp:CheckBox ID="chkDel"  runat="server" />  
                                    <asp:HiddenField ID="selectedId" runat="server" Value='<%# Bind("PaymentMode") %>' />
                                </ItemTemplate>  
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FirstFreeNumber.aspx.cs" Inherits="XpressBilling.Account.FirstFreeNumber" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    First Free Number               
                </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right">
                            <span class="icon-wrap pull-left"><a href="FirstFreeNumberEdit"><i class="glyphicon glyphicon-plus" style="color: white;"></i></a></span>
                            <span class="icon-wrap pull-left">
                                <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server"><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton>
                            </span>
                        </div>
                    </div>
                    <asp:GridView ID="ListFirstFreeNumber" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="FirstFreeNumberPageIndexChanging" PageSize="3" AutoGenerateColumns="false" EmptyDataText="There are no Records" OnDataBound="listFirstFreeNumberDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="FirstFreeNumberEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="DocumentNo" HeaderText="Number Group"></asp:BoundField>
                            <asp:BoundField DataField="TransactionType" HeaderText="Type"></asp:BoundField>
                            <asp:BoundField DataField="DocumentDate" HeaderText="Date"></asp:BoundField>
                            <%--<asp:TemplateField HeaderText="OrderType">
                                <ItemTemplate><%# Eval("SeqType").ToString()=="0" ? (Eval("OrderType").ToString()=="0" ? "Local":"Import"):(Eval("OrderType").ToString()=="0" ? "Cash":"Credit") %></ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="CreatedBy" HeaderText="User"></asp:BoundField>
                            <%--<asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                   <asp:dropdownlist id="FirstFreeDdl" IdFirstFreeNumber='<%# Eval("ID") %>' AutoPostBack="true" runat="server" OnSelectedIndexChanged="FirstFreeNumberDdlSelectedIndexChanged">
                                        <asp:listitem value="1" text="active"></asp:listitem>
                                        <asp:listitem value="0" text="inactive"></asp:listitem>
                                    </asp:dropdownlist>
                                    <asp:HiddenField ID="selectedvalue" runat="server" Value='<%# Bind("Status") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
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

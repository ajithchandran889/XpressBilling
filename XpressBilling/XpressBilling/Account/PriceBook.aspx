<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PriceBook.aspx.cs" Inherits="XpressBilling.Account.PriceBook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Price Book
                <%--<div class="pull-right">
                    <span class="icon-wrap pull-left"> <a href="PriceBookEdit"><i class="glyphicon glyphicon-plus "></i></a></span>
                    <span class="icon-wrap pull-left"><asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server" ><i class="glyphicon glyphicon-trash"></i></asp:LinkButton> </span>
                </div>--%>
                </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right"> 
              <span class="icon-wrap pull-left"> <a href="PriceBookEdit"><i class="glyphicon glyphicon-plus" style="color:white;"></i></a></span>
               <span class="icon-wrap pull-left"> <asp:LinkButton ID="LinkButton1" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server" ><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton> </span>
              </div>
                    </div>
                    <asp:GridView ID="ListPriceBook" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="PriceBookPageIndexChanging" PageSize="20" AutoGenerateColumns="false" EmptyDataText="There are no Records" OnDataBound="listPriceBookDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="PriceBookEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="DocumentNo" HeaderText="Price Book"></asp:BoundField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate><%# Eval("PriceType").ToString()=="0" ? "Cost" : "Purchase" %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DocumentDate" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                            <asp:TemplateField HeaderText="OrderType">
                                <ItemTemplate><%# Eval("PriceType").ToString()=="0" ? (Eval("OrderType").ToString()=="0" ? "Local":"Import"):(Eval("OrderType").ToString()=="0" ? "Cash":"Credit") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CreatedBy" HeaderText="User"></asp:BoundField>
                            <asp:TemplateField HeaderText="Approval Status">
                                <ItemTemplate><%# Eval("ApprovalStatus").ToString()=="0" ? "Un Approved" : "Approved" %></ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                   <asp:dropdownlist id="PriceBookDdl" IdPriceBook='<%# Eval("ID") %>' AutoPostBack="true" runat="server" OnSelectedIndexChanged="PriceBookDdlSelectedIndexChanged">
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

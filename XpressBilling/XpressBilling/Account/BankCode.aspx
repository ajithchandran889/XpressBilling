﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BankCode.aspx.cs" Inherits="XpressBilling.Account.BankCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                   <span style="float:left;"> BankCode   </span>            
                    <span style="float:right;">
                        Filtering
                    <input type="checkbox" id="chkFilter" title="Search" onclick="javascript:fnShowSearch(this);" /></span>
                    <div style="clear:both;"></div>
                </div>
                <div class="col-xs-10 col-md-12" runat="server" id="filterArea" style="display:none;">
                        <div class="form-group">                            
                            <label class="control-label col-xs-12 col-sm-4 col-md-1">Bank Code</label>
                            <div class="col-xs-12 col-sm-2 col-md-2">
                                <asp:TextBox runat="server" ID="BankSearch" class="form-control" placeholder="Bank Code" ClientIDMode="Static" onkeyup="SearchGrid('BankSearch', 'listBankCode')"></asp:TextBox>
                            </div>
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">Name</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="BankNameSearch" class="form-control" placeholder="Name" ClientIDMode="Static" onkeyup="SearchGrid('BankNameSearch', 'listBankCode')"></asp:TextBox>
                            </div> 
                           <%-- <asp:Button ID="search" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Search" OnClick="searchbankcodeClick" /> --%>                          
                        </div>                       
                    </div>                
                        </div>                       
                    </div>
                <hr/>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right"> 
              <span class="icon-wrap pull-left"> <a href="EditBankCode"><i class="glyphicon glyphicon-plus" style="color:white;"></i></a></span>
               <%--<span class="icon-wrap pull-left"> <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server" ><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton> </span>--%>
              </div>
                    </div>
                    <asp:GridView ID="listBankCode" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="listBankCodePageIndexChanging" PageSize="20" AutoGenerateColumns="false" EmptyDataText="There are no Records" OnDataBound="listBankCodeDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="EditBankCode?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="BankCode" HeaderText="BankCode"></asp:BoundField>   
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                            <asp:BoundField DataField="Reference" HeaderText="User"></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("Status").ToString()=="0" ? "InActive":"Active" %></ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                   <asp:dropdownlist id="BankCodeDdl" IdBankCode='<%# Eval("ID") %>' AutoPostBack="true" runat="server" OnSelectedIndexChanged="BankCodeDdlSelectedIndexChanged">
                                        <asp:listitem value="1" text="active"></asp:listitem>
                                        <asp:listitem value="0" text="inactive"></asp:listitem>
                                    </asp:dropdownlist>
                                    <asp:HiddenField ID="selectedvalue" runat="server" Value='<%# Bind("Status") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
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
    <asp:HiddenField ID="searchbankcodeid" ClientIDMode="Static" runat="server" />
</asp:Content>

﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="City.aspx.cs" Inherits="XpressBilling.Account.City" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">                                  
                <span style="float:left;">
                    City              </span>
                    <span style="float:right;">
                    Filtering    
                    <input type="checkbox" id="chkFilter" title="Search" onclick="javascript:fnShowSearch(this);" /></span>
                    <div style="clear:both;"></div>   
                </div>
                <div class="col-xs-10 col-md-12" runat="server" id="filterArea" style="display:none;">
                        <div class="form-group">  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">CityCode</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="CityCodeSearch" class="form-control" placeholder="CityCode" ClientIDMode="Static" onkeyup="SearchGrid('CityCodeSearch', 'listCity')"></asp:TextBox>
                            </div>  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">Name</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="NameSearch" class="form-control" placeholder="Name" ClientIDMode="Static" onkeyup="SearchGrid('NameSearch', 'listCity')"></asp:TextBox>
                            </div>   
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">CountryName</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="CountryNameSearch" class="form-control" placeholder="CountryName" ClientIDMode="Static" onkeyup="SearchGrid('CountryNameSearch', 'listCity')"></asp:TextBox>
                            </div>                                        
                        </div>                       
                    </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right"> 
              <span class="icon-wrap pull-left"> <a href="EditCity"><i class="glyphicon glyphicon-plus" style="color:white;"></i></a></span>
               <%--<span class="icon-wrap pull-left"> <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server" ><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton> </span>--%>
              </div>
                    </div>
                    <asp:GridView ID="listCity" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="listCityPageIndexChanging" PageSize="20" AutoGenerateColumns="false" EmptyDataText="There are no Records" OnDataBound="listCityDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="EditCity?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="CityCode" HeaderText="City Code"></asp:BoundField>                            
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>  
                            <asp:BoundField DataField="CountryName" HeaderText="Country"></asp:BoundField> 
                            <asp:BoundField DataField="CreatedBy" HeaderText="User"></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("Status").ToString()=="0" ? "InActive":"Active" %></ItemTemplate>
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

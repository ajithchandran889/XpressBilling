<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Location.aspx.cs" Inherits="XpressBilling.Account.Location" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                     <span style="float:left;">
                    Location               </span>
                    <span style="float:right;">
                    Filtering    
                    <input type="checkbox" id="chkFilter" title="Search" onclick="javascript:fnShowSearch(this);" /></span>
                    <div style="clear:both;"></div>   
                </div>
                <div class="col-xs-10 col-md-12" runat="server" id="filterArea" style="display:none;">
                        <div class="form-group">  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">LocationCode</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="LocationCodeSearch" class="form-control" placeholder="LocationCode" ClientIDMode="Static" onkeyup="SearchGrid('LocationCodeSearch', 'listLocation')"></asp:TextBox>
                            </div>  
                            <label class="control-label col-xs-12 col-sm-4 col-md-1">Name</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="NameSearch" class="form-control" placeholder="Name" ClientIDMode="Static" onkeyup="SearchGrid('NameSearch', 'listLocation')"></asp:TextBox>
                            </div>    
                            <label class="control-label col-xs-12 col-sm-4 col-md-1">Email</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="EmailSearch" class="form-control" placeholder="Email" ClientIDMode="Static" onkeyup="SearchGrid('EmailSearch', 'listLocation')"></asp:TextBox>
                            </div> 
                             <label class="control-label col-xs-12 col-sm-4 col-md-1">Mobile</label>
                            <div class="col-xs-12 col-sm-8 col-md-1">
                                <asp:TextBox runat="server" ID="MobileSearch" class="form-control" placeholder="Mobile" ClientIDMode="Static" onkeyup="SearchGrid('MobileSearch', 'listLocation')"></asp:TextBox>
                            </div>                                      
                        </div>                       
                    </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right">
                            <span class="icon-wrap pull-left"><a href="LocationEdit"><i class="glyphicon glyphicon-plus" style="color: white;"></i></a></span>
                            <%--<span class="icon-wrap pull-left">
                                <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server"><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton>
                            </span>--%>
                        </div>
                    </div>
                    <asp:GridView ID="listLocation" runat="server" CssClass="table" CellPadding="0" CellSpacing="0" AllowPaging="true"
                        OnPageIndexChanging="listLocationPageIndexChanging" PageSize="20" AutoGenerateColumns="false" EmptyDataText="There are no Locations" OnDataBound="listLocationDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="LocationEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="LocationCode" HeaderText="Location"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                            <asp:BoundField DataField="ContactName" HeaderText="Contact Person"></asp:BoundField>
                            <asp:BoundField DataField="Mobile" HeaderText="Mobile"></asp:BoundField>
                            <asp:BoundField DataField="City" HeaderText="City"></asp:BoundField>
                            <asp:BoundField DataField="Country" HeaderText="Country"></asp:BoundField>
                            <asp:BoundField DataField="ZipCode" HeaderText="Zip/Postal Code" ItemStyle-Width="80"></asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="150"></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("Status").ToString()=="0" ? "InActive":"Active" %></ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                   <asp:dropdownlist id="LocationDdl" IdLocation='<%# Eval("ID") %>' AutoPostBack="true" runat="server" OnSelectedIndexChanged="LocationDdlSelectedIndexChanged">
                                        <asp:listitem value="1" text="Active"></asp:listitem>
                                        <asp:listitem value="0" text="Inactive"></asp:listitem>
                                    </asp:dropdownlist>
                                    <asp:HiddenField ID="selectedvalue" runat="server" Value='<%# Bind("Status") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDel" runat="server" />
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

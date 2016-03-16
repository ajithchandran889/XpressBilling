<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemGroup.aspx.cs" Inherits="XpressBilling.Account.ItemGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Item Group              
                </div>
                 <div class="col-xs-10 col-md-8" runat="server" id="filterArea">
                        <div class="form-group">  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">ItemGroupCode</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="ItemGroupCodeSearch" class="form-control" placeholder="IGroupCode" ClientIDMode="Static" onkeyup="SearchGrid('ItemGroupCodeSearch', 'listItemGroup')"></asp:TextBox>
                            </div>  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">Name</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="NameSearch" class="form-control" placeholder="Name" ClientIDMode="Static" onkeyup="SearchGrid('NameSearch', 'listItemGroup')"></asp:TextBox>
                            </div>  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">TaxCodeName</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="TaxCodeNameSearch" class="form-control" placeholder="TaxCodeName" ClientIDMode="Static" onkeyup="SearchGrid('TaxCodeNameSearch', 'listItemGroup')"></asp:TextBox>
                            </div>                                                                     
                        </div>                       
                    </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right"> 
              <span class="icon-wrap pull-left"> <a href="EditItemGroup"><i class="glyphicon glyphicon-plus" style="color:white;"></i></a></span>
               <span class="icon-wrap pull-left"> <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server" ><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton> </span>
              </div>                       
                    </div>
                    <asp:GridView ID="listItemGroup" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="listItemGroupPageIndexChanging" PageSize="20" AutoGenerateColumns="false" EmptyDataText="There are no Records" OnDataBound="listItemGroupDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="EditItemGroup?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="ItemGroupCode" HeaderText="ItemGroup"></asp:BoundField>                            
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>  
                            <asp:BoundField DataField="TaxCodeName" HeaderText="Tax"></asp:BoundField> 
                            <asp:BoundField DataField="CessCode" HeaderText="CessCode"></asp:BoundField> 
                            <asp:BoundField DataField="CreatedBy" HeaderText="User"></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("Status").ToString()=="0" ? "InActive":"Active" %></ItemTemplate>
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

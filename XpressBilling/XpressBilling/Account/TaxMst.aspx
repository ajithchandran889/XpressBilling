<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaxMst.aspx.cs" Inherits="XpressBilling.Account.TaxMst" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Tax               
                </div>
                <div class="col-xs-10 col-md-8" runat="server" id="filterArea">
                        <div class="form-group">  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">Tax</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="TaxSearch" class="form-control" placeholder="Tax" ClientIDMode="Static" onkeyup="SearchGrid('TaxSearch', 'listTaxMst')"></asp:TextBox>
                            </div>  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">Name</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="NameSearch" class="form-control" placeholder="Name" ClientIDMode="Static" onkeyup="SearchGrid('NameSearch', 'listTaxMst')"></asp:TextBox>
                            </div>   
                             <label class="control-label col-xs-12 col-sm-4 col-md-2">TaxCode</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="TaxCodeNameSearch" class="form-control" placeholder="TaxCodeName" ClientIDMode="Static" onkeyup="SearchGrid('TaxCodeNameSearch', 'listTaxMst')"></asp:TextBox>
                            </div>         
                                           
                        </div>                       
                    </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right"> 
              <span class="icon-wrap pull-left"> <a href="EditTaxMst"><i class="glyphicon glyphicon-plus" style="color:white;"></i></a></span>
               <span class="icon-wrap pull-left"> <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server" ><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton> </span>
              </div>
                    </div>
                    <asp:GridView ID="listTaxMst" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="listTaxMstPageIndexChanging" PageSize="20" AutoGenerateColumns="false" EmptyDataText="There are no Records" OnDataBound="listTaxMstDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="EditTaxMst?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="Tax" HeaderText="Tax"></asp:BoundField>                            
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>  
                            <asp:BoundField DataField="TaxCodeName" HeaderText="TaxCode"></asp:BoundField> 
                            <asp:BoundField DataField="TaxPercentage" HeaderText="TaxPercentage"></asp:BoundField>
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

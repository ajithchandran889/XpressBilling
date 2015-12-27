<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="XpressBilling.Account.Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Company                
                </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right"> 
              <span class="icon-wrap pull-left"> <a href="CompanyEdit"><i class="glyphicon glyphicon-plus" style="color:white;"></i></a></span>
               <span class="icon-wrap pull-left"> <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server" ><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton> </span>
              </div></div>
                    <asp:GridView ID="listCompany" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="listCompanyPageIndexChanging" PageSize="20" AutoGenerateColumns="false" EmptyDataText="There are no records listing" OnDataBound="listCompanyDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="CompanyEdit?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="CompanyCode" HeaderText="Company"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                            <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person"></asp:BoundField>
                            <asp:BoundField DataField="Mobile" HeaderText="Mobile"></asp:BoundField>
                            <asp:BoundField DataField="City" HeaderText="City"></asp:BoundField>
                            <asp:BoundField DataField="Country" HeaderText="Country"></asp:BoundField>
                            <asp:BoundField DataField="ZipCode" HeaderText="Zip/Postal Code" ItemStyle-Width="80"></asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="150"></asp:BoundField>                            
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

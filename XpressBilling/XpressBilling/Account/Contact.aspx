<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="XpressBilling.Account.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Contact                
                </div>
                <div class="col-xs-10 col-md-8" runat="server" id="filterArea">
                        <div class="form-group">  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">Contact</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="ContactSearch" class="form-control" placeholder="Contact" ClientIDMode="Static" onkeyup="SearchGrid('ContactSearch', 'listContact')"></asp:TextBox>
                            </div>  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">Name</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="NameSearch" class="form-control" placeholder="Name" ClientIDMode="Static" onkeyup="SearchGrid('NameSearch', 'listContact')"></asp:TextBox>
                            </div>  
                             <label class="control-label col-xs-12 col-sm-4 col-md-2">CompanyName</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="CompanyNameSearch" class="form-control" placeholder="CompName" ClientIDMode="Static" onkeyup="SearchGrid('CompanyNameSearch', 'listContact')"></asp:TextBox>
                            </div>  
                            <label class="control-label col-xs-12 col-sm-4 col-md-2">Email</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="EmailSearch" class="form-control" placeholder="Email" ClientIDMode="Static" onkeyup="SearchGrid('EmailSearch', 'listContact')"></asp:TextBox>
                            </div> 
                             <label class="control-label col-xs-12 col-sm-4 col-md-2">Mobile</label>
                            <div class="col-xs-12 col-sm-8 col-md-2">
                                <asp:TextBox runat="server" ID="MobileSearch" class="form-control" placeholder="Mobile" ClientIDMode="Static" onkeyup="SearchGrid('MobileSearch', 'listContact')"></asp:TextBox>
                            </div>                                      
                        </div>                       
                    </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right"> 
              <span class="icon-wrap pull-left"> <a href="EditContact"><i class="glyphicon glyphicon-plus" style="color:white;"></i></a></span>
               <span class="icon-wrap pull-left"> <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server" ><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton> </span>
              </div></div>

                    <asp:GridView ID="listContact" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="listContactPageIndexChanging" PageSize="20" EmptyDataText="There are no Records" AutoGenerateColumns="false" OnDataBound="listContactDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="EditContact?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            
                            <asp:BoundField DataField="Contact" HeaderText="Contact"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                            <asp:BoundField DataField="Designation" HeaderText="Designation"></asp:BoundField>
                            <asp:BoundField DataField="CompanyName" HeaderText="Company"></asp:BoundField>
                            <%--<asp:BoundField DataField="CompanyCodeName" HeaderText="Company"></asp:BoundField>--%>
                            <asp:BoundField DataField="CountryCode" HeaderText="Country"></asp:BoundField>
                            <asp:BoundField DataField="CityCode" HeaderText="City"></asp:BoundField>
                            <asp:BoundField DataField="Mobile" HeaderText="Mobile"></asp:BoundField>                               
                            <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="150"></asp:BoundField>
                            <asp:BoundField DataField="ZipPostalCode" HeaderText="Zip/Postal Code" ItemStyle-Width="80"></asp:BoundField>
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

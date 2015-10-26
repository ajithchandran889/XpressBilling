<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="XpressBilling.Account.Employee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Employee              
                </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right"> 
                            <span class="icon-wrap pull-left"> <a href="EditEmployeeMaster"><i class="glyphicon glyphicon-plus" style="color:white;"></i></a></span>
               <span class="icon-wrap pull-left"> <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server" ><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton> </span>
              
              </div>
                    </div>
                    <asp:GridView ID="listEmployee" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="listEmployeePageIndexChanging" PageSize="20" AutoGenerateColumns="false" OnDataBound="listEmployeeDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="EditEmployeeMaster?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="EmployeeCode" HeaderText="Employee ID"></asp:BoundField>                            
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>  
                            <asp:BoundField DataField="DateofJoining" HeaderText="Date of Joining"></asp:BoundField> 
                            <asp:BoundField DataField="CreatedBy" HeaderText="User"></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                   <asp:dropdownlist id="EmployeeDdl" IdEmployee='<%# Eval("ID") %>' AutoPostBack="true" runat="server" OnSelectedIndexChanged="EmployeeDdlSelectedIndexChanged">
                                        <asp:listitem value="1" text="Active"></asp:listitem>
                                        <asp:listitem value="0" text="Inactive"></asp:listitem>
                                    </asp:dropdownlist>
                                    <asp:HiddenField ID="selectedvalue" runat="server" Value='<%# Bind("Status") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>  
                                <ItemTemplate>  
                                    <asp:CheckBox ID="chkDel"  runat="server" />  
                                    <asp:HiddenField ID="selectedId" runat="server" Value='<%# Bind("ID") %>' />
                                </ItemTemplate>  
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

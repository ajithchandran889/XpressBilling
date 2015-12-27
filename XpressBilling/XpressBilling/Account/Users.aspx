<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="XpressBilling.Account.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Users                
                </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2 class="pull-left">List View</h2>
                        <div class="pull-right"> 
              <span class="icon-wrap pull-left"> <a href="AddUser"><i class="glyphicon glyphicon-plus" style="color:white;"></i></a></span>
               <%--<span class="icon-wrap pull-left"> <asp:LinkButton ID="deleteRecords" OnClick="deleteRecordsClick" OnClientClick="DeleteConfirm()" ClientIDMode="Static" runat="server" ><i class="glyphicon glyphicon-trash" style="color:white;"></i></asp:LinkButton> </span>--%>
              </div>
                    </div>
                    <asp:GridView ID="listUser" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="listUserPageIndexChanging" PageSize="20" AutoGenerateColumns="false" OnDataBound="listUserDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="AddUser?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="UserName" HeaderText="User Name"></asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="Email"  ItemStyle-Width="150"></asp:BoundField>
                            <asp:BoundField DataField="EmployeeId" HeaderText="Employee Id"></asp:BoundField>
                            <asp:BoundField DataField="UserType" HeaderText="User Type"></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("Status").ToString()=="0" ? "InActive":"Active" %></ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField>  
                                <ItemTemplate>  
                                    <asp:CheckBox ID="chkDel"  runat="server" />  
                                    <asp:HiddenField ID="selectedId" runat="server" Value='<%# Bind("ID") %>' />
                                </ItemTemplate>  
                            </asp:TemplateField>--%>
                            <%-- <asp:Label runat="server" Text='<%# Convert.ToBoolean(Eval("Status"))?"Active":"InActive" %>'></asp:Label>--%>
                            <%--<asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                   
                                    <asp:dropdownlist id="UserStatusDdl" userName='<%# Eval("UserName") %>' AutoPostBack="true" runat="server" OnSelectedIndexChanged="UserStatusDdlSelectedIndexChanged">
                                        <asp:listitem value="1" text="active"></asp:listitem>
                                        <asp:listitem value="0" text="inactive"></asp:listitem>
                                    </asp:dropdownlist>
                                    <asp:HiddenField ID="selectedvalue" runat="server" Value='<%# Bind("Status") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

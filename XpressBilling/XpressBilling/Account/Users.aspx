<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="XpressBilling.Account.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">
                    Users
                <div class="pull-right"><a href="AddUser">ADD</a></div>
                </div>
                <div class="grid_wrapper">
                    <div class="grid_header">
                        <h2>List View</h2>
                    </div>
                    <asp:GridView ID="listUser" runat="server" CssClass="table" AllowPaging="true"
                        OnPageIndexChanging="listUserPageIndexChanging" PageSize="4" AutoGenerateColumns="false" OnDataBound="listUserDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="AddUser?Id={0}" HeaderText="" ControlStyle-CssClass="glyphicon glyphicon-pencil" />
                            <asp:BoundField DataField="UserName" HeaderText="User Name"></asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="Email"  ItemStyle-Width="150"></asp:BoundField>
                            <asp:BoundField DataField="EmployeeId" HeaderText="Employee Id"></asp:BoundField>
                            <asp:BoundField DataField="UserType" HeaderText="User Type"></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                   <%-- <asp:Label runat="server" Text='<%# Convert.ToBoolean(Eval("Status"))?"Active":"InActive" %>'></asp:Label>--%>
                                    <asp:dropdownlist id="UserStatusDdl" userName='<%# Eval("UserName") %>' AutoPostBack="true" runat="server" OnSelectedIndexChanged="UserStatusDdlSelectedIndexChanged">
                                        <asp:listitem value="1" text="active"></asp:listitem>
                                        <asp:listitem value="0" text="inactive"></asp:listitem>
                                    </asp:dropdownlist>
                                    <asp:HiddenField ID="selectedvalue" runat="server" Value='<%# Bind("Status") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditEmployeeMaster.aspx.cs" Inherits="XpressBilling.Account.EditEmployeeMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">Employee Details</div>
                <div class="form-group">
                   
                    <label for="Employee" class="control-label col-xs-2 col-md-2">Employee</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Employee" class="form-control required" placeholder="Employee" ClientIDMode="Static"></asp:TextBox>                        
                    </div><div class="col-xs-10 col-md-2"></div>
                    <label for="Date" id="lbldate" runat="server" class="control-label col-xs-2 col-md-2">Date</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Date" class="form-control required" placeholder="Date" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>                   

                </div>
                <div class="form-group">
                   
                    <label for="Name" class="control-label col-xs-2 col-md-2">Name</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Name" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>                        
                    </div>
                    <div class="col-xs-10 col-md-2"></div>
                    <label for="UserName" runat="server" ID="lblusername" placeholder="username" class="control-label col-xs-2 col-md-2">User</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="UserName" class="form-control required" placeholder="User" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>

                </div>
                 <div class="form-group"> 
                    
                        <label for="Date Of Joining" runat="server" id="lbldoj" class="control-label col-xs-2 col-md-2">Date of Joining</label>
                    <div class="col-xs-10 col-md-2">                        
                         <asp:TextBox runat="server" ID="DOJ" class="form-control required" placeholder="Date of Joining" ClientIDMode="Static"></asp:TextBox>
                    </div><div class="col-xs-10 col-md-2"></div>
                    <label for="Status" runat="server" id="lblstatus" class="control-label col-xs-2 col-md-2">Status</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control required" ID="ddlStatus" ClientIDMode="Static">
                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>                

                <div class="form-group">
                    
                    <asp:HiddenField ID="EmployeeId" runat="server" />
                    <asp:HiddenField ID="hdncompanycode" runat="server" />
                    
                    <asp:HiddenField ID="hdnreference" runat="server" />
                    <div class="col-xs-10 col-md-8">
                         <a href="/Account/Employee.aspx" class="btn btn-primary pull-left">Cancel</a><asp:Button ID="saveEmployee" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="SaveClick" />
                       <label id="lblMsg" style="color:red;" runat="server"></label>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

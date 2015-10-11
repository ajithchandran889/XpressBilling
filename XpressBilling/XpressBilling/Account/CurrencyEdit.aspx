<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CurrencyEdit.aspx.cs" Inherits="XpressBilling.Account.CurrencyEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">Add Crrency Details</div>
                <div class="form-group">
                    <label for="Company" class="control-label col-xs-2 col-md-2">Company</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Company" name="Company" class="form-control required" placeholder="Company" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label for="Currency" class="control-label col-xs-2 col-md-2">Currency</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Currency" class="form-control required" placeholder="Currency" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="Name" class="control-label col-xs-2 col-md-2">Name</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Name" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>

                </div>
                <div class="form-group">
                    <label for="Decimal" class="control-label col-xs-2 col-md-2">No. Of Decimal</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Decimal" class="form-control required" placeholder="No. of Decimal" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="Date" class="control-label col-xs-2 col-md-2">Date</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Date" class="form-control required" placeholder="Date" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="UserName" class="control-label col-xs-2 col-md-2">User</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="UserName" class="form-control required" placeholder="User" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>

                </div>

                <div class="form-group">
                   
                    <asp:HiddenField ID="CurrencyId" runat="server" />
                    <div class="col-xs-10 col-md-8">
                        <asp:Button ID="saveCurrency" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right" Text="Save" OnClick="SaveClick" />
                        <a href="/Account/Currency.aspx" class="btn btn-primary pull-right">Cancel</a>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

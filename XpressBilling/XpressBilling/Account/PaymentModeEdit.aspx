<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentModeEdit.aspx.cs" Inherits="XpressBilling.Account.PaymentModeEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div>
            <asp:Label runat="server" ID="Message"></asp:Label>
        </div>
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">Add Payment Mode Details</div>
                <div class="form-group">
                   <label id="lblPaymentMode" runat="server" for="PaymentMode" class="control-label col-xs-2 col-md-2">Payment Mode</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="PaymentMode" name="PaymentMode" class="form-control" placeholder="PaymentMode" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label id="lblCreatedDate" runat="server" for="Date" class="control-label col-xs-2 col-md-2">Created Date</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="CreatedDate" name="Date" class="form-control" placeholder="Date" ClientIDMode="Static"></asp:TextBox>
                    </div>

                </div>
                <div class="form-group">
                    <label for="Name" class="control-label col-xs-2 col-md-2">Name</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Name" name="Name" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label id="lblCreatedUser" runat="server" for="CreatedUser" class="control-label col-xs-2 col-md-2">User</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="CreatedUser" name="CreatedUser" class="form-control " placeholder="User" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                
                <div class="form-group">
                    <label  class="control-label col-xs-2 col-md-2">Transaction</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control required" ID="Transaction" ClientIDMode="Static">
                            <asp:ListItem Selected="True" Value="-1" Text="Select Transaction"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Cash"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Bank"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Discount"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Round-off"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                 <div class="form-group">
                    <asp:Label id="lblBankDetail" runat="server"  ClientIDMode="Static"  for="BankAccount" class="control-label col-xs-2 col-md-2">Bank Account</asp:Label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="BankAccount" class="form-control required" placeholder="Bank Account" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    

                </div>
                <div class="form-group">
                    
                    <asp:HiddenField ID="PaymentModeId" runat="server" />
                    <div class="col-xs-10 col-md-8">
                        <asp:Button ID="savePaymentMode" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right" Text="Save" OnClick="SaveClick" />
                        <a href="/Account/PaymentMode.aspx" class="btn btn-primary pull-right">Cancel</a>
                
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentModeEdit.aspx.cs" Inherits="XpressBilling.Account.PaymentModeEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">      
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                 <div id="SaveSuccess" visible="false" class="alert alert-success" role="alert" runat="server">
                    <span runat="server"><img src="~/Images/like.png" alt="" runat="server" />	</span>
                    Saved Successfully
                </div>
                <div id="UpdateSuccess" visible="false" class="alert alert-success" role="alert" runat="server">
                    <span runat="server"><img src="~/Images/like.png" alt="" runat="server" />	</span>
                    Updated Successfully
                </div>
                <div id="failure" visible="false" class="alert alert-danger" role="alert" runat="server">
                    <span id="failureMessage" runat="server">Sorry,Something went wrong!</span>
                </div>
                <div id="alreadyexist" visible="false" class="alert alert-danger" role="alert" runat="server">
                    <span id="alreadyexistmsg" runat="server">Code Already Exists</span>
                </div>
                <div class="page-header">Add Payment Mode Details</div>
                <div class="form-group">
                   <label id="lblPaymentMode" runat="server" for="PaymentMode" class="control-label col-xs-2 col-md-2">Payment Mode</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="PaymentMode" name="PaymentMode" class="form-control" placeholder="PaymentMode" ClientIDMode="Static"></asp:TextBox>
                    </div><div class="col-xs-10 col-md-2"></div>
                    <label id="lblCreatedDate" runat="server" for="Date" class="control-label col-xs-2 col-md-2">Created Date</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="CreatedDate" name="Date" class="form-control" placeholder="Date" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-xs-10 col-md-2"></div>
                </div>
                <div class="form-group">
                    <label for="Name" class="control-label col-xs-2 col-md-2">Name</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Name" name="Name" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                    </div><div class="col-xs-10 col-md-2"></div>
                    <label id="lblCreatedUser" runat="server" for="CreatedUser" class="control-label col-xs-2 col-md-2">User</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="CreatedUser" name="CreatedUser" class="form-control " placeholder="User" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-xs-10 col-md-2"></div>
                </div>
                
                <div class="form-group">
                    <label  class="control-label col-xs-2 col-md-2">Transaction</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control required" ID="Transaction" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="TransactionSelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="-1" Text="Select Transaction"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Cash"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Bank"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Discount"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Round-off"></asp:ListItem>
                        </asp:DropDownList>
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
                    <label id="lblBankacc" runat="server" for="BankAccount" class="control-label col-xs-2 col-md-2">Bank Account</label>
                    <div class="col-xs-10 col-md-2">
                        <%--<asp:TextBox runat="server" ID="BankAccount" class="form-control required" placeholder="Bank Account" ClientIDMode="Static"></asp:TextBox>--%>
                        <asp:DropDownList runat="server" class="form-control required" ID="ddlBankAccount" AutoPostBack="true" OnSelectedIndexChanged="ddlBankAccountSelectedIndexChanged" ClientIDMode="Static">                            
                        </asp:DropDownList>                        
                    </div>
                    <label id="lblbankcode" runat="server" class="control-label col-xs-2 col-md-2"></label>

                </div>
                <div class="form-group">
                    
                    <asp:HiddenField ID="PaymentModeId" runat="server" />
                    <div class="col-xs-10 col-md-8">
                        <a href="/Account/PaymentMode.aspx" class="btn btn-primary pull-left">Cancel</a><asp:Button ID="savePaymentMode" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="SaveClick" />
                </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

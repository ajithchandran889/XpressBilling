<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PasswordRecovery.aspx.cs" Inherits="XpressBilling.Account.PasswordRecovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:PasswordRecovery ID="PasswordRecovery1"  UserNameLabelText="Email:" UserNameTitleText="" UserNameInstructionText="Enter registered Email to receive password" runat="server"  OnVerifyingUser="PasswordRecoveryVerifyingUser"
            >
            <MailDefinition Subject="Password Recovery">
            </MailDefinition>
        </asp:PasswordRecovery>
    </div>
</asp:Content>

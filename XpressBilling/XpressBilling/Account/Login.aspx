<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="XpressBilling.Account.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="shortcut icon" href="../images/img/favicon.ico" type="image/x-icon">
    <title></title>
    <link href="../Content/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div>
        <div class="application-login"></div>
        <div class="log-wrap">

            <div class="log-container">
                <form runat="server">
                    <asp:Login ID="LoginCtrl" runat="server" ViewStateMode="Disabled" RenderOuterTable="false" OnLoggingIn="LoginCtrl_LoggingIn">
                        <LayoutTemplate>
                            <ul>
                                <li>
                                    <label>User Name</label></li>
                                <li>
                                    <asp:TextBox runat="server" ID="UserName" placeholder="User Name" class="text-log" />
                                    
                                </li>
                            </ul>
                            <ul>
                                <li>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                        CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                                </li>
                            </ul>
                            <ul>
                                <li>
                                    <label>Password</label>

                                </li>
                                <li>
                                    <asp:TextBox runat="server" ID="Password" class="text-log" placeholder="Password" TextMode="Password" />
                                    
                                </li>
                            </ul>
                             <ul>
                                <li>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                                        CssClass="field-validation-error" ErrorMessage="The password field is required." />
                                </li>
                            </ul>
                            <ul>
                                <li></li>
                                <li>
                                    <asp:CheckBox runat="server" ID="RememberMe" />
                                    <span>Remember Me</span>

                                </li>
                            </ul>
                            <ul>
                                <li></li>
                                <li>
                                    <asp:Button runat="server" class="log_button" CommandName="Login" Text="Log in" />
                                </li>
                            </ul>
                            <ul>
                                <li></li>
                                <li><a href="~/Account/PasswordRecovery" class="left">Forgot Password?</a></li>
                            </ul>
                        </LayoutTemplate>
                    </asp:Login>
                </form>
            </div>
            <div class="log-footer">

                <span>Copyright 2016</span>
            </div>

        </div>
    </div>
</body>
</html>

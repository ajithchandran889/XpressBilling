<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="XpressBilling.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <%--<hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Use the form below to create a new account.</h2>
    </hgroup>--%>

    <asp:CreateUserWizard runat="server" ID="RegisterUser" OnCreatedUser="RegisterUser_CreatedUser">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="wizardStepPlaceholder" />
            <asp:PlaceHolder runat="server" ID="navigationPlaceholder" />
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server" ID="RegisterUserWizardStep">
                <ContentTemplate>
                    <p class="message-info">
                        Passwords are required to be a minimum of <%: Membership.MinRequiredPasswordLength %> characters in length.
                    </p>

                    <p class="validation-summary-errors">
                        <asp:Literal runat="server" ID="ErrorMessage" />
                    </p>

                    <fieldset>
                        <div class="row">
                            <div class="page-header">User Registraion</div>
                            <div class="col-xs-12 col-sm-12 col-md-6">
                                <div class="form-group">
                                    <label for="UserName" class="control-label col-xs-12 col-sm-4 col-md-4">User Name</label>
                                    <div class="col-xs-12 col-sm-8 col-md-8">
                                        <asp:TextBox runat="server" ID="UserName" placeholder="User Name" class="form-control" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                            CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                                    </div>
                                    <label for="Password" class="control-label col-xs-12 col-sm-4 col-md-4">Password</label>
                                    <div class="col-xs-12 col-sm-8 col-md-8">
                                        <asp:TextBox runat="server" ID="Password" class="form-control" placeholder="Password" TextMode="Password" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                                            CssClass="field-validation-error" ErrorMessage="The password field is required." />
                                    </div>
                                    <label for="ConfirmPassword" class="control-label col-xs-12 col-sm-4 col-md-4">Confirm Password</label>
                                    <div class="col-xs-12 col-sm-8 col-md-8">
                                        <asp:TextBox runat="server" ID="ConfirmPassword" class="form-control" placeholder="Confirm Password" TextMode="Password" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                                            CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                                        <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                            CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                                    </div>
                                    <label for="inputEmail" class="control-label col-xs-12 col-sm-4 col-md-4">Email</label>
                                    <div class="col-xs-12 col-sm-8 col-md-8">
                                        <asp:TextBox runat="server" ID="Email" class="form-control" TextMode="Email" placeholder="Email" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                            CssClass="field-validation-error" placeholder="Email" ErrorMessage="The email address field is required." />
                                    </div>
                                    <label for="CompanyCode" class="control-label col-xs-12 col-sm-4 col-md-4">Company</label>
                                    <div class="col-xs-12 col-sm-8 col-md-8">
                                        <asp:DropDownList runat="server" ID="CompanyCode" class="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" InitialValue="-1" ControlToValidate="CompanyCode"
                                            CssClass="field-validation-error" ErrorMessage="The company field is required." />
                                    </div>
                                    <label for="UserType" class="control-label col-xs-12 col-sm-4 col-md-4">User Type</label>
                                    <div class="col-xs-12 col-sm-8 col-md-8">
                                        <asp:DropDownList runat="server" ID="UserType" class="form-control">
                                            <asp:ListItem Value="SuperUser" Text="Super User" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="User" Text="Normal User" Selected="False"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" InitialValue="-1" ControlToValidate="UserType"
                                            CssClass="field-validation-error" ErrorMessage="The UserType is required." />
                                    </div>
                                    <div class="form-group">
                                        <div class="col-xs-12 col-sm-12 col-md-12">
                                            <button type="button" class="btn btn-primary pull-left">Cancel</button>
                                            <asp:Button runat="server" class="btn btn-primary pull-left" CommandName="MoveNext" Text="Register" />

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-6">
                                <div class="form-group">
                                    <label for="input-1" class="control-label col-xs-2 col-md-2">Upload Photo</label>
                                    <div class="col-xs-10 col-md-2">
                                        <%--<input id="input-1" type="file" class="file">--%>
                                        <asp:Image ID="imgPreview" ClientIDMode="Static" runat="server" ImageUrl="/Images/user/preview.png" />
                                        <asp:FileUpload ID="inputUpload" ClientIDMode="Static" runat="server" class="file required" data-show-upload="false" data-show-caption="true" />
                                    </div>
                                </div>
                            </div>
                        </div>

                    </fieldset>
                </ContentTemplate>
                <CustomNavigationTemplate />
            </asp:CreateUserWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>

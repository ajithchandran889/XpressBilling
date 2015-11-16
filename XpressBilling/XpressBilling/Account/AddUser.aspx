<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="XpressBilling.Account.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div>
            <asp:Label runat="server" ID="Message"></asp:Label>
        </div>
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">Profile Details</div>
                <div class="form-group">
                    <label for="UserName" class="control-label col-xs-2 col-md-2">User Name</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="UserName" name="UserName" class="form-control required" placeholder="User Name" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label for="Email" class="control-label col-xs-2 col-md-2">Email</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Email" class="form-control required email" placeholder="Email" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="UserType" class="control-label col-xs-2 col-md-2">User Type</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList  runat="server" ID="UserType" class="form-control required" ClientIDMode="Static">
                            <asp:ListItem Value="SuperUser" Text="Super User" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="User" Text="Normal User" Selected="False"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>
                <div class="form-group">
                    <label for="Password" class="control-label col-xs-2 col-md-2">Password</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Password" class="form-control required" TextMode="Password" placeholder="Password" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="ConfPassword" class="control-label col-xs-2 col-md-2">Confirm Password</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="ConfPassword" equalto="#Password" class="form-control required" TextMode="Password" placeholder="Password" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="EmployeeId" class="control-label col-xs-2 col-md-2">Employee ID</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="EmployeeId" class="form-control required" placeholder="Employee ID" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>

                </div>
                <div class="form-group">
                    <label for="Company" class="control-label col-xs-2 col-md-2">Company</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Company" class="form-control required" placeholder="Company" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                     <label for="Location" class="control-label col-xs-2 col-md-2">Location</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList  runat="server" ID="Location" class="form-control required" ClientIDMode="Static">
                        </asp:DropDownList>
                    </div>
                    <label for="DefLocation" class="control-label col-xs-2 col-md-2">Default Location</label>
                    <div class="col-xs-10 col-md-2">
                       <asp:DropDownList  runat="server" ID="DefLocation" class="form-control required" ClientIDMode="Static">
                        </asp:DropDownList>
                    </div>
                   
                </div>
                <div class="form-group">
                    <label for="Status" class="control-label col-xs-2 col-md-2">Status</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList  runat="server" ID="Status" class="form-control required" ClientIDMode="Static">
                            <asp:ListItem Value="1" Text="Active" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="InActive" Selected="False"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label for="input-1" class="control-label col-xs-2 col-md-2">Upload Photo</label>
                    <div class="col-xs-10 col-md-2">
                        <%--<input id="input-1" type="file" class="file">--%>
                        <asp:Image ID="imgPreview" ClientIDMode="Static" runat="server" ImageUrl="/Images/user/preview.png" />
                        <asp:FileUpload id="inputUpload" ClientIDMode="Static" runat="server" class="file" data-show-upload="false" data-show-caption="true" />
                    </div>
                    <asp:HiddenField ID="UserId" runat="server" ClientIDMode="Static" />
                    <div class="col-xs-10 col-md-8">
                        <asp:Button ID="saveCompany" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right" Text="Save" OnClick="SaveClick" />
                        <a href="/Account/Users" class="btn btn-primary pull-right">Cancel</a>
                
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

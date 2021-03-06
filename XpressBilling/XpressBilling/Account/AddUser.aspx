﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="XpressBilling.Account.AddUser" %>
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
                    <span id="alreadyexistmsg" runat="server">UserName Already Exists</span>
                </div>
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
                        <asp:DropDownList  runat="server" ID="ddlEmployeeId" class="form-control required" ClientIDMode="Static">
                        </asp:DropDownList>
                    </div>

                </div>
                <div class="form-group">
                    <label for="Company" class="control-label col-xs-2 col-md-2">Company</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList  runat="server" ID="ddlCompany" class="form-control required" ClientIDMode="Static">
                        </asp:DropDownList>
                    </div>
                     <label for="Location" class="control-label col-xs-2 col-md-2">Location</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList  runat="server" ID="Location" class="form-control required" ClientIDMode="Static">
                        </asp:DropDownList>
                    </div>
                    <label for="DefLocation" class="control-label col-xs-2 col-md-2">Default Location</label>
                    <div class="col-xs-10 col-md-2">
                       <asp:DropDownList  runat="server" ID="DefLocation" class="form-control" ClientIDMode="Static">
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
                       <input id="btnCencelDtl" type="button" class="btn btn-primary pull-left" value="Cancel" onclick="location.href = '/Account/Users';" /> 
                       <asp:Button ID="saveCompany" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="SaveClick" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

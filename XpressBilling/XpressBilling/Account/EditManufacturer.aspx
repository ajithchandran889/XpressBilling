﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditManufacturer.aspx.cs" Inherits="XpressBilling.Account.EditManufacturer" %>
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
                <div class="page-header">Manufacturer Details</div>
                <div class="form-group">
                   
                    <label for="Manufacturer" class="control-label col-xs-2 col-md-2">Manufacturer</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Manufacturer" class="form-control required" placeholder="Manufacturer" ClientIDMode="Static"></asp:TextBox>                        
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
                        <label for="BusinessPartnerCode" runat="server" id="lblBusinessPartner" class="control-label col-xs-2 col-md-2">BusinessPartner</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="ManSupplierId" class="form-control required" placeholder="Customer Id" ClientIDMode="Static"></asp:TextBox>
                        <%--<asp:DropDownList runat="server" class="form-control" ID="ddlBusinessPartner" ClientIDMode="Static">                            
                        </asp:DropDownList>--%>
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
                    <asp:HiddenField ID="ManufacturerId" runat="server" />
                    <asp:HiddenField ID="mansupid" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hdncompanycode" runat="server" />
                    <asp:HiddenField ID="hdnreference" runat="server" />
                     <asp:HiddenField ID="CompanyCode" runat="server" ClientIDMode="Static" />
                    <div class="col-xs-10 col-md-8">
                        <%--<a href="/Account/Manufacturer.aspx" class="btn btn-primary pull-left">Cancel</a>--%>
                        <input id="cancelManufacturer" type="button" class="btn btn-primary pull-left" value="Cancel" onclick="location.href = '/Account/Manufacturer';" />
                        <asp:Button ID="saveManufacturer" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="SaveClick" />
                     </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

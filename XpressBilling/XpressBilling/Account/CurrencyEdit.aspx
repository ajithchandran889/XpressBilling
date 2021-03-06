﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CurrencyEdit.aspx.cs" Inherits="XpressBilling.Account.CurrencyEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div>
            <%--<asp:Label runat="server" ID="Message"></asp:Label>--%>
        </div>
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
                <div class="page-header">Currency Details</div>
                <div class="form-group">                    
                    <label for="Currency" class="control-label col-xs-2 col-md-2">Currency</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Currency" class="form-control required" placeholder="Currency" ClientIDMode="Static"></asp:TextBox>                        
                    </div><div class="col-xs-10 col-md-2"></div>
                    <label for="Date" id="lbldate" runat="server" class="control-label col-xs-2 col-md-2">Date</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="CreatedDate" class="form-control" ReadOnly="true" placeholder="Date" ClientIDMode="Static"></asp:TextBox>                        
                    </div> 
                </div>
                <div class="form-group">
                   
                    <label for="Name" class="control-label col-xs-2 col-md-2">Name</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Name" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>                        
                    </div>
                    <div class="col-xs-10 col-md-2"></div>
                    <label for="UserName" runat="server" ID="lblusername" placeholder="Name" class="control-label col-xs-2 col-md-2">User</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="UserName" class="form-control" ReadOnly="true" placeholder="User" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>

                </div>
                 <div class="form-group"> 
                    <label for="Decimal" class="control-label col-xs-2 col-md-2">No. Of Decimal</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Decimal" class="form-control required txtNumeric" placeholder="No. of Decimal" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                     <div class="col-xs-10 col-md-2">&nbsp;</div>
                    <label for="Status" runat="server" id="lblstatus" class="control-label col-xs-2 col-md-2">Status</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control required" ID="ddlStatus" ClientIDMode="Static">
                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>

                <div class="form-group">
                   
                    <asp:HiddenField ID="CurrencyId" runat="server" />
                    <div class="col-xs-10 col-md-8">
                        <%--<a href="/Account/Currency.aspx" class="btn btn-primary pull-left">Cancel</a>--%>
                        <input id="cancelCurrency" type="button" class="btn btn-primary pull-left" value="Cancel" onclick="location.href = '/Account/Currency';" />
                        <asp:Button ID="saveCurrency" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="SaveClick" />
                        <%--<asp:Label runat="server" style="color:red;" ID="Message"></asp:Label>--%>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

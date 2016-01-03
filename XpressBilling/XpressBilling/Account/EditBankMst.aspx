<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditBankMst.aspx.cs" Inherits="XpressBilling.Account.EditBankMst" %>
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
                <div class="page-header">BankCode Details</div>
                <h6>General Information</h6>

                <div class="form-group">
                    <label for="AccountNo" class="control-label col-xs-2 col-md-2">Account No</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="AccountNo" class="form-control required txtNumeric" placeholder="AccountNo" ClientIDMode="Static"></asp:TextBox>                        
                    </div>

                    <div class="col-xs-10 col-md-2"></div>

                    <label for="Date" id="lbldate" runat="server" class="control-label col-xs-2 col-md-2">Date</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="CreatedDate" class="form-control required" placeholder="Date" ClientIDMode="Static"></asp:TextBox>                        
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
                        <asp:TextBox runat="server" ID="UserName" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>                        
                    </div>
                </div>


                 <div class="form-group"> 
                    <label for="AccountType" runat="server" id="AccountType" class="control-label col-xs-2 col-md-2">Account Type</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control required" ID="ddlAccountType" ClientIDMode="Static">
                            <asp:ListItem Value="1" Text="Current"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Savings"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtaccounttype" class="form-control required" placeholder="txtaccounttype" ClientIDMode="Static"></asp:TextBox>
                    </div><div class="col-xs-10 col-md-2">&nbsp;</div>
                    <label for="Status" runat="server" id="lblstatus" class="control-label col-xs-2 col-md-2">Status</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control required" ID="ddlStatus" ClientIDMode="Static">
                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">                   
                    <label for="Bank" class="control-label col-xs-2 col-md-2">Bank</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control required" ID="ddlbankcode" ClientIDMode="Static">                            
                        </asp:DropDownList>                        
                    </div>
                    <div class="col-xs-10 col-md-2"></div>
                    <div class="col-xs-10 col-md-2"></div>
                </div>


                <div class="form-group">                   
                    <label for="Branch" class="control-label col-xs-2 col-md-2">Branch</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Branch" class="form-control required" placeholder="Branch" ClientIDMode="Static"></asp:TextBox>                        
                    </div>
                    <div class="col-xs-10 col-md-2"></div>
                    <div class="col-xs-10 col-md-2"></div>
                </div>
                <h6>More Information</h6>

                <div class="form-group">
                    <label for="IBAN" class="control-label col-xs-2 col-md-2">IBAN</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="IBAN" class="form-control" placeholder="IBAN" ClientIDMode="Static"></asp:TextBox>                        
                    </div>
                    <div class="col-xs-10 col-md-2"></div>
                    <div class="col-xs-10 col-md-2"></div>  
                </div>

                <div class="form-group">
                    <label for="IFSC" class="control-label col-xs-2 col-md-2">IFSC</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="IFSC" class="form-control required" placeholder="IFSC" ClientIDMode="Static"></asp:TextBox>                        
                    </div>
                    <div class="col-xs-10 col-md-2"></div>
                    <div class="col-xs-10 col-md-2"></div>  
                </div>

                <div class="form-group">
                    <label for="SWIFT" class="control-label col-xs-2 col-md-2">SWIFT</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="SWIFT" class="form-control" placeholder="SWIFT" ClientIDMode="Static"></asp:TextBox>                        
                    </div>
                    <div class="col-xs-10 col-md-2"></div>
                    <div class="col-xs-10 col-md-2"></div>  
                </div>

                <div class="form-group">
                    <label for="MICR" class="control-label col-xs-2 col-md-2">MICR</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="MICR" class="form-control" placeholder="MICR" ClientIDMode="Static"></asp:TextBox>                        
                    </div>
                    <div class="col-xs-10 col-md-2"></div>
                    <div class="col-xs-10 col-md-2"></div>  
                </div>

                <div class="form-group">
                    <label for="Reference" class="control-label col-xs-2 col-md-2">Reference</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Reference" class="form-control" placeholder="Reference" ClientIDMode="Static"></asp:TextBox>                        
                    </div>
                    <div class="col-xs-10 col-md-2"></div>
                    <div class="col-xs-10 col-md-2"></div>  
                </div>

                <div class="form-group">                    
                    <asp:HiddenField ID="hdnBankCode" runat="server" />
                    <asp:HiddenField ID="hdncompanycode" runat="server" />
                    <div class="col-xs-10 col-md-8">
                        <a href="/Account/BankMst.aspx" class="btn btn-primary pull-left">Cancel</a><asp:Button ID="saveBankMst" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="SaveClick" />
                    </div>
                </div>


            </div>
        </div>
    </div>
</asp:Content>

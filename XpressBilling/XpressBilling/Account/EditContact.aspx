<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditContact.aspx.cs" Inherits="XpressBilling.Account.EditAccount" %>
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
                <div class="page-header">Contact Details</div>
                <div class="form-group">
                    <label for="Contact" class="control-label col-xs-2 col-md-2">Contact</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Contact" name="Contact" class="form-control required" placeholder="Contact" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label for="Name" class="control-label col-xs-2 col-md-2">Name</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Name" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                </div>

                <div class="form-group">
                <label for="Designation" class="control-label col-xs-2 col-md-2">Designation</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Designation" class="form-control required" placeholder="Designation" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label for="Date" id="lblDate" runat="server" class="control-label col-xs-2 col-md-2">Date</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Date" class="form-control" ReadOnly="true" placeholder="Date" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>    
                </div>

                <div class="form-group">
                    <label for="Company" class="control-label col-xs-2 col-md-2">Company</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control" ID="ddlCompany" ClientIDMode="Static">
                        </asp:DropDownList>                        
                    </div>
                    <label for="UserName" runat="server" ID="lblusername" placeholder="UserName"  class="control-label col-xs-2 col-md-2">User</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Username" class="form-control" ReadOnly="true" placeholder="User" ClientIDMode="Static"></asp:TextBox>
                    </div>
                  </div>

                
                 <div class="form-group"><div></div></div>
                <div class="form-group"><div></div></div>
                <div class="form-group">
                    <label for="Phone" class="control-label col-xs-2 col-md-2">Phone</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Phone" class="form-control txtNumeric" placeholder="Phone" ClientIDMode="Static"></asp:TextBox>                        
                    </div>
                    <label for="Mobile" class="control-label col-xs-2 col-md-2">Mobile</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Mobile" class="form-control required txtNumeric" placeholder="Mobile" ClientIDMode="Static"></asp:TextBox>                        
                    </div>
                </div>
                <div class="form-group">
                    <label for="Fax" class="control-label col-xs-2 col-md-2">Fax</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Fax" class="form-control" placeholder="Fax" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label for="Email" class="control-label col-xs-2 col-md-2">Email</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Email" class="form-control required email" placeholder="Email" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="Web" class="control-label col-xs-2 col-md-2">Web</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Web" class="form-control" placeholder="Web" ClientIDMode="Static"></asp:TextBox>
                    </div>                        
                    </div>
                <div class="form-group"><div></div><div></div></div>
                <div class="form-group"><div></div><div></div></div>
                <div class="form-group">
                    <label for="Address1" class="control-label col-xs-2 col-md-2">Address Street 1</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Address1" class="form-control" placeholder="Address Street 1" ClientIDMode="Static"></asp:TextBox>
                       
                    </div>
                    <label for="Address2" class="control-label col-xs-2 col-md-2">Address Street 2</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Address2" class="form-control" placeholder="Address Street 2" ClientIDMode="Static"></asp:TextBox>
                     </div>
                </div>
                <div class="form-group">
                    <label for="Area" class="control-label col-xs-2 col-md-2">Area</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Area" class="form-control required" placeholder="Area" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label  class="control-label col-xs-2 col-md-2">Country</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control required" ID="Country" ClientIDMode="Static">
                        </asp:DropDownList>
                    </div>
                  </div>
                <div class="form-group">
                    <label for="State" class="control-label col-xs-2 col-md-2">State</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="State" class="form-control" placeholder="State" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="City" class="control-label col-xs-2 col-md-2">City</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control" ID="City" ClientIDMode="Static" >
                        </asp:DropDownList>
                        
                    </div>
                </div>

                <div class="form-group">
                    <label for="Zip" class="control-label col-xs-2 col-md-2">Zip/Postal Code</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Zip" class="form-control required txtNumeric" placeholder="Zip" ClientIDMode="Static"></asp:TextBox>
                    </div>                    
                </div> 
                <div class="form-group">
                    <label for="Status" runat="server" id="lblstatus" class="control-label col-xs-2 col-md-2">Status</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control required" ID="ddlStatus" ClientIDMode="Static">
                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                    <div class="form-group">
                        <asp:HiddenField ID="ContactId" runat="server" />
                    <asp:HiddenField ID="CompanyId" runat="server" />
                    <div class="col-xs-10 col-md-8">
                        <a href="/Account/Contact.aspx" class="btn btn-primary pull-left">Cancel</a><asp:Button ID="saveCompany" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="SaveClick" />
             <label id="lblMsg" style="color:red;" runat="server"></label>
                
                    </div>

                </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

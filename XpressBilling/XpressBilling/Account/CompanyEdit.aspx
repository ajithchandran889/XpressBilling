﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="CompanyEdit.aspx.cs" Inherits="XpressBilling.Account.CompanyEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div>
            <asp:Label runat="server" ID="Message"></asp:Label>
        </div>
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div class="page-header">Add Company Details</div>
                <div class="form-group">
                    <label for="Company" class="control-label col-xs-2 col-md-2">Company</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Company" name="Company" class="form-control required" placeholder="Company" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label for="Name" class="control-label col-xs-2 col-md-2">Name</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Name" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="FormationDate" class="control-label col-xs-2 col-md-2">Formation Date</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="FormationDate" class="form-control required" placeholder="Formation Date" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>

                </div>
                <div class="form-group">
                    <label for="TIN" class="control-label col-xs-2 col-md-2">TIN</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="TIN" class="form-control required" placeholder="TIN" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="PAN" class="control-label col-xs-2 col-md-2">PAN</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="PAN" class="form-control required" placeholder="PAN" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="RegistrationNo" class="control-label col-xs-2 col-md-2">Registration</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="RegistrationNo" class="form-control required" placeholder="Registration No:" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>

                </div>
                 <div class="form-group">
                    <label for="ContactPerson" class="control-label col-xs-2 col-md-2">Contact Person</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="ContactPerson" class="form-control required" placeholder="Contact Person" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    

                </div>
                <div class="form-group">
                    <label for="Mobile" class="control-label col-xs-2 col-md-2">Mobile</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Mobile" class="form-control required" placeholder="Mobile" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="Phone" class="control-label col-xs-2 col-md-2">Phone</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Phone" class="form-control required" placeholder="Phone" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="Email" class="control-label col-xs-2 col-md-2">Email</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Email" class="form-control required email" placeholder="Email" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                </div>
                <div class="form-group">
                    <label for="Fax" class="control-label col-xs-2 col-md-2">Fax</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Fax" class="form-control required" placeholder="Fax" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="Web" class="control-label col-xs-2 col-md-2">Web</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Web" class="form-control required" placeholder="Web" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    
                    <label for="Designation" class="control-label col-xs-2 col-md-2">Designation</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Designation" class="form-control required" placeholder="Designation" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                </div>
                <div class="form-group">
                    <label for="Address1" class="control-label col-xs-2 col-md-2">Address Street 1</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Address1" class="form-control required" placeholder="Address Street 1" ClientIDMode="Static"></asp:TextBox>
                       
                    </div>
                    <label for="Address2" class="control-label col-xs-2 col-md-2">Address Street 2</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Address2" class="form-control required" placeholder="Address Street 2" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    
                    <label  class="control-label col-xs-2 col-md-2">Country</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control required" ID="Country" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Select Country"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <label for="Area" class="control-label col-xs-2 col-md-2">Area</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Area" class="form-control required" placeholder="Area" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="State" class="control-label col-xs-2 col-md-2">State</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="State" class="form-control required" placeholder="State" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="City" class="control-label col-xs-2 col-md-2">City</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control required" ID="City" ClientIDMode="Static">
                        </asp:DropDownList>
                        
                    </div>
                </div>

                <div class="form-group">
                    <label for="Zip" class="control-label col-xs-2 col-md-2">Zip</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Zip" class="form-control required" placeholder="Zip" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label for="Note" class="control-label col-xs-2 col-md-2">Note</label>
                    <div class="col-xs-10 col-md-6">
                        <asp:TextBox runat="server" ID="Note" class="form-control required" placeholder="Note" ClientIDMode="Static"></asp:TextBox>
                    </div>

                </div>
                <div class="form-group">
                    <label for="input-1" class="control-label col-xs-2 col-md-2">Upload Logo</label>
                    <div class="col-xs-10 col-md-2">
                        <%--<input id="input-1" type="file" class="file">--%>
                        <asp:FileUpload id="logoUpload" runat="server" class="required" />
                    </div>
                    <asp:HiddenField ID="CompanyId" runat="server" />
                    <div class="col-xs-10 col-md-8">
                        <asp:Button ID="saveCompany" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right" Text="Save" OnClick="SaveClick" />
                        <a href="/Account/Company.aspx" class="btn btn-primary pull-right">Cancel</a>
                
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterCompany.aspx.cs" Inherits="XpressBilling.Account.RegisterCompany" %>
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
                    <label for="Note" class="control-label col-xs-2 col-md-2">Note</label>
                    <div class="col-xs-10 col-md-6">
                        <asp:TextBox runat="server" ID="Note" class="form-control required" placeholder="Note" ClientIDMode="Static"></asp:TextBox>
                    </div>

                </div>
                <div class="form-group">
                    
                    <div class="col-xs-12 col-md-12">
                        <asp:Button ID="saveCompany" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right" Text="Next" OnClick="SaveCompanyClick" />
                
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RptSalesInvoice.aspx.cs" Inherits="XpressBilling.Account.RptSalesInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
        //MainContent_
        function ToogleHeader() {
            if (document.getElementById("HeaderCB").checked) {
                document.getElementById("Header").readOnly = false;
            }
            else {
                document.getElementById("Header").readOnly = true;
            }
        }
        function ToogleDeclaration() {
            if (document.getElementById("DeclarationCB").checked) {
                document.getElementById("Declaration").readOnly = false;
            }
            else {
                document.getElementById("Declaration").readOnly = true;
            }
        }
        function ToogleFooter() {
            if (document.getElementById("FooterCB").checked) {
                document.getElementById("Footer").readOnly = false;
            }
            else {
                document.getElementById("Footer").readOnly = true;
            }
        }
        function ToogleBank() {
            if (document.getElementById("BankCB").checked) {
                document.getElementById("txtAccNoid").readOnly = false;
            }
            else {
                document.getElementById("txtAccNoid").readOnly = true;
            }
        }
    </script>

    
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-sm-12 col-md-12">
                <div id="SaveSuccess" visible="false" class="alert alert-success" role="alert" runat="server">
                    <span runat="server">
                        <img src="~/Images/like.png" alt="" runat="server" />
                    </span>
                    Saved Successfully
                </div>
                <div id="UpdateSuccess" visible="false" class="alert alert-success" role="alert" runat="server">
                    <span runat="server">
                        <img src="~/Images/like.png" alt="" runat="server" />
                    </span>
                    Updated Successfully
                </div>
                <div id="failure" visible="false" class="alert alert-danger" role="alert" runat="server">
                    <span id="failureMessage" runat="server">Sorry,Something went wrong!</span>
                </div>
                <%-- <div id="alreadyexist" visible="false" class="alert alert-danger" role="alert" runat="server">
                    <span id="alreadyexistmsg" runat="server">UserName Already Exists</span>
                </div>--%>
                <div class="page-header">Sales Invoice Report Parameters</div>
                <div class="form-group">
                    <label for="HeaderCB" class="control-label col-xs-2 col-md-2">Header</label>
                    <div class="col-xs-10 col-md-1">
                        <asp:CheckBox Checked="false" runat="server" ID="HeaderCB" name="HeaderCB"  onchange="javascript: ToogleHeader()" ClientIDMode="Static"></asp:CheckBox>
                    </div>
                    <div class="col-xs-10 col-md-6">
                        <asp:TextBox runat="server" ID="Header" name="Header" class="form-control" ReadOnly="true" placeholder="Header" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="DeclarationCB" class="control-label col-xs-2 col-md-2">Declaration</label>
                    <div class="col-xs-10 col-md-1">
                        <asp:CheckBox Checked="false" runat="server" ID="DeclarationCB" name="DeclarationCB" onchange="javascript: ToogleDeclaration()" ClientIDMode="Static"></asp:CheckBox>
                    </div>
                    <div class="col-xs-10 col-md-6">
                        <asp:TextBox runat="server" ID="Declaration" name="Declaration" ReadOnly="true" class="form-control" placeholder="Declaration" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="FooterCB" class="control-label col-xs-2 col-md-2">Footer</label>
                    <div class="col-xs-10 col-md-1">
                        <asp:CheckBox Checked="false" runat="server" ID="FooterCB" name="FooterCB" onchange="javascript: ToogleFooter()" ClientIDMode="Static"></asp:CheckBox>
                    </div>
                    <div class="col-xs-10 col-md-6">
                        <asp:TextBox runat="server" ID="Footer" name="Footer" ReadOnly="true" class="form-control" placeholder="Footer" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="BankCB" class="control-label col-xs-2 col-md-2">Bank</label>
                    <div class="col-xs-10 col-md-1">
                        <asp:CheckBox Checked="false" runat="server" ID="BankCB" name="FooterCB" onchange="javascript: ToogleBank()" ClientIDMode="Static"></asp:CheckBox>
                    </div>
                    <div class="col-xs-10 col-md-3">
                        <asp:TextBox runat="server" ID="txtAccNoid" name="txtAccNoid" ReadOnly="true" class="form-control" placeholder="Account No" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <%-- Add code for auto lookup --%>
                    <label for="BankCode" id="lblBankCode" runat="server" class="control-label col-xs-2 col-md-2"></label>
                </div>
                <div class="form-group">
                    <label for="NoOfCopies" class="control-label col-xs-2 col-md-2">Number of Copies</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="NoOfCopies" name="NoOfCopies" class="form-control txtNumeric" placeholder="No Of Copies" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="Report" class="control-label col-xs-2 col-md-2">Report</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" ID="Report" class="form-control" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="TEST" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <%--  <div class="form-group">
                    <label for="Header" class="control-label col-xs-2 col-md-2">Header</label>
                    
                </div>
                <div class="form-group">
                    <label for="Declaration" class="control-label col-xs-2 col-md-2">Declaration</label>
                   
                </div>
                <div class="form-group">
                    <label for="Footer" class="control-label col-xs-2 col-md-2">Footer</label>
                   
                </div>
                <div class="form-group">
                    <label for="AccountNo" class="control-label col-xs-2 col-md-2">Account No</label>
                  
                </div>--%>
                <div class="form-group">
                    <asp:HiddenField ID="AccNoid" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="UserId" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="CompanyCode" runat="server" ClientIDMode="Static" />
                    <div class="col-xs-10 col-md-8">
                        <a href="/Account/RptSalesInvoice" class="btn btn-primary pull-left">Clear</a>
                        <asp:Button ID="saveCompany" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="SaveClick" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

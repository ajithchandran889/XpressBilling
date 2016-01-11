<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BussinessPartnerEdit.aspx.cs" Inherits="XpressBilling.Account.BussinessPartnerEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div>
            <asp:Label runat="server" ID="Message"></asp:Label>
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
                    <span id="alreadyexistmsg" runat="server">Already Exists</span>
                </div>
                <div class="page-header">Bussiness Partner</div>
                <div class="form-group">
                    <label for="BussinessPartner" class="control-label col-xs-2 col-md-2">Bussiness Partner</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="BussinessPartner" name="BussinessPartner" class="form-control required" placeholder="Bussiness Partner" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label for="Name" class="control-label col-xs-2 col-md-2">Name</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Name" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>

                    </div>
                </div>
                <div class="form-group">
                    <label for="BusinessPartnerType" class="control-label col-xs-2 col-md-2">Business Partner Type</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" ID="BusinessPartnerType" class="form-control required" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Supplier"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Customer"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label for="OrderType" class="control-label col-xs-2 col-md-2">Order Type</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" ID="OrderType_0" class="form-control required" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Local"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Import"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" ID="OrderType_1" class="form-control required"  Style="display: none"  ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Cash"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Credit"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="Discount" class="control-label col-xs-2 col-md-2">Discount</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Discount" class="form-control txtNumeric" placeholder="Discount" ClientIDMode="Static"></asp:TextBox>

                    </div>
                    <label for="CreditLimit" class="control-label col-xs-2 col-md-2">Credit Limit</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="CreditLimit" class="form-control required txtNumeric" placeholder="Credit Limit" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="Tin" class="control-label col-xs-2 col-md-2">TIN</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Tin" class="form-control required" placeholder="TIN" ClientIDMode="Static"></asp:TextBox>

                    </div>
                    <label for="Cst" class="control-label col-xs-2 col-md-2">CST</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Cst" class="form-control" placeholder="CST" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                 <hr>
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
                    <label for="ContactPerson" class="control-label col-xs-2 col-md-2">Contact Person</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="ContactPerson" class="form-control required" placeholder="Contact Person" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
               <%-- <div class="form-group">
                    <div></div>
                    <div></div>
                </div>--%>
                <div class="form-group">
                    <label for="Designation" class="control-label col-xs-2 col-md-2">Designation</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Designation" class="form-control required" placeholder="Designation" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                 <hr>
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
                    <label class="control-label col-xs-2 col-md-2">Country</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control required" ID="Country" ClientIDMode="Static">
                            <asp:ListItem Value="" Text="Select Country"></asp:ListItem>
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
                            <asp:ListItem Value="" Text="Select City"></asp:ListItem>
                        </asp:DropDownList>

                    </div>
                </div>

                <div class="form-group">
                    <label for="Zip" class="control-label col-xs-2 col-md-2">Zip/Postal Code</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Zip" class="form-control required txtNumeric" MaxLength="6" placeholder="Zip" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                 <hr>
                <div class="form-group">
                    <label for="Note" class="control-label col-xs-2 col-md-2">Note</label>
                    <div class="col-xs-10 col-md-6">
                        <asp:TextBox runat="server" ID="Note" class="form-control" placeholder="Note" ClientIDMode="Static"></asp:TextBox>
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
                    <asp:HiddenField ID="BPId" runat="server" />
                    <div class="col-xs-10 col-md-8">
                        <%--<a href="/Account/BussinessPartner.aspx" class="btn btn-primary pull-left">Cancel</a>--%>
                        <input id="cancelBussinessPartner" type="button" class="btn btn-primary pull-left" value="Cancel" onclick="location.href = '/Account/BussinessPartner';" />
                        <asp:Button ID="saveBussinessPartner" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="SaveClick" />
                     </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

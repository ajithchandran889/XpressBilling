<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LocationEdit.aspx.cs" Inherits="XpressBilling.Account.LocationEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                <div id="alreadyexist" visible="false" class="alert alert-danger" role="alert" runat="server">
                    <span id="alreadyexistmsg" runat="server">Code Already Exists</span>
                </div>
                <div class="page-header">Location Details</div>
                <div class="form-group">
                    <label for="Location" class="control-label col-xs-2 col-md-2">Location</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Location" name="Location" class="form-control required" placeholder="Location" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label for="Name" class="control-label col-xs-2 col-md-2">Name</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Name" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>

                    </div>


                </div>
                <div class="form-group">
                    <label for="FormationDate" class="control-label col-xs-2 col-md-2">Formation Date</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="FormationDate" class="form-control required" placeholder="Formation Date" ClientIDMode="Static"></asp:TextBox>

                    </div>
                    <label for="lblRegistrationNo" class="control-label col-xs-2 col-md-2">Registration Number</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="RegistrationNo" class="form-control txtNumeric" placeholder="Registration Number" ClientIDMode="Static"></asp:TextBox>

                    </div>
                </div>
                <div class="form-group">
                    <label for="TIN" class="control-label col-xs-2 col-md-2">TIN</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="TIN" class="form-control" placeholder="TIN" ClientIDMode="Static"></asp:TextBox>

                    </div>
                    <label for="PAN" class="control-label col-xs-2 col-md-2">PAN</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="PAN" class="form-control" placeholder="PAN" ClientIDMode="Static"></asp:TextBox>

                    </div>

                </div>
                 <hr>
                <%--<div class="form-group"></div>
                <div class="form-group"></div>--%>
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
                <div class="form-group">
                    <label for="Designation" class="control-label col-xs-2 col-md-2">Designation</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Designation" class="form-control required" placeholder="Designation" ClientIDMode="Static"></asp:TextBox>

                    </div>
                </div> <hr>
               <%-- <div class="form-group"></div>
                <div class="form-group"></div>--%>
                <div class="form-group">
                    <label for="Address1" class="control-label col-xs-2 col-md-2">Address Street 1</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Address1" class="form-control required" placeholder="Address Street 1" ClientIDMode="Static"></asp:TextBox>

                    </div>
                    <label for="Address2" class="control-label col-xs-2 col-md-2">Address Street 2</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Address2" class="form-control required" placeholder="Address Street 2" ClientIDMode="Static"></asp:TextBox>

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
                        <asp:TextBox runat="server" ID="State" class="form-control required" placeholder="State" ClientIDMode="Static"></asp:TextBox>

                    </div>
                    <label for="lblCityCountry" class="control-label col-xs-2 col-md-2">City</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control required" ID="City" ClientIDMode="Static">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="form-group">
                    <label for="Zip" class="control-label col-xs-2 col-md-2">Zip/Postal Code</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Zip" MaxLength="6" class="form-control required txtNumeric" placeholder="Zip" ClientIDMode="Static"></asp:TextBox>
                    </div>

                </div> <hr>
                <%--<div class="form-group"></div>
                <div class="form-group"></div>--%>
                <div class="form-group">
                    <label for="Note" class="control-label col-xs-2 col-md-2">Note</label>
                    <div class="col-xs-10 col-md-6">
                        <asp:TextBox runat="server" ID="Note" class="form-control" placeholder="Note" ClientIDMode="Static"></asp:TextBox>
                    </div>

                </div>
                <div class="form-group">
                    <label for="input-1" class="control-label col-xs-2 col-md-2">Upload Logo</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:Image ID="imgPreview" ClientIDMode="Static" runat="server" ImageUrl="/Images/user/preview.png" />
                        <asp:FileUpload id="inputUpload" ClientIDMode="Static" runat="server" class="file" data-show-upload="false" data-show-caption="true" />
                    </div>
                    <asp:HiddenField ID="LocationId" runat="server" />
                    <asp:HiddenField ID="ContactId" runat="server" />
                    <asp:HiddenField ID="CompanyId" runat="server" />
                </div>
                <div class="form-group">                    
                    <label for="Status" runat="server" id="lblstatus" class="control-label col-xs-2 col-md-2">Status</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:DropDownList runat="server" class="form-control required" ID="ddlStatus" ClientIDMode="Static">
                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-10 col-md-2">
                    </div>
                    <div class="col-xs-10 col-md-2"></div>
                    <div class="col-xs-10 col-md-2">&nbsp;</div>
                </div>
                <div class="form-group">
                    <div class="col-xs-10 col-md-8">
                        <a href="/Account/Location.aspx" class="btn btn-primary pull-left">Cancel</a>

                        <asp:Button ID="saveCompany" runat="server" ClientIDMode="Static" CssClass="btn btn-primary pull-left" Text="Save" OnClick="SaveClick" />

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


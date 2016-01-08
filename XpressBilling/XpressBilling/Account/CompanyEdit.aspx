<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="CompanyEdit.aspx.cs" Inherits="XpressBilling.Account.CompanyEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <%--<div>
            <asp:Label runat="server" ID="Message"></asp:Label>
        </div>--%>
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
                
                <div class="page-header">Company Details</div>
                <div class="col-sm-12 col-md-8">
                <div class="form-group">
                    <label for="Company" class="control-label col-xs-12  col-md-3">Company</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="AddCompany" name="Company" class="form-control required" placeholder="Company" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label for="Name" class="control-label col-xs-12  col-md-3">Name</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="Name" ValidationGroup="save" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                </div>
                <div class="form-group">
                    <label for="FormationDate" class="control-label col-xs-12  col-md-3">Formation Date</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="FormationDate" ValidationGroup="save" class="form-control" ReadOnly="true" placeholder="Formation Date" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="RegistrationNo" class="control-label col-xs-12  col-md-3">Registration Number</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="RegistrationNo" ValidationGroup="save" class="form-control txtNumeric" placeholder="Registration No:" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
               </div>
                <div class="form-group">
                    <label for="TIN" class="control-label col-xs-12  col-md-3">TIN</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="TIN" class="form-control" ValidationGroup="save" placeholder="TIN" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="PAN" class="control-label col-xs-12  col-md-3">PAN</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="PAN" class="form-control" ValidationGroup="save" placeholder="PAN" ClientIDMode="Static"></asp:TextBox>                        
                    </div>
                </div>
                <div class="form-group">
                    <label for="Currency" class="control-label col-xs-12  col-md-3">Currency</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:DropDownList runat="server" class="form-control required" ValidationGroup="save" ID="ddlCurrency" ClientIDMode="Static">
                           <%-- <asp:ListItem Value="" Text="Select Currency"></asp:ListItem>--%>
                        </asp:DropDownList>
                        
                    </div>                   
                    <div class="col-sm-12 col-md-3 col-lg-3">                                               
                    </div>
                    
                </div>
                <hr>
                 <div class="form-group">
                    <label for="ContactPerson" class="control-label col-xs-12  col-md-3">Contact Person</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="ContactPerson" ValidationGroup="save" class="form-control required" placeholder="Contact Person" ClientIDMode="Static"></asp:TextBox>
                    </div>
                     <label for="Designation" class="control-label col-xs-12  col-md-3">Designation</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="Designation" class="form-control required" ValidationGroup="save" placeholder="Designation" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="Phone" class="control-label col-xs-12  col-md-3">Phone</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="Phone" class="form-control txtNumeric" ValidationGroup="save" placeholder="Phone" ClientIDMode="Static"></asp:TextBox>                        
                    </div>
                    <label for="Mobile" class="control-label col-xs-12  col-md-3">Mobile</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="Mobile" class="form-control required txtNumeric" ValidationGroup="save" placeholder="Mobile" ClientIDMode="Static"></asp:TextBox>                        
                    </div>
                </div>
                <div class="form-group">
                    <label for="Fax" class="control-label col-xs-12  col-md-3">Fax</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="Fax" class="form-control" placeholder="Fax" ValidationGroup="save" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label for="Email" class="control-label col-xs-12  col-md-3">Email</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="Email" class="form-control required email" ValidationGroup="save" placeholder="Email" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                    <div class="form-group">
                    <label for="Web" class="control-label col-xs-12  col-md-3">Web</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="Web" class="form-control" ValidationGroup="save" placeholder="Web" ClientIDMode="Static"></asp:TextBox>
                    </div>
                         
                    </div>
               <hr />
                <div class="form-group"><div></div><div></div></div>
                <div class="form-group">
                    <label for="Address1" class="control-label col-xs-12  col-md-3">Address Street 1</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="Address1" ValidationGroup="save" class="form-control required" placeholder="Address Street 1" ClientIDMode="Static"></asp:TextBox>
                       
                    </div>
                    <label for="Address2" class="control-label col-xs-12  col-md-3">Address Street 2</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="Address2" ValidationGroup="save" class="form-control required" placeholder="Address Street 2" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                </div>

                <div class="form-group">
                    <label for="Area" class="control-label col-xs-12  col-md-3">Area</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="Area" ValidationGroup="save" class="form-control required" placeholder="Area" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label  class="control-label col-xs-12  col-md-3">Country</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:DropDownList runat="server" ValidationGroup="save" class="form-control required" ID="Country" ClientIDMode="Static">
                            <asp:ListItem Value="" Text="Select Country"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                  </div>
                <div class="form-group">
                    <label for="State" class="control-label col-xs-12  col-md-3">State</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ValidationGroup="save" ID="State" class="form-control required" placeholder="State" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>
                    <label for="City" class="control-label col-xs-12  col-md-3">City</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:DropDownList runat="server" ValidationGroup="save" class="form-control required" ID="City" ClientIDMode="Static" >
                        </asp:DropDownList>
                        
                    </div>
                </div>

                <div class="form-group">
                    <label for="Zip" class="control-label col-xs-12  col-md-3">Zip/Postal Code</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:TextBox runat="server" ID="Zip" ValidationGroup="save" class="form-control required txtNumeric" MaxLength="6" placeholder="Zip" ClientIDMode="Static"></asp:TextBox>
                    </div>  
                                      
                </div>
                <hr> 
                <div class="form-group">
                <label for="Note" class="control-label col-xs-12  col-md-3">Note</label>
                    <div class="col-xs-10 col-md-6">
                        <asp:TextBox runat="server" ID="Note" ValidationGroup="save" class="form-control" placeholder="Note" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    </div>
             
                <div class="form-group">                    
                    <label for="Status" runat="server" id="lblstatus" class="control-label col-xs-12  col-md-3">Status</label>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <asp:DropDownList runat="server" ValidationGroup="save" class="form-control required" ID="ddlStatus" ClientIDMode="Static">
                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                            <asp:ListItem Value="0" Text="InActive"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-3 col-lg-3">
                    </div>
                    <div class="col-sm-12 col-md-3 col-lg-3"></div>
                    <div class="col-sm-12 col-md-3 col-lg-3">&nbsp;</div>
                </div>
                     </div>
                <div class="col-xs-10 col-md-4">
                       <div class="form-group">
                    <label for="input-1" class="control-label col-xs-12  col-md-12">Upload Logo</label>
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <asp:Image ID="imgPreview" ClientIDMode="Static" runat="server" ImageUrl="/Images/user/preview.png" />
                        <asp:FileUpload id="inputUpload" ValidationGroup="save" ClientIDMode="Static" runat="server" class="file" data-show-upload="false" data-show-caption="true" />
                    </div>
                    </div>

                </div>
                    <div class="form-group">
                    <asp:HiddenField ID="CompanyId" runat="server" />
                    <div class="col-xs-10 col-md-8">
                       <a href="/Account/Company.aspx" class="btn btn-primary pull-left">Cancel</a> 
                      
                        <%--<asp:Button ID="btncancel" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Cancel" PostBackUrl="/Account/Company.aspx"  />--%>
                        <asp:Button ValidationGroup="save" ID="saveCompany" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="SaveClick" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

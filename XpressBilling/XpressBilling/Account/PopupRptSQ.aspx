<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PopupRptSQ.aspx.cs" Inherits="XpressBilling.Account.PopupRptSQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        function fnCancelPopUp() {
            //debugger;
            //var ds = parent.window.document.getElementById("PopupTest");
            //alert(ds);

            window.close();
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
                </div></div>
                <%--<div id="alreadyexist" visible="false" class="alert alert-danger" role="alert" runat="server">
                    <span id="alreadyexistmsg" runat="server">Code Already Exists</span>
                </div>--%>
                <div class="page-header">Sales Quotation Report Terms and conditions</div>
                <div class="form-group">

                    <label for="Country" class="control-label col-xs-2 col-md-2">Heading</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="Country" class="form-control required" placeholder="Heading" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-xs-10 col-md-2"></div>
                    <%--<label for="Date" id="lbldate" runat="server" class="control-label col-xs-2 col-md-2">Date</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="CreatedDate" class="form-control required" placeholder="Date" ClientIDMode="Static"></asp:TextBox>--%>
                </div>

            
            <div class="form-group">

                <label for="Name" class="control-label col-xs-2 col-md-2">Line1</label>
                <div class="col-xs-10 col-md-2">
                    <asp:TextBox runat="server" ID="Name" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-xs-10 col-md-2"></div>
                <%--<label for="UserName" runat="server" ID="lblusername" placeholder="username" class="control-label col-xs-2 col-md-2">User</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="UserName" class="form-control required" placeholder="User" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>--%>
            </div>

            <div class="form-group">

                <label for="Name" class="control-label col-xs-2 col-md-2">Line2</label>
                <div class="col-xs-10 col-md-2">
                    <asp:TextBox runat="server" ID="TextBox1" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-xs-10 col-md-2"></div>
                <%--<label for="UserName" runat="server" ID="lblusername" placeholder="username" class="control-label col-xs-2 col-md-2">User</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="UserName" class="form-control required" placeholder="User" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>--%>
            </div>
            <div class="form-group">

                <label for="Name" class="control-label col-xs-2 col-md-2">Line3</label>
                <div class="col-xs-10 col-md-2">
                    <asp:TextBox runat="server" ID="TextBox2" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-xs-10 col-md-2"></div>
                <%--<label for="UserName" runat="server" ID="lblusername" placeholder="username" class="control-label col-xs-2 col-md-2">User</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="UserName" class="form-control required" placeholder="User" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>--%>
            </div>
            <div class="form-group">

                <label for="Name" class="control-label col-xs-2 col-md-2">Line4</label>
                <div class="col-xs-10 col-md-2">
                    <asp:TextBox runat="server" ID="TextBox3" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-xs-10 col-md-2"></div>
                <%--<label for="UserName" runat="server" ID="lblusername" placeholder="username" class="control-label col-xs-2 col-md-2">User</label>
                    <div class="col-xs-10 col-md-2">
                        <asp:TextBox runat="server" ID="UserName" class="form-control required" placeholder="User" ClientIDMode="Static"></asp:TextBox>
                        
                    </div>--%>
            </div>

            <div class="form-group">
                <asp:HiddenField ID="CountryId" runat="server" />
                <asp:HiddenField ID="hdncompanycode" runat="server" />
                <div class="col-xs-10 col-md-8">
                    <input id="cancelPopupRptSQ" type="button" class="btn btn-primary pull-left" value="Cancel" onclick="javascript: fnCancelPopUp();" />
                    <asp:Button ID="savePopupRptSQ" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="SaveClick" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

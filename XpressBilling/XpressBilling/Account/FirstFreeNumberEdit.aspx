<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FirstFreeNumberEdit.aspx.cs" Inherits="XpressBilling.Account.FirstFreeNumberEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <div id="SaveSuccess" visible="false" class="alert alert-success" role="alert" runat="server">
                    <span runat="server"><img src="~/Images/like.png" alt="" runat="server" />	</span>
                    Saved Successfully
                </div>
                <div id="failure" visible="false" class="alert alert-danger" role="alert" runat="server">
                    <span id="failureMessage" runat="server">Sorry,Something went wrong!</span>
                </div>
                <div class="page-header">First Free Number</div>
                <div class="form-group">
                    <div class="form-group">
                        <label class="control-label col-xs-12 col-sm-4 col-md-2">Type</label>
                        <div class="col-xs-12 col-sm-8 col-md-2">
                            <asp:DropDownList runat="server" ID="Type" class="form-control required" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="TypeSelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Company"></asp:ListItem>
                                <asp:ListItem Value="1" Text="EU"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <label class="control-label col-xs-12 col-sm-4 col-md-2">Number Group</label>
                        <div class="col-xs-12 col-sm-8 col-md-2">
                            <asp:TextBox runat="server" ID="NumberGroup" class="form-control" ReadOnly="true" placeholder="Number Group" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-xs-12 col-sm-4 col-md-2">Transaction</label>
                        <div class="col-xs-12 col-sm-8 col-md-2">
                            <asp:DropDownList runat="server" ID="Transaction" class="form-control required" ClientIDMode="Static">
                                <asp:ListItem Value="0" Text="Sales Quotation"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Sales Order"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Manual Invoice"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Sales Return"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Purchase Order"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Stock Adjustment"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Material Issue"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Sales Invoice"></asp:ListItem>
                                <asp:ListItem Value="8" Text="Goods Receipt"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <label id="lblDate" runat="server" class="control-label col-xs-12 col-sm-4 col-md-2">Date</label>
                        <div class="col-xs-12 col-sm-8 col-md-2">
                            <asp:TextBox runat="server" ID="CreatedDate" class="form-control required" ReadOnly="true" placeholder="Date" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-xs-12 col-sm-4 col-md-2">Reference</label>
                        <div class="col-xs-12 col-sm-8 col-md-2">
                            <asp:TextBox runat="server" ID="Reference" class="form-control" placeholder="Reference" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <label id="lblUser" runat="server" class="control-label col-xs-12 col-sm-4 col-md-2">User</label>
                        <div class="col-xs-12 col-sm-8 col-md-2">
                            <asp:TextBox runat="server" ID="CreatedUser" ReadOnly="true" class="form-control required" placeholder="User" ClientIDMode="Static"></asp:TextBox>
                        </div>

                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-10 col-md-8">
                        <asp:HiddenField ID="FirstFreeNumberId" runat="server" />
                        <asp:HiddenField ID="LastFirstFreeNumber" runat="server" />
                        <a id="cancelFirstFreeNumber" href="/Account/FirstFreenumber.aspx" runat="server" class="btn btn-primary pull-left">Cancel</a>
                        <asp:Button ID="saveFirstFreeNumberBtn" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="saveFirstFreeNumber" />
                        
                    </div>
                </div>
            </div>

            <asp:GridView ID="FirstFreeDetail" runat="server" class="table table-fix" ClientIDMode="Static" ShowFooter="true" AutoGenerateColumns="false" DataKeyNames="ID" OnDataBound="listFirstFreeNumberDataBound" OnPreRender="FirstFreeDetailPreRender">
                <Columns>
                    <asp:TemplateField HeaderText="Order Type">
                        <ItemTemplate>
                            <asp:TextBox ID="OrderType" class="form-control OrderType" ClientIDMode="Static" runat="server" Text='<%# Bind("OrderType") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Enterprise Unit">
                        <ItemTemplate>
                            <asp:TextBox ID="EnterpriseUnit" class="form-control required"  ClientIDMode="Static" runat="server" Text='<%# Bind("EnterpriseUnitCode") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No of Digits">
                        <ItemTemplate>
                            <asp:TextBox ID="NoOfDigits" class="form-control NoOfDigits txtNumeric required" ClientIDMode="Static" runat="server" Text='<%# Bind("Digits") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prefix">
                        <ItemTemplate>
                            <asp:TextBox ID="Prefix" class="form-control Prefix required" ClientIDMode="Static" runat="server" Text='<%# Bind("Prefix") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sequence Number">
                        <ItemTemplate>
                            <asp:TextBox ID="SequenceNumber" class="form-control SequenceNumber txtNumeric required" ClientIDMode="Static" runat="server" Text='<%# Bind("SequenceNo") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Default">
                        <ItemTemplate>
                            <asp:TextBox ID="Default" class="form-control required" ClientIDMode="Static" runat="server" Text='<%# Bind("Defaults") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <%--<asp:GridView ID="FirstFreeDetailEU" runat="server" ShowFooter="true" AutoGenerateColumns="false" DataKeyNames="ID" OnDataBound="listFirstFreeNumberDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Order Type">
                        <ItemTemplate>
                            <asp:TextBox ID="OrderType" class="form-control OrderType" ClientIDMode="Static" runat="server" Text='<%# Bind("OrderType") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No of Digits">
                        <ItemTemplate>
                            <asp:TextBox ID="NoOfDigits" class="form-control NoOfDigits" ClientIDMode="Static" runat="server" Text='<%# Bind("Digits") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prefix">
                        <ItemTemplate>
                            <asp:TextBox ID="Prefix" class="form-control Prefix" ClientIDMode="Static" runat="server" Text='<%# Bind("Prefix") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sequence Number">
                        <ItemTemplate>
                            <asp:TextBox ID="SequenceNumber" class="form-control" ClientIDMode="Static" runat="server" Text='<%# Bind("SequenceNo") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Default">
                        <ItemTemplate>
                            <asp:TextBox ID="Default" class="form-control" ClientIDMode="Static" runat="server" Text='<%# Bind("Defaults") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:DropDownList ID="FirstFreeDtlStatus" runat="server">
                                <asp:ListItem Value="1" Text="active"></asp:ListItem>
                                <asp:ListItem Value="0" Text="inactive"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:HiddenField ID="selectedvalue" runat="server" Value='<%# Bind("Status") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>--%>

            <div class="form-group">
                <div class="col-xs-10 col-md-8">
                    <a id="CancelFirstFreeDetails" href="/Account/FirstFreeNumber.aspx" runat="server" class="btn btn-primary pull-left">Cancel</a><asp:Button ID="SaveFirstFreeDetails" runat="server" ClientIDMode="Static" class="btn btn-primary pull-left" Text="Save" OnClick="SaveFirstFreeNumberDetails" />
                    
                </div>
            </div>
            <br />

        </div>
    </div>
</asp:Content>

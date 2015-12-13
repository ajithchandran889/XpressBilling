<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockEntryEdit.aspx.cs" Inherits="XpressBilling.Account.StokeEntryEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="page-content">
        <div class="row content-holder">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <div id="SaveSuccess" visible="false" class="alert alert-success" role="alert" runat="server">
                    <span runat="server"><img src="~/Images/like.png" alt="" runat="server" />	</span>
                    Saved Successfully
                </div>
                <div id="UpdateSuccess" visible="false" class="alert alert-success" role="alert" runat="server">
                    <span runat="server"><img src="~/Images/like.png" alt="" runat="server" />	</span>
                    Updated Successfully
                </div>
                <div id="FinalizeSuccess" visible="false" class="alert alert-success" role="alert" runat="server">
                    <span runat="server"><img src="~/Images/like.png" alt="" runat="server" />	</span>
                    Finalized Successfully
                </div>
                <div id="failure" visible="false" class="alert alert-danger" role="alert" runat="server">
                    <span id="failureMessage" runat="server">Sorry,Something went wrong!</span>
                </div>
                <div class="page-header">Stock Entry</div>
                <div class="form-group">

                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Adjustment Type</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="AdjustmentType" class="form-control required" ClientIDMode="Static">
                            <asp:ListItem Value="2" Text="Addition"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Deduction"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Opening"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Document</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Document" class="form-control required" placeholder="Document" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="AdditionSequenceNo" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="DeductionSequenceNo" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="OpeningSequenceNo" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="AdditionSequenceNoID" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="DeductionSequenceNoID" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="OpeningSequenceNoID" ClientIDMode="Static" />
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Status</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="Status" class="form-control required" Enabled="false" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Free" ></asp:ListItem>
                            <asp:ListItem Value="1" Text="Open"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Finalized"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">

                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Location</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="Location" class="form-control required" ClientIDMode="Static" AutoPostBack="true">
                        </asp:DropDownList>

                    </div>

                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Date</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Date" class="form-control required" placeholder="Date" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">User</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="CreatedUser" class="form-control required" ClientIDMode="Static" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Reference</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Reference" class="form-control required" placeholder="Reference" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Amount</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Amount" ReadOnly="true" class="form-control" placeholder="Amount" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Currency</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Currency" ReadOnly="true" class="form-control required" placeholder="Currency Code" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-10 col-md-8">
                        <asp:HiddenField ID="StokeEntryMstId" runat="server" ClientIDMode="Static"/>
                        <asp:HiddenField runat="server" ID="PageStatus" ClientIDMode="Static" />
                        <a id="CancelBtn" href="/Account/StockEntry" runat="server" class="btn btn-primary">Cancel</a>
                        <asp:Button ID="SaveBtn" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Save" OnClick="SaveBtnClick" />
                       </div>
                </div>
                <asp:Panel runat="server" ID="gridDetails">
                    <div class="grid_wrapper">
                        <div class="grid_header">
                            <h2 class="pull-left">Transaction</h2>
                            <div class="pull-right">
                                <span class="icon-wrap pull-left"><i class="glyphicon glyphicon-plus "></i></span>
                            </div>
                        </div>
                        <asp:GridView ID="StockEntryDetail" runat="server" class="table" ClientIDMode="Static" ShowFooter="False"  AutoGenerateColumns="false" DataKeyNames="ID" >
                            <RowStyle CssClass="Odd" />
                            <AlternatingRowStyle CssClass="Even" />
                           
                            <Columns>
                                <asp:TemplateField HeaderText="No:" ControlStyle-Width="50">
                                    <ItemTemplate >
                                       <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item" ControlStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Item" class="form-control StockItem"  ClientIDMode="Static" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" ControlStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Name" class="form-control StockName" ClientIDMode="Static" runat="server" Text='<%# Bind("ItemName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate"  ControlStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SERate" class="form-control StockRate txtNumeric" ClientIDMode="Static" runat="server" Text='<%# Bind("Rate") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity"  ControlStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SEQuantity" class="form-control StockQuantity txtNumeric"  ClientIDMode="Static" runat="server" Text='<%# Bind("Qty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit"  ControlStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Unit" class="form-control StockUnit" ClientIDMode="Static" runat="server" Text='<%# Bind("BaseUnitCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount"  ControlStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SEAmount" class="form-control StockAmount" ClientIDMode="Static" runat="server" Text='<%#Eval("Amount","{0:2}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-8">
                        <div class="form-group ">
                            <asp:Button ID="AddNewRow" runat="server" Text="Add Rows" OnClick="AddNewRowClick" />
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <div class="row">
                            <a id="btnCencelDtl" href="/Account/SalesQuotation" runat="server" class="btn btn-primary">Cancel</a>
                            <asp:Button ID="btnSaveDtl" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Save" OnClick="SaveBtnDetailClick" />
                            <asp:Button ID="btnConvertStockRegister" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Finalize"  Visible="false" OnClick="btnConvertStockRegisterClick"/>
                            <asp:Button ID="btnPrint" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right"  Text="Print" Visible="false" OnClientClick="javascript:window.print();"/>
                        </div>

                    </div>
                    
                </asp:Panel>
               <asp:HiddenField ID="CompanyCode" runat="server" ClientIDMode="Static"/>
            </div>
        </div>
    </div>
</asp:Content>

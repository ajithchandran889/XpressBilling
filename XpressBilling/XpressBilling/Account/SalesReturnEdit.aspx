<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalesReturnEdit.aspx.cs" Inherits="XpressBilling.Account.SalesReturnEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="page-content">
        <div class="row content-holder">
            <div class="col-xs-12 col-sm-12 col-md-12" >
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
                <div class="page-header">Sales Return Edit</div>
                <div class="form-group">
                     <label class="control-label col-xs-12 col-sm-4 col-md-2">Return Order Type</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="SalesReturnType" class="form-control required" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="SalesReturnTypeSelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Against Order"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Manual Return Order"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Sales Return</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="SalesReturn" class="form-control required" placeholder="Sales Return" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="AgainstSequenceNo" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="ManualSequenceNo" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="AgainstSequenceNoID" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="ManualSequenceNoID" ClientIDMode="Static" />
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Status</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="Status" class="form-control required" Enabled="false" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Free"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Open"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Finalized"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Customer ID</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="CustomerId" class="form-control required" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="CustomerIdSelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="CustomerIdText" ReadOnly="true" class="form-control" Visible="false" ClientIDMode="Static"></asp:TextBox>
                    
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Date</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Date" class="form-control required" placeholder="Date" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Name</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Name" class="form-control required" ReadOnly="true" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Location</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Location" class="form-control required" ReadOnly="true" placeholder="Location" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Sales man</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="SalesMan" class="form-control required" style="width:160px" placeholder="Sales Man" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Telephone</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Telephone" class="form-control required" ReadOnly="true" placeholder="Telephone" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Amount</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Amount" class="form-control required" ReadOnly="true" placeholder="Amount" ClientIDMode="Static" Text="0"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Invoice</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="InvoiceNoList" class="form-control required" ClientIDMode="Static" >
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="InvoiceNoText" ReadOnly="true" class="form-control" Visible="false" ClientIDMode="Static"></asp:TextBox>
                     </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Available Credit</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="AvailableCredit" ReadOnly="true" class="form-control" placeholder="Available Credit" ClientIDMode="Static" Text="0"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Reference</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Reference" ReadOnly="true"  class="form-control" placeholder="Reference" ClientIDMode="Static" ></asp:TextBox>
                     </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-10 col-md-8">
                        <asp:HiddenField ID="SalesReturnMstId" runat="server" ClientIDMode="Static"/>
                        <a id="CancelBtn" href="/Account/SalesReturn" runat="server" class="btn btn-primary">Cancel</a>
                        <asp:Button ID="SaveBtn" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Save" OnClick="SaveBtnClick" />
                       </div>
                </div>
                <asp:Panel runat="server" ID="gridDetails">
                    <div class="grid_wrapper">
                        <div class="">
                            <h2 class="pull-left">Transaction</h2>
                        </div>
                        <asp:GridView ID="SalesReturnDetail" runat="server" class="table  table-fix" ClientIDMode="Static" ShowFooter="False" AutoGenerateColumns="false" DataKeyNames="ID">
                            <RowStyle CssClass="Odd" />
                            <AlternatingRowStyle CssClass="Even" />

                            <Columns>
                                <asp:TemplateField HeaderText="No:">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item" ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRItem" class="form-control SRItem required" ClientIDMode="Static" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRItemName" class="form-control SRItemName required" ClientIDMode="Static" runat="server" Text='<%# Bind("ItemName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRItemRate" class="form-control SRItemRate txtNumeric required" ClientIDMode="Static" runat="server" Text='<%#Eval("Rate","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRQuantity" class="form-control SRQuantity txtNumeric required" ClientIDMode="Static" runat="server" Text='<%# Bind("Qty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRUnit" class="form-control SRUnit required" ClientIDMode="Static" runat="server" Text='<%# Bind("BaseUnitCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disc%"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRDiscPer" class="form-control SRDiscPer txtNumeric required" ClientIDMode="Static" runat="server" Text='<%#Eval("DiscountPercentage","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disc Amt"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRDiscAmt" class="form-control SRDiscAmt txtNumeric required" ClientIDMode="Static" runat="server" Text='<%#Eval("DiscountAmt","{0:n}")%>'></asp:TextBox>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax%"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRTaxPer" class="form-control SRTaxPer required" ClientIDMode="Static" runat="server" Text='<%# Bind("TaxPercentage") %>'></asp:TextBox>
                                        <asp:HiddenField ID="SRTaxCode" runat="server" ClientIDMode="Static" Value='<%# Bind("Tax") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Amt"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRTaxAmt" class="form-control SRTaxAmt required" ClientIDMode="Static" runat="server" Text='<%#Eval("TaxAmount","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Amt" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRNetAmt" class="form-control SRNetAmt required" ClientIDMode="Static" runat="server" Text='<%#Eval("NetAmount","{0:n}")%>'></asp:TextBox>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-8">
                        <div class="form-group ">
                            <asp:Button ID="AddNewRow" runat="server" Text="Add Rows" OnClick="AddNewRowClick" Visible="false" />
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-8">
                        <div class="form-group ">
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Payment Terms</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <asp:TextBox runat="server" ID="SRPayTerms" class="form-control" placeholder="Payment Terms" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-4">
                        <div class="border-bg">
                            <div class="form-group ">
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Amount</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="SRTotalAmount" class="form-control" placeholder="P100.5" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Discount Amt</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="SRTotalDiscountAmt" class="form-control" placeholder="Discount Amt" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Tax Amt</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="SRTotalTaxAmt" class="form-control" placeholder="Tax Amt" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Order Amount</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="SRTotalOrderAmt" class="form-control" placeholder="Order Amount" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-8">
                        <div class="form-group ">
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-4">
                        <div class="border-bg">
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Payments</label>
                            <div class="form-group ">
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Cash</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="Cash" class="form-control" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group ">
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Demurages</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="Demurages" class="form-control" placeholder="Demurages" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group ">
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Roud-off</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="RoudOff" class="form-control" placeholder="Roud-off" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group ">
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Total</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="Total" class="form-control" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group ">
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Change</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="Change" class="form-control" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <div class="row">
                            <asp:HiddenField ID="SalesOrderDate" runat="server" ClientIDMode="Static"  />
                            <asp:HiddenField runat="server" ID="PageStatus" ClientIDMode="Static" />
                            <asp:Button ID="btnSaveDtl" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Save" OnClick="SaveBtnDetailClick" />
                            <asp:Button ID="btnFinalize" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Finalize"  OnClick="btnFinalizeClick" />
                            <a id="btnCencelDtl" href="/Account/SalesReturn" runat="server" class="btn btn-primary">Cancel</a>
                            <asp:Button ID="btnPrint" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right"  Text="Print"  OnClientClick="javascript:window.print();"/>
                        </div>

                    </div>
                </asp:Panel>
                <asp:HiddenField ID="CompanyCode" runat="server" ClientIDMode="Static"/>
            </div>
        </div>
    </div>
</asp:Content>

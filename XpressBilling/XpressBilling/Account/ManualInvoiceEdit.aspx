<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManualInvoiceEdit.aspx.cs" Inherits="XpressBilling.Account.ManualInvoiceEdit" %>

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
                <div id="failure" visible="false" class="alert alert-danger" role="alert" runat="server">
                    <span id="failureMessage" runat="server">Sorry,Something went wrong!</span>
                </div>
                <div class="page-header">Manual Invoice Edit</div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Customer ID</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="CustomerId" class="form-control required" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="CustomerIdSelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Invoice</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Invoice" class="form-control required" placeholder="Invoice" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="CashSequenceNo" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="CreditSequenceNo" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="CashSequenceNoID" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="CreditSequenceNoID" ClientIDMode="Static" />
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Status</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="Status" class="form-control required" Enabled="false" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Free"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Open"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Paid"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                     <label class="control-label col-xs-12 col-sm-4 col-md-2">Invoice Type</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="InvoiceType" class="form-control required" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Cash"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Credit"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Date</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Date" class="form-control required" placeholder="Date" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Name</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Name" class="form-control required" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Location</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Location" class="form-control required" placeholder="Location" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Sales man</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="SalesMan" class="form-control required" placeholder="Sales Man" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Telephone</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Telephone" class="form-control required" placeholder="Telephone" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Amount</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Amount" class="form-control required" ReadOnly="true" placeholder="Validity" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Customer Reference</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Reference" class="form-control required" placeholder="Reference" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Available Credit</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="AvailableCredit" class="form-control" ReadOnly="true" placeholder="Available Credit" ClientIDMode="Static" Text="0"></asp:TextBox>

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
                        <asp:GridView ID="ManualInvoiceDetail" runat="server" class="table" ClientIDMode="Static" ShowFooter="False" AutoGenerateColumns="false" DataKeyNames="ID">
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
                                        <asp:TextBox ID="MIItem" class="form-control MIItem " ClientIDMode="Static" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="MIItemName" class="form-control MIItemName " ClientIDMode="Static" runat="server" Text='<%# Bind("ItemName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="MIItemRate" class="form-control MIItemRate txtNumeric" ClientIDMode="Static" runat="server" Text='<%# Bind("Rate") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="MIQuantity" class="form-control MIQuantity txtNumeric" ClientIDMode="Static" runat="server" Text='<%# Bind("Qty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="MIUnit" class="form-control MIUnit " ClientIDMode="Static" runat="server" Text='<%# Bind("BaseUnitCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disc%"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="MIDiscPer" class="form-control txtNumeric" ClientIDMode="Static" runat="server" Text='<%# Bind("DiscountPercentage") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disc Amt"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="MIDiscAmt" class="form-control MIDiscAmt txtNumeric" ClientIDMode="Static" runat="server" Text='<%# Bind("DiscountAmt") %>'></asp:TextBox>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax%"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="MITaxPer" class="form-control MITaxPer txtNumeric" ClientIDMode="Static" runat="server" Text='<%# Bind("TaxPercentage") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Amt"  ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="MITaxAmt" class="form-control MITaxAmt txtNumeric" ClientIDMode="Static" runat="server" Text='<%# Bind("TaxAmount") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Amt" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="MINetAmt" class="form-control MINetAmt" ClientIDMode="Static" runat="server" Text='<%# Bind("NetAmount") %>'></asp:TextBox>
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
                    <div class="col-xs-12 col-sm-6 col-md-8">
                        <div class="form-group ">
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Payment Terms</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <asp:TextBox runat="server" ID="MIPayTerms" class="form-control" placeholder="Payment Terms" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Delivery Terms</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <asp:TextBox runat="server" ID="MIDeliveryTerms" class="form-control" placeholder="Delivery Terms" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Ship To Address</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <asp:TextBox runat="server" ID="MIShipToAddress" class="form-control required" placeholder="Ship To Address" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-4">
                        <div class="border-bg">
                            <div class="form-group ">
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Amount</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="MITotalAmount" class="form-control" placeholder="P100.5" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Discount Amt</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="MITotalDiscountAmt" class="form-control" placeholder="Discount Amt" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Tax Amt</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="MITotalTaxAmt" class="form-control" placeholder="Tax Amt" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Order Amount</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="MITotalOrderAmt" class="form-control" placeholder="Order Amount" ClientIDMode="Static"></asp:TextBox>
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
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Reciept</label>
                            <div class="form-group ">
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Cash</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="Cash" class="form-control" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group ">
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Debit/Credit Card</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="Card" class="form-control" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group ">
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Cash Discount</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="CashDiscount" class="form-control" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group ">
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Round Off</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="RoundOff" class="form-control" placeholder="" ClientIDMode="Static"></asp:TextBox>
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
                            <asp:HiddenField ID="InvoiceId" runat="server" ClientIDMode="Static"/>
                            <asp:HiddenField runat="server" ID="PageStatus" ClientIDMode="Static" />
                            <asp:Button ID="btnSaveDtl" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Save" OnClick="SaveBtnDetailClick" />
                            <a id="btnCencelDtl" href="/Account/SalesQuotation" runat="server" class="btn btn-primary">Cancel</a>
                            <%--<asp:Button ID="btnConverOrder" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right" Text="Order" Visible="false" OnClick="BtnConvertOrderClick" />--%>
                            <asp:Button ID="btnPrint" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right" Visible="false" Text="Print"  OnClientClick="javascript:window.print();"/>
                        </div>

                    </div>
                </asp:Panel>
                <asp:HiddenField ID="CompanyCode" runat="server" ClientIDMode="Static"/>
            </div>
        </div>
    </div>
</asp:Content>

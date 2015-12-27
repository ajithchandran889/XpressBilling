<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SQEdit.aspx.cs" Inherits="XpressBilling.Account.sales_quotation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-xs-12 col-sm-12 col-md-12">
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
                <div id="FinalizeSuccess" visible="false" class="alert alert-success" role="alert" runat="server">
                    <span runat="server">
                        <img src="~/Images/like.png" alt="" runat="server" />
                    </span>
                    Finalized Successfully
                </div>
                <div id="failure" visible="false" class="alert alert-danger" role="alert" runat="server">
                    <span id="failureMessage" runat="server">Sorry,Something went wrong!</span>
                </div>
                <div class="page-header">Sales Quotation</div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Customer ID</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="CustomerId" class="form-control required" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="CustomerIdSelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Quotation</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Quotation" class="form-control required" placeholder="Quotation" ClientIDMode="Static"></asp:TextBox>
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
                            <asp:ListItem Value="2" Text="Finalized"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Order Type</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="QuotationType" class="form-control required" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Cash"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Credit"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Date</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="CreatedDate" class="form-control required" placeholder="Date" ClientIDMode="Static"></asp:TextBox>
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
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Validity</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Validity" class="form-control required" placeholder="Validity" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Reference</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Reference" class="form-control required" placeholder="Reference" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Sales Order</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="SalesOrder" class="form-control" ReadOnly="true" placeholder="SalesOrder" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="salesOrderLastIncId" ClientIDMode="Static" />
                    </div>

                </div>
                <div class="form-group">
                    <div class="col-xs-10 col-md-8">
                        <asp:HiddenField ID="rowCount" runat="server" ClientIDMode="Static" Value="1" />
                        <asp:HiddenField ID="SalesQuotationId" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="PageStatus" ClientIDMode="Static" />
                        <a id="CancelBtn" href="/Account/SalesQuotation" runat="server" class="btn btn-primary">Cancel</a>
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
                        <asp:GridView ID="SalesQuotationDetail" runat="server" class="table table-fix" ClientIDMode="Static" ShowFooter="False" AutoGenerateColumns="false" DataKeyNames="ID">
                            <RowStyle CssClass="Odd" />
                            <AlternatingRowStyle CssClass="Even" />

                            <Columns>
                                <asp:TemplateField HeaderText="No:">
                                    <ItemTemplate>
                                        <asp:Label ID="indexIcrement" runat="server" Text='<%# Container.DataItemIndex + 1 %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item" ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SQItem" name="SQItem" class="form-control SQItem gridTxtBox required" ClientIDMode="Static" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SQName" name="SQName" class="form-control SQName gridTxtBox required" ClientIDMode="Static" runat="server" Text='<%# Bind("ItemName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SQRate" name="SQRate" class="form-control SQRate gridTxtBox txtNumeric required" ClientIDMode="Static" runat="server" Text='<%#Eval("Rate","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SQQuantity" name="SQQuantity" class="form-control SQQuantity gridTxtBox txtNumeric required" ClientIDMode="Static" runat="server" Text='<%# Bind("Qty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SQUnit" name="SQUnit" class="form-control SQUnit gridTxtBox required" ClientIDMode="Static" runat="server" Text='<%# Bind("BaseUnitCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disc%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SQDiscPer" name="SQDiscPer" class="form-control gridTxtBox SQDiscPer txtNumeric required" ClientIDMode="Static" runat="server" Text='<%#Eval("Discount","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disc Amt">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SQDiscAmt" name="SQDiscAmt" class="form-control SQDiscAmt gridTxtBox txtNumeric required" ClientIDMode="Static" runat="server" Text='<%#Eval("DiscountAmt","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SQTaxPer" name="SQTaxPer" class="form-control SQTaxPer gridTxtBox required" ClientIDMode="Static" runat="server" Text='<%# Bind("TaxPercentage") %>'></asp:TextBox>
                                        <asp:HiddenField ID="SQTaxCode" runat="server" ClientIDMode="Static" Value='<%# Bind("Tax") %>' />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Amt">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SQTaxAmt" name="SQTaxAmt" class="form-control SQTaxAmt gridTxtBox required" ClientIDMode="Static" runat="server" Text='<%#Eval("TaxAmount","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Amt" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SQNetAmt" name="SQNetAmt" class="form-control SQNetAmt gridTxtBox required" ClientIDMode="Static" runat="server" Text='<%#Eval("NetAmount","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="col-xs-12 col-sm-6 col-md-8">
                        <div class="form-group ">
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Payment Terms</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <asp:TextBox runat="server" ID="PayTerms" class="form-control gridTxtBox" placeholder="Payment Terms" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Delivery Terms</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <asp:TextBox runat="server" ID="DeliveryTerms" class="form-control gridTxtBox" placeholder="Delivery Terms" ClientIDMode="Static"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-4">
                        <div class="border-bg">
                            <div class="form-group ">
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Amount</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="TotalAmount" class="form-control" placeholder="P100.5" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Discount Amt</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="TotalDiscountAmt" class="form-control" placeholder="Discount Amt" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Tax Amt</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="TotalTaxAmt" class="form-control" placeholder="Tax Amt" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Order Amount</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="TotalOrderAmt" class="form-control" placeholder="Order Amount" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <div class="row">
                            <a id="btnCencelDtl" href="/Account/SalesQuotation" runat="server" class="btn btn-primary">Cancel</a>
                            <asp:Button ID="btnSaveDtl" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Save" OnClick="SaveBtnDetailClick" />
                            <asp:Button ID="btnConverOrder" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Order" Visible="false" OnClick="BtnConvertOrderClick" />
                            <asp:Button ID="btnPrint" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right" Visible="false" Text="Print" OnClientClick="javascript:window.print();" />
                        </div>

                    </div>
                </asp:Panel>
                <asp:HiddenField ID="CompanyCode" runat="server" ClientIDMode="Static" />
                
            </div>
        </div>
    </div>
</asp:Content>

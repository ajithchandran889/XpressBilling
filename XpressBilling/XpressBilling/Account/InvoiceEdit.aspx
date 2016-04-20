<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvoiceEdit.aspx.cs" Inherits="XpressBilling.Account.InvoiceEdit" %>

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
                <div class="page-header">Sales Invoice Edit</div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Bussiness Partner</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="CustomerIdSI" class="form-control required" placeholder="Bussiness Partner" ClientIDMode="Static"></asp:TextBox>

                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Order Type</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="InvoiceType" class="form-control required" Enabled="false" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Cash"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Credit"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField ID="selectedInvoiceType" runat="server" ClientIDMode="Static" />
                    </div>


                </div>

                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Name</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Name" class="form-control required" ReadOnly="true" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Invoice</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Invoice" class="form-control required" placeholder="Invoice" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="SISequenceNoID" ClientIDMode="Static" />
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
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Telephone</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Telephone" class="form-control required" ReadOnly="true" placeholder="Telephone" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Date</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Date" class="form-control required" placeholder="Date" ClientIDMode="Static"></asp:TextBox>
                    </div>

                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Customer Reference</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Reference" class="form-control" placeholder="Reference" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Location</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Location" class="form-control required" placeholder="Location" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="LocationHidden" ClientIDMode="Static" />
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Sales man</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="SalesMan" class="form-control required" placeholder="Sales Man" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="SalesManHidden" ClientIDMode="Static" />
                    </div>


                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-4 col-md-2">
                    </div>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Amount</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <span id="currencyCode1" runat="server" />
                        <asp:TextBox runat="server" ID="Amount" class="form-control required" ReadOnly="true" placeholder="101" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Available Credit</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="AvailableCredit" ReadOnly="true" class="form-control" placeholder="Available Credit" ClientIDMode="Static" Text="0"></asp:TextBox>

                    </div>

                </div>
            
            <asp:Panel runat="server" ID="gridDetails">
                <div class="grid_wrapper">
                    <div class="">
                        <h2 class="page-header color-blue">Transaction</h2>
                    </div>
                    <div class="for-tab">

  <!-- Nav tabs -->
  <ul class="nav nav-tabs" role="tablist">
    <li role="presentation" class="active"><a href="#Invoicetab" aria-controls="Invoice" role="tab" data-toggle="tab">Invoice</a></li>
    <li role="presentation"><a href="#Receipttab" aria-controls="Receipt" role="tab" data-toggle="tab">Receipt</a></li>
  
  </ul>

  <!-- Tab panes -->
  <div class="tab-content">
    <div role="tabpanel" class="tab-pane active" id="Invoicetab">
       <%--Invoice.....--%>
        <asp:GridView ID="InvoiceDetail" runat="server" class="table table-fix" ClientIDMode="Static" ShowFooter="False" AutoGenerateColumns="false" DataKeyNames="ID"
                        OnRowDataBound="InvoiceDetailRowDataBound">
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
                                    <asp:TextBox ID="IItem" class="form-control IItem required" ClientIDMode="Static" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:TextBox ID="IItemName" class="form-control IItemName required" ClientIDMode="Static" runat="server" Text='<%# Bind("ItemName") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Quantity" ControlStyle-Width="50">
                                <ItemTemplate>
                                    <asp:TextBox ID="IQuantity" class="form-control IQuantity txtNumeric required" ClientIDMode="Static" runat="server" Text='<%# Bind("Qty") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit" ControlStyle-Width="50">
                                <ItemTemplate>
                                    <asp:TextBox ID="IUnit" class="form-control IUnit required" ClientIDMode="Static" runat="server" Text='<%# Bind("BaseUnitCode") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate" ControlStyle-Width="50">
                                <ItemTemplate>
                                    <asp:TextBox ID="IItemRate" class="form-control IItemRate txtNumeric required" ClientIDMode="Static" runat="server" Text='<%#Eval("Rate","{0:n}")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Disc%" ControlStyle-Width="50">
                                <ItemTemplate>
                                    <asp:TextBox ID="IDiscPer" class="form-control IDiscPer txtNumeric required" ClientIDMode="Static" runat="server" Text='<%#Eval("DiscountPercentage","{0:n}")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Disc Amt" ControlStyle-Width="50">
                                <ItemTemplate>
                                    <asp:TextBox ID="IDiscAmt" class="form-control IDiscAmt txtNumeric required" ClientIDMode="Static" runat="server" Text='<%#Eval("DiscountAmt","{0:n}")%>'></asp:TextBox>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tax%" ControlStyle-Width="50">
                                <ItemTemplate>
                                    <asp:TextBox ID="ITaxPer" class="form-control ITaxPer required" ClientIDMode="Static" runat="server" Text='<%# Bind("TaxPercentage") %>'></asp:TextBox>
                                    <asp:HiddenField ID="ITaxCode" runat="server" ClientIDMode="Static" Value='<%# Bind("Tax") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tax Amt" ControlStyle-Width="50">
                                <ItemTemplate>
                                    <asp:TextBox ID="ITaxAmt" class="form-control ITaxAmt required" ClientIDMode="Static" runat="server" Text='<%#Eval("TaxAmount","{0:n}")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net Amt" ControlStyle-Width="50">
                                <ItemTemplate>
                                    <asp:TextBox ID="INetAmt" class="form-control INetAmt required" ClientIDMode="Static" runat="server" Text='<%#Eval("NetAmount","{0:n}")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="50">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDeleteSI" Text="Delete" data-id='<%# Eval("ID") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
    </div>
    <div role="tabpanel" class="tab-pane" id="Receipttab">
        <%--receipt grid here...--%>
        <asp:GridView ID="ReceiptDetailinvoice" runat="server" class="table table-fix" ClientIDMode="Static" ShowFooter="False"  AutoGenerateColumns="false" DataKeyNames="ID"
                            OnRowDataBound="ReceiptDetailRowDataBound" OnRowCreated="ReceiptDetailRowCreated" ShowHeader="false">
                            <RowStyle CssClass="Odd"/>
                            <AlternatingRowStyle CssClass="Even" />
                        
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate >
                                       <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="RPaymentMode" class="form-control RPaymentMode " ClientIDMode="Static" runat="server" Text='<%# Bind("PaymentMode") %>'></asp:TextBox>
                                        <asp:HiddenField ID="RPaymentModeID" runat="server" Value='<%# Eval("PaymentModeID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="RNumber" class="form-control RNumber " ClientIDMode="Static" runat="server" Text='<%# Bind("ReferenceNo") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="RDate" class="form-control RDate " ClientIDMode="Static" runat="server" Text='<%# Eval("ReferenceDate")==""?"": Convert.ToDateTime(Eval("ReferenceDate")).ToString("MM/dd/yyyy") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField >
                                    <ItemTemplate>
                                        <asp:TextBox ID="RDueAmount" class="form-control RDueAmount"  ClientIDMode="Static" runat="server" Text='<%# Bind("DueAmount") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField >
                                    <ItemTemplate>
                                        <asp:TextBox ID="RAmount" class="form-control RAmount " ClientIDMode="Static" runat="server" Text='<%# Bind("Amount") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TDSAmount" class="form-control TDSAmount " ClientIDMode="Static" runat="server" Text='<%#Eval("TDSAmount","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="RNetAmount" class="form-control RNetAmount " ClientIDMode="Static" runat="server" Text='<%#Eval("NetAmount","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDeleteR" Text="Delete" data-id='<%# Eval("ID") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
    </div>

  </div>

</div>

                    
                </div>
                <div class="col-xs-12 col-sm-6 col-md-8">
                    <div class="form-group ">
                        <label class="control-label col-xs-12 col-sm-4 col-md-4">Payment Terms</label>
                        <div class="col-xs-12 col-sm-8 col-md-8">
                            <asp:TextBox runat="server" ID="IPayTerms" class="form-control" placeholder="Payment Terms" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <label class="control-label col-xs-12 col-sm-4 col-md-4">Delivery Terms</label>
                        <div class="col-xs-12 col-sm-8 col-md-8">
                            <asp:TextBox runat="server" ID="IDeliveryTerms" class="form-control" placeholder="Delivery Terms" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <label class="control-label col-xs-12 col-sm-4 col-md-4">Ship To Address</label>
                        <div class="col-xs-12 col-sm-8 col-md-8">
                            <asp:TextBox runat="server" ID="IShipToAddress" class="form-control" placeholder="Ship To Address" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4">
                    <div class="border-bg">
                        <div class="form-group ">
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Amount</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <asp:TextBox runat="server" ID="ITotalAmount" class="form-control" placeholder="P100.5" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Discount Amt</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <asp:TextBox runat="server" ID="ITotalDiscountAmt" class="form-control" placeholder="Discount Amt" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group ">
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Tax Amt</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <asp:TextBox runat="server" ID="ITotalTaxAmt" class="form-control" placeholder="Tax Amt" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Order Amount</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <span id="currencyCode2" runat="server" />
                                <asp:TextBox runat="server" ID="ITotalOrderAmt" class="form-control" placeholder="Order Amount" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xs-12 col-sm-6 col-md-8">
                    <div class="form-group ">
                    </div>
                </div>
              <%--  <div class="col-xs-12 col-sm-6 col-md-4">
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
                </div>--%>
                <div class="col-xs-12 col-sm-12 col-md-12">
                    <div class="row">
                        <asp:HiddenField ID="currencyDecimal" runat="server" />
                        <asp:HiddenField ID="currencyCode" runat="server" />
                        <asp:HiddenField ID="DeletedRowIDs" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="rowCount" runat="server" ClientIDMode="Static" Value="1" />
                        <asp:HiddenField ID="SalesInvoiceId" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="PageStatus" ClientIDMode="Static" />
                        <input id="btnCencelDtl" type="button" class="btn btn-primary pull-left" value="Cancel" onclick="location.href = '/Account/Invoice';" />
                        <asp:Button ID="btnSaveDtl" runat="server" ClientIDMode="Static" class="btn btn-primary SalesInvoiceBtnDetail" Text="Save" OnClick="SaveBtnDetailClick" />
                        <asp:Button ID="btnFinalizeI" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Finalize" Visible="false" OnClick="btnFinalizeClick" />
                        <asp:Button ID="btnPrint" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right" Visible="false" Text="Print" OnClientClick="btnPrintClick" />
                    </div>

                </div>
            </asp:Panel>
            <asp:HiddenField ID="CompanyCode" runat="server" ClientIDMode="Static" />
                </div>
        </div>
    </div>
</asp:Content>

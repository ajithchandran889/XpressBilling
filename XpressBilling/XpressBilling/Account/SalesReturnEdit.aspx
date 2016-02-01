<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalesReturnEdit.aspx.cs" Inherits="XpressBilling.Account.SalesReturnEdit" %>

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
                <div class="page-header">Sales Return Edit</div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Return Order Type</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="SalesReturnType" class="form-control required" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Against Order"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Manual Return Order"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Sales Return</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="SalesReturn" class="form-control required" placeholder="Sales Return" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="SRSequenceNoID" ClientIDMode="Static" />
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
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Business Partner</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="SRCustomerId" class="form-control required" placeholder="Business Partner"  ClientIDMode="Static"></asp:TextBox>
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
                        <asp:TextBox runat="server" ID="Location" class="form-control required" placeholder="Location" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="LocationHidden" ClientIDMode="Static" />
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Sales man</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="SalesMan" class="form-control required" Style="width: 160px" placeholder="Sales Man" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="SalesManHidden" ClientIDMode="Static" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Telephone</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Telephone" class="form-control required" ReadOnly="true" placeholder="Telephone" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Amount</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <span id="currencyCode1" runat="server" />
                        <asp:TextBox runat="server" ID="Amount" class="form-control required" ReadOnly="true" placeholder="Amount" ClientIDMode="Static" Text="0"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Sales Order</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="SalesOrderNo" class="form-control required" placeholder="Sales Order"  ClientIDMode="Static"></asp:TextBox>

                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Available Credit</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="AvailableCredit" ReadOnly="true" class="form-control" placeholder="Available Credit" ClientIDMode="Static" Text="0"></asp:TextBox>
                    </div>
                </div>
                <asp:Panel runat="server" ID="gridDetails">
                    <div class="grid_wrapper">
                        <div class="">
                            <h2 class="pull-left">Transaction</h2>
                        </div>
                        <asp:GridView ID="SalesReturnDetail" runat="server" class="table  table-fix" ClientIDMode="Static" ShowFooter="False" AutoGenerateColumns="false" DataKeyNames="ID"
                            OnRowDataBound="SalesReturnDetailRowDataBound">
                            <RowStyle CssClass="Odd" />
                            <AlternatingRowStyle CssClass="Even" />

                            <Columns>
                                <asp:TemplateField HeaderText="No:">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRItem" class="form-control SRItem" ClientIDMode="Static" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRItemName" class="form-control SRItemName" ClientIDMode="Static" runat="server" Text='<%# Bind("ItemName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRQuantity" class="form-control SRQuantity txtNumeric" ClientIDMode="Static" runat="server" Text='<%# Bind("Qty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRUnit" class="form-control SRUnit" ClientIDMode="Static" runat="server" Text='<%# Bind("BaseUnitCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRItemRate" class="form-control SRItemRate txtNumeric" ClientIDMode="Static" runat="server" Text='<%#Eval("Rate","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disc%" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRDiscPer" class="form-control SRDiscPer txtNumeric" ClientIDMode="Static" runat="server" Text='<%#Eval("DiscountPercentage","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disc Amt" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRDiscAmt" class="form-control SRDiscAmt txtNumeric" ClientIDMode="Static" runat="server" Text='<%#Eval("DiscountAmt","{0:n}")%>'></asp:TextBox>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax%" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRTaxPer" class="form-control SRTaxPer" ClientIDMode="Static" runat="server" Text='<%# Bind("TaxPercentage") %>'></asp:TextBox>
                                        <asp:HiddenField ID="SRTaxCode" runat="server" ClientIDMode="Static" Value='<%# Bind("Tax") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Amt" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRTaxAmt" class="form-control SRTaxAmt" ClientIDMode="Static" runat="server" Text='<%#Eval("TaxAmount","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Amt" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SRNetAmt" class="form-control SRNetAmt" ClientIDMode="Static" runat="server" Text='<%#Eval("NetAmount","{0:n}")%>'></asp:TextBox>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDeleteSR" Text="Delete" data-id='<%# Eval("ID") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-8">
                        <div class="form-group ">
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Payment Terms</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <asp:TextBox runat="server" ID="SRPayTerms" class="form-control" placeholder="Payment Terms" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group ">
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Reference</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <asp:TextBox runat="server" ID="Reference" ReadOnly="true" class="form-control" placeholder="Reference" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-4">
                        <div class="border-bg">
                            <div class="form-group ">
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Amount</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="SRTotalAmount" class="form-control" placeholder="P100.5" ClientIDMode="Static"></asp:TextBox>
                                    <asp:HiddenField ID="SRTotalAmountHidden" runat="server" ClientIDMode="Static" />
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Discount Amt</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="SRTotalDiscountAmt" class="form-control" placeholder="Discount Amt" ClientIDMode="Static"></asp:TextBox>
                                    <asp:HiddenField ID="SRTotalDiscountAmtHidden" runat="server" ClientIDMode="Static" />
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Tax Amt</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="SRTotalTaxAmt" class="form-control" placeholder="Tax Amt" ClientIDMode="Static"></asp:TextBox>
                                    <asp:HiddenField ID="SRTotalTaxAmtHidden" runat="server" ClientIDMode="Static" />
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Demurages</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="Demurages" class="form-control txtNumeric" placeholder="Demurages" ClientIDMode="Static" Text="0"></asp:TextBox>
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Order Amount</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <span id="currencyCode2" runat="server" />
                                    <asp:TextBox runat="server" ID="SRTotalOrderAmt" class="form-control" placeholder="Order Amount" ClientIDMode="Static"></asp:TextBox>
                                    <asp:HiddenField ID="SRTotalOrderAmtHidden" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="SRCorrectTotalOrderAmtHidden" runat="server" ClientIDMode="Static" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <div class="row">
                            <asp:HiddenField ID="currencyDecimal" runat="server" />
                            <asp:HiddenField ID="currencyCode" runat="server" />
                            <asp:HiddenField ID="DeletedRowIDs" ClientIDMode="Static" runat="server" />
                            <asp:HiddenField ID="rowCount" runat="server" ClientIDMode="Static" Value="1" />
                            <asp:HiddenField ID="SalesReturnMstId" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="SalesOrderDate" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField runat="server" ID="PageStatus" ClientIDMode="Static" />
                            <asp:Button ID="btnSaveDtl" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Save" OnClick="SaveBtnDetailClick" />
                            <asp:Button ID="btnFinalize" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Finalize" OnClick="btnFinalizeClick" />
                            <input id="btnCencelDtl" type="button" class="btn btn-primary pull-left" value="Cancel" onclick="location.href = '/Account/SalesReturn';" />
                            <asp:Button ID="btnPrint" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right" Text="Print" OnClientClick="javascript:window.print();" />
                        </div>

                    </div>
                </asp:Panel>
                <asp:HiddenField ID="CompanyCode" runat="server" ClientIDMode="Static" />
                <asp:Button ID="buttonUpdate"  runat="server" ClientIDMode="Static" OnClick="buttonUpdate_Click" style="display:none;"/>
            </div>
        </div>
    </div>
</asp:Content>

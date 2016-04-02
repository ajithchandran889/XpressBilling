<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceiptEdit.aspx.cs" Inherits="XpressBilling.Account.ReceiptEdit" %>
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
                <div class="page-header">Receipt</div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Transaction Type</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="TransactionTypeR"  class="form-control required" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Receipt Against Invoice"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Advance Receipt"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Advance Allocation"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Credit Note"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Receipt</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="ReceiptNo" class="form-control required" ReadOnly="true" placeholder="Receipt" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="RSequenceNoID" ClientIDMode="Static" />
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Status</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="Status" class="form-control required" Enabled="false" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Free" ></asp:ListItem>
                            <asp:ListItem Value="1" Text="Open"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Finalized"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Cancelled" ></asp:ListItem>
                            <asp:ListItem Value="4" Text="Partially Received"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Closed "></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Business Partner</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="BusinessPartnerR" class="form-control required" placeholder="Business Partner" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="BussinessPartnerCode" ClientIDMode="Static" />
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Date</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="CreatedDateR" class="form-control required" placeholder="Date" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                    </div>
                    
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Currency</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Currency" class="form-control" placeholder="Currency" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Cashier</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="SalesMan" class="form-control required" placeholder="Cashier" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="SalesManHidden" ClientIDMode="Static" />
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Location</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Location" class="form-control required" placeholder="Location" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="LocationHidden" ClientIDMode="Static" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Reference</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Reference" class="form-control" placeholder="Reference" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Amount</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Amount" class="form-control required" Text="0" ReadOnly="true" placeholder="Amount" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-8 col-md-2">
                    </div>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">UnAllocated Amount</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="UnAllocatedAmount" class="form-control" disabled="disabled"  placeholder="UnAllocated Amount" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    
                    
                </div>
                <asp:Panel runat="server" ID="gridDetails">
                    <div class="grid_wrapper">
                        <div class="">
                            <h2 class="page-header color-blue">Transaction</h2>
                        </div>
                        <asp:GridView ID="ReceiptDetail" runat="server" class="table table-fix" ClientIDMode="Static" ShowFooter="False"  AutoGenerateColumns="false" DataKeyNames="ID"
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
                        <asp:GridView ID="ReceiptDetailAdvanceReceipt" runat="server" class="table table-fix" ClientIDMode="Static" ShowFooter="False"  AutoGenerateColumns="false" DataKeyNames="ID"
                            OnRowDataBound="ReceiptDetailAdvanceReceiptRowDataBound" OnRowCreated="ReceiptDetailAdvanceReceiptRowCreated" ShowHeader="false">
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
                                        <asp:TextBox ID="ARPaymentMode" class="form-control ARPaymentMode " ClientIDMode="Static" runat="server" Text='<%# Bind("PaymentMode") %>'></asp:TextBox>
                                        <asp:HiddenField ID="ARPaymentModeID" runat="server" Value='<%# Eval("PaymentModeID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="ARNumber" class="form-control ARNumber " ClientIDMode="Static" runat="server" Text='<%# Bind("ReferenceNo") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="ARDate" class="form-control ARDate " ClientIDMode="Static" runat="server" Text='<%# Eval("ReferenceDate")==""?"": Convert.ToDateTime(Eval("ReferenceDate")).ToString("MM/dd/yyyy") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="ARAmount" class="form-control ARAmount " ClientIDMode="Static" runat="server" Text='<%# Bind("Amount") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="ATDSAmount" class="form-control ATDSAmount " ClientIDMode="Static" runat="server" Text='<%#Eval("TDSAmount","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="ARNetAmount" class="form-control ARNetAmount " ClientIDMode="Static" runat="server" Text='<%#Eval("NetAmount","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDeleteAR" Text="Delete" data-id='<%# Eval("ID") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    
                    <%--<div class="grid_wrapper">
                        <div class="">
                            <h2 class="page-header color-blue">Recent Transaction</h2>
                        </div>
                        <asp:GridView ID="ReceiptRecentTransaction" runat="server" class="table table-fix" ClientIDMode="Static" ShowFooter="False"  AutoGenerateColumns="false">
                            <RowStyle CssClass="Odd" />
                            <AlternatingRowStyle CssClass="Even" />
                           
                            <Columns>
                                <asp:TemplateField HeaderText="Receipt" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="RReceiptNo" runat="server" Text='<%# Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="ReceiptDate" runat="server" Text='<%# Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction Type" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="TransactionTypeR" runat="server" Text='<%# Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Mode" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="PaymentModeR" runat="server" Text='<%# Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reference" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="ReferenceR" runat="server" Text='<%# Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Due Amount" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="DueAmountR" runat="server" Text='<%# Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received Amt" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="ReceivedAmtR" runat="server" Text='<%# Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TDS Amt" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="TDSAmtR" runat="server" Text='<%# Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cashier" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="CashierR" runat="server" Text='<%# Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Enterprise Unit" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="EnterpriseUnitR" runat="server" Text='<%# Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Label ID="StatusR" runat="server" Text='<%# Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                        </asp:GridView>
                    </div>--%>

                    <div class="col-xs-12 col-sm-12 col-md-12">

                        <div class="row">
                            <asp:HiddenField runat="server" ID="OrginalDueAmount" ClientIDMode="Static" />
                            <asp:HiddenField runat="server" ID="CurrentDueAmount" ClientIDMode="Static" />
                            <asp:HiddenField ID="ReceiptId" runat="server" ClientIDMode="Static"/>
                            <asp:HiddenField runat="server" ID="PageStatus" ClientIDMode="Static" />
                            <asp:HiddenField ID="currencyDecimal" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="currencyCode" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="DeletedRowIDs" ClientIDMode="Static" runat="server" />
                            <asp:HiddenField ID="DeletedRowIDsAR" ClientIDMode="Static" runat="server" />
                            <asp:HiddenField ID="IsFinalized" runat="server" ClientIDMode="Static" Value="0" />
                            <asp:HiddenField ID="rowCount" runat="server" ClientIDMode="Static" Value="1" />
                            <asp:HiddenField ID="rowCountDatePicker" runat="server" ClientIDMode="Static" Value="1" />
                            <asp:HiddenField ID="rowCountAR" runat="server" ClientIDMode="Static" Value="1" />
                            <input id="btnCencelDtl" type="button" class="btn btn-primary pull-left" value="Cancel" onclick="location.href = '/Account/Receipt';" />
                            <asp:Button ID="btnSaveDtl" runat="server" ClientIDMode="Static" class="btn btn-primary PurchaseOrderBtnDetail" Text="Save" OnClick="SaveBtnDetailClick" />
                            <asp:Button ID="btnConverOrder" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Order" Visible="false" OnClick="BtnFinalizeReceiptClick" />
                            <asp:Button ID="btnPrint" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right" Visible="false" Text="Print"  OnClientClick="javascript:window.print();" />
                        </div>

                    </div>
                </asp:Panel>
                <asp:HiddenField ID="CompanyCode" runat="server" ClientIDMode="Static"/>
            </div>
        </div>
    </div>
</asp:Content>

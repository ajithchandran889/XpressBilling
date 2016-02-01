
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderEdit.aspx.cs" Inherits="XpressBilling.Account.PurchaseOrderEdit" %>
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
                <div class="page-header">Purchase Order</div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Business Partner</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="SupplierId" class="form-control required" placeholder="Supplier Id" ClientIDMode="Static"></asp:TextBox>
                    
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Order Type</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="OrderType" Enabled="false" class="form-control required" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Local"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Import"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField ID="selectedOrderType" runat="server" ClientIDMode="Static" />
                    </div>
                    
                </div>
                
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Name</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Name" class="form-control required" ReadOnly="true" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Order No</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="OrderNo" class="form-control required" placeholder="Order No" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="POSequenceNoID" ClientIDMode="Static" />
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
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Telephone</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Telephone" class="form-control required" ReadOnly="true" placeholder="Telephone" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Date</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="CreatedDate" class="form-control required" placeholder="Date" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Supplier Reference</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Reference" class="form-control" placeholder="Reference" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Location</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Location" class="form-control required" placeholder="Location" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="LocationHidden" ClientIDMode="Static" />
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Buyer</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="SalesMan" class="form-control required" placeholder="Buyer" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="SalesManHidden" ClientIDMode="Static" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-8 col-md-2">
                    </div>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Amount</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <span id="currencyCode1" runat="server" />
                        <asp:TextBox runat="server" ID="Amount" class="form-control" ReadOnly="true" placeholder="Amount" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Available Credit</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="AvailableCredit" class="form-control" ReadOnly="true" placeholder="Available Credit" ClientIDMode="Static" Text="0"></asp:TextBox>
                     </div>
                </div>
                <asp:Panel runat="server" ID="gridDetails">
                    <div class="grid_wrapper">
                        <div class="">
                            <h2 class="pull-left">Transaction</h2>
                        </div>
                        <asp:GridView ID="PurchaseOrderDetail" runat="server" class="table table-fix" ClientIDMode="Static" ShowFooter="False"  AutoGenerateColumns="false" DataKeyNames="ID"
                            OnRowDataBound="PurchaseOrderDetailRowDataBound">
                            <RowStyle CssClass="Odd" />
                            <AlternatingRowStyle CssClass="Even" />
                           
                            <Columns>
                                <asp:TemplateField HeaderText="No:">
                                    <ItemTemplate >
                                       <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="POItem" class="form-control POItem required" ClientIDMode="Static" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="POName" class="form-control POName required" ClientIDMode="Static" runat="server" Text='<%# Bind("ItemName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity"  ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="POQuantity" class="form-control POQuantity txtNumeric required"  ClientIDMode="Static" runat="server" Text='<%# Bind("Qty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit"  ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="POUnit" class="form-control POUnit required" ClientIDMode="Static" runat="server" Text='<%# Bind("BaseUnitCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate"  ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="PORate" class="form-control PORate txtNumeric required" ClientIDMode="Static" runat="server" Text='<%#Eval("Rate","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disc%"  ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="PODiscPer" class="form-control PODiscPer txtNumeric required" ClientIDMode="Static" runat="server" Text='<%#Eval("Discount","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disc Amt"  ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="PODiscAmt" class="form-control PODiscAmt txtNumeric" ClientIDMode="Static" runat="server" Text='<%#Eval("DiscountAmt","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax%"  ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="POTaxPer" class="form-control POTaxPer required" ClientIDMode="Static" runat="server" Text='<%# Bind("TaxPercentage") %>'></asp:TextBox>
                                        <asp:HiddenField ID="POTaxCode" runat="server" ClientIDMode="Static" value='<%# Bind("Tax") %>'/>
                               </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Amt"  ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="POTaxAmt" class="form-control POTaxAmt required" ClientIDMode="Static" runat="server" Text='<%#Eval("TaxAmount","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Amt" ControlStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="PONetAmt" class="form-control PONetAmt required" ClientIDMode="Static" runat="server" Text='<%#Eval("NetAmount","{0:n}")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDeletePO" Text="Delete" data-id='<%# Eval("ID") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-8">
                        <div class="form-group ">
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Payment Terms</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <asp:TextBox runat="server" ID="PayTerms" class="form-control  " placeholder="Payment Terms" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Delivery Terms</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <asp:TextBox runat="server" ID="DeliveryTerms" class="form-control  " placeholder="Delivery Terms" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Ship To Address</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <asp:TextBox runat="server" ID="ShipToAddress" class="form-control" placeholder="Ship To Address" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-4">
                        <div class="border-bg">
                            <div class="form-group ">
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Amount</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="POTotalAmount" class="form-control" placeholder="P100.5" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Discount Amt</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="POTotalDiscountAmt" class="form-control" placeholder="Discount Amt" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Tax Amt</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <asp:TextBox runat="server" ID="POTotalTaxAmt" class="form-control" placeholder="Tax Amt" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <label class="control-label col-xs-12 col-sm-4 col-md-4">Order Amount</label>
                                <div class="col-xs-12 col-sm-8 col-md-8">
                                    <span id="currencyCode2" runat="server" />
                                    <asp:TextBox runat="server" ID="POTotalOrderAmt" class="form-control" placeholder="Order Amount" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12 col-md-12">

                        <div class="row">
                            <asp:HiddenField ID="PurchaseOrderId" runat="server" ClientIDMode="Static"/>
                            <asp:HiddenField runat="server" ID="PageStatus" ClientIDMode="Static" />
                            <asp:HiddenField ID="currencyDecimal" runat="server" />
                            <asp:HiddenField ID="currencyCode" runat="server" />
                            <asp:HiddenField ID="DeletedRowIDs" ClientIDMode="Static" runat="server" />
                            <asp:HiddenField ID="IsFinalized" runat="server" ClientIDMode="Static" Value="0" />
                            <asp:HiddenField ID="rowCount" runat="server" ClientIDMode="Static" Value="1" />
                            <input id="btnCencelDtl" type="button" class="btn btn-primary pull-left" value="Cancel" onclick="location.href = '/Account/PurchaseOrder';" />
                            <asp:Button ID="btnSaveDtl" runat="server" ClientIDMode="Static" class="btn btn-primary PurchaseOrderBtnDetail" Text="Save" OnClick="SaveBtnDetailClick" />
                            <asp:Button ID="btnConverOrder" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Order" Visible="false" OnClick="BtnConvertOrderClick" />
                             <asp:Button ID="btnPrint" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right" Visible="false" Text="Print"  OnClientClick="javascript:window.print();" />
                        </div>

                    </div>
                </asp:Panel>
                <asp:HiddenField ID="CompanyCode" runat="server" ClientIDMode="Static"/>
            </div>
        </div>
    </div>
</asp:Content>

﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GrnEdit.aspx.cs" Inherits="XpressBilling.Account.GrnEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <div class="page-header">GRN</div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Order Type</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="GRNType" class="form-control required" ClientIDMode="Static">
                            <asp:ListItem Value="0" Text="Goods Receipt Auto"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Manual Goods Receipt"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Goods Receipt</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="GoodsReceipt" ReadOnly="true" class="form-control required" placeholder="Goods Receipt" ClientIDMode="Static"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="ManualSequenceNo" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="AutoSequenceNo" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="ManualSequenceNoID" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="AutoSequenceNoID" ClientIDMode="Static" />
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
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Supplier ID</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="SupplierId" class="form-control required" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="SupplierIdSelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="SupplierIdText" Visible="false" ReadOnly="true" class="form-control required" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Date</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="CreatedDate" class="form-control required" ReadOnly="true" placeholder="Date" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Name</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Name" class="form-control required" placeholder="Name" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Location</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="Location" class="form-control required" placeholder="Location" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Purchase Order</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:DropDownList runat="server" ID="PurchaseOrder" class="form-control required" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="PurchaseOrderSelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="PurchaseOrderText" Visible="false" ReadOnly="true" class="form-control required" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">User</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="CreatedUser" class="form-control required" ReadOnly="true" placeholder="User" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Packing Slip</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="PackingSlip" class="form-control required" placeholder="Packing Slip" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Total Qty</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="TotalQty" class="form-control" ReadOnly="true" Text="0" placeholder="Total Qty" ClientIDMode="Static"></asp:TextBox>
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
                        <asp:GridView ID="GRNDetail" runat="server" class="table" ClientIDMode="Static" ShowFooter="False"  AutoGenerateColumns="false" DataKeyNames="ID" >
                            <RowStyle CssClass="Odd" />
                            <AlternatingRowStyle CssClass="Even" />
                           
                            <Columns>
                                <asp:TemplateField HeaderText="No:">
                                    <ItemTemplate >
                                       <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item" ControlStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GRItem" class="form-control GRItem" ReadOnly="true" ClientIDMode="Static" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GRName" class="form-control GRName" ReadOnly="true" ClientIDMode="Static" runat="server" Text='<%# Bind("ItemName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GRUnit" class="form-control GRUnit" ReadOnly="true" ClientIDMode="Static" runat="server" Text='<%# Bind("BaseUnitCode") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Qnty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="OrderQuantity" class="form-control OrderQuantity" ReadOnly="true"  ClientIDMode="Static" runat="server" Text='<%# Bind("OrderQty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received Qnty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="ReceivedQuantity" class="form-control ReceivedQuantity txtNumeric"  ClientIDMode="Static" runat="server" Text='<%# Bind("OrderQty") %>'></asp:TextBox>
                                        <asp:HiddenField ID="prevRecvdQnty"  ClientIDMode="Static" runat="server" Value='<%# Bind("OrderQty") %>'></asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-8">
                        <div class="form-group ">
                            <label class="control-label col-xs-12 col-sm-4 col-md-4">Reference</label>
                            <div class="col-xs-12 col-sm-8 col-md-8">
                                <asp:TextBox runat="server" ID="Reference" class="form-control  " placeholder="Reference" ClientIDMode="Static"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <div class="row">
                            <asp:HiddenField ID="GRNId" runat="server" ClientIDMode="Static"/>
                            <asp:HiddenField runat="server" ID="PageStatus" ClientIDMode="Static" />
                            <asp:Button ID="btnSaveDtl" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Save" OnClick="SaveBtnDetailClick" />
                            <asp:Button ID="btnConverGRN" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Convert" Visible="false" OnClick="BtnConvertGRNClick" />
                            <a id="btnCencelDtl" href="/Account/GRN" runat="server" class="btn btn-primary">Cancel</a>
                            <asp:Button ID="btnPrint" runat="server" ClientIDMode="Static" class="btn btn-primary pull-right" Visible="false" Text="Print"  OnClientClick="javascript:window.print();" />
                        </div>

                    </div>
                </asp:Panel>
                <asp:HiddenField ID="CompanyCode" runat="server" ClientIDMode="Static"/>
            </div>
        </div>
    </div>
</asp:Content>

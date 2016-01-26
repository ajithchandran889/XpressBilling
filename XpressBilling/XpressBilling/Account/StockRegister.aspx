<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockRegister.aspx.cs" Inherits="XpressBilling.Account.StockRegister" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="row content-holder">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <div class="page-header">Stock Register</div>
                <div class="form-group">
                   <label class="control-label col-xs-12 col-sm-4 col-md-2">Item </label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <%--<asp:DropDownList runat="server" ID="ItemCode" class="form-control required" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ItemCodeSelectedIndexChanged">
                            
                        </asp:DropDownList>--%>
                        <asp:TextBox runat="server" ID="ItemCodeSR" class="form-control required" placeholder="Item Code"  ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Description</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="ItemNameSR" class="form-control required" placeholder="Item Name"  ClientIDMode="Static"></asp:TextBox>
                     </div>
                </div>
                <div class="form-group">

                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Period From</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="PeriodFrom" class="form-control required"  ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Location</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="LocationSR" class="form-control required" placeholder="Location"  ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-xs-12 col-sm-4 col-md-2">Period To</label>
                    <div class="col-xs-12 col-sm-8 col-md-2">
                        <asp:TextBox runat="server" ID="PeriodTo" class="form-control required"  ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div>
                    <asp:HiddenField ID="StockRegisterPage" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="CompanyCode" ClientIDMode="Static" runat="server" />
                    <asp:Label ID="message" runat="server"></asp:Label>
                </div>
                <asp:Panel runat="server" ID="gridDetails" Visible="false">
                    <div class="grid_wrapper">
                        <div class="grid_header">
                            <h2 class="pull-left">Transaction</h2>
                            <div class="pull-right">
                                <span class="icon-wrap pull-left"><i class="glyphicon glyphicon-plus "></i></span>
                            </div>
                        </div>
                        <asp:GridView ID="StockRegisterDetail" runat="server" class="table" ClientIDMode="Static" ShowFooter="False"  AutoGenerateColumns="false"  >
                            <RowStyle CssClass="Odd" />
                            <AlternatingRowStyle CssClass="Even" />
                           
                            <Columns>
                                <asp:TemplateField HeaderText="No:" ControlStyle-Width="50">
                                    <ItemTemplate >
                                       <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Transaction" HeaderText="Transaction"></asp:BoundField>
                                <asp:BoundField DataField="DocNo" HeaderText="Document No"></asp:BoundField>
                                <asp:BoundField DataField="DocDate" HeaderText="Document Date"></asp:BoundField>
                                <asp:BoundField DataField="BaseUnit" HeaderText="Base Unit"></asp:BoundField>
                                <asp:BoundField DataField="InQty" HeaderText="In Qty"></asp:BoundField>
                                <asp:BoundField DataField="OutQnty" HeaderText="Out Qty"></asp:BoundField>
                                <asp:BoundField DataField="AvilableQnty" HeaderText="Avilable Qty"></asp:BoundField>
                                <asp:BoundField DataField="UnitCost" HeaderText="Unit Cost"></asp:BoundField>
                                <%--<asp:BoundField DataField="Total" HeaderText="Total"></asp:BoundField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:Panel>
                <div class="form-group">
                    <div class="col-xs-10 col-md-8">

                        <asp:Button ID="SearchBtn" runat="server" ClientIDMode="Static" class="btn btn-primary" Text="Search" OnClick="SearchBtnClick" />
                    </div>
                </div>
                
               </div>
            </div>
        </div>
</asp:Content>

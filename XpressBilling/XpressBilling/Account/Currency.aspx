<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="Currency.aspx.cs" Inherits="XpressBilling.Account.Currency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="ace-settings-container" id="ace-settings-container">
            <div class="btn btn-app btn-xs btn-warning ace-settings-btn" id="ace-settings-btn"><i class="ace-icon fa fa-cog bigger-130"></i></div>
            <div class="ace-settings-box clearfix" id="ace-settings-box">
                <div class="pull-left width-50">
                    <div class="ace-settings-item">
                        <div class="pull-left">
                            <select id="skin-colorpicker" class="hide">
                                <option data-skin="no-skin" value="#438EB9">#438EB9</option>
                                <option data-skin="skin-1" value="#222A2D">#222A2D</option>
                                <option data-skin="skin-2" value="#C6487E">#C6487E</option>
                                <option data-skin="skin-3" value="#D0D0D0">#D0D0D0</option>
                            </select>
                        </div>
                        <span>&nbsp; Choose Skin</span>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-navbar" />
                        <label class="lbl" for="ace-settings-navbar">Fixed Navbar</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-sidebar" />
                        <label class="lbl" for="ace-settings-sidebar">Fixed Sidebar</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-breadcrumbs" />
                        <label class="lbl" for="ace-settings-breadcrumbs">Fixed Breadcrumbs</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-rtl" />
                        <label class="lbl" for="ace-settings-rtl">Right To Left (rtl)</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-add-container" />
                        <label class="lbl" for="ace-settings-add-container">Inside <b>.container</b> </label>
                    </div>
                </div>
                <!-- /.pull-left -->

                <div class="pull-left width-50">
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-hover" />
                        <label class="lbl" for="ace-settings-hover">Submenu on Hover</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-compact" />
                        <label class="lbl" for="ace-settings-compact">Compact Sidebar</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-highlight" />
                        <label class="lbl" for="ace-settings-highlight">Alt. Active Item</label>
                    </div>
                </div>
                <!-- /.pull-left -->
            </div>
            <!-- /.ace-settings-box -->
        </div>
        <!-- /.ace-settings-container -->

        <div class="page-header">
            <h1>Currency <small><i class="ace-icon fa fa-angle-double-right"></i></small></h1>
        </div>
        <!-- /.page-header -->

        <div class="row">
            <div class="col-sm-6">
                <h3>Currency </h3>
                <hr>
                <div class="row">

                    <div class="form-group">

                        <div class="col-sm-12 col-sx-12 col-md-4">
                            <label for="#">Currency</label>
                            <input runat="server" type="text" class="form-control validate[required]" name="inputCurrency" id="inputCurrency" >
                        </div>
                        <div class="col-sm-12 col-sx-12 col-md-4">
                            <label for="#">Name</label>
                            <input runat="server" type="text" class="form-control validate[required]" name="inputName" id="inputName">
                        </div>
                        <div class="col-sm-12 col-sx-12 col-md-4">
                            <label for="#">No. of Decimal</label>
                            <input runat="server" type="text" class="form-control validate[required,custom[integer]]" name="inputDecimal" id="inputDecimal">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <h3>Document </h3>
                <hr>
                <div class="row">
                    <div class="form-group">

                        <div class="col-sm-12 col-sx-12 col-md-4">
                            <label for="#">Date</label>
                            <asp:TextBox runat="server" ClientIDMode="Static" type="text" class="form-control validate[required]"  id="inputDate"></asp:TextBox>
                        </div>
                        <div class="col-sm-12 col-sx-12 col-md-4">
                            <label for="#">User</label>
                            <input runat="server" type="text" class="form-control validate[required]" name="inputUser" id="inputUser">
                        </div>
                        <div class="col-sm-12 col-sx-12 col-md-4">
                            <label for="#">Status</label>
                            <input runat="server" type="text" class="form-control validate[required]" name="inputStatus" id="inputStatus">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <asp:Button runat="server" id="btnSaveCurrency" ClientIDMode="Static" class="btn btn-primary pull-right padding-2" Text="SAVE" OnClick="btnSaveCurrencyClick" />
                <asp:Button runat="server" id="btnResetCurrency" ClientIDMode="Static" class="btn btn-warning pull-right" Text="RESET" OnClick="btnSaveCurrencyClick" />
               
            </div>
            
            <div class="col-sm-12 col-md-12">
                <div class="grid_wrapper">
                    <hr>
                    <div class="grid_header">
                        <h2>List View</h2>
                    </div>

                    <%--<table width="100%" cellspacing="0" cellpadding="0" border="0" class="grid">

                        <tr class="even">
                            <th width="161">Currency</th>
                            <th width="159">Name</th>
                            <th width="144">No. of Decimals</th>
                            <th width="144">User</th>
                            <th width="146">Status</th>
                        </tr>
                        <tr class="odd">
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Name</td>
                        </tr>
                        <tr class="even">
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr class="odd">
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr class="even">
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr class="odd">
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr class="even">
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr class="odd">
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        </tbody>
                    </table>--%>
                    <asp:GridView ID="listCurrency" runat="server"></asp:GridView>
                </div>


            </div>


            <!--col-sm-12 closed-->

        </div>
        <!--row closed-->

    </div>
</asp:Content>

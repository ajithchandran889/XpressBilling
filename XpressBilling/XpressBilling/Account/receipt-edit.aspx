<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="receipt-edit.aspx.cs" Inherits="XpressBilling.Account.receipt_edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
      <div class="row content-holder">
        <div class="col-xs-12 col-sm-12 col-md-12">
          <div class="page-header">Add Receipt Details</div>
            <div class="form-group">
              <label  class="control-label col-xs-12 col-sm-4 col-md-2">Transaction Type</label>
              <div class="col-xs-12 col-sm-8 col-md-2">
                <select class="form-control" id="#">
                  <option>Type</option>
                  <option>2</option>
                  <option>3</option>
                  <option>4</option>
                </select>
              </div>
              <label  class="control-label col-xs-12 col-sm-4 col-md-2">Business Partner</label>
              <div class="col-xs-12 col-sm-8 col-md-2">
                <input  class="form-control"  placeholder="Business Partner">
              </div>
              <label  class="control-label col-xs-12 col-sm-4 col-md-2">Currency</label>
              <div class="col-xs-12 col-sm-8 col-md-2">
                <input  class="form-control"  placeholder="Currency">
              </div>
              <label  class="control-label col-xs-12 col-sm-4 col-md-2">Reference</label>
              <div class="col-xs-12 col-sm-8 col-md-2">
                <input  class="form-control"  placeholder="Reference">
              </div>
              <label  class="control-label col-xs-12 col-sm-4 col-md-2">Receipt </label>
              <div class="col-xs-12 col-sm-8 col-md-2">
                <input  class="form-control"  placeholder="Receipt ">
              </div>
              <label  class="control-label col-xs-12 col-sm-4 col-md-2">Date </label>
              <div class="col-xs-12 col-sm-8 col-md-2">
                <input  class="form-control"  placeholder="Date ">
              </div>
              <label  class="control-label col-xs-12 col-sm-4 col-md-2">Date </label>
              <div class="col-xs-12 col-sm-8 col-md-2">
                <input  class="form-control"  placeholder="Date">
              </div>
              <label  class="control-label col-xs-12 col-sm-4 col-md-2">Cashier </label>
              <div class="col-xs-12 col-sm-8 col-md-2">
                <input  class="form-control"  placeholder="Cashier">
              </div>
              <label  class="control-label col-xs-12 col-sm-4 col-md-2">Amount </label>
              <div class="col-xs-12 col-sm-8 col-md-2">
                <input  class="form-control"  placeholder="Amount ">
              </div>
              <label  class="control-label col-xs-12 col-sm-4 col-md-2">Location </label>
              <div class="col-xs-12 col-sm-8 col-md-2">
                <input  class="form-control"  placeholder="Location ">
              </div>
              <label  class="control-label col-xs-12 col-sm-4 col-md-2">Status</label>
              <div class="col-xs-12 col-sm-8 col-md-2">
                <input  class="form-control"  placeholder="Status">
              </div>
             
            </div>
          <div class="grid_wrapper margin-bottom-15">
            <div class="grid_header">
              <h2 class="pull-left">Transaction</h2>
              <div class="pull-right"> <span class="icon-wrap pull-left"> <i class="glyphicon glyphicon-plus "></i></span> <span class="icon-wrap pull-left"> <i class="glyphicon glyphicon-trash"></i></span> </div>
            </div>
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="table">
                <tbody>
                  <tr class="even">
                    <th rowspan="2" class="width_25">&nbsp;</th>
                    <th rowspan="2">POS</th>
                    <th rowspan="2">Payment Mode</th>
                    <th colspan="2">Reference</th>
                    <th rowspan="2">Due Amount</th>
                    <th rowspan="2">Amount</th>
                    <th rowspan="2">TDS Amt</th>
                    <th rowspan="2">Net Amount</th>
                   
                    <th rowspan="2" class="width_25"><input type="checkbox" class="checkbox" /></th>
                  </tr>
                  <tr class="even">
                    <th>Number</th>
                    <th>Date</th>
                  </tr>
                  <tr class="odd">
                    <td><a href="#" class="glyphicon glyphicon-pencil"></a></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                   <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                   <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input type="checkbox" class="checkbox" /></td>
                  </tr>
                  <tr class="even">
                     <td><a href="#" class="glyphicon glyphicon-pencil"></a></td>
                      <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                   <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                   <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                     <td><input type="checkbox" class="checkbox" /></td>
                  </tr>
                  <tr class="odd">
                    <td><a href="#" class="glyphicon glyphicon-pencil"></a></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                   <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                   <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                     <td><input type="checkbox" class="checkbox" /></td>
                  </tr>
                  <tr class="even">
                    <td><a href="#" class="glyphicon glyphicon-pencil"></a></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                   <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                   <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                     <td><input type="checkbox" class="checkbox" /></td>
                  </tr>
                  <tr class="odd">
                    <td><a href="#" class="glyphicon glyphicon-pencil"></a></td>
                      <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                   <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                   <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                     <td><input type="checkbox" class="checkbox" /></td>
                  </tr>
              </table>
          </div>
          
          <div class="grid_wrapper margin-bottom-15">
            <div class="grid_header">
              <h2 class="pull-left">Recent Transaction</h2>
              <div class="pull-right"> <span class="icon-wrap pull-left"> <i class="glyphicon glyphicon-plus "></i></span> <span class="icon-wrap pull-left"> <i class="glyphicon glyphicon-trash"></i></span> </div>
            </div>
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="table">
                <tbody>
                  <tr class="even">
                    <th class="width_25">&nbsp;</th>
                    <th>Receipt</th>
                    <th>Date</th>
                    <th>Transaction Type</th>
                     <th>Payment Mode</th>
                    <th>Reference</th>
                    <th>Due Amount</th>
                    <th>Received Amt</th>
                   
                    <th>TDS Amt</th>
                    <th>Cashier</th>
                    <th>Enter---U</th>
                    <th>Status</th> <th class="width_25"><input type="checkbox" class="checkbox" /></th>
                  </tr>
                  <tr class="odd">
                    <td><a href="#" class="glyphicon glyphicon-pencil"></a></td>
                    <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                      <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                    <td><input type="checkbox" class="checkbox" /></td>
                  </tr>
                  <tr class="even">
                     <td><a href="#" class="glyphicon glyphicon-pencil"></a></td>
                      <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                      <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                    <td><input type="checkbox" class="checkbox" /></td>
                    
                  </tr>
                  <tr class="odd">
                     <td><a href="#" class="glyphicon glyphicon-pencil"></a></td>
                      <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                      <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                    <td><input type="checkbox" class="checkbox" /></td>
                  </tr>
                  <tr class="even">
                    <td><a href="#" class="glyphicon glyphicon-pencil"></a></td>
                      <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                      <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                    <td><input type="checkbox" class="checkbox" /></td>
                  </tr>
                  <tr class="odd">
                    <td><a href="#" class="glyphicon glyphicon-pencil"></a></td>
                      <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                      <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                    <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                     <td><input  class="form-control"  placeholder=""></td>
                    <td><input type="checkbox" class="checkbox" /></td>
                  </tr>
              </table>
          </div>
          
          
          <div class="col-xs-12 col-sm-12 col-md-12">
            <button type="Cancel" class="btn btn-primary pull-left">Cancel</button>
            <button type="Save" class="btn btn-primary pull-left">Save</button>
          </div>
        </div>
      </div>
    </div>
</asp:Content>

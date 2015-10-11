<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="XpressBilling.Account.Company" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
          <div class="page-content">
       <div class="row content-holder">
          <div class="col-sm-12 col-md-12">
            <div class="page-header">Company
                <div class="pull-right"><a href="CompanyEdit">ADD</a></div>
            </div>
            <div class="grid_wrapper">
              <div class="grid_header">
                <h2>List View</h2>
              </div>
              <asp:GridView ID="listCompany" runat="server" CssClass="table" AllowPaging="true" 
                  OnPageIndexChanging="listCompanyPageIndexChanging" PageSize="2">
                  <PagerStyle HorizontalAlign="Right" />
              </asp:GridView>
            </div>
            
            <%--<div class="grid_wrapper">
              <div class="grid_header">
                <h2>List View</h2>
              </div>
              <table width="100%" cellspacing="0" cellpadding="0" border="0" class="table">
                <tbody>
                  <tr class="even">
                    <th>&nbsp;</th>
                    <th>Company</th>
                    <th>Name</th>
                    <th>Contact Person</th>
                    <th>Mobile</th>
                    <th>City</th>
                    <th>Country</th>
                    <th>Zip/Postal Code</th>
                    <th>E-mail</th>
                    <th><input type="checkbox" class="checkbox" /></th>
                  </tr>
                  <tr class="odd">
                    <td><a href="company-edit.html" class="glyphicon glyphicon-pencil"></a></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><input type="checkbox" class="checkbox" /></td>
                  </tr>
                  <tr class="even">
                    <td><a href="company-edit.html" class="glyphicon glyphicon-pencil"></a></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><input type="checkbox" class="checkbox" /></td>
                  </tr>
                  <tr class="odd">
                    <td><a href="company-edit.html" class="glyphicon glyphicon-pencil"></a></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><input type="checkbox" class="checkbox" /></td>
                  </tr>
                  <tr class="even">
                    <td><a href="company-edit.html" class="glyphicon glyphicon-pencil"></a></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><input type="checkbox" class="checkbox" /></td>
                  </tr>
                  <tr class="odd">
                    <td><a href="company-edit.html" class="glyphicon glyphicon-pencil"></a></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><input type="checkbox" class="checkbox" /></td>
                  </tr>
              </table>
            </div>
            <nav>
              <ul class="pagination pull-right">
                <li> <a href="#" aria-label="Previous"> <span aria-hidden="true">&laquo;</span> </a> </li>
                <li><a href="#">1</a></li>
                <li><a href="#">2</a></li>
                <li><a href="#">3</a></li>
                <li><a href="#">4</a></li>
                <li><a href="#">5</a></li>
                <li> <a href="#" aria-label="Next"> <span aria-hidden="true">&raquo;</span> </a> </li>
              </ul>
            </nav>--%>
          </div>
        </div>
      </div>
</asp:Content>


function getParameterByNameMaster(name) {
    var url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)", "i"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}


function MasterMenu(page) {
  
    var spltPage = page.split('?');
    spltPage = spltPage[0].split('/');


    switch (spltPage[2].toLowerCase()) {
        case "company":
            document.getElementById("breadCrumb_three").innerHTML = "Organisation";
            document.getElementById("breadCrumb_four").innerHTML = "Company";
            break;
        case "companyedit":
            document.getElementById("breadCrumb_three").innerHTML = "Organisation";
            document.getElementById("breadCrumb_four").innerHTML = "Company";
            break;

        case "location":
            document.getElementById("breadCrumb_three").innerHTML = "Organisation";
            document.getElementById("breadCrumb_four").innerHTML = "Location";
            break;
        case "locationedit":
            document.getElementById("breadCrumb_three").innerHTML = "Organisation";
            document.getElementById("breadCrumb_four").innerHTML = "Location";
            break;

        case "currency":
            document.getElementById("breadCrumb_three").innerHTML = "Code";
            document.getElementById("breadCrumb_four").innerHTML = "Currency";
            break;
        case "currencyedit":
            document.getElementById("breadCrumb_three").innerHTML = "Code";
            document.getElementById("breadCrumb_four").innerHTML = "Currency";
            break;


        case "taxcode":
            document.getElementById("breadCrumb_three").innerHTML = "Code";
            document.getElementById("breadCrumb_four").innerHTML = "Tax";
            break;

        case "edittaxcode":
            document.getElementById("breadCrumb_three").innerHTML = "Code";
            document.getElementById("breadCrumb_four").innerHTML = "Tax";
            break;


        case "baseunit":
            document.getElementById("breadCrumb_three").innerHTML = "Code";
            document.getElementById("breadCrumb_four").innerHTML = "Base Unit";
            break;

        case "editbaseunit":
            document.getElementById("breadCrumb_three").innerHTML = "Code";
            document.getElementById("breadCrumb_four").innerHTML = "Base Unit";
            break;

        case "bankcode":
            document.getElementById("breadCrumb_three").innerHTML = "Code";
            document.getElementById("breadCrumb_four").innerHTML = "Bank";
            break;

        case "editbankcode":
            document.getElementById("breadCrumb_three").innerHTML = "Code";
            document.getElementById("breadCrumb_four").innerHTML = "Bank";
            break;

        case "bankmst":
            document.getElementById("breadCrumb_three").innerHTML = "Common";
            document.getElementById("breadCrumb_four").innerHTML = "Bank";
            break;

        case "editbankmst":
            document.getElementById("breadCrumb_three").innerHTML = "Common";
            document.getElementById("breadCrumb_four").innerHTML = "Bank";
            break;

        case "paymentmode":
            document.getElementById("breadCrumb_three").innerHTML = "Common";
            document.getElementById("breadCrumb_four").innerHTML = "Payment Mode";
            break;

        case "paymentmodeedit":
            document.getElementById("breadCrumb_three").innerHTML = "Common";
            document.getElementById("breadCrumb_four").innerHTML = "Payment Mode";
            break;

        case "taxmst":
            document.getElementById("breadCrumb_three").innerHTML = "Common";
            document.getElementById("breadCrumb_four").innerHTML = "Tax";
            break;

        case "edittaxmst":
            document.getElementById("breadCrumb_three").innerHTML = "Common";
            document.getElementById("breadCrumb_four").innerHTML = "Tax";
            break;

        case "pricebook":
            document.getElementById("breadCrumb_three").innerHTML = "Common";
            document.getElementById("breadCrumb_four").innerHTML = "Price Book";
            break;

        case "pricebookedit":
            document.getElementById("breadCrumb_three").innerHTML = "Common";
            document.getElementById("breadCrumb_four").innerHTML = "Price Book";
            break;

        case "employee":
            document.getElementById("breadCrumb_three").innerHTML = "Common";
            document.getElementById("breadCrumb_four").innerHTML = "Employee";
            break;

        case "editemployeemaster":
            document.getElementById("breadCrumb_three").innerHTML = "Common";
            document.getElementById("breadCrumb_four").innerHTML = "Employee";
            break;

        case "firstfreenumber":
            document.getElementById("breadCrumb_three").innerHTML = "Common";
            document.getElementById("breadCrumb_four").innerHTML = "First Free Number";
            break;

        case "firstfreenumberedit":
            document.getElementById("breadCrumb_three").innerHTML = "Common";
            document.getElementById("breadCrumb_four").innerHTML = "First Free Number";
            break;

        case "contact":
            document.getElementById("breadCrumb_three").innerHTML = "Common";
            document.getElementById("breadCrumb_four").innerHTML = "Contacts";
            break;

        case "editcontact":
            document.getElementById("breadCrumb_three").innerHTML = "Common";
            document.getElementById("breadCrumb_four").innerHTML = "Contacts";
            break;

        case "itemgroup":
            document.getElementById("breadCrumb_three").innerHTML = "Item";
            document.getElementById("breadCrumb_four").innerHTML = "Item Group";
            break;

        case "edititemgroup":
            document.getElementById("breadCrumb_three").innerHTML = "Item";
            document.getElementById("breadCrumb_four").innerHTML = "Item Group";
            break;

        case "manufacturer":
            document.getElementById("breadCrumb_three").innerHTML = "Item";
            document.getElementById("breadCrumb_four").innerHTML = "Manufacturer";
            break;

        case "editmanufacturer":
            document.getElementById("breadCrumb_three").innerHTML = "Item";
            document.getElementById("breadCrumb_four").innerHTML = "Manufacturer";
            break;

        case "itemmaster":
            document.getElementById("breadCrumb_three").innerHTML = "Item";
            document.getElementById("breadCrumb_four").innerHTML = "Item Data";
            break;

        case "itemmasteredit":
            document.getElementById("breadCrumb_three").innerHTML = "Item";
            document.getElementById("breadCrumb_four").innerHTML = "Item Data";
            break;

        case "country":
            document.getElementById("breadCrumb_three").innerHTML = "Country";
            document.getElementById("breadCrumb_four").innerHTML = "Country";
            break;

        case "countryedit":
            document.getElementById("breadCrumb_three").innerHTML = "Country";
            document.getElementById("breadCrumb_four").innerHTML = "Country";
            break;

        case "city":
            document.getElementById("breadCrumb_three").innerHTML = "Country";
            document.getElementById("breadCrumb_four").innerHTML = "City";
            break;

        case "editcity":
            document.getElementById("breadCrumb_three").innerHTML = "Country";
            document.getElementById("breadCrumb_four").innerHTML = "City";
            break;

        case "bussinesspartner":
            document.getElementById("breadCrumb_three").innerHTML = "Bussiness Partner";
            document.getElementById("breadCrumb_four").innerHTML = "Bussiness Partner";
            break;

        case "bussinesspartneredit":
            document.getElementById("breadCrumb_three").innerHTML = "Bussiness Partner";
            document.getElementById("breadCrumb_four").innerHTML = "Bussiness Partner";
            break;

            BussinessPartnerEdit

            ///sales starts 
        case "salesquotation":
            document.getElementById("breadCrumb_two").innerHTML = "Sales";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "Sales Quotation";
            break;

        case "sqedit":
            document.getElementById("breadCrumb_two").innerHTML = "Sales";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "Sales Quotation";
            break;

        case "invoice":
            document.getElementById("breadCrumb_two").innerHTML = "Sales";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "Sales Order";
            break;

        case "invoiceedit":
            document.getElementById("breadCrumb_two").innerHTML = "Sales";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "Sales Order";
            break;

        case "manualinvoice":
            document.getElementById("breadCrumb_two").innerHTML = "Sales";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "Manual Invoice";
            break;

        case "manualinvoiceedit":
            document.getElementById("breadCrumb_two").innerHTML = "Sales";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "Manual Invoice";
            break;


        case "salesreturn":
            document.getElementById("breadCrumb_two").innerHTML = "Sales";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "Sales Return";
            break;

        case "salesreturnedit":
            document.getElementById("breadCrumb_two").innerHTML = "Sales";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "Sales Return";
            break;



            ///sales ends 

            ////Purchase starts

        case "purchaseorder":
            document.getElementById("breadCrumb_two").innerHTML = "Purchase";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "Purchase Order";
            break;

        case "purchaseorderedit":
            document.getElementById("breadCrumb_two").innerHTML = "Purchase";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "Purchase Order";
            break;


            //// Purchase ends


            ///Inventory starts

        case "grn":
            document.getElementById("breadCrumb_two").innerHTML = "Inventory";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "Goods Receipt Note";
            break;

        case "grnedit":
            document.getElementById("breadCrumb_two").innerHTML = "Inventory";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "Goods Receipt Note";
            break;

        case "stockentry":
            document.getElementById("breadCrumb_two").innerHTML = "Inventory";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "Stock Adjustment";
            break;

        case "stockentryedit":
            document.getElementById("breadCrumb_two").innerHTML = "Inventory";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "Stock Adjustment";
            break;




        case "stockregister":
            document.getElementById("breadCrumb_two").innerHTML = "Inventory";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "Stock Register";
            break;

            /// inventory ends

            ///Cash starts

        case "rptsalesquote":
            document.getElementById("breadCrumb_two").innerHTML = "Cash";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "ReportSalesQuotation";
            break;


        case "rptsalesinvoice":
            document.getElementById("breadCrumb_two").innerHTML = "Cash";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "ReportSalesInvoice";
            break;


        case "rptreceipt":
            document.getElementById("breadCrumb_two").innerHTML = "Cash";
            document.getElementById("breadCrumb_three").innerHTML = "Transaction";
            document.getElementById("breadCrumb_four").innerHTML = "ReportReceipt";
            break;

            ///Cash ends

            ///tools starts

        case "users":
            document.getElementById("breadCrumb_two").innerHTML = "Tools";
            document.getElementById("breadCrumb_three").innerHTML = "User Management";
            document.getElementById("breadCrumb_four").innerHTML = "User";
            break;

        case "adduser":
            document.getElementById("breadCrumb_two").innerHTML = "Tools";
            document.getElementById("breadCrumb_three").innerHTML = "User Management";
            document.getElementById("breadCrumb_four").innerHTML = "User";
            break;



        default:

            document.getElementById("breadCrumb_one").style.display = "none";
            document.getElementById("breadCrumb_two").style.display = "none";
            document.getElementById("breadCrumb_three").style.display = "none";
            document.getElementById("breadCrumb_four").style.display = "none";
            break;

            ///tools end
    }
}

function SalesMenu(page) {
    var spltPage = page.split('?');
    spltPage = spltPage[0].split('/');

    switch (spltPage[2].toLowerCase()) {
        case "employee":
            document.getElementById("breadCrumb_two").innerHTML = "Sales";
            document.getElementById("breadCrumb_three").innerHTML = "Master";
            document.getElementById("breadCrumb_four").innerHTML = "Employee";
            break;

        case "bussinesspartner":
            document.getElementById("breadCrumb_two").innerHTML = "Sales";
            document.getElementById("breadCrumb_three").innerHTML = "Master";
            document.getElementById("breadCrumb_four").innerHTML = "Bussiness Partner";
            break;
    }
}

function PurchaseMenu(page) {
    var spltPage = page.split('?');
    spltPage = spltPage[0].split('/');

    switch (spltPage[2].toLowerCase()) {
        case "employee":
            document.getElementById("breadCrumb_two").innerHTML = "Purchase";
            document.getElementById("breadCrumb_three").innerHTML = "Master";
            document.getElementById("breadCrumb_four").innerHTML = "Employee";
            break;
    }
}


function InventoryMenu(page) {

    var spltPage = page.split('?');
    spltPage = spltPage[0].split('/');

    switch (spltPage[2].toLowerCase()) {
        case "location":
            document.getElementById("breadCrumb_two").innerHTML = "Inventory";
            document.getElementById("breadCrumb_three").innerHTML = "Master";
            document.getElementById("breadCrumb_four").innerHTML = "Location";
            break;
    }

}

function fnBrudCrumb() {
    var page = window.location.href.toString().split(window.location.host)[1];

    if (page.indexOf("Account") == 1) {
        {
            var subMenuId = getParameterByNameMaster("subMenuId");

            if (subMenuId == "1") {
                SalesMenu(page);
            } else if (subMenuId == "2") {
                PurchaseMenu(page);
            } else if (subMenuId == "3") {
                InventoryMenu(page);
            } else {
                MasterMenu(page);
            }
        }
    } else {
        document.getElementById("breadCrumb_one").style.display = "none";
        document.getElementById("breadCrumb_two").style.display = "none";
        document.getElementById("breadCrumb_three").style.display = "none";
        document.getElementById("breadCrumb_four").style.display = "none";
    }
}
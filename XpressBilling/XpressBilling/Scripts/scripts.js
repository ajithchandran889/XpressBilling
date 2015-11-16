var itemMasterArray = [];""
var itemMasterDetails = {};
var itemMasterArraySQ = [];
var itemMasterDetailsEditSQ = {};
var SQItemRowDetails = [];
var itemMasterArrayStockEntry = [];
var itemMasterDetailsEditStockEntry = {};
var SEItemRowDetails = [];
$(function () {
    $("#inputDate").datepicker(); 
    $("#FormationDate").datepicker();
    $("#Date").datepicker();
    $("#DOJ").datepicker(); 
    $("#Validity").datepicker();
});


$(document).ready(function () {
    if ($("#SalesQuotationId").val() != "" && $("#PageStatus").val() != "create")
    {
        itemMasterArraySQ = [];
        itemMasterDetailsSQ = {};
        var i = 0;
        $("tr", $("#SalesQuotationDetail")).each(function () {
            var val = $("input[id*='Item']", $(this)).val();
            var qnty = parseInt($("input[id*='Quantity']", $(this)).val());
            var rate = parseInt($("input[id*='Rate']", $(this)).val());
            if(typeof(val)!=="undefined")
            {
                
                SQItemRowDetails[i] = [qnty, rate, qnty * rate, parseInt($("input[id*='DiscAmt']", $(this)).val()), parseInt($("input[id*='TaxAmt']", $(this)).val()), parseInt($("input[id*='NetAmt']", $(this)).val())];
                i++;
            }

        });
    }
    if ($("#StokeEntryMstId").val() != "" && $("#PageStatus").val() != "create") {
        itemMasterArrayStockEntry = [];
        itemMasterDetailsStockEntry = {};
        var i = 0;
        $("tr", $("#StockEntryDetail")).each(function () {
            var val = $("input[id*='Item']", $(this)).val();
            var qnty = parseInt($("input[id*='SEQuantity']", $(this)).val());
            var rate = parseInt($("input[id*='SERate']", $(this)).val());
            if (typeof (val) !== "undefined") {
                SEItemRowDetails[i] = [qnty, rate, qnty * rate];
                i++;
            }

        });
    }
    $("#mainForm").validate();
});


$(document).ready(function () {
    
    SearchText();

    function DeleteConfirm()
    {
        var Ans = confirm("Do you want to Delete Selected Employee Record?");
        if (Ans) {
            return true;
        }
        else {
            return false;
        }
    }

    $(document).on("click", "#saveItemMaster", function () {
        if (parseInt($("#SafetStock").val()) < parseInt($("#ReorderQty").val())) {
            alert("Reorder quantity should be lesser or equal to safety stock");
            return false;
        }
        else {
            return true;
        }

    });

    $(document).on("keyup", "#Currency", function () {
        if (this.id == "Currency") {

        }
    });

    $(document).on("change", "#Type", function () {

        if ($("#Type").val() == "0") {
            $("#OrderType_0").show();
            $("#OrderType_1").hide();
        }
        else if ($("#Type").val() == "1") {
            $("#OrderType_0").hide();
            $("#OrderType_1").show();
        }
    });

    $(document).on("change", "#QuotationType", function () {

        if ($("#QuotationType").val() == "0") {
            $("#Quotation").val($("#CashSequenceNo").val());
        }
        else if ($("#QuotationType").val() == "1") {
            $("#Quotation").val($("#CreditSequenceNo").val());
        }
    });

    $(document).on("change", "#AdjustmentType", function () {

        if ($("#AdjustmentType").val() == "0") {
            $("#Document").val($("#AdditionSequenceNo").val());
        }
        else if ($("#AdjustmentType").val() == "1") {
            $("#Document").val($("#DeductionSequenceNo").val());
        }
        else if ($("#AdjustmentType").val() == "2") {
            $("#Document").val($("#OpeningSequenceNo").val());
        }
    });

    $(document).on("change", "#InvoiceType", function () {

        if ($("#InvoiceType").val() == "0") {
            $("#Invoice").val($("#CashSequenceNo").val());
        }
        else if ($("#InvoiceType").val() == "1") {
            $("#Invoice").val($("#CreditSequenceNo").val());
        }
    });

    $(document).on("change", "#BusinessPartnerType", function () {

        if ($("#BusinessPartnerType").val() == "0") {
            $("#OrderType_0").show();
            $("#OrderType_1").hide();
        }
        else if ($("#BusinessPartnerType").val() == "1") {
            $("#OrderType_0").hide();
            $("#OrderType_1").show();
        }
    });

    $(document).on("change", "#Transaction", function () {
        
        if($("#Transaction").val()=="1")
        {
            $("#lblBankDetail").text("Bank Code");
        }
        else
        {
            $("#lblBankDetail").text("Bank Account");
        }
    });

    $(document).on("change", "#Country", function () {
        var obj = {};
        obj.countryCode = $.trim($("#Country").val());
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "CompanyEdit.aspx/GetCities",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                $("#City").empty();
                $.each(data.d, function (i, j) {
                    $("#City").append(
                        $('<option></option>').val(j.cityCode).html(j.cityName)
                    );
                });
            },
            error: function (result) {
                alert("Error");
            }
        });
    });

    $(document).on("change", "#inputUpload", function () {
        var files = !!this.files ? this.files : [];
        if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

        if (/^image/.test(files[0].type)) { // only image file
            var reader = new FileReader(); // instance of the FileReader
            reader.readAsDataURL(files[0]); // read the local file

            reader.onloadend = function () { // set image data as background of div
             
                $("#imgPreview").attr('src', this.result);
            }
        }
    });

    //if ($("#SalesQuotationDetail").length > 0) {

    //    $("#SalesQuotationDetail tr:nth-child(2)").clone().appendTo("#SalesQuotationDetail");
    //    return false;
    //}
});
function SearchText() {
    $("#Company").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "AddUser.aspx/GatAllCompanies",
                dataType: "json",
                success: function (data) {
                    response(data.d);
                },
                error: function (result) {
                    alert("Error");
                }
            });
        }
    });
}
    function CalculateNetAmount(txtBox)
    {
        var row = txtBox.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        if (row.cells[1].getElementsByTagName("input")[0].value != "")
        {
            var rate = row.cells[3].getElementsByTagName("input")[0].value;
            var qnty = row.cells[4].getElementsByTagName("input")[0].value;
            var discountAmt = row.cells[7].getElementsByTagName("input")[0].value;
            var taxAmt = row.cells[9].getElementsByTagName("input")[0].value;
            if (qnty != "" && discountAmt != "" && taxAmt != "") {
                var amount = qnty * rate;
                var netAmount = (amount - discountAmt) * taxAmt;
                if (!SQItemRowDetails[rowIndex] && row.cells[10].getElementsByTagName("input")[0].value == "") {

                    row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                    SQItemRowDetails[rowIndex] = [qnty, rate, amount, discountAmt, taxAmt, netAmount];
                    if ($("#TotalAmount").val() == "") {
                        $("#TotalAmount").val(amount);
                    }
                    else {
                        $("#TotalAmount").val(parseInt($("#TotalAmount").val()) + parseInt(amount));
                    }
                    if ($("#TotalDiscountAmt").val() == "") {
                        $("#TotalDiscountAmt").val(discountAmt);
                    }
                    else {
                        $("#TotalDiscountAmt").val(parseInt($("#TotalDiscountAmt").val()) + parseInt(discountAmt));
                    }
                    if ($("#TotalTaxAmt").val() == "") {
                        $("#TotalTaxAmt").val(taxAmt);
                    }
                    else {
                        $("#TotalTaxAmt").val(parseInt($("#TotalTaxAmt").val()) + parseInt(taxAmt));
                    }
                    if ($("#TotalOrderAmt").val() == "") {
                        $("#TotalOrderAmt").val(netAmount);
                    }
                    else {
                        $("#TotalOrderAmt").val(parseInt($("#TotalOrderAmt").val()) + netAmount);
                    }
                }
                else {
                    //alert("okkk");
                    var itemArray = SQItemRowDetails[rowIndex];
                    var totalAmt = parseInt($("#TotalAmount").val());
                    var totalDiscount = parseInt($("#TotalDiscountAmt").val());
                    var totalTax = parseInt($("#TotalTaxAmt").val());
                    var totalNet = parseInt($("#TotalOrderAmt").val());
                    var oldAmount = parseInt(itemArray[2]);
                    var oldDiscount = parseInt(itemArray[3]);
                    var oldTax = parseInt(itemArray[4]);
                    var oldNet = parseInt(itemArray[5]);
                    totalAmt -= oldAmount;
                    totalDiscount -= oldDiscount;
                    totalTax -= oldTax;
                    totalNet -= oldNet;
                    totalAmt += amount;
                    totalDiscount += parseInt(discountAmt);
                    totalTax += parseInt(taxAmt);
                    totalNet += netAmount;
                    row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                    $("#TotalAmount").val(totalAmt);
                    $("#TotalDiscountAmt").val(totalDiscount);
                    $("#TotalTaxAmt").val(totalTax);
                    $("#TotalOrderAmt").val(totalNet);
                    SQItemRowDetails[rowIndex] = [qnty, rate, amount, discountAmt, taxAmt, netAmount];
                }


            }
        }
        
    }

    $(document).on("focusout", "#Quantity,#DiscAmt,#TaxAmt", function (e) {
        CalculateNetAmount(this);
    });

    $(document).on("keydown", "#TaxAmt", function (e) {
        if (e.keyCode == 9) {
            CalculateNetAmount(this);
            
        }
    });

    $(document).on("focusin", "#NetAmt", function (e) {
        CalculateNetAmount(this);
    });
    
    function SetSelectedRowSQ(lnk,selectedItem) {
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        var itemArr = itemMasterDetailsSQ[selectedItem];
        row.cells[2].getElementsByTagName("input")[0].value = itemArr[0];
        row.cells[3].getElementsByTagName("input")[0].value = itemArr[3];
        row.cells[5].getElementsByTagName("input")[0].value = itemArr[4];
        
        return false;
    }

    function SetSelectedRowStockEntry(lnk, selectedItem) {
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        var itemArr = itemMasterDetailsStockEntry[selectedItem];
        row.cells[2].getElementsByTagName("input")[0].value = itemArr[0];
        row.cells[3].getElementsByTagName("input")[0].value = itemArr[3];
        row.cells[5].getElementsByTagName("input")[0].value = itemArr[4];

        return false;
    }
    function SetSelectedRow(lnk, selectedItem) {
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        var itemArr = itemMasterDetails[selectedItem];
        row.cells[1].getElementsByTagName("input")[0].value = itemArr[1];
        row.cells[2].getElementsByTagName("input")[0].value = $("#Currency").val();
        row.cells[3].getElementsByTagName("input")[0].value = itemArr[2];
        row.cells[4].getElementsByTagName("input")[0].value = itemArr[3];
        return false;
    }

    $(".ItemCode").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PriceBookEdit.aspx/GetItemMasters",
                dataType: "json",
                success: function (data) {
                    itemMasterArray = [];
                    itemMasterDetails = {};
                    $.each(data.d, function (i, j) {
                        itemMasterArray.push(j.code);
                        itemMasterDetails[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice];
                    });
                    response(itemMasterArray);
                },
                error: function (result) {
                    alert("Error");
                }
            });
        },
        select: function (event, ui) {
            SetSelectedRow(this, ui.item.label);
        
        },
    });

    $("#SalesMan").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "SalesQuotationEdit.aspx/GetContactCodes",
                dataType: "json",
                success: function (data) {
                    var contactArray = [];
                    $.each(data.d, function (i, j) {
                        contactArray.push(j.code);
                    });
                    response(contactArray);
                },
                error: function (result) {
                    alert("Error");
                }
            });
        },
        //select: function (event, ui) {
        //    SetSelectedRow(this, ui.item.label);

        //},
    });

    $(".Name").attr('readonly', 'readonly');
    $(".Rate").attr('readonly', 'readonly');
    $(".Unit").attr('readonly', 'readonly');
    $(".NetAmt").attr('readonly', 'readonly');
    $("#TotalAmount").attr('readonly', 'readonly');
    $("#TotalDiscountAmt").attr('readonly', 'readonly');
    $("#TotalTaxAmt").attr('readonly', 'readonly');
    $("#TotalOrderAmt").attr('readonly', 'readonly');
    if ($("#SalesOrder").val()!="")
    {
        $(".gridTxtBox").attr('readonly', 'readonly');
    }
    var options = {
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PriceBookEdit.aspx/GetItemMasters",
                dataType: "json",
                success: function (data) {
                    itemMasterArraySQ = [];
                    itemMasterDetailsSQ = {};
                    $.each(data.d, function (i, j) {
                        itemMasterArraySQ.push(j.code);
                        itemMasterDetailsSQ[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode];
                    });
                    response(itemMasterArraySQ);
                },
                error: function (result) {
                    alert("Error");
                }
            });
        },
        select: function (event, ui) {
            SetSelectedRowSQ(this, ui.item.label);

        },
    };

    var optionsStockEntry = {
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PriceBookEdit.aspx/GetItemMasters",
                dataType: "json",
                success: function (data) {
                    itemMasterArrayStockEntry = [];
                    itemMasterDetailsStockEntry = {};
                    $.each(data.d, function (i, j) {
                        itemMasterArrayStockEntry.push(j.code);
                        itemMasterDetailsStockEntry[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode];
                    });
                    response(itemMasterArrayStockEntry);
                },
                error: function (result) {
                    alert("Error");
                }
            });
        },
        select: function (event, ui) {
            SetSelectedRowStockEntry(this, ui.item.label);

        },
    };

    //var optionsStockEntryName = {
    //    source: function (request, response) {
    //        $.ajax({
    //            type: "POST",
    //            contentType: "application/json; charset=utf-8",
    //            url: "PriceBookEdit.aspx/GetItemMasters",
    //            dataType: "json",
    //            success: function (data) {
    //                itemMasterArrayStockEntry = [];
    //                itemMasterDetailsStockEntry = {};
    //                $.each(data.d, function (i, j) {
    //                    itemMasterArrayStockEntry.push(j.code);
    //                    itemMasterDetailsStockEntry[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode];
    //                });
    //                response(itemMasterArrayStockEntry);
    //            },
    //            error: function (result) {
    //                alert("Error");
    //            }
    //        });
    //    },
    //    select: function (event, ui) {
    //        SetSelectedRowStockEntry(this, ui.item.label);

    //    },
    //};

    $(document).on("keydown", ".Item", function (e) {
      $(this).autocomplete(options);
    });

    $(document).on("keydown", ".StockItem", function (e) {
        $(this).autocomplete(optionsStockEntry);
    });

    function CalculateSEAmount(txtBox) {
        var row = txtBox.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        if (row.cells[1].getElementsByTagName("input")[0].value != "") {
            var rate = row.cells[3].getElementsByTagName("input")[0].value;
            var qnty = row.cells[4].getElementsByTagName("input")[0].value;
            if (qnty != "") {
                var amount = qnty * rate;
                if (!SEItemRowDetails[rowIndex] && row.cells[6].getElementsByTagName("input")[0].value == "") {

                    row.cells[6].getElementsByTagName("input")[0].value = amount;
                    SEItemRowDetails[rowIndex] = [qnty, rate, amount];
                    if ($("#Amount").val() == "" || $("#Amount").val() == "0") {
                        $("#Amount").val(amount);
                    }
                    else {
                        $("#Amount").val(parseInt($("#Amount").val()) + parseInt(amount));
                    }
                }
                else {
                    //alert("okkk");
                    var itemArray = SEItemRowDetails[rowIndex];
                    var totalAmt = parseInt($("#Amount").val());
                    var oldAmount = parseInt(itemArray[2]);
                    totalAmt -= oldAmount;
                    totalAmt += amount;
                    row.cells[6].getElementsByTagName("input")[0].value = amount;
                    $("#Amount").val(totalAmt);
                    SEItemRowDetails[rowIndex] = [qnty, rate, amount];
                }


            }
        }

    }

    $(document).on("focusout", "#SEQuantity", function (e) {
        CalculateSEAmount(this);
    });

  
    //$(".Item").autocomplete({
    //    source: function (request, response) {
    //        $.ajax({
    //            type: "POST",
    //            contentType: "application/json; charset=utf-8",
    //            url: "PriceBookEdit.aspx/GetItemMasters",
    //            dataType: "json",
    //            success: function (data) {
    //                itemMasterArraySQ = [];
    //                itemMasterDetailsSQ = {};
    //                $.each(data.d, function (i, j) {
    //                    itemMasterArraySQ.push(j.code); 
    //                    itemMasterDetailsSQ[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode];
    //                });
    //                response(itemMasterArraySQ);
    //            },
    //            error: function (result) {
    //                alert("Error");
    //            }
    //        });
    //    },
    //    select: function (event, ui) {
    //        SetSelectedRowSQ(this, ui.item.label);

    //    },
    //});
    if ($(".ItemCode").val()!="")
    {
        $(".ItemCode").attr('readonly', 'readonly');

    }
    $(".SupplierBarcode").attr('readonly', 'readonly'); 
    $(".CurrencyCode").attr('readonly', 'readonly');
    $(".OrderType").attr('readonly', 'readonly');
    $(document).on("click", "#SaveFirstFreeDetails", function (e) {
            var textBox1 = null;
            var textBox2 = null;
            $("#FirstFreeDetail tr").each(function () {
                textBox1 = $(this).find(".Prefix").val(); 
                textBox2 = $(this).find(".SequenceNumber").val(); 
                if (textBox1 == textBox2) {
                    alert("Prefix and Sequence Number should not be same");
                    e.preventtDefault();
                    return false;
                }
           
                        
            });
    });


    $(document).on("keydown", ".txtNumeric", function (e) {
        if (e.shiftKey || e.ctrlKey || e.altKey) {
            e.preventDefault();
        } else {
            var key = e.keyCode;
            if (!((key == 190 )||( key == 110) || (key == 9) || (key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                e.preventDefault();
            }
        }
    });



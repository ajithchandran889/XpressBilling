var itemMasterArray = [];""
var itemMasterDetails = {};
//var itemMasterArraySQ = [];
//var itemMasterDetailsEditSQ = {};

var itemMasterArrayStockEntry = [];
var itemMasterDetailsStockEntry = {};
var itemMasterArrayStockEntryByName = [];
var itemMasterDetailsStockEntryByName = {};
var SEItemRowDetails = [];
var itemMasterArrayManualInvoice = [];
var itemMasterDetailsManualInvoice = {};
var itemMasterArrayManualInvoiceByName = [];
var itemMasterDetailsManualInvoiceByName = {};
var itemMasterArrayInvoice = [];
var itemMasterDetailsInvoice = {};
var itemMasterArrayInvoiceByName = [];
var itemMasterDetailsInvoiceByName = {};
var itemMasterQuantity = {};
var itemMasterArraySQ = [];
var itemMasterDetailsSQ = {};
var itemMasterArraySQByName = [];
var itemMasterDetailsSQByName = {};
var itemMasterQuantitySQ = {};
var SQItemRowDetails = [];
var itemTaxCodes = [];
var itemTaxDetails = {};
var MIItemRowDetails = [];
var IItemRowDetails = [];
var itemMasterQuantityI = {};
$(function () {
    $("#inputDate").datepicker(); 
    $("#FormationDate").datepicker();
    $("#Date").datepicker();
    $("#DOJ").datepicker(); 
    $("#Validity").datepicker();
    $("#PeriodFrom").datepicker();
    $("#PeriodTo").datepicker();
});


$(document).ready(function () {
    
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
    else if ($("#PageStatus").val() == "create") {
        $(".StockUnit").attr('readonly', 'readonly');
        $(".StockAmount").attr('readonly', 'readonly');
    }
    if (($("#InvoiceId").val() != "" || $("#InvoiceId").val() != "0") && $("#PageStatus").val() != "create") {
        itemMasterArrayManualInvoice = []; 
        itemMasterDetailsManualInvoice = {};
        var i = 0;
        $("tr", $("#ManualInvoiceDetail")).each(function () {
            
            var val = $("input[id*='MIItem']", $(this)).val();
            var qnty = parseInt($("input[id*='MIQuantity']", $(this)).val());
            var rate = parseInt($("input[id*='MIItemRate']", $(this)).val());
            var discountPer = parseInt($("input[id*='MIDiscPer']", $(this)).val());
            var taxPer = parseInt($("input[id*='MITaxPer']", $(this)).val());
            if (typeof (val) !== "undefined") {
                var rowTotalRate = qnty * rate;
                var discountAmt = rowTotalRate * discountPer;
                var taxAmount = (rowTotalRate - discountAmt) * taxPer;
                var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                var orderAmount = netAmount;
                MIItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                i++;
            }

        });
        $(".MITaxAmt").attr('readonly', 'readonly');
        $(".MINetAmt").attr('readonly', 'readonly');
        $(".MIUnit").attr('readonly', 'readonly');
        $(".MIItem").attr('readonly', 'readonly');
        $(".MIItemName").attr('readonly', 'readonly');
    }
    else if ($("#PageStatus").val() == "create")
    {
        $(".MITaxAmt").attr('readonly', 'readonly');
        $(".MINetAmt").attr('readonly', 'readonly');
        $(".MIUnit").attr('readonly', 'readonly');
    }
    if (($("#SalesInvoiceId").val() != "" || $("#SalesInvoiceId").val() != "0") && $("#PageStatus").val() != "create") {
        itemMasterArrayInvoice = [];
        itemMasterDetailsInvoice = {};
        var i = 0;
        $("tr", $("#InvoiceDetail")).each(function () {

            var val = $("input[id*='IItem']", $(this)).val();
            var qnty = parseInt($("input[id*='IQuantity']", $(this)).val());
            var rate = parseInt($("input[id*='IItemRate']", $(this)).val());
            var discountPer = parseInt($("input[id*='IDiscPer']", $(this)).val());
            var taxPer = parseInt($("input[id*='ITaxPer']", $(this)).val());
            if (typeof (val) !== "undefined") {
                var rowTotalRate = qnty * rate;
                var discountAmt = rowTotalRate * discountPer;
                var taxAmount = (rowTotalRate - discountAmt) * taxPer;
                var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                var orderAmount = netAmount;
                IItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                i++;
            }

        }); 
        if ($("#Status").val() == "1")
        {
            $(".ITaxAmt").attr('readonly', 'readonly');
            $(".INetAmt").attr('readonly', 'readonly');
            $(".IUnit").attr('readonly', 'readonly');
            $(".IItem").attr('readonly', 'readonly');
            $(".IItemName").attr('readonly', 'readonly');
        }
        else if ($("#Status").val() == "2")
        {
            $(".ITaxAmt").attr('readonly', 'readonly');
            $(".INetAmt").attr('readonly', 'readonly');
            $(".IUnit").attr('readonly', 'readonly');
            $(".IItem").attr('readonly', 'readonly');
            $(".IItemName").attr('readonly', 'readonly');
            $(".IItemRate").attr('readonly', 'readonly');
            $(".IQuantity").attr('readonly', 'readonly');
            $(".IDiscPer").attr('readonly', 'readonly');
            $(".IDiscAmt").attr('readonly', 'readonly');
            $(".ITaxPer").attr('readonly', 'readonly');
        }
       
    }
    else if ($("#PageStatus").val() == "create") {
        $(".ITaxAmt").attr('readonly', 'readonly');
        $(".INetAmt").attr('readonly', 'readonly');
        $(".IUnit").attr('readonly', 'readonly');
    }
    if (($("#SalesQuotationId").val() != "" || $("#SalesQuotationId").val() != "0") && $("#PageStatus").val() != "create") {
        itemMasterArraySQ = [];
        itemMasterDetailsSQ = {};
        var i = 0;
        $("tr", $("#SalesQuotationDetail")).each(function () {

            var val = $("input[id*='SQItem']", $(this)).val();
            var qnty = parseInt($("input[id*='SQQuantity']", $(this)).val());
            var rate = parseInt($("input[id*='SQRate']", $(this)).val());
            var discountPer = parseInt($("input[id*='SQDiscPer']", $(this)).val());
            var taxPer = parseInt($("input[id*='SQTaxPer']", $(this)).val());
            if (typeof (val) !== "undefined") {
                var rowTotalRate = qnty * rate;
                var discountAmt = rowTotalRate * discountPer;
                var taxAmount = (rowTotalRate - discountAmt) * taxPer;
                var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                var orderAmount = netAmount;
                SQItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                i++;
            }

        });
        $(".SQTaxAmt").attr('readonly', 'readonly');
        $(".SQNetAmt").attr('readonly', 'readonly');
        $(".SQUnit").attr('readonly', 'readonly');
        $(".SQItem").attr('readonly', 'readonly');
        $(".SQName").attr('readonly', 'readonly');
    }
    else if ($("#PageStatus").val() == "create") {
        $(".SQTaxAmt").attr('readonly', 'readonly');
        $(".SQNetAmt").attr('readonly', 'readonly');
        $(".SQUnit").attr('readonly', 'readonly');
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

        if ($("#AdjustmentType").val() == "2") {
            $("#Document").val($("#AdditionSequenceNo").val());
        }
        else if ($("#AdjustmentType").val() == "3") {
            $("#Document").val($("#DeductionSequenceNo").val());
        }
        else if ($("#AdjustmentType").val() == "1") {
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


    function SetSelectedRowStockEntry(lnk, selectedItem) {
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        var itemArr = itemMasterDetailsStockEntry[selectedItem];
        row.cells[2].getElementsByTagName("input")[0].value = itemArr[0];
        row.cells[3].getElementsByTagName("input")[0].value = itemArr[3];
        row.cells[5].getElementsByTagName("input")[0].value = itemArr[4];

        return false;
    }

    function SetSelectedRowStockEntryByName(lnk, selectedItem) {
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        var itemArr = itemMasterDetailsStockEntryByName[selectedItem];
        row.cells[1].getElementsByTagName("input")[0].value = itemArr[0];
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

    $(document).on("keydown", "#SalesMan", function (e) {
        var contactArray = [];
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SalesQuotationEdit.aspx/GetContactCodes",
            dataType: "json",
            success: function (data) {
                
                $.each(data.d, function (i, j) {
                    contactArray.push(j.code);
                });
                $("#SalesMan").autocomplete({
                    source: contactArray
                });
            },
            error: function (result) {
                alert("Error");
            }
        });
        
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

    var optionsStockEntryByName = {
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PriceBookEdit.aspx/GetItemMasters",
                dataType: "json",
                success: function (data) {
                    itemMasterArrayStockEntryByName = [];
                    itemMasterDetailsStockEntryByName = {};
                    itemMasterQuantity = {};
                    $.each(data.d, function (i, j) {
                        itemMasterArrayStockEntryByName.push(j.name);
                        itemMasterDetailsStockEntryByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode];
                      });
                    response(itemMasterArrayStockEntryByName);
                },
                error: function (result) {
                    alert("Error");
                }
            });
        },
        select: function (event, ui) {
            SetSelectedRowStockEntryByName(this, ui.item.label);

        },
    };

    $(document).on("keydown", ".Item", function (e) {
      $(this).autocomplete(options);
    });

    $(document).on("keydown", ".StockItem", function (e) {
        $(this).autocomplete(optionsStockEntry);
    });
    $(document).on("keydown", ".StockName", function (e) {
        $(this).autocomplete(optionsStockEntryByName);
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
    $(document).on("focusout", "#SERate", function (e) {
        CalculateSEAmount(this);
    });

    if ($(".ItemCode").val()!="")
    {
        $(".ItemCode").attr('readonly', 'readonly');

    }
    $(".SupplierBarcode").attr('readonly', 'readonly'); 
    $(".CurrencyCode").attr('readonly', 'readonly');
    $(".OrderType").attr('readonly', 'readonly');
    $(document).on("click", "#SaveFirstFreeDetails", function (e) {
            var prefix = [];
            var sequence = [];
            $("#FirstFreeDetail tr").each(function () {
                var val1 = $(this).find(".Prefix").val();
                var val2 = $(this).find(".SequenceNumber").val();
                if (typeof val1 !== "undefined") {
                    prefix.push(val1);
                }
                if (typeof val2 !== "undefined") {
                    sequence.push(val2);
                }       
            });
            var lastValue = "";
            var i = 0;
            $.each(prefix, function( index, value ) {
                if(lastValue!="")
                {
                    if(value==lastValue)
                    {
                        if(sequence[i-1]==sequence[i])
                        {
                            alert("Same prefix and sequence number not allowed for more than one time");
                            return false;
                        }
                    }
                }
                lastValue = value;
                i++;
            });
            
            return true;
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

    function SetSelectedRowManualInvoice(lnk, selectedItem) {
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        var itemArr = itemMasterDetailsManualInvoice[selectedItem];
        row.cells[2].getElementsByTagName("input")[0].value = itemArr[0];
        row.cells[3].getElementsByTagName("input")[0].value = itemArr[3];
        row.cells[5].getElementsByTagName("input")[0].value = itemArr[4];
        row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
        return false;
    }
    function SetSelectedRowManualInvoiceByName(lnk, selectedItem) {
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        var itemArr = itemMasterDetailsManualInvoiceByName[selectedItem];
        row.cells[1].getElementsByTagName("input")[0].value = itemArr[0];
        row.cells[3].getElementsByTagName("input")[0].value = itemArr[3];
        row.cells[5].getElementsByTagName("input")[0].value = itemArr[4];
        row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
        return false;
    }
    function setSelectedTaxCode(lnk, selectedItem) {
        
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        var taxPer = itemTaxDetails[selectedItem];
        $(lnk).val('');
        row.cells[8].getElementsByTagName("input")[0].value = taxPer;
        row.cells[8].getElementsByTagName("input")[1].value = selectedItem;
        return false;
    }
    var optionsManualInvoice = {
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PriceBookEdit.aspx/GetItemMasters",
                dataType: "json",
                success: function (data) {
                    itemMasterArrayManualInvoice = [];
                    itemMasterDetailsManualInvoice = {};
                    //itemMasterQuantityMI = {};
                    $.each(data.d, function (i, j) {
                        itemMasterArrayManualInvoice.push(j.code);
                        itemMasterDetailsManualInvoice[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty];
                        //itemMasterQuantityMI[j.code] = [j.Qnty];
                    });
                    response(itemMasterArrayManualInvoice);
                },
                error: function (result) {
                    alert("Error");
                }
            });
        },
        select: function (event, ui) {
            SetSelectedRowManualInvoice(this, ui.item.label);

        },
    };
    var optionsManualInvoiceByName = {
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PriceBookEdit.aspx/GetItemMasters",
                dataType: "json",
                success: function (data) {
                    itemMasterArrayManualInvoiceByName = [];
                    itemMasterDetailsManualInvoiceByName = {};
                    //itemMasterQuantityMI = {};
                    $.each(data.d, function (i, j) {
                        itemMasterArrayManualInvoiceByName.push(j.name);
                        itemMasterDetailsManualInvoiceByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty];
                        //itemMasterQuantityMI[j.code] = [j.Qnty];
                    });
                    response(itemMasterArrayManualInvoiceByName);
                },
                error: function (result) {
                    alert("Error");
                }
            });
        },
        select: function (event, ui) {
            SetSelectedRowManualInvoiceByName(this, ui.item.label);

        },
    };
    var optionsTax = {
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "TaxMst.aspx/GetAllTaxDetails",
                dataType: "json",
                success: function (data) {
                    //console.log(data.d);
                    itemTaxCodes = [];
                    itemTaxDetails = {};
                    $.each(data.d, function (i, j) {
                        itemTaxCodes.push(j.code);
                        itemTaxDetails[j.code] = [j.Per];
                    });
                    //console.log(itemTaxDetails);
                    response(itemTaxCodes);
                },
                error: function (result) {
                    alert("Error");
                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            setSelectedTaxCode(this, ui.item.label);

        },
        change: function (event, ui) {
            if (ui.item === null || !ui.item)
                $(this).val(''); /* clear the value */
        }
    };
    $(document).on("keydown", ".MIItem", function (e) {
        $(this).autocomplete(optionsManualInvoice);
    });
    $(document).on("keydown", ".MIItemName", function (e) {
        $(this).autocomplete(optionsManualInvoiceByName);
    });
    $(document).on("focusout", "#MIQuantity", function (e) {
        
        CalculateMIAmount(this);
    });
    $(document).on("focusout", "#MIItemRate", function (e) {
        CalculateMIAmount(this);
    });
    $(document).on("focusout", "#MIDiscPer", function (e) {
        CalculateMIAmount(this);
    });
    $(document).on("focusout", "#MITaxPer", function (e) {
        CalculateMIAmount(this);
    });
    $(document).on("focusout", "#MIDiscAmt", function (e) {
        CalculateMIAmount(this);
    });
    
    $("#MITotalAmount").attr('readonly', 'readonly');
    $("#MITotalDiscountAmt").attr('readonly', 'readonly');
    $("#MITotalTaxAmt").attr('readonly', 'readonly');
    $("#MITotalOrderAmt").attr('readonly', 'readonly');
    function CalculateMIAmount(txtBox) {
        var row = txtBox.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        if (row.cells[1].getElementsByTagName("input")[0].value != "") {
            var rate = row.cells[3].getElementsByTagName("input")[0].value;
            var qnty = row.cells[4].getElementsByTagName("input")[0].value;
            var discountPer = row.cells[6].getElementsByTagName("input")[0].value;
            var discountAmt = row.cells[7].getElementsByTagName("input")[0].value;
            var taxPer = row.cells[8].getElementsByTagName("input")[0].value;
            if (qnty != "" && (discountPer != "" || discountAmt != "") && taxPer != "") {
                var rowTotalRate = qnty * rate;
                if ($(txtBox).attr("id") == "MIDiscAmt")
                {
                    discountPer = Math.round(discountAmt / rowTotalRate);
                }
                else
                {
                    discountAmt = rowTotalRate * discountPer;
                }
                var taxAmount = (rowTotalRate - discountAmt) * taxPer;
                var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                //var orderAmount = ((qnty * rowTotalRate) - discountAmt) * taxAmount;
                var orderAmount = netAmount;
                if ($(txtBox).attr("id") == "MIDiscAmt")
                {
                    row.cells[6].getElementsByTagName("input")[0].value = discountPer;
                }
                else {
                    row.cells[7].getElementsByTagName("input")[0].value = discountAmt;
                }
                row.cells[9].getElementsByTagName("input")[0].value = taxAmount;
                
                if (!MIItemRowDetails[rowIndex] && row.cells[10].getElementsByTagName("input")[0].value == "") {
                    row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                    MIItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                    if ($("#MITotalAmount").val() == "") {
                        $("#MITotalAmount").val(netAmount);
                    }
                    else {
                        $("#MITotalAmount").val(parseInt($("#MITotalAmount").val()) + parseInt(netAmount));
                    } 
                    if ($("#Amount").val() == "") {
                        $("#Amount").val(netAmount);
                    }
                    else {
                        $("#Amount").val(parseInt($("#Amount").val()) + parseInt(netAmount));
                    }
                    if ($("#MITotalDiscountAmt").val() == "") {
                        $("#MITotalDiscountAmt").val(discountAmt);
                    }
                    else {
                        $("#MITotalDiscountAmt").val(parseInt($("#MITotalDiscountAmt").val()) + parseInt(discountAmt));
                    }
                    if ($("#MITotalTaxAmt").val() == "") {
                        $("#MITotalTaxAmt").val(taxAmount);
                    }
                    else {
                        $("#MITotalTaxAmt").val(parseInt($("#MITotalTaxAmt").val()) + parseInt(taxAmount));
                    }
                    if ($("#MITotalOrderAmt").val() == "") {
                        $("#MITotalOrderAmt").val(orderAmount);
                    }
                    else {
                        $("#MITotalOrderAmt").val(parseInt($("#MITotalOrderAmt").val()) + orderAmount);
                    }
                }
                else {
                    var itemArray = MIItemRowDetails[rowIndex];
                    var totalAmt = parseInt($("#Amount").val());
                    var totalDiscount = parseInt($("#MITotalDiscountAmt").val());
                    var totalTax = parseInt($("#MITotalTaxAmt").val());
                    var totalOder = parseInt($("#MITotalOrderAmt").val());
                    var oldAmount = parseInt(itemArray[6]);
                    var oldDiscount = parseInt(itemArray[3]);
                    var oldTax = parseInt(itemArray[5]);
                    var oldOder = parseInt(itemArray[7]);
                    totalAmt -= oldAmount;
                    totalDiscount -= oldDiscount;
                    totalTax -= oldTax;
                    totalOder -= oldOder;
                    totalAmt += parseInt(netAmount);
                    totalDiscount += parseInt(discountAmt);
                    totalTax += parseInt(taxAmount);
                    totalOder += parseInt(orderAmount);
                    row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                    $("#Amount").val(totalAmt);
                    $("#MITotalAmount").val(totalAmt);
                    $("#MITotalDiscountAmt").val(totalDiscount);
                    $("#MITotalTaxAmt").val(totalTax);
                    $("#MITotalOrderAmt").val(totalOder);
                    MIItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                }


            }
        }

    }

    function iSQuantityAvailableI(txtBox) {
        var row = txtBox.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        if (row.cells[1].getElementsByTagName("input")[0].value != ""
            && row.cells[4].getElementsByTagName("input")[0].value!="") {
            var itemCode = row.cells[1].getElementsByTagName("input")[0].value;
            var itemArr = itemMasterQuantityI[itemCode];
            if (typeof (itemArr) === "undefined") {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "PriceBookEdit.aspx/GetItemMasters",
                    dataType: "json",
                    success: function (data) {
                        itemMasterQuantityI = {};
                        $.each(data.d, function (i, j) {
                            itemMasterQuantityI[j.code] = [j.Qnty];
                        });
                        itemArr = itemMasterQuantityI[itemCode];
                        if (parseInt(itemArr[0]) >= parseInt(row.cells[4].getElementsByTagName("input")[0].value)) {
                            CalculateIAmount(txtBox);
                        }
                        else {
                            row.cells[4].getElementsByTagName("input")[0].value = itemArr[0];
                            alert("Sorry,Avilable Items:" + itemArr[0]);
                        }
                    },
                    error: function (result) {
                        alert("Error");
                    }
                });
                
            }
            else
            {
                if (parseInt(itemArr[0]) >= parseInt(row.cells[4].getElementsByTagName("input")[0].value)) {
                    CalculateIAmount(txtBox);
                }
                else {
                    row.cells[4].getElementsByTagName("input")[0].value = itemArr[0];
                    alert("Sorry,Avilable Items:" + itemArr[0]);
                }
            }
            
        }
    }
    function iSQuantityAvailableSQ(txtBox) {
        var row = txtBox.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        if (row.cells[1].getElementsByTagName("input")[0].value != ""
            && row.cells[4].getElementsByTagName("input")[0].value != "") {
            var itemCode = row.cells[1].getElementsByTagName("input")[0].value;
            var itemArr = itemMasterQuantitySQ[itemCode];
            if (typeof (itemArr) === "undefined") {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "PriceBookEdit.aspx/GetItemMasters",
                    dataType: "json",
                    success: function (data) {
                        itemMasterQuantitySQ = {};
                        $.each(data.d, function (i, j) {
                            itemMasterQuantitySQ[j.code] = [j.Qnty];
                        });
                        itemArr = itemMasterQuantitySQ[itemCode];
                        if (parseInt(itemArr[0]) >= parseInt(row.cells[4].getElementsByTagName("input")[0].value)) {
                            CalculateSQAmount(txtBox);
                        }
                        else {
                            row.cells[4].getElementsByTagName("input")[0].value = itemArr[0];
                            alert("Sorry,Avilable Items:" + itemArr[0]);
                        }
                    },
                    error: function (result) {
                        alert("Error");
                    }
                });

            }
            else {
                if (parseInt(itemArr[0]) >= parseInt(row.cells[4].getElementsByTagName("input")[0].value)) {
                    CalculateSQAmount(txtBox);
                }
                else {
                    row.cells[4].getElementsByTagName("input")[0].value = itemArr[0];
                    alert("Sorry,Avilable Items:" + itemArr[0]);
                }
            }

        }
    }

    function SetSelectedRowInvoice(lnk, selectedItem) {
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        var itemArr = itemMasterDetailsInvoice[selectedItem];
        row.cells[2].getElementsByTagName("input")[0].value = itemArr[0];
        row.cells[3].getElementsByTagName("input")[0].value = itemArr[3];
        row.cells[5].getElementsByTagName("input")[0].value = itemArr[4];
        row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
        row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
        return false;
    }
    function SetSelectedRowInvoiceByName(lnk, selectedItem) {
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        var itemArr = itemMasterDetailsInvoiceByName[selectedItem];
        row.cells[1].getElementsByTagName("input")[0].value = itemArr[0];
        row.cells[3].getElementsByTagName("input")[0].value = itemArr[3];
        row.cells[5].getElementsByTagName("input")[0].value = itemArr[4];
        row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
        row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
        return false;
    }
    var optionsInvoice = {
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PriceBookEdit.aspx/GetItemMasters",
                dataType: "json",
                success: function (data) {
                    itemMasterArrayInvoice = [];
                    itemMasterDetailsInvoice = {};
                    itemMasterQuantityI = {};
                    $.each(data.d, function (i, j) {
                        itemMasterArrayInvoice.push(j.code);
                        itemMasterDetailsInvoice[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty];
                        itemMasterQuantityI[j.code] = [j.Qnty];
                    });
                    response(itemMasterArrayInvoice);
                },
                error: function (result) {
                    alert("Error");
                }
            });
        },
        select: function (event, ui) {
            SetSelectedRowInvoice(this, ui.item.label);

        },
    };
    var optionsInvoiceByName = {
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PriceBookEdit.aspx/GetItemMasters",
                dataType: "json",
                success: function (data) {
                    itemMasterArrayInvoiceByName = [];
                    itemMasterDetailsInvoiceByName = {};
                    itemMasterQuantityI = {};
                    $.each(data.d, function (i, j) {
                        itemMasterArrayInvoiceByName.push(j.name);
                        itemMasterDetailsInvoiceByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty];
                        itemMasterQuantityI[j.code] = [j.Qnty];
                    });
                    response(itemMasterArrayInvoiceByName);
                },
                error: function (result) {
                    alert("Error");
                }
            });
        },
        select: function (event, ui) {
            SetSelectedRowInvoiceByName(this, ui.item.label);

        },
    };

    $(document).on("keydown", ".IItem", function (e) {
        $(this).autocomplete(optionsInvoice);
    });
    $(document).on("keydown", ".IItemName", function (e) {
        $(this).autocomplete(optionsInvoiceByName);
    });
    $(document).on("keydown", ".ITaxPer", function (e) {
        $(this).autocomplete(optionsTax);
    });
    $(document).on("focusout", "#IQuantity", function (e) {
        iSQuantityAvailableI(this);
       
    });
    $(document).on("focusout", "#IItemRate", function (e) {
        CalculateIAmount(this);
    });
    $(document).on("focusout", "#IDiscPer", function (e) {
        CalculateIAmount(this);
    });
    $(document).on("focusout", "#ITaxPer", function (e) {
        CalculateIAmount(this);
    });
    $(document).on("focusout", "#IDiscAmt", function (e) {
        CalculateIAmount(this);
    });
    //$(".MIItemRate").attr('readonly', 'readonly');
    $("#ITotalAmount").attr('readonly', 'readonly');
    $("#ITotalDiscountAmt").attr('readonly', 'readonly');
    $("#ITotalTaxAmt").attr('readonly', 'readonly');
    $("#ITotalOrderAmt").attr('readonly', 'readonly');
    function CalculateIAmount(txtBox) {
        var row = txtBox.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        if (row.cells[1].getElementsByTagName("input")[0].value != "") {
            var rate = row.cells[3].getElementsByTagName("input")[0].value;
            var qnty = row.cells[4].getElementsByTagName("input")[0].value;
            var discountPer = row.cells[6].getElementsByTagName("input")[0].value; 
            var discountAmt = row.cells[7].getElementsByTagName("input")[0].value; 
            var taxPer = row.cells[8].getElementsByTagName("input")[0].value;
            if (qnty != "" && (discountPer != "" || discountAmt != "") && taxPer != "") {
                var rowTotalRate = qnty * rate;
                if ($(txtBox).attr("id") == "IDiscAmt")
                {
                    discountPer = Math.round(discountAmt / rowTotalRate);
                }
                else {
                    discountAmt = rowTotalRate * discountPer;
                }
                var taxAmount = (rowTotalRate - discountAmt) * taxPer;
                var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                //var orderAmount = ((qnty * rowTotalRate) - discountAmt) * taxAmount;
                var orderAmount = netAmount;
                if ($(txtBox).attr("id") == "IDiscAmt")
                {
                    row.cells[6].getElementsByTagName("input")[0].value = discountPer;
                    
                }
                else
                {
                    row.cells[7].getElementsByTagName("input")[0].value = discountAmt;
                }
                row.cells[9].getElementsByTagName("input")[0].value = taxAmount;
                if (!IItemRowDetails[rowIndex] && row.cells[10].getElementsByTagName("input")[0].value == "") {
                    row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                    IItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                    if ($("#ITotalAmount").val() == "") {
                        $("#ITotalAmount").val(netAmount);
                    }
                    else {
                        $("#ITotalAmount").val(parseInt($("#ITotalAmount").val()) + parseInt(netAmount));
                    }
                    if ($("#Amount").val() == "") {
                        $("#Amount").val(netAmount);
                    }
                    else {
                        $("#Amount").val(parseInt($("#Amount").val()) + parseInt(netAmount));
                    }
                    if ($("#ITotalDiscountAmt").val() == "") {
                        $("#ITotalDiscountAmt").val(discountAmt);
                    }
                    else {
                        $("#ITotalDiscountAmt").val(parseInt($("#ITotalDiscountAmt").val()) + parseInt(discountAmt));
                    }
                    if ($("#ITotalTaxAmt").val() == "") {
                        $("#ITotalTaxAmt").val(taxAmount);
                    }
                    else {
                        $("#ITotalTaxAmt").val(parseInt($("#ITotalTaxAmt").val()) + parseInt(taxAmount));
                    }
                    if ($("#ITotalOrderAmt").val() == "") {
                        $("#ITotalOrderAmt").val(orderAmount);
                    }
                    else {
                        $("#ITotalOrderAmt").val(parseInt($("#ITotalOrderAmt").val()) + orderAmount);
                    }
                }
                else {
                    var itemArray = IItemRowDetails[rowIndex];
                    var totalAmt = parseInt($("#Amount").val());
                    var totalDiscount = parseInt($("#ITotalDiscountAmt").val());
                    var totalTax = parseInt($("#ITotalTaxAmt").val());
                    var totalOder = parseInt($("#ITotalOrderAmt").val());
                    var oldAmount = parseInt(itemArray[6]);
                    var oldDiscount = parseInt(itemArray[3]);
                    var oldTax = parseInt(itemArray[5]);
                    var oldOder = parseInt(itemArray[7]);
                    totalAmt -= oldAmount;
                    totalDiscount -= oldDiscount;
                    totalTax -= oldTax;
                    totalOder -= oldOder;
                    totalAmt += parseInt(netAmount);
                    totalDiscount += parseInt(discountAmt);
                    totalTax += parseInt(taxAmount);
                    totalOder += parseInt(orderAmount);
                    row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                    $("#Amount").val(totalAmt);
                    $("#ITotalAmount").val(totalAmt);
                    $("#ITotalDiscountAmt").val(totalDiscount);
                    $("#ITotalTaxAmt").val(totalTax);
                    $("#ITotalOrderAmt").val(totalOder);
                    IItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                }


            }
        }

    }

    function SetSelectedRowSQ(lnk, selectedItem) {
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        var itemArr = itemMasterDetailsSQ[selectedItem];
        row.cells[2].getElementsByTagName("input")[0].value = itemArr[0];
        row.cells[3].getElementsByTagName("input")[0].value = itemArr[3];
        row.cells[5].getElementsByTagName("input")[0].value = itemArr[4];
        row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
        row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
        return false;
    }
    function SetSelectedRowSQByName(lnk, selectedItem) {
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        var itemArr = itemMasterDetailsSQByName[selectedItem];
        row.cells[1].getElementsByTagName("input")[0].value = itemArr[0];
        row.cells[3].getElementsByTagName("input")[0].value = itemArr[3];
        row.cells[5].getElementsByTagName("input")[0].value = itemArr[4];
        row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
        row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
        return false;
    }
    var optionsSQ = {
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PriceBookEdit.aspx/GetItemMasters",
                dataType: "json",
                success: function (data) {
                    itemMasterArraySQ = [];
                    itemMasterDetailsSQ = {};
                    itemMasterQuantitySQ = {};
                    $.each(data.d, function (i, j) {
                        itemMasterArraySQ.push(j.code);
                        itemMasterDetailsSQ[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty];
                        itemMasterQuantitySQ[j.code] = [j.Qnty];
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
    var optionsSQByName = {
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PriceBookEdit.aspx/GetItemMasters",
                dataType: "json",
                success: function (data) {
                    itemMasterArraySQByName = [];
                    itemMasterDetailsSQByName = {};
                    itemMasterQuantitySQ = {};
                    $.each(data.d, function (i, j) {
                        itemMasterArraySQByName.push(j.name);
                        itemMasterDetailsSQByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty];
                        itemMasterQuantitySQ[j.code] = [j.Qnty];
                    });
                    response(itemMasterArraySQByName);
                },
                error: function (result) {
                    alert("Error");
                }
            });
        },
        select: function (event, ui) {
            SetSelectedRowSQByName(this, ui.item.label);

        },
    };

    $(document).on("keydown", ".SQItem", function (e) {
        $(this).autocomplete(optionsSQ);
    });
    $(document).on("keydown", ".SQName", function (e) {
        $(this).autocomplete(optionsSQByName);
    });
    $(document).on("keydown", ".SQTaxPer", function (e) {
        $(this).autocomplete(optionsTax);
    });
    $(document).on("focusout", "#SQQuantity", function (e) {
        iSQuantityAvailableSQ(this);
   });
    $(document).on("focusout", "#SQRate", function (e) {
        CalculateSQAmount(this);
    });
    $(document).on("focusout", "#SQDiscPer", function (e) {
        CalculateSQAmount(this);
    });
    $(document).on("focusout", "#SQTaxPer", function (e) {
        CalculateSQAmount(this);
    });
    $(document).on("focusout", "#SQDiscAmt", function (e) {
        CalculateSQAmount(this);
    });
    //$(".MIItemRate").attr('readonly', 'readonly');
    $("#TotalAmount").attr('readonly', 'readonly');
    $("#TotalDiscountAmt").attr('readonly', 'readonly');
    $("#TotalTaxAmt").attr('readonly', 'readonly');
    $("#TotalOrderAmt").attr('readonly', 'readonly');
    function CalculateSQAmount(txtBox) {
        var row = txtBox.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        if (row.cells[1].getElementsByTagName("input")[0].value != "") {
            var rate = row.cells[3].getElementsByTagName("input")[0].value;
            var qnty = row.cells[4].getElementsByTagName("input")[0].value;
            var discountPer = row.cells[6].getElementsByTagName("input")[0].value;
            var discountAmt = row.cells[7].getElementsByTagName("input")[0].value;
            var taxPer = row.cells[8].getElementsByTagName("input")[0].value;
            if (qnty != "" && (discountPer != "" || discountAmt != "") && taxPer != "") {
                var rowTotalRate = qnty * rate;
                if ($(txtBox).attr("id") == "SQDiscAmt")
                {
                    discountPer = Math.round(discountAmt / rowTotalRate);
                }
                else {
                    discountAmt = rowTotalRate * discountPer;
                }
                var taxAmount = (rowTotalRate - discountAmt) * taxPer;
                var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                //var orderAmount = ((qnty * rowTotalRate) - discountAmt) * taxAmount;
                var orderAmount = netAmount;
                if ($(txtBox).attr("id") == "SQDiscAmt")
                {
                    row.cells[6].getElementsByTagName("input")[0].value = discountPer;
                }
                else {
                    row.cells[7].getElementsByTagName("input")[0].value = discountAmt;
                    
                }
                row.cells[9].getElementsByTagName("input")[0].value = taxAmount;
                if (!SQItemRowDetails[rowIndex] && row.cells[10].getElementsByTagName("input")[0].value == "") {
                    
                    row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                    SQItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                    if ($("#TotalAmount").val() == "") {
                        $("#TotalAmount").val(netAmount);
                    }
                    else {
                        $("#TotalAmount").val(parseInt($("#TotalAmount").val()) + parseInt(netAmount));
                    }
                    if ($("#TotalDiscountAmt").val() == "") {
                        $("#TotalDiscountAmt").val(discountAmt);
                    }
                    else {
                        $("#TotalDiscountAmt").val(parseInt($("#TotalDiscountAmt").val()) + parseInt(discountAmt));
                    }
                    if ($("#TotalTaxAmt").val() == "") {
                        $("#TotalTaxAmt").val(taxAmount);
                    }
                    else {
                        $("#TotalTaxAmt").val(parseInt($("#TotalTaxAmt").val()) + parseInt(taxAmount));
                    }
                    if ($("#TotalOrderAmt").val() == "") {
                        $("#TotalOrderAmt").val(orderAmount);
                    }
                    else {
                        $("#TotalOrderAmt").val(parseInt($("#TotalOrderAmt").val()) + orderAmount);
                    }
                }
                else {
                    var itemArray = SQItemRowDetails[rowIndex];
                    var totalAmt = parseInt($("#TotalAmount").val());
                    var totalDiscount = parseInt($("#TotalDiscountAmt").val());
                    var totalTax = parseInt($("#TotalTaxAmt").val());
                    var totalOder = parseInt($("#TotalOrderAmt").val());
                    var oldAmount = parseInt(itemArray[6]);
                    var oldDiscount = parseInt(itemArray[3]);
                    var oldTax = parseInt(itemArray[5]);
                    var oldOder = parseInt(itemArray[7]);
                    totalAmt -= oldAmount;
                    totalDiscount -= oldDiscount;
                    totalTax -= oldTax;
                    totalOder -= oldOder;
                    totalAmt += parseInt(netAmount);
                    totalDiscount += parseInt(discountAmt);
                    totalTax += parseInt(taxAmount);
                    totalOder += parseInt(orderAmount);
                    row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                    $("#TotalAmount").val(totalAmt);
                    $("#TotalDiscountAmt").val(totalDiscount);
                    $("#TotalTaxAmt").val(totalTax);
                    $("#TotalOrderAmt").val(totalOder);
                    SQItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                }


            }
        }

    }
﻿var itemMasterSelectedItem = [];
var itemMasterArray = [];
//var itemMasterArrayOriginal = []; ""
var itemMasterDetails = {};
var itemMasterArrayByName = []; ""
//var itemMasterArrayByNameOriginal = []; ""
var itemMasterDetailsByName = {};
//var itemMasterArraySQ = [];
//var itemMasterDetailsEditSQ = {};
var customerCodes = [];
var customerCodesWithDetails = {};
var contactArray = [];
var contactArrayWithName = {};
var locationArray = [];
var locationArrayWithName = {};
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
var itemTaxCodevalues = [];
var itemTaxDetails = {};
var MIItemRowDetails = [];
var IItemRowDetails = [];
var itemMasterQuantityI = {};
var itemMasterQuantityPO = {};
var itemMasterArrayPO = [];
var itemMasterDetailsPO = {};
var itemMasterArrayPOByName = [];
var itemMasterDetailsPOByName = {};
var POItemRowDetails = [];
var itemMasterArraySalesReturn = [];
var itemMasterDetailsSalesReturn = {};
var itemMasterArraySalesReturnByName = [];
var itemMasterDetailsSalesReturnByName = {};
var SRItemRowDetails = [];
var firstFreeSQ = [];
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
    var obj = {};
    obj.companyCode = $.trim($("#CompanyCode").val());
    if ($("#SalesMan").length > 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SQEdit.aspx/GetEmployeeCodes",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                contactArray = [];
                $.each(data.d, function (i, j) {
                    contactArray.push(j.name);
                    contactArrayWithName[j.name] = [j.code];
                });
            },
            error: function (result) {
               // alert("Error");
            }
        });
    }
    if ($("#Location").length > 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SQEdit.aspx/GetLocationCodes",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                locationArray = [];
                $.each(data.d, function (i, j) {
                    locationArray.push(j.name);
                    locationArrayWithName[j.name] = [j.code];
                });
            },
            error: function (result) {
                //alert("Error");
            }
        });
    }
    if ($("#StokeEntryMstId").length > 0) {
        if ($("#OrderNo").val() == "") {
            alert("Please create first free number for stock adjustment");
            $("#SaveBtn").attr("disabled", true);
        }
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PriceBookEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                itemMasterArrayStockEntry = [];
                itemMasterDetailsStockEntry = {};
                $.each(data.d, function (i, j) {
                    itemMasterArrayStockEntry.push(j.code);
                    itemMasterDetailsStockEntry[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode];
                });
            },
            error: function (result) {
                alert("Error");
            }
        });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PriceBookEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                itemMasterArrayStockEntryByName = [];
                itemMasterDetailsStockEntryByName = {};
                itemMasterQuantity = {};
                $.each(data.d, function (i, j) {
                    itemMasterArrayStockEntryByName.push(j.name);
                    itemMasterDetailsStockEntryByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode];
                });
            },
            error: function (result) {
                //alert("Error");
            }
        });
    }
    Fif ($("#GRNId").length > 0) {
        if ($("#GoodsReceipt").val() == "") {
            alert("Please create first free number for goods receipt");
            $("#btnSaveDtl").attr("disabled", true);
        }
    }
    if ($("#SalesQuotationId").length > 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SQEdit.aspx/GetFirstFreerDetails",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                firstFreeSQ = [];
                $.each(data.d, function (i, j) {
                    firstFreeSQ.push({
                        id: j.id,
                        sequenceNumber: j.sequenceNumber,
                        seqType: j.seqType,
                        orderType: j.orderType,
                        enterpriseUnitCode:j.enterpriseUnitCode
                    });
                    if(firstFreeSQ.length==0)
                    {
                        alert("Please create first free number for Sales Quotation");
                        $("#SaveBtn").attr("disabled", true);
                    }
                });
            },
            error: function (result) {
               // alert("Error");
            }
        });
       
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SQEdit.aspx/GetCustomerDetails",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                customerCodes = [];
                customerCodesWithDetails = {};
                $.each(data.d, function (i, j) {
                    customerCodes.push(j.code);
                    customerCodesWithDetails[j.code] = [j.name, j.telephone, j.orderType];
                });
            },
            error: function (result) {
                //alert("Error");
            }
        });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "TaxMst.aspx/GetAllTaxDetails",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                itemTaxCodes = [];
                itemTaxCodevalues = [];
                itemTaxDetails = {};
                $.each(data.d, function (i, j) {
                    itemTaxCodes.push(j.code);
                    itemTaxCodevalues.push(j.Per);
                    itemTaxDetails[j.code] = [j.Per];
                });
            },
            error: function (result) {
                //alert("Error");
            }
        });
        $(".SQTaxAmt").attr('readonly', 'readonly');
        $(".SQNetAmt").attr('readonly', 'readonly');
        $(".SQUnit").attr('readonly', 'readonly');
    }
    if ($("#PurchaseOrderId").length > 0) {
        if ($("#OrderNo").val() == "") {
            alert("Please create first free number for purchase order");
            $("#SaveBtn").attr("disabled", true);
        }
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "TaxMst.aspx/GetAllTaxDetails",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                itemTaxCodes = [];
                itemTaxDetails = {};
                $.each(data.d, function (i, j) {
                    itemTaxCodes.push(j.code);
                    itemTaxDetails[j.code] = [j.Per];
                });
            },
            error: function (result) {
                //alert("Error");
            }
        });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PriceBookEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                itemMasterArrayPO = [];
                itemMasterDetailsPO = {};
                itemMasterQuantityPO = {};
                $.each(data.d, function (i, j) {
                    itemMasterArrayPO.push(j.code);
                    itemMasterDetailsPO[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty];
                    itemMasterQuantityPO[j.code] = [j.Qnty];
                });
            },
            error: function (result) {
                //alert("Error");
            }
        });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PriceBookEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                itemMasterArrayPOByName = [];
                itemMasterDetailsPOByName = {};
                itemMasterQuantityPO = {};
                $.each(data.d, function (i, j) {
                    itemMasterArrayPOByName.push(j.name);
                    itemMasterDetailsPOByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty];
                    itemMasterQuantityPO[j.code] = [j.Qnty];
                });
            },
            error: function (result) {
                //alert("Error");
            }
        });
    }
    if ($("#SalesReturnMstId").length > 0) {
        if ($("#SalesReturn").val() == "") {
            alert("Please create first free number for Sales Return");
            $("#SaveBtn").attr("disabled", true);
        }
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "TaxMst.aspx/GetAllTaxDetails",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                itemTaxCodes = [];
                itemTaxDetails = {};
                $.each(data.d, function (i, j) {
                    itemTaxCodes.push(j.code);
                    itemTaxDetails[j.code] = [j.Per];
                });
            },
            error: function (result) {
               // alert("Error");
            }
        });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PriceBookEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                itemMasterArraySalesReturn = [];
                itemMasterDetailsSalesReturn = {};
                //itemMasterQuantityI = {};
                $.each(data.d, function (i, j) {
                    itemMasterArraySalesReturn.push(j.code);
                    itemMasterDetailsSalesReturn[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty];
                    //itemMasterQuantityI[j.code] = [j.Qnty];
                });

            },
            error: function (result) {
               // alert("Error");
            }
        });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PriceBookEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                itemMasterArraySalesReturnByName = [];
                itemMasterDetailsSalesReturnByName = {};
                //itemMasterQuantityI = {};
                $.each(data.d, function (i, j) {
                    itemMasterArraySalesReturnByName.push(j.name);
                    itemMasterDetailsSalesReturnByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty];
                    //itemMasterQuantityI[j.code] = [j.Qnty];
                });
            },
            error: function (result) {
               // alert("Error");
            }
        });
    }
    if ($("#InvoiceId").length > 0) {
        if ($("#Invoice").val() == "") {
            alert("Please create first free number for Manual Invoice");
            $("#btnSaveDtl").attr("disabled", true);
        }
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PriceBookEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj),
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
            },
            error: function (result) {
               // alert("Error");
            }
        });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PriceBookEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj),
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
            },
            error: function (result) {
              //  alert("Error");
            }
        });
    }
    if ($("#PriceBookId").length > 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PriceBookEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                itemMasterArray = [];
                //itemMasterArrayOriginal = [];
                itemMasterDetails = {};
                itemMasterArrayByName = [];
                //itemMasterArrayByNameOriginal = [];
                itemMasterDetailsByName = {};
                $.each(data.d, function (i, j) {
                    itemMasterArray.push(j.code);
                    
                    itemMasterDetails[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.name];
                    itemMasterArrayByName.push(j.name);
                    //itemMasterArrayByNameOriginal.push(j.name);
                    itemMasterDetailsByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.name];
                });
            },
            error: function (result) {
               // alert("Error");
            }
        });
    }
    if ($("#SalesInvoiceId").length > 0) {
        if ($("#Invoice").val() == "") {
            alert("Please create first free number for Sales Invoice");
            $("#btnSaveDtl").attr("disabled", true);
        }
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "TaxMst.aspx/GetAllTaxDetails",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                itemTaxCodes = [];
                itemTaxDetails = {};
                $.each(data.d, function (i, j) {
                    itemTaxCodes.push(j.code);
                    itemTaxDetails[j.code] = [j.Per];
                });
            },
            error: function (result) {
               // alert("Error");
            }
        });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PriceBookEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj),
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

            },
            error: function (result) {
                //alert("Error");
            }
        });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PriceBookEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj),
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
            },
            error: function (result) {
              //  alert("Error");
            }
        });
    }
    if ($("#GRNDetail").length == 1 && $("#Status").val() == 2) {
        $(".ReceivedQuantity").attr('readonly', 'readonly');
    }
    if ($("#GRNDetail").length == 1 && $("#TotalQty").val() == 0) {
        var totalQty = 0;
        $("tr", $("#GRNDetail")).each(function () {

            var val = $("input[id*='ReceivedQuantity']", $(this)).val();
            if (typeof (val) !== "undefined") {
                totalQty += parseInt(val);
            }

        });
        $("#TotalQty").val(totalQty);
    }
    if ($("#StokeEntryMstId").length > 0 && $("#PageStatus").val() != "create") {
        itemMasterArrayStockEntry = [];
        itemMasterDetailsStockEntry = {};
        var i = 0;
        $("tr", $("#StockEntryDetail")).each(function () {
            var val = $("input[id*='Item']", $(this)).val();
            var qnty = parseInt($("input[id*='SEQuantity']", $(this)).val());
            var rate = parseFloat($("input[id*='SERate']", $(this)).val());
            if (typeof (val) !== "undefined" && val != "" && val != "undefined") {
                SEItemRowDetails[i] = [qnty, rate, qnty * rate];
                i++;
            }

        });
    }
    if ($("#StokeEntryMstId").length > 0 && $("#PageStatus").val() == "creating") {
        $(".StockUnit").attr('readonly', 'readonly');
        $(".StockAmount").attr('readonly', 'readonly');
    }
    if (($("#SalesReturnMstId").val() != "" || $("#SalesReturnMstId").val() != "0") && $("#PageStatus").val() != "create") {
        itemMasterArraySalesReturn = [];
        itemMasterDetailsSalesReturn = {};
        var i = 0;
        $("tr", $("#SalesReturnDetail")).each(function () {

            var val = $("input[id*='SRItem']", $(this)).val();
            var qnty = parseInt($("input[id*='SRQuantity']", $(this)).val());
            var rate = parseFloat($("input[id*='SRItemRate']", $(this)).val());
            var discountAmt = parseFloat($("input[id*='SRDiscAmt']", $(this)).val());
            var taxPer = parseFloat($("input[id*='SRTaxPer']", $(this)).val());
            if (typeof (val) !== "undefined" && val != "" && val != "undefined") {
                var rowTotalRate = qnty * rate;
                var discountPer = (discountAmt / rowTotalRate).toFixed(2);
                var taxAmount = (rowTotalRate - discountAmt) * taxPer;
                var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                var orderAmount = netAmount;
                SRItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                i++;
            }

        });
        if ($("#Status").val() == "2") {
            $(".SRItem").attr('readonly', 'readonly');
            $(".SRItemName").attr('readonly', 'readonly');
            $(".SRItemRate").attr('readonly', 'readonly');
            $(".SRQuantity").attr('readonly', 'readonly');
            $(".SRUnit").attr('readonly', 'readonly');
            $(".SRDiscPer").attr('readonly', 'readonly');
            $(".SRDiscAmt").attr('readonly', 'readonly');
            $(".SRTaxPer").attr('readonly', 'readonly');
            $(".SRTaxAmt").attr('readonly', 'readonly');
            $(".SRNetAmt").attr('readonly', 'readonly');
            $("#SRTotalAmount").attr('readonly', 'readonly');
            $("#SRTotalDiscountAmt").attr('readonly', 'readonly');
            $("#SRTotalTaxAmt").attr('readonly', 'readonly');
            $("#SRTotalOrderAmt").attr('readonly', 'readonly');
        }
        else if ($("#Status").val() == "1") {
            $(".SRItem").attr('readonly', 'readonly');
            $(".SRItemName").attr('readonly', 'readonly');
            $(".SRItemRate").attr('readonly', 'readonly');
            $(".SRUnit").attr('readonly', 'readonly');
            $(".SRDiscPer").attr('readonly', 'readonly');
            $(".SRDiscAmt").attr('readonly', 'readonly');
            $(".SRTaxPer").attr('readonly', 'readonly');
            $(".SRTaxAmt").attr('readonly', 'readonly');
            $(".SRNetAmt").attr('readonly', 'readonly');
            $("#SRTotalAmount").attr('readonly', 'readonly');
            $("#SRTotalDiscountAmt").attr('readonly', 'readonly');
            $("#SRTotalTaxAmt").attr('readonly', 'readonly');
            $("#SRTotalOrderAmt").attr('readonly', 'readonly');
        }
    }
    if ($("#InvoiceId").length > 0 && $("#PageStatus").val() != "create") {
        itemMasterArrayManualInvoice = [];
        itemMasterDetailsManualInvoice = {};
        var i = 0;
        $("tr", $("#ManualInvoiceDetail")).each(function () {

            var val = $("input[id*='MIItem']", $(this)).val();
            var qnty = parseInt($("input[id*='MIQuantity']", $(this)).val());
            var rate = parseFloat($("input[id*='MIItemRate']", $(this)).val());
            var discountAmt = parseFloat($("input[id*='MIDiscAmt']", $(this)).val());
            var taxPer = parseFloat($("input[id*='MITaxPer']", $(this)).val());
            if (typeof (val) !== "undefined" && val != "" && val != "undefined") {
                var rowTotalRate = qnty * rate;
                var discountPer = (discountAmt / rowTotalRate).toFixed(2);
                var taxAmount = (rowTotalRate - discountAmt) * taxPer;
                var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                var orderAmount = netAmount;
                MIItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                i++;
            }

        });
        if ($("#PageStatus").val() == "edit") {
            $(".MITaxAmt").attr('readonly', 'readonly');
            $(".MINetAmt").attr('readonly', 'readonly');
            $(".MIUnit").attr('readonly', 'readonly');
            $(".MIItem").attr('readonly', 'readonly');
            $(".MIItemName").attr('readonly', 'readonly');
        }
    }
    else if ($("#PageStatus").val() == "create") {
        $(".MITaxAmt").attr('readonly', 'readonly');
        $(".MINetAmt").attr('readonly', 'readonly');
        $(".MIUnit").attr('readonly', 'readonly');
    }
    if ($("#SalesInvoiceId").length > 0 && $("#PageStatus").val() != "create") {
        itemMasterArrayInvoice = [];
        itemMasterDetailsInvoice = {};
        var i = 0;
        $("tr", $("#InvoiceDetail")).each(function () {

            var val = $("input[id*='IItem']", $(this)).val();
            var qnty = parseInt($("input[id*='IQuantity']", $(this)).val());
            var rate = parseFloat($("input[id*='IItemRate']", $(this)).val());
            var discountAmt = parseFloat($("input[id*='IDiscAmt']", $(this)).val());
            var taxPer = parseFloat($("input[id*='ITaxPer']", $(this)).val());
            if (typeof (val) !== "undefined" && val != "" && val != "undefined") {
                var rowTotalRate = qnty * rate;
                var discountPer = (discountAmt / rowTotalRate).toFixed(2);
                var taxAmount = (rowTotalRate - discountAmt) * taxPer;
                var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                var orderAmount = netAmount;
                IItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                i++;
            }

        });
        if ($("#Status").val() == "1") {
            $(".ITaxAmt").attr('readonly', 'readonly');
            $(".INetAmt").attr('readonly', 'readonly');
            $(".IUnit").attr('readonly', 'readonly');
            $(".IItem").attr('readonly', 'readonly');
            $(".IItemName").attr('readonly', 'readonly');
        }
        else if ($("#Status").val() == "2") {
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
        var obj1 = {};
        obj1.companyCode = $.trim($("#CompanyCode").val());
        obj1.orderType = $("#QuotationType").val();
        obj1.priceType = 0;
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SQEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj1),
            dataType: "json",
            success: function (data) {
                itemMasterArraySQ = [];
                itemMasterDetailsSQ = {};
                itemMasterQuantitySQ = {};
                itemMasterArraySQByName = [];
                itemMasterDetailsSQByName = {};
                $.each(data.d, function (i, j) {
                    itemMasterArraySQ.push(j.code);
                    itemMasterDetailsSQ[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint];
                    itemMasterQuantitySQ[j.code] = [j.Qnty];
                    itemMasterArraySQByName.push(j.name);
                    itemMasterDetailsSQByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint];

                });
            },
            error: function (result) {
                //alert("Error");
            }
        });
        itemMasterArraySQ = [];
        itemMasterDetailsSQ = {};
        var i = 0;
        $("tr", $("#SalesQuotationDetail")).each(function () {

            var val = $("input[id*='SQItem']", $(this)).val();
            var qnty = parseInt($("input[id*='SQQuantity']", $(this)).val());
            var rate = parseFloat($("input[id*='SQRate']", $(this)).val());
            var discountAmt = parseFloat($("input[id*='SQDiscAmt']", $(this)).val());
            var taxPer = parseFloat($("input[id*='SQTaxPer']", $(this)).val());
            if (typeof (val) !== "undefined" && val != "" && val != "undefined") {

                var rowTotalRate = qnty * rate;
                var discountPer = (discountAmt / rowTotalRate).toFixed(2);
                var taxAmount = (rowTotalRate - discountAmt) * taxPer;
                var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                var orderAmount = netAmount;
                SQItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                i++;
            }

        });
        if ($("#PageStatus").val() == "edit") {
            $(".SQTaxAmt").attr('readonly', 'readonly');
            $(".SQNetAmt").attr('readonly', 'readonly');
            $(".SQUnit").attr('readonly', 'readonly');
            $(".SQItem").attr('readonly', 'readonly');
            $(".SQName").attr('readonly', 'readonly');
        }
    }
    if (($("#PurchaseOrderId").val() != "" || $("#PurchaseOrderId").val() != "0") && $("#PageStatus").val() != "create") {
        itemMasterArrayPO = [];
        itemMasterDetailsPO = {};
        var i = 0;
        $("tr", $("#PurchaseOrderDetail")).each(function () {

            var val = $("input[id*='POItem']", $(this)).val();
            var qnty = parseInt($("input[id*='POQuantity']", $(this)).val());
            var rate = parseFloat($("input[id*='PORate']", $(this)).val());
            var discountAmt = parseFloat($("input[id*='PODiscAmt']", $(this)).val());
            var taxPer = parseFloat($("input[id*='POTaxPer']", $(this)).val());
            if (typeof (val) !== "undefined" && val != "" && val != "undefined") {
                var rowTotalRate = qnty * rate;
                var discountPer = (discountAmt / rowTotalRate).toFixed(2);
                var taxAmount = (rowTotalRate - discountAmt) * taxPer;
                var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                var orderAmount = netAmount;
                POItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                i++;
            }

        });
        if ($("#Status").val() == "1") {
            $(".POTaxAmt").attr('readonly', 'readonly');
            $(".PONetAmt").attr('readonly', 'readonly');
            $(".POUnit").attr('readonly', 'readonly');
            $(".POItem").attr('readonly', 'readonly');
            $(".POName").attr('readonly', 'readonly');
        }
        else if ($("#Status").val() == "2" || $("#IsFinalized").val() != "0") {
            $(".POTaxAmt").attr('readonly', 'readonly');
            $(".PONetAmt").attr('readonly', 'readonly');
            $(".POUnit").attr('readonly', 'readonly');
            $(".POItem").attr('readonly', 'readonly');
            $(".POName").attr('readonly', 'readonly');
            $(".PORate").attr('readonly', 'readonly');
            $(".POQuantity").attr('readonly', 'readonly');
            $(".PODiscPer").attr('readonly', 'readonly');
            $(".PODiscAmt").attr('readonly', 'readonly');
            $(".POTaxPer").attr('readonly', 'readonly');
        }
        else if ($("#PageStatus").val() == "creating") {
            $(".POTaxAmt").attr('readonly', 'readonly');
            $(".PONetAmt").attr('readonly', 'readonly');
            $(".POUnit").attr('readonly', 'readonly');
        }
    }
    
    $("#mainForm").validate();
});


$(document).ready(function () {

    SearchText();

    function DeleteConfirm() {
        var Ans = confirm("Do you want to Delete Selected Employee Record?");
        if (Ans) {
            return true;
        }
        else {
            return false;
        }
    }

    //$(document).on("click", "#saveItemMaster", function () {
    //    if (parseInt($("#SafetStock").val()) < parseInt($("#ReorderQty").val())) {
    //        alert("Reorder quantity should be lesser or equal to safety stock");
    //        return false;
    //    }
    //    else {
    //        return true;
    //    }

    //});

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

        if ($("#Transaction").val() == "1") {
            $("#lblBankDetail").text("Bank Code");
        }
        else {
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
               // alert("Error");
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
                  //  alert("Error");
                }
            });
        },
        response: function (event, ui) {
            if (!ui.content.length) {
                var noResult = { value: "", label: "No results found" };
                ui.content.push(noResult);
            }
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
    itemMasterSelectedItem.push(selectedItem);
    var itemArr = itemMasterDetails[selectedItem];
    row.cells[2].getElementsByTagName("input")[0].value = itemArr[4];
    row.cells[3].getElementsByTagName("input")[0].value = itemArr[1];
    row.cells[4].getElementsByTagName("input")[0].value = $("#Currency").val();
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[2];
    row.cells[6].getElementsByTagName("input")[0].value = itemArr[3];
    return false;
}
function SetSelectedRowByName(lnk, selectedItem) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var itemArr = itemMasterDetailsByName[selectedItem];
    itemMasterSelectedItem.push(itemArr[0]);
    row.cells[1].getElementsByTagName("input")[0].value = itemArr[0];
    row.cells[3].getElementsByTagName("input")[0].value = itemArr[1];
    row.cells[4].getElementsByTagName("input")[0].value = $("#Currency").val();
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[2];
    row.cells[6].getElementsByTagName("input")[0].value = itemArr[3];
    return false;
}
//var obj = {};
//obj.companyCode = $.trim($("#CompanyCode").val());
//$(".ItemCode").autocomplete({
//    source: function (request, response) {
//        $.ajax({
//            type: "POST",
//            contentType: "application/json; charset=utf-8",
//            url: "PriceBookEdit.aspx/GetItemMasters",
//            data: JSON.stringify(obj),
//            dataType: "json",
//            success: function (data) {
//                itemMasterArray = [];
//                itemMasterDetails = {};
//                $.each(data.d, function (i, j) {
//                    itemMasterArray.push(j.code);
//                    itemMasterDetails[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice];
//                });
//                response(itemMasterArray);
//            },
//            error: function (result) {
//                alert("Error");
//            }
//        });
//    },
//    select: function (event, ui) {
//        SetSelectedRow(this, ui.item.label);

//    },
//});
function CheckItemAlreadyAdded(val)
{
    var flag = false;
    $("tr", $("#PriceBookDetail")).each(function () {
        if(val==$("input[id*='ItemCode']", $(this)).val())
        {
            flag = true;
        }
    });
    return flag;
}
$(document).on("keydown", "#Description", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayByName,
        select: function (event, ui) {
            var itemArr = itemMasterDetailsByName[ui.item.label];
            if (!CheckItemAlreadyAdded(itemArr[0])) {
                SetSelectedRowByName(this, ui.item.label);
            }
            else
            {
                alert("Item Already added");
                $(this).val("");
                var row = this.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;
                row.cells[1].getElementsByTagName("input")[0].value = "";
                row.cells[3].getElementsByTagName("input")[0].value = "";
                row.cells[4].getElementsByTagName("input")[0].value = "";
                row.cells[5].getElementsByTagName("input")[0].value = "";
                row.cells[6].getElementsByTagName("input")[0].value = "";
                return false;
                
            }
        },
        change: function (event, ui) {
            val = $(this).val(); 
            exists = $.inArray(val, itemMasterArrayByName);
            if (exists < 0) {
                $(this).val("");
                var row = this.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;
                row.cells[1].getElementsByTagName("input")[0].value = "";
                row.cells[3].getElementsByTagName("input")[0].value = "";
                row.cells[4].getElementsByTagName("input")[0].value = "";
                row.cells[5].getElementsByTagName("input")[0].value = "";
                row.cells[6].getElementsByTagName("input")[0].value = "";
                return false;
            }
            
        },
        response: function (event, ui) {
            if (!ui.content.length) {
                var noResult = { value: "", label: "No item found" };
                ui.content.push(noResult);
            }
        }
    });
});
$(document).on("keydown", ".ItemCode", function (e) {
    $(this).autocomplete({
        source: itemMasterArray,
        select: function (event, ui) {
            if (!CheckItemAlreadyAdded(ui.item.label)) {
                SetSelectedRow(this, ui.item.label);
                
            }
            else {
                alert("Item Already added");
                $(this).val("");
                var row = this.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;
                row.cells[2].getElementsByTagName("input")[0].value = "";
                row.cells[3].getElementsByTagName("input")[0].value = "";
                row.cells[4].getElementsByTagName("input")[0].value = "";
                row.cells[5].getElementsByTagName("input")[0].value = "";
                row.cells[6].getElementsByTagName("input")[0].value = "";
                return false;
            }

        },
        change: function (event, ui) {
            val = $(this).val();
            exists = $.inArray(val, itemMasterArray);
            if (exists < 0) {
                $(this).val("");
                $(this).focus();
                var row = this.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;
                row.cells[2].getElementsByTagName("input")[0].value = "";
                row.cells[3].getElementsByTagName("input")[0].value = "";
                row.cells[4].getElementsByTagName("input")[0].value = "";
                row.cells[5].getElementsByTagName("input")[0].value = "";
                row.cells[6].getElementsByTagName("input")[0].value = "";
                return false;
            }
            
        },
        response: function (event, ui) {
            if (!ui.content.length) {
                var noResult = { value: "", label: "No item found" };
                ui.content.push(noResult);
            }
        }
    });
});
$(document).on("click", "#lnkDelete", function (e) {
    if (typeof $(this).attr('data-id') !== "undefined")
        $("#DeletedRowIDs").val($("#DeletedRowIDs").val()+$(this).attr('data-id')+",");
    var row = this.parentNode.parentNode;
    row.remove();
    var index = 1;
    $("td:first-child", $("#PriceBookDetail")).each(function () {
        $(this).find("span:first").text(index);
        index++;

    }); 
    $("#rowCount").val(--index);
    return false;
});
$(document).on("click", "#lnkDeleteSQ", function (e) {
    if (typeof $(this).attr('data-id') !== "undefined")
        $("#DeletedRowIDs").val($("#DeletedRowIDs").val() + $(this).attr('data-id') + ",");
    var row = this.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (SQItemRowDetails[rowIndex])
    {
        var itemArray = SQItemRowDetails[rowIndex];
        var totalAmt = parseFloat($("#TotalAmount").val());
        var totalDiscount = parseFloat($("#TotalDiscountAmt").val());
        var totalTax = parseFloat($("#TotalTaxAmt").val());
        var totalOder = parseFloat($("#TotalOrderAmt").val());
        var oldAmount = parseFloat(itemArray[6]);
        var oldDiscount = parseFloat(itemArray[3]);
        var oldTax = parseFloat(itemArray[5]);
        var oldOder = parseFloat(itemArray[7]);
        totalAmt -= parseFloat(oldAmount);
        totalDiscount -= parseFloat(oldDiscount);
        totalTax -= parseFloat(oldTax);
        totalOder -= parseFloat(oldOder);
        $("#TotalAmount").val(totalAmt);
        $("#TotalDiscountAmt").val(totalDiscount);
        $("#TotalTaxAmt").val(totalTax);
        $("#TotalOrderAmt").val(totalOder);
        var i;
        for (i = rowIndex; i < SQItemRowDetails.length; i++)
        {
            if (SQItemRowDetails[i + 1])
            {
                SQItemRowDetails[i] = SQItemRowDetails[i + 1];
            }
            else
            {
                SQItemRowDetails.splice(i, 1);
            }
        }
    }
    row.remove();
    var index = 1;
    $("td:first-child", $("#SalesQuotationDetail")).each(function () {
        $(this).find("span:first").text(index);
        index++;

    });
    $("#rowCount").val(--index);
    return false;
});
$(document).on("keydown", "#SalesMan", function (e) {
    
    $(this).autocomplete({
        source: contactArray,
            change: function (event, ui) {
                val = $(this).val();
                exists = $.inArray(val, contactArray);
                if (exists < 0) {
                    $(this).val("");
                    $("#SalesManHidden").val("");
                    return false;
                }
                else
                {
                    $("#SalesManHidden").val(contactArrayWithName[val]);
                }
            },
        response: function (event, ui) {
            if (!ui.content.length) {
                var noResult = { value: "", label: "No results found" };
                ui.content.push(noResult);
            }
        }
    });
});
$(document).on("keydown", "#Location", function (e) {

    $(this).autocomplete({
        source: locationArray,
        change: function (event, ui) {
            val = $(this).val();
            exists = $.inArray(val, locationArray);
            if (exists < 0) {
                $(this).val("");
                $("#LocationHidden").val("");
                return false;
            }
            else {
                $("#LocationHidden").val(locationArrayWithName[val]);
                $.each(firstFreeSQ, function (i, j) {
                   
                    if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == $("#QuotationType").val()) {
                        $("#Quotation").val(j.sequenceNumber);
                        $("#SQSequenceNoID").val(j.id);
                    }
                });
            }
        },
        response: function (event, ui) {
            if (!ui.content.length) {
                var noResult = { value: "", label: "No results found" };
                ui.content.push(noResult);
            }
        }
    });
});
$(document).on("keydown", "#CustomerId", function (e) {

    $(this).autocomplete({
        source: customerCodes,
        change: function (event, ui) {
            val = $(this).val();
            exists = $.inArray(val, customerCodes);
            if (exists < 0) {
                $(this).val("");
                $("#Name").val("");
                $("#Telephone").val("");
                $("#Quotation").val("");
                $("#SQSequenceNoID").val("");
                return false;
            }
            else {
                var item = customerCodesWithDetails[val];
                $("#Name").val(item[0]);
                $("#Telephone").val(item[1]);
                $("#QuotationType").val(item[2]);
                $('#QuotationType option[value="0"]').attr("selected", null);
                $('#QuotationType option[value="1"]').attr("selected", null);
                $('#QuotationType option[value="' + item[2] + '"]').attr("selected", "selected");
                $("#selectedQuotationType").val(item[2]);
                $.each(firstFreeSQ, function (i, j) {
                    if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == item[2])
                    {
                        $("#Quotation").val(j.sequenceNumber);
                        $("#SQSequenceNoID").val(j.id);
                    }
                    else if (j.seqType == "0" && j.orderType == item[2]) {
                        $("#Quotation").val(j.sequenceNumber);
                        $("#SQSequenceNoID").val(j.id);
                    }
                });
                var obj1 = {};
                obj1.companyCode = $.trim($("#CompanyCode").val());
                obj1.orderType = item[2];
                obj1.priceType = 0;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "SQEdit.aspx/GetItemMasters",
                    data: JSON.stringify(obj1),
                    dataType: "json",
                    success: function (data) {
                        itemMasterArraySQ = [];
                        itemMasterDetailsSQ = {};
                        itemMasterQuantitySQ = {};
                        itemMasterArraySQByName = [];
                        itemMasterDetailsSQByName = {};
                        $.each(data.d, function (i, j) {
                            itemMasterArraySQ.push(j.code);
                            itemMasterDetailsSQ[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint];
                            itemMasterQuantitySQ[j.code] = [j.Qnty];
                            itemMasterArraySQByName.push(j.name);
                            itemMasterDetailsSQByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint];

                        });
                    },
                    error: function (result) {
                        //alert("Error");
                    }
                });
                
            }
        },
        response: function (event, ui) {
            if (!ui.content.length) {
                var noResult = { value: "", label: "No results found" };
                ui.content.push(noResult);
            }
        }
    });
});
//$(document).on("keydown", "#SalesMan", function (e) {
//    var obj = {};
//    obj.companyCode = $.trim($("#CompanyCode").val());



//});


$(".Name").attr('readonly', 'readonly');
$(".Rate").attr('readonly', 'readonly');
$(".Unit").attr('readonly', 'readonly');
$(".NetAmt").attr('readonly', 'readonly');
$("#TotalAmount").attr('readonly', 'readonly');
$("#TotalDiscountAmt").attr('readonly', 'readonly');
$("#TotalTaxAmt").attr('readonly', 'readonly');
$("#TotalOrderAmt").attr('readonly', 'readonly');
if ($("#SalesOrder").val() != "") {
    $(".gridTxtBox").attr('readonly', 'readonly');
}


$(document).on("keydown", ".StockItem", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayStockEntry,
        select: function (event, ui) {
            SetSelectedRowStockEntry(this, ui.item.label);

        }
    });
});
$(document).on("keydown", ".StockName", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayStockEntryByName,
        select: function (event, ui) {
            SetSelectedRowStockEntryByName(this, ui.item.label);

        }
    });
    //$(this).autocomplete(optionsStockEntryByName);
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
                    $("#Amount").val(parseFloat($("#Amount").val()) + parseFloat(amount));
                }
            }
            else {
                //alert("okkk");
                var itemArray = SEItemRowDetails[rowIndex];
                var totalAmt = parseFloat($("#Amount").val());
                var oldAmount = parseFloat(itemArray[2]);
                totalAmt -= parseFloat(oldAmount);
                totalAmt += parseFloat(amount);
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

if ($(".ItemCode").val() != "") {
    $(".ItemCode").attr('readonly', 'readonly');

}
$(".Description").attr('readonly', 'readonly');
$(".SupplierBarcode").attr('readonly', 'readonly');
$(".CurrencyCode").attr('readonly', 'readonly');
$(".OrderType").attr('readonly', 'readonly');
$(".EnterpriseUnitLocation").attr('readonly', 'readonly');
$(document).on("click", "#saveFirstFreeNumberBtn", function (e) {
    var prefix = [];
    var sequence = [];
    var flag = 0;
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
    $.each(prefix, function (index, value) {
        if (lastValue != "") {
            if (value == lastValue) {
                if (sequence[i - 1] == sequence[i]) {
                    
                    flag = 1;
                }
            }
        }
        lastValue = value;
        i++;
    });
    if (flag == 1) {
        alert("Same prefix and sequence number not allowed for more than one time");
        return false;
    }
    else {
        return true;
    }

});


$(document).on("keydown", ".txtNumeric", function (e) {
    if (e.shiftKey || e.ctrlKey || e.altKey) {
        e.preventDefault();
    } else {
        var key = e.keyCode;
        if (!((key == 190) || (key == 110) || (key == 9) || (key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
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


$(document).on("keydown", ".MIItem", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayManualInvoice,
        select: function (event, ui) {
            SetSelectedRowManualInvoice(this, ui.item.label);

        }
    });
});
$(document).on("keydown", ".MIItemName", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayManualInvoiceByName,
        select: function (event, ui) {
            SetSelectedRowManualInvoiceByName(this, ui.item.label);

        }
    });
    //$(this).autocomplete(optionsManualInvoiceByName);
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
            if ($(txtBox).attr("id") == "MIDiscAmt") {
                discountPer = (discountAmt / rowTotalRate).toFixed(2);
            }
            else {
                discountAmt = (rowTotalRate * discountPer).toFixed(2);
            }
            var taxAmount = ((rowTotalRate - discountAmt) * taxPer).toFixed(2);
            var netAmount = ((rowTotalRate - discountAmt) + parseFloat(taxAmount)).toFixed(2);
            //var orderAmount = ((qnty * rowTotalRate) - discountAmt) * taxAmount;
            var orderAmount = netAmount;
            if ($(txtBox).attr("id") == "MIDiscAmt") {
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
                    $("#MITotalAmount").val(parseFloat($("#MITotalAmount").val()) + parseFloat(netAmount));
                }
                if ($("#Amount").val() == "") {
                    $("#Amount").val(netAmount);
                }
                else {
                    $("#Amount").val(parseFloat($("#Amount").val()) + parseFloat(netAmount));
                }
                if ($("#MITotalDiscountAmt").val() == "") {
                    $("#MITotalDiscountAmt").val(discountAmt);
                }
                else {
                    $("#MITotalDiscountAmt").val(parseFloat($("#MITotalDiscountAmt").val()) + parseFloat(discountAmt));
                }
                if ($("#MITotalTaxAmt").val() == "") {
                    $("#MITotalTaxAmt").val(taxAmount);
                }
                else {
                    $("#MITotalTaxAmt").val(parseFloat($("#MITotalTaxAmt").val()) + parseFloat(taxAmount));
                }
                if ($("#MITotalOrderAmt").val() == "") {
                    $("#MITotalOrderAmt").val(orderAmount);
                }
                else {
                    $("#MITotalOrderAmt").val(parseFloat($("#MITotalOrderAmt").val()) + parseFloat(orderAmount));
                }
            }
            else {
                var itemArray = MIItemRowDetails[rowIndex];
                var totalAmt = parseFloat($("#Amount").val());
                var totalDiscount = parseFloat($("#MITotalDiscountAmt").val());
                var totalTax = parseFloat($("#MITotalTaxAmt").val());
                var totalOder = parseFloat($("#MITotalOrderAmt").val());
                var oldAmount = parseFloat(itemArray[6]);
                var oldDiscount = parseFloat(itemArray[3]);
                var oldTax = parseFloat(itemArray[5]);
                var oldOder = parseFloat(itemArray[7]);
                totalAmt -= parseFloat(oldAmount);
                totalDiscount -= parseFloat(oldDiscount);
                totalTax -= parseFloat(oldTax);
                totalOder -= parseFloat(oldOder);
                totalAmt += parseFloat(netAmount);
                totalDiscount += parseFloat(discountAmt);
                totalTax += parseFloat(taxAmount);
                totalOder += parseFloat(orderAmount);
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
        && row.cells[4].getElementsByTagName("input")[0].value != "") {
        var itemCode = row.cells[1].getElementsByTagName("input")[0].value;
        var itemArr = itemMasterQuantityI[itemCode];
        if (typeof (itemArr) === "undefined") {
            var obj = {};
            obj.companyCode = $.trim($("#CompanyCode").val());
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PriceBookEdit.aspx/GetItemMasters",
                data: JSON.stringify(obj),
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
                   // alert("Error");
                }
            });

        }
        else {
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
        && row.cells[3].getElementsByTagName("input")[0].value != "") {
        var itemCode = row.cells[1].getElementsByTagName("input")[0].value;
        var itemArr = itemMasterQuantitySQ[itemCode];
        if (typeof (itemArr) === "undefined") {
            var obj = {};
            obj.companyCode = $.trim($("#CompanyCode").val());
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PriceBookEdit.aspx/GetItemMasters",
                data: JSON.stringify(obj),
                dataType: "json",
                success: function (data) {
                    itemMasterQuantitySQ = {};
                    $.each(data.d, function (i, j) {
                        itemMasterQuantitySQ[j.code] = [j.Qnty];
                    });
                    itemArr = itemMasterQuantitySQ[itemCode];
                    if (parseInt(itemArr[0]) < parseInt(row.cells[3].getElementsByTagName("input")[0].value)) {
                        alert("Insufficient Qty, Available " + itemArr[0] + " " + row.cells[4].getElementsByTagName("input")[0].value);
                        
                    }
                    CalculateSQAmount(txtBox);
                },
                error: function (result) {
                    //alert("Error");
                }
            });

        }
        else {
            if (parseInt(itemArr[0]) < parseInt(row.cells[3].getElementsByTagName("input")[0].value)) {
                alert("Insufficient Qty, Available " + itemArr[0] + " " + row.cells[4].getElementsByTagName("input")[0].value);
                
            }
            CalculateSQAmount(txtBox);
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


$(document).on("keydown", ".IItem", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayInvoice,
        select: function (event, ui) {
            SetSelectedRowInvoice(this, ui.item.label);

        }
    });
});
$(document).on("keydown", ".IItemName", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayInvoiceByName,
        select: function (event, ui) {
            SetSelectedRowInvoiceByName(this, ui.item.label);

        }
    });
});
$(document).on("keydown", ".ITaxPer", function (e) {
    $(this).autocomplete({
        source: itemTaxCodes,
        select: function (event, ui) {
            event.preventDefault();
            setSelectedTaxCode(this, ui.item.label);

        },
        change: function (event, ui) {
            if (ui.item === null || !ui.item) {
                $(this).val('');
            }
            /* clear the value */
        }
    });
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
            if ($(txtBox).attr("id") == "IDiscAmt") {
                discountPer = (discountAmt / rowTotalRate).toFixed(2);
            }
            else {
                discountAmt = (rowTotalRate * discountPer).toFixed(2);
            }
            var taxAmount = ((rowTotalRate - discountAmt) * taxPer).toFixed(2);
            var netAmount = ((rowTotalRate - discountAmt) + parseFloat(taxAmount)).toFixed(2);
            //var orderAmount = ((qnty * rowTotalRate) - discountAmt) * taxAmount;
            var orderAmount = netAmount;
            if ($(txtBox).attr("id") == "IDiscAmt") {
                row.cells[6].getElementsByTagName("input")[0].value = discountPer;

            }
            else {
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
                    $("#ITotalAmount").val(parseFloat($("#ITotalAmount").val()) + parseFloat(netAmount));
                }
                if ($("#Amount").val() == "") {
                    $("#Amount").val(netAmount);
                }
                else {
                    $("#Amount").val(parseFloat($("#Amount").val()) + parseFloat(netAmount));
                }
                if ($("#ITotalDiscountAmt").val() == "") {
                    $("#ITotalDiscountAmt").val(discountAmt);
                }
                else {
                    $("#ITotalDiscountAmt").val(parseFloat($("#ITotalDiscountAmt").val()) + parseFloat(discountAmt));
                }
                if ($("#ITotalTaxAmt").val() == "") {
                    $("#ITotalTaxAmt").val(taxAmount);
                }
                else {
                    $("#ITotalTaxAmt").val(parseFloat($("#ITotalTaxAmt").val()) + parseFloat(taxAmount));
                }
                if ($("#ITotalOrderAmt").val() == "") {
                    $("#ITotalOrderAmt").val(orderAmount);
                }
                else {
                    $("#ITotalOrderAmt").val(parseFloat($("#ITotalOrderAmt").val()) + parseFloat(orderAmount));
                }
            }
            else {
                var itemArray = IItemRowDetails[rowIndex];
                var totalAmt = parseFloat($("#Amount").val());
                var totalDiscount = parseFloat($("#ITotalDiscountAmt").val());
                var totalTax = parseFloat($("#ITotalTaxAmt").val());
                var totalOder = parseFloat($("#ITotalOrderAmt").val());
                var oldAmount = parseFloat(itemArray[6]);
                var oldDiscount = parseFloat(itemArray[3]);
                var oldTax = parseFloat(itemArray[5]);
                var oldOder = parseFloat(itemArray[7]);
                totalAmt -= parseFloat(oldAmount);
                totalDiscount -= parseFloat(oldDiscount);
                totalTax -= parseFloat(oldTax);
                totalOder -= parseFloat(oldOder);
                totalAmt += parseFloat(netAmount);
                totalDiscount += parseFloat(discountAmt);
                totalTax += parseFloat(taxAmount);
                totalOder += parseFloat(orderAmount);
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
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[4].getElementsByTagName("input")[0].value = itemArr[4];
    row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
    row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
    return false;
}
function SetSelectedRowSQByName(lnk, selectedItem) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var itemArr = itemMasterDetailsSQByName[selectedItem];
    row.cells[1].getElementsByTagName("input")[0].value = itemArr[0];
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[4].getElementsByTagName("input")[0].value = itemArr[4];
    row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
    row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
    return false;
}

function CheckItemAlreadyAddedSQ(val) {
    var flag = false;
    $("tr", $("#SalesQuotationDetail")).each(function () {
        if (val == $("input[id*='SQItem']", $(this)).val()) {
            flag = true;
        }
    });
    return flag;
}
$(document).on("keydown", ".SQItem", function (e) {
    $(this).autocomplete({
        source: itemMasterArraySQ,
        select: function (event, ui) {
            if (!CheckItemAlreadyAddedSQ(ui.item.label)) {
                SetSelectedRowSQ(this, ui.item.label);

            }
            else {
                alert("Item Already added");
                $(this).val("");
                var row = this.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;
                row.cells[2].getElementsByTagName("input")[0].value = "";
                row.cells[3].getElementsByTagName("input")[0].value = "";
                row.cells[4].getElementsByTagName("input")[0].value = "";
                row.cells[5].getElementsByTagName("input")[0].value = "";
                row.cells[6].getElementsByTagName("input")[0].value = "";
                row.cells[7].getElementsByTagName("input")[0].value = "";
                row.cells[8].getElementsByTagName("input")[0].value = "";
                row.cells[9].getElementsByTagName("input")[0].value = "";
                row.cells[10].getElementsByTagName("input")[0].value = "";
                return false;
            }

        },
        change: function (event, ui) {
            val = $(this).val();
            exists = $.inArray(val, itemMasterArraySQ);
            if (exists < 0) {
                $(this).val("");
                $(this).focus();
                var row = this.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;
                row.cells[2].getElementsByTagName("input")[0].value = "";
                row.cells[3].getElementsByTagName("input")[0].value = "";
                row.cells[4].getElementsByTagName("input")[0].value = "";
                row.cells[5].getElementsByTagName("input")[0].value = "";
                row.cells[6].getElementsByTagName("input")[0].value = "";
                row.cells[7].getElementsByTagName("input")[0].value = "";
                row.cells[8].getElementsByTagName("input")[0].value = "";
                row.cells[9].getElementsByTagName("input")[0].value = "";
                row.cells[10].getElementsByTagName("input")[0].value = "";
                return false;
            }

        },
        response: function (event, ui) {
            if (!ui.content.length) {
                var noResult = { value: "", label: "No item found" };
                ui.content.push(noResult);
            }
        }
    });
    //$(this).autocomplete(optionsSQ);
});
$(document).on("keydown", ".SQName", function (e) {
    $(this).autocomplete({
        source: itemMasterArraySQByName,
        select: function (event, ui) {
            var itemArr = itemMasterDetailsSQ[ui.item.label];
            if (!CheckItemAlreadyAddedSQ(itemArr[0])) {
                SetSelectedRowSQByName(this, ui.item.label);
            }
            else {
                alert("Item Already added");
                $(this).val("");
                var row = this.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;
                row.cells[1].getElementsByTagName("input")[0].value = "";
                row.cells[3].getElementsByTagName("input")[0].value = "";
                row.cells[4].getElementsByTagName("input")[0].value = "";
                row.cells[5].getElementsByTagName("input")[0].value = "";
                row.cells[6].getElementsByTagName("input")[0].value = "";
                row.cells[7].getElementsByTagName("input")[0].value = "";
                row.cells[8].getElementsByTagName("input")[0].value = "";
                row.cells[9].getElementsByTagName("input")[0].value = "";
                row.cells[10].getElementsByTagName("input")[0].value = "";
                return false;

            }
        },
        change: function (event, ui) {
            val = $(this).val();
            exists = $.inArray(val, itemMasterArraySQByName);
            if (exists < 0) {
                $(this).val("");
                var row = this.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;
                row.cells[1].getElementsByTagName("input")[0].value = "";
                row.cells[3].getElementsByTagName("input")[0].value = "";
                row.cells[4].getElementsByTagName("input")[0].value = "";
                row.cells[5].getElementsByTagName("input")[0].value = "";
                row.cells[6].getElementsByTagName("input")[0].value = "";
                row.cells[7].getElementsByTagName("input")[0].value = "";
                row.cells[8].getElementsByTagName("input")[0].value = "";
                row.cells[9].getElementsByTagName("input")[0].value = "";
                row.cells[10].getElementsByTagName("input")[0].value = "";
                return false;
            }

        },
        response: function (event, ui) {
            if (!ui.content.length) {
                var noResult = { value: "", label: "No item found" };
                ui.content.push(noResult);
            }
        }
    });
    
});
$(document).on("keydown", ".SQTaxPer", function (e) {
    $(this).autocomplete({
        source: itemTaxCodes,
        select: function (event, ui) {
            event.preventDefault();
            setSelectedTaxCode(this, ui.item.label);

        },
        change: function (event, ui) {
             
            val = $.trim($(this).val()); 
            exists = $.inArray(4, itemTaxCodevalues); 
            if (exists < 0) {
                $(this).val("");
                return false;
            }
        }
    });
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
        var itemArr = itemMasterDetailsSQ[row.cells[1].getElementsByTagName("input")[0].value];
        var rate = row.cells[5].getElementsByTagName("input")[0].value;
        var qnty = row.cells[3].getElementsByTagName("input")[0].value;
        var discountPer = row.cells[6].getElementsByTagName("input")[0].value;
        var discountAmt = row.cells[7].getElementsByTagName("input")[0].value;
        var taxPer = row.cells[8].getElementsByTagName("input")[0].value;
        if (qnty != "" && (discountPer != "" || discountAmt != "") && taxPer != "") {
            var rowTotalRate = qnty * rate;
            if ($(txtBox).attr("id") == "SQDiscAmt") {
                discountPer = (discountAmt / rowTotalRate).toFixed(itemArr[9]);
            }
            else {
                discountAmt = (rowTotalRate * discountPer).toFixed(itemArr[9]);
            }
            var taxAmount = ((rowTotalRate - discountAmt) * taxPer).toFixed(itemArr[9]);
            var netAmount = ((rowTotalRate - discountAmt) + parseFloat(taxAmount)).toFixed(itemArr[9]);
            //var orderAmount = ((qnty * rowTotalRate) - discountAmt) * taxAmount;
            var orderAmount = netAmount;
            if ($(txtBox).attr("id") == "SQDiscAmt") {
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
                    $("#TotalAmount").val(parseFloat($("#TotalAmount").val()) + parseFloat(netAmount));
                }
                if ($("#TotalDiscountAmt").val() == "") {
                    $("#TotalDiscountAmt").val(discountAmt);
                }
                else {
                    $("#TotalDiscountAmt").val(parseFloat($("#TotalDiscountAmt").val()) + parseFloat(discountAmt));
                }
                if ($("#TotalTaxAmt").val() == "") {
                    $("#TotalTaxAmt").val(taxAmount);
                }
                else {
                    $("#TotalTaxAmt").val(parseFloat($("#TotalTaxAmt").val()) + parseFloat(taxAmount));
                }
                if ($("#TotalOrderAmt").val() == "") {
                    $("#TotalOrderAmt").val(orderAmount);
                }
                else {
                    $("#TotalOrderAmt").val(parseFloat($("#TotalOrderAmt").val()) + parseFloat(orderAmount));
                }
            }
            else {
                var itemArray = SQItemRowDetails[rowIndex];
                var totalAmt = parseFloat($("#TotalAmount").val());
                var totalDiscount = parseFloat($("#TotalDiscountAmt").val());
                var totalTax = parseFloat($("#TotalTaxAmt").val());
                var totalOder = parseFloat($("#TotalOrderAmt").val());
                var oldAmount = parseFloat(itemArray[6]);
                var oldDiscount = parseFloat(itemArray[3]);
                var oldTax = parseFloat(itemArray[5]);
                var oldOder = parseFloat(itemArray[7]);
                totalAmt -= parseFloat(oldAmount);
                totalDiscount -= parseFloat(oldDiscount);
                totalTax -= parseFloat(oldTax);
                totalOder -= parseFloat(oldOder);
                totalAmt += parseFloat(netAmount);
                totalDiscount += parseFloat(discountAmt);
                totalTax += parseFloat(taxAmount);
                totalOder += parseFloat(orderAmount);
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

function iSQuantityAvailablePO(txtBox) {
    var row = txtBox.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (row.cells[1].getElementsByTagName("input")[0].value != ""
        && row.cells[4].getElementsByTagName("input")[0].value != "") {
        var itemCode = row.cells[1].getElementsByTagName("input")[0].value;
        var itemArr = itemMasterQuantityPO[itemCode];
        if (typeof (itemArr) === "undefined") {
            var obj = {};
            obj.companyCode = $.trim($("#CompanyCode").val());
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PriceBookEdit.aspx/GetItemMasters",
                data: JSON.stringify(obj),
                dataType: "json",
                success: function (data) {
                    itemMasterQuantityPO = {};
                    $.each(data.d, function (i, j) {
                        itemMasterQuantityPO[j.code] = [j.Qnty];
                    });
                    itemArr = itemMasterQuantityPO[itemCode];
                    if (parseFloat(itemArr[0]) >= parseFloat(row.cells[4].getElementsByTagName("input")[0].value)) {
                        CalculatePOAmount(txtBox);
                    }
                    else {
                        row.cells[4].getElementsByTagName("input")[0].value = itemArr[0];
                        alert("Sorry,Avilable Items:" + itemArr[0]);
                    }
                },
                error: function (result) {
                    //alert("Error");
                }
            });

        }
        else {
            if (parseFloat(itemArr[0]) >= parseFloat(row.cells[4].getElementsByTagName("input")[0].value)) {
                CalculatePOAmount(txtBox);
            }
            else {
                row.cells[4].getElementsByTagName("input")[0].value = itemArr[0];
                alert("Sorry,Avilable Items:" + itemArr[0]);
            }
        }

    }
}

function SetSelectedRowPO(lnk, selectedItem) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var itemArr = itemMasterDetailsPO[selectedItem];
    row.cells[2].getElementsByTagName("input")[0].value = itemArr[0];
    row.cells[3].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[4];
    row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
    row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
    return false;
}

function SetSelectedRowPOByName(lnk, selectedItem) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var itemArr = itemMasterDetailsPOByName[selectedItem];
    row.cells[1].getElementsByTagName("input")[0].value = itemArr[0];
    row.cells[3].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[4];
    row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
    row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
    return false;
}


$(document).on("keydown", ".POItem", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayPO,
        select: function (event, ui) {
            SetSelectedRowPO(this, ui.item.label);

        }
    });
});
$(document).on("keydown", ".POName", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayPOByName,
        select: function (event, ui) {
            SetSelectedRowPOByName(this, ui.item.label);

        }
    });
});
$(document).on("keydown", ".POTaxPer", function (e) {
    $(this).autocomplete({
        source: itemTaxCodes,
        select: function (event, ui) {
            event.preventDefault();
            setSelectedTaxCode(this, ui.item.label);

        },
        change: function (event, ui) {
            if (ui.item === null || !ui.item)
                $(this).val(''); /* clear the value */
        }
    });
});
$(document).on("focusout", "#POQuantity", function (e) {
    iSQuantityAvailablePO(this);
});
$(document).on("focusout", "#PORate", function (e) {
    CalculatePOAmount(this);
});
$(document).on("focusout", "#PODiscPer", function (e) {
    CalculatePOAmount(this);
});
$(document).on("focusout", "#POTaxPer", function (e) {
    CalculatePOAmount(this);
});
$(document).on("focusout", "#PODiscAmt", function (e) {
    CalculatePOAmount(this);
});
//$(".MIItemRate").attr('readonly', 'readonly');
$("#POTotalAmount").attr('readonly', 'readonly');
$("#POTotalDiscountAmt").attr('readonly', 'readonly');
$("#POTotalTaxAmt").attr('readonly', 'readonly');
$("#POTotalOrderAmt").attr('readonly', 'readonly');
function CalculatePOAmount(txtBox) {
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
            if ($(txtBox).attr("id") == "PODiscAmt") {
                discountPer = (discountAmt / rowTotalRate).toFixed(2);
            }
            else {
                discountAmt = (rowTotalRate * discountPer).toFixed(2);
            }
            var taxAmount = ((rowTotalRate - discountAmt) * taxPer).toFixed(2);
            var netAmount = ((rowTotalRate - discountAmt) + parseFloat(taxAmount)).toFixed(2);
            //var orderAmount = ((qnty * rowTotalRate) - discountAmt) * taxAmount;
            var orderAmount = netAmount;
            if ($(txtBox).attr("id") == "PODiscAmt") {
                row.cells[6].getElementsByTagName("input")[0].value = discountPer;
            }
            else {
                row.cells[7].getElementsByTagName("input")[0].value = discountAmt;

            }
            row.cells[9].getElementsByTagName("input")[0].value = taxAmount;
            if (!POItemRowDetails[rowIndex] && row.cells[10].getElementsByTagName("input")[0].value == "") {

                row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                POItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                if ($("#POTotalAmount").val() == "") {
                    $("#POTotalAmount").val(netAmount);
                    $("#Amount").val(netAmount);
                }
                else {
                    $("#POTotalAmount").val(parseFloat($("#POTotalAmount").val()) + parseFloat(netAmount));
                    $("#Amount").val(parseFloat($("#Amount").val()) + parseFloat(netAmount));
                }
                if ($("#POTotalDiscountAmt").val() == "") {
                    $("#POTotalDiscountAmt").val(discountAmt);
                }
                else {
                    $("#POTotalDiscountAmt").val(parseFloat($("#POTotalDiscountAmt").val()) + parseFloat(discountAmt));
                }
                if ($("#POTotalTaxAmt").val() == "") {
                    $("#POTotalTaxAmt").val(taxAmount);
                }
                else {
                    $("#POTotalTaxAmt").val(parseFloat($("#POTotalTaxAmt").val()) + parseFloat(taxAmount));
                }
                if ($("#POTotalOrderAmt").val() == "") {
                    $("#POTotalOrderAmt").val(orderAmount);
                }
                else {
                    $("#POTotalOrderAmt").val(parseFloat($("#POTotalOrderAmt").val()) + parseFloat(orderAmount));
                }
            }
            else {
                var itemArray = POItemRowDetails[rowIndex];
                var totalAmt = parseFloat($("#POTotalAmount").val());
                var totalDiscount = parseFloat($("#POTotalDiscountAmt").val());
                var totalTax = parseFloat($("#POTotalTaxAmt").val());
                var totalOder = parseFloat($("#POTotalOrderAmt").val());
                var oldAmount = parseFloat(itemArray[6]);
                var oldDiscount = parseFloat(itemArray[3]);
                var oldTax = parseFloat(itemArray[5]);
                var oldOder = parseFloat(itemArray[7]);
                totalAmt -= parseFloat(oldAmount);
                totalDiscount -= parseFloat(oldDiscount);
                totalTax -= parseFloat(oldTax);
                totalOder -= parseFloat(oldOder);
                totalAmt += parseFloat(netAmount);
                totalDiscount += parseFloat(discountAmt);
                totalTax += parseFloat(taxAmount);
                totalOder += parseFloat(orderAmount);
                row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                $("#POTotalAmount").val(totalAmt);
                $("#Amount").val(totalAmt);
                $("#POTotalDiscountAmt").val(totalDiscount);
                $("#POTotalTaxAmt").val(totalTax);
                $("#POTotalOrderAmt").val(totalOder);
                POItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
            }


        }
    }

}

$(document).on("focusout", "#ReceivedQuantity", function (e) {
    var row = this.parentNode.parentNode;
    var qty = parseInt(row.cells[5].getElementsByTagName("input")[0].value);
    var preQnty = parseInt(row.cells[5].getElementsByTagName("input")[1].value);
    var currentTotal = parseInt($("#TotalQty").val());
    $("#TotalQty").val((currentTotal - preQnty) + qty);
});

function SetSelectedRowSalesReturn(lnk, selectedItem) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var itemArr = itemMasterDetailsSalesReturn[selectedItem];
    row.cells[2].getElementsByTagName("input")[0].value = itemArr[0];
    row.cells[3].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[4];
    row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
    row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
    return false;
}
function SetSelectedRowSalesReturnByName(lnk, selectedItem) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var itemArr = itemMasterDetailsSalesReturnByName[selectedItem];
    row.cells[1].getElementsByTagName("input")[0].value = itemArr[0];
    row.cells[3].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[4];
    row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
    row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
    return false;
}


$(document).on("keydown", ".SRItem", function (e) {
    $(this).autocomplete({
        source: itemMasterArraySalesReturn,
        select: function (event, ui) {
            SetSelectedRowSalesReturn(this, ui.item.label);

        }
    });
});
$(document).on("keydown", ".SRItemName", function (e) {
    $(this).autocomplete({
        source: itemMasterArraySalesReturnByName,
        select: function (event, ui) {
            SetSelectedRowSalesReturnByName(this, ui.item.label);

        }
    });
});
$(document).on("keydown", ".SRTaxPer", function (e) {
    $(this).autocomplete({
        source: itemTaxCodes,
        select: function (event, ui) {
            event.preventDefault();
            setSelectedTaxCode(this, ui.item.label);

        },
        change: function (event, ui) {
            if (ui.item === null || !ui.item)
                $(this).val(''); /* clear the value */
        }
    });
});
$(document).on("focusout", "#SRItemRate", function (e) {
    CalculateSRAmount(this);
});
$(document).on("focusout", "#SRDiscPer", function (e) {
    CalculateSRAmount(this);
});
$(document).on("focusout", "#SRTaxPer", function (e) {
    CalculateSRAmount(this);
});
$(document).on("focusout", "#SRDiscAmt", function (e) {
    CalculateSRAmount(this);
});
$(document).on("focusout", "#SRQuantity", function (e) {
    CalculateSRAmount(this);
});
function CalculateSRAmount(txtBox) {
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
            if ($(txtBox).attr("id") == "SRDiscAmt") {
                discountPer = (discountAmt / rowTotalRate).toFixed(2);
            }
            else {
                discountAmt = (rowTotalRate * discountPer).toFixed(2);
            }
            var taxAmount = ((rowTotalRate - discountAmt) * taxPer).toFixed(2);
            var netAmount = ((rowTotalRate - discountAmt) + parseFloat(taxAmount)).toFixed(2);
            //var orderAmount = ((qnty * rowTotalRate) - discountAmt) * taxAmount;
            var orderAmount = netAmount;
            if ($(txtBox).attr("id") == "SRDiscAmt") {
                row.cells[6].getElementsByTagName("input")[0].value = discountPer;

            }
            else {
                row.cells[7].getElementsByTagName("input")[0].value = discountAmt;
            }
            row.cells[9].getElementsByTagName("input")[0].value = taxAmount;
            if (!SRItemRowDetails[rowIndex] && row.cells[10].getElementsByTagName("input")[0].value == "") {
                row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                SRItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                if ($("#SRTotalAmount").val() == "") {
                    $("#SRTotalAmount").val(netAmount);
                }
                else {
                    $("#SRTotalAmount").val(parseFloat($("#SRTotalAmount").val()) + parseFloat(netAmount));
                }
                if ($("#Amount").val() == "") {
                    $("#Amount").val(netAmount);
                }
                else {
                    $("#Amount").val(parseFloat($("#Amount").val()) + parseFloat(netAmount));
                }
                if ($("#SRTotalDiscountAmt").val() == "") {
                    $("#SRTotalDiscountAmt").val(discountAmt);
                }
                else {
                    $("#SRTotalDiscountAmt").val(parseFloat($("#SRTotalDiscountAmt").val()) + parseFloat(discountAmt));
                }
                if ($("#SRTotalTaxAmt").val() == "") {
                    $("#SRTotalTaxAmt").val(taxAmount);
                }
                else {
                    $("#SRTotalTaxAmt").val(parseFloat($("#SRTotalTaxAmt").val()) + parseFloat(taxAmount));
                }
                if ($("#SRTotalOrderAmt").val() == "") {
                    $("#SRTotalOrderAmt").val(orderAmount);
                }
                else {
                    $("#SRTotalOrderAmt").val(parseFloat($("#SRTotalOrderAmt").val()) + parseFloat(orderAmount));
                }
            }
            else {
                var itemArray = SRItemRowDetails[rowIndex];
                var totalAmt = parseFloat($("#Amount").val());
                var totalDiscount = parseFloat($("#SRTotalDiscountAmt").val());
                var totalTax = parseFloat($("#SRTotalTaxAmt").val());
                var totalOder = parseFloat($("#SRTotalOrderAmt").val());
                var oldAmount = parseFloat(itemArray[6]);
                var oldDiscount = parseFloat(itemArray[3]);
                var oldTax = parseFloat(itemArray[5]);
                var oldOder = parseFloat(itemArray[7]);
                totalAmt -= parseFloat(oldAmount);
                totalDiscount -= parseFloat(oldDiscount);
                totalTax -= parseFloat(oldTax);
                totalOder -= parseFloat(oldOder);
                totalAmt += parseFloat(netAmount);
                totalDiscount += parseFloat(discountAmt);
                totalTax += parseFloat(taxAmount);
                totalOder += parseFloat(orderAmount);
                row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                $("#Amount").val(totalAmt);
                $("#SRTotalAmount").val(totalAmt);
                $("#SRTotalDiscountAmt").val(totalDiscount);
                $("#SRTotalTaxAmt").val(totalTax);
                $("#SRTotalOrderAmt").val(totalOder);
                SRItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
            }


        }
    }

}

$(document).on("click", ".ManualInvoiceBtnDetail", function (e) {
    if ($("#MIShipToAddress").val() == "") {
        $("#MIShipToAddress").addClass("errorValidation");
        return false;
    }
    else {
        $("#MIShipToAddress").removeClass("errorValidation");
        return true;
    }
});
$(document).on("click", ".SalesInvoiceBtnDetail", function (e) {
    if ($("#IShipToAddress").val() == "") {
        $("#IShipToAddress").addClass("errorValidation");
        return false;
    }
    else {
        $("#IShipToAddress").removeClass("errorValidation");
        return true;
    }
});
$(document).on("click", ".PurchaseOrderBtnDetail", function (e) {
    if ($("#ShipToAddress").val() == "") {
        $("#ShipToAddress").addClass("errorValidation");
        return false;
    }
    else {
        $("#ShipToAddress").removeClass("errorValidation");
        return true;
    }
});
function CreateNewRowSQ() {

    $("#rowCount").val(parseInt($("#rowCount").val()) + 1);
    var row = '<tr><td><span>' + $("#rowCount").val() + '</span></td><td><input name="SQItem" type="text" id="SQItem" class="form-control SQItem gridTxtBox required" style="width:70px;"></td><td><input name="SQName" type="text" id="SQName" class="form-control SQName gridTxtBox required"></td><td><input name="SQQuantity" type="text" id="SQQuantity" class="form-control SQQuantity gridTxtBox txtNumeric required"></td><td><input name="SQUnit" type="text" id="SQUnit" class="form-control SQUnit gridTxtBox required" readonly="readonly"></td><td><input name="SQRate" type="text" id="SQRate" class="form-control SQRate gridTxtBox txtNumeric required"></td><td><input name="SQDiscPer" type="text" id="SQDiscPer" class="form-control gridTxtBox SQDiscPer txtNumeric required"></td><td><input name="SQDiscAmt" type="text" id="SQDiscAmt" class="form-control SQDiscAmt gridTxtBox txtNumeric required"></td><td><input name="SQTaxPer" type="text" id="SQTaxPer" class="form-control SQTaxPer gridTxtBox required"><input type="hidden" name="SQTaxCode" id="SQTaxCode"></td><td><input name="SQTaxAmt" type="text" id="SQTaxAmt" class="form-control SQTaxAmt gridTxtBox required" readonly="readonly"></td><td><input name="SQNetAmt" type="text" id="SQNetAmt" class="form-control SQNetAmt gridTxtBox required" readonly="readonly" style="width:50px;"></td><td><a id="lnkDeleteSQ" style="cursor:pointer" >Delete</a><input type="hidden" name="ID" value="0" /></td></tr>';
    $("#SalesQuotationDetail tbody").append(row);
}
$(document).on("keydown", "#SQNetAmt", function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode == 9 && $("#SalesOrder").val()=="" ) {
        CreateNewRowSQ()
    }
});
function CreateNewRowPriceBook() {

    $("#rowCount").val(parseInt($("#rowCount").val()) + 1);
    var row = '<tr><td><span>' + $("#rowCount").val() + '</span></td><td><input name="ItemCode" type="text" id="ItemCode" class="form-control ItemCode required"></td><td><input name="Description" type="text" id="Description" class="form-control valid" aria-invalid="false"></td><td><input name="SupplierBarcode" type="text" id="SupplierBarcode" class="form-control SupplierBarcode required" readonly="readonly"></td><td><input name="CurrencyCode" type="text" id="CurrencyCode" class="form-control CurrencyCode required" readonly="readonly"></td><td><input name="MRP" type="text" id="MRP" class="form-control required"></td><td><input name="Price" type="text" id="Price" class="form-control priceBookPricetxt required"></td><td><a id="lnkDelete" style="cursor:pointer" >Delete</a><input type="hidden" name="ID" value="0" /></td></tr>';
    $("#PriceBookDetail tbody").append(row);
}
$(document).on("keydown", ".priceBookPricetxt", function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode == 9) {
        CreateNewRowPriceBook()
    }
});
function CreateNewRowStockEntry() {
    $("#rowCount").val(parseInt($("#rowCount").val()) + 1);
    var row = '<tr><td><span style="display:inline-block;width:50px;">' + $("#rowCount").val() + '</span></td><td><input name="Item" type="text" id="Item" class="form-control StockItem required" style="width:100px;" aria-required="true"></td><td><input name="Name" type="text" id="Name" class="form-control StockName required" style="width:100px;" aria-required="true"></td><td><input name="SERate" type="text" id="SERate" class="form-control StockRate txtNumeric required" style="width:100px;" aria-required="true"></td><td><input name="SEQuantity" type="text" id="SEQuantity" class="form-control StockQuantity txtNumeric required" style="width:100px;" aria-required="true"></td><td><input name="Unit" type="text" id="Unit" class="form-control StockUnit required" style="width:100px;" readonly="readonly" aria-required="true"></td><td><input name="SEAmount" type="text" id="SEAmount" class="form-control StockAmount required" style="width:100px;" readonly="readonly" aria-required="true"></td></tr>';
    $("#StockEntryDetail tbody").append(row);
}
$(document).on("keydown", "#SEAmount", function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode == 9 && $("#PageStatus").val() == "creating") {
        CreateNewRowStockEntry()
    }
});
function CreateNewRowInvoice() {
    $("#rowCount").val(parseInt($("#rowCount").val()) + 1);
    var row = '<tr class="Odd"><td><span>' + $("#rowCount").val() + '</span></td><td><input name="IItem" type="text" id="IItem" class="form-control IItem required" style="width:70px;" aria-required="true"></td><td><input name="IItemName" type="text" id="IItemName" class="form-control IItemName required" style="width:70px;" aria-required="true"></td><td><input name="IItemRate" type="text" id="IItemRate" class="form-control IItemRate txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="IQuantity" type="text" id="IQuantity" class="form-control IQuantity txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="IUnit" type="text" id="IUnit" class="form-control IUnit required" style="width:70px;" readonly="readonly" aria-required="true"></td><td><input name="IDiscPer" type="text" id="IDiscPer" class="form-control IDiscPer txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="IDiscAmt" type="text" id="IDiscAmt" class="form-control IDiscAmt txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="ITaxPer" type="text" id="ITaxPer" class="form-control ITaxPer required" style="width:70px;" aria-required="true"><input type="hidden" name="ITaxCode" id="ITaxCode"></td><td><input name="ITaxAmt" type="text" id="ITaxAmt" class="form-control ITaxAmt required" style="width:70px;" readonly="readonly" aria-required="true"></td><td><input name="INetAmt" type="text" id="INetAmt" class="form-control INetAmt required" style="width:50px;" readonly="readonly" aria-required="true"></td></tr>';
    $("#InvoiceDetail tbody").append(row);
}
$(document).on("keydown", "#INetAmt", function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode == 9 && $("#SalesInvoiceId").val() == "0") {
        CreateNewRowInvoice()
    }
});
function CreateNewRowManualInvoice() {
    $("#rowCount").val(parseInt($("#rowCount").val()) + 1);
    var row = '<tr><td><span>' + $("#rowCount").val() + '</span></td><td><input name="MIItem" type="text" id="MIItem" class="form-control MIItem required" style="width:70px;" aria-required="true"></td><td><input name="MIItemName" type="text" id="MIItemName" class="form-control MIItemName required" style="width:70px;" aria-required="true"></td><td><input name="MIItemRate" type="text" id="MIItemRate" class="form-control MIItemRate txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="MIQuantity" type="text" id="MIQuantity" class="form-control MIQuantity txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="MIUnit" type="text" id="MIUnit" class="form-control MIUnit required" style="width:70px;" readonly="readonly" aria-required="true"></td><td><input name="MIDiscPer" type="text" id="MIDiscPer" class="form-control txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="MIDiscAmt" type="text" id="MIDiscAmt" class="form-control MIDiscAmt txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="MITaxPer" type="text" id="MITaxPer" class="form-control MITaxPer txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="MITaxAmt" type="text" id="MITaxAmt" class="form-control MITaxAmt txtNumeric required" style="width:70px;" readonly="readonly" aria-required="true"></td><td><input name="MINetAmt" type="text" id="MINetAmt" class="form-control MINetAmt required" style="width:50px;" readonly="readonly" aria-required="true"></td></tr>';
    $("#ManualInvoiceDetail tbody").append(row);
}
$(document).on("keydown", "#MINetAmt", function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode == 9 && $("#InvoiceId").val() == "0") {
        CreateNewRowManualInvoice()
    }
});
function CreateNewRowPO() {
    $("#rowCount").val(parseInt($("#rowCount").val()) + 1);
    var row = '<tr class="Odd"><td><span>' + $("#rowCount").val() + '</span></td><td><input name="POItem" type="text" id="POItem" class="form-control POItem required" style="width:70px;" aria-required="true"></td><td><input name="POName" type="text" id="POName" class="form-control POName required" aria-required="true"></td><td><input name="PORate" type="text" id="PORate" class="form-control PORate txtNumeric required" aria-required="true"></td><td><input name="POQuantity" type="text" id="POQuantity" class="form-control POQuantity txtNumeric required" aria-required="true"></td><td><input name="POUnit" type="text" id="POUnit" readonly="readonly"  class="form-control POUnit required" aria-required="true"></td><td><input name="PODiscPer" type="text" id="PODiscPer" class="form-control PODiscPer txtNumeric required" aria-required="true"></td><td><input name="PODiscAmt" type="text" id="PODiscAmt" class="form-control PODiscAmt txtNumeric"></td><td><input name="POTaxPer" type="text" id="POTaxPer" class="form-control POTaxPer required" aria-required="true"><input type="hidden" name="POTaxCode" id="POTaxCode"></td><td> <input name="POTaxAmt" type="text" id="POTaxAmt" class="form-control POTaxAmt required" readonly="readonly"  aria-required="true"></td><td><input name="PONetAmt" type="text" readonly="readonly" id="PONetAmt" class="form-control PONetAmt required" style="width:50px;" aria-required="true"></td></tr>';
    $("#PurchaseOrderDetail tbody").append(row);
}
$(document).on("keydown", "#PONetAmt", function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode == 9 && $("#PageStatus").val() == "creating") {
        CreateNewRowPO()
    }
});
$(function () {
    if (parseInt($("#PriceBookId").val()) > 0) {
        GetPriceBook(parseInt(1),0);
    }
});
$(document).on("keyup", "#ItemSearch", function () {
    $("#ItemNameSearch").val('');
    $("#ItemSCSearch").val('');
    GetPriceBook(parseInt(1),1);
});
$(document).on("keyup", "#ItemNameSearch", function () {
    $("#ItemSearch").val('');
    $("#ItemSCSearch").val('');
    GetPriceBook(parseInt(1),2);
});
$(document).on("keyup", "#ItemSCSearch", function () {
    $("#ItemSearch").val('');
    $("#ItemNameSearch").val('');
    GetPriceBook(parseInt(1),3);
});
function SearchTerm(filterItem) {
    if (filterItem == 0)
    {
        return "";
    }
    else if (filterItem == 1) {
        return jQuery.trim($("#ItemSearch").val());
    }
    else if (filterItem == 2) {
        return jQuery.trim($("#ItemNameSearch").val());
    }
    else if (filterItem == 3) {
        return jQuery.trim($("#ItemSCSearch").val());
    }
};
function GetPriceBook(pageIndex,filterItem) {
    $.ajax({
        type: "POST",
        url: "PriceBookEdit.aspx/GetPriceBookDetails",
        data: '{searchTerm: "' + SearchTerm(filterItem) + '", pageIndex: ' + pageIndex + ', priceBookId: ' + parseInt($("#PriceBookId").val()) + ', filterItem: ' + filterItem + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });
}
var row;
function OnSuccess(response) {
    var xmlDoc = $.parseXML(response.d);
    var xml = $(xmlDoc);
    var priceBookDetails = xml.find("Table1");
    if (row == null) {
        row = $("#PriceBookDetail tr:last-child").clone(true);
    }
    $("#PriceBookDetail tr").not($("#PriceBookDetail tr:first-child")).remove();
    var index = 0;
    if (priceBookDetails.length > 0) {
        $("#rowCount").val('1');
        $("#DeletedRowIDs").val("");
        $.each(priceBookDetails, function () {
            var priceBookDetail = $(this);
            var row1;
            if ($("#rowCount").val() == "1")
            {
                row1 = '<tr><td><span>' + $("#rowCount").val() + '</span></td><td><input name="ItemCode" readonly="readonly" type="text" id="ItemCode" class="form-control ItemCode required" value=' + $(this).find("ItemCode").text() + '></td><td><input name="Description" type="text" readonly="readonly" id="Description" class="form-control valid" aria-invalid="false" value=' + $(this).find("Name").text() + '></td><td><input name="SupplierBarcode"  readonly="readonly" type="text" id="SupplierBarcode" class="form-control SupplierBarcode required" readonly="readonly" value=' + $(this).find("SupplierBarcode").text() + '></td><td><input name="CurrencyCode" type="text" readonly="readonly" id="CurrencyCode" class="form-control CurrencyCode required" readonly="readonly" value=' + $(this).find("CurrencyCode").text() + '></td><td><input name="MRP" type="text" id="MRP" class="form-control required" value=' + $(this).find("MRP").text() + '></td><td><input name="Price" type="text" id="Price" class="form-control priceBookPricetxt required" value=' + $(this).find("Price").text() + '></td><td><input type="hidden" name="ID" value="' + $(this).find("ID").text() + '" /></td></tr>';
                
            }
            else
            {
               row1 = '<tr><td><span>' + $("#rowCount").val() + '</span></td><td><input name="ItemCode" readonly="readonly" type="text" id="ItemCode" class="form-control ItemCode required" value=' + $(this).find("ItemCode").text() + '></td><td><input name="Description" type="text" readonly="readonly" id="Description" class="form-control valid" aria-invalid="false" value=' + $(this).find("Name").text() + '></td><td><input name="SupplierBarcode"  readonly="readonly" type="text" id="SupplierBarcode" class="form-control SupplierBarcode required" readonly="readonly" value=' + $(this).find("SupplierBarcode").text() + '></td><td><input name="CurrencyCode" type="text" readonly="readonly" id="CurrencyCode" class="form-control CurrencyCode required" readonly="readonly" value=' + $(this).find("CurrencyCode").text() + '></td><td><input name="MRP" type="text" id="MRP" class="form-control required" value=' + $(this).find("MRP").text() + '></td><td><input name="Price" type="text" id="Price" class="form-control priceBookPricetxt required" value=' + $(this).find("Price").text() + '></td><td><a id="lnkDelete" style="cursor:pointer" data-id="' + $(this).find("ID").text() + '" >Delete</a><input type="hidden" name="ID" value="' + $(this).find("ID").text() + '" /></td></tr>';
            }
            $("#PriceBookDetail tbody").append(row1);
            $("#rowCount").val(parseInt($("#rowCount").val()) + 1);
            row = $("#PriceBookDetail tr:last-child").clone(true);
        });
        var pager = xml.find("Pager");
        $(".Pager").ASPSnippets_Pager({
            ActiveCssClass: "current",
            PagerCssClass: "pager",
            PageIndex: parseInt(pager.find("PageIndex").text()),
            PageSize: parseInt(pager.find("PageSize").text()),
            RecordCount: parseInt(pager.find("RecordCount").text())
        });

        //$(".ContactName").each(function () {
        //    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
        //    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
        //});
    } else {
        var empty_row = row.clone(true);
        $("td:first-child", empty_row).attr("colspan", $("td", row).length);
        $("td:first-child", empty_row).attr("align", "center");
        $("td:first-child", empty_row).html("No records found for the search criteria.");
        $("td", empty_row).not($("td:first-child", empty_row)).remove();
        $("#PriceBookDetail").append(empty_row);
    }
};
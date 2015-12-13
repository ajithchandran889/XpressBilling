﻿var itemMasterArray = [];""
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
    if ($("#StokeEntryMstId").length > 0)
    {
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
                alert("Error");
            }
        });
    }
    if ($("#SalesQuotationId").length > 0)
    {
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
                itemMasterArraySQ = [];
                itemMasterDetailsSQ = {};
                itemMasterQuantitySQ = {};
                $.each(data.d, function (i, j) {
                    itemMasterArraySQ.push(j.code);
                    itemMasterDetailsSQ[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty];
                    itemMasterQuantitySQ[j.code] = [j.Qnty];
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
                itemMasterArraySQByName = [];
                itemMasterDetailsSQByName = {};
                itemMasterQuantitySQ = {};
                $.each(data.d, function (i, j) {
                    itemMasterArraySQByName.push(j.name);
                    itemMasterDetailsSQByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty];
                    itemMasterQuantitySQ[j.code] = [j.Qnty];
                });
            },
            error: function (result) {
                alert("Error");
            }
        });
    }
    if ($("#PurchaseOrderId").length > 0)
    {
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
                alert("Error");
            }
        });
    }
    if ($("#SalesReturnMstId").length > 0) {
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
                alert("Error");
            }
        });
    }
    if ($("#InvoiceId").length > 0)
    {
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
                alert("Error");
            }
        });
    }
    if($("#PriceBookId").length>0)
    {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PriceBookEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                itemMasterArray = [];
                itemMasterDetails = {};
                $.each(data.d, function (i, j) {
                    itemMasterArray.push(j.code);
                    itemMasterDetails[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice];
                });
            },
            error: function (result) {
                alert("Error");
            }
        });
    }
    if ($("#SalesInvoiceId").length > 0)
    {
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
                alert("Error");
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
    if ($("#StokeEntryMstId").val() != "" && $("#PageStatus").val() != "create") {
        itemMasterArrayStockEntry = [];
        itemMasterDetailsStockEntry = {};
        var i = 0;
        $("tr", $("#StockEntryDetail")).each(function () {
            var val = $("input[id*='Item']", $(this)).val();
            var qnty = parseInt($("input[id*='SEQuantity']", $(this)).val());
            var rate = parseFloat($("input[id*='SERate']", $(this)).val());
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
            if (typeof (val) !== "undefined") {
                var rowTotalRate = qnty * rate;
                var discountPer = (discountAmt / rowTotalRate).toFixed(2);
                var taxAmount = (rowTotalRate - discountAmt) * taxPer;
                var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                var orderAmount = netAmount;
                SRItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                i++;
            }

        }); 
        if ($("#Status").val() == "2")
        {
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
        else
        {
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
    if (($("#InvoiceId").val() != "" || $("#InvoiceId").val() != "0") && $("#PageStatus").val() != "create") {
        itemMasterArrayManualInvoice = []; 
        itemMasterDetailsManualInvoice = {};
        var i = 0;
        $("tr", $("#ManualInvoiceDetail")).each(function () {
            
            var val = $("input[id*='MIItem']", $(this)).val();
            var qnty = parseInt($("input[id*='MIQuantity']", $(this)).val());
            var rate = parseFloat($("input[id*='MIItemRate']", $(this)).val());
            var discountAmt = parseFloat($("input[id*='MIDiscAmt']", $(this)).val());
            var taxPer = parseFloat($("input[id*='MITaxPer']", $(this)).val());
            if (typeof (val) !== "undefined") {
                var rowTotalRate = qnty * rate;
                var discountPer = (discountAmt / rowTotalRate).toFixed(2);
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
            var rate = parseFloat($("input[id*='IItemRate']", $(this)).val());
            var discountAmt = parseFloat($("input[id*='IDiscAmt']", $(this)).val());
            var taxPer = parseFloat($("input[id*='ITaxPer']", $(this)).val());
            if (typeof (val) !== "undefined") {
                var rowTotalRate = qnty * rate;
                var discountPer = (discountAmt / rowTotalRate).toFixed(2);
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
            var rate = parseFloat($("input[id*='SQRate']", $(this)).val());
            var discountAmt = parseFloat($("input[id*='SQDiscAmt']", $(this)).val());
            var taxPer = parseFloat($("input[id*='SQTaxPer']", $(this)).val());
            if (typeof (val) !== "undefined") {
                var rowTotalRate = qnty * rate;
                var discountPer = (discountAmt / rowTotalRate).toFixed(2);
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
            if (typeof (val) !== "undefined") {
                var rowTotalRate = qnty * rate;
                var discountPer = (discountAmt / rowTotalRate).toFixed(2);
                var taxAmount = (rowTotalRate - discountAmt) * taxPer;
                var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                var orderAmount = netAmount;
                POItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                i++;
            }

        });
        
        $(".POTaxAmt").attr('readonly', 'readonly');
        $(".PONetAmt").attr('readonly', 'readonly');
        $(".POUnit").attr('readonly', 'readonly');
        $(".POItem").attr('readonly', 'readonly');
        $(".POName").attr('readonly', 'readonly');
    }
    else if ($("#PageStatus").val() == "create") {
        $(".POTaxAmt").attr('readonly', 'readonly');
        $(".PONetAmt").attr('readonly', 'readonly');
        $(".POUnit").attr('readonly', 'readonly');
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
    $(document).on("keydown", ".ItemCode", function (e) {
        $(this).autocomplete({
            source: itemMasterArray,
            select: function (event, ui) {
                SetSelectedRow(this, ui.item.label);

            }
        });
    });
    $(document).on("keydown", "#SalesMan", function (e) {
        var obj = {};
        obj.companyCode = $.trim($("#CompanyCode").val());
        var contactArray = [];
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SalesQuotationEdit.aspx/GetContactCodes",
            data: JSON.stringify(obj),
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
                if ($(txtBox).attr("id") == "MIDiscAmt")
                {
                    discountPer = (discountAmt / rowTotalRate).toFixed(2);
                }
                else
                {
                    discountAmt = (rowTotalRate * discountPer).toFixed(2);
                }
                var taxAmount = ((rowTotalRate - discountAmt) * taxPer).toFixed(2);
                var netAmount = ((rowTotalRate - discountAmt) + parseFloat(taxAmount)).toFixed(2);
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
            && row.cells[4].getElementsByTagName("input")[0].value!="") {
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
                if (ui.item === null || !ui.item)
                    $(this).val(''); /* clear the value */
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
                if ($(txtBox).attr("id") == "IDiscAmt")
                {
                    discountPer = (discountAmt / rowTotalRate).toFixed(2);
                }
                else {
                    discountAmt = (rowTotalRate * discountPer).toFixed(2);
                }
                var taxAmount = ((rowTotalRate - discountAmt) * taxPer).toFixed(2);
                var netAmount = ((rowTotalRate - discountAmt) + parseFloat(taxAmount)).toFixed(2);
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


    $(document).on("keydown", ".SQItem", function (e) {
        $(this).autocomplete({
            source: itemMasterArraySQ,
            select: function (event, ui) {
                SetSelectedRowSQ(this, ui.item.label);

            }
        });
        //$(this).autocomplete(optionsSQ);
    });
    $(document).on("keydown", ".SQName", function (e) {
        $(this).autocomplete({
            source: itemMasterArraySQByName,
            select: function (event, ui) {
                SetSelectedRowSQByName(this, ui.item.label);

            }
        });
        //$(this).autocomplete(optionsSQByName);
    });
    $(document).on("keydown", ".SQTaxPer", function (e) {
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
                    discountPer = (discountAmt / rowTotalRate).toFixed(2);
                }
                else {
                    discountAmt = (rowTotalRate * discountPer).toFixed(2);
                }
                var taxAmount = ((rowTotalRate - discountAmt) * taxPer).toFixed(2);
                var netAmount = ((rowTotalRate - discountAmt) + parseFloat(taxAmount)).toFixed(2);
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
                        alert("Error");
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
                var netAmount = ((rowTotalRate - discountAmt) +parseFloat(taxAmount)).toFixed(2);
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
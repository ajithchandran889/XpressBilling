var loading = $("#sl-loadingscreen");
var minDelay = 500;
var start = new Date();
var loaderTime;
$(document).ajaxStart(function () {
    loaderTime = setTimeout(function () { showLoader(); }, 1000);
});
function showLoader() {

    minDelay = 500;
    start = new Date();
    loading.fadeIn("slow");
}
$(document).ajaxStop(function () {
    var end = new Date();
    var timeInMilliseconds = (end - start);
    if (timeInMilliseconds < minDelay) {
        setTimeout(function () { callback(); }, minDelay - timeInMilliseconds);
    }
    else callback();
});
function callback() {
    clearTimeout(loaderTime);
    loading.fadeOut("slow");
}
var opt = {
    autoOpen: false,
    modal: true,
    width: 550,
    title: 'Alert',
    resizable: false,
    buttons: {
        "OK": function () {
            $(this).dialog("close");
        }
    }
};
var itemMasterSelectedItem = [];
var itemMasterArray = [];
//var itemMasterArrayOriginal = []; ""
var itemMasterDetails = {};
var itemMasterArrayByName = []; 
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
var itemMasterQuantitySE = {};
var SQItemRowDetails = [];
var itemTaxCodes = [];
var itemTaxCodevalues = [];
var itemTaxDetails = {};
var MIItemRowDetails = [];
var IItemRowDetails = [];
var itemMasterQuantityI = {};
var itemMasterQuantityPO = {};
var itemMasterQuantitySR = {};
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
var itemDetailsStockRegCodeArray = [];
var itemDetailsStockRegNameArray = [];
var itemDetailsStockRegCode = {};
var itemDetailsStockRegName = {};
var salesOrderDetails = {};
var salesOrderNos = {};
var firstFreeSQ = [];
var firstFreeSI = [];
var firstFreeSE = [];
var firstFreeMI = [];
var firstFreePO = [];
var firstFreeGrn = [];
var firstFreeSR = [];
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
    if ($("#LocationSR").length > 0) {
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
    if ($("#StockRegisterPage").length > 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "StockRegister.aspx/GetItemMasters",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                itemDetailsStockRegCodeArray = [];
                itemDetailsStockRegNameArray = [];
                itemDetailsStockRegCode = {};
                itemDetailsStockRegName = {};
                $.each(data.d, function (i, j) {
                    itemDetailsStockRegCodeArray.push(j.itemCode);
                    itemDetailsStockRegNameArray.push(j.itemName);
                    itemDetailsStockRegCode[j.itemCode] = [j.itemName, j.itemType];
                    itemDetailsStockRegName[j.itemName] = [j.itemCode, j.itemType];

                });
            },
            error: function (result) {
                //alert("Error");
            }
        });
    }
    if ($("#StokeEntryMstId").length > 0) {

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "StockEntryEdit.aspx/GetFirstFreerDetails",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                firstFreeSE = [];
                $.each(data.d, function (i, j) {
                    firstFreeSE.push({
                        id: j.id,
                        sequenceNumber: j.sequenceNumber,
                        seqType: j.seqType,
                        orderType: j.orderType,
                        enterpriseUnitCode: j.enterpriseUnitCode
                    });

                });
                if (firstFreeSE.length == 0) {
                    $("#alertMessage").text("Please create first free number for Sales Quotation");
                    var theDialog = $("#dialog-alert").dialog(opt);
                    theDialog.dialog("open");
                    $("#SaveBtn").attr("disabled", true);
                }
            },
            error: function (result) {
                // alert("Error");
            }
        });

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "StockEntryEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                itemMasterArrayStockEntry = [];
                itemMasterDetailsStockEntry = {};
                itemMasterQuantitySE = {};
                itemMasterArrayStockEntryByName = [];
                itemMasterDetailsStockEntryByName = {};
                $.each(data.d, function (i, j) {
                    itemMasterArrayStockEntry.push(j.code);
                    itemMasterDetailsStockEntry[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.currencyCode, j.decimalPoint, j.itemType];
                    itemMasterArrayStockEntryByName.push(j.name);
                    itemMasterDetailsStockEntryByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.currencyCode, j.decimalPoint, j.itemType];
                    itemMasterQuantitySE[j.code] = [j.Qnty];
                });
            },
            error: function (result) {
                // alert("Error");
            }
        });
    }
    if ($("#GRNId").length > 0) {

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "GrnEdit.aspx/GetFirstFreerDetails",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                firstFreeGrn = [];
                $.each(data.d, function (i, j) {
                    firstFreeGrn.push({
                        id: j.id,
                        sequenceNumber: j.sequenceNumber,
                        seqType: j.seqType,
                        orderType: j.orderType,
                        enterpriseUnitCode: j.enterpriseUnitCode
                    });

                });
                if (firstFreeGrn.length == 0) {
                    $("#alertMessage").text("Please create first free number for goods receipt");
                    var theDialog = $("#dialog-alert").dialog(opt);
                    theDialog.dialog("open");
                    $("#btnSaveDtl").attr("disabled", true);
                }
            },
            error: function (result) {
                // alert("Error");
            }
        });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PurchaseOrderEdit.aspx/GetPurchaseCustomerDetails",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                customerCodes = [];
                customerCodesWithDetails = {};
                $.each(data.d, function (i, j) {
                    customerCodes.push(j.name);
                    customerCodesWithDetails[j.name] = [j.name, j.telephone, j.orderType, j.code];
                });
            },
            error: function (result) {
                //alert("Error");
            }
        });
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
                        enterpriseUnitCode: j.enterpriseUnitCode
                    });

                });
                if (firstFreeSQ.length == 0) {
                    $("#alertMessage").text("Please create first free number for Sales Quotation");
                    var theDialog = $("#dialog-alert").dialog(opt);
                    theDialog.dialog("open");
                    $("#SaveBtn").attr("disabled", true);
                }
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
                    customerCodes.push(j.name);
                    customerCodesWithDetails[j.name] = [j.name, j.telephone, j.orderType, j.code];
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

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PurchaseOrderEdit.aspx/GetFirstFreerDetails",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                firstFreePO = [];
                $.each(data.d, function (i, j) {
                    firstFreePO.push({
                        id: j.id,
                        sequenceNumber: j.sequenceNumber,
                        seqType: j.seqType,
                        orderType: j.orderType,
                        enterpriseUnitCode: j.enterpriseUnitCode
                    });

                });
                if (firstFreePO.length == 0) {
                    $("#alertMessage").text("Please create first free number for Purchase Order");
                    var theDialog = $("#dialog-alert").dialog(opt);
                    theDialog.dialog("open");
                    $("#btnSaveDtl").attr("disabled", true);
                }
            },
            error: function (result) {
                // alert("Error");
            }
        });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "PurchaseOrderEdit.aspx/GetPurchaseCustomerDetails",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                customerCodes = [];
                customerCodesWithDetails = {};
                $.each(data.d, function (i, j) {
                    customerCodes.push(j.name);
                    customerCodesWithDetails[j.name] = [j.name, j.telephone, j.orderType,j.code];
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
        if ($("#PageStatus").val() == "create") {
            $(".POTaxAmt").attr('readonly', 'readonly');
            $(".PONetAmt").attr('readonly', 'readonly');
            $(".POUnit").attr('readonly', 'readonly');
        }
    }
    if ($("#SalesReturnMstId").length > 0) {
        if ($("#SalesReturnType").val() == "0") {
            $("#Location").attr("readonly", "readonly");
            $("#SalesMan").attr("readonly", "readonly");
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
        var i = 0;
        if ($("#PageStatus").val() == "create") {
            var obj1 = {};
            obj1.companyCode = $.trim($("#CompanyCode").val());
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "SalesReturnEdit.aspx/GetItemMasters",
                data: JSON.stringify(obj1),
                dataType: "json",
                success: function (data) {
                    itemMasterArraySalesReturn = [];
                    itemMasterDetailsSalesReturn = {};
                    itemMasterQuantitySR = {};
                    itemMasterArraySalesReturnByName = [];
                    itemMasterDetailsSalesReturnByName = {};
                    $.each(data.d, function (i, j) {
                        itemMasterArraySalesReturn.push(j.code);
                        itemMasterDetailsSalesReturn[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];
                        itemMasterQuantitySR[j.code] = [j.Qnty];
                        itemMasterArraySalesReturnByName.push(j.name);
                        itemMasterDetailsSalesReturnByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];

                    });
                    var i = 0;
                    $("tr", $("#SalesReturnDetail")).each(function () {

                        var val = $("input[id*='SRItem']", $(this)).val();
                        var qnty = parseInt($("input[id*='SRQuantity']", $(this)).val());
                        var rate = parseFloat($("input[id*='SRItemRate']", $(this)).val());
                        var discountAmt = parseFloat($("input[id*='SRDiscAmt']", $(this)).val());
                        var taxPer = parseFloat($("input[id*='SRTaxPer']", $(this)).val());
                        if (typeof (val) !== "undefined" && val != "" && val != "undefined") {
                            var itemArr = itemMasterDetailsSalesReturn[$("input[id*='SRItem']", $(this)).val()];
                            var rowTotalRate = qnty * rate;
                            var discountPer = 0;
                            if (rowTotalRate != 0) {
                                discountPer = ((discountAmt / rowTotalRate) * 100).toFixed(itemArr[9]);
                            }
                            var taxAmount = ((rowTotalRate - discountAmt) / 100) * taxPer;
                            var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                            var orderAmount = netAmount;
                            SRItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, rowTotalRate, orderAmount];
                            i++;
                        }

                    });
                },
                error: function (result) {
                    //alert("Error");
                }
            });
        }

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
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SalesReturnEdit.aspx/GetFirstFreerDetails",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                firstFreeSR = [];
                $.each(data.d, function (i, j) {
                    firstFreeSR.push({
                        id: j.id,
                        sequenceNumber: j.sequenceNumber,
                        seqType: j.seqType,
                        orderType: j.orderType,
                        enterpriseUnitCode: j.enterpriseUnitCode
                    });

                });
                if (firstFreeSR.length == 0) {
                    $("#alertMessage").text("Please create first free number for Sales Return");
                    var theDialog = $("#dialog-alert").dialog(opt);
                    theDialog.dialog("open");
                    $("#SaveBtn").attr("disabled", true);
                }
                //else
                //{
                //    $.each(firstFreeSR, function (i, j) {
                //        if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == $("#SalesReturnType").val()) {
                //            $("#SalesReturn").val(j.sequenceNumber);
                //            $("#SRSequenceNoID").val(j.id);
                //        }
                //        else if (j.seqType == "0" && j.orderType == $("#SalesReturnType").val()) {
                //            $("#SalesReturn").val(j.sequenceNumber);
                //            $("#SRSequenceNoID").val(j.id);
                //        }
                //    });
                //}
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
                    customerCodes.push(j.name);
                    customerCodesWithDetails[j.name] = [j.name, j.telephone, j.orderType,j.code];
                });
            },
            error: function (result) {
                //alert("Error");
            }
        });

    }
    if ($("#InvoiceId").length > 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "ManualInvoiceEdit.aspx/GetFirstFreerDetails",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                firstFreeMI = [];
                $.each(data.d, function (i, j) {
                    firstFreeMI.push({
                        id: j.id,
                        sequenceNumber: j.sequenceNumber,
                        seqType: j.seqType,
                        orderType: j.orderType,
                        enterpriseUnitCode: j.enterpriseUnitCode
                    });

                });
                if (firstFreeMI.length == 0) {
                    $("#alertMessage").text("Please create first free number for Manual Invoice");
                    var theDialog = $("#dialog-alert").dialog(opt);
                    theDialog.dialog("open");
                    $("#btnSaveDtl").attr("disabled", true);
                }
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
                    customerCodes.push(j.name);
                    customerCodesWithDetails[j.name] = [j.name, j.telephone, j.orderType, j.code];
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
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "InvoiceEdit.aspx/GetFirstFreerDetails",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (data) {
                firstFreeSI = [];
                $.each(data.d, function (i, j) {
                    firstFreeSI.push({
                        id: j.id,
                        sequenceNumber: j.sequenceNumber,
                        seqType: j.seqType,
                        orderType: j.orderType,
                        enterpriseUnitCode: j.enterpriseUnitCode
                    });

                });
                if (firstFreeSI.length == 0) {
                    $("#alertMessage").text("Please create first free number for Sales Quotation");
                    var theDialog = $("#dialog-alert").dialog(opt);
                    theDialog.dialog("open");
                    $("#btnSaveDtl").attr("disabled", true);
                }
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
                    customerCodes.push(j.name);
                    customerCodesWithDetails[j.name] = [j.name, j.telephone, j.orderType, j.code];
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
    if ($("#StokeEntryMstId").length > 0 && $("#PageStatus").val() == "create") {
        $(".StockUnit").attr('readonly', 'readonly');
        $(".StockAmount").attr('readonly', 'readonly');
    }
    else if ($("#StokeEntryMstId").length > 0 && $("#PageStatus").val() == "edit") {
        if ($("#Status").val() == "2") {
            $(".StockItem").attr('readonly', 'readonly');
            $(".StockName").attr('readonly', 'readonly');
            $(".StockQuantity").attr('readonly', 'readonly');
            $(".StockUnit").attr('readonly', 'readonly');
            $(".StockRate").attr('readonly', 'readonly');
            $(".StockAmount").attr('readonly', 'readonly');
        }
        else if ($("#Status").val() == "1") {
            $(".StockItem").attr('readonly', 'readonly');
            $(".StockName").attr('readonly', 'readonly');
            $(".StockUnit").attr('readonly', 'readonly');
            $(".StockAmount").attr('readonly', 'readonly');
        }
    }
    if (($("#SalesReturnMstId").val() != "" || $("#SalesReturnMstId").val() != "0") && $("#PageStatus").val() != "create") {
        var obj1 = {};
        obj1.companyCode = $.trim($("#CompanyCode").val());
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SalesReturnEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj1),
            dataType: "json",
            success: function (data) {
                itemMasterArraySalesReturn = [];
                itemMasterDetailsSalesReturn = {};
                itemMasterQuantitySR = {};
                itemMasterArraySalesReturnByName = [];
                itemMasterDetailsSalesReturnByName = {};
                $.each(data.d, function (i, j) {
                    itemMasterArraySalesReturn.push(j.code);
                    itemMasterDetailsSalesReturn[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];
                    itemMasterQuantitySR[j.code] = [j.Qnty];
                    itemMasterArraySalesReturnByName.push(j.name);
                    itemMasterDetailsSalesReturnByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];

                });
                var i = 0;
                $("tr", $("#SalesReturnDetail")).each(function () {

                    var val = $("input[id*='SRItem']", $(this)).val();
                    var qnty = parseInt($("input[id*='SRQuantity']", $(this)).val());
                    var rate = parseFloat($("input[id*='SRItemRate']", $(this)).val());
                    var discountAmt = parseFloat($("input[id*='SRDiscAmt']", $(this)).val());
                    var taxPer = parseFloat($("input[id*='SRTaxPer']", $(this)).val());
                    if (typeof (val) !== "undefined" && val != "" && val != "undefined") {
                        var itemArr = itemMasterDetailsSalesReturn[$("input[id*='SRItem']", $(this)).val()];
                        var rowTotalRate = qnty * rate;
                        var discountPer = 0;
                        if (rowTotalRate != 0) {
                            discountPer = ((discountAmt / rowTotalRate) * 100).toFixed(itemArr[9]);
                        }
                        var taxAmount = ((rowTotalRate - discountAmt) / 100) * taxPer;
                        var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                        var orderAmount = netAmount;
                        SRItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, rowTotalRate, orderAmount];
                        i++;
                    }

                });
            },
            error: function (result) {
                //alert("Error");
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
        var obj1 = {};
        obj1.companyCode = $.trim($("#CompanyCode").val());
        obj1.orderType = $("#InvoiceType").val();
        obj1.priceType = 0;
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SQEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj1),
            dataType: "json",
            success: function (data) {
                itemMasterArrayManualInvoice = [];
                itemMasterDetailsManualInvoice = {};
                itemMasterArrayManualInvoiceByName = [];
                itemMasterDetailsManualInvoiceByName = {};
                $.each(data.d, function (i, j) {
                    itemMasterArrayManualInvoice.push(j.code);
                    itemMasterDetailsManualInvoice[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];
                    itemMasterArrayManualInvoiceByName.push(j.name);
                    itemMasterDetailsManualInvoiceByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];

                });
                var i = 0;
                $("tr", $("#ManualInvoiceDetail")).each(function () {

                    var val = $("input[id*='MIItem']", $(this)).val();
                    var qnty = parseInt($("input[id*='MIQuantity']", $(this)).val());
                    var rate = parseFloat($("input[id*='MIItemRate']", $(this)).val());
                    var discountAmt = parseFloat($("input[id*='MIDiscAmt']", $(this)).val());
                    var taxPer = parseFloat($("input[id*='MITaxPer']", $(this)).val());
                    if (typeof (val) !== "undefined" && val != "" && val != "undefined") {
                        var itemArr = itemMasterDetailsManualInvoice[$("input[id*='MIItem']", $(this)).val()];
                        var rowTotalRate = qnty * rate;
                        var discountPer = 0;
                        if (rowTotalRate != 0) {
                            discountPer = ((discountAmt / rowTotalRate) * 100).toFixed(itemArr[9]);
                        }
                        var taxAmount = ((rowTotalRate - discountAmt) / 100) * taxPer;
                        var netAmount = rowTotalRate
                        var orderAmount = (rowTotalRate - discountAmt) + taxAmount;
                        MIItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                        i++;
                    }

                });
            },
            error: function (result) {
                //alert("Error");
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
        var obj1 = {};
        obj1.companyCode = $.trim($("#CompanyCode").val());
        obj1.orderType = $("#InvoiceType").val();
        obj1.priceType = 0;
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SQEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj1),
            dataType: "json",
            success: function (data) {
                itemMasterArrayInvoice = [];
                itemMasterDetailsInvoice = {};
                itemMasterQuantityI = {};
                itemMasterArrayInvoiceByName = [];
                itemMasterDetailsInvoiceByName = {};
                $.each(data.d, function (i, j) {
                    itemMasterArrayInvoice.push(j.code);
                    itemMasterDetailsInvoice[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];
                    itemMasterQuantityI[j.code] = [j.Qnty];
                    itemMasterArrayInvoiceByName.push(j.name);
                    itemMasterDetailsInvoiceByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];

                });
                var i = 0;
                $("tr", $("#InvoiceDetail")).each(function () {

                    var val = $("input[id*='IItem']", $(this)).val();
                    var qnty = parseInt($("input[id*='IQuantity']", $(this)).val());
                    var rate = parseFloat($("input[id*='IItemRate']", $(this)).val());
                    var discountAmt = parseFloat($("input[id*='IDiscAmt']", $(this)).val());
                    var taxPer = parseFloat($("input[id*='ITaxPer']", $(this)).val());
                    if (typeof (val) !== "undefined" && val != "" && val != "undefined") {
                        var itemArr = itemMasterDetailsInvoice[$("input[id*='IItem']", $(this)).val()];
                        var rowTotalRate = qnty * rate;
                        var discountPer = 0;
                        if (rowTotalRate != 0) {
                            discountPer = ((discountAmt / rowTotalRate) * 100).toFixed(itemArr[9]);
                        }

                        var taxAmount = ((rowTotalRate - discountAmt) / 100) * taxPer;
                        var netAmount = rowTotalRate;
                        var orderAmount = (rowTotalRate - discountAmt) + taxAmount;
                        IItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, netAmount, orderAmount];
                        i++;
                    }

                });
            },
            error: function (result) {
                //alert("Error");
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
                var i = 0;
                $("tr", $("#SalesQuotationDetail")).each(function () {

                    var val = $("input[id*='SQItem']", $(this)).val();
                    var qnty = parseInt($("input[id*='SQQuantity']", $(this)).val());
                    var rate = parseFloat($("input[id*='SQRate']", $(this)).val());
                    var discountAmt = parseFloat($("input[id*='SQDiscAmt']", $(this)).val());
                    var taxPer = parseFloat($("input[id*='SQTaxPer']", $(this)).val());
                    if (typeof (val) !== "undefined" && val != "" && val != "undefined") {
                        var itemArr = itemMasterDetailsSQ[$("input[id*='SQItem']", $(this)).val()];
                        var rowTotalRate = qnty * rate;
                        var discountPer = 0;
                        if (rowTotalRate != 0) {
                            discountPer = ((discountAmt / rowTotalRate) * 100).toFixed(itemArr[9]);
                        }
                        var taxAmount = ((rowTotalRate - discountAmt) / 100) * taxPer;
                        var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                        var orderAmount = netAmount;
                        SQItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, rowTotalRate, orderAmount];
                        i++;
                    }

                });
            },
            error: function (result) {
                //alert("Error");
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

        var obj1 = {};
        obj1.companyCode = $.trim($("#CompanyCode").val());
        obj1.orderType = $("#OrderType").val();
        obj1.priceType = 1;
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SQEdit.aspx/GetItemMasters",
            data: JSON.stringify(obj1),
            dataType: "json",
            success: function (data) {
                itemMasterArrayPO = [];
                itemMasterDetailsPO = {};
                itemMasterQuantityPO = {};
                itemMasterArrayPOByName = [];
                itemMasterDetailsPOByName = {};
                $.each(data.d, function (i, j) {
                    itemMasterArrayPO.push(j.code);
                    itemMasterDetailsPO[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint];
                    itemMasterQuantityPO[j.code] = [j.Qnty];
                    itemMasterArrayPOByName.push(j.name);
                    itemMasterDetailsPOByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint];

                });
                var i = 0;
                $("tr", $("#PurchaseOrderDetail")).each(function () {

                    var val = $("input[id*='POItem']", $(this)).val();
                    var qnty = parseInt($("input[id*='POQuantity']", $(this)).val());
                    var rate = parseFloat($("input[id*='PORate']", $(this)).val());
                    var discountAmt = parseFloat($("input[id*='PODiscAmt']", $(this)).val());
                    var taxPer = parseFloat($("input[id*='POTaxPer']", $(this)).val());
                    if (typeof (val) !== "undefined" && val != "" && val != "undefined") {
                        var itemArr = itemMasterDetailsPO[$("input[id*='POItem']", $(this)).val()];
                        var rowTotalRate = qnty * rate;
                        var discountPer = 0;
                        if (rowTotalRate != 0) {
                            discountPer = ((discountAmt / rowTotalRate) * 100).toFixed(itemArr[9]);
                        }
                        var taxAmount = ((rowTotalRate - discountAmt) / 100) * taxPer;
                        var netAmount = (rowTotalRate - discountAmt) + taxAmount;
                        var orderAmount = netAmount;
                        POItemRowDetails[i] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, rowTotalRate, orderAmount];
                        i++;
                    }

                });
            },
            error: function (result) {
                //alert("Error");
            }
        });
        if ($("#IsFinalized").val() == "1") {
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
        else if ($("#Status").val() == "1") {
            $(".POTaxAmt").attr('readonly', 'readonly');
            $(".PONetAmt").attr('readonly', 'readonly');
            $(".POUnit").attr('readonly', 'readonly');
            $(".POItem").attr('readonly', 'readonly');
            $(".POName").attr('readonly', 'readonly');
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

    //$(document).on("change", "#BusinessPartnerType", function () {

    //    if ($("#BusinessPartnerType").val() == "0") {
    //        alert("ok1");
    //        $("#OrderType_0").show();
    //        $("#OrderType_1").hide();
    //    }
    //    else if ($("#BusinessPartnerType").val() == "1") {
    //        alert("ok2");
    //        $("#OrderType_0").hide();
    //        $("#OrderType_1").show();
    //    }
    //});

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
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[4].getElementsByTagName("input")[0].value = itemArr[4];

    return false;
}

function SetSelectedRowStockEntryByName(lnk, selectedItem) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;

    var itemArr = itemMasterDetailsStockEntryByName[selectedItem];
    row.cells[1].getElementsByTagName("input")[0].value = itemArr[0];
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[4].getElementsByTagName("input")[0].value = itemArr[4];

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

function CheckItemAlreadyAdded(val) {
    var flag = false;
    $("tr", $("#PriceBookDetail")).each(function () {
        if (val == $("input[id*='ItemCode']", $(this)).val()) {
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
            else {
                $("#alertMessage").text("Item Already added");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
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
                $("#alertMessage").text("Item Already added");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
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
        $("#DeletedRowIDs").val($("#DeletedRowIDs").val() + $(this).attr('data-id') + ",");
    var row = this.parentNode.parentNode;
    row.remove();
    var index = 1;
    $("td:first-child", $("#PriceBookDetail")).each(function () {
        $(this).find("span:first").text(index);
        index++;

    });
    var newIndex = --index;
    if (newIndex == 1) {
        $("tr", $("#PriceBookDetail")).each(function () {
            var val = $("input[id*='ItemCode']", $(this)).val();
            if (typeof (val) !== "undefined" && val != "undefined") {
                $("a[id*='lnkDelete']", $(this)).css("display", "none");
            }


        });
    }
    $("#rowCount").val(newIndex);
    return false;
});
$(document).on("click", "#lnkDeleteSQ", function (e) {
    if (typeof $(this).attr('data-id') !== "undefined")
        $("#DeletedRowIDs").val($("#DeletedRowIDs").val() + $(this).attr('data-id') + ",");
    var row = this.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (SQItemRowDetails[rowIndex]) {
        var itemArr = itemMasterDetailsSQ[row.cells[1].getElementsByTagName("input")[0].value];
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
        $("#TotalAmount").val(totalAmt.toFixed(itemArr[9]));
        $("#TotalDiscountAmt").val(totalDiscount.toFixed(itemArr[9]));
        $("#TotalTaxAmt").val(totalTax.toFixed(itemArr[9]));
        $("#TotalOrderAmt").val(totalOder.toFixed(itemArr[9]));
        var i;
        for (i = rowIndex; i < SQItemRowDetails.length; i++) {
            if (SQItemRowDetails[i + 1]) {
                SQItemRowDetails[i] = SQItemRowDetails[i + 1];
            }
            else {
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
    var newIndex = --index;
    if (newIndex == 1) {
        $("tr", $("#SalesQuotationDetail")).each(function () {
            var val = $("input[id*='SQItem']", $(this)).val();
            if (typeof (val) !== "undefined" && val != "undefined") {
                $("a[id*='lnkDeleteSQ']", $(this)).css("display", "none");
            }


        });
    }
    $("#rowCount").val(newIndex);
    return false;
});
$(document).on("click", "#lnkDeleteSE", function (e) {
    if (typeof $(this).attr('data-id') !== "undefined")
        $("#DeletedRowIDs").val($("#DeletedRowIDs").val() + $(this).attr('data-id') + ",");
    var row = this.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (SEItemRowDetails[rowIndex]) {
        var itemArr = itemMasterDetailsStockEntry[row.cells[1].getElementsByTagName("input")[0].value];
        var itemArray = SEItemRowDetails[rowIndex];
        var totalAmt = parseFloat($("#Amount").val());
        var oldAmount = parseFloat(itemArray[2]);
        totalAmt -= parseFloat(oldAmount);
        $("#Amount").val(totalAmt.toFixed(itemArr[6]));
        var i;
        for (i = rowIndex; i < SEItemRowDetails.length; i++) {
            if (SEItemRowDetails[i + 1]) {
                SEItemRowDetails[i] = SEItemRowDetails[i + 1];
            }
            else {
                SEItemRowDetails.splice(i, 1);
            }
        }
    }
    row.remove();
    var index = 1;
    $("td:first-child", $("#StockEntryDetail")).each(function () {
        $(this).find("span:first").text(index);
        index++;

    });
    var newIndex = --index;
    if (newIndex == 1) {

        $("tr", $("#StockEntryDetail")).each(function () {
            var val = $("input[id*='Item']", $(this)).val();
            if (typeof (val) !== "undefined" && val != "undefined") {
                $("a[id*='lnkDeleteSE']", $(this)).css("display", "none");
            }


        });
    }
    $("#rowCount").val(newIndex);
    return false;
});
$(document).on("click", "#lnkDeleteMI", function (e) {
    if (typeof $(this).attr('data-id') !== "undefined")
        $("#DeletedRowIDs").val($("#DeletedRowIDs").val() + $(this).attr('data-id') + ",");
    var row = this.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (MIItemRowDetails[rowIndex]) {
        var itemArr = itemMasterDetailsManualInvoice[row.cells[1].getElementsByTagName("input")[0].value];
        var itemArray = MIItemRowDetails[rowIndex];
        var totalAmt = parseFloat($("#MITotalAmount").val());
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
        $("#MITotalAmount").val(totalAmt.toFixed(itemArr[9]));
        $("#MITotalDiscountAmt").val(totalDiscount.toFixed(itemArr[9]));
        $("#MITotalTaxAmt").val(totalTax.toFixed(itemArr[9]));
        $("#MITotalOrderAmt").val(totalOder.toFixed(itemArr[9]));
        $("#Amount").val(totalOder.toFixed(itemArr[9]));
        var i;
        for (i = rowIndex; i < MIItemRowDetails.length; i++) {
            if (MIItemRowDetails[i + 1]) {
                MIItemRowDetails[i] = MIItemRowDetails[i + 1];
            }
            else {
                MIItemRowDetails.splice(i, 1);
            }
        }
    }
    row.remove();
    var index = 1;
    $("td:first-child", $("#ManualInvoiceDetail")).each(function () {
        $(this).find("span:first").text(index);
        index++;

    });
    var newIndex = --index;
    if (newIndex == 1) {
        $("tr", $("#ManualInvoiceDetail")).each(function () {
            var val = $("input[id*='MIItem']", $(this)).val();
            if (typeof (val) !== "undefined" && val != "undefined") {
                $("a[id*='lnkDeleteMI']", $(this)).css("display", "none");
            }


        });
    }
    $("#rowCount").val(newIndex);
    return false;
});
$(document).on("click", "#lnkDeleteSI", function (e) {
    if (typeof $(this).attr('data-id') !== "undefined")
        $("#DeletedRowIDs").val($("#DeletedRowIDs").val() + $(this).attr('data-id') + ",");
    var row = this.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (IItemRowDetails[rowIndex]) {
        var itemArr = itemMasterDetailsInvoice[row.cells[1].getElementsByTagName("input")[0].value];
        var itemArray = IItemRowDetails[rowIndex];
        var totalAmt = parseFloat($("#ITotalAmount").val());
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
        $("#ITotalAmount").val(totalAmt.toFixed(itemArr[9]));
        $("#ITotalDiscountAmt").val(totalDiscount.toFixed(itemArr[9]));
        $("#ITotalTaxAmt").val(totalTax.toFixed(itemArr[9]));
        $("#ITotalOrderAmt").val(totalOder.toFixed(itemArr[9]));
        $("#Amount").val(totalOder.toFixed(itemArr[9]));
        var i;
        for (i = rowIndex; i < IItemRowDetails.length; i++) {
            if (IItemRowDetails[i + 1]) {
                IItemRowDetails[i] = IItemRowDetails[i + 1];
            }
            else {
                IItemRowDetails.splice(i, 1);
            }
        }
    }
    row.remove();
    var index = 1;
    $("td:first-child", $("#InvoiceDetail")).each(function () {
        $(this).find("span:first").text(index);
        index++;

    });
    var newIndex = --index;
    if (newIndex == 1) {
        $("tr", $("#InvoiceDetail")).each(function () {
            var val = $("input[id*='IItem']", $(this)).val();
            if (typeof (val) !== "undefined" && val != "undefined") {
                $("a[id*='lnkDeleteSI']", $(this)).css("display", "none");
            }


        });
    }
    $("#rowCount").val(newIndex);
    return false;
});
$(document).on("click", "#lnkDeleteSR", function (e) {
    if (typeof $(this).attr('data-id') !== "undefined")
        $("#DeletedRowIDs").val($("#DeletedRowIDs").val() + $(this).attr('data-id') + ",");
    var row = this.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (SRItemRowDetails[rowIndex]) {
        var itemArr = itemMasterDetailsSalesReturn[row.cells[1].getElementsByTagName("input")[0].value];
        var itemArray = SRItemRowDetails[rowIndex];
        var totalAmt = parseFloat($("#SRTotalAmount").val());
        var totalDiscount = parseFloat($("#SRTotalDiscountAmt").val());
        var totalTax = parseFloat($("#SRTotalTaxAmt").val());
        var totalOder = parseFloat($("#SRCorrectTotalOrderAmtHidden").val());
        var oldAmount = parseFloat(itemArray[6]);
        var oldDiscount = parseFloat(itemArray[3]);
        var oldTax = parseFloat(itemArray[5]);
        var oldOder = parseFloat(itemArray[7]);
        totalAmt -= parseFloat(oldAmount);
        totalDiscount -= parseFloat(oldDiscount);
        totalTax -= parseFloat(oldTax);
        totalOder -= parseFloat(oldOder);
        $("#SRTotalAmount").val(totalAmt.toFixed(itemArr[9]));
        $("#SRTotalDiscountAmt").val(totalDiscount.toFixed(itemArr[9]));
        $("#SRTotalTaxAmt").val(totalTax.toFixed(itemArr[9]));
        $("#SRTotalOrderAmt").val((totalOder - parseFloat($("#Demurages").val())).toFixed(itemArr[9]));
        $("#Amount").val((totalOder - parseFloat($("#Demurages").val())).toFixed(itemArr[9]));
        $("#SRCorrectTotalOrderAmtHidden").val(totalOder.toFixed(itemArr[9]));
        var i;
        for (i = rowIndex; i < SRItemRowDetails.length; i++) {
            if (SRItemRowDetails[i + 1]) {
                SRItemRowDetails[i] = SRItemRowDetails[i + 1];
            }
            else {
                SRItemRowDetails.splice(i, 1);
            }
        }
    }
    row.remove();
    var index = 1;
    $("td:first-child", $("#SalesReturnDetail")).each(function () {
        $(this).find("span:first").text(index);
        index++;

    });
    var newIndex = --index;
    if (newIndex == 1) {
        $("tr", $("#SalesReturnDetail")).each(function () {
            var val = $("input[id*='SRItem']", $(this)).val();
            if (typeof (val) !== "undefined" && val != "undefined") {
                $("a[id*='lnkDeleteSR']", $(this)).css("display", "none");
            }


        });
    }
    $("#rowCount").val(newIndex);
    return false;
});
$(document).on("click", "#lnkDeletePO", function (e) {
    if (typeof $(this).attr('data-id') !== "undefined")
        $("#DeletedRowIDs").val($("#DeletedRowIDs").val() + $(this).attr('data-id') + ",");
    var row = this.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (POItemRowDetails[rowIndex]) {
        var itemArr = itemMasterDetailsPO[row.cells[1].getElementsByTagName("input")[0].value];
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
        $("#POTotalAmount").val(totalAmt.toFixed(itemArr[9]));
        $("#POTotalDiscountAmt").val(totalDiscount.toFixed(itemArr[9]));
        $("#POTotalTaxAmt").val(totalTax.toFixed(itemArr[9]));
        $("#POTotalOrderAmt").val(totalOder.toFixed(itemArr[9]));
        $("#Amount").val(totalOder.toFixed(itemArr[9]));
        var i;
        for (i = rowIndex; i < POItemRowDetails.length; i++) {
            if (POItemRowDetails[i + 1]) {
                POItemRowDetails[i] = POItemRowDetails[i + 1];
            }
            else {
                POItemRowDetails.splice(i, 1);
            }
        }
    }
    row.remove();
    var index = 1;
    $("td:first-child", $("#PurchaseOrderDetail")).each(function () {
        $(this).find("span:first").text(index);
        index++;

    });
    var newIndex = --index;
    if (newIndex == 1) {
        $("tr", $("#PurchaseOrderDetail")).each(function () {
            var val = $("input[id*='POItem']", $(this)).val();
            if (typeof (val) !== "undefined" && val != "undefined") {
                $("a[id*='lnkDeletePO']", $(this)).css("display", "none");
            }


        });
    }
    $("#rowCount").val(newIndex);
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
            else {
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
                if ($("#SalesQuotationId").length > 0) {
                    $.each(firstFreeSQ, function (i, j) {

                        if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == $("#QuotationType").val()) {
                            $("#Quotation").val(j.sequenceNumber);
                            $("#SQSequenceNoID").val(j.id);
                        }

                    });
                }
                else if ($("#SalesInvoiceId").length > 0) {
                    $.each(firstFreeSI, function (i, j) {

                        if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == $("#InvoiceType").val()) {
                            $("#Invoice").val(j.sequenceNumber);
                            $("#SISequenceNoID").val(j.id);
                        }

                    });

                }
                else if ($("#StokeEntryMstId").length > 0) {
                    $.each(firstFreeSE, function (i, j) {
                        if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == $("#AdjustmentType").val()) {
                            $("#Document").val(j.sequenceNumber);
                            $("#SESequenceNoID").val(j.id);
                        }

                    });

                }
                else if ($("#InvoiceId").length > 0) {
                    $.each(firstFreeMI, function (i, j) {
                        if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == $("#InvoiceType").val()) {
                            $("#Invoice").val(j.sequenceNumber);
                            $("#MISequenceNoID").val(j.id);
                        }

                    });

                }
                else if ($("#PurchaseOrderId").length > 0) {
                    $.each(firstFreePO, function (i, j) {
                        if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == $("#OrderType").val()) {
                            $("#OrderNo").val(j.sequenceNumber);
                            $("#POSequenceNoID").val(j.id);
                        }

                    });

                }
                else if ($("#GRNId").length > 0) {
                    $.each(firstFreeGrn, function (i, j) {
                        if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == $("#GRNType").val()) {
                            $("#GoodsReceipt").val(j.sequenceNumber);
                            $("#GrnSequenceNoID").val(j.id);
                        }

                    });

                }
                else if ($("#SalesReturnMstId").length > 0) {
                    $.each(firstFreeSR, function (i, j) {
                        if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == $("#SalesReturnType").val()) {
                            $("#SalesReturn").val(j.sequenceNumber);
                            $("#SRSequenceNoID").val(j.id);
                        }

                    });

                }
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
                $("#CustomerId").val(item[3]);
                $("#Name").val(item[0]);
                $("#Telephone").val(item[1]);
                $("#QuotationType").val(item[2]);
                $('#QuotationType option[value="0"]').attr("selected", null);
                $('#QuotationType option[value="1"]').attr("selected", null);
                $('#QuotationType option[value="' + item[2] + '"]').attr("selected", "selected");
                $("#selectedQuotationType").val(item[2]);
                $.each(firstFreeSQ, function (i, j) {
                    if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == item[2]) {
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
$(document).on("keydown", "#LocationSR", function (e) {

    $(this).autocomplete({
        source: locationArray,
        response: function (event, ui) {
            if (!ui.content.length) {
                var noResult = { value: "", label: "No results found" };
                ui.content.push(noResult);
            }
        }
    });
});
$(document).on("keydown", "#SRCustomerId", function (e) {

    $(this).autocomplete({
        source: customerCodes,
        change: function (event, ui) {
            val = $(this).val();
            exists = $.inArray(val, customerCodes);
            if (exists < 0) {
                $(this).val("");
                $("#Name").val("");
                $("#Telephone").val("");
                $("#SalesReturn").val("");
                $("#SRSequenceNoID").val("");
                return false;
            }
            else {
                var item = customerCodesWithDetails[val];
                $("#SRCustomerId").val(item[3]);
                $("#Name").val(item[0]);
                $("#Telephone").val(item[1]);
                $.each(firstFreeSR, function (i, j) {
                    if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == $("#SalesReturnType").val()) {
                        $("#SalesReturn").val(j.sequenceNumber);
                        $("#SRSequenceNoID").val(j.id);
                    }
                    else if (j.seqType == "0" && j.orderType == $("#SalesReturnType").val()) {
                        $("#SalesReturn").val(j.sequenceNumber);
                        $("#SRSequenceNoID").val(j.id);
                    }
                });
                var obj1 = {};
                obj1.companyCode = $.trim($("#CompanyCode").val());
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "SalesReturnEdit.aspx/GetItemMasters",
                    data: JSON.stringify(obj1),
                    dataType: "json",
                    success: function (data) {
                        itemMasterArraySalesReturn = [];
                        itemMasterDetailsSalesReturn = {};
                        itemMasterArraySalesReturnByName = [];
                        itemMasterDetailsSalesReturnByName = {};
                        $.each(data.d, function (i, j) {
                            itemMasterArraySalesReturn.push(j.code);
                            itemMasterDetailsSalesReturn[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint];
                            itemMasterArraySalesReturnByName.push(j.name);
                            itemMasterDetailsSalesReturnByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint];

                        });
                    },
                    error: function (result) {
                        //alert("Error");
                    }
                });
                var obj2 = {};
                obj2.bpCode = val;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "SalesReturnEdit.aspx/GetFinalizedSalesOrderDetails",
                    data: JSON.stringify(obj2),
                    dataType: "json",
                    success: function (data) {
                        salesOrderNos = [];
                        salesOrderDetails = {};
                        $.each(data.d, function (i, j) {
                            salesOrderNos.push(j.salesOrderNo);
                            salesOrderDetails[j.salesOrderNo] = [j.locationCode, j.locationName, j.salesMan, j.salesManName, j.amount, j.taxAmount, j.discountAmount, j.orderAmount, j.salesOrderDate];
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
$(document).on("keydown", "#SalesOrderNo", function (e) {

    $(this).autocomplete({
        source: salesOrderNos,
        change: function (event, ui) {
            val = $(this).val();
            exists = $.inArray(val, salesOrderNos);
            if (exists < 0) {
                $(this).val("");
                $("#Location").val("");
                $("#LocationHidden").val("");
                $("#SalesMan").val("");
                $("#SalesManHidden").val("");
                return false;
            }
            else {
                var item = salesOrderDetails[val];
                $("#Location").val(item[1]);
                $("#LocationHidden").val(item[0]);
                $("#SalesMan").val(item[3]);
                $("#SalesManHidden").val(item[2]);
                $("#SRTotalAmountHidden").val(item[4]);
                $("#SRTotalDiscountAmtHidden").val(item[5]);
                $("#SRTotalTaxAmtHidden").val(item[6]);
                $("#SRTotalOrderAmtHidden").val(item[7]);
                $("#SalesOrderDate").val(item[8]);
                $.each(firstFreeSR, function (i, j) {
                    if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == $("#SalesReturnType").val()) {
                        $("#SalesReturn").val(j.sequenceNumber);
                        $("#SRSequenceNoID").val(j.id);
                    }
                    else if (j.seqType == "0" && j.orderType == $("#SalesReturnType").val()) {
                        $("#SalesReturn").val(j.sequenceNumber);
                        $("#SRSequenceNoID").val(j.id);
                    }
                    $("#buttonUpdate").click();
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

function CheckItemAlreadyAddedSE(val, rowIndex) {
    var flag = false;
    var rowIndexCurrent = -1;
    $("tr", $("#StockEntryDetail")).each(function () {
        var row = this.parentNode.parentNode;
        if (val == $("input[id*='Item']", $(this)).val() && rowIndex !== rowIndexCurrent) {
            flag = true;
        }
        rowIndexCurrent = rowIndexCurrent + 1;
    });
    return flag;
}
$(document).on("keydown", ".StockItem", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayStockEntry,
        select: function (event, ui) {
            var itemArr = itemMasterDetailsStockEntry[ui.item.label];
            if (parseInt(itemArr[7]) == 1) {
                var row = this.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;
                if (!CheckItemAlreadyAddedSE(ui.item.label, rowIndex)) {
                    SetSelectedRowStockEntry(this, ui.item.label);

                }
                else {
                    $("#alertMessage").text("Item already added");
                    var theDialog = $("#dialog-alert").dialog(opt);
                    theDialog.dialog("open");
                    row.cells[1].getElementsByTagName("input")[0].value = "";
                    row.cells[2].getElementsByTagName("input")[0].value = "";
                    row.cells[3].getElementsByTagName("input")[0].value = "";
                    row.cells[4].getElementsByTagName("input")[0].value = "";
                    row.cells[5].getElementsByTagName("input")[0].value = "";
                    row.cells[6].getElementsByTagName("input")[0].value = "";
                    $(this).focus();
                    return false;
                }
            }
            else {
                $("#alertMessage").text("Non inventory Item – Not allowed");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
                $(this).val("");
                $(this).focus();
                return false;
            }

        },
        change: function (event, ui) {

            val = $(this).val();
            exists = $.inArray(val, itemMasterArrayStockEntry);
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (exists < 0) {
                $(this).val("");
                $(this).focus();

                row.cells[2].getElementsByTagName("input")[0].value = "";
                row.cells[3].getElementsByTagName("input")[0].value = "";
                row.cells[4].getElementsByTagName("input")[0].value = "";
                row.cells[5].getElementsByTagName("input")[0].value = "";
                row.cells[6].getElementsByTagName("input")[0].value = "";;
                return false;
            }
            else {
                var itemArr = itemMasterDetailsStockEntry[$(this).val()];
                if (parseInt(itemArr[7]) == 1) {
                    if (!CheckItemAlreadyAddedSE($(this).val(), rowIndex)) {
                        SetSelectedRowStockEntry(this, $(this).val());
                    }
                    else {
                        $("#alertMessage").text("Item already added");
                        var theDialog = $("#dialog-alert").dialog(opt);
                        theDialog.dialog("open");
                        row.cells[1].getElementsByTagName("input")[0].value = "";
                        row.cells[2].getElementsByTagName("input")[0].value = "";
                        row.cells[3].getElementsByTagName("input")[0].value = "";
                        row.cells[4].getElementsByTagName("input")[0].value = "";
                        row.cells[5].getElementsByTagName("input")[0].value = "";
                        row.cells[6].getElementsByTagName("input")[0].value = "";
                        $(this).focus();
                    }
                }
                else {
                    $("#alertMessage").text("Non inventory Item – Not allowed");
                    var theDialog = $("#dialog-alert").dialog(opt);
                    theDialog.dialog("open");
                    $(this).val("");
                    $(this).focus();
                }


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
$(document).on("keydown", ".StockName", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayStockEntryByName,
        select: function (event, ui) {
            var itemArr = itemMasterDetailsStockEntryByName[ui.item.label];
            if (parseInt(itemArr[7]) == 1) {
                var row = this.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;
                if (!CheckItemAlreadyAddedSE(itemArr[0], rowIndex)) {
                    SetSelectedRowStockEntryByName(this, ui.item.label);
                }
                else {
                    $("#alertMessage").text("Item already added");
                    var theDialog = $("#dialog-alert").dialog(opt);
                    theDialog.dialog("open");
                    row.cells[1].getElementsByTagName("input")[0].value = "";
                    row.cells[2].getElementsByTagName("input")[0].value = "";
                    row.cells[3].getElementsByTagName("input")[0].value = "";
                    row.cells[4].getElementsByTagName("input")[0].value = "";
                    row.cells[5].getElementsByTagName("input")[0].value = "";
                    row.cells[6].getElementsByTagName("input")[0].value = "";
                    $(this).focus();
                    return false;
                }
            }
            else {
                $("#alertMessage").text("Non inventory Item – Not allowed");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
                $(this).val("");
                $(this).focus();
                return false;
            }


        },
        change: function (event, ui) {
            val = $(this).val();
            exists = $.inArray(val, itemMasterArrayStockEntryByName);
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (exists < 0) {
                $(this).val("");
                row.cells[1].getElementsByTagName("input")[0].value = "";
                row.cells[3].getElementsByTagName("input")[0].value = "";
                row.cells[4].getElementsByTagName("input")[0].value = "";
                row.cells[5].getElementsByTagName("input")[0].value = "";
                row.cells[6].getElementsByTagName("input")[0].value = "";
                return false;
            }
            else {
                var itemArr = itemMasterDetailsStockEntryByName[$(this).val()];
                if (parseInt(itemArr[7]) == 1) {
                    if (!CheckItemAlreadyAddedSE(itemArr[0], rowIndex)) {
                        SetSelectedRowStockEntryByName(this, $(this).val());
                    }
                    else {
                        $("#alertMessage").text("Item already added");
                        var theDialog = $("#dialog-alert").dialog(opt);
                        theDialog.dialog("open");
                        row.cells[1].getElementsByTagName("input")[0].value = "";
                        row.cells[2].getElementsByTagName("input")[0].value = "";
                        row.cells[3].getElementsByTagName("input")[0].value = "";
                        row.cells[4].getElementsByTagName("input")[0].value = "";
                        row.cells[5].getElementsByTagName("input")[0].value = "";
                        row.cells[6].getElementsByTagName("input")[0].value = "";
                        $(this).focus();
                        return false;
                    }
                }
                else {
                    $("#alertMessage").text("Non inventory Item – Not allowed");
                    var theDialog = $("#dialog-alert").dialog(opt);
                    theDialog.dialog("open");
                    $(this).val("");
                    $(this).focus();
                }


            }

        },
        response: function (event, ui) {
            if (!ui.content.length) {
                var noResult = { value: "", label: "No results found" };
                ui.content.push(noResult);
            }
        }
    });
    //$(this).autocomplete(optionsStockEntryByName);
});
function CalculateSEAmount(txtBox) {

    var row = txtBox.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (row.cells[1].getElementsByTagName("input")[0].value != "") {
        var itemArr = itemMasterDetailsStockEntry[row.cells[1].getElementsByTagName("input")[0].value];
        var rate = row.cells[5].getElementsByTagName("input")[0].value;
        var qnty = row.cells[3].getElementsByTagName("input")[0].value;
        if (qnty != "") {
            var amount = (qnty * rate).toFixed(itemArr[6]);
            if (!SEItemRowDetails[rowIndex] && row.cells[6].getElementsByTagName("input")[0].value == "") {

                row.cells[6].getElementsByTagName("input")[0].value = amount;
                SEItemRowDetails[rowIndex] = [qnty, rate, amount];
                if ($("#Amount").val() == "" || $("#Amount").val() == "0") {
                    $("#Amount").val(amount);
                }
                else {
                    $("#Amount").val((parseFloat($("#Amount").val()) + parseFloat(amount)).toFixed(itemArr[6]));
                }
            }
            else {
                var itemArray = SEItemRowDetails[rowIndex];
                var totalAmt = parseFloat($("#Amount").val());
                var oldAmount = parseFloat(itemArray[2]);
                totalAmt -= parseFloat(oldAmount);
                totalAmt += parseFloat(amount);
                row.cells[6].getElementsByTagName("input")[0].value = amount;
                $("#Amount").val(totalAmt.toFixed(itemArr[6]));
                SEItemRowDetails[rowIndex] = [qnty, rate, amount];
            }


        }
    }

}
function iSQuantityAvailableSE(txtBox) {
    var row = txtBox.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (row.cells[1].getElementsByTagName("input")[0].value != ""
        && row.cells[3].getElementsByTagName("input")[0].value != "") {
        var itemCode = row.cells[1].getElementsByTagName("input")[0].value;
        var itemArr = itemMasterQuantitySE[itemCode];
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
                        $("#alertMessage").text("Insufficient Qty, Available " + itemArr[0] + " " + row.cells[4].getElementsByTagName("input")[0].value);
                        var theDialog = $("#dialog-alert").dialog(opt);
                        theDialog.dialog("open");
                        row.cells[3].getElementsByTagName("input")[0].focus();
                        row.cells[3].getElementsByTagName("input")[0].value = "";
                    }
                    CalculateSQAmount(txtBox);
                },
                error: function (result) {
                    //alert("Error");
                }
            });
            var obj1 = {};
            obj1.companyCode = $.trim($("#CompanyCode").val());
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "StockEntryEdit.aspx/GetItemMasters",
                data: JSON.stringify(obj1),
                dataType: "json",
                success: function (data) {

                    itemMasterQuantitySE = {};
                    $.each(data.d, function (i, j) {
                        itemMasterQuantitySE[j.code] = [j.Qnty];
                    });
                    itemArr = itemMasterQuantitySE[itemCode];
                    if (parseInt(itemArr[0]) < parseInt(row.cells[3].getElementsByTagName("input")[0].value)) {
                        $("#alertMessage").text("Insufficient Qty, Available " + itemArr[0] + " " + row.cells[4].getElementsByTagName("input")[0].value);
                        var theDialog = $("#dialog-alert").dialog(opt);
                        theDialog.dialog("open");
                        row.cells[3].getElementsByTagName("input")[0].focus();
                        row.cells[3].getElementsByTagName("input")[0].value = "";
                    }
                    else {
                        CalculateSEAmount(txtBox);
                    }

                },
                error: function (result) {
                    // alert("Error");
                }
            });
        }
        else {
            if (parseInt(itemArr[0]) < parseInt(row.cells[3].getElementsByTagName("input")[0].value)) {
                $("#alertMessage").text("Insufficient Qty, Available " + itemArr[0] + " " + row.cells[4].getElementsByTagName("input")[0].value);
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
                row.cells[3].getElementsByTagName("input")[0].focus();
                row.cells[3].getElementsByTagName("input")[0].value = "";
            }
            else {
                CalculateSEAmount(txtBox);
            }

        }

    }
}
$(document).on("focusout", "#SEQuantity", function (e) {
    var row = this.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if ($("#AdjustmentType").val() == "3") {
        if (row.cells[1].getElementsByTagName("input")[0].value != ""
        && $.trim(row.cells[3].getElementsByTagName("input")[0].value) != "") {
            iSQuantityAvailableSE(this);
        }
        else {
            row.cells[3].getElementsByTagName("input")[0].value = "";
            row.cells[3].getElementsByTagName("input")[0].focus();
            $("#alertMessage").text("Please fill an item with quantity");
            var theDialog = $("#dialog-alert").dialog(opt);
            theDialog.dialog("open");
            return false;
        }
    }
    else {
        if (row.cells[1].getElementsByTagName("input")[0].value != ""
        && $.trim(row.cells[3].getElementsByTagName("input")[0].value) != "") {
            CalculateSEAmount(this);
        }
        else {
            if (row.cells[1].getElementsByTagName("input")[0].value != "")
            {
                row.cells[3].getElementsByTagName("input")[0].value = "";
                row.cells[3].getElementsByTagName("input")[0].focus();
                $("#alertMessage").text("Please fill an item with quantity");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
            }
            
            return false;
        }
        
    }
    
    
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
        $("#alertMessage").text("Same prefix and sequence number not allowed for more than one time");
        var theDialog = $("#dialog-alert").dialog(opt);
        theDialog.dialog("open");
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
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[4].getElementsByTagName("input")[0].value = itemArr[4];
    row.cells[6].getElementsByTagName("input")[0].value = 0;
    row.cells[7].getElementsByTagName("input")[0].value = 0;
    row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
    return false;
}
function SetSelectedRowManualInvoiceByName(lnk, selectedItem) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var itemArr = itemMasterDetailsManualInvoiceByName[selectedItem];
    row.cells[1].getElementsByTagName("input")[0].value = itemArr[0];
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[4].getElementsByTagName("input")[0].value = itemArr[4];
    row.cells[6].getElementsByTagName("input")[0].value = 0;
    row.cells[7].getElementsByTagName("input")[0].value = 0;
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

function CheckItemAlreadyAddedMI(val, rowIndex) {
    var flag = false;
    var rowIndexCurrent = -1;
    $("tr", $("#ManualInvoiceDetail")).each(function () {
        var row = this.parentNode.parentNode;
        if (val == $("input[id*='MIItem']", $(this)).val() && rowIndex !== rowIndexCurrent) {
            flag = true;
        }
        rowIndexCurrent = rowIndexCurrent + 1;
    });
    return flag;
}

$(document).on("keydown", ".MIItem", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayManualInvoice,
        select: function (event, ui) {
            if (!CheckItemAlreadyAddedMI(ui.item.label, rowIndex)) {
                SetSelectedRowManualInvoice(this, ui.item.label);
            }
            else {
                $("#alertMessage").text("Item already added");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
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
            exists = $.inArray(val, itemMasterArrayManualInvoice);
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (exists < 0) {
                $(this).val("");
                $(this).focus();

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
            else {
                if (!CheckItemAlreadyAddedMI($(this).val(), rowIndex)) {
                    SetSelectedRowManualInvoice(this, $(this).val());
                }
                else {
                    $(this).val("");
                    $(this).focus();
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
$(document).on("keydown", ".MIItemName", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayManualInvoiceByName,
        select: function (event, ui) {
            var itemArr = itemMasterDetailsManualInvoiceByName[ui.item.label];
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (!CheckItemAlreadyAddedMI(itemArr[0], rowIndex)) {
                SetSelectedRowManualInvoiceByName(this, ui.item.label);

            }
            else {
                $("#alertMessage").text("Item already added");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
                $(this).val("");
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
            exists = $.inArray(val, itemMasterArrayManualInvoiceByName);
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (exists < 0) {
                $(this).val("");
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
            else {
                var itemArr = itemMasterDetailsInvoiceByName[$(this).val()];
                if (!CheckItemAlreadyAddedMI(itemArr[0], rowIndex)) {
                    SetSelectedRowManualInvoiceByName(this, $(this).val());

                }
                else {
                    $(this).val("");
                    $(this).focus();
                }
            }

        },
        response: function (event, ui) {
            if (!ui.content.length) {
                var noResult = { value: "", label: "No results found" };
                ui.content.push(noResult);
            }
        }
    });
    //$(this).autocomplete(optionsManualInvoiceByName);
});
$(document).on("focusout", "#MIQuantity", function (e) {
    var row = this.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (row.cells[1].getElementsByTagName("input")[0].value != ""
        && $.trim(row.cells[3].getElementsByTagName("input")[0].value) != "") {
        CalculateMIAmount(this);
    }
    else {
        if (row.cells[1].getElementsByTagName("input")[0].value != "") {
            row.cells[3].getElementsByTagName("input")[0].value = "";
            row.cells[3].getElementsByTagName("input")[0].focus();
            $("#alertMessage").text("Please fill an item with quantity");
            var theDialog = $("#dialog-alert").dialog(opt);
            theDialog.dialog("open");
        }
        return false;
    }
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
        var itemArr = itemMasterDetailsManualInvoice[row.cells[1].getElementsByTagName("input")[0].value];
        var rate = row.cells[5].getElementsByTagName("input")[0].value;
        var qnty = row.cells[3].getElementsByTagName("input")[0].value;
        var discountPer = row.cells[6].getElementsByTagName("input")[0].value;
        var discountAmt = row.cells[7].getElementsByTagName("input")[0].value;
        var taxPer = row.cells[8].getElementsByTagName("input")[0].value;
        if (qnty != "" && (discountPer != "" || discountAmt != "") && taxPer != "") {
            var rowTotalRate = (parseInt(qnty) * parseFloat(rate)).toFixed(itemArr[9]);
            if ($(txtBox).attr("id") == "MIDiscAmt") {
                if (parseFloat(rowTotalRate) != 0) {
                    discountPer = ((parseFloat(discountAmt) / parseFloat(rowTotalRate)) * 100).toFixed(itemArr[9]);
                }
                else {
                    discountPer = 0;
                }
            }
            else {
                discountAmt = ((rowTotalRate * parseFloat(discountPer)) / 100).toFixed(itemArr[9]);
            }
            var taxAmount = (((rowTotalRate - discountAmt) / 100) * taxPer).toFixed(itemArr[9]);
            var netAmount = ((rowTotalRate - discountAmt) + parseFloat(taxAmount)).toFixed(itemArr[9]);
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
                MIItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, rowTotalRate, orderAmount];
                if ($("#MITotalAmount").val() == "") {
                    $("#MITotalAmount").val(rowTotalRate);
                }
                else {
                    $("#MITotalAmount").val(parseFloat($("#MITotalAmount").val()) + parseFloat(rowTotalRate));
                }
                if ($("#Amount").val() == "") {
                    $("#Amount").val(orderAmount);
                }
                else {
                    $("#Amount").val(parseFloat($("#Amount").val()) + parseFloat(orderAmount));
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
                var totalAmt = parseFloat($("#MITotalAmount").val());
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
                totalAmt += parseFloat(rowTotalRate);
                totalDiscount += parseFloat(discountAmt);
                totalTax += parseFloat(taxAmount);
                totalOder += parseFloat(orderAmount);
                row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                $("#Amount").val(totalOder.toFixed(itemArr[9]));
                $("#MITotalAmount").val(totalAmt.toFixed(itemArr[9]));
                $("#MITotalDiscountAmt").val(totalDiscount.toFixed(itemArr[9]));
                $("#MITotalTaxAmt").val(totalTax.toFixed(itemArr[9]));
                $("#MITotalOrderAmt").val(totalOder.toFixed(itemArr[9]));
                MIItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, rowTotalRate, orderAmount];
            }


        }
    }

}

function iSQuantityAvailableI(txtBox) {
    
    var row = txtBox.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (row.cells[1].getElementsByTagName("input")[0].value != ""
        && $.trim(row.cells[3].getElementsByTagName("input")[0].value) != "") {
        var itemCode = row.cells[1].getElementsByTagName("input")[0].value;
        var itemArr = itemMasterQuantityI[itemCode];
        var itemArray = itemMasterDetailsInvoice[itemCode];
        if (typeof (itemArr) === "undefined") {
            var val = $("#CustomerIdSI").val();
            var item = customerCodesWithDetails[val];
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
                    itemMasterQuantityI = {};
                    itemMasterDetailsInvoice = {};
                    $.each(data.d, function (i, j) {
                        itemMasterQuantityI[j.code] = [j.Qnty];
                        itemMasterDetailsInvoice[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];
                    });
                    itemArr = itemMasterQuantityI[itemCode];
                    itemArray = itemMasterDetailsInvoice[itemCode];
                    console.log(itemMasterDetailsInvoice); console.log(itemArray);
                    if (parseInt(itemArray[10]) == 0) {
                        CalculateIAmount(txtBox);
                    }
                    else if (parseInt(itemArr[0]) >= parseInt(row.cells[3].getElementsByTagName("input")[0].value)) {
                        CalculateIAmount(txtBox);
                    }
                    else {
                        row.cells[3].getElementsByTagName("input")[0].value = itemArr[0];
                        $("#alertMessage").text("Insufficient Qty, Available qty is" + itemArr[0]);
                        var theDialog = $("#dialog-alert").dialog(opt);
                        theDialog.dialog("open");
                        CalculateIAmount(txtBox);
                        row.cells[3].getElementsByTagName("input")[0].focus();
                    }
                },
                error: function (result) {
                    // alert("Error");
                }
            });

        }
        else {
            if (parseInt(itemArray[10]) == 0) {
                CalculateIAmount(txtBox);
            }
            else if (parseInt(itemArr[0]) >= parseInt(row.cells[3].getElementsByTagName("input")[0].value)) {
                CalculateIAmount(txtBox);
            }
            else {
                row.cells[3].getElementsByTagName("input")[0].value = itemArr[0];
                $("#alertMessage").text("Insufficient Qty, Available qty is" + itemArr[0]);
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
                CalculateIAmount(txtBox);
                row.cells[3].getElementsByTagName("input")[0].focus();
            }
        }

    }
    else {
        if (row.cells[1].getElementsByTagName("input")[0].value != "") {
            row.cells[3].getElementsByTagName("input")[0].value = "";
            row.cells[3].getElementsByTagName("input")[0].focus();
            $("#alertMessage").text("Please fill an item with quantity");
            var theDialog = $("#dialog-alert").dialog(opt);
            theDialog.dialog("open");
        }
        return false;
    }
}
function iSQuantityAvailableSQ(txtBox) {
    var row = txtBox.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (row.cells[1].getElementsByTagName("input")[0].value != ""
        && $.trim(row.cells[3].getElementsByTagName("input")[0].value) != "") {
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
                        $("#alertMessage").text("Insufficient Qty, Available " + itemArr[0] + " " + row.cells[4].getElementsByTagName("input")[0].value);
                        var theDialog = $("#dialog-alert").dialog(opt);
                        theDialog.dialog("open");
                        
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
                $("#alertMessage").text("Insufficient Qty, Available " + itemArr[0] + " " + row.cells[4].getElementsByTagName("input")[0].value);
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
                
            }
            CalculateSQAmount(txtBox);
        }

    }
    else
    {
        if (row.cells[1].getElementsByTagName("input")[0].value != "")
        {
            row.cells[3].getElementsByTagName("input")[0].value = "";
            row.cells[3].getElementsByTagName("input")[0].focus();
            $("#alertMessage").text("Please fill an item with quantity");
            var theDialog = $("#dialog-alert").dialog(opt);
            theDialog.dialog("open");
        }
        
        return false;
    }
}

function SetSelectedRowInvoice(lnk, selectedItem) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var itemArr = itemMasterDetailsInvoice[selectedItem];
    row.cells[2].getElementsByTagName("input")[0].value = itemArr[0];
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[4].getElementsByTagName("input")[0].value = itemArr[4];
    row.cells[6].getElementsByTagName("input")[0].value = 0;
    row.cells[7].getElementsByTagName("input")[0].value = 0;
    row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
    row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
    return false;
}
function SetSelectedRowInvoiceByName(lnk, selectedItem) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var itemArr = itemMasterDetailsInvoiceByName[selectedItem];
    row.cells[1].getElementsByTagName("input")[0].value = itemArr[0];
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[4].getElementsByTagName("input")[0].value = itemArr[4];
    row.cells[6].getElementsByTagName("input")[0].value = 0;
    row.cells[7].getElementsByTagName("input")[0].value = 0;
    row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
    row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
    return false;
}
function CheckItemAlreadyAddedSI(val, rowIndex) {
    var flag = false;
    var rowIndexCurrent = -1;
    $("tr", $("#InvoiceDetail")).each(function () {
        var row = this.parentNode.parentNode;
        if (val == $("input[id*='IItem']", $(this)).val() && rowIndex !== rowIndexCurrent) {
            flag = true;
        }
        rowIndexCurrent = rowIndexCurrent + 1;
    });
    return flag;
}
function IsItemAvailableSi(item) {
    var itemArr = itemMasterQuantityI[item];
    var itemArray = itemMasterDetailsInvoice[item];
    if (parseInt(itemArray[10]) == 0) {
        return true;
    }
    else if (parseInt(itemArr[0]) > 0) {
        return true;
    }
    else {
        $("#alertMessage").text("Available qty is 0");
        var theDialog = $("#dialog-alert").dialog(opt);
        theDialog.dialog("open");
        return false;
    }
}
$(document).on("keydown", ".IItem", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayInvoice,
        select: function (event, ui) {
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (!CheckItemAlreadyAddedSI(ui.item.label, rowIndex)) {
                if (IsItemAvailableSi(ui.item.label)) {
                    SetSelectedRowInvoice(this, ui.item.label);
                }
                else {
                    $(this).val("");
                }
            }
            else {
                $("#alertMessage").text("Item Already added");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
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
            exists = $.inArray(val, itemMasterArrayInvoice);
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (exists < 0) {
                $(this).val("");
                $(this).focus();

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
            else {
                if (!CheckItemAlreadyAddedSI($(this).val(), rowIndex)) {
                    if (IsItemAvailableSi($(this).val())) {
                        SetSelectedRowInvoice(this, $(this).val());
                    }
                    else {
                        $(this).val("");
                        $(this).focus();
                    }
                }
                else {
                    $(this).val("");
                    $(this).focus();
                }
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
$(document).on("keydown", ".IItemName", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayInvoiceByName,
        select: function (event, ui) {
            var itemArr = itemMasterDetailsInvoiceByName[ui.item.label];
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (!CheckItemAlreadyAddedSI(itemArr[0], rowIndex)) {
                if (IsItemAvailableSi(itemArr[0])) {
                    SetSelectedRowInvoiceByName(this, ui.item.label);
                }
                else {
                    $(this).val("");
                }

            }
            else {
                $("#alertMessage").text("Item Already added");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
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
            exists = $.inArray(val, itemMasterArrayInvoiceByName);
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (exists < 0) {
                $(this).val("");
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
            else {
                var itemArr = itemMasterDetailsInvoiceByName[$(this).val()];
                if (!CheckItemAlreadyAddedSI(itemArr[0], rowIndex)) {
                    if (IsItemAvailableSi(itemArr[0])) {
                        SetSelectedRowInvoiceByName(this, $(this).val());
                    }
                    else {
                        $(this).val("");
                        $(this).focus();
                    }

                }
                else {
                    $(this).val("");
                    $(this).focus();
                }
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
$(document).on("keydown", ".ITaxPer", function (e) {
    $(this).autocomplete({
        source: itemTaxCodes,
        select: function (event, ui) {
            event.preventDefault();
            setSelectedTaxCode(this, ui.item.label);

        },
        change: function (event, ui) {

            val = $.trim($(this).val());
            exists = $.inArray(parseInt(val), itemTaxCodevalues);
            if (exists < 0) {
                $(this).val("");
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
        var itemArr = itemMasterDetailsInvoice[row.cells[1].getElementsByTagName("input")[0].value];
        var rate = row.cells[5].getElementsByTagName("input")[0].value;
        var qnty = row.cells[3].getElementsByTagName("input")[0].value;
        var discountPer = row.cells[6].getElementsByTagName("input")[0].value;
        var discountAmt = row.cells[7].getElementsByTagName("input")[0].value;
        var taxPer = row.cells[8].getElementsByTagName("input")[0].value;
        if (qnty != "" && (discountPer != "" || discountAmt != "") && taxPer != "") {
            var rowTotalRate = (parseInt(qnty) * parseFloat(rate)).toFixed(itemArr[9]);
            if ($(txtBox).attr("id") == "IDiscAmt") {
                if (parseFloat(rowTotalRate) != 0) {
                    discountPer = ((parseFloat(discountAmt) / parseFloat(rowTotalRate)) * 100).toFixed(itemArr[9]);
                }
                else {
                    discountPer = 0;
                }

            }
            else {
                discountAmt = ((rowTotalRate * parseFloat(discountPer)) / 100).toFixed(itemArr[9]);
            }
            var taxAmount = (((rowTotalRate - discountAmt) / 100) * taxPer).toFixed(itemArr[9]);
            var netAmount = ((rowTotalRate - discountAmt) + parseFloat(taxAmount)).toFixed(itemArr[9]);
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
                IItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, rowTotalRate, orderAmount];
                if ($("#ITotalAmount").val() == "") {
                    $("#ITotalAmount").val(rowTotalRate);
                }
                else {
                    $("#ITotalAmount").val((parseFloat($("#ITotalAmount").val()) + parseFloat(rowTotalRate)).toFixed(itemArr[9]));
                }
                if ($("#Amount").val() == "") {
                    $("#Amount").val(orderAmount);
                }
                else {
                    $("#Amount").val((parseFloat($("#Amount").val()) + parseFloat(orderAmount)).toFixed(itemArr[9]));
                }
                if ($("#ITotalDiscountAmt").val() == "") {
                    $("#ITotalDiscountAmt").val(discountAmt);
                }
                else {
                    $("#ITotalDiscountAmt").val((parseFloat($("#ITotalDiscountAmt").val()) + parseFloat(discountAmt)).toFixed(itemArr[9]));
                }
                if ($("#ITotalTaxAmt").val() == "") {
                    $("#ITotalTaxAmt").val(taxAmount);
                }
                else {
                    $("#ITotalTaxAmt").val((parseFloat($("#ITotalTaxAmt").val()) + parseFloat(taxAmount)).toFixed(itemArr[9]));
                }
                if ($("#ITotalOrderAmt").val() == "") {
                    $("#ITotalOrderAmt").val(orderAmount);
                }
                else {
                    $("#ITotalOrderAmt").val((parseFloat($("#ITotalOrderAmt").val()) + parseFloat(orderAmount)).toFixed(itemArr[9]));
                }
            }
            else {
                var itemArray1 = IItemRowDetails[rowIndex];
                var totalAmt = parseFloat($("#ITotalAmount").val());
                var totalDiscount = parseFloat($("#ITotalDiscountAmt").val());
                var totalTax = parseFloat($("#ITotalTaxAmt").val());
                var totalOder = parseFloat($("#ITotalOrderAmt").val());
                var oldAmount = parseFloat(itemArray1[6]);
                var oldDiscount = parseFloat(itemArray1[3]);
                var oldTax = parseFloat(itemArray1[5]);
                var oldOder = parseFloat(itemArray1[7]);
                totalAmt -= parseFloat(oldAmount);
                totalDiscount -= parseFloat(oldDiscount);
                totalTax -= parseFloat(oldTax);
                totalOder -= parseFloat(oldOder);
                totalAmt += parseFloat(rowTotalRate);
                totalDiscount += parseFloat(discountAmt);
                totalTax += parseFloat(taxAmount);
                totalOder += parseFloat(orderAmount);
                row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                $("#Amount").val(totalOder.toFixed(itemArr[9]));
                $("#ITotalAmount").val(totalAmt.toFixed(itemArr[9]));
                $("#ITotalDiscountAmt").val(totalDiscount.toFixed(itemArr[9]));
                $("#ITotalTaxAmt").val(totalTax.toFixed(itemArr[9]));
                $("#ITotalOrderAmt").val(totalOder.toFixed(itemArr[9]));
                IItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, rowTotalRate, orderAmount];
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
    row.cells[6].getElementsByTagName("input")[0].value = 0;
    row.cells[7].getElementsByTagName("input")[0].value = 0;
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
    row.cells[6].getElementsByTagName("input")[0].value = 0;
    row.cells[7].getElementsByTagName("input")[0].value = 0;
    row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
    row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
    return false;
}

function CheckItemAlreadyAddedSQ(val, rowIndex) {
    var flag = false;
    var rowIndexCurrent = -1;
    $("tr", $("#SalesQuotationDetail")).each(function () {
        var row = this.parentNode.parentNode;
        if (val == $("input[id*='SQItem']", $(this)).val() && rowIndex !== rowIndexCurrent) {
            flag = true;
        }
        rowIndexCurrent = rowIndexCurrent + 1;
    });
    return flag;
}
$(document).on("keydown", ".SQItem", function (e) {
    $(this).autocomplete({
        source: itemMasterArraySQ,
        select: function (event, ui) {
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (!CheckItemAlreadyAddedSQ(ui.item.label, rowIndex)) {
                SetSelectedRowSQ(this, ui.item.label);

            }
            else {
                $("#alertMessage").text("Item Already added");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
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
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
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
            else {
                if (!CheckItemAlreadyAddedSQ($(this).val(), rowIndex)) {
                    SetSelectedRowSQ(this, $(this).val());
                }
                else {
                    $(this).val("");
                    $(this).focus();
                }
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
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var itemArr = itemMasterDetailsSQByName[ui.item.label];
            if (!CheckItemAlreadyAddedSQ(itemArr[0], rowIndex)) {
                SetSelectedRowSQByName(this, ui.item.label);
            }
            else {
                $("#alertMessage").text("Item Already added");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
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
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
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
            else {
                var itemArr = itemMasterDetailsSQByName[$(this).val()];
                if (!CheckItemAlreadyAddedSQ(itemArr[0], rowIndex)) {
                    SetSelectedRowSQByName(this, $(this).val());
                }
                else {
                    $(this).val("");
                    $(this).focus();
                }
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
            exists = $.inArray(parseInt(val), itemTaxCodevalues);
            if (exists < 0) {
                $(this).val("");
                return false;
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
            var rowTotalRate = (qnty * rate).toFixed(itemArr[9]);
            if ($(txtBox).attr("id") == "SQDiscAmt") {
                if (parseFloat(rowTotalRate) != 0) {
                    discountPer = ((parseFloat(discountAmt) / parseFloat(rowTotalRate)) * 100).toFixed(itemArr[9]);
                }
                else {
                    discountPer = 0;
                }
            }
            else {
                discountAmt = ((rowTotalRate * discountPer) / 100).toFixed(itemArr[9]);
            }
            var taxAmount = (((rowTotalRate - discountAmt) / 100) * taxPer).toFixed(itemArr[9]);
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
                SQItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, rowTotalRate, orderAmount];
                if ($("#TotalAmount").val() == "") {
                    $("#TotalAmount").val(rowTotalRate);
                }
                else {
                    $("#TotalAmount").val((parseFloat($("#TotalAmount").val()) + parseFloat(rowTotalRate)).toFixed(itemArr[9]));
                }
                if ($("#TotalDiscountAmt").val() == "") {
                    $("#TotalDiscountAmt").val(discountAmt);
                }
                else {
                    $("#TotalDiscountAmt").val((parseFloat($("#TotalDiscountAmt").val()) + parseFloat(discountAmt)).toFixed(itemArr[9]));
                }
                if ($("#TotalTaxAmt").val() == "") {
                    $("#TotalTaxAmt").val(taxAmount);
                }
                else {
                    $("#TotalTaxAmt").val((parseFloat($("#TotalTaxAmt").val()) + parseFloat(taxAmount)).toFixed(itemArr[9]));
                }
                if ($("#TotalOrderAmt").val() == "") {
                    $("#TotalOrderAmt").val(orderAmount);
                }
                else {
                    $("#TotalOrderAmt").val((parseFloat($("#TotalOrderAmt").val()) + parseFloat(orderAmount)).toFixed(itemArr[9]));
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
                totalAmt += parseFloat(rowTotalRate);
                totalDiscount += parseFloat(discountAmt);
                totalTax += parseFloat(taxAmount);
                totalOder += parseFloat(orderAmount);
                row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                $("#TotalAmount").val(totalAmt.toFixed(itemArr[9]));
                $("#TotalDiscountAmt").val(totalDiscount.toFixed(itemArr[9]));
                $("#TotalTaxAmt").val(totalTax.toFixed(itemArr[9]));
                $("#TotalOrderAmt").val(totalOder.toFixed(itemArr[9]));
                SQItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, rowTotalRate, orderAmount];
            }


        }
    }

}

function iSQuantityAvailablePO(txtBox) {
    var row = txtBox.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (row.cells[1].getElementsByTagName("input")[0].value != ""
        && $.trim(row.cells[3].getElementsByTagName("input")[0].value) != "") {
        var itemCode = row.cells[1].getElementsByTagName("input")[0].value;
        var itemArr = itemMasterQuantityPO[itemCode];
        var itemArray = itemMasterDetailsPO[itemCode];
        if (typeof (itemArr) === "undefined") {
            var val = $("#SupplierId").val();
            var item = customerCodesWithDetails[val];
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
                    itemMasterQuantityPO = {};
                    itemMasterDetailsInvoice = {};
                    $.each(data.d, function (i, j) {
                        itemMasterQuantityPO[j.code] = [j.Qnty];
                        itemMasterDetailsPO[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];
                    });
                    itemArr = itemMasterQuantityPO[itemCode];
                    itemArray = itemMasterDetailsPO[itemCode];
                    if (parseInt(itemArray[10]) == 0) {
                        CalculatePOAmount(txtBox);
                    }
                    else if (parseInt(itemArr[0]) >= parseInt(row.cells[3].getElementsByTagName("input")[0].value)) {
                        CalculatePOAmount(txtBox);
                    }
                    else {
                        row.cells[3].getElementsByTagName("input")[0].value = itemArr[0];
                        $("#alertMessage").text("Insufficient Qty, Available qty is" + itemArr[0]);
                        var theDialog = $("#dialog-alert").dialog(opt);
                        theDialog.dialog("open");
                        CalculatePOAmount(txtBox);
                        row.cells[3].getElementsByTagName("input")[0].focus();
                    }
                },
                error: function (result) {
                    // alert("Error");
                }
            });

        }
        else {
            if (parseInt(itemArray[10]) == 0) {
                CalculatePOAmount(txtBox);
            }
            else if (parseInt(itemArr[0]) >= parseInt(row.cells[3].getElementsByTagName("input")[0].value)) {
                CalculatePOAmount(txtBox);
            }
            else {
                row.cells[3].getElementsByTagName("input")[0].value = itemArr[0];
                $("#alertMessage").text("Insufficient Qty, Available qty is" + itemArr[0]);
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
                CalculatePOAmount(txtBox);
                row.cells[3].getElementsByTagName("input")[0].focus();
            }
        }

    }
    else {
        if (row.cells[1].getElementsByTagName("input")[0].value != "")
        {
            row.cells[3].getElementsByTagName("input")[0].value = "";
            row.cells[3].getElementsByTagName("input")[0].focus();
            $("#alertMessage").text("Please fill an item with quantity");
            var theDialog = $("#dialog-alert").dialog(opt);
            theDialog.dialog("open");
        }
        
        return false;
    }
}

function SetSelectedRowPO(lnk, selectedItem) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var itemArr = itemMasterDetailsPO[selectedItem];
    row.cells[2].getElementsByTagName("input")[0].value = itemArr[0];
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[4].getElementsByTagName("input")[0].value = itemArr[4];
    row.cells[6].getElementsByTagName("input")[0].value = 0;
    row.cells[7].getElementsByTagName("input")[0].value = 0;
    row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
    row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
    return false;
}

function SetSelectedRowPOByName(lnk, selectedItem) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var itemArr = itemMasterDetailsPOByName[selectedItem];
    row.cells[1].getElementsByTagName("input")[0].value = itemArr[0];
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[4].getElementsByTagName("input")[0].value = itemArr[4];
    row.cells[6].getElementsByTagName("input")[0].value = 0;
    row.cells[7].getElementsByTagName("input")[0].value = 0;
    row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
    row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
    return false;
}

function CheckItemAlreadyAddedPO(val, rowIndex) {
    var flag = false;
    var rowIndexCurrent = -1;
    $("tr", $("#PurchaseOrderDetail")).each(function () {
        var row = this.parentNode.parentNode;
        if (val == $("input[id*='POItem']", $(this)).val() && rowIndex !== rowIndexCurrent) {
            flag = true;
        }
        rowIndexCurrent = rowIndexCurrent + 1;
    });
    return flag;
}
function IsItemAvailablePO(item) {
    var itemArr = itemMasterQuantityPO[item];
    var itemArray = itemMasterDetailsPO[item];
    if (parseInt(itemArray[10]) == 0) {
        return true;
    }
    else if (parseInt(itemArr[0]) > 0) {
        return true;
    }
    else {
        alert("Available qty is 0");
        return false;
    }
}

$(document).on("keydown", ".POItem", function (e) {
    console.log(itemMasterArrayPO);
    $(this).autocomplete({
        source: itemMasterArrayPO,
        select: function (event, ui) {
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (!CheckItemAlreadyAddedPO(ui.item.label, rowIndex)) {
                if (IsItemAvailablePO(ui.item.label)) {
                    SetSelectedRowPO(this, ui.item.label);
                }
                else {
                    $(this).val("");
                }
            }
            else {
                $("#alertMessage").text("Item Already added");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
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
            exists = $.inArray(val, itemMasterArrayPO);
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (exists < 0) {
                $(this).val("");
                $(this).focus();

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
            else {
                if (!CheckItemAlreadyAddedPO($(this).val(), rowIndex)) {
                    if (IsItemAvailablePO($(this).val())) {
                        SetSelectedRowPO(this, $(this).val());
                    }
                    else {
                        $(this).val("");
                        $(this).focus();
                    }
                }
                else {
                    $(this).val("");
                    $(this).focus();
                }
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
$(document).on("keydown", ".POName", function (e) {
    $(this).autocomplete({
        source: itemMasterArrayPOByName,
        select: function (event, ui) {
            var itemArr = itemMasterDetailsPOByName[ui.item.label];
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (!CheckItemAlreadyAddedPO(itemArr[0], rowIndex)) {
                if (IsItemAvailablePO(itemArr[0])) {
                    SetSelectedRowPOByName(this, ui.item.label);
                }
                else {
                    $(this).val("");
                }

            }
            else {
                $("#alertMessage").text("Item Already added");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
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
            exists = $.inArray(val, itemMasterArrayPOByName);
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (exists < 0) {
                $(this).val("");
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
            else {
                var itemArr = itemMasterDetailsPOByName[$(this).val()];
                if (!CheckItemAlreadyAddedPO(itemArr[0], rowIndex)) {
                    if (IsItemAvailablePO(itemArr[0])) {
                        SetSelectedRowPOByName(this, $(this).val());
                    }
                    else {
                        $(this).val("");
                        $(this).focus();
                    }

                }
                else {
                    $(this).val("");
                    $(this).focus();
                }
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
$(document).on("keydown", ".POTaxPer", function (e) {
    $(this).autocomplete({
        source: itemTaxCodes,
        select: function (event, ui) {
            event.preventDefault();
            setSelectedTaxCode(this, ui.item.label);

        },
        change: function (event, ui) {

            val = $.trim($(this).val());
            exists = $.inArray(parseInt(val), itemTaxCodevalues);
            if (exists < 0) {
                $(this).val("");
                return false;
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
        var itemArr = itemMasterDetailsPO[row.cells[1].getElementsByTagName("input")[0].value];
        var rate = row.cells[5].getElementsByTagName("input")[0].value;
        var qnty = row.cells[3].getElementsByTagName("input")[0].value;
        var discountPer = row.cells[6].getElementsByTagName("input")[0].value;
        var discountAmt = row.cells[7].getElementsByTagName("input")[0].value;
        var taxPer = row.cells[8].getElementsByTagName("input")[0].value;
        if (qnty != "" && (discountPer != "" || discountAmt != "") && taxPer != "") {
            var rowTotalRate = (parseInt(qnty) * parseFloat(rate)).toFixed(itemArr[9]);

            if ($(txtBox).attr("id") == "PODiscAmt") {
                if (parseFloat(rowTotalRate) != 0) {
                    discountPer = ((parseFloat(discountAmt) / parseFloat(rowTotalRate)) * 100).toFixed(itemArr[9]);
                }
                else {
                    discountPer = 0;
                }

            }
            else {
                discountAmt = ((rowTotalRate * parseFloat(discountPer)) / 100).toFixed(itemArr[9]);
            }
            var taxAmount = (((rowTotalRate - discountAmt) / 100) * taxPer).toFixed(itemArr[9]);
            var netAmount = ((rowTotalRate - discountAmt) + parseFloat(taxAmount)).toFixed(itemArr[9]);
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
                POItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, rowTotalRate, orderAmount];
                if ($("#POTotalAmount").val() == "") {
                    $("#POTotalAmount").val(rowTotalRate);
                    $("#Amount").val(orderAmount);
                }
                else {
                    $("#POTotalAmount").val(parseFloat($("#POTotalAmount").val()) + parseFloat(rowTotalRate));
                    $("#Amount").val(parseFloat($("#Amount").val()) + parseFloat(orderAmount));
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
                totalAmt += parseFloat(rowTotalRate);
                totalDiscount += parseFloat(discountAmt);
                totalTax += parseFloat(taxAmount);
                totalOder += parseFloat(orderAmount);
                row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                $("#POTotalAmount").val(totalAmt.toFixed(itemArr[9]));
                $("#Amount").val(totalOder.toFixed(itemArr[9]));
                $("#POTotalDiscountAmt").val(totalDiscount.toFixed(itemArr[9]));
                $("#POTotalTaxAmt").val(totalTax.toFixed(itemArr[9]));
                $("#POTotalOrderAmt").val(totalOder.toFixed(itemArr[9]));
                POItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, rowTotalRate, orderAmount];
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
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[4].getElementsByTagName("input")[0].value = itemArr[4];
    row.cells[6].getElementsByTagName("input")[0].value = 0;
    row.cells[7].getElementsByTagName("input")[0].value = 0;
    row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
    row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
    return false;
}
function SetSelectedRowSalesReturnByName(lnk, selectedItem) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var itemArr = itemMasterDetailsSalesReturnByName[selectedItem];
    row.cells[1].getElementsByTagName("input")[0].value = itemArr[0];
    row.cells[5].getElementsByTagName("input")[0].value = itemArr[3];
    row.cells[4].getElementsByTagName("input")[0].value = itemArr[4];
    row.cells[6].getElementsByTagName("input")[0].value = 0;
    row.cells[7].getElementsByTagName("input")[0].value = 0;
    row.cells[8].getElementsByTagName("input")[0].value = itemArr[6];
    row.cells[8].getElementsByTagName("input")[1].value = itemArr[5];
    return false;
}

function CheckItemAlreadyAddedSR(val, rowIndex) {
    var flag = false;
    var rowIndexCurrent = -1;
    $("tr", $("#SalesReturnDetail")).each(function () {
        var row = this.parentNode.parentNode;
        if (val == $("input[id*='SRItem']", $(this)).val() && rowIndex !== rowIndexCurrent) {
            flag = true;
        }
        rowIndexCurrent = rowIndexCurrent + 1;
    });
    return flag;
}
function IsItemAvailableSR(item) {
    var itemArr = itemMasterQuantitySR[item];
    var itemArray = itemMasterArraySalesReturn[item];
    if (parseInt(itemArray[10]) == 0) {
        return true;
    }
    else if (parseInt(itemArr[0]) > 0) {
        return true;
    }
    else {
        $("#alertMessage").text("Available qty is 0");
        var theDialog = $("#dialog-alert").dialog(opt);
        theDialog.dialog("open");
        return false;
    }
}

$(document).on("keydown", ".SRItem", function (e) {
    $(this).autocomplete({
        source: itemMasterArraySalesReturn,
        select: function (event, ui) {
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (!CheckItemAlreadyAddedSR(ui.item.label, rowIndex)) {
                SetSelectedRowSalesReturn(this, ui.item.label);
            }
            else {
                $("#alertMessage").text("Item Already added");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
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
            exists = $.inArray(val, itemMasterArraySalesReturn);
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (exists < 0) {
                $(this).val("");
                $(this).focus();

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
            else {
                if (!CheckItemAlreadyAddedSR($(this).val(), rowIndex)) {
                    SetSelectedRowSalesReturn(this, $(this).val());
                    
                }
                else {
                    $(this).val("");
                    $(this).focus();
                }
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
$(document).on("keydown", ".SRItemName", function (e) {
    $(this).autocomplete({
        source: itemMasterArraySalesReturnByName,
        select: function (event, ui) {
            var itemArr = itemMasterDetailsSalesReturnByName[ui.item.label];
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (!CheckItemAlreadyAddedSR(itemArr[0], rowIndex)) {
                SetSelectedRowSalesReturnByName(this, ui.item.label);
               
            }
            else {
                $("#alertMessage").text("Item Already added");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
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
            exists = $.inArray(val, itemMasterArraySalesReturnByName);
            var row = this.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            if (exists < 0) {
                $(this).val("");
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
            else {
                var itemArr = itemMasterDetailsSalesReturnByName[$(this).val()];
                if (!CheckItemAlreadyAddedSR(itemArr[0], rowIndex)) {
                    SetSelectedRowSalesReturnByName(this, $(this).val());
                    
                }
                else {
                    $(this).val("");
                    $(this).focus();
                }
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
$(document).on("keydown", ".SRTaxPer", function (e) {
    $(this).autocomplete({
        source: itemTaxCodes,
        select: function (event, ui) {
            event.preventDefault();
            setSelectedTaxCode(this, ui.item.label);

        },
        change: function (event, ui) {

            val = $.trim($(this).val());
            exists = $.inArray(parseInt(val), itemTaxCodevalues);
            if (exists < 0) {
                $(this).val("");
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
$(document).on("focusout", "#Demurages", function (e) {
    if($(this).val()!="")
    {
        $("#SRTotalOrderAmt").val((parseFloat($("#SRCorrectTotalOrderAmtHidden").val()) - parseFloat($("#Demurages").val())).toFixed(parseInt($("#currencyDecimal").val())));
        $("#Amount").val(parseFloat(parseFloat($("#SRCorrectTotalOrderAmtHidden").val()) - parseFloat($("#Demurages").val())).toFixed(parseInt($("#currencyDecimal").val())));
    }
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
    var row = this.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (row.cells[1].getElementsByTagName("input")[0].value != ""
        && $.trim(row.cells[3].getElementsByTagName("input")[0].value) != "") {
        CalculateSRAmount(this);
    }
    else {
        if (row.cells[1].getElementsByTagName("input")[0].value != "")
        {
            row.cells[3].getElementsByTagName("input")[0].value = "";
            row.cells[3].getElementsByTagName("input")[0].focus();
            $("#alertMessage").text("Please fill an item with quantity");
            var theDialog = $("#dialog-alert").dialog(opt);
            theDialog.dialog("open");
        }
        
        return false;
    }
    
});
function CalculateSRAmount(txtBox) {
    var row = txtBox.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    if (row.cells[1].getElementsByTagName("input")[0].value != "") {
        var itemArr = itemMasterDetailsSalesReturn[row.cells[1].getElementsByTagName("input")[0].value];
        var rate = row.cells[5].getElementsByTagName("input")[0].value;
        var qnty = row.cells[3].getElementsByTagName("input")[0].value;
        var discountPer = row.cells[6].getElementsByTagName("input")[0].value;
        var discountAmt = row.cells[7].getElementsByTagName("input")[0].value;
        var taxPer = row.cells[8].getElementsByTagName("input")[0].value;
        if (qnty != "" && (discountPer != "" || discountAmt != "") && taxPer != "") {
            var rowTotalRate = (parseInt(qnty) * parseFloat(rate)).toFixed(itemArr[9]);
            if ($(txtBox).attr("id") == "SRDiscAmt") {
                if (parseFloat(rowTotalRate) != 0) {
                    discountPer = ((parseFloat(discountAmt) / parseFloat(rowTotalRate)) * 100).toFixed(itemArr[9]);
                }
                else {
                    discountPer = 0;
                }

            }
            else {
                discountAmt = ((rowTotalRate * parseFloat(discountPer)) / 100).toFixed(itemArr[9]);
            }
            var taxAmount = (((rowTotalRate - discountAmt) / 100) * taxPer).toFixed(itemArr[9]);
            var netAmount = ((rowTotalRate - discountAmt) + parseFloat(taxAmount)).toFixed(itemArr[9]);
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
                SRItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, rowTotalRate, orderAmount];
                if ($("#SRTotalAmount").val() == "") {
                    $("#SRTotalAmount").val(rowTotalRate);
                }
                else {
                    $("#SRTotalAmount").val((parseFloat($("#SRTotalAmount").val()) + parseFloat(rowTotalRate)).toFixed(itemArr[9]));
                }
                if ($("#Amount").val() == "0") {
                    $("#Amount").val((netAmount - parseFloat($("#Demurages").val())).toFixed(itemArr[9]));
                }
                else {
                    $("#Amount").val((parseFloat($("#SRCorrectTotalOrderAmtHidden").val()) + parseFloat(netAmount)).toFixed(itemArr[9]));
                }
                if ($("#SRTotalDiscountAmt").val() == "") {
                    $("#SRTotalDiscountAmt").val(discountAmt);
                }
                else {
                    $("#SRTotalDiscountAmt").val((parseFloat($("#SRTotalDiscountAmt").val()) + parseFloat(discountAmt)).toFixed(itemArr[9]));
                }
                if ($("#SRTotalTaxAmt").val() == "") {
                    $("#SRTotalTaxAmt").val(taxAmount);
                }
                else {
                    $("#SRTotalTaxAmt").val((parseFloat($("#SRTotalTaxAmt").val()) + parseFloat(taxAmount)).toFixed(itemArr[9]));
                }
                if ($("#SRTotalOrderAmt").val() == "") {
                    $("#SRCorrectTotalOrderAmtHidden").val(orderAmount);
                    $("#SRTotalOrderAmt").val((orderAmount-parseFloat($("#Demurages").val())).toFixed(itemArr[9]));
                }
                else {
                   
                    $("#SRTotalOrderAmt").val(((parseFloat($("#SRCorrectTotalOrderAmtHidden").val()) + parseFloat(orderAmount)) - parseFloat($("#Demurages").val())).toFixed(itemArr[9]));
                    $("#SRCorrectTotalOrderAmtHidden").val(parseFloat($("#SRCorrectTotalOrderAmtHidden").val()) + parseFloat(orderAmount));
                }
            }
            else {
                var itemArray = SRItemRowDetails[rowIndex];
                var totalAmt = parseFloat($("#SRTotalAmount").val());
                var totalDiscount = parseFloat($("#SRTotalDiscountAmt").val());
                var totalTax = parseFloat($("#SRTotalTaxAmt").val());
                var totalOder = parseFloat($("#SRCorrectTotalOrderAmtHidden").val());
                var oldAmount = parseFloat(itemArray[6]);
                var oldDiscount = parseFloat(itemArray[3]);
                var oldTax = parseFloat(itemArray[5]);
                var oldOder = parseFloat(itemArray[7]);
                totalAmt -= parseFloat(oldAmount);
                totalDiscount -= parseFloat(oldDiscount);
                totalTax -= parseFloat(oldTax);
                totalOder -= parseFloat(oldOder);
                totalAmt += parseFloat(rowTotalRate);
                totalDiscount += parseFloat(discountAmt);
                totalTax += parseFloat(taxAmount);
                totalOder += parseFloat(orderAmount);
                $("#SRCorrectTotalOrderAmtHidden").val(totalOder.toFixed(itemArr[9]));
                totalOder -= parseFloat($("#Demurages").val());
                row.cells[10].getElementsByTagName("input")[0].value = netAmount;
                $("#Amount").val(totalOder.toFixed(itemArr[9]));
                $("#SRTotalAmount").val(totalAmt.toFixed(itemArr[9]));
                $("#SRTotalDiscountAmt").val(totalDiscount.toFixed(itemArr[9]));
                $("#SRTotalTaxAmt").val(totalTax.toFixed(itemArr[9]));
                $("#SRTotalOrderAmt").val(totalOder.toFixed(itemArr[9]));
                SRItemRowDetails[rowIndex] = [qnty, rate, discountPer, discountAmt, taxPer, taxAmount, rowTotalRate, orderAmount];
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
function CreateNewRowSR() {
    if (parseInt($("#rowCount").val()) == 1) {
        $("tr", $("#SalesReturnDetail")).each(function () {
            var val = $("input[id*='SRItem']", $(this)).val();
            if (typeof (val) !== "undefined" && val != "undefined") {
                $("a[id*='lnkDeleteSR']", $(this)).css("display", "block");
            }


        });
    }
    $("#rowCount").val(parseInt($("#rowCount").val()) + 1);
    var row = '<tr><td><span>' + $("#rowCount").val() + '</span></td><td><input name="SRItem" type="text" id="SRItem" class="form-control SRItem required" style="width:70px;"></td><td><input name="SRItemName" type="text" id="SRItemName" class="form-control SRItemName required" style="width:70px;" ></td><td><input name="SRQuantity" type="text" id="SRQuantity" class="form-control SRQuantity txtNumeric required" style="width:70px;"></td><td><input name="SRUnit" type="text" id="SRUnit" class="form-control SRUnit" style="width:70px;" readonly="readonly"></td><td><input name="SRItemRate" type="text" id="SRItemRate" class="form-control SRItemRate txtNumeric required" style="width:70px;"></td><td><input name="SRDiscPer" type="text" id="SRDiscPer" class="form-control SRDiscPer txtNumeric" style="width:70px;"></td><td><input name="SRDiscAmt" type="text" id="SRDiscAmt" class="form-control SRDiscAmt txtNumeric required" style="width:70px;"></td><td><input name="SRTaxPer" type="text" id="SRTaxPer" class="form-control SRTaxPer required" style="width:70px;"><input type="hidden" name="SRTaxCode" id="SRTaxCode"></td><td><input name="SRTaxAmt" type="text" id="SRTaxAmt" class="form-control SRTaxAmt " style="width:70px;" readonly="readonly"></td><td><input name="SRNetAmt" type="text" id="SRNetAmt" class="form-control SRNetAmt" style="width:50px;" readonly="readonly"></td><td><a id="lnkDeleteSR" data-id="0" >Delete</a></td></tr>';
    $("#SalesReturnDetail tbody").append(row);
}
$(document).on("keydown", "#SRNetAmt", function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode == 9 && $("#SalesReturnType").val() == 1 && $("#Status").val() != 2) {
        var row = this.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        if (row.cells[1].getElementsByTagName("input")[0].value != ""
            && $.trim(row.cells[3].getElementsByTagName("input")[0].value) != "") {
            CreateNewRowSR();
        }
        else {
            $("#alertMessage").text("Please fill an item with quantity");
            var theDialog = $("#dialog-alert").dialog(opt);
            theDialog.dialog("open");
            return false;
        }
        
    }
});
function CreateNewRowSQ() {
    if (parseInt($("#rowCount").val()) == 1) {
        $("tr", $("#SalesQuotationDetail")).each(function () {
            var val = $("input[id*='SQItem']", $(this)).val();
            if (typeof (val) !== "undefined" && val != "undefined") {
                $("a[id*='lnkDeleteSQ']", $(this)).css("display", "block");
            }


        });
    }
    $("#rowCount").val(parseInt($("#rowCount").val()) + 1);
    var row = '<tr><td><span>' + $("#rowCount").val() + '</span></td><td><input name="SQItem" type="text" id="SQItem" class="form-control SQItem gridTxtBox required" style="width:70px;"></td><td><input name="SQName" type="text" id="SQName" class="form-control SQName gridTxtBox required"></td><td><input name="SQQuantity" type="text" id="SQQuantity" class="form-control SQQuantity gridTxtBox txtNumeric required"></td><td><input name="SQUnit" type="text" id="SQUnit" class="form-control SQUnit gridTxtBox required" readonly="readonly"></td><td><input name="SQRate" type="text" id="SQRate" class="form-control SQRate gridTxtBox txtNumeric required"></td><td><input name="SQDiscPer" type="text" id="SQDiscPer" class="form-control gridTxtBox SQDiscPer txtNumeric required"></td><td><input name="SQDiscAmt" type="text" id="SQDiscAmt" class="form-control SQDiscAmt gridTxtBox txtNumeric required"></td><td><input name="SQTaxPer" type="text" id="SQTaxPer" class="form-control SQTaxPer gridTxtBox required"><input type="hidden" name="SQTaxCode" id="SQTaxCode"></td><td><input name="SQTaxAmt" type="text" id="SQTaxAmt" class="form-control SQTaxAmt gridTxtBox required" readonly="readonly"></td><td><input name="SQNetAmt" type="text" id="SQNetAmt" class="form-control SQNetAmt gridTxtBox required" readonly="readonly" style="width:50px;"></td><td><a id="lnkDeleteSQ" style="cursor:pointer" >Delete</a><input type="hidden" name="ID" value="0" /></td></tr>';
    $("#SalesQuotationDetail tbody").append(row);
}
$(document).on("keydown", "#SQNetAmt", function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode == 9 && $("#SalesOrder").val() == "") {
        var row = this.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        if (row.cells[1].getElementsByTagName("input")[0].value != ""
            && $.trim(row.cells[3].getElementsByTagName("input")[0].value) != "") {
            CreateNewRowSQ();
        }
        else
        {
            $("#alertMessage").text("Please fill an item with quantity");
            var theDialog = $("#dialog-alert").dialog(opt);
            theDialog.dialog("open");
            return false;
        }
    }
});
function CreateNewRowPriceBook() {
    if (parseInt($("#rowCount").val()) == 1) {
        $("tr", $("#PriceBookDetail")).each(function () {
            var val = $("input[id*='ItemCode']", $(this)).val();
            if (typeof (val) !== "undefined" && val != "undefined") {
                $("a[id*='lnkDelete']", $(this)).css("display", "block");
            }


        });
    }
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
    if (parseInt($("#rowCount").val()) == 1) {
        $("tr", $("#StockEntryDetail")).each(function () {
            var val = $("input[id*='Item']", $(this)).val();
            if (typeof (val) !== "undefined" && val != "undefined") {
                $("a[id*='lnkDeleteSE']", $(this)).css("display", "block");
            }


        });
    }
    $("#rowCount").val(parseInt($("#rowCount").val()) + 1);
    var row = '<tr><td><span style="display:inline-block;width:50px;">' + $("#rowCount").val() + '</span></td><td><input name="Item" type="text" id="Item" class="form-control StockItem required" style="width:100px;" aria-required="true"></td><td><input name="Name" type="text" id="Name" class="form-control StockName required" style="width:100px;" aria-required="true"></td><td><input name="SEQuantity" type="text" id="SEQuantity" class="form-control StockQuantity txtNumeric required" style="width:100px;" aria-required="true"></td><td><input name="Unit" type="text" id="Unit" class="form-control StockUnit required" style="width:100px;" readonly="readonly" aria-required="true"></td><td><input name="SERate" type="text" id="SERate" class="form-control StockRate txtNumeric required" style="width:100px;" aria-required="true"></td><td><input name="SEAmount" type="text" id="SEAmount" class="form-control StockAmount required" style="width:100px;" readonly="readonly" aria-required="true"></td><td><a id="lnkDeleteSE" data-id="0">Delete</a><input type="hidden" name="ID" value="0" /></td></tr>';
    $("#StockEntryDetail tbody").append(row);
}
$(document).on("keydown", "#SEAmount", function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode == 9 && $("#Status").val() != 2) {
        var row = this.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        if (row.cells[1].getElementsByTagName("input")[0].value != ""
            && $.trim(row.cells[3].getElementsByTagName("input")[0].value) != "") {
            CreateNewRowStockEntry();
        }
        else {
            $("#alertMessage").text("Please fill an item with quantity");
            var theDialog = $("#dialog-alert").dialog(opt);
            theDialog.dialog("open");
            return false;
        }
        
    }
});
function CreateNewRowInvoice() {
    if (parseInt($("#rowCount").val()) == 1) {
        $("tr", $("#InvoiceDetail")).each(function () {
            var val = $("input[id*='IItem']", $(this)).val();
            if (typeof (val) !== "undefined" && val != "undefined") {
                $("a[id*='lnkDeleteSI']", $(this)).css("display", "block");
            }


        });
    }
    $("#rowCount").val(parseInt($("#rowCount").val()) + 1);
    var row = '<tr class="Odd"><td><span>' + $("#rowCount").val() + '</span></td><td><input name="IItem" type="text" id="IItem" class="form-control IItem required" style="width:70px;" aria-required="true"></td><td><input name="IItemName" type="text" id="IItemName" class="form-control IItemName required" style="width:70px;" aria-required="true"></td><td><input name="IQuantity" type="text" id="IQuantity" class="form-control IQuantity txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="IUnit" type="text" id="IUnit" class="form-control IUnit required" style="width:70px;" readonly="readonly" aria-required="true"></td><td><input name="IItemRate" type="text" id="IItemRate" class="form-control IItemRate txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="IDiscPer" type="text" id="IDiscPer" class="form-control IDiscPer txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="IDiscAmt" type="text" id="IDiscAmt" class="form-control IDiscAmt txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="ITaxPer" type="text" id="ITaxPer" class="form-control ITaxPer required" style="width:70px;" aria-required="true"><input type="hidden" name="ITaxCode" id="ITaxCode"></td><td><input name="ITaxAmt" type="text" id="ITaxAmt" class="form-control ITaxAmt required" style="width:70px;" readonly="readonly" aria-required="true"></td><td><input name="INetAmt" type="text" id="INetAmt" class="form-control INetAmt required" style="width:50px;" readonly="readonly" aria-required="true"></td><td><a id="lnkDeleteSI" style="cursor:pointer" >Delete</a><input type="hidden" name="ID" value="0" /></td></tr>';
    $("#InvoiceDetail tbody").append(row);
}
$(document).on("keydown", "#INetAmt", function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode == 9 && $("#Status").val() != 2) {
        var row = this.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        if (row.cells[1].getElementsByTagName("input")[0].value != ""
            && $.trim(row.cells[3].getElementsByTagName("input")[0].value) != "") {
            CreateNewRowInvoice();
        }
        else {
            $("#alertMessage").text("Please fill an item with quantity");
            var theDialog = $("#dialog-alert").dialog(opt);
            theDialog.dialog("open");
            return false;
        }
    }
});
function CreateNewRowManualInvoice() {
    if (parseInt($("#rowCount").val()) == 1) {
        $("tr", $("#ManualInvoiceDetail")).each(function () {
            var val = $("input[id*='MIItem']", $(this)).val();
            if (typeof (val) !== "undefined" && val != "undefined") {
                $("a[id*='lnkDeleteMI']", $(this)).css("display", "block");
            }


        });
    }
    $("#rowCount").val(parseInt($("#rowCount").val()) + 1);
    var row = '<tr><td><span>' + $("#rowCount").val() + '</span></td><td><input name="MIItem" type="text" id="MIItem" class="form-control MIItem required" style="width:70px;" aria-required="true"></td><td><input name="MIItemName" type="text" id="MIItemName" class="form-control MIItemName required" style="width:70px;" aria-required="true"></td><td><input name="MIQuantity" type="text" id="MIQuantity" class="form-control MIQuantity txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="MIUnit" type="text" id="MIUnit" class="form-control MIUnit required" style="width:70px;" readonly="readonly" aria-required="true"></td><td><input name="MIItemRate" type="text" id="MIItemRate" class="form-control MIItemRate txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="MIDiscPer" type="text" id="MIDiscPer" class="form-control txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="MIDiscAmt" type="text" id="MIDiscAmt" class="form-control MIDiscAmt txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="MITaxPer" type="text" id="MITaxPer" class="form-control MITaxPer txtNumeric required" style="width:70px;" aria-required="true"></td><td><input name="MITaxAmt" type="text" id="MITaxAmt" class="form-control MITaxAmt txtNumeric required" style="width:70px;" readonly="readonly" aria-required="true"></td><td><input name="MINetAmt" type="text" id="MINetAmt" class="form-control MINetAmt required" style="width:50px;" readonly="readonly" aria-required="true"></td><td><a id="lnkDeleteMI" style="cursor:pointer" >Delete</a><input type="hidden" name="ID" value="0" /></td></tr>';
    $("#ManualInvoiceDetail tbody").append(row);
}
$(document).on("keydown", "#MINetAmt", function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode == 9) {
        var row = this.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        if (row.cells[1].getElementsByTagName("input")[0].value != ""
            && $.trim(row.cells[3].getElementsByTagName("input")[0].value) != "") {
            CreateNewRowManualInvoice();
        }
        else {
            $("#alertMessage").text("Please fill an item with quantity");
            var theDialog = $("#dialog-alert").dialog(opt);
            theDialog.dialog("open");
            return false;
        }
        
    }
});
function CreateNewRowPO() {
    if (parseInt($("#rowCount").val()) == 1) {
        $("tr", $("#PurchaseOrderDetail")).each(function () {
            var val = $("input[id*='POItem']", $(this)).val();
            if (typeof (val) !== "undefined" && val != "undefined") {
                $("a[id*='lnkDeletePO']", $(this)).css("display", "block");
            }


        });
    }
    $("#rowCount").val(parseInt($("#rowCount").val()) + 1);
    var row = '<tr class="Odd"><td><span>' + $("#rowCount").val() + '</span></td><td><input name="POItem" type="text" id="POItem" class="form-control POItem required" style="width:70px;" aria-required="true"></td><td><input name="POName" type="text" id="POName" class="form-control POName required" aria-required="true"></td><td><input name="POQuantity" type="text" id="POQuantity" class="form-control POQuantity txtNumeric required" aria-required="true"></td><td><input name="POUnit" type="text" id="POUnit" readonly="readonly"  class="form-control POUnit required" aria-required="true"></td><td><input name="PORate" type="text" id="PORate" class="form-control PORate txtNumeric required" aria-required="true"></td><td><input name="PODiscPer" type="text" id="PODiscPer" class="form-control PODiscPer txtNumeric required" aria-required="true"></td><td><input name="PODiscAmt" type="text" id="PODiscAmt" class="form-control PODiscAmt txtNumeric"></td><td><input name="POTaxPer" type="text" id="POTaxPer" class="form-control POTaxPer required" aria-required="true"><input type="hidden" name="POTaxCode" id="POTaxCode"></td><td> <input name="POTaxAmt" type="text" id="POTaxAmt" class="form-control POTaxAmt required" readonly="readonly"  aria-required="true"></td><td><input name="PONetAmt" type="text" readonly="readonly" id="PONetAmt" class="form-control PONetAmt required" style="width:50px;" aria-required="true"></td><td><a id="lnkDeletePO" style="cursor:pointer" >Delete</a><input type="hidden" name="ID" value="0" /></td></tr>';
    $("#PurchaseOrderDetail tbody").append(row);
}
$(document).on("keydown", "#PONetAmt", function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode == 9 && $("#Status").val() != 2) {
        var row = this.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;
        if (row.cells[1].getElementsByTagName("input")[0].value != ""
            && $.trim(row.cells[3].getElementsByTagName("input")[0].value) != "") {
            CreateNewRowPO();
        }
        else {
            $("#alertMessage").text("Please fill an item with quantity");
            var theDialog = $("#dialog-alert").dialog(opt);
            theDialog.dialog("open");
            return false;
        }
    }
});
$(function () {
    if (parseInt($("#PriceBookId").val()) > 0) {
        GetPriceBook(parseInt(1), 0);
    }
});
$(document).on("keyup", "#ItemSearch", function () {
    $("#ItemNameSearch").val('');
    $("#ItemSCSearch").val('');
    GetPriceBook(parseInt(1), 1);
});
$(document).on("keyup", "#ItemNameSearch", function () {
    $("#ItemSearch").val('');
    $("#ItemSCSearch").val('');
    GetPriceBook(parseInt(1), 2);
});
$(document).on("keyup", "#ItemSCSearch", function () {
    $("#ItemSearch").val('');
    $("#ItemNameSearch").val('');
    GetPriceBook(parseInt(1), 3);
});
function SearchTerm(filterItem) {
    if (filterItem == 0) {
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
function GetPriceBook(pageIndex, filterItem) {
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
            if (priceBookDetails.length == 1) {
                row1 = '<tr><td><span>' + $("#rowCount").val() + '</span></td><td><input name="ItemCode" readonly="readonly" type="text" id="ItemCode" class="form-control ItemCode required" value=' + $(this).find("ItemCode").text() + '></td><td><input name="Description" type="text" readonly="readonly" id="Description" class="form-control valid" aria-invalid="false" value=' + $(this).find("Name").text() + '></td><td><input name="SupplierBarcode"  readonly="readonly" type="text" id="SupplierBarcode" class="form-control SupplierBarcode required" readonly="readonly" value=' + $(this).find("SupplierBarcode").text() + '></td><td><input name="CurrencyCode" type="text" readonly="readonly" id="CurrencyCode" class="form-control CurrencyCode required" readonly="readonly" value=' + $(this).find("CurrencyCode").text() + '></td><td><input name="MRP" type="text" id="MRP" class="form-control required" value=' + $(this).find("MRP").text() + '></td><td><input name="Price" type="text" id="Price" class="form-control priceBookPricetxt required" value=' + $(this).find("Price").text() + '></td><td><input type="hidden" name="ID" value="' + $(this).find("ID").text() + '" /></td></tr>';

            }
            else {
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
$(document).on("keydown", "#CustomerIdMI", function (e) {

    $(this).autocomplete({
        source: customerCodes,
        change: function (event, ui) {
            val = $(this).val();
            exists = $.inArray(val, customerCodes);
            if (exists < 0) {
                $(this).val("");
                $("#Name").val("");
                $("#Telephone").val("");
                $("#Invoice").val("");
                $("#MISequenceNoID").val("");
                return false;
            }
            else {
                var item = customerCodesWithDetails[val];
                $("#CustomerIdMI").val(item[3]);
                $("#Name").val(item[0]);
                $("#Telephone").val(item[1]);
                $("#InvoiceType").val(item[2]);
                $('#InvoiceType option[value="0"]').attr("selected", null);
                $('#InvoiceType option[value="1"]').attr("selected", null);
                $('#InvoiceType option[value="' + item[2] + '"]').attr("selected", "selected");
                $("#selectedInvoiceType").val(item[2]);
                $.each(firstFreeMI, function (i, j) {
                    if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == item[2]) {
                        $("#Invoice").val(j.sequenceNumber);
                        $("#MISequenceNoID").val(j.id);
                    }
                    else if (j.seqType == "0" && j.orderType == item[2]) {
                        $("#Invoice").val(j.sequenceNumber);
                        $("#MISequenceNoID").val(j.id);
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
                        itemMasterArrayManualInvoice = [];
                        itemMasterDetailsManualInvoice = {};
                        itemMasterArrayManualInvoiceByName = [];
                        itemMasterDetailsManualInvoiceByName = {};
                        $.each(data.d, function (i, j) {
                            itemMasterArrayManualInvoice.push(j.code);
                            itemMasterDetailsManualInvoice[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];
                            itemMasterArrayManualInvoiceByName.push(j.name);
                            itemMasterDetailsManualInvoiceByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];

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
$(document).on("keydown", "#CustomerIdSI", function (e) {

    $(this).autocomplete({
        source: customerCodes,
        change: function (event, ui) {
            val = $(this).val();
            exists = $.inArray(val, customerCodes);
            if (exists < 0) {
                $(this).val("");
                $("#Name").val("");
                $("#Telephone").val("");
                $("#Invoice").val("");
                $("#SISequenceNoID").val("");
                return false;
            }
            else {
                var item = customerCodesWithDetails[val]; 
                $("#CustomerIdSI").val(item[3]);
                $("#Name").val(item[0]);
                $("#Telephone").val(item[1]);
                $("#InvoiceType").val(item[2]);
                $('#InvoiceType option[value="0"]').attr("selected", null);
                $('#InvoiceType option[value="1"]').attr("selected", null);
                $('#InvoiceType option[value="' + item[2] + '"]').attr("selected", "selected");
                $("#selectedInvoiceType").val(item[2]);
                $.each(firstFreeSI, function (i, j) {
                    if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == item[2]) {
                        $("#Invoice").val(j.sequenceNumber);
                        $("#SISequenceNoID").val(j.id);
                    }
                    else if (j.seqType == "0" && j.orderType == item[2]) {
                        $("#Invoice").val(j.sequenceNumber);
                        $("#SISequenceNoID").val(j.id);
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
                        itemMasterArrayInvoice = [];
                        itemMasterDetailsInvoice = {};
                        itemMasterQuantityI = {};
                        itemMasterArrayInvoiceByName = [];
                        itemMasterDetailsInvoiceByName = {};
                        $.each(data.d, function (i, j) {
                            itemMasterArrayInvoice.push(j.code);
                            itemMasterDetailsInvoice[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];
                            itemMasterQuantityI[j.code] = [j.Qnty];
                            itemMasterArrayInvoiceByName.push(j.name);
                            itemMasterDetailsInvoiceByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];

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
$(document).on("keydown", "#SupplierId", function (e) {

    $(this).autocomplete({
        source: customerCodes,
        change: function (event, ui) {
            val = $(this).val();
            exists = $.inArray(val, customerCodes);
            if (exists < 0) {
                $(this).val("");
                $("#Name").val("");
                $("#Telephone").val("");
                $("#OrderNo").val("");
                $("#POSequenceNoID").val("");
                return false;
            }
            else {
                var item = customerCodesWithDetails[val];
                $("#SupplierId").val(item[3]);
                $("#Name").val(item[0]);
                $("#Telephone").val(item[1]);
                $("#OrderType").val(item[2]);
                $('#OrderType option[value="0"]').attr("selected", null);
                $('#OrderType option[value="1"]').attr("selected", null);
                $('#OrderType option[value="' + item[2] + '"]').attr("selected", "selected");
                $("#selectedOrderType").val(item[2]);
                $.each(firstFreePO, function (i, j) {
                    if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == item[2]) {
                        $("#OrderNo").val(j.sequenceNumber);
                        $("#POSequenceNoID").val(j.id);
                    }
                    else if (j.seqType == "0" && j.orderType == item[2]) {
                        $("#OrderNo").val(j.sequenceNumber);
                        $("#POSequenceNoID").val(j.id);
                    }
                });
                var obj1 = {};
                obj1.companyCode = $.trim($("#CompanyCode").val());
                obj1.orderType = item[2];
                obj1.priceType = 1;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "SQEdit.aspx/GetItemMasters",
                    data: JSON.stringify(obj1),
                    dataType: "json",
                    success: function (data) {
                        
                        itemMasterArrayPO = [];
                        itemMasterDetailsPO = {};
                        itemMasterQuantityPO = {};
                        itemMasterArrayPOByName = [];
                        itemMasterDetailsPOByName = {};
                        $.each(data.d, function (i, j) {
                            itemMasterArrayPO.push(j.code);
                            itemMasterDetailsPO[j.code] = [j.name, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];
                            itemMasterQuantityPO[j.code] = [j.Qnty];
                            itemMasterArrayPOByName.push(j.name);
                            itemMasterDetailsPOByName[j.name] = [j.code, j.supplierBarcode, j.mrp, j.retailPrice, j.BaseUnitCode, j.TaxCode, j.TaxPer, j.Qnty, j.currencyCode, j.decimalPoint, j.itemType];

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
$(document).on("keydown", "#SupplierIdGrn", function (e) {

    $(this).autocomplete({
        source: customerCodes,
        change: function (event, ui) {
            val = $(this).val();
            exists = $.inArray(val, customerCodes);
            if (exists >= 0) {
                var item = customerCodesWithDetails[val];
                $("#SupplierIdGrn").val(item[3]);
                $("#selectedSupplier").val(item[3]);
                $("#buttonUpdate").click();
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
$(document).on("click", "#btnFinalizeI", function (e) {
    $("tr", $("#InvoiceDetail")).each(function () {

        var val = $("input[id*='IItem']", $(this)).val();
        var qty = parseInt($("input[id*='IQuantity']", $(this)).val());
        var itemArr = itemMasterQuantityI[val];
        if (parseInt(itemArr[0]) < qty) {
            $("#alertMessage").text(val + " :Insufficient Qty, Available qty is" + itemArr[0]);
            var theDialog = $("#dialog-alert").dialog(opt);
            theDialog.dialog("open");
            return false;
        }
    });
});
$("#AdjustmentType").change(function () {

    if ($("#Document").val() != "") {
        var val = this.value;
        $.each(firstFreeSE, function (i, j) {
            if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == val) {
                $("#Document").val(j.sequenceNumber);
                $("#SESequenceNoID").val(j.id);
            }

        });
    }
    return false;
});
$(document).on("keydown", "#ItemCodeSR", function (e) {
    $(this).autocomplete({
        source: itemDetailsStockRegCodeArray,
        select: function (event, ui) {
            var itemArr = itemDetailsStockRegCode[ui.item.label];
            if (itemArr[1] == "0") {
                $("#alertMessage").text("Non inventory Item – Not allowed");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
                $(this).val('');
                return false;
            }
            else {
                $("#ItemNameSR").val(itemArr[0]);
            }
        },
        change: function (event, ui) {

            val = $(this).val();
            exists = $.inArray(val, itemDetailsStockRegCodeArray);
            if (exists >= 0) {
                var itemArr = itemDetailsStockRegCode[val];
                if (itemArr[1] == "0") {
                    $("#alertMessage").text("Non inventory Item – Not allowed");
                    var theDialog = $("#dialog-alert").dialog(opt);
                    theDialog.dialog("open");
                    $(this).val('');
                    return false;
                }
                else {
                    $("#ItemNameSR").val(itemArr[0]);
                }
            }
            else {
                $("#ItemCodeSR").val('');
                $("#ItemNameSR").val('');
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
$(document).on("keydown", "#ItemNameSR", function (e) {
    $(this).autocomplete({
        source: itemDetailsStockRegNameArray,
        select: function (event, ui) {
            var itemArr = itemDetailsStockRegName[ui.item.label];
            if (itemArr[1] == "0") {
                $("#alertMessage").text("Non inventory Item – Not allowed");
                var theDialog = $("#dialog-alert").dialog(opt);
                theDialog.dialog("open");
                $(this).val('');
                return false;
            }
            else {
                $("#ItemCodeSR").val(itemArr[0]);
            }
        },
        change: function (event, ui) {

            val = $(this).val();
            exists = $.inArray(val, itemDetailsStockRegNameArray);
            if (exists >= 0) {
                var itemArr = itemDetailsStockRegName[val];
                if (itemArr[1] == "0") {
                    $("#alertMessage").text("Non inventory Item – Not allowed");
                    var theDialog = $("#dialog-alert").dialog(opt);
                    theDialog.dialog("open");
                    $(this).val('');
                    return false;
                }
                else {
                    $("#ItemCodeSR").val(itemArr[0]);
                }
            }
            else {
                $("#ItemCodeSR").val('');
                $("#ItemNameSR").val('');
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
$("#SalesReturnType").change(function () {

    if ($("#SalesReturnType").val() == "0") {
        $("#Location").attr("readonly", "readonly");
        $("#SalesMan").attr("readonly", "readonly");
        $("#SalesOrderNo").attr("readonly", false);
        if(!$("#SalesOrderNo").hasClass("required"))
        {
            $("#SalesOrderNo").addClass("required");
        }
        $("tr", $("#SalesReturnDetail")).each(function () {

            if ($("input[id*='SRItem']", $(this)).hasClass("required")) {
                $("input[id*='SRItem']", $(this)).removeClass("required");
            }
            if ($("input[id*='SRItemName']", $(this)).hasClass("required")) {
                $("input[id*='SRItemName']", $(this)).removeClass("required");
            }
            if ($("input[id*='SRQuantity']", $(this)).hasClass("required")) {
                $("input[id*='SRQuantity']", $(this)).removeClass("required");
            }
        });
        $.each(firstFreeSR, function (i, j) {
            if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == $("#SalesReturnType").val()) {
                $("#SalesReturn").val(j.sequenceNumber);
                $("#SRSequenceNoID").val(j.id);
            }
            else if (j.seqType == "0" && j.orderType == $("#SalesReturnType").val()) {
                $("#SalesReturn").val(j.sequenceNumber);
                $("#SRSequenceNoID").val(j.id);
            }
        });
    }
    else {
        $("#Location").attr("readonly", false);
        $("#SalesMan").attr("readonly", false);
        $("#SalesOrderNo").attr("readonly", "readonly");
        $(".SRItem").attr('readonly', false);
        $(".SRItemName").attr('readonly', false);
        $(".SRItemRate").attr('readonly', false);
        $(".SRDiscPer").attr('readonly', false);
        $(".SRDiscAmt").attr('readonly', false);
        $(".SRTaxPer").attr('readonly', false);
        if ($("#SalesOrderNo").hasClass("required")) {
            $("#SalesOrderNo").removeClass("required");
        }
        $("tr", $("#SalesReturnDetail")).each(function () {

            if (!$("input[id*='SRItem']", $(this)).hasClass("required")) {
                $("input[id*='SRItem']", $(this)).addClass("required");
            }
            if (!$("input[id*='SRItemName']", $(this)).hasClass("required")) {
                $("input[id*='SRItemName']", $(this)).addClass("required");
            }
            if (!$("input[id*='SRQuantity']", $(this)).hasClass("required")) {
                $("input[id*='SRQuantity']", $(this)).addClass("required");
            }
        });
        $.each(firstFreeSR, function (i, j) {
            if (j.seqType == "1" && j.enterpriseUnitCode == $("#LocationHidden").val() && j.orderType == $("#SalesReturnType").val()) {
                $("#SalesReturn").val(j.sequenceNumber);
                $("#SRSequenceNoID").val(j.id);
            }
            else if (j.seqType == "0" && j.orderType == $("#SalesReturnType").val()) {
                $("#SalesReturn").val(j.sequenceNumber);
                $("#SRSequenceNoID").val(j.id);
            }
        });
        if ($("#SalesOrderNo").val()!="")
        {
            $("#buttonUpdate").click();
        }

        
    } 
    
    return false;
});
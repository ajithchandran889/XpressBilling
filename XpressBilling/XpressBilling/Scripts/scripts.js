$(function () {
    $("#inputDate").datepicker(); 
    $("#FormationDate").datepicker();
});


$(document).ready(function () {
    //$.validator.addMethod("valueNotEquals", function (value, element, arg) {
    //    return arg != value;
    //}, "Value must not equal arg.");

    $("#mainForm").validate();
//    $("form").validate({
//    rules: {
//        SelectName: { valueNotEquals: "default" }
//    },
//    messages: {
//        SelectName: { valueNotEquals: "Please select an item!" }
//    }
//});
});



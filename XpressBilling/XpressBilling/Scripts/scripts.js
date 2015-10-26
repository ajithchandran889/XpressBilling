var countryArray = {};

$(function () {
    $("#inputDate").datepicker(); 
    $("#FormationDate").datepicker();
    $("#Date").datepicker();
    $("#DOJ").datepicker();
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

$(function () {
    //var availableTags = [
    //  "ActionScript",
    //  "AppleScript",
    //  "Asp",
    //  "BASIC",
    //  "C",
    //  "C++",
    //  "Clojure",
    //  "COBOL",
    //  "ColdFusion",
    //  "Erlang",
    //  "Fortran",
    //  "Groovy",
    //  "Haskell",
    //  "Java",
    //  "JavaScript",
    //  "Lisp",
    //  "Perl",
    //  "PHP",
    //  "Python",
    //  "Ruby",
    //  "Scala",
    //  "Scheme"
    //];
    //$("#Company").autocomplete({
    //    source: availableTags
    //});
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
    $("#ContactPerson").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "CompanyEdit.aspx/GatAllContacts",
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


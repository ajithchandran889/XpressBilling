$(function () {
    $("#inputDate").datepicker(); 
    $("#FormationDate").datepicker();
    $("#Date").datepicker();
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


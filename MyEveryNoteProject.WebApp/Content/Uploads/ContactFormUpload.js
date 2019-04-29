//Captha Application

var process;
var code;
function sample() {
    process = document.getElementById("captha").getContext("2d");
    process.fillStyle = "palegoldenrod";
    change();
    //$('.hiddenvalue').addClass("amina");
}
function change() {

    code = "";
    for (var i = 0; i < 6; i++) {
        var randomvalue = Math.random();
        if (Math.round(randomvalue) == 0) {
            code += String.fromCharCode(49 + 9 * Math.random());
        } else {
            code += String.fromCharCode(65 + 25 * Math.random());
        }
    }
    process.clearRect(0, 0, 150, 50);
    process.fillRect(0, 0, 150, 50);
    process.font = "30px calibri";
    process.strokeText(code, 24, 34);



}


$(function () {
    $("#contactformbtn").click(function ()
    {
        var cntrl = $("#capthacontrol").val();
        var cntrl = cntrl.toUpperCase();
        if (code === cntrl)
        {

            //PURECODE BEGIN
            $('#loadingpanel').css('display', 'inline');
            var vname = $('#nametxt').val();
            var vemail = $('#emailtxt').val();
            var vsubject = $('#subjecttxt').val();
            var vmessage = $('#messagetxt').val();

            var product = { Name: vname, Email: vemail, Subject: vsubject, Message: vmessage }

            $.ajax({
                type: "POST",
                url: "/Home/ContactFormUpload",
                data: '{product: ' + JSON.stringify(product) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
                //success: function (response) {
                //    alert("Hello: " + response.Name + ".\nCurrent Date and Time: " + response.DateTime);
                //},
                //failure: function (response) {
                //    alert(response.responseText);
                //},
                //error: function (response) {
                //    alert(response.responseText);
                //}
            }).done(function (result) {

                if (result.hasError === false) {
                    $('#loadingpanel').css('display', 'none');
                    toastr.success(result.message);
                    sample();
                }
                else {
                    $('#loadingpanel').css('display', 'none');
                    toastr.error(result.message);
                    sample();
                }
            });
        //PURECODE END

        }
        else {
            toastr.warning("Captha was wrong!")
        }
       
    });


});










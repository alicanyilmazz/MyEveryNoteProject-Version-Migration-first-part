$('#btnsendfile').click(function () {

    var formdata = new FormData($("form").get(0));

    $.ajax({

        method: "POST",
        url: '/Home/ResumeUpload',   // url: '@Url.Action("ResumeUpload", "Home")', Js içerisinde kullanılamaz cshtml de kullanabilirsin
        data: formdata,
        processData: false,
        contentType: false


    }).done(function (result) {

        if (result.hasError === false) {
            toastr.success(result.message);
        }
        else {
            toastr.error(result.message);
        }

    });

})
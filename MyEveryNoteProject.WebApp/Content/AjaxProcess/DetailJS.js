//$(window).load(function () {
//    $('a#pagnumlink').parent().removeClass("active");
//    $("a[data-pagnum=1]").parent().addClass("active");;
//});

// Add the following code if you want the name of the file appear on select



// <script>

//    $(document).ready(function () {
//        $('a#pagnumlink').click(function (e) {

//            var btn = $(e.currentTarget);
//            var dat = btn.data("pagnum");

//            $.ajax({

//                method: "POST",
//                url: "/Home/Index",
//                data: { "pagenum": dat } //soldaki property adi,sağdaki yukardaki articleid değişkenin adi.

//            }).done(function (data) {

//                if (data.result) {
//                    /*yorumlar tekrar yükleniyor*/
//                    $(modalCommentBodyId).load("/Comment/ShowArticleComment/" + articleid);
//                    toastr.success('Your comment has been added!.')
//                }
//                else {
//                    //alert("Comment not added!");
//                    toastr.error('Comment not added!')
//                }

//            }).fail(function () {
//                //alert("Could not connect to server!");
//                toastr.error('Could not connect to server!')
//            });


//        });
//    });

//</script> 
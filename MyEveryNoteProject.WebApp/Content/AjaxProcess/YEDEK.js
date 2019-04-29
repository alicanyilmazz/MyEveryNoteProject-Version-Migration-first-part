//<script>

//    //$('a#pagnumlink').parent().removeClass("active");
//    $(document).on('click', 'a#categoryids', function (e) {
        
//        var btn = $(e.currentTarget);
//    var dat = btn.data("category-id");

//        $.ajax({

//        method: "GET",
//    url: "/Home/TakeCategoryFilteredArticle/" + dat,

//        }).done(function (data) {

//        $('#nbr').empty();
//    $('#nbr').html(data);

//    //$("a[data-pagnum=" + dat + "]").parent().addClass("active");

//        }).fail(function () {
//        //alert("Could not connect to server!");
//        toastr.error('Could not connect to server!')
//    });

//});







////$('a#pagnumlink').parent().removeClass("active");
//    $(document).on('click', 'a#pagnumlink', function (e) {

//        var btn = $(e.currentTarget);
//    var dat = btn.data("pagnum");

//        $.ajax({

//        method: "GET",
//    url: "/Home/TakeAricleBody/" + dat,

//        }).done(function (data) {

//        $('#nbr').empty();
//    $('#nbr').html(data);

//    $("a[data-pagnum=" + dat + "]").parent().addClass("active");

//        }).fail(function () {
//        //alert("Could not connect to server!");
//        toastr.error('Could not connect to server!')
//    });

//});


//</script>

//Brk

////$('a#pagnumlink').parent().removeClass("active");
//$(document).on('click', 'a#pagnumlink', function (e) {

//    var btn = $(e.currentTarget);
//    var dat = btn.data("pagnum");
//    var datcat = btn.data("pagnum-categorid");

//    //$.ajax({

//    //    method: "GET",
//    //    url: "/Home/TakeAricleBody/" + dat,

//    //}).done(function (data) {

//    //    $('#nbr').empty();
//    //    $('#nbr').html(data);

//    //    $("a[data-pagnum=" + dat + "]").parent().addClass("active");

//    //}).fail(function () {
//    //    //alert("Could not connect to server!");
//    //    toastr.error('Could not connect to server!')
//    //});




//    $.get("/Home/TakeArticleBody", { datpagnum: dat, datcatnum: datcat }, function (d) {
//        //$('#nbr').empty();
//        $('#nbr').html(d);
//        $("a[data-pagnum=" + dat + "]").parent().addClass("active");
//    });


//});




//$.get("/Home/TakeArticleBody", { datpagnum: dat, datcatnum: datcat }, function (d) {
//    //$('#nbr').empty();
//    $('#nbr').html(d);
//    $("a[data-pagnum=" + dat + "]").parent().addClass("active");
//});





    //$(document).ready(function () {
    //    $('a#categoryids').click(function (e) {

    //        var btn = $(e.currentTarget);
    //        var dat = btn.data("category-id");

    //        $.ajax({

    //            method: "GET",
    //            url: "/Home/TakeAricleBody/" + dat,

    //        }).done(function (data) {
    //            $('#nbr').empty();
    //            $('#nbr').html(data);
    //            //if (data.result) {
    //            //    /*yorumlar tekrar yükleniyor*/
    //            //    $(modalCommentBodyId).load("/Comment/ShowArticleComment/" + articleid);
    //            //    toastr.success('Your comment has been added!.')
    //            //}
    //            //else {
    //            //    //alert("Comment not added!");
    //            //    toastr.error('Comment not added!')
    //            //}

    //        }).fail(function () {
    //            //alert("Could not connect to server!");
    //            toastr.error('Could not connect to server!')
    //        });


    //    });
    //});

     //$(document).ready(function () {


    //    $('a#pagnumlink').click(function (e) {

    //        var btn = $(e.currentTarget);
    //        var dat = btn.data("pagnum");
    //        $('a#pagnumlink').parent().removeClass("active");
    //        btn.parent().addClass("active");
    //        $.ajax({

    //            method: "GET",
    //            url: "/Home/TakeAricleBody/" + dat,

    //        }).done(function (data) {
    //            $('#nbr').empty();
    //            $('#nbr').html(data);

    //            //if (data.result) {
    //            //    /*yorumlar tekrar yükleniyor*/
    //            //    $(modalCommentBodyId).load("/Comment/ShowArticleComment/" + articleid);
    //            //    toastr.success('Your comment has been added!.')
    //            //}
    //            //else {
    //            //    //alert("Comment not added!");
    //            //    toastr.error('Comment not added!')
    //            //}

    //        }).fail(function () {
    //            //alert("Could not connect to server!");
    //            toastr.error('Could not connect to server!')
    //        });


    //    });


    //});




//$(document).on('click', 'a#categoryids', function (e) {

//    var btn = $(e.currentTarget);
//    var datcat = btn.data("partial-category-id");
//    dat = 0;
//    var datliked = btn.data("likecntrl");

//    $.get("/Home/TakeArticleBody", { datpagnum: dat, datcatnum: datcat, likecntrl: datliked }, function (d) {
//        //$('#nbr').empty();
//        $('#nbr').html(d);
//        $("a[data-pagnum='1']").parent().addClass("active");
//    }).fail(function () {
//        toastr.warning("Could not connect to server");
//    });

//});


//$(document).on('click', 'a#pagnumlink', function (e) {

//    var btn = $(e.currentTarget);
//    var dat = btn.data("pagnum");
//    var datcat = btn.data("pagnum-categorid");
//    var datliked = btn.data("likecntrl");

//    $.get("/Home/TakeArticleBody", { datpagnum: dat, datcatnum: datcat, likecntrl: datliked }, function (d) {
//        //$('#nbr').empty();
//        $('#nbr').html(d);
//        $("a[data-pagnum=" + dat + "]").parent().addClass("active");
//    }).fail(function () {
//        toastr.warning("Could not connect to server");
//    });


//});

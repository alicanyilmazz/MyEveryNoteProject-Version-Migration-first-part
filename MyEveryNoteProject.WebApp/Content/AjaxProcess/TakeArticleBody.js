$(document).on('click', 'a#categoryids', function (e) {

    var btn = $(e.currentTarget);
    var datcat = btn.data("partial-category-id");
    dat = 0;
    var datliked = btn.data("likecntrl");

    $.get("/Home/TakeArticleBody", { datpagnum: dat, datcatnum: datcat, likecntrl: datliked }, function (d) {
        //$('#nbr').empty();
        $('#nbr').html(d);
        $("a[data-pagnum='1']").parent().addClass("active");
        
    }).always(function () {
        likeservice();
    }).fail(function () {
        toastr.warning("Could not connect to server");
    });

});


$(document).on('click', 'a#pagnumlink', function (e) {

    var btn = $(e.currentTarget);
    var dat = btn.data("pagnum");
    var datcat = btn.data("pagnum-categorid");
    var datliked = btn.data("likecntrl");

    $.get("/Home/TakeArticleBody", { datpagnum: dat, datcatnum: datcat, likecntrl: datliked }, function (d) {
        //$('#nbr').empty();
        $('#nbr').html(d);
        $("a[data-pagnum=" + dat + "]").parent().addClass("active");
       
    }).always(function () {
        likeservice();
    }).fail(function () {
        toastr.warning("Could not connect to server");
    });


});




//Ajax işlemlerinden sonrada Like işlemlerinin çalışabilmesi için likeservice yapısı eklendi.

function likeservice() {



    var articleids = [];

    //@* user 'in sayfada begendiklerinin isaretli olarak gelmesi *@

    $("div[data-article-id]").each(function (i, e) {  // i:index , e:element

        articleids.push($(e).data("article-id"));

    });

    $.ajax({

        method: "POST",
        url: "/Article/GetLiked",
        data: { ids: articleids }


    }).done(function (data) {

        if (data.result != null && data.result.length > 0) {

            for (var i = 0; i < data.result.length; i++) {

                var id = data.result[i];
                var likedarticle = $("div[data-article-id=" + id + "]"); //selector yazımına dikkat edelim!Burada orneğin id si 43 olan div i bulduk.
                var btn = likedarticle.find("button[data-liked]"); //button u bulduk.
                var span = btn.find("span.like-star"); // icon-olan span'i bulduk.
                var SpanCount = btn.find("span.like-count");

                btn.data("liked", true);

                var icon = span.children();

                icon.removeClass("far fa-star");
                icon.addClass("fas fa-star");
                icon.css("color", "gold");
                SpanCount.css("color", "gold");
                //btn.removeClass("btn btn-dark");
                //btn.addClass("btn btn-warning");

                //counter++;

            }
            //console.log("sonuc : "+counter);
        }

    }).fail(function () {
        toastr.error('Could not connect to server!')

    });

    //@* Like atma ve kaldirma islemi *@

    $("button[data-liked]").click(function () {

        var btn = $(this);
        var liked = btn.data("liked");
        var articleid = btn.data("article-id");
        var spanStar = btn.find("span.like-star");
        var SpanCount = btn.find("span.like-count");
        var icon = spanStar.children();
        //console.log("control1");
        //console.log(liked);
        //console.log("like count(before) : "+SpanCount.text());

        $.ajax({

            method: "POST",
            url: "/Article/SetLikeState",
            data: { "articleid": articleid, "liked": !liked }

        }).done(function (data) {
            //console.log("control2");
            //console.log(data);

            if (data.hasError) {
                //console.log("control3");
                //alert(data.errorMessage);
                toastr.warning(data.errorMessage)
                //console.log("giris yapilmamis!");
            }
            else {
                //console.log("giris yapilmis!");
                liked = !liked;
                btn.data("liked", liked);
                SpanCount.text(data.result);
                //console.log("control4");
                //console.log("data result : " + data.result);
                //console.log("like count(after) : " + SpanCount.text());

                icon.removeClass("far fa-star");
                icon.removeClass("fas fa-star");

                if (liked) {
                    //console.log("control5");
                    icon.addClass("fas fa-star");
                    icon.css("color", "gold");
                    SpanCount.css("color", "gold");
                    toastr.success('Liked!')
                }
                else {
                    //console.log("control6");
                    icon.addClass("far fa-star");
                    icon.css("color", "white");
                    SpanCount.css("color", "white");
                    toastr.warning('Unliked!')
                }

            }

        }).fail(function () {

            toastr.warning('Could not connect to server')
        });

    });



}


$(".custom-file-input").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});

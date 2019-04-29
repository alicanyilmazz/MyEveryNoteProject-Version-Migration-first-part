$(document).ready(function () {
    $('a#pagnumlink').click(function (e) {

        var btn = $(e.currentTarget);
        var dat = btn.data("pagnum");

        $.ajax({

            method: "GET",
            url: "/Home/TakeAricleBody/" + dat,

        }).done(function (data) {
            $('#nbr').empty();
            $('#nbr').html(data);
            //if (data.result) {
            //    /*yorumlar tekrar yükleniyor*/
            //    $(modalCommentBodyId).load("/Comment/ShowArticleComment/" + articleid);
            //    toastr.success('Your comment has been added!.')
            //}
            //else {
            //    //alert("Comment not added!");
            //    toastr.error('Comment not added!')
            //}

        }).fail(function () {
            //alert("Could not connect to server!");
            toastr.error('Could not connect to server!')
        });


    });
});
var articleid = -1;
var FullPageArticleBody = "div#ArticleHolePageCommentSection";

//$(document).ready(function () {
//    $('#modal_comment').on('show.bs.modal', function (e) {
//        var btn = $(e.relatedTarget);
//        articleid = btn.data("article-id");
//        $("#modal_comment_body").load("/Comment/ShowArticleComment/" + articleid);

//    });

//    $('a#')

//});

function doCommentfullpage(btn, clickstatus, commentid, commentareaid) {
    var vl = $('p#fullpagearticleid');
    articleid = vl.data("artid");
    var button = $(btn);
    var mode = button.data("edit-mode");

    if (clickstatus == 'edit_clicked') {

        if (!mode) {

            button.data("edit-mode", true);
            button.removeClass("btneditdefault");
            button.addClass("btndedit");
            //var btnicon = button.find("i");
            var btnicon = button.find(".cntrl")
            btnicon.removeClass("fa-edit");
            btnicon.addClass("fa-check-circle");
            

            $(commentareaid).addClass("editable");
            $(commentareaid).attr("contenteditable", true);
            $(commentareaid).focus();

        }
        else {

            button.data("edit-mode", false);
            button.addClass("btneditdefault");
            button.removeClass("btndedit");
            //var btnicon = button.find("i");
            var btnicon = button.find(".cntrl")
            btnicon.removeClass("fa-check-circle");
            btnicon.addClass("fa-edit");
           

            $(commentareaid).removeClass("editable");
            $(commentareaid).attr("contenteditable", false);

            var txt = $(commentareaid).text();

            $.ajax({
                method: "POST",
                url: "/Comment/Edit/" + commentid,
                data: { text: txt }
            }).done(function (data) {

                if (data.result) {
                    /*yorumlar tekrar yükleniyor*/
                   
                    $(FullPageArticleBody).load("/Home/ArticleWholePagePartialView/" + articleid);
                }
                else {
                    //alert("Comment not updated!");
                    toastr.error('Comment not updated!', 'Inconceivable!')
                }

            }).fail(function () {
                //alert("Could not connect to server!");
                toastr.error('"Could not connect to server!', 'Inconceivable!')
            });

            toastr.info('Your comment has been edited!!')
        }



    }
    else if (clickstatus == 'delete_clicked') {
        //var dialog_result = confirm("Yorum silinsin mi?");
        var dialog_result = true;
        toastr.warning('Your comment has been deleted!!')
        if (!dialog_result) {
            return false;
        }
        else {
            $.ajax({

                method: "GET",
                url: "/Comment/Delete/" + commentid,

            }).done(function (data) {

                if (data.result) {
                    /*yorumlar tekrar yükleniyor*/
                    console.log("oldumu : " + articleid);
                    
                    $(FullPageArticleBody).load("/Home/ArticleWholePagePartialView/" + articleid);
                }
                else {
                    //alert("Comment not deleted!");
                    toastr.error('Comment not deleted!')
                }

            }).fail(function () {
                //alert("Could not connect to server!");
                toastr.error('Could not connect to server!')
            });
        }

    }
    else if (clickstatus == 'new_clicked') {

        var txt = $('#new_comment_text').val();

        $.ajax({

            method: "POST",
            url: "/Comment/Create",
            data: { text: txt, "articleid": articleid } //soldaki property adi,sağdaki yukardaki articleid değişkenin adi.

        }).done(function (data) {

            if (data.result) {
                /*yorumlar tekrar yükleniyor*/
                $(FullPageArticleBody).load("/Home/ArticleWholePagePartialView/" + articleid);
                toastr.success('Your comment has been added!.')
            }
            else {
                //alert("Comment not added!");
                toastr.error('Comment not added!')
            }

        }).fail(function () {
            //alert("Could not connect to server!");
            toastr.error('Could not connect to server!')
        });

    }

}
$(document).ready(function () {
    $('#modal_articledetail').on('show.bs.modal', function (e) {
        var btn = $(e.relatedTarget);
        articleid = btn.data("article-id");
        $('#modal_articledetail_body').load("/Article/GetArticleText/" + articleid);

    });
});
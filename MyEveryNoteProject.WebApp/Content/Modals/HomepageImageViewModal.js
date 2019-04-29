$(document).ready(function () {
   
    $('#modal_image_detail').on('show.bs.modal', function (e) {

       
        var btn = $(e.relatedTarget);
        var path = btn.data("article-image-path");

        //var trgg = "<img id='detimg' src='' />";
        $('img#image_view_id').attr('src', '/images/articleimg/' + path);

    });
});


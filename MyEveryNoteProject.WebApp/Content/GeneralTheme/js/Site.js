$(document).ready(function () {


    $("#myef1").hover(  //yanlıslık yok buna bilerek id digerlerine class verdim ornek olsun diye
        function () { $('.codes').addClass('myef1ef') },
        function () { $('.codes').removeClass('myef1ef') });

    $(".myef2").hover(
        function () { $(".codes").addClass('myef2ef') },
        function () { $(".codes").removeClass('myef2ef') });

    $(".myef3").hover(
        function () { $(".codes").addClass('myef3ef') },
        function () { $(".codes").removeClass('myef3ef') });

    $(".myef4").hover(
        function () { $(".codes").addClass('myef4ef') },
        function () { $(".codes").removeClass('myef4ef') });

    $(".myef5").hover(
        function () { $(".codes").addClass('myef5ef') },
        function () { $(".codes").removeClass('myef5ef') });

    $(".myef6").hover(
        function () { $(".codes").addClass('myef6ef') },
        function () { $(".codes").removeClass('myef6ef') });

    $(".myef7").hover(
        function () { $(".codes").addClass('myef7ef') },
        function () { $(".codes").removeClass('myef7ef') });

    //tiklama ile (phone icin)

    $("#myef1").mousedown(       //yanlıslık yok buna bilerek id digerlerine class verdim ornek olsun diye
        function () { $('.codes').addClass('myef1ef') });

    $("#myef1").mouseup(

        function () { $('.codes').removeClass('myef1ef') });

    $(".myef2").mousedown(
        function () { $(".codes").addClass('myef2ef') });

    $(".myef2").mouseup(
        function () { $(".codes").removeClass('myef2ef') });

    $(".myef3").mousedown(
        function () { $(".codes").addClass('myef3ef') });

    $(".myef3").mouseup(
        function () { $(".codes").removeClass('myef3ef') });

    $(".myef4").mousedown(
        function () { $(".codes").addClass('myef4ef') });

    $(".myef4").mouseup(
        function () { $(".codes").removeClass('myef4ef') });

    $(".myef5").mousedown(
        function () { $(".codes").addClass('myef5ef') });

    $(".myef5").mouseup(
        function () { $(".codes").removeClass('myef5ef') });

    $(".myef6").mousedown(
        function () { $(".codes").addClass('myef6ef') });

    $(".myef6").mouseup(
        function () { $(".codes").removeClass('myef6ef') });

    $(".myef7").mousedown(
        function () { $(".codes").addClass('myef7ef') });

    $(".myef7").mouseup(
        function () { $(".codes").removeClass('myef7ef') });

    //turnback

    $("#turnback").click(
        function () { alert("alican") });




});


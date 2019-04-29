
$(document).ready(function () {

    $('.xxx').click(function () {

        $(".xxx").removeClass('active');

    });

    var contentlength = 0;

    $('#dropDown').change(function () {

        var value = $(this).children("option:selected").val();
        if (value != 4) {
            $(".drop-down-show-hide").hide();

        } else {
            $(".drop-down-show-hide").show();

        }

        if (value == "") {
            $(".next1").addClass('disabled');
        }
        else if (value == 1 || value == 2 || value == 3) {
            $(".next1").removeClass('disabled');
        }
        else {
            if (contentlength >= 50) {

                $(".next1").removeClass('disabled');
                $("#charnumber").text("successful");
            }
            else {

                $(".next1").addClass('disabled');
                var reminder = (50 - contentlength);
                $("#charnumber").text(reminder);

            }
        }

    });

    $('#validationTextarea').change(function () {

        var content = $("#validationTextarea").val();
        contentlength = content.length;
        //alert("text area is using and contentlenght is "+contentlength);

        if (contentlength >= 50) {

            $(".next1").removeClass('disabled');
            $("#holedata").removeClass('text-danger');
            $("#holedata").addClass('text-success');
            $("#charnumber").text(0);

        }
        else if (contentlength < 50) {

            $(".next1").addClass('disabled');
            $("#holedata").removeClass('text-success');
            $("#holedata").addClass('text-danger');
            var reminder = (50 - contentlength);
            $("#charnumber").text(reminder);

        }

    });


    //step 2
    var step_cntrl_1 = $(this).val('cnt1'), step_cntrl_2 = $(this).val('cnt2');


    $('.cnt1').change(function () {

        step_cntrl_1 = $('.cnt1').val();
        step_cntrl_2 = $('.cnt2').val();
      
        if (step_cntrl_1.length > 1) {
            $(".txt_1").text("Successful");
            $(".txt_1").removeClass("text-danger");
            $(".txt_1").addClass("text-success");
            //alert("1");
            if (step_cntrl_2.length > 1) {
                $(".next2").removeClass('disabled');
                //alert("2");
            }
        }
        else {
            $(".next2").addClass('disabled');
            $(".txt_1").text("Please enter your username!");
            $(".txt_1").removeClass("text-success");
            $(".txt_1").addClass("text-danger");
            //alert("3");
        }


    });

    $('.cnt2').change(function () {

        step_cntrl_1 = $('.cnt1').val();
        step_cntrl_2 = $('.cnt2').val();

        if (step_cntrl_2.length > 1) {
            $(".txt_2").text("Successful");
            $(".txt_2").removeClass("text-danger");
            $(".txt_2").addClass("text-success");
            //alert("4");
            if (step_cntrl_1.length > 1) {
                $(".next2").removeClass('disabled');
                //alert("5");
            }
        }
        else {
            $(".next2").addClass('disabled');
            $(".txt_2").text("Please enter your username!");
            $(".txt_2").removeClass("text-success");
            $(".txt_2").addClass("text-danger");
            //alert("6");
        }


    });
   
    
});







        //if (value == "") {

        //    $("#stp2").addClass('disabled');
        //    alert("value is 3");
        //}
        //else if (value == 1 || value == 2 || value == 3) {

        //    $("#stp2").removeClass('disabled');
        //    alert("value is 4");
        //}
        //else {
        //    if (contentlength >= 50) {

        //        $("#stp2").removeClass('disabled');
        //        //alert("contentlenght is >= 50 " + contentlength);
        //        $("#charnumber").text("successful");
        //        alert("value is 5");
        //    }
        //    else {

        //        $("#stp2").addClass('disabled');
        //        //alert("contentlenght is < 50 " + contentlength);
        //        var reminder = (50 - contentlength);
        //        $("#charnumber").text(reminder);
        //        alert("value is 6");
        //    }
        //}

        //if ($("#stp2").hasClass("disabled")) {
        //    $("#stp3").addClass('disabled');
        //    alert("value is 7");
        //}
        //else {
        //    l1 = $('.cnt1').val().length;
        //    l2 = $('.cnt2').val().length;
        //    alert("value is 8");
        //    if (l1 > 1 && l2 > 1) {
        //        $("#stp3").removeClass('disabled');
        //        alert("value is 9");
        //    }
        //}





    //$('#validationTextarea').change(function () {

    //    var content = $("#validationTextarea").val();
    //    contentlength = content.length;
    //    //alert("text area is using and contentlenght is "+contentlength);

    //    if (contentlength >= 50) {

    //        $("#stp2").removeClass('disabled');
    //        //alert("contentlenght is >= 50 " + contentlength);
    //        $("#charnumber").text("successful");
    //        alert("value is 10");
    //    }
    //    else {

    //        $("#stp2").addClass('disabled');
    //        //alert("contentlenght is < 50 " + contentlength);
    //        var reminder = (50 - contentlength);
    //        $("#charnumber").text(reminder);
    //        alert("value is 11");
    //    }

    //    if ($("#stp2").hasClass("disabled")) {
    //        $("#stp3").addClass('disabled');
    //        alert("value is 12");
    //    }
    //    else {
    //        l1 = $('.cnt1').val().length;
    //        l2 = $('.cnt2').val().length;
    //        alert("value is 13");
    //        if (l1 > 1 && l2 > 1) {
    //            $("#stp3").removeClass('disabled');
    //            alert("value is 14");
    //        }
    //    }

    //});


    ////step 3

    //var step_cntrl_1 = $(this).val('cnt1'), step_cntrl_2 = $(this).val('cnt2');


    //$('.cnt1').change(function () {

    //    step_cntrl_1 = $(this).val();
    //    //alert("uzunluk" + step_cntrl_1.length);
    //    //alert("uzunluk" + step_cntrl_2.length);
    //    if (step_cntrl_1.length > 1 && step_cntrl_2.length > 1) {
    //        $("#stp3").removeClass('disabled');
    //        $(".txt_1").text("Successful");
    //        $(".txt_1").removeClass("text-danger");
    //        $(".txt_1").addClass("text-success");
    //        //alert("value is 15");
    //    }
    //    else if (step_cntrl_1 < 1 || step_cntrl_2 < 1) {
    //        $("#stp3").addClass('disabled');
    //        if (step_cntrl_1 < 1) {
    //            $(".txt_1").text("Please enter your username!");
    //            $(".txt_1").removeClass("text-success");
    //            $(".txt_1").addClass("text-danger");
    //        }
    //        //alert("value is 16");
    //    }

    //    //if (step_cntrl_1.length > 1) {
    //    //    $(".txt_1").text("Successful");
    //    //    $(".txt_1").removeClass("text-danger");
    //    //    $(".txt_1").addClass("text-success");
    //    //    alert("value is 17");
    //    //}
    //    //else {
    //    //    $(".txt_1").text("Please enter your username!");
    //    //    $(".txt_1").removeClass("text-success");
    //    //    $(".txt_1").addClass("text-danger");
    //    //    alert("value is 18");
    //    //}

    //});

    //$('.cnt2').change(function () {

    //    step_cntrl_2 = $(this).val();
    //    //alert("agir control 2!");
    //    if (step_cntrl_1.length > 1 && step_cntrl_2.length > 1) {
    //        $("#stp3").removeClass('disabled');
    //        $(".txt_2").text("Successful");
    //        $(".txt_2").removeClass("text-danger");
    //        $(".txt_2").addClass("text-success");
    //        //alert("value is 19");
    //    }
    //    else if (step_cntrl_1 < 1 || step_cntrl_2 < 1) {
    //        $("#stp3").addClass('disabled');
    //        if (step_cntrl_2 < 1) {
    //            $(".txt_2").text("Please enter your username!");
    //            $(".txt_2").removeClass("text-success");
    //            $(".txt_2").addClass("text-danger");
    //        }
    //        //alert("value is 20");
    //    }

    //    //if (step_cntrl_2.length > 1) {
    //    //    $(".txt_2").text("Successful");
    //    //    $(".txt_2").removeClass("text-danger");
    //    //    $(".txt_2").addClass("text-success");
    //    //    alert("value is 21");
    //    //}
    //    //else {
    //    //    $(".txt_2").text("Please enter your username!");
    //    //    $(".txt_2").removeClass("text-success");
    //    //    $(".txt_2").addClass("text-danger");
    //    //    alert("value is 22");
    //    //}


    //});
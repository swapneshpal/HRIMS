

    
    function fnValidation() {

        var deptIndex = $("#ddlDepartment").get(0).selectedIndex;
        if (deptIndex == 0)
            $("#spnDept").show();

        var desigtIndex = $("#ddlDesignation").get(0).selectedIndex;
        if (desigtIndex == 0)
            $("#spnDesig").show();

        var reptHeadIndex = $("#ddlReportingHead").get(0).selectedIndex;
        if (reptHeadIndex == 0)
            $("#spnReptHead").show();
    
        if (s = 1)
        {
            return false;
        }
     
        if (s = 2) {
            return true;
        }
    }
    function fnReset() {
        $("#spnDept").hide();
        $("#spnDesig").hide();
        $("#spnReptHead").hide();
    }

$(document).ready(function () {
    $("#txtLoginName").focusout(function () {
        var txtEmail = $("#txtLoginName").val().trim();    
        $.ajax({
            type: 'post',
            url: '/Login/CheckForDuplication',
            contentType: 'application/json',
            dataType: 'json',
            data: "{EmailId:" + JSON.stringify(txtEmail) + "}",
            success: function (data) {
                if (data == 1)
                {
                    s = 1;
                    $("#emailId").show();
                }
                else
                {
                    s = 2;
                    $("#emailId").hide();
                }
            },
            failur: function (data) {
               
            }
        });

    });
});

function val1() {
    var mail = /^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,5000})$/;
    var count = 0;
    if (!$('#txtusername').val().trim().match(mail) || $('#txtusername').val().trim() == "") {
        if ($('#txtusername').val().trim() == "") {

            count = 1;
        }
        else {

            count = 2;
        }
    }

    if (count > 0) {
        if (count == 1) {
            $('#lblEmail').css("display", "block");
            $('#lblEmail').text("Please enter email id  ");
        }
        else if (count == 2) {
            $('#lblEmail').css("display", "block");
            $('#lblEmail').text("Please enter valid email id");
        }
        return "1";
    }
    else {
        $('#lblEmail').css("display", "none");
        return "2";
    }
}

function val2() {
    var password = /^[a-zA-Z0-9\s]+$/;
    var count = 0;
    if ($('#txtpassword').val().trim().length < 6 || $('#txtpassword').val().trim() == "") {
        if ($('#txtpassword').val().trim() == "") {
            count = 1;
        }
        else {

            count = 2;
        }
    }
    if (count > 0) {

        if (count == 1) {
            $('#lblpwd').css("display", "block");
            $('#lblpwd').text("Please enter Password  ");
        }
        else if (count == 2) {
            $('#lblpwd').css("display", "block");
            $('#lblpwd').text("Password length miminum 6 ");
        }
        return "1";
    }
    else {
        $('#lblpwd').css("display", "none");
        return "2";
    }
}

function val() {
    var a = val1();
    var b = val2();
    if (a == "1" || b == "1") {
        return false;
    }
    else {
        return true;
    }
}
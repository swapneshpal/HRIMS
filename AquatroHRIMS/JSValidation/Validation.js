function fnValidation() {
    var cnt = 0;
    var regex = /^[a-zA-Z\-\s]+$/;
    var txt = $("#txtName").val();
    email_regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i;
    var pattern = /^\d{10}$/;
    var email = $("#txtEmail").val();
    var contact = $("#txtContact").val();
    if (txt == "") {
        $("#msgName").css("display", "block");
        cnt++;
    }
    if (regex.test(txt)) {
        if (txt.length < 2)
        {
            cnt++;
            $("#msgName").css("display", "block").text("Please enter atleast 2 character");
        }
        else{
            $("#msgName").css("display", "none");
        }
        
    }
    else {
            $("#msgName").css("display", "block");
        cnt++;
    }

    if (email_regex.test(email)) {
        $("#msgEmail").css("display", "none");
    }
    else {
        $("#msgEmail").css("display", "block");
        cnt++;
    }
    if (pattern.test(contact)) {
        $("#msgContact").css("display", "none");
    }
    else {
        $("#msgContact").css("display", "block");
        cnt++;
    }

    if ($('#ddlClient option:selected').val() == 0) {
        $("#msgClient").css("display", "block");
        cnt++;
    }
    else {
        $("#msgClient").css("display", "none");
    }
    if (cnt > 0) {
        return false;
    }
    return true;
}
$(document).ready(function () {
    $("#ddlClient").change(function () {
        if ($('#ddlClient option:selected').val() == 0) {
            $("#msgClient").css("display", "block");
            cnt++;
            return false;
        }
        else {
            $("#msgClient").css("display", "none");
        }
    })
})
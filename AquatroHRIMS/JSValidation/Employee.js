
$(function () {
        $("#ddlDesignation").change(function () {
            var val = $('option:selected', this).val();
            if (val == "") {
                $("#spnDesig").css("display", "block");

            } else {
                $("#spnDesig").css("display", "none");
            }
        });


        $("#ddlDepartmentType").change(function () {
            var val = $('option:selected', this).val();

            if (val == "") {
                $("#spnDepartType").css("display", "block");

            } else {
                $("#spnDepartType").css("display", "none");
            }
        });

        $("#ddlDepartment").change(function () {
            var val = $('option:selected', this).val();
            var val1 = $('#ddlDepartmentType :selected').val("");
            
            if (val == "") {
                $("#spnDept").css("display", "block");

            } else {
                $("#spnDept").css("display", "none");
            }
        });
        $("#ddlReportingHead").change(function () {
            var val = $('option:selected', this).val();
            if (val == "") {
                $("#spnReptHead").css("display", "block");

            } else {
                $("#spnReptHead").css("display", "none");
            }
        });
        $("#ddlRollAccess").change(function () {
            var val = $('option:selected', this).val();
            if (val == "") {
                $("#spnRollAccess").css("display", "block");

            } else {
                $("#spnRollAccess").css("display", "none");
            }
        });
        $("#ddlLocation").change(function () {
            var val = $('option:selected', this).val();
            if (val == "") {
                $("#spnLocation").css("display", "block");

            } else {
                $("#spnLocation").css("display", "none");
            }
        });
});

function ShowAdd() {
   
        $("#divEmployeeList").hide();
        $("#divEmployeeAdd").show();
        $("#btnCreate").show();
        $("#btnUpdate").hide();
        $("#spnWorkEmail").show();
        $("#spnUpdateWorkEmail").hide();
        $("#spnPersonalEmail").show();
        $("#spnPersonalUpdateEmail").hide();

        $(".field-validation-error").text('');

        $("#divEmployeeList").hide();
        $("#divEmployeeAdd").show();
        $("#btnCreate").show();
        $("#btnUpdate").hide();
        
        $("#spnDateOfJoing").show();
        $("#spnWorkEmail").show();
        $("#spnUpdateWorkEmail").hide();
        $("#spnPersonalEmail").show();
        $("#spnPersonalUpdateEmail").hide();
        $('#txtFirstName').val('');
        $('#txtMiddleName').val('');
        $('#txtLastName').val('');
        $('#txtUpdateWorkEmail').val('');
        $('#txtPersonalEmailUpdate').val('');


        $('#hdnEmployeeID').val();
        $('#ddlDepartment').val();
        $('#ddlDepartmentType').val();
        $('#ddlDesignation').val();
        $('#ddlTitle').val();
        $('#ddlReportingHead').val();
        $('#ddlRollAccess').val();
        $('#ddlLocation').val();

}

function ShowList() {
    $("#divEmployeeList").show();
    $("#divEmployeeAdd").hide();
}

function fnValidation() {

    var flag = 0;
        if ($("#ddlDepartmentType option:selected").val() != 0) {
           
            $("#spnDepartType").css("display", "none");
        }
        else {
            $("#spnDepartType").css("display", "block");
            flag++;
        }
        if ($("#ddlDesignation option:selected").val() != 0) {
            $("#spnDesig").css("display", "none");
        }
        else {
          
            $("#spnDesig").css("display", "block");

            flag++;
        }

        if ($("#ddlDepartment option:selected").val() != 0) {
            $("#spnDept").css("display", "none");

        }
        else {
            $("#spnDept").css("display", "block");
            flag++;
        }

        if ($("#ddlReportingHead option:selected").val() != 0) {
            $("#spnReptHead").css("display", "none");

        }
        else {
            $("#spnReptHead").css("display", "block");
            flag++;
        }

        if ($("#ddlRollAccess option:selected").val() != 0) {
            $("#spnRollAccess").css("display", "none");


        }
        else {
            $("#spnRollAccess").css("display", "block");
            flag++;
        }
        if ($("#ddlLocation option:selected").val() != 0) {
            $("#spnLocation").css("display", "none");

        }
        else {
            $("#spnLocation").css("display", "block");
            flag++;
        }
        if (flag > 0) {
            return false;
        } else {
            return true;
        }

}

    $(function () {
        $(".chzn-select").chosen();
        $(".chzn-select-deselect").chosen({
            allow_single_deselect: false
        });
        $("#ddlDepartment_chosen").css("width", '290px');
        $("#ddlDepartmentType_chosen").css("width", '290px');
        $("#ddlDesignation_chosen").css("width", '290px');
        $("#ddlReportingHead_chosen").css("width", '290px');
        $("#ddlLocation_chosen").css("width", '290px');
        $("#ddlRollAccess_chosen").css("width", '290px');
        $("#ddlTitle_chosen").css("width", '290px');
        
    });

    function fnReset() {
        $('select').val('').trigger('chosen:updated')
        $(".field-validation-error").text('');
        $('#txtFirstName').val('');
        $('#txtMiddleName').val('');
        $('#txtLastName').val('');
        $('#txtWorkEmail').val('');
        $('#txtPersonalEmail').val('');
        $('#txtUpdateWorkEmail').val('');
        $('#txtPersonalEmailUpdate').val('');
        $('#txtPassword').val('');
        $('#txtConfirmPassword').val('');
        $('#DateOfJoing').val('');
        $("#spnDepartType").css("display", "none");
        $("#spnLocation").css("display", "none");
        $("#spnRollAccess").css("display", "none");
        $("#spnReptHead").css("display", "none");
        $("#spnDesig").css("display", "none");
        $("#spnDept").css("display", "none");
       
        
        
    }

   
/*Button Edit Profile Account Admin And Client*/
$(document).ready(function () {
    //Enable input Edit Name
    $("#btnEditName").click(function () {
        if (this.value == "Enable") {
            this.value = "Disable";
            $("#user-name").prop("readonly", false);
        }
        else {
            this.value = "Enable";
            $("#user-name").prop("readonly", true);
        }
    });

    //Enable input Edit Phone
    $("#btnEditPhone").click(function () {
        if (this.value == "Enable") {
            this.value = "Disable"
            $("#phone").prop("readonly", false);
        } else {
            this.value = "Enable";
            $("#phone").prop("readonly", true);
        }
    });

    //Enable input Edit Address
    $("#btnEditAddress").click(function () {
        if (this.value == "Enable") {
            this.value = "Disable"
            $("#address").prop("readonly", false);
        } else {
            this.value = "Enable";
            $("#address").prop("readonly", true);
        }
    });

    //Enable input Edit Date Of Birthday
    $("#btnEditDOB").click(function () {
        if (this.value == "Enable") {
            this.value = "Disable"
            $("#dob").prop("readonly", false);
        } else {
            this.value = "Enable";
            $("#dob").prop("readonly", true);
        }
    });
});

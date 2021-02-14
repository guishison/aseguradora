function onSaveClicking(sender, args) {
    var strMessage = "";

    var rtbName = $find($("[id$='rtbName']").attr("id"));
    var rtbEmail = $find($("[id$='rtbEmail']").attr("id"));
    var rtbLogin = $find($("[id$='rtbLogin']").attr("id"));
    var rtbPassword = $find($("[id$='rtbPassword']").attr("id"));
    var rtbNewPassword = $find($("[id$='rtbNewPassword']").attr("id"));
    var rtbConfirmNewPassword = $find($("[id$='rtbConfirmNewPassword']").attr("id"));

    if (rtbName.isEmpty()) {
        strMessage += " - El Nombre Completo es obligatorio <br />";
    }
    if (rtbEmail.isEmpty()) {
        strMessage += " - El Email del usuario es obligatorio <br />";
    }
    if (rtbLogin.isEmpty()) {
        strMessage += " - El Nombre de usuario  es obligatorio <br />";
    }
    if (rtbPassword.isEmpty()) {
        strMessage += " - La Antigua Clave es obligatoria <br />";
    }
    //intento de cambio de contraseña
    if (!(rtbNewPassword.isEmpty())) {
        if (rtbPassword.isEmpty()) {
            strMessage += " - La Antigua Clave es obligatoria para realizar el cambio de clave <br />";
        }
        if (rtbNewPassword.get_textBoxValue() != rtbConfirmNewPassword.get_textBoxValue()) {
            strMessage += " - La Nueva Clave no coincide con la confirmación <br />";
        }
    }

    if (strMessage != "") {
        proShow_Validation("Por favor verifique lo siguiente: <br />" + strMessage, "VALIDACION");
        args.set_cancel(true);
    }
}

function PasswordCheker() {
    var rtbPassword = $find($("[id$='rtbNewPassword']").attr("id")).get_textBoxValue();
    var rtbConfirmPassword = $find($("[id$='rtbConfirmNewPassword']").attr("id")).get_textBoxValue();

    if (rtbConfirmPassword == "") {
        $get("PasswordRepeatedIndicator").innerHTML = "";
        $get("PasswordRepeatedIndicator").className = "Base L0";
    } else if (rtbPassword == rtbConfirmPassword) {
        $get("PasswordRepeatedIndicator").innerHTML = "Ok";
        $get("PasswordRepeatedIndicator").className = "Base L5";
    } else {
        $get("PasswordRepeatedIndicator").innerHTML = "No es Igual";
        $get("PasswordRepeatedIndicator").className = "Base L1";
    }
}
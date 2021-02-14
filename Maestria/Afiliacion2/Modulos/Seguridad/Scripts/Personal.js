//#region --- GLOBALS ---

var _intUserID;

//#endregion

//#region --- EVENTS ---

$(document).ready(function () {
    //_intUserID = $get("hfUserID").value;

    $("#divDetail").dialog({
        autoOpen: false,
        draggable: true,
        resizable: false,
        modal: true,
        fluid: true,
        width: "auto",
        maxWidth: window.innerWidth,
        maxHeight: window.innerHeight,
        overflow: 'scroll',
        position: ["center", "top"],
        title: "Datos del personal",
        open: function (type, data) { $(this).parent().appendTo("form"); }
    });
});



// on window resize run function
$(window).resize(function () {
    fluidDialog();
});

// catch dialog if opened within a viewport smaller than the dialog width
$(document).on("dialogopen", ".ui-dialog", function (event, ui) {
    fluidDialog();
});

function fluidDialog() {
    var $visible = $(".ui-dialog:visible");
    // each open dialog
    $visible.each(function () {
        var $this = $(this);
        var dialog = $this.find(".ui-dialog-content").data("ui-dialog");
        // if fluid option == true
        if (dialog.options.fluid) {
            var wWidth = $(window).width();
            // check window width against dialog width
            if (wWidth < (parseInt(dialog.options.maxWidth) + 50)) {
                // keep dialog from filling entire screen
                $this.css("max-width", "90%");
            } else {
                // fix maxWidth bug
                $this.css("max-width", dialog.options.maxWidth + "px");
            }
            //reposition dialog
            dialog.option("position", dialog.options.position);
        }
    });

}


function onAddClicking(sender, args) {
    showDialog("divDetail");
}

var index;

function RowMouseOver(sender, eventArgs) {
    index = eventArgs.get_itemIndexHierarchical();
}

function OnEditUser() {
    showDialog("divDetail");
}

function onCancelClicking(sender, args) {
    closeDialog("divDetail");
    args.set_cancel(true);
}

function funValidate(sender, args) {
    var strMessage = "";

    var rtbName = $find($("[id$='rtbName']").attr("id"));
    var rtbRut = $find($("[id$='rtbRut']").attr("id"));
    var rtbEmail = $find($("[id$='rtbEmail']").attr("id"));
    var rtbTelefono = $find($("[id$='rtbTelefono']").attr("id"));
    var rcbDependencia = $find($("[id$='rcbDependencia']").attr("id"));
    var rcbUnidadNegocio = $find($("[id$='rcbUnidadNegocio']").attr("id"));
    
    var rtbLogin = $find($("[id$='rtbLogin']").attr("id"));
    var rtbPassword = $get($("[id$='rtbPassword']").attr("id"));
    var rbCambiarPassword = $find($("[id$='rbCambioPassword']").attr("id"));
    var rcbDepartamento = $find($("[id$='rcbDepartamento']").attr("id"));
    var rcbPosition = $find($("[id$='rcbPosition']").attr("id"));

    if (rtbName.isEmpty()) {
        strMessage += " - El nombre del usuario es obligatorio <br />";
    }
    if (rtbRut.isEmpty()) {
        strMessage += " - El RUT del usuario es obligatorio <br />";
    }
    if (rtbEmail.isEmpty()) {
        strMessage += " - El email del usuario es obligatorio <br />";
    }
    if (rtbTelefono.isEmpty()) {
        strMessage += " - El teléfono del usuario es obligatorio <br />";
    }
    if (rtbLogin.isEmpty()) {
        strMessage += " - El login del usuario es obligatorio <br />";
    }
    if (rbCambiarPassword.get_checked()) {

    if (rtbPassword.value == "") {
        strMessage += " - La clave del usuario es obligatorio <br />";
    }

    }
    if (rcbDepartamento.get_value() == "-1") {
        strMessage += " - El departamento es obligatorio <br />";
    }
    if (rcbDependencia.get_value() == "-1") {
        strMessage += " - La depencencia es obligatorio <br />";
    }
    if (rcbUnidadNegocio.get_value() == "-1") {
        strMessage += " - La Unidad de Negocio es obligatoria <br />";
    }
    if (rcbPosition.get_value() == "-1") {
        strMessage += " - El cargo es obligatorio <br />";
    }
    if (strMessage == "") {
        //funCloseModal("divDetail");
    } else {
        proShow_Validation("Verifique lo siguiente: <br />" + strMessage, "VALIDACION");
        args.set_cancel(true);
    }
}

function ConfirmDelete(sender, args) {

    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        if (shouldSubmit) {
            //initiate the origianal postback again
            this.click();
        }
    });

    var text = "¿Está seguro de eliminar el usuario seleccionado?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");

    args.set_cancel(true);
}
function ConfirmChangeStateUser(sender, args) {
    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        if (shouldSubmit) {
            //initiate the origianal postback again
            this.click();
        }
    });

    var strText = (!sender._checked) ? "habilitar" : "deshabilitar";

    var text = "Esta seguro de <b>" + strText.toUpperCase() + "</b> el usuario seleccionado?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");

    args.set_cancel(true);
}
function PasswordCheker() {

    var rtbPassword = $find($("[id$='rtbPassword']").attr("id")).get_textBoxValue();
    var rtbConfirmPassword = $find($("[id$='rtbConfirmPassword']").attr("id")).get_textBoxValue();

    if (rtbConfirmPassword == "") {
        $get("PasswordRepeatedIndicator").innerHTML = "";
        $get("PasswordRepeatedIndicator").className = "Base L0";
    }

    else if (rtbPassword == rtbConfirmPassword) {
        $get("PasswordRepeatedIndicator").innerHTML = "Ok";
        $get("PasswordRepeatedIndicator").className = "Base L5";
    }

    else {
        $get("PasswordRepeatedIndicator").innerHTML = "No es Igual";
        $get("PasswordRepeatedIndicator").className = "Base L1";
    }

}

function setPassword() {
    var rtbPassword = $get($("[id$='rtbPassword']").attr("id"));
    var rtbConfirmPassword = $get($("[id$='rtbConfirmPassword']").attr("id"));
    var hdfPassword = $get($("[id$=hdfPassword]").attr("id"));

    if (hdfPassword.value != '') {
        rtbPassword.value = hdfPassword.value;
        rtbConfirmPassword.value = hdfPassword.value;

        $get($("[id$=PasswordRepeatedIndicator]").attr("id")).innerHTML = "Coincide";
        $get($("[id$=PasswordRepeatedIndicator]").attr("id")).className = "Base L5";
    }
}
function OnKeyPressEnterSearch(seder, args) {
    var rtbFilter = $find($("[id$='rbFilter']").attr("id"));
    if (args.get_keyCode() == 13) {
        rtbFilter.click();
        args.set_cancel(true)
    }
}

//#endregion

//#region --- METHODS ---

//#endregion
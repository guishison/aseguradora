$(document).ready(function () {
    $("#divDetail").dialog({
        autoOpen: false,
        draggable: true,
        resizable: false,
        fluid: true,
        modal: true,
        width: "auto",
        maxWidth: window.innerWidth,
        maxHeight: window.innerHeight,
        overflow: "scroll",
        position: ["center", "top"],
        title: "Registro de Parametros del Login",
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

function OnEditProvider() {
    showDialog("divDetail");
}

function onCancelClicking(sender, args) {
    closeDialog("divDetail");
    args.set_cancel(true);
}

function funValidate(sender, args) {
    var strMessage = "";


    var rntLongitudMaxima = $find($("[id$='rntLongitudMaxima']").attr("id"));
    var rntLongitudMinima = $find($("[id$='rntLongitudMinima']").attr("id"));
    var rntCantidadIntentosFallidos = $find($("[id$='rntCantidadIntentosFallidos']").attr("id"));
    var rntTiempoBloqueo = $find($("[id$='rntTiempoBloqueo']").attr("id"));
    var rntComprobacionPasswordAnteriores = $find($("[id$='rntComprobacionPasswordAnteriores']").attr("id"));
    var rcbNivelPassword = $find($("[id$='rcbNivelPassword']").attr("id"));
    var rcbExpiracion = $find($("[id$='rcbExpiracion']").attr("id"));
    //var rcbMoneda = $find($("[id$='rcbMoneda']").attr("id"));
    //var rtbMontoInicial = $find($("[id$='rtbMontoInicial']").attr("id"));

    if (rntLongitudMaxima.isEmpty() ) {
        strMessage += " - Longitud máxima es requerida <br />";
    }
    if (rntLongitudMinima.isEmpty() ) {
        strMessage += " - Longitud mínima es requerida <br />";
    }

    if (rcbNivelPassword.get_value() == "-1") {
        strMessage += " - Seleccione un nivel de password <br />";
    } 
    if (rcbExpiracion.get_value() == "-1") {
            strMessage += " - Seleccione una expiración <br />";        
    }

    if (rntCantidadIntentosFallidos.isEmpty()) {
        strMessage += " - Cantidad de intentos fallidos es requerido <br />";
    }
    if (rntTiempoBloqueo.isEmpty()) {
        strMessage += " - El tiempo de bloqueo es requerido <br />";
    }
    if (rntComprobacionPasswordAnteriores.isEmpty()) {
        strMessage += " - La comprobación de password anteriores es requerido <br />";
    }
    //if (rcbMoneda.get_value() == "-1") {
    //    strMessage += " - Seleccione una Moneda <br />";
    //}
    //if (rtbMontoInicial.isEmpty()) {
    //    strMessage += " - Monto Inicial es requerido <br />";
    //}

    if (strMessage == "") {
        //funCloseModal("divDetail");
    } else {
        proShow_Validation("Por Favor revise los siguientes ítems: <br />" + strMessage, "VALIDACION");
        args.set_cancel(true);
    }
}


function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    return re.test(email);
}

function OnKeyPressEnterSearch(seder, args) {
    var rtbFilter = $find($("[id$='rbFilter']").attr("id"));
    if (args.get_keyCode() == 13) {
        rtbFilter.click();
        args.set_cancel(true)
    }
}
function ConfirmDelete(sender, args) {

    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        if (shouldSubmit) {
            //initiate the origianal postback again
            this.click();
        }
    });

    var text = "¿Está seguro que desea eliminar la caja seleccionada?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");

    args.set_cancel(true);

}





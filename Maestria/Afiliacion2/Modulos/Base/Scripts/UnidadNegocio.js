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
        title: "Datos de la concesionaria",
        open: function (type, data) { $(this).parent().appendTo("form"); }
    });
});

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
function onCancelClicking(sender, args) {
    closeDialog("divDetail");
    args.set_cancel(true);
}
function onAddClicking(sender, args) {
    showDialog("divDetail");
}

function onSaveClicking(sender, args) {
    var strMessage = "";
    var rcbUnidadNegocio = $find($("[id$='rcbUnidadNegocio']").attr("id"));
    var rtbNombre = $find($("[id$='rtbNombre']").attr("id"));
    var rtbAlias = $find($("[id$='rtbAlias']").attr("id"));
    //var rcbHotel = $telerik.findComboBox(AVA.getID("rcbHotel"));
    if (rcbUnidadNegocio.get_value() == "-1") {
        strMessage += " - Seleccione una unidad de negocio <br />";
    }
    if (rtbNombre.isEmpty()) {
        strMessage += " - La placa es requerida <br />";
    }
    if (rtbAlias.isEmpty()) {
        strMessage += " - El alias es requerido <br />";
    }
    if (strMessage == "") {
        //funCloseModal("divDetail");
    } else {
        proShow_Validation("Verifique lo siguiente: <br />" + strMessage, "VALIDACION");
        args.set_cancel(true);
    }
}

function OnEditDatabase() {
    showDialog("divDetail");
}

function OnDeleteDatabase() {
    ConfirmDelete('¿Está seguro de eliminar el departamento?', event);
}

function OnkeyPress(sender, args) {
    console.log(10)
    if (args.get_keyCode() == 13) {
        var rbFilter = $find($("[id$='rbFilter']").attr("id"));
        rbFilter.click();
        args.set_cancel(true);
    }
}


function OnkeyPressGuardar(sender, args) {

    if (args.get_keyCode() == 13) {
        var rtbSave = $find($("[id$='rtbSave']").attr("id"));

        var strMessage = "";
        var rcbCliente = $find($("[id$='rcbCliente']").attr("id"));
        var rcbMarca = $find($("[id$='rcbMarca']").attr("id"));
        var rcbModelo = $find($("[id$='rcbModelo']").attr("id"));
        var rcbTipoVehiculo = $find($("[id$='rcbTipoVehiculo']").attr("id"));
        var rcbOrigen = $find($("[id$='rcbOrigen']").attr("id"));
        var rtbPlaca = $find($("[id$='rtbPlaca']").attr("id"));
        var rtbPotencia = $find($("[id$='rtbPotencia']").attr("id"));
        //var rcbHotel = $telerik.findComboBox(AVA.getID("rcbHotel"));
        if (rcbCliente.get_value() == "-1") {
            strMessage += " - Seleccione un cliente <br />";
        }
        if (rcbMarca.get_value() == "-1") {
            strMessage += " - Seleccione una marca <br />";
        }
        if (rcbModelo.get_value() == "-1") {
            strMessage += " - Seleccione un modelo <br />";
        }
        if (rcbTipoVehiculo.get_value() == "-1") {
            strMessage += " - Seleccione un tipo de vehiculo <br />";
        }
        if (rcbOrigen.get_value() == "-1") {
            strMessage += " - Seleccione un origen <br />";
        }
        if (rtbPlaca.isEmpty()) {
            strMessage += " - La placa es requerida <br />";
        }
        if (rtbPotencia.isEmpty()) {
            strMessage += " - La potencia es requerida <br />";
        }
        if (strMessage != "") {
            proShow_Validation("Revise los siguiente: <br />" + strMessage, "VALIDACION");
            args.set_cancel(true);
        } else {
            rtbSave.click();
            args.set_cancel(true);
        }
    }
}



function ConfirmDelete(sender, args) {
    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        if (shouldSubmit) {
            //initiate the origianal postback again
            this.click();
        }
    });

    var text = "¿Está seguro de eliminar el vehiculo seleccionado?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");
    args.set_cancel(true);
}

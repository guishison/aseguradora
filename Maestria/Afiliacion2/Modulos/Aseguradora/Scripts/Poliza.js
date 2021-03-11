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
        title: "Datos de la póliza",
        open: function (type, data) { $(this).parent().appendTo("form"); }
    });
    $("#divDetail2").dialog({
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
        title: "Datos de la Cotizacion",
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
function onFindClicking(sender, args) {
    showDialog("divDetail2");
}
function OnSelection() {
    closeDialog("divDetail2");
}
function onCalcular(sender, args) {
    var strMessage = "";
    var rcbCiudad = $find($("[id$='rcbCiudad']").attr("id"));
    var rcbVehiculo = $find($("[id$='rcbVehiculo']").attr("id"));
    var rntPrecioVehiculo = $find($("[id$='rntPrecioVehiculo']").attr("id"));
    var rntMontoAsegurado = $find($("[id$='rntMontoAsegurado']").attr("id"));
    var rntTiempo = $find($("[id$='rntTiempo']").attr("id"));
    var rntTasa = $find($("[id$='rntTasa']").attr("id"));
    if (rcbCiudad.get_value() == "-1") {
        strMessage += " - Seleccione una ciudad <br />";
    }
    if (rcbVehiculo.get_value() == "-1") {
        strMessage += " - Seleccione un vehiculo <br />";
    }
    if (rntPrecioVehiculo.get_value() == 0) {
        strMessage += " - El precio del vehiculo debe ser mayor a 0 <br />";
    }
    if (rntMontoAsegurado.get_value() == 0) {
        strMessage += " - El monto asegurado del vehiculo debe ser mayor a 0 <br />";
    }
    if (rntTiempo.get_value() == 0) {
        strMessage += " - El tiempo de adquisicion del seguro debe ser mayor a 0 <br />";
    }
    if (rntTasa.get_value() == 0) {
        strMessage += " - La tasa debe ser  mayor a 0 <br />";
    }
    if (strMessage == "") {
        //funCloseModal("divDetail");
    } else {
        proShow_Validation("Verifique lo siguiente: <br />" + strMessage, "VALIDACION");
        args.set_cancel(true);
    }
}
function onSaveClicking(sender, args) {
    var strMessage = "";
    var rdpFechaPoliza = $find($("[id$='rdpFechaPoliza']").attr("id"));
    var rdpFechaInicio = $find($("[id$='rdpFechaInicio']").attr("id"));

    var rcbCiudad = $find($("[id$='rcbCiudad']").attr("id"));
    var rcbCliente = $find($("[id$='rcbCliente']").attr("id"));
    var rcbVehiculo = $find($("[id$='rcbVehiculo']").attr("id"));
    var rntPrecioVehiculo = $find($("[id$='rntPrecioVehiculo']").attr("id"));
    var rntMontoAsegurado = $find($("[id$='rntMontoAsegurado']").attr("id"));
    var rntTiempo = $find($("[id$='rntTiempo']").attr("id"));
    var rtbNumeroPoliza = $find($("[id$='rtbNumeroPoliza']").attr("id"));
    var rtbCotizacion = $find($("[id$='rtbCotizacion']").attr("id"));
    var rntTasa = $find($("[id$='rntTasa']").attr("id"));
    var rntCostoPrima = $find($("[id$='rntCostoPrima']").attr("id"));
    var rntCostoTotal = $find($("[id$='rntCostoTotal']").attr("id"));
    //var rcbHotel = $telerik.findComboBox(AVA.getID("rcbHotel"));
    if (rdpFechaPoliza.get_selectedDate() == null) {
        strMessage += " - Ingrese Una fecha correcta en Fecha Poliza <br />";
    }
    if (rdpFechaInicio.get_selectedDate() == null) {
        strMessage += " - Ingrese Una fecha correcta en Fecha Inicio <br />";
    }
    if (rtbNumeroPoliza.isEmpty()) {
        strMessage += " - El numero de poliza es requerido <br />";
    }
    if (rtbCotizacion.isEmpty()) {
        strMessage += " - Seleccione una cotizacion <br />";
    }
    if (rcbCiudad.get_value() == "-1") {
        strMessage += " - Seleccione una ciudad <br />";
    }
    if (rcbCliente.get_value() == "-1") {
        strMessage += " - Seleccione un cliente <br />";
    }
    if (rcbVehiculo.get_value() == "-1") {
        strMessage += " - Seleccione un vehiculo <br />";
    }
    if (rntPrecioVehiculo.get_value() == 0) {
        strMessage += " - El precio del vehiculo debe ser mayor a 0 <br />";
    }
    if (rntMontoAsegurado.get_value() == 0) {
        strMessage += " - El monto asegurado del vehiculo debe ser mayor a 0 <br />";
    }
    if (rntTiempo.get_value() == 0) {
        strMessage += " - El tiempo de adquisicion del seguro debe ser mayor a 0 <br />";
    }
    if (rntTasa.get_value() == 0) {
        strMessage += " - La tasa debe ser  mayor a 0 <br />";
    }
    if (rntCostoPrima.get_value() == 0) {
        strMessage += " - El costo de la prima debe ser  mayor a 0 <br />";
    }
    if (rntCostoTotal.get_value() == 0) {
        strMessage += " - El costo total de la prima debe ser  mayor a 0 <br />";
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
    ConfirmDelete('¿Está seguro de eliminar la cotizacion?', event);
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

    var text = "¿Está seguro de eliminar la cotizacion seleccionada?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");
    args.set_cancel(true);
}

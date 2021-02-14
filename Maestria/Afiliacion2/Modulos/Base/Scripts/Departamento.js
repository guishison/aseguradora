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
        title: "Datos del departamento",
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
    var rtbNombre = $find($("[id$='rtbNombre']").attr("id"));
    //var rcbDepartamentoSup = $telerik.findComboBox(AVA.getID("rcbDepartamentoSup"));
    if (rtbNombre.isEmpty()) {
        strMessage += " - El nombre es requerido <br />";
    }
    //if (rcbDepartamentoSup.get_value() == "-1") {
    //    strMessage += " - Seleccione un Departamento <br />";
    //}
    if (strMessage != "") {
        proShow_Validation("Revise los siguientes errores: <br />" + strMessage, "VALIDACION");
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
        var rtbNombre = $find($("[id$='rtbNombre']").attr("id"));
        //var rcbHotel = $telerik.findComboBox(AVA.getID("rcbHotel"));
        if (rtbNombre.isEmpty()) {
            strMessage += " - El nombre es requerido <br />";
        }
        //if (rcbHotel.get_value() == "-1") {
        //    strMessage += " - Seleccione un Hotel <br />";
        //}
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

    var text = "¿Está seguro de eliminar el departamento seleccionado?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");
    args.set_cancel(true);
}

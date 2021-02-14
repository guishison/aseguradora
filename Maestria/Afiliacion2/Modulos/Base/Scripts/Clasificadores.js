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
        title: "CLASIFICADORES",
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

function OnEditClicking(sender, args) {
    showDialog("divDetail");
}

function onCancelClicking(sender, args) {
    closeDialog("divDetail");
    args.set_cancel(true);
}

function Validate(sender, args) {
    var strMessage = "";

    var rtbNombre = $find($("[id$='rtbNombre']").attr("id"));
    var rtbValor = $find($("[id$='rtbValor']").attr("id"));

    if (rtbNombre.isEmpty()) {
        strMessage += " -El nombre del clasificador es requerido <br />";
    }

    if (rtbValor.isEmpty()) {
        strMessage += " -El valor del clasificador es requerido <br />";
    }

    if (strMessage == "") {
        //funCloseModal("divDetail");
    } else {
        proShow_Validation("Revise lo siguiente: <br />" + strMessage, "VALIDATION");
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

    var text = "¿Está seguro de eliminar el clasificador seleccionado?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");

    args.set_cancel(true);

}


function OnkeyPressEnterSearch(sender, args) {
    if (args.get_keyCode() == 13) {
        var rbFilter = $find($("[id$='rbFilter']").attr("id"));
        var rtbFilter = $find($("[id$='rtbFilter']").attr("id"));        
        rbFilter.click();
        args.set_cancel(true);    
    }
}
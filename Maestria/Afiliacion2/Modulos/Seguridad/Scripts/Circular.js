$(document).ready(function () {
    $("#divDetail").dialog({
        autoOpen: false,
        draggable: true,
        resizable: false,
        fluid: true,
        modal: true,
        width: 720,
        maxWitdh: 720,
        maxHeight: window.innerHeight - 50,
        overflow: "scroll",
        position: ["center", 60],
        title: "Formulario Registro Multa",
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

function onAddClicking(sender, args) {
    showDialog("divDetail");
}

function onCancelClicking(sender, args) {
    closeDialog("divDetail");
    args.set_cancel(true);
}


function OnDeleteDatabase() {
    ConfirmDelete('Esta seguro de eliminar este item y los contactos relacionados ?', event);
}

function ConfirmDelete(sender, args) {
    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        if (shouldSubmit) {
            //initiate the origianal postback again
            this.click();
        }
    });

    var text = "Esta seguro de eliminar la base de datos seleccionada?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");
    args.set_cancel(true);
}

function OnkeyPress(sender, args) {
    console.log(10)
    if (args.get_keyCode() == 13) {
        var rbFilter = $find($("[id$='rbFilter']").attr("id"));
        rbFilter.click();
        args.set_cancel(true);
    }
}

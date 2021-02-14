//html2canvas(document.body, {
//    onrendered (canvas) {
//        var link = document.getElementById('Img1');;
//        var image = canvas.toDataURL();
//        link.src = image;
//    }
//});

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
        position: ["center", "top"],
        title: "Help desk",
        open: function (type, data) { $(this).parent().appendTo("form"); }
    });

});
    $(document).ready(function () {
        $("#divDescripcion").dialog({
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
            title: "Modificar Descripcion de Datos Adjuntos",
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


$(document).ready(function () {
    AjaxFileUpload_change_text();
});

function pageLoad(sender, args) {

    //By jQuery
    $(".ajax__fileupload_selectFileButton").text("Seleccione archivo");

    //By JavaScript
    document.getElementsByClassName('ajax__fileupload_selectFileButton')[0].innerHTML = "Seleccione archivo";
    //By jQuery
    $(".ajax__fileupload_dropzone").text("Arrastre su archivo aqui");
    $(".ajax__fileupload_uploadbutton").text("Cargar");

    //By JavaScript
    document.getElementsByClassName('ajax__fileupload_dropzone')[0].innerHTML = "Arrastre su archivo aqui";
    document.getElementsByClassName('ajax__fileupload_uploadbutton')[0].innerHTML = "Cargar";
}

function AjaxFileUpload_change_text() {

    Sys.Extended.UI.Resources.AjaxFileUpload_SelectFile = "Selecione..."
    Sys.Extended.UI.Resources.AjaxFileUpload_DropFiles = "Arrastre foto";
    Sys.Extended.UI.Resources.AjaxFileUpload_Pending = "Pendiente";
    Sys.Extended.UI.Resources.AjaxFileUpload_Remove = "Borrar";
    Sys.Extended.UI.Resources.AjaxFileUpload_Upload = "Cargar";
    Sys.Extended.UI.Resources.AjaxFileUpload_Uploaded = "Cargado";
    Sys.Extended.UI.Resources.AjaxFileUpload_UploadedPercentage = "Cargado {0} %";
    Sys.Extended.UI.Resources.AjaxFileUpload_Uploading = "Cargando";
    Sys.Extended.UI.Resources.AjaxFileUpload_FileInQueue = "{0} Archivo(s) en cola.";
    Sys.Extended.UI.Resources.AjaxFileUpload_AllFilesUploaded = "Todos los archivos cargados.";
    Sys.Extended.UI.Resources.AjaxFileUpload_FileList = "Lista de archivos cargados:";
    Sys.Extended.UI.Resources.AjaxFileUpload_SelectFileToUpload = "Por favor seleccione archivo(s) para cargar.";
    Sys.Extended.UI.Resources.AjaxFileUpload_Cancelling = "Cancelando...";
    Sys.Extended.UI.Resources.AjaxFileUpload_UploadError = "Ha ocurrido un error durante la carga del archivo.";
    Sys.Extended.UI.Resources.AjaxFileUpload_CancellingUpload = "Cancelando carga...";
    Sys.Extended.UI.Resources.AjaxFileUpload_UploadingInputFile = "Cargando archivo: {0}.";
    Sys.Extended.UI.Resources.AjaxFileUpload_Cancel = "Cancelar";
    Sys.Extended.UI.Resources.AjaxFileUpload_Canceled = "Cancelado";
    Sys.Extended.UI.Resources.AjaxFileUpload_UploadCanceled = "Archivo cargado cancelado";
    Sys.Extended.UI.Resources.AjaxFileUpload_DefaultError = "Error al cargar el archivo";
    Sys.Extended.UI.Resources.AjaxFileUpload_UploadingHtml5File = "Cargando archivo: {0} de tamaño {1} bytes.";
    Sys.Extended.UI.Resources.AjaxFileUpload_error = "error";
    Sys.Extended.UI.Resources.AjaxFileUpload_DropFiles = "Arrastre archivos aqui"
}

function CerrarDescripcion(sender, args) {
    closeDialog("divDescripcion");
    args.set_cancel(true);
    $('html, body').css('overflowY', 'auto');
}
function onCancelAdjunto(sender, args) {
    closeDialog("divDetail");
    args.set_cancel(true);
    $('html, body').css('overflowY', 'auto');
}
function LimpiarMyFileUpload(sender, args) {
    $(".ajax__fileupload_queueContainer").empty();
}

function onAddClicking(sender, args) {
    showDialog("divDetail");
}

function OnEditPosition() {
    showDialog("divDetail");
}

function onCancelClicking(sender, args) {
    closeDialog("divDetail");
    args.set_cancel(true);
}

function funValidate(sender, args) {
    var strMessage = "";
    var rcbModulo = $telerik.findComboBox(AVA.getID("rcbModulo"));
    var rcbFormulario = $telerik.findComboBox(AVA.getID("rcbFormulario"));
    var rcbTipoTicket = $telerik.findComboBox(AVA.getID("rcbTipoTicket"));
    var rcbPrioridad = $telerik.findComboBox(AVA.getID("rcbPrioridad"));
    var rcbEstadoHelpDesk = $telerik.findComboBox(AVA.getID("rcbEstadoHelpDesk"));
    var rtbComentario = $find($("[id$='rtbComentario']").attr("id"));
    //var rcbParent = $telerik.findComboBox($("[id$='rcbParent']").attr("id"));

    if (rcbModulo.get_value() == "-1") {
        strMessage += " - El modulo es requerido <br />";
    }
    if (rcbFormulario.get_value() == "-1") {
        strMessage += " - El formulario es requerido <br />";
    }
    if (rcbTipoTicket.get_value() == "-1") {
        strMessage += " - El tipo de ticket es requerido <br />";
    }
    if (rcbPrioridad.get_value() == "-1") {
        strMessage += " - La prioridad es requerida <br />";
    }
    if (rcbEstadoHelpDesk.get_value() == "-1") {
        strMessage += " - El estado es requerido <br />";
    }
    if (rtbComentario.isEmpty()) {
        strMessage += " - El comentario es requerido. <br />";
    }
    //if (rtbParent.get_value() == "-1") {
    //    strMessage += " - Cargo superior es requerido <br />";
    //}

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

    var text = "¿Esta seguro de eliminar el cargo seleccionado?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");

    args.set_cancel(true);

}

function AbrirDescripcion(sender, args) {
    $('html, body').css('overflowY', 'hidden');
    showDialog("divDescripcion");
}
function OnkeyPressEnterSearch(sender, args) {
    if (args.get_keyCode() == 13) {
        var rbFilter = $find($("[id$='rbFilter']").attr("id"));
        var rtbFilter = $find($("[id$='rtbFilter']").attr("id"));
        rbFilter.click();
        args.set_cancel(true);
    }
}

function ConfirmDeleteAdjunto(sender, args) {

    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        if (shouldSubmit) {
            //initiate the origianal postback again
            this.click();
        }
    });

    var text = "Esta seguro de eliminar el Dato Adjunto seleccionado?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");

    args.set_cancel(true);

}
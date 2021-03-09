$(document).ready(function () {
    $("#divDetailHotelPrograma").dialog({
        autoOpen: false,
        draggable: true,
        resizable: false,
        fluid: true,
        modal: true,
        width: "auto",
        maxWitdh: window.innerWidth,
        maxHeight: window.innerHeight,
        overflow: "scroll",
        position: ["center", "top"],
        title: "Asignación de programas",
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
        maxWitdh: window.innerWidth,
        maxHeight: window.innerHeight,
        overflow: "scroll",
        position: ["center", "top"],
        title: "Modificar descripción de datos adjuntos",
        open: function (type, data) { $(this).parent().appendTo("form"); }
    });
});


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
        title: "Datos de hotel",

        open: function (type, data) { $(this).parent().appendTo("form"); }
    });
    //$("#divDetail").dialog("widget").find(".ui-dialog-titlebar-close").hide();
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


function abrirgaleria() {
    var Ocultito = $get($("[id$=_Ocultito]").attr("id"));
}


$('#form').keypress(function (e) { //generico luna generico!!!!!
    var key = e.which;
    if (key == 13)  
    {
        var rbFilter = $find($("[id$='rbFilter']").attr("id")); //nombre del boton de la busqueda !!
        $("#rtbFilter").focus(           //Nombre del radtextbox de donde se busca!!
            rbFilter.click()
        );
        e.preventDefault();
    }
});

$(document).ready(function () {
    AjaxFileUpload_change_text();
});

//function pageLoad(sender, args) {

//    //By jQuery
//    $(".ajax__fileupload_selectFileButton").text("Seleccione Archivo");

//    //By JavaScript
//    document.getElementsByClassName('ajax__fileupload_selectFileButton')[0].innerHTML = "Seleccione Archivo";
//    //By jQuery
//    $(".ajax__fileupload_dropzone").text("Arrastre su Archivo Aqui");
//    $(".ajax__fileupload_uploadbutton").text("Cargar");
    
//    //By JavaScript
//    document.getElementsByClassName('ajax__fileupload_dropzone')[0].innerHTML = "Arrastre su Archivo Aqui";
//    document.getElementsByClassName('ajax__fileupload_uploadbutton')[0].innerHTML = "Cargar";
//}

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

function LimpiarMyFileUpload(sender, args) {
    $(".ajax__fileupload_queueContainer").empty();
    //var rtbDescripcionAdjuntos = $find($("[id$='rtbDescripcionAdjuntos']").attr("id"));
    //rtbDescripcionAdjuntos.set_value("");
    //ctl00_ContentPlaceHolder1_rtbDescripcionAdjuntos
}
function CerrarDescripcion(sender, args) {
    closeDialog("divDescripcion");
    args.set_cancel(true);
    $('html, body').css('overflowY', 'auto');
}

function AbrirDescripcion(sender, args) {
    $('html, body').css('overflowY', 'hidden');
    showDialog("divDescripcion");
}

//function Open() {
//    window.radopen(data, 'RadWindow2');
//}

//function onAddClicking2(sender, args) {
//    var rbAdjuntar = $find($("[id$='rbAdjuntar']").attr("id"));
//    if (rbAdjuntar.enabled = false) {
//        args.set_cancel(true);
//    }
//}

//Hotel Programa
var oculto = "0"
function capturarValor(sender, args) {
    oculto = $find($("[id$='oculto']").attr("id"));
    oculto = sender.get_value();
}
function ValorCambia(sender, args) {
    var regex = /\(([\w\s]*)\)/g,
    match,
    resultado = [];
    var rntValor = $telerik.$("[id$='rntValor']").text();
    while ((match = regex.exec(rntValor)) !== null) {
        resultado.push(match[1]);
    }
    var nuevoValor = (parseFloat(resultado[0]) - oculto) + sender.get_value()
    $telerik.$("[id$='rntValor']").text((rntValor.replace(resultado[0], nuevoValor))) 
}
function Onblur(sender, args)
{
    ////lblCantidadProgr = $find($("[id$='lblCantidadProgr']").attr("id"));
    ////console.log(lblCantidadProgr);
    if (sender.get_value() == "")
    {
        sender.set_value("0");
    }
}


function onAddClicking(sender, args) {
    if (args.get_domEvent().type == 'click') {
        $('html, body').css('overflowY', 'hidden');
        showDialog("divDetail");
    } else {
        if (args.get_keyCode() == 13) {
            args.set_cancel(true);
        } else {
            $('html, body').css('overflowY', 'hidden');
            showDialog("divDetail");
        }
    }
}

function onAddClickingHotelPrograma(sender, args) {
    //$('html, body').css('overflowY', 'hidden');

    $('html, body').css('overflowY', 'hidden');
    showDialog("divDetailHotelPrograma");
}

function OnEditClickingHotelPrograma(sender, args) {
    //$('html, body').css('overflowY', 'hidden');
    showDialog("divDetailHotelPrograma");
}

function onCancelClickingHotelPrograma(sender, args) {
    closeDialog("divDetailHotelPrograma");
    args.set_cancel(true);
    $('html, body').css('overflowY', 'auto');
}

function ConfirmDeleteHotelPrograma(sender, args) {

    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        if (shouldSubmit) {
            //initiate the origianal postback again
            this.click();
        }
    });

    var text = "¿Está seguro de eliminar el ítem seleccionado?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");

    args.set_cancel(true);
}


function OnEditClicking(sender, args) {
    $('html, body').css('overflowY', 'hidden');
    showDialog("divDetail");
}

function onCancelClicking(sender, args) {
    closeDialog("divDetail");
    $('html, body').css('overflowY', 'auto');

    args.set_cancel(true);
    //args.preventDefault();
}

function ValidarHotel(sender, args) {
    var strMessage = "";
    var rtbNombre = $find($("[id$='rtbNombre']").attr("id"));
    var rcbCategoria = $telerik.findComboBox(AVA.getID("rcbCategoria"));
    var rcbEstado = $telerik.findComboBox(AVA.getID("rcbEstado"));
    var rntDormitorio = $find($("[id$='rntDormitorio']").attr("id"));
    var rntBano = $find($("[id$='rntBano']").attr("id"));
    var rntMetrosCuadrados = $find($("[id$='rntMetrosCuadrados']").attr("id"));
    var rntCamas = $find($("[id$='rntCamas']").attr("id"));

    if (rtbNombre.isEmpty()) {
        strMessage += " -El nombre del hotel es requerido <br />";
    }
    if (rcbCategoria.get_value() == "-1") {
        strMessage += " - Seleccione categoria<br />";
    }
    if (rcbEstado.get_value() == "-1") {
        strMessage += " - Seleccione estado<br />";
    }
    if (rntMetrosCuadrados.get_Value > 0) {
        strMessage += " - La cantidad de los dormitorios es requerido <br />";
    }
    if (rntCamas.get_Value > 0) {
        strMessage += " - El numero de camas de los dormitorios es requerido <br />";
    }

    if (strMessage == "") {
        //funCloseModal("divDetail");
    } else {
        proShow_Validation("Por favor revise los siguientes ítems: <br />" + strMessage, "VALIDACION");
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

    var text = "¿Está seguro de eliminar el album seleccionado?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");

    args.set_cancel(true);

}
function ConfirmDeleteAdjunto(sender, args) {

    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        if (shouldSubmit) {
            //initiate the origianal postback again
            this.click();
        }
    });

    var text = "¿Está seguro de eliminar el dato adjunto seleccionado?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");

    args.set_cancel(true);

}

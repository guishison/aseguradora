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
        fluid: true,
        modal: true,
        width: "auto",
        maxWidth: window.innerWidth,
        maxHeight: window.innerHeight,
        overflow: "scroll",
        position: ["center", "top"],
        title: "Permisos habilitados de la plantilla",
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
        title: "Datos de la plantilla",
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
    //var rcbType = funFind_Control("rcbType");

    //if (rcbType.get_value() == "-1") {
    //    proShow_Validation("Classifier Type is required.", "VALIDATION");
    //    args.set_cancel(true);
    //} else {
    showDialog("divDetail");
    //}
}

var index;

function RowMouseOver(sender, eventArgs) {
    index = eventArgs.get_itemIndexHierarchical();
}

function OnEditProfile() {
    showDialog("divDetail");
}

function onCancelClicking(sender, args) {
    closeDialog("divDetail");
    args.set_cancel(true);
}
function onAddClicking() {
    console.log("Chau")
    showDialog("divDetail2");
}
function onCancelClicking2(sender, args) {
    console.log("HOla")
    closeDialog("divDetail2");
    args.set_cancel(true);
}

function funValidate2(sender, args) {
    var strMessage = ""
    var rtbNombrePlantilla = $find($("[id$='rtbNombrePlantilla']").attr("id"));
    console.log("hola")
    if (rtbNombrePlantilla.isEmpty()) {
        console.log("choco")
        strMessage += " - El nombre de la plantilla es obligatorio <br />";
    }
    if (strMessage == "") {
        //funCloseModal("divDetail");
    } else {
        proShow_Validation("Verifique lo siguiente: <br />" + strMessage, "VALIDACION");
        args.set_cancel(true);
    }
}

function funValidate(sender, args) {
    //var permisoarray = Array(Array());
    //console.log("hola");
    var permisoarray2 = Array();
    //permisoarray2.push("9,AMBL");
    //permisoarray2.push("14,AML");
    //permisoarray2.push("26,BL");
    //permisoarray2.push("29,AMBL");
    //console.log(permisoarray2);
    //var tabStrip = $find($("[id$='rtsModules']").attr("id"));
    //console.log(tabStrip);
    //var tab=tabStrip.findTabByText("rpv4");
    //var tabla=tab.findControl("rgvModule_Administracion");


    var strMessage = "";
    var rtbProfile = $find($("[id$='rtbProfile']").attr("id"));
    var hdnOptionIds = $get($("[id$=_hdnOptionIds]").attr("id"));
    var hdnOptionIds2 = $get($("[id$=_hdnOptionIds2]").attr("id"));
    //var chau = $("[id$='_rpvTabPages']").attr("id");
    //console.log(chau); console.log("Chau");
    hdnOptionIds.value = "";
    var rtsModules = $find($("[id$='rtsModules']").attr("id"));
    //console.log(rtsModules);
    //console.log(rtsModules.get_element());
    //console.log(rtsModules.get_element().innerText);
    var aqui = rtsModules.get_element().innerText.trim().split("\n");
    //console.log(aqui);

    $("#" + $("[id$='_rpvTabPages']").attr("id") + " input[type=checkbox]:checked").each(function () {

        var intOption = this.value;
        var hola = $("#" + $("[id$='_rpvTabPages']").attr("id"));
        //console.log(hola);
        var hola2 = $("#" + $("[id$='_rpvTabPages']").attr("id") + " input[type=checkbox]:checked");
        //console.log(aqui.length);
        //console.log("sigoo");
        for (var x = 0; x < aqui.length; x++) {
            //console.log("for");
            //console.log(String(this.id));
            //console.log(String(this.id).includes("00"));
            var valor1, valor2 = ""
            if (String(this.id).includes(aqui[x])) {
                //console.log(String(this.id).charAt(this.id.length - 1));
                if (String(this.id).charAt(this.id.length - 1) == "0") {
                    valor1 = this.value;
                    valor2 = "A"
                    permisoarray2.push(valor1.concat("-", valor2));
                } if (String(this.id).charAt(this.id.length - 1) == "1") {
                    valor1 = this.value;
                    valor2 = "M"
                    permisoarray2.push(valor1.concat("-", valor2));
                } if (String(this.id).charAt(this.id.length - 1) == "2") {
                    valor1 = this.value;
                    valor2 = "B"
                    permisoarray2.push(valor1.concat("-", valor2));
                } if (String(this.id).charAt(this.id.length - 1) == "3") {
                    valor1 = this.value;
                    valor2 = "E"
                    permisoarray2.push(valor1.concat("-", valor2));
                }
                //console.log(permisoarray2);
            }

            //console.log(hola2);
            hdnOptionIds2.value += intOption + ",";

        }

    });
    hdnOptionIds.value = permisoarray2

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

    var text = "¿Está seguro de eliminar la plantilla seleccionada?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");

    args.set_cancel(true);

}
//#endregion

//#region --- METHODS ---

//#endregion
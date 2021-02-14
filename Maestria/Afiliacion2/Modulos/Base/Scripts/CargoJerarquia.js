$(document).ready(function () {
    $("#divDetail").dialog({
        autoOpen: false,
        draggable: true,
        resizable: false,
        modal: true,
        width: 520,
        position: ["center", 100],
        title: "Datos del cargo",
        open: function (type, data) { $(this).parent().appendTo("form"); }
    });
});

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

    var rtbDescription = $find($("[id$='rtbDescription']").attr("id"));
    //var rcbParent = $telerik.findComboBox($("[id$='rcbParent']").attr("id"));

    if (rtbDescription.isEmpty()) {
        strMessage += " - Descripción es requerida <br />";
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

    var text = "¿Está seguro de eliminar el cargo seleccionado?";
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
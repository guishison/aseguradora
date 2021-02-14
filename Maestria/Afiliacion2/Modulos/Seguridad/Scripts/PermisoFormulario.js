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
        modal: true,
        width: 490,
        position: ["center", 100],
        title: "Detalle Usuario",
        open: function (type, data) { $(this).parent().appendTo("form"); }
    });
});

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

function funValidate(sender, args) {

    var strMessage = "";
    var rtbProfile = $find($("[id$='rtbProfile']").attr("id"));
    var hdnOptionIds = $get($("[id$=_hdnOptionIds]").attr("id"));

    hdnOptionIds.value = "";
    $("#" + $("[id$='_rpvTabPages']").attr("id") + " input[type=checkbox]:checked").each(function () {
        var intOption = this.value;
        hdnOptionIds.value += intOption + ",";
    });

    if (rtbProfile.isEmpty()) {
        strMessage += " - El nombre del perfil es obligatorio <br />";
    }

    if (strMessage == "") {
        //funCloseModal("divDetail");
    } else {
        proShow_Validation("Por favor verifique lo siguiente: <br />" + strMessage, "VALIDACION");
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

    var text = "Esta seguro de eliminar el perfil seleccionado?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");

    args.set_cancel(true);

}
//#endregion

//#region --- METHODS ---

//#endregion

function OnkeyPress(sender, args) {
    if (args.get_keyCode() == 13) {
        //var rbSaveList = $find($("[id$='rbSaveList']").attr("id"));
        //rbSaveList.click();
        args.set_cancel(true);
    }
}
//#########   validar cambio de interbalos de cuotas desde ##########
var oculto = "0"
var DatoHasta = 0
function capturarValor(sender, args) {
    oculto = $find($("[id$='oculto']").attr("id"));
    oculto = sender.get_value();
    var sw = 0;
    var ce = $find($("[id$='rgvGrid']").attr("id")).get_masterTableView();
    var Rows = ce.get_dataItems();

    for (var i = 0; i < Rows.length; i++) {
        var row = Rows[i];
        var rnbCuotaInferior = row.findControl("rnbValorInicial");
        var rnbCuotaSuperior = row.findControl("rnbValorFinal");
        var desde = rnbCuotaInferior.get_value();
        var hasta = rnbCuotaSuperior.get_value();
        if (desde > oculto) {
            DatoHasta = hasta;
        }
    }
}
function ValorCambia(sender, args) {
    var strMessage = "";
    var sw = 0;
    var ce = $find($("[id$='rgvGrid']").attr("id")).get_masterTableView();
    var nuevoDato = sender.get_value();
    var Rows = ce.get_dataItems();
    var i = 0;
    while (i < Rows.length) {
        var row = Rows[i];
        var rnbCuotaInferior = row.findControl("rnbValorInicial");
        var rnbCuotaSuperior = row.findControl("rnbValorFinal");
        var desde = rnbCuotaInferior.get_value();
        var hasta = rnbCuotaSuperior.get_value();
        if (DatoHasta == hasta) {
            if (nuevoDato > hasta) {
                i = Rows.length;
                rnbCuotaInferior.set_value(hasta);
                sw = 1;
            } else {

            }
        } else {
            if (desde <= nuevoDato && nuevoDato <= hasta) {
                i = Rows.length;

                var j = 0;

                while (j < Rows.length) {
                    var row2 = Rows[j];
                    var rnbCuotaSuperior2 = row2.findControl("rnbValorInicial");
                    var hasta2 = rnbCuotaSuperior2.get_value();
                    if (DatoHasta == hasta2) {
                        var rnbCuotaInferior2 = row2.findControl("rnbValorFinal");
                        rnbCuotaInferior2.set_value(oculto);
                        j = Rows.length;
                    }
                    j = j + 1;
                }
            }
        }
        i = i + 1;
    }
}
function Onblur(sender, args) {
    if (sender.get_value() == "") {
        sender.set_value("0");
    }
}
//#########   validar cambio de interbalos de cuotas hasta ##########
var oculto2 = "0"
var datoDesde = 0
function capturarValor2(sender, args) {
    oculto2 = $find($("[id$='oculto2']").attr("id"));
    oculto2 = sender.get_value();
    var sw = 0;
    var ce = $find($("[id$='rgvGrid']").attr("id")).get_masterTableView();
    var Rows = ce.get_dataItems();

    for (var i = 0; i < Rows.length; i++) {
        var row = Rows[i];
        var rnbCuotaInferior = row.findControl("rnbValorInicial");
        var rnbCuotaSuperior = row.findControl("rnbValorFinal");
        var desde = rnbCuotaInferior.get_value();
        var hasta = rnbCuotaSuperior.get_value();
        console.log(desde);
        console.log(i);
        console.log(hasta);
        if (hasta > oculto2) {
            datoDesde = desde;
        }
    }
}
function ValorCambia2(sender, args) {
    var strMessage = "";
    var sw = 0;
    var ce = $find($("[id$='rgvGrid']").attr("id")).get_masterTableView();
    var nuevoDato = sender.get_value();
    var Rows = ce.get_dataItems();
    var i = 0;
    while (i < Rows.length) {
        var row = Rows[i];
        var rnbCuotaInferior = row.findControl("rnbValorInicial");
        var rnbCuotaSuperior = row.findControl("rnbValorFinal");
        var desde = rnbCuotaInferior.get_value();
        var hasta = rnbCuotaSuperior.get_value();
        console.log(desde);
        console.log(i+i);
        console.log(hasta);
        if (datoDesde == desde) {
            if (nuevoDato < desde) {
                i = Rows.length;
                rnbCuotaSuperior.set_value(desde);
                sw = 1;
            } else {

            }
        } else {
            if (desde <= nuevoDato && nuevoDato <= hasta) {
                i = Rows.length;
                var j = 0;
                while (j < Rows.length) {
                    var row2 = Rows[j];
                    var rnbCuotaInferior2 = row2.findControl("rnbValorInicial");
                    var desde2 = rnbCuotaInferior2.get_value();
                    if (datoDesde == desde2) {
                        var rnbCuotaSuperior2 = row2.findControl("rnbValorFinal");
                        rnbCuotaSuperior2.set_value(oculto2);
                        j = Rows.length;
                    }
                    j = j + 1;
                }
            }
        }
        i = i + 1;
    }
}
function Onblur2(sender, args) {
    if (sender.get_value() == "") {
        sender.set_value("0");
    }
}
//###################################$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
function OnkeyPressGuardar(sender, args) {

    if (args.get_keyCode() == 13) {
        var rtbSave = $find($("[id$='rtbSave']").attr("id"));

        var strMessage = "";
        var rtbNombre = $find($("[id$='rtbNombre']").attr("id"));
        //var rcbHotel = $telerik.findComboBox(AVA.getID("rcbHotel"));

        var rtbDescripcion = $find($("[id$='rtbDescripcion']").attr("id"));

        //if (rcbHotel.get_value() == "-1") {
        //    strMessage += " - Seleccione un Hotel <br />";
        //}
        if (rtbNombre.isEmpty()) {
            strMessage += " - El Nombre es requerido <br />";
        }
        if (rtbDescripcion.isEmpty()) {
            strMessage += " - La Descripcion es requerida <br />";
        }
        if (strMessage != "") {
            proShow_Validation("Porfavor revise los siguientes errores: <br />" + strMessage, "VALIDACION");
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

    var text = "Esta seguro de eliminar la base de datos seleccionada?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");
    args.set_cancel(true);
}

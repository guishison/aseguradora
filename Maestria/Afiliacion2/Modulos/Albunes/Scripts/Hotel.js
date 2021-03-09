


//(function (global, undefined) {
//    var demo = {};
//    var lastClickedImg = null;
//    var $ = $telerik.$;

//    function sizePreviewWindow() {

//        //Debido a los detalles de la demostracion: cargando dinamicamente una imagen y dandole el tamaño del radwindows basado en eso
//        // Tamaño de la imagen, debemos asegurarnos de que la imagen se cargue antes de llamar al metodo autoSize()
//        demo.previewWin.autoSize(false);
//    }

//    function openWin(clickedImg) {
//        //change the border of the clicked image
//        $(clickedImg).parent().css("background-color", "red");
//        console.log(demo);
//        //get the name of the thumbnail image
//        var imgName = clickedImg.src.substring(clickedImg.src.lastIndexOf("/") + 1);
//        //use the thumbnail image's name to build the src for the preview window
//        demo.imgHolder.src = "../../assets/image/Hotel_Pucon/" + imgName;

//        //show the window
//        demo.previewWin.show();


//        //clear the border of the previously clicked image
//        $(lastClickedImg).parent().css("background-color", "");

//        lastClickedImg = clickedImg;
//    }

//    function toggleExpand(clickedLink) {
//        //togle the hidden pane containing extra images
//        $('#hiddenPane').toggle();
//        //change link's text
//        if ($.trim($(clickedLink).text()) == "Mostrar Todos") {
//            $(clickedLink).text("Show less").addClass("showLess");
//        } else {
//            $(clickedLink).text("Show more").removeClass("showLess");
//        }

//        demo.galleryWin.restore();

//        //autosize the gallery window
//        demo.galleryWin.autoSize(false);
//    }

//    global.$autoSizeDemo = demo;
//    global.sizePreviewWindow = sizePreviewWindow;
//    global.toggleExpand = toggleExpand;
//    global.openWin = openWin;
//})(window);






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
        title: "HOTEL",
        
        open: function (type, data) { $(this).parent().appendTo("form"); }
    });
    //$("#divDetail").dialog("widget").find(".ui-dialog-titlebar-close").hide();
});
//$(document).ready(function () {
//    $("#divDetail3").dialog({
//        autoOpen: false,
//        draggable: true,
//        resizable: false,
//        fluid: true,
//        modal: true,
//        width: window.innerWidth-100,
//        maxWitdh: window.innerWidth-50,
//        Height: window.innerHeight-100,
//        maxHeight: window.innerHeight - 50,
//        overflow: "scroll",
//        position: ["center", 0],
//        title: "Visores",
//        open: function (type, data) { $(this).parent().appendTo("form"); }
//    });
//});

// on window resize run function
$(window).resize(function () {
    fluidDialog();
});

// catch dialog if opened within a viewport smaller than the dialog width
$(document).on("dialogopen", ".ui-dialog", function (event, ui) {
    fluidDialog();
});
//$(document).ready(function () { alert('C:/Users/programador5/Desktop/repositorio/testFundatio/Crm_Sage/Afiliacion2 ---- C:/Users/programador5/Desktop/repositorio/testFundatio/Crm_Sage/'); });

//var $lg = $('#divDetail3').lightGallery();
//console.log($lg);

//// Perform any action just before opening the gallery
//$lg.on('onCloseAfter.lg', function (event) {
//    $('#divDetail3').data('lightGallery').destroy(true)
//});


function abrirgaleria() {
    var Ocultito = $get($("[id$=_Ocultito]").attr("id"));

    //$(document).ready(function () {
    //        $('#divDetail3').lightGallery({
    //            dynamic: true,
    //            html: true,
    //            mobileSrc: true,
    //            dynamicEl: [
    //                { "src": "../../assets/image/Hotel_Pucon/11.jpg", "thumb": "../../assets/image/Hotel_Pucon/11.jpg", "sub-html": "<div class='custom-html'><h4>Custom HTML</h4></div>", "mobileSrc": "../../assets/image/Hotel_Pucon/11.jpg" },
    //                { "src": "../../assets/image/Hotel_Pucon/11.jpg", "thumb": "../../assets/image/Hotel_Pucon/11.jpg", "sub-html": "#divDetail3", "mobileSrc": "../../assets/image/Hotel_Pucon/11.jpg" },
    //                { "src": "../../assets/image/Hotel_Pucon/11.jpg", "thumb": "../../assets/image/Hotel_Pucon/11.jpg", "sub-html": "#divDetail3", "mobileSrc": "../../assets/image/Hotel_Pucon/11.jpg" }
    //            ]
    //        });        
    //});
    //$(document).ready(function () {
    //    $('#divDetail3').lightGallery({
    //        dynamic: true,
    //        html: true,
    //        mobileSrc: true,
    //        dynamicEl: [
    //        { 'src': '../../assets/image/Hotel_Pucon/1.jpg', 'thumb': '../../assets/image/Hotel_Pucon/1.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/1.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/10.jpg', 'thumb': '../../assets/image/Hotel_Pucon/10.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/10.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/11.jpg', 'thumb': '../../assets/image/Hotel_Pucon/11.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/11.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/12.jpg', 'thumb': '../../assets/image/Hotel_Pucon/12.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/12.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/13.jpg', 'thumb': '../../assets/image/Hotel_Pucon/13.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/13.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/14.jpg', 'thumb': '../../assets/image/Hotel_Pucon/14.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/14.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/15.jpg', 'thumb': '../../assets/image/Hotel_Pucon/15.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/15.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/2.jpg', 'thumb': '../../assets/image/Hotel_Pucon/2.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/2.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/3.jpg', 'thumb': '../../assets/image/Hotel_Pucon/3.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/3.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/4.jpg', 'thumb': '../../assets/image/Hotel_Pucon/4.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/4.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/5.jpg', 'thumb': '../../assets/image/Hotel_Pucon/5.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/5.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/6.jpg', 'thumb': '../../assets/image/Hotel_Pucon/6.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/6.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/7.jpg', 'thumb': '../../assets/image/Hotel_Pucon/7.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/7.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/8.jpg', 'thumb': '../../assets/image/Hotel_Pucon/8.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/8.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/9.jpg', 'thumb': '../../assets/image/Hotel_Pucon/9.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/9.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/enjoy.jpg', 'thumb': '../../assets/image/Hotel_Pucon/enjoy.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/enjoy.jpg' },
    //        { 'src': '../../assets/image/Hotel_Pucon/enjoy2.jpg', 'thumb': '../../assets/image/Hotel_Pucon/enjoy2.jpg', 'Sub-html': "<div Class='custom-html'><h4>Custom HTML</h4></div>", 'mobileSrc': '../../assets/image/Hotel_Pucon/enjoy2.jpg' },
    //        ]
    //    });
    //});



    ////showDialog("divDetail3");
    //var hola = $('#divDetail3');
    ////console.log(variables);
    //$('#divDetail3').html("<a href='../../assets/image/Hotel_Pucon/10.jpg'><img src='../../assets/image/Hotel_Pucon/11.jpg' /></a>");
    //$('#divDetail3').lightGallery({
    //    showThumbByDefault:true,
    //    addClass:'showThumbByDefault'
    //}); 
}


//$('#divDetail3').on('click', function () {
//    console.log($(this));
//    $(this).lightGallery({
//        dynamic: true,
//        dynamicEl: [{
//            "src": '../../assets/image/Hotel_Pucon/11.jpg',
//            'thumb': '../../assets/image/Hotel_Pucon/11.jpg',
//            'subHtml': '<h4>Prueba1</h4><p>Aqui puede ir la descripcion de la imagen de los hoteles</p>'
//        }, {
//            'src': '../../assets/image/Hotel_Pucon/12.jpg',
//            'thumb': '../../assets/image/Hotel_Pucon/12.jpg',
//            'subHtml': "<h4>Prueba2</h4><p>Aqui puede ir la descripcion de la imagen de los hoteles</p>"
//        }, {
//            'src': '../../assets/image/Hotel_Pucon/13.jpg',
//            'thumb': '../../assets/image/Hotel_Pucon/13.jpg',
//            'subHtml': "<h4>Prueba3</h4><p>Aqui puede ir la descripcion de la imagen de los hoteles</p>"
//        }]
//    })

//});

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

//$("body").dialog({
//    close: function (event, ui) {
//        $('html, body').css('overflowY', 'auto');
//    }
//});

//$("divDetail").on("dialogclose", function (event, ui) { $('html, body').css('overflowY', 'auto'); });


//$('#form').keypress(function (e) {
//    console.log("holapas");
//    var key = e.which;
//    if (key == 13)  // the enter key code
//    {
//        if ($("#rbFilter").is(":focus")) {
//            var rbFilter = $find($("[id$='rbFilter']").attr("id"));
//            rbFilter.click();
//            e.preventDefault();
//        }

//        e.preventDefault();
//    }
//});

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
        strMessage += " -El Nombre del hotel es requerido <br />";
    }
    if (rcbCategoria.get_value() == "-1") {
        strMessage += " - Seleccione Categoria<br />";
    }
    if (rcbEstado.get_value() == "-1") {
        strMessage += " - Seleccione Estado<br />";
    }
    if (rntMetrosCuadrados.get_Value > 0) {
        strMessage += " - La dimension de los dormitorios es requerido <br />";
    }
    if (rntCamas.get_Value > 0) {
        strMessage += " - El numero de camas de los dormitorios es requerido <br />";
    }

    if (strMessage == "") {
        //funCloseModal("divDetail");
    } else {
        proShow_Validation("Por Favor revise los siguientes Items: <br />" + strMessage, "VALIDACION");
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

    var text = "Esta seguro de eliminar el hotel seleccionado?";
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

    var text = "Esta seguro de eliminar el Dato Adjunto seleccionado?";
    radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");

    args.set_cancel(true);

}

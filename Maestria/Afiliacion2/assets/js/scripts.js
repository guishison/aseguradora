//#region --- COMMON VARIABLES ---
if (typeof AVA !== 'object') {
    AVA = {};
}

var Actions = { "NONE": 0, "INSERT": 1, "UPDATE": 2, "DELETE": 3 };

//#endregion

//#region --- COMMON METHODS ---

function funTrim(p_strCad) {
    p_strCad = p_strCad.replace(/^\s*|\s*$/g, "");
    return p_strCad;
}

function webdte() {
    //window.location = "http:\\www.dte.com.bo"
}
function Clickcito() {
    var link;
    var image;
    var SuperOculto;

    var elemento = window.location.href.split("/")
    document.getElementById('ImgHelpDesk').href = document.getElementById('ImgHelpDesk').href + '?ModuloId=' + elemento[elemento.length - 2] + '/' + elemento[elemento.length - 1];
    html2canvas(document.body, {
        onrendered(canvas) {
            link = document.getElementById('Img1');
            image = canvas.toDataURL();
            link.src = image;
            localStorage.setItem("imgDatas", "Pruebanga")
            localStorage.setItem("imgDatass", image)
            //var path = window.location.host;
            //var path2 = window.location.hostname;
            //var path3 = window.location.origin;

        }
    });
}
var Automatic;
function SuperEmpty() {
    var allRadControls = $telerik.radControls;
    var i = 0
    for (i; i < allRadControls.length; i++) {
        if (allRadControls[i]._element.className.includes("riTextBox")) {
            //console.log(allRadControls[i]);
            if (allRadControls[i]._element.className.includes("riDisabled") & allRadControls[i]._validationText == "") {
                allRadControls[i].addCssClass("EmptyDisabled");
            }
        }
        if (i == allRadControls.length - 1) {
            clearInterval(Automatic);
        }
    }

}

var cont1 = 31;
var cont2 = 32;
$.widget("ui.dialog", $.ui.dialog,
    {
        open: function () {
            var $dialog = $(this.element[0]);

            var maxZ = $dialog.css("z-Index");
            //$('*').each(function () {
            //    var thisZ = $(this).css('zIndex');
            //    thisZ = (thisZ === 'auto' ? (Number(maxZ) + 1) : thisZ);
            //    if (thisZ > maxZ) maxZ = thisZ;
            //});

            $(".ui-widget-overlay").css("zIndex", (maxZ / 3 + cont1));
            $dialog.parent().css("zIndex", (maxZ / 3 + cont2));
            cont1 = cont1 + 2
            cont2 = cont2 + 2;
            return this._super();
        }
    });
$.widget("ui.dialog", $.ui.dialog,
    {
        close: function () {


            $(".ui-widget-overlay").css("zIndex", 0);
            //close();
            return this._super();
        }
    });

var formatNumber = {
    separador: ".", // separador para los miles
    sepDecimal: ',', // separador para los decimales
    formatear: function (num) {
        num += '';
        var splitStr = num.split('.');
        var splitLeft = splitStr[0];
        var splitRight = splitStr.length > 1 ? this.sepDecimal + splitStr[1] : '';
        var regx = /(\d+)(\d{3})/;
        while (regx.test(splitLeft)) {
            splitLeft = splitLeft.replace(regx, '$1' + this.separador + '$2');
        }
        return this.simbol + splitLeft + splitRight;
    },
    new: function (num, simbol) {
        this.simbol = simbol || '';
        return this.formatear(num);
    }
}
//function that converts a String to Date
function funConvertToDate(string) {
    var date = new Date()
    mes = parseInt(string.substring(3, 5));
    date.setMonth(mes - 1); //en javascript los meses van de 0 a 11
    date.setDate(string.substring(0, 2));
    date.setYear(string.substring(6, 10));
    return date;
}
$(document).keypress(function (event) {
    var key = event.which;
    //console.log(key)
    if (key == 60 || key == 13) {
        event.preventDefault();
    }
});
//$(document).keydown(function (event) {
//    var key = event.which;
//    if (event.ctrlKey) {
//        if (event.keyCode == 74 && event.ctrlKey) {
//            event.preventDefault();
//        }
//    }
//});

function radComboonBlur(comboBox, eventArgs) {
    try {
        comboBox.get_highlightedItem().select();
    }
    catch (err) {
        return;
    }
}
//document.onkeydown = function (e) {
//    e = window.event;
//    if (!window.event) return false;
//    if (e.keyCode == 74 && e.ctrlkey == true)
//    { return false; }
//}
//$(document).blur(function (event) {    
//    console.log(event);
//});
function haha() {
    $('section[onload]').trigger('onload');
}
$(function () {
    $('section[onload]').trigger('onload');
});
var Intervall
function RemoverPanel() {
    Intervall = window.setInterval(RemoverPanel2, 300);
}

function RemoverPanel2() {

    var clase = document.getElementById("Bodycito");
    clase.classList.remove("MyModalPanel");
    clearInterval(Intervall);
}
//Verifies if it is a numeric intervals and returns a boolean
function funIsNumeric(p_strText) {
    var ValidChars = "0123456789.";
    var IsNumber = true;
    var Char;

    if (p_strText != null) {
        for (i = 0; i < p_strText.length && IsNumber == true; i++) {
            Char = p_strText.charAt(i);
            if (ValidChars.indexOf(Char) == -1) {
                IsNumber = false;
            }
        }
    }
    else
        IsNumber = false;

    return IsNumber;
}


(function (global) {
    var chart;

    function ChartLoad(sender, args) {
        chart = sender.get_kendoWidget(); //store a reference to the Kendo Chart widget, we will use its methods
    }

    global.ChartLoad = ChartLoad;

    function resizeChart() {
        if (chart)
            chart.resize(); //por si cambia el tamaño de la ventana
    }


    //por si las dudas verifica cada 200 ms
    var TO = false;
    window.onresize = function () {
        if (TO !== false)
            clearTimeout(TO);
        TO = setTimeout(resizeChart, 200);
    }

})(window);


function radComboKeyPress(comboBox, eventArgs) {
    var eventoDom = eventArgs.get_domEvent();
    var keyCode2 = eventoDom.keyCode;
    if (keyCode2 == 9) {
        try {
            comboBox.get_highlightedItem().select();
        }
        catch (err) {
            return;
        }
    }
}


function funAddCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

function funFind_Control(p_strID) {
    var strID = $telerik.$("[id$='" + p_strID + "']").attr("id");
    return $find(strID);
}

function funGet_Control(p_strID) {
    var strID = $telerik.$("[id$='" + p_strID + "']").attr("id");
    return $get(strID);
}

function funGet_ClientID(p_strID) {
    var strID = $telerik.$("[id$='" + p_strID + "']").attr("id");
    return strID;
}

function funGet_DateFromJSON(p_strJSONDate) {
    if (p_strJSONDate == null || p_strJSONDate == "" || p_strJSONDate == "/Date(NaN)/") {
        return "";
    } else {
        var re = /-?\d+/;
        var m = re.exec(p_strJSONDate);
        var objDate = new Date(parseInt(m[0]));
        return objDate;
    }
}

var reISO = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}(?:\.\d*)?)Z$/;
var reMsAjax = /^\/Date\((d|-|.*)\)\/$/;

JSON.fromDate = function (p_objDate) {
    return "/Date(" + Date.parse(p_objDate) + ")/";
};

JSON.toDate = function (p_strDate) {
    var a = reISO.exec(p_strDate);
    if (a)
        return new Date(Date.UTC(+a[1], +a[2] - 1, +a[3], +a[4], +a[5], +a[6]));
    a = reMsAjax.exec(p_strDate);
    if (a) {
        var b = a[1].split(/[-,.]/);
        return new Date(+b[0]);
    }
    return null;
};

/**
* @param scope Object :  The scope in which to execute the delegated function.
* @param func Function : The function to execute
* @param data Object or Array : The data to pass to the function. If the function is also passed arguments, the data is appended to the arguments list. If the data is an Array, each item is appended as a new argument.
*/
function delegate(scope, func, data) {
    return function () {
        var args = Array.prototype.slice.apply(arguments).concat(data);
        func.apply(scope, args);
    }
}

function funLeft(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else
        return String(str).substring(0, n);
}

function funRight(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}

AVA.getID = function (p_strID) {
    return $telerik.$("[id$='" + p_strID + "']").attr("id");
}

//#endregion

//#region --- GOOGLE MAPS METHODS ---

var g_imgPosition = '../../../Resources/Images/Icons/24x24/Position.png';
var g_imgSeller = '../../../Resources/Images/Icons/24x24/User2.png';

//CREATE INFO WINDOWS
function funCreate_InfoWindow(p_Marker, p_Content, p_bolTabs) {
    //g_InfoWindows is a global variable declared on page
    if (p_bolTabs == true) {
        google.maps.event.addListener(p_Marker, 'click', function () {
            g_InfoWindows.setContent(p_Content);
            g_InfoWindows.open(g_Map, p_Marker);
            //setTimeout(function () { g_InfoWindows.close(); }, 4000); //auto close
            $("#tabs").tabs();
        });
    }
    else {
        google.maps.event.addListener(p_Marker, 'click', function () {
            g_InfoWindows.setContent(p_Content);
            g_InfoWindows.open(g_Map, p_Marker);
            //setTimeout(function () { g_InfoWindows.close(); }, 4000); //auto close
        });

        /* CHANGE ICON ON "MOUSEOVER", ALSO HAVE AN EVENT "MOUSEOUT"
        google.maps.event.addListener(p_Marker, "mouseover", function() {
        p_Marker.setIcon(g_imgSeller);
        });*/

        /* SHOW INFOWINDOWS ON MOUSEOVER ICON
        google.maps.event.addListener(p_Marker, 'mouseover', function () {
        g_InfoWindows.setContent(p_Content);
        g_InfoWindows.open(g_Map, p_Marker);
        });*/
    }
}

//CREATE INFO WINDOWS CONTENT
function funContent_InfoWindow(p_Cod, p_Nom, p_NIT, p_Phone, p_Address, p_IdSale, p_bolTabs) {
    var contentString;

    if (p_bolTabs == true) {
        contentString = [
            '<div id="tabs" style="width:300px;">',
            '<ul>',
            '<li><a href="#tab-1"><span>DATOS</span></a></li>',
            '<li><a href="#tab-2"><span>Two</span></a></li>',
            '</ul>',
            '<div id="tab-1">',
            '<p><b>Cod.Cliente : </b> ' + p_Cod + '</p>',
            '<p><b>Nombre : </b> ' + p_Nom + '</p>',
            '</div>',
            '<div id="tab-2">',
            '<p>Tab 2</p>',
            '</div>',
            '</div>'
        ].join('');
    }
    else {
        var report;
        if (p_IdSale == "") {
            report = '<b>Reporte : </b>';
        }
        else {
            report = '<b>Reporte : </b><a href="Reports/SalesBYDateUser.aspx?SaleId=' + p_IdSale + '" target="_blank">Abrir</a>';
        }

        var NIT;
        if (p_NIT == 0) {
            NIT = "";
        } else {
            NIT = p_NIT;
        }

        var Phone;
        if (p_Phone == undefined) {
            Phone = "";
        } else {
            Phone = p_Phone;
        }

        contentString = [
            '<div id="content">',
            '<h3 id="firstHeading" class="firstHeading">DATOS CLIENTE</h3>',
            '<div id="bodyContent">',
            '<b>Cod.Cliente : </b> ' + p_Cod + '<br />',
            '<b>Nombre : </b> ' + p_Nom + '<br />',
            '<b>NIT : </b> ' + NIT + '&nbsp;&nbsp;&nbsp;<b>Tel&eacute;fono : </b> ' + Phone + '<br />',
            '<b>Direcci&oacute;n : </b> ' + p_Address + '<br />',
            report,
            '</div>',
            '</div>'
        ].join('');
    }

    return contentString;
}

function funContent_InfoWindowSeller(p_Name, p_Zone) {
    var contentString;

    contentString = [
        '<div id="content">',
        '<div id="siteNotice">',
        '</div>',
        '<h1 id="firstHeading" class="firstHeading">VENDEDOR</h1>',
        '<div id="bodyContent">',
        '<p><b>Nombre : </b> ' + p_Name + '',
        '<p><b>Ruta : </b> ' + p_Zone + '',
        '</div>',
        '</div>'
    ].join('');

    return contentString;
}

//CREATE MARKERS FOR ALL ZONES
function funCreate_Markers_AllZones(p_Title, p_Latitude, p_Longitude, p_Position) {
    //var myMarker;
    g_Marker = new google.maps.Marker({
        title: p_Title,
        position: new google.maps.LatLng(p_Latitude, p_Longitude),
        clickable: true,
        draggable: false,
        icon: '../../../Resources/Images/Icons/24x24/Route' + p_Position + '.png',
        map: g_Map
    });
}

//CREATE MARKERS
function funCreate_Marker(p_Title, p_Latitude, p_Longitude, p_Count) {
    //g_Marker is a global variable declared on page
    //g_Map is a global variable declared on page
    g_Marker = new google.maps.Marker({
        title: p_Title,
        position: new google.maps.LatLng(p_Latitude, p_Longitude),
        clickable: true,
        draggable: false,
        icon: '../../../Resources/Images/Icons/24x24/Client' + p_Count + '.png',
        map: g_Map
    });
}

function funCreate_MarkerVisit(p_Title, p_Latitude, p_Longitude, p_Count) {
    //g_Marker is a global variable declared on page
    //g_Map is a global variable declared on page
    g_Marker = new google.maps.Marker({
        title: p_Title,
        position: new google.maps.LatLng(p_Latitude, p_Longitude),
        clickable: true,
        draggable: false,
        icon: '../../../Resources/Images/Icons/24x24/Visits/Client' + p_Count + '.png',
        map: g_Map
    });
}

//CREATE MARKERS SELLER
function funCreate_Marker_Seller(p_Title, p_Latitude, p_Longitude, bolSw) {
    //g_Marker is a global variable declared on page
    //g_Map is a global variable declared on page

    var imgIcon = g_imgPosition;
    if (bolSw) {
        imgIcon = g_imgSeller;
    }

    g_Marker_Sellers = new google.maps.Marker({
        title: p_Title,
        position: new google.maps.LatLng(p_Latitude, p_Longitude),
        clickable: true,
        draggable: false,
        icon: imgIcon,
        map: g_Map
    });
}

//CREATE MARKERS
function funCreate_Marker_Seller2(p_Title, p_Latitude, p_Longitude, p_Count) {
    //g_Marker is a global variable declared on page
    //g_Map is a global variable declared on page
    g_Marker_Sellers = new google.maps.Marker({
        title: p_Title,
        position: new google.maps.LatLng(p_Latitude, p_Longitude),
        clickable: true,
        draggable: false,
        icon: '../../../Resources/Images/Icons/24x24/Position' + p_Count + '.png',
        map: g_Map
    });
}

//CREATE DIRECTIONS
function funCreate_Directions(p_strType, p_) {
    if (g_lstMarkers.length > 0) {
        g_DirectionsDisplay = new google.maps.DirectionsRenderer();
        //Set in the map
        g_DirectionsDisplay.setMap(g_Map);
        //Set the options
        g_DirectionsDisplay.setOptions(directionRendererOptions);

        //Starting point
        var starPoint = new google.maps.LatLng(g_lstMarkers[0].position.lat(), g_lstMarkers[0].position.lng());
        //End point
        var endPoint = new google.maps.LatLng(g_lstMarkers[g_lstMarkers.length - 1].position.lat(), g_lstMarkers[g_lstMarkers.length - 1].position.lng());

        var objTypeTravel;
        if (p_strType == 'Pie') {
            objTypeTravel = google.maps.DirectionsTravelMode.WALKING;
            //DRIVING   //WALKING   //BICYCLING
        }
        else {
            objTypeTravel = google.maps.DirectionsTravelMode.DRIVING;
            //DRIVING   //WALKING   //BICYCLING
        }

        var request = {
            origin: starPoint,
            destination: endPoint,
            waypoints: waypts,
            travelMode: objTypeTravel
        };

        var directionsService = new google.maps.DirectionsService();
        directionsService.route(request, function (response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                g_DirectionsDisplay.setDirections(response);
            }
        });
    } // end if
}

//CREATE POLYLINES
function funCreate_PolyLine(p_SW) {
    if (p_SW) {
        //Vendedor
        if (g_lstMarkers_Sellers.length > 0) {
            //Set Options
            g_PolySeller = new google.maps.Polyline(polyOptionsSeller);
            //Set int the map
            g_PolySeller.setMap(g_Map);

            var path = g_PolySeller.getPath();

            for (var i = 0; i < g_lstMarkers_Sellers.length; i++) {
                path.insertAt(i, g_lstMarkers_Sellers[i].position);
            }
        }
    }
    else {
        //Ruta
        if (g_lstMarkers.length > 0) {
            //Set Options
            g_PolyRout = new google.maps.Polyline(polyOptionsRoute);
            //Set int the map
            g_PolyRout.setMap(g_Map);

            var path = g_PolyRout.getPath();

            for (var i = 0; i < g_lstMarkers.length; i++) {
                path.insertAt(i, g_lstMarkers[i].position);
            }
        }
    }
}

// REMOVE ALL DIRECTION LINES AND POLYLINES
function funRemove_Lines(p_SW) {
    if (p_SW) {
        if (g_PolySeller != undefined) {
            g_PolySeller.setMap(null);
        }
    }
    else {
        if (g_PolyRout != undefined) {
            g_PolyRout.setMap(null);
        }
    }

    /*if (g_DirectionsDisplay != undefined) {
    g_DirectionsDisplay.setMap(null);

    //var path = poly.getPath();
    //var lstMarks = path.b;
    //for (var i = 0; i < g_lstMarkers.length; i++) {
    //path.removeAt(0, lstMarks[0]);
    //}
    }*/
}

// REMOVE ALL MARKERS FROM ARRAY OF MARKERS
function funClear_Markers() {
    if (g_lstMarkers) {
        for (i in g_lstMarkers) {
            g_lstMarkers[i].setMap(null);
        }

        //Reset the array
        g_lstMarkers = [];
        g_lstInfoWindows = [];
    }
}

// REMOVE ALL MARKERS FROM ARRAY OF MARKERS SELLERS
function funClear_MarkersSellers() {
    if (g_lstMarkers_Sellers) {
        for (i in g_lstMarkers_Sellers) {
            g_lstMarkers_Sellers[i].setMap(null);
        }
        //Reset the array
        g_lstMarkers_Sellers = [];
    }
}

//#endregion

//#region --- MESSAGES METHODS ---

//Displays prompt message (p_strDefaultValue, width, height are optional parameters)
function funShow_Prompt(p_strMessage, p_strTitle, p_funPrompt_CallBack, p_strDefaultValue, p_Width, p_Height) {
    /*  "p_funPrompt_CallBack" is the name of the function that gets the result of RadPrompt
    SAMPLE : function funPrompt_CallBack(arg){ alert("Prompt returned the following result: " + arg); }   */

    if (typeof p_Width == "undefined")
        Width = 360;
    else
        Width = p_Width;

    if (typeof p_Height == "undefined")
        Height = 80;
    else
        Height = p_Height;

    if (typeof p_strDefaultValue == "undefined")
        DefaultValue = "";
    else
        DefaultValue = p_strDefaultValue;

    radprompt('<div class="message_prompt">' + p_strMessage, p_funPrompt_CallBack, Width, Height, null, p_strTitle, DefaultValue); return false;
}

//Displays confirmation message (width and height are optional parameters)
function funShow_Confirm(p_strMessage, p_ObjArgs, p_Width, p_Height) {
    /*radconfirm("Are you sure you want to keep the entered text <"
    + "span style='font-size:20px;color:red'>"
    + "Harold" + " <"
    + "/span> characters long?", callBackFn, 400, 150, 'Harold', 'Mio de Mi');*/

    if (typeof p_Width == "undefined")
        Width = 350;
    else
        Width = p_Width;

    if (typeof p_Height == "undefined")
        Height = 80;
    else
        Height = p_Height;

    radconfirm('<div class="confirm_validation">' + p_strMessage + '</div>', p_ObjArgs, Width, Height, null, 'Pregunta');
}

//Displays messages (width and height are optional parameters)
function funShow_Message(p_strMessage, p_MessageType, p_Width, p_Height) {
    if (typeof p_Width == "undefined")
        Width = 340;
    else
        Width = p_Width;

    if (typeof p_Height == "undefined")
        Height = 80;
    else
        Height = p_Height;

    switch (p_MessageType) {
        case "CONFIRMATION":
            radalert('<div class="message_confirm">' + p_strMessage + '</div>', Width, Height, 'Confirmaci&oacute;n');
            break;
        case "INFORMATION":
            radalert('<div class="message_info">' + p_strMessage + '</div>', Width, Height, 'Informaci&oacute;n');
            break;
        case "ERROR":
            radalert('<div class="message_error">' + p_strMessage + '</div>', Width, Height, 'Error');
            break;
        case "VALIDATION":
            radalert('<div class="message_validation">' + p_strMessage + '</div>', Width, Height, 'Validaci&oacute;n');
            break;
        case "SECURITY":
            radalert('<div class="message_security">' + p_strMessage + '</div>', Width, Height, 'Seguridad');
            break;
        default: radalert('TE EQUIVOCASTE DE TIPO DE MENSAJE (REVISAR JAVASCRIPT)', 350, 150, 'APRENDE CHE');
    }
}

//#endregion

function ColocarPosition(p_DivID) {
    $('#' + p_DivID).dialog("option", "position", [{ my: "top center", at: "top center" }]);
    clearInterval(Automatic2);
}
//#region --- MASTER PAGE METHOD ---
var dialoguin
function showDialog(p_DivID) {
    $('#' + p_DivID).dialog("open");
    $('#' + p_DivID).on("dialogbeforeclose", function (event, ui) { activarscroll2(); });
    $('#' + p_DivID).dialog("option", "closeOnEscape", false);
    Automatic = window.setInterval(SuperEmpty, 3000);
    //Automatic2 = window.setInterval(ColocarPosition(p_DivID),2000);
    desactivarscroll2();
}

function closeDialog(p_DivID) {
    $('#' + p_DivID).dialog("close");
    activarscroll2();
}

function desactivarscroll2() {
    $('html, body').css('overflowY', 'hidden');
}
function activarscroll2() {
    $('html, body').css('overflowY', 'auto');
}
//#endregion

//#region --- SHOW MESSAGE CLIENT VALIDATION METHODS ---

var g_strPatronEmail = /^[a-zA-Z0-9\.\-_]+@{1}[a-zA-Z0-9\.\-_]+$/;
var g_strPatronName = /^[a-zA-Z0-9\-_\. ]+$/;

function proShow_Validation(strMessage, strTitle) {
    /// <sumary>Show Validation message</sumary>
    /// <param name="strMessage">Message to be shown</param>
    /// <param name="strTitle">Title of the message</param>
    /// <returns></returns>
    //var strId = new Date().getMilliseconds();
    //var strUrlBase = "";
    //strId = "message" + strId;
    //$('.Inbox .messageBox').append('<li id="' + strId + '"><div class="avaMSG_container"><span>' + strTitle + '</span><div class="avaMSG_content">' + strMessage + '</div></div></li>');
    //proInit();
    funShow_Message(strMessage, "VALIDATION");
}

function DisplayDiv(p_DivID, isVisible) {

    if (isVisible) {
        $('#' + p_DivID).show();
    } else {
        $('#' + p_DivID).hide();
    }

}
//#endregion
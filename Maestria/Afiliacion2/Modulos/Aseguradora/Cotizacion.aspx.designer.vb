'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class Cotizacion

    '''<summary>
    '''Control form.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents form As Global.System.Web.UI.HtmlControls.HtmlForm

    '''<summary>
    '''Control ScriptManager1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ScriptManager1 As Global.System.Web.UI.ScriptManager

    '''<summary>
    '''Control RadAjaxManager1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RadAjaxManager1 As Global.Telerik.Web.UI.RadAjaxManager

    '''<summary>
    '''Control RadWindowManager1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RadWindowManager1 As Global.Telerik.Web.UI.RadWindowManager

    '''<summary>
    '''Control RadAjaxLoadingPanel1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RadAjaxLoadingPanel1 As Global.Telerik.Web.UI.RadAjaxLoadingPanel

    '''<summary>
    '''Control RadScriptBlock1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RadScriptBlock1 As Global.Telerik.Web.UI.RadScriptBlock

    '''<summary>
    '''Control rbAdd.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rbAdd As Global.Telerik.Web.UI.RadButton

    '''<summary>
    '''Control rtbFilter.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rtbFilter As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control rbFilter.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rbFilter As Global.Telerik.Web.UI.RadButton

    '''<summary>
    '''Control rgvGrid.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rgvGrid As Global.Telerik.Web.UI.RadGrid

    '''<summary>
    '''Control paDetail.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents paDetail As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control rdpDesde.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rdpDesde As Global.Telerik.Web.UI.RadDatePicker

    '''<summary>
    '''Control rdpHasta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rdpHasta As Global.Telerik.Web.UI.RadDatePicker

    '''<summary>
    '''Control rcbCiudad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rcbCiudad As Global.Telerik.Web.UI.RadComboBox

    '''<summary>
    '''Control rcbCliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rcbCliente As Global.Telerik.Web.UI.RadComboBox

    '''<summary>
    '''Control rcbVehiculo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rcbVehiculo As Global.Telerik.Web.UI.RadComboBox

    '''<summary>
    '''Control rtbPotencia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rtbPotencia As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control rtbMarca.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rtbMarca As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control rtbTipoVehiculo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rtbTipoVehiculo As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control rtbModelo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rtbModelo As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control Origen.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Origen As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control rtbPrecioVehiculo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rtbPrecioVehiculo As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control rtbMontoAsegurado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rtbMontoAsegurado As Global.Telerik.Web.UI.RadNumericTextBox

    '''<summary>
    '''Control rntTiempo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rntTiempo As Global.Telerik.Web.UI.RadNumericTextBox

    '''<summary>
    '''Control rntTasa.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rntTasa As Global.Telerik.Web.UI.RadNumericTextBox

    '''<summary>
    '''Control rntCostoPrima.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rntCostoPrima As Global.Telerik.Web.UI.RadNumericTextBox

    '''<summary>
    '''Control rntDescuento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rntDescuento As Global.Telerik.Web.UI.RadNumericTextBox

    '''<summary>
    '''Control RadTextBox4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RadTextBox4 As Global.Telerik.Web.UI.RadNumericTextBox

    '''<summary>
    '''Control rbCalcular.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rbCalcular As Global.Telerik.Web.UI.RadButton

    '''<summary>
    '''Control rtbSave.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rtbSave As Global.Telerik.Web.UI.RadButton

    '''<summary>
    '''Control rtbCancel.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rtbCancel As Global.Telerik.Web.UI.RadButton
End Class

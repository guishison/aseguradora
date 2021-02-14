Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class PruebaPeticionPuntos
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function RegistrarPuntos(Rut As String, Puntos As Int32, CodigoTransaccion As String, Token As String) As Boolean

        Return True
    End Function
    <WebMethod()>
    Public Function Futuro(Nombre As String) As String

        Return Nombre + " No molestes ya es hora de salir."
    End Function
End Class
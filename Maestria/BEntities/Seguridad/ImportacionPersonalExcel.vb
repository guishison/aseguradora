Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations
Imports BEntities.Seguridad
Imports BEntities.Base
Imports BEntities.Venta

Namespace Seguridad

    <Serializable>
    Public Class ImportacionPersonalExcel
        Inherits BEntity

        Public Property Nro As Int32
        Public Property Nombre As String
        Public Property Rut As String
        Public Property Correo As String
        Public Property Telefono As String
        Public Property Cargo As String
        Public Property Departamento As String
        Public Property Supervisor As String
        Public Property Comision As String

        Public Sub New()

        End Sub

    End Class
End Namespace
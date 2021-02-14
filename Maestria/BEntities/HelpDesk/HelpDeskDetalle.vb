Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations
Imports BEntities.Seguridad

Namespace AyudaUsuario

    <Serializable>
    Public Class HelpDeskDetalle
        Inherits BEntity


#Region " Properties "

        Public Property Id As Int32
        Public Property HelpDeskId As Int32
        Public Property NombreArchivo As String
        Public Property Descripcion As String
        Public Property Url As String
        Public Property Archivo As Byte()
        Public Property PersonalId As Int32
        Public Property FechaReg As DateTime
        Public Property FechaActualizacion As DateTime
        Public Property Estado As Int32
#End Region

#Region " Additional properties "


#End Region

#Region " Contructors "

        Public Sub New()
        End Sub

#End Region

    End Class

End Namespace



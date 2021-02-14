Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations
Imports BEntities.Seguridad

Namespace Bitacora

    <Serializable>
    Public Class BitacoraError
        Inherits BEntity


#Region " Properties "

        Public Property Id As Int32
        Public Property Evento As String
        Public Property Url As String
        Public Property MensajeError As String
        Public Property Version As String
        Public Property SistemaOperativo As String
        Public Property Navegador As String
        Public Property PersonalId As Int32
        Public Property FechaReg As DateTime

#End Region

#Region " Additional properties "

#End Region

#Region " Contructors "

        Public Sub New()
        End Sub

#End Region

    End Class


End Namespace



Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations
Imports BEntities.Seguridad

Namespace Base

    <Serializable>
    Public Class PatronIdentificador
        Inherits BEntity


#Region " Properties "

        Public Property Id As Int32
        Public Property PaisIdc As Int32
        Public Property Denomninacion As String
        Public Property Nomenclatura As String
        Public Property ExpresionRegular As String
        Public Property Longitud As Int32
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
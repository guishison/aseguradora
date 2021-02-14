Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations
Imports BEntities.Seguridad
Imports BEntities.Base

Namespace AyudaUsuario

    <Serializable>
    Public Class HelpDesk
        Inherits BEntity


#Region " Properties "

        Public Property Id As Int32
        Public Property PersonalSolicitanteId As Int32
        Public Property ModuloId As Int32
        Public Property FormularioId As Int32
        Public Property ProblemaDescripcion As String
        Public Property TipoTicketIdc As Int32
        Public Property PrioridadIdc As Int32
        Public Property EstadoHelpDeskIdc As Int32
        Public Property PersonalId As Int32
        Public Property FechaReg As DateTime
        Public Property FechaActualizacion As DateTime
        Public Property Estado As Int32
#End Region

#Region " Additional properties "

        Public Property ListaDetalleHelpDesk As List(Of HelpDeskDetalle)
        Public Property Modulo() As Modulo
        Public Property Formulario() As Formulario
        Public Property Personal() As Personal
        Public Property TipoTicket() As Clasificadores
        Public Property Prioridad() As Clasificadores
        Public Property EstadoHelpDesk() As Clasificadores
#End Region

#Region " Contructors "

        Public Sub New()
            ListaDetalleHelpDesk = New List(Of HelpDeskDetalle)
        End Sub

#End Region

    End Class

    ''' <summary>
    ''' Relationship enumerator Position
    ''' </summary>
    ''' <remarks></remarks>      
    Public Enum relHelpDesk
        HelpDeskDetalle
        Modulo
        Formulario
        Personal
        TipoTicket
        Prioridad
        EstadoHelpDesk
    End Enum


End Namespace



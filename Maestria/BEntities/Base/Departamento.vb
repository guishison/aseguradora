﻿
Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations
Imports BEntities.Seguridad
Imports BEB = BEntities.Base
Imports BEntities.Base

Namespace Base

    <Serializable>
    Public Class Departamento
        Inherits BEntity

#Region "Propiedades"
        Public Property Id As Int32
        Public Property Nombre As String
        Public Property DepartamentoSuperiorId As Int32
        Public Property PersonalId As Int32
        Public Property FechaReg As DateTime
        Public Property FechaActualizacion As DateTime
        Public Property Estado As Int16
#End Region

#Region " Additional properties "
        'Public ReadOnly Property EstadoCivil As String
        '    Get
        '        Dim est As String = ""
        '        If (EstadoSala = BEntities.EstadoSala.Abierta) Then
        '            est = Abierta
        '        ElseIf (EstadoSala = BEntities.EstadoSala.Cerrada) Then
        '            est = Cerrada
        '        ElseIf (EstadoSala = BEntities.EstadoSala.ReAbierta) Then
        '            est = ReAbierta
        '        End If
        '        Return est
        '    End Get
        'End Property
        'Public Property Comuna() As Comuna


#End Region
#Region " Contructors "

        Public Sub New()
        End Sub

#End Region
    End Class
    Public Enum relDepartamento
        Personal
    End Enum
End Namespace


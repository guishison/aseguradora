
Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations

Namespace Base

    ''' -----------------------------------------------------------------------------
    ''' Project   : Code Generator
    ''' NameSpace : Base
    ''' Class     : BusinessUnit
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''    This Business Entity has all properties to work with a database table 
    '''	   MsBusinessUnit
    ''' </summary>
    ''' <remarks>	
    ''' </remarks>
    ''' <history>
    '''   [Code]  9/21/2015 5:45:21 PM Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Serializable>
    Public Class UnidadNegocio
    Inherits BEntity


#Region " Properties "

        Public Property Id As Int32

        <Required(AllowEmptyStrings:=False, ErrorMessage:="*")>
        <StringLength(150, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property Nombre As String

        <Required(ErrorMessage:="*")>
        Public Property Correlativo As Int32

        <Required(AllowEmptyStrings:=False, ErrorMessage:="*")>
        <StringLength(50, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property AliasUN As String

        <Required(ErrorMessage:="*")>
        Public Property PersonalId As Int32
        <Required(ErrorMessage:="*")>
        Public Property FechaReg As DateTime

        <Required(ErrorMessage:="*")>
        Public Property FechaActualizacion As DateTime
        Public Property Estado As Int32


#End Region

#Region " Additional properties "

        Public Property Clasificadores As Clasificadores
        Public Property UNSucursal As UNSucursal
        Public Property ColeccionUNSucursal As List(Of UNSucursal)

#End Region

#Region " Contructors "

        Public Sub New()

    End Sub

#End Region

End Class

    ''' <summary>
    ''' Relationship enumerator ClassifierType
    ''' </summary>
    ''' <remarks></remarks>      
    Public Enum relUnidadNegocio
        Clasificadores
        UNSucursal
    End Enum


End Namespace


Imports System.ComponentModel.DataAnnotations
Imports BEntities.Seguridad

Namespace Bitacora

    <Serializable>
    Public Class BitacoraGeneral
        Inherits BEntity


#Region " Properties "

        Public Property Id As Int32
        Public Property TransaccionId As Int32
        Public Property Procedimiento As String
        Public Property Accion As String
        Public Property Campos As String
        Public Property PersonalId As Int32
        Public Property IpCliente As String
        Public Property MaquinaCliente As String
        Public Property FechaReg As DateTime
        Public Property TipoTransaccionIdc As Int32

        Public ReadOnly Property Transaccion As String
            Get
                Return IIf(TipoTransaccionIdc = 1226, "Transaccion Exitosa", "Transaccion Fallida")
            End Get
        End Property

#End Region

#Region " Additional properties "

#End Region

#Region " Contructors "

        Public Sub New()
        End Sub

#End Region

    End Class


End Namespace



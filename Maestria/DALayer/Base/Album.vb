Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports System
Imports BE = BEntities
Imports BAS = BEntities.Base
Imports System.Data.Common
Imports System.Net
Imports BEntities

Namespace Base

    <Serializable>
    Public Class Album
        Inherits DALEntity

        Public Sub Save(ByRef BEObject As BEntities.Base.Album)
            Dim strQuery As String = ""
            If BEObject.StatusType = BEntities.StatusType.Insert Then
                strQuery = "crm_foto_album_insert"
            ElseIf BEObject.StatusType = BEntities.StatusType.Update Then
                strQuery = "crm_foto_album_update"
            ElseIf BEObject.StatusType = BEntities.StatusType.Delete Then
                strQuery = "crm_foto_album_update"
            End If

            'Dim DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
            'DALBitacora.OpenConnection()
            'Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
            'With beBitacora
            '    .Id = 0
            '    .Procedimiento = strQuery
            '    .Accion = If(BEObject.StatusType = BEntities.StatusType.Insert, "INSERT", If(BEObject.StatusType = BEntities.StatusType.Update, "UPDATE", "DELETE"))
            '    .PersonalId = BEObject.idpersonal
            '    .StatusType = BEntities.StatusType.Insert
            '    .FechaReg = Now
            '    .TipoTransaccionIdc = BEntities.TIPOTRANSACCIONBITACORA.EXITOSA
            'End With

            Try
                If BEObject.StatusType <> BEntities.StatusType.NoAction Then
                    MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                    If (BEObject.StatusType <> BEntities.StatusType.Insert) Then
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEObject.Id)
                    End If
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEObject.Nombre)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Descripcion", DbType.String, BEObject.Descripcion)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObject.PersonalId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalModificacionId", DbType.Int32, BEObject.PersonalModificacionId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaRegistro", DbType.DateTime, BEObject.FechaRegistro)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaModificacion", DbType.DateTime, BEObject.FechaModificacion)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEObject.Estado)

                    Dim Campos As String = ""
                    For i As Int32 = 0 To MyBase.Command.Parameters.Count - 1
                        Campos = Campos + MyBase.Command.Parameters.Item(i).ParameterName + "=" + MyBase.Command.Parameters.Item(i).Value.ToString + "-.-"
                    Next
                    Campos = Campos.Replace("@", "")
                    Dim strHostName As String = Dns.GetHostName()
                    Dim ipEntry As IPHostEntry = Dns.GetHostEntry(strHostName)
                    Dim IPcita As String = ipEntry.AddressList(1).ToString
                    'beBitacora.Campos = Campos
                    'beBitacora.IpCliente = IPcita

                    'beBitacora.MaquinaCliente = strHostName
                    If BEObject.StatusType = BEntities.StatusType.Insert Then
                        BEObject.Id = CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command, MyBase.Transaction))
                        'beBitacora.TransaccionId = BEObject.idempresa
                        'DALBitacora.Save(beBitacora)
                        'DALBitacora.Commit()
                    Else
                        MyBase.DBFactory.ExecuteNonQuery(MyBase.Command, MyBase.Transaction)
                        'beBitacora.TransaccionId = BEObject.idempresa
                        'DALBitacora.Save(beBitacora)
                        'DALBitacora.Commit()
                    End If
                End If
            Catch ex As Exception
                'beBitacora.TipoTransaccionIdc = BEntities.TIPOTRANSACCIONBITACORA.FALLIDA
                'DALBitacora.Save(beBitacora)
                'DALBitacora.Commit()
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
            Finally
                'DALBitacora.CloseConnection()
                MyBase.DisposeCommand()
            End Try
        End Sub


        Public Function ListById(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As BAS.Album
            Dim strQuery As String = "crm_foto_album_buscarporid"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, Id)
                Dim Collection As BAS.Album = MyBase.SQLConvertidorIDataReader(Of BAS.Album)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                'If Collection.Count > 0 Then
                '    'Me.CargarRelaciones(Collection, Relations)
                '    Me.CargarRelacionesTabla(Collection, Relations)
                'End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListBuscar(ByVal text As String, ByVal CantidadRegistro As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.Album)
            Dim strQuery As String = "crm_foto_album_buscarportext"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@text", DbType.String, text)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CantidadRegistros", DbType.Int32, CantidadRegistro)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@NumeroPagina", DbType.Int32, NumeroPagina)
                Dim Collection As List(Of BAS.Album) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.Album)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                'If Collection.Count > 0 Then
                '    'Me.CargarRelaciones(Collection, Relations)
                '    Me.CargarRelacionesTabla(Collection, Relations)
                'End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListByAlbum(ByVal text As String) As List(Of List(Of Object))
            'Dim strQuery As String = "crm_foto_album_buscarportext"
            Try
                Return (MyBase.ObjectList(text))
                'If Collection.Count > 0 Then
                '    'Me.CargarRelaciones(Collection, Relations)
                '    Me.CargarRelacionesTabla(Collection, Relations)
                'End If
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListBusquedaCount(text As String) As Int32
            Dim strQuery As String = "crm_foto_album_buscarportextcount"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@text", DbType.String, text)
                Return CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command))
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            End Try
        End Function
        Protected Overrides Function LoadBE(Of T As BEntity)(ByRef DR As IDataReader) As T
            Dim BEObject As New BAS.Album
            With DR

                If Not .IsDBNull(0) Then
                    BEObject.Id = .GetInt32(0)
                End If

                If Not .IsDBNull(1) Then
                    BEObject.Nombre = .GetString(1)
                End If

                If Not .IsDBNull(2) Then
                    BEObject.Descripcion = .GetString(2)
                End If

                If Not .IsDBNull(3) Then
                    BEObject.PersonalId = .GetInt32(3)
                End If

                If Not .IsDBNull(4) Then
                    BEObject.PersonalModificacionId = .GetInt32(4)
                End If

                If Not .IsDBNull(5) Then
                    BEObject.FechaRegistro = .GetDateTime(5)
                End If

                If Not .IsDBNull(6) Then
                    BEObject.FechaModificacion = .GetDateTime(6)
                End If

                If Not .IsDBNull(7) Then
                    BEObject.Estado = .GetInt16(7)
                End If

            End With
            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        Public Sub New()
            MyBase.New("Negocio")
        End Sub
        Public Sub New(ByVal StringConnection As String)
            MyBase.New(StringConnection)
        End Sub
        Friend Sub New(ByVal UseDBCon As Boolean, ByRef BD As Object)
            MyBase.New(UseDBCon, BD)
        End Sub
        Friend Sub New(ByVal UseDBCon As Boolean, ByRef BD As Object, ByVal Transaction As DbTransaction)
            MyBase.New(UseDBCon, BD, Transaction)
        End Sub
    End Class
End Namespace
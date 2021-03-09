Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BE = BEntities
Imports MEB = BEntities.Aseguradora
Imports BEB = BEntities.Base
Imports DALB = DALayer.Base
Imports System.Data.Common
Imports System.Net

Namespace Aseguradora

    <Serializable>
    Public Class Cliente
    Inherits DALEntity

#Region " Save Methods"

        ''' <summary>
        ''' 	Saves business information object of type InstanciaSala
        ''' </summary>
        ''' <param name="BEObj">Business object of type InstanciaSala </param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Guardar(ByRef BEObj As MEB.Cliente)

            Dim strQuery As String = ""
            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "crm_aseguradora_cliente_insert"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                strQuery = "crm_aseguradora_cliente_update"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                strQuery = "crm_aseguradora_cliente_update"
            End If

            Dim DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
            DALBitacora.OpenConnection()
            Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
            With beBitacora
                .Id = 0
                .Procedimiento = strQuery
                .Accion = If(BEObj.StatusType = BE.StatusType.Insert, "INSERT", If(BEObj.StatusType = BE.StatusType.Update, "UPDATE", "DELETE"))
                .PersonalId = BEObj.PersonalId
                .StatusType = BE.StatusType.Insert
                .FechaReg = Now
                .TipoTransaccionIdc = BE.TipoTransaccionBitacora.Exitosa
            End With

            Try
                If BEObj.StatusType <> BE.StatusType.NoAction Then
                    MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                    If (BEObj.StatusType <> BE.StatusType.Insert) Then
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEObj.Id)
                    End If
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEObj.Nombre)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Apellido", DbType.String, BEObj.Apellido)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@CI", DbType.String, BEObj.CI)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@ExpedidoIdc", DbType.Int32, BEObj.ExpedidoIdc)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Telefono", DbType.String, BEObj.Telefono)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaNacimiento", DbType.DateTime, BEObj.FechaNacimiento)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Direccion", DbType.String, BEObj.Direccion)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, BEObj.UnidadNegocioId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalModificacionId", DbType.Int32, BEObj.PersonalModificacionId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaRegistro", DbType.DateTime, BEObj.FechaRegistro)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaModificacion", DbType.DateTime, BEObj.FechaModificacion)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEObj.Estado)
                    Dim hola As String = ""
                    For i As Int32 = 0 To MyBase.Command.Parameters.Count - 1
                        hola = hola + MyBase.Command.Parameters.Item(i).ParameterName + "=" + MyBase.Command.Parameters.Item(i).Value.ToString + "-.-"
                    Next
                    hola = hola.Replace("@", "")
                    Dim strHostName As String = Dns.GetHostName()
                    Dim ipEntry As IPHostEntry = Dns.GetHostEntry(strHostName)
                    Dim IPcita As String = ipEntry.AddressList(1).ToString
                    beBitacora.Campos = hola
                    beBitacora.IpCliente = IPcita
                    beBitacora.MaquinaCliente = strHostName
                    If BEObj.StatusType = BE.StatusType.Insert Then
                        BEObj.Id = CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command, MyBase.Transaction))
                        beBitacora.TransaccionId = BEObj.Id
                        DALBitacora.Save(beBitacora)
                        DALBitacora.Commit()
                    Else
                        MyBase.DBFactory.ExecuteNonQuery(MyBase.Command, MyBase.Transaction)
                        beBitacora.TransaccionId = BEObj.Id
                        DALBitacora.Save(beBitacora)
                        DALBitacora.Commit()
                    End If
                End If
            Catch ex As Exception
                beBitacora.TipoTransaccionIdc = BE.TipoTransaccionBitacora.Fallida
                DALBitacora.Save(beBitacora)
                DALBitacora.Commit()
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
            Finally
                DALBitacora.CloseConnection()
                MyBase.DisposeCommand()
            End Try
        End Sub

        Public Sub Guardar(ByRef colBEObj As List(Of MEB.Cliente))

            Dim strQuery As String = ""
            For Each BEobj As MEB.Cliente In colBEObj
                If BEobj.StatusType = BE.StatusType.Insert Then
                    strQuery = "crm_aseguradora_cliente_insert"
                ElseIf BEobj.StatusType = BE.StatusType.Update Then
                    strQuery = "crm_aseguradora_cliente_update"
                ElseIf BEobj.StatusType = BE.StatusType.Delete Then
                    strQuery = "crm_aseguradora_cliente_update"
                End If

                Dim DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
                DALBitacora.OpenConnection()
                Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
                With beBitacora
                    .Id = 0
                    .Procedimiento = strQuery
                    .Accion = If(BEobj.StatusType = BE.StatusType.Insert, "INSERT", If(BEobj.StatusType = BE.StatusType.Update, "UPDATE", "DELETE"))
                    .PersonalId = BEobj.PersonalId
                    .StatusType = BE.StatusType.Insert
                    .FechaReg = Now
                    .TipoTransaccionIdc = BE.TipoTransaccionBitacora.Exitosa
                End With

                Try
                    If BEobj.StatusType <> BE.StatusType.NoAction Then
                        MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                        If (BEobj.StatusType <> BE.StatusType.Insert) Then
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEobj.Id)
                        End If
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEObj.Nombre)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Apellido", DbType.String, BEObj.Apellido)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@CI", DbType.String, BEObj.CI)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@ExpedidoIdc", DbType.Int32, BEobj.ExpedidoIdc)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Telefono", DbType.String, BEObj.Telefono)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaNacimiento", DbType.DateTime, BEObj.FechaNacimiento)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Direccion", DbType.String, BEObj.Direccion)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, BEObj.UnidadNegocioId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalModificacionId", DbType.Int32, BEObj.PersonalModificacionId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaRegistro", DbType.DateTime, BEObj.FechaRegistro)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaModificacion", DbType.DateTime, BEObj.FechaModificacion)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEObj.Estado)

                        Dim hola As String = ""
                        For i As Int32 = 0 To MyBase.Command.Parameters.Count - 1
                            hola = hola + MyBase.Command.Parameters.Item(i).ParameterName + "=" + MyBase.Command.Parameters.Item(i).Value.ToString + "-.-"
                        Next
                        hola = hola.Replace("@", "")
                        Dim strHostName As String = Dns.GetHostName()
                        Dim ipEntry As IPHostEntry = Dns.GetHostEntry(strHostName)
                        Dim IPcita As String = ipEntry.AddressList(1).ToString
                        beBitacora.Campos = hola
                        beBitacora.IpCliente = IPcita
                        beBitacora.MaquinaCliente = strHostName
                        If BEobj.StatusType = BE.StatusType.Insert Then
                            BEobj.Id = CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command, MyBase.Transaction))
                            beBitacora.TransaccionId = BEobj.Id
                            DALBitacora.Save(beBitacora)
                            DALBitacora.Commit()
                        Else
                            MyBase.DBFactory.ExecuteNonQuery(MyBase.Command, MyBase.Transaction)
                            beBitacora.TransaccionId = BEobj.Id
                            DALBitacora.Save(beBitacora)
                            DALBitacora.Commit()
                        End If
                    End If
                Catch ex As Exception
                    beBitacora.TipoTransaccionIdc = BE.TipoTransaccionBitacora.Fallida
                    DALBitacora.Save(beBitacora)
                    DALBitacora.Commit()
                    MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Finally
                    DALBitacora.CloseConnection()
                    MyBase.DisposeCommand()
                End Try
            Next
        End Sub

#End Region
#Region " Methods "


        ''' <summary>
        ''' 	Metodo sobrecargardo que permite cargar la informacion de la base de datos al
        '''		Entidad de Negocio
        ''' </summary>
        ''' <param name="DR">El Datareader que contiene la informacion del objeto</param>
        ''' <returns>Un tipo Generico que herreda de BEGenerico.BEEntidad</returns>
        ''' <remarks>
        ''' 	Para el conjunto de Hijos se deben pasar los enumeradores de los objetos relacionados
        ''' </remarks>
        Protected Overrides Function LoadBE(Of T As BE.BEntity)(ByRef DR As IDataReader) As T
            Dim BEObject As New MEB.Cliente

            With DR
            BEObject.Id = .GetInt32(0)
                BEObject.Nombre = .GetString(1)
                BEObject.Apellido = .GetString(2)
                BEObject.CI = .GetString(3)
                BEObject.ExpedidoIdc = .GetInt32(4)
                BEObject.Telefono = .GetString(5)
                BEObject.FechaNacimiento = .GetDateTime(6)
                BEObject.Direccion = .GetString(7)
                BEObject.UnidadNegocioId = .GetInt32(8)
                BEObject.PersonalId = .GetInt32(9)
                BEObject.PersonalModificacionId = .GetInt32(10)
                BEObject.FechaRegistro = .GetDateTime(11)
                BEObject.FechaModificacion = .GetDateTime(12)
                BEObject.Estado = .GetInt16(13)
        End With

        Return CType(DirectCast(BEObject, BE.BEntity), T)
    End Function

        ''' <summary>
        ''' Carga las Relations de la Collection dada
        ''' </summary>
        ''' <param name="Collection">Lista de objetos para cargar las Relations</param>
        ''' <param name="Relations">Enumerador de Relations a cargar</param>
        ''' <remarks></remarks>
        Protected Sub LoadRelations(ByRef Collection As List(Of MEB.Cliente), ByVal ParamArray Relations() As [Enum])
            Dim DALExpedido As DALB.Clasificadores
            'Dim DALVehiculo As Vehiculo
            Dim colExpedido As List(Of BEB.Clasificadores) = Nothing
            'Dim colVehiculo As List(Of MEB.Vehiculo) = Nothing
            Dim Keys As IEnumerable(Of Int32)

            For Each RelationEnum As [Enum] In Relations
                If RelationEnum.Equals(MEB.relCliente.Expedido) Then
                    DALExpedido = New DALB.Clasificadores(True, CType(MyBase.DBFactory, Object))
                    Keys = (From BEObject In Collection Select BEObject.ExpedidoIdc).Distinct
                    colExpedido = DALExpedido.ReturnChild(Keys, Relations)
                End If
                'Keys = From BEInstanciaSala In Collection Select BEInstanciaSala.Id
                'If RelationEnum.Equals(MEM.relInstanciaSala.Sala) Then
                '    DALSala = New Sala(True, CType(MyBase.DBFactory, Object))
                '    colSala = DALSala.ReturnChildInstanciaSala(Keys, Relations)
                'End If
            Next

            If Relations.GetLength(0) > 0 Then
                For Each BECliente In Collection
                    If colExpedido IsNot Nothing Then
                        BECliente.Expedido = (From BEObject In colExpedido
                                              Where BEObject.Id = BECliente.ExpedidoIdc
                                              Select BEObject).FirstOrDefault
                    End If
                Next
            End If
            DALExpedido = Nothing
        End Sub

        ''' <summary>
        ''' Load Relationship of an Object
        ''' </summary>
        ''' <param name="BECliente">Given Object</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <remarks></remarks>
        Protected Sub LoadRelations(ByRef BECliente As MEB.Cliente, ByVal ParamArray Relations() As [Enum])
            Dim DALExpedicion As DALB.Clasificadores
            'Dim DALPersonal As Personal

            For Each RelationEnum As [Enum] In Relations
                If RelationEnum.Equals(MEB.relCliente.Expedido) Then
                    DALExpedicion = New DALB.Clasificadores(True, CType(MyBase.DBFactory, Object))
                    BECliente.Expedido = DALExpedicion.ReturnMaster(BECliente.ExpedidoIdc, Relations)
                End If
                'Dim Keys() As Int32 = {BEInstanciaSala.Id}
                'If RelationEnum.Equals(MEM.relInstanciaSala.Sala) Then
                '    DALSala = New Sala(True, CType(MyBase.DBFactory, Object))
                '    BEInstanciaSala.ColeccionSala = DALSala.ReturnChildInstanciaSala(Keys, Relations)
                'End If
            Next
            DALExpedicion = Nothing
            'DALPersonal = Nothing
        End Sub
        Friend Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of MEB.Cliente)
            Dim strQuery2 As String = "SELECT * from crm_aseguradora_cliente where Id IN " & MyBase.KeysArray(Keys)
            Try
                MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery2)
                Dim Coleccion As List(Of MEB.Cliente) = MyBase.SQLConvertidorIDataReaderListas(Of MEB.Cliente)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Coleccion.Count > 0 Then
                    Me.LoadRelations(Coleccion, Relaciones)
                End If
                Return Coleccion
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Return an object Collection of InstanciaSala
        ''' </summary>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>A Collection of type InstanciaSala</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function List(ByVal UnidadNegocioId As Int32, ByVal CantidadRegistros As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Cliente)
            Dim strQuery As String = "crm_aseguradora_cliente_listado"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@Desde", DbType.DateTime, Desde)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@Hasta", DbType.DateTime, Hasta)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, UnidadNegocioId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CantidadRegistros", DbType.Int32, CantidadRegistros)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@NumeroPagina", DbType.Int32, NumeroPagina)
                Dim Collection As List(Of MEB.Cliente) = MyBase.SQLConvertidorIDataReaderListas(Of MEB.Cliente)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    Me.LoadRelations(Collection, Relations)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListCount(ByVal UnidadNegocioId As Int32) As Int32
            Dim strQuery As String = "crm_aseguradora_cliente_listadocount"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, UnidadNegocioId)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@Login", DbType.String, If(Login = "", "%%", String.Concat("%", Login, "%")))
                Return CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command))

                'Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListadoUN(ByVal UnidadNegocioId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Cliente)
            Dim strQuery As String = "crm_aseguradora_cliente_listadoUN"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, UnidadNegocioId)
                Dim Collection As List(Of MEB.Cliente) = MyBase.SQLConvertidorIDataReaderListas(Of MEB.Cliente)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    Me.LoadRelations(Collection, Relations)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function List(ByVal Filter As String, ByVal Order As String, ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Cliente)
            Dim strQuery As String = "crm_base_pais_lista"
            Dim Collection As List(Of MEB.Cliente) = MyBase.SQLList(Of MEB.Cliente)(strQuery)

            If Collection.Count > 0 Then
                Me.LoadRelations(Collection, Relations)
            End If
            Return Collection
        End Function

        ''' <summary>
        ''' 	Return an object Collection of InstanciaSala
        ''' </summary>
        ''' <param name="IdPadre">Object Identifier</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>A Collection of type InstanciaSala</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function List(ByVal IdPadre As Long, ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Cliente)
            Dim strQuery As String = "Select * FROM membership_msInstanciaSala WHERE IdPadre = " & IdPadre.ToString
            Dim Collection As List(Of MEB.Cliente) = MyBase.SQLList(Of MEB.Cliente)(strQuery)

            If Collection.Count > 0 Then
                Me.LoadRelations(Collection, Relations)
            End If
            Return Collection
        End Function
        Public Function List(ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Cliente)
            Dim strQuery As String = "crm_base_pais_listado"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of MEB.Cliente) = MyBase.SQLConvertidorIDataReaderListas(Of MEB.Cliente)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function


#End Region

#Region " Search Methods "
        Friend Function ReturnMaster(ByVal Id As Int32, ByVal ParamArray Relaciones() As [Enum]) As MEB.Cliente
            Return Search(Id, Relaciones)
        End Function

        ''' <summary>
        ''' 	Search an object of type InstanciaSala    	''' </summary>
        ''' <param name="Id">Object identifier InstanciaSala</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>An object of type InstanciaSala</returns>
        ''' <remarks>
        ''' </remarks> 
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As MEB.Cliente
            Dim strQuery As String = "crm_aseguradora_cliente_search"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, Id)
                Dim Collection As MEB.Cliente = MyBase.SQLConvertidorIDataReader(Of MEB.Cliente)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection IsNot Nothing Then
                    Me.LoadRelations(Collection, Relations)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function


#End Region

#Region " Constructors"

        ''' <summary>
        ''' El constructor por defecto que crear una instancia de la base de datos
        ''' utilizando el Factory Pattern
        ''' </summary>
        ''' <remarks>
        '''  La instancia de la Base de datos se pasa al constructor
        '''	</remarks>
        Public Sub New()
        MyBase.New("Negocio")
    End Sub

    ''' <summary>
    ''' El constructor por defecto que crear una instancia de la base de datos
    ''' utilizando el Factory Pattern
    ''' </summary>
    ''' <remarks>
    '''  La instancia de la Base de datos se pasa al constructor
    '''	</remarks>
    Public Sub New(ByVal StringConnection As String)
        MyBase.New(StringConnection)
    End Sub

    ''' <summary>
    ''' Constructor que crea la instancia del la base de datos utilizando
    ''' el Factory pattern
    ''' </summary>
    ''' <param name="UseDBCon">True para utilizar la Connection del padre</param>
    ''' <param name="BD">la instancia de la base de datos del padre</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal UseDBCon As Boolean, ByRef BD As Object)
        MyBase.New(UseDBCon, BD)
    End Sub

    ''' <summary>
    ''' Constructor que crea la instancia de base mas la Transaction
    ''' </summary>
    ''' <param name="UseDBCon"></param>
    ''' <param name="BD"></param>
    ''' <param name="Transaction"></param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal UseDBCon As Boolean, ByRef BD As Object, ByVal Transaction As DbTransaction)
        MyBase.New(UseDBCon, BD, Transaction)
    End Sub

#End Region

End Class

End Namespace


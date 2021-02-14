Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BE = BEntities
Imports MEB = BEntities.Base
Imports DAL = DALayer.Base
Imports DALayer.Base
Imports System.Data.Common
Imports System.Net

Namespace Base

    <Serializable>
    Partial Public Class Comuna
        Inherits DALEntity

#Region " Save Methods"

        ''' <summary>
        ''' 	Saves business information object of type InstanciaSala
        ''' </summary>
        ''' <param name="BEObj">Business object of type InstanciaSala </param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Guardar(ByRef BEObj As MEB.Comuna)

            Dim strQuery As String = ""
            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "crm_base_comuna_insert"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                strQuery = "crm_base_comuna_update"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                strQuery = "crm_base_comuna_update"
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
                    'MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEObj.Id)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEObj.Nombre)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@ProvinciaId", DbType.Int32, BEObj.ProvinciaId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEObj.FechaReg)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaActualizacion", DbType.DateTime, BEObj.FechaActualizacion)
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

        Public Sub Guardar(ByRef colBEObj As List(Of MEB.Comuna))

            Dim strQuery As String = ""
            For Each BEobj As MEB.Comuna In colBEObj
                If BEobj.StatusType = BE.StatusType.Insert Then
                    strQuery = "crm_base_comuna_insert"
                ElseIf BEobj.StatusType = BE.StatusType.Update Then
                    strQuery = "crm_base_comuna_update"
                ElseIf BEobj.StatusType = BE.StatusType.Delete Then
                    strQuery = "crm_base_comuna_update"
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

                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEobj.Nombre)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@ProvinciaId", DbType.Int32, BEobj.ProvinciaId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEobj.PersonalId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEobj.FechaReg)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaActualizacion", DbType.DateTime, BEobj.FechaActualizacion)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEobj.Estado)

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
        ''' 	For use on data access layer at assembly level, return an  InstanciaSala type object
        ''' </summary>
        ''' <param name="Id">Object Identifier InstanciaSala</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>An object of type InstanciaSala</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnMaster(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As MEB.Comuna
            Return Search(Id, Relations)
        End Function

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
            Dim BEObject As New MEB.Comuna

            With DR
                BEObject.Id = .GetInt32(0)
                BEObject.Nombre = .GetString(1)
                BEObject.Ciudad = .GetString(2)
                BEObject.ProvinciaId = .GetInt32(3)
                BEObject.PersonalId = .GetInt32(4)
                BEObject.FechaReg = .GetDateTime(5)
                BEObject.FechaActualizacion = .GetDateTime(6)
                BEObject.Estado = .GetInt16(7)
            End With

            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        Friend Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of MEB.Comuna)
            'Dim llaves As String = MyBase.KeysArray(Keys).ToString.Replace("(", "(',") 
            'llaves = llaves.Replace(")", ",')")
            'Dim strQuery As String = "crm_base_clasificadores_buscarcomunas"
            Dim strQuery2 As String = "SELECT * from crm_base_comuna where Id IN " & MyBase.KeysArray(Keys)
            Try
                MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery2)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@llaves", DbType.String, llaves)
                Dim Coleccion As List(Of MEB.Comuna) = MyBase.SQLConvertidorIDataReaderListas(Of MEB.Comuna)(MyBase.DBFactory.ExecuteReader(MyBase.Command))

                Return Coleccion
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        ''' <summary>
        ''' Carga las Relations de la Collection dada
        ''' </summary>
        ''' <param name="Collection">Lista de objetos para cargar las Relations</param>
        ''' <param name="Relations">Enumerador de Relations a cargar</param>
        ''' <remarks></remarks>
        Protected Sub LoadRelations(ByRef Collection As List(Of MEB.Comuna), ByVal ParamArray Relations() As [Enum])
            'Dim DALPais As Pais
            'Dim colInstanciaSala As List(Of MEB.Pais) = Nothing
            'Dim Keys As IEnumerable(Of Int32)

            'For Each RelationEnum As [Enum] In Relations
            '    If RelationEnum.Equals(MEM.relMovimientoSala.Sala) Then
            '        DALInstanciaSala = New InstanciaSala(True, CType(MyBase.DBFactory, Object))
            '        Keys = (From BEObject In Collection Select BEObject.InstanciaSalaId).Distinct
            '        colInstanciaSala = DALInstanciaSala.ReturnChild(Keys, Relations)
            '    End If
            '    'Keys = From BEInstanciaSala In Collection Select BEInstanciaSala.Id
            '    'If RelationEnum.Equals(MEM.relInstanciaSala.Sala) Then
            '    '    DALSala = New Sala(True, CType(MyBase.DBFactory, Object))
            '    '    colSala = DALSala.ReturnChildInstanciaSala(Keys, Relations)
            '    'End If
            'Next

            'If Relations.GetLength(0) > 0 Then
            '    For Each BEMovimientoSala In Collection
            '        If colInstanciaSala IsNot Nothing Then
            '            BEMovimientoSala.InstanciaSala = (From BEObject In colInstanciaSala
            '                                              Where BEObject.Id = BEMovimientoSala.InstanciaSalaId
            '                                              Select BEObject).FirstOrDefault
            '        End If
            '    Next
            'End If
            'DALInstanciaSala = Nothing
        End Sub

        ''' <summary>
        ''' Load Relationship of an Object
        ''' </summary>
        ''' <param name="BEInstanciaSala">Given Object</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <remarks></remarks>
        Protected Sub LoadRelations(ByRef BEInstanciaSala As MEB.Comuna, ByVal ParamArray Relations() As [Enum])
            Dim DALProvincia As Provincia

            For Each RelationEnum As [Enum] In Relations
                If RelationEnum.Equals(MEB.relComuna.Provincia) Then
                    DALProvincia = New Provincia(True, CType(MyBase.DBFactory, Object))
                    BEInstanciaSala.Provincia = DALProvincia.ReturnMaster(BEInstanciaSala.ProvinciaId, Relations)
                End If
                'Dim Keys() As Int32 = {BEInstanciaSala.Id}
                'If RelationEnum.Equals(MEM.relInstanciaSala.Sala) Then
                '    DALSala = New Sala(True, CType(MyBase.DBFactory, Object))
                '    BEInstanciaSala.ColeccionSala = DALSala.ReturnChildInstanciaSala(Keys, Relations)
                'End If
            Next
            DALProvincia = Nothing
        End Sub

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Return an object Collection of InstanciaSala
        ''' </summary>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>A Collection of type InstanciaSala</returns>
        ''' <remarks>
        ''' </remarks>


        Public Function List(ByVal Filter As String, ByVal Order As String, ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Comuna)
            Dim strQuery As String = "crm_base_comuna_lista"
            Dim Collection As List(Of MEB.Comuna) = MyBase.SQLList(Of MEB.Comuna)(strQuery)

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
        Public Function ListaCompleta(ByVal UnidadNegocioId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Comuna)
            Dim strQuery As String = "Select * FROM crm_base_comuna "
            Dim Collection As List(Of MEB.Comuna) = MyBase.SQLList(Of MEB.Comuna)(strQuery)

            If Collection.Count > 0 Then
                Me.LoadRelations(Collection, Relations)
            End If
            Return Collection
        End Function
        Public Function List(ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Comuna)
            Dim strQuery As String = "crm_base_comuna_listado"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of MEB.Comuna) = MyBase.SQLConvertidorIDataReaderListas(Of MEB.Comuna)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListPorIdProvincia(ByVal IdProvincia As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Comuna)
            Dim strQuery As String = "crm_captacion_contacto_posiblecliente"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@IdProvincia", DbType.Int32, IdProvincia)
                Dim Collection As List(Of MEB.Comuna) = MyBase.SQLConvertidorIDataReaderListas(Of MEB.Comuna)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    'Me.CargarRelaciones(Collection, Relations)
                    'Me.CargarRelaciones(Collection, Relations)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListPorIdRegion(ByVal IdRegion As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Comuna)
            Dim strQuery As String = "crm_base_comuna_listacomunadeprovincia"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@IdRegion", DbType.Int32, IdRegion)
                Dim Collection As List(Of MEB.Comuna) = MyBase.SQLConvertidorIDataReaderListas(Of MEB.Comuna)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    'Me.CargarRelaciones(Collection, Relations)
                    'Me.CargarRelaciones(Collection, Relations)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            End Try
        End Function

#End Region

#Region " Search Methods "

        ''' <summary>
        ''' 	Search an object of type InstanciaSala    	''' </summary>
        ''' <param name="Id">Object identifier InstanciaSala</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>An object of type InstanciaSala</returns>
        ''' <remarks>
        ''' </remarks> 
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As MEB.Comuna
            Dim strQuery As String = "crm_base_comuna_buscarporid"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, Id)
                Dim Collection As MEB.Comuna = MyBase.SQLConvertidorIDataReader(Of MEB.Comuna)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
        Public Function SearchIdProvincia(ByVal IdProvinvia As Int32, ByVal ParamArray Relations() As [Enum]) As MEB.Comuna
            Dim strQuery As String = "crm_base_comuna_buscarporidprovincia"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@IdProvinvia", DbType.Int32, IdProvinvia)
                Dim Collection As MEB.Comuna = MyBase.SQLConvertidorIDataReader(Of MEB.Comuna)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function SearchActiva(ByVal Id As Int32, ByVal Estado As Int16, ByVal ParamArray Relations() As [Enum]) As MEB.Comuna
            Dim strQuery As String = "crm_base_comuna_buscarporidActiva"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, Id)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, Estado)
                Dim Collection As MEB.Comuna = MyBase.SQLConvertidorIDataReader(Of MEB.Comuna)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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


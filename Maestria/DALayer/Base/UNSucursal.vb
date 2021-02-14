Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports System
Imports BE = BEntities
Imports BAS = BEntities.Base
Imports CAL = BEntities.CallCenter
Imports CHA = BEntities.Charges
Imports MEM = BEntities.Membership
Imports SAL = BEntities.Sales
Imports SEC = BEntities.Security
Imports DALayer.CallCenter
Imports DALayer.Charges
Imports DALayer.Membership
Imports DALayer.Sales
Imports DALayer.Security
Imports System.Data.Common
Imports System.Collections.Generic
Imports System.Net


Namespace Base
    ''' -----------------------------------------------------------------------------
    ''' Project   : Code Generator
    ''' NameSpace : Base
    ''' Class     : ClassifierType
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''    This data access component saves business object information of type BusinessPlan
    '''    for the service Base.
    ''' </summary>
    ''' <remarks>
    '''    Data access layer for the service Base
    ''' </remarks>
    ''' <history>
    '''   [Code]   9/21/2015 6:47:10 PM Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Serializable> Partial Public Class UNSucursal
        Inherits DALEntity

#Region " Save Methods"

        ''' <summary>
        ''' 	Saves business information object of type BusinessUnit
        ''' </summary>
        ''' <param name="BEObj">Business object of type BusinessUnit </param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Save(ByRef BEObj As BAS.UNSucursal)

            Dim strQuery As String = ""

            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "INSERT INTO base_msbusinessunit " &
                        "VALUES(@Id,@Name,@Alias,@Correlative,@ClassifierId,@UserId,@Registry); " &
                        "SELECT @@IDENTITY;"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                strQuery = "UPDATE base_msbusinessunit SET " &
                        "Name = @Name, Alias = @Alias, Correlative = @Correlative, ClassifierId = @ClassifierId, UserId = @UserId, Registry = @Registry " &
                        "WHERE Id = @Id"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                strQuery = "UPDATE base_msbusinessunit SET UserId = @UserId WHERE Id = @Id;DELETE FROM base_msbusinessunit WHERE Id = @Id"
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

                    MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEObj.Id)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, BEObj.UnidadNegocioId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEObj.Nombre)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Correlativo", DbType.Int32, BEObj.Correlativo)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@AliasUN", DbType.String, BEObj.AliasUN)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEObj.FechaReg)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaActualizacion", DbType.DateTime, BEObj.FechaActualizacion)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int32, BEObj.Estado)

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

        ''' <summary>
    	''' 	Saves a collection business information object of type  BusinessUnit
    	''' </summary>
    	''' <param name="colBEObj">Business object of type BusinessUnit para Save</param>
    	''' <remarks>
    	''' </remarks>
        Public Sub Save(ByRef colBEObj As List(Of BAS.UNSucursal), ByVal IdMaster As Int32)

            Dim strQuery As String = ""
            For Each BEobj As BAS.UNSucursal In colBEObj

                If BEobj.StatusType = BE.StatusType.Insert Then
                    strQuery = "INSERT INTO base_msbusinessunit " &
                        "VALUES(@Id,@Name,@Alias,@Correlative,@ClassifierId,@UserId,@Registry); " &
                        "SELECT @@IDENTITY;"
                ElseIf BEobj.StatusType = BE.StatusType.Update Then
                    strQuery = "UPDATE base_msbusinessunit SET " &
                        "Name = @Name, Alias = @Alias, Correlative = @Correlative, ClassifierId = @ClassifierId, UserId = @UserId, Registry = @Registry " &
                        "WHERE Id = @Id"
                ElseIf BEobj.StatusType = BE.StatusType.Delete Then
                    strQuery = "UPDATE base_msbusinessunit SET UserId = @UserId WHERE Id = @Id;DELETE FROM base_msbusinessunit WHERE Id = @Id"
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

                        MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEobj.Id)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, BEobj.UnidadNegocioId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEobj.Nombre)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Correlativo", DbType.Int32, BEobj.Correlativo)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@AliasUN", DbType.String, BEobj.AliasUN)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEobj.PersonalId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEobj.FechaReg)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaActualizacion", DbType.DateTime, BEobj.FechaActualizacion)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int32, BEobj.Estado)

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
        ''' 	For use on data access layer at assembly level, return an  BusinessUnit type object
        ''' </summary>
        ''' <param name="Id">Object Identifier BusinessUnit</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>An object of type BusinessUnit</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnMaster(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As BAS.UNSucursal
            Return Search(Id, Relations)
        End Function

        ''' <summary>
        ''' 	Devuelve un objeto ClassifierType de tipo uno a uno con otro objeto
        ''' </summary>
        ''' <param name="Keys">Los identificadores de los objetos relacionados a BusinessUnit</param>
        ''' <param name="Relaciones">Enumerador de Relations a retorar</param>
        ''' <returns>Un Objeto de tipo BusinessUnit</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.UNSucursal)
            'Dim llaves As String = MyBase.KeysArray(Keys).ToString.Replace("(", "(',")
            'llaves = llaves.Replace(")", ",')")
            'Dim strQuery As String = "crm_base_unsucursal_buscarunsucursales"
            Dim strQuery2 As String = "SELECT * from crm_base_unsucursal where Id IN " & MyBase.KeysArray(Keys)
            Try
                MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery2)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@llaves", DbType.String, llaves)
                Dim Coleccion As List(Of BAS.UNSucursal) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.UNSucursal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Coleccion.Count > 0 Then
                    Me.CargarRelaciones(Coleccion, Relaciones)
                End If
                Return Coleccion
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
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
            Dim BEObject As New BAS.UNSucursal
            With DR
                BEObject.Id = .GetInt32(0)
                BEObject.UnidadNegocioId = .GetInt32(1)
                BEObject.Nombre = .GetString(2)
                BEObject.Correlativo = .GetInt32(3)
                BEObject.AliasUN = .GetString(4)
                BEObject.PersonalId = .GetInt32(5)
                BEObject.FechaReg = .GetDateTime(6)
                BEObject.FechaActualizacion = .GetDateTime(7)
                BEObject.Estado = .GetInt16(8)
            End With
            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        ''' <summary>
        ''' Carga las Relations de la Collection dada
        ''' </summary>
        ''' <param name="Collection">Lista de objetos para cargar las Relations</param>
        ''' <param name="Relations">Enumerador de Relations a cargar</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef Collection As List(Of BAS.UNSucursal), ByVal ParamArray Relations() As [Enum])

            'Dim DALClassifiers As Clasificadores
            'Dim colClassifiers As List(Of BAS.Classifiers) = Nothing
            'Dim Keys As IEnumerable(Of Long)

            'For Each RelationEnum As [Enum] In Relations
            '    If RelationEnum.Equals(BAS.relClassifierType.Classifiers) Then
            '        DALClassifiers = New Classifiers(True, CType(MyBase.DBFactory, Object))
            '        Keys = (From BEClassifierType In Collection Select BEClassifierType.ClassfierIdc)
            '        colClassifiers = DALClassifiers.ReturnChild(Keys, Relations)
            '    End If
            'Next

            'If Relations.GetLength(0) > 0 Then
            '    For Each BEBusinessUnit In Collection
            '        If colClassifiers IsNot Nothing Then
            '            BEBusinessUnit.Classifier = (From BEObject In colClassifiers
            '                                         Where BEObject.Id = BEBusinessUnit.ClassfierIdc
            '                                         Select BEObject).FirstOrDefault
            '        End If
            '    Next
            'End If

            'DALClassifiers = Nothing

        End Sub

        ''' <summary>
        ''' Load Relationship of an Object
        ''' </summary>
        ''' <param name="BEBusinessUnit">Given Object</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <remarks></remarks>
        Protected Sub LoadRelations(ByRef BEBusinessUnit As BAS.UNSucursal, ByVal ParamArray Relations() As [Enum])
            'Dim DALClassifiers As Classifiers
            'For Each RelationEnum As [Enum] In Relations
            '    If RelationEnum.Equals(BAS.relClassifierType.Classifiers) Then
            '        DALClassifiers = New Classifiers(True, CType(MyBase.DBFactory, Object))
            '        BEBusinessUnit.Classifier = DALClassifiers.ReturnMaster(BEBusinessUnit.ClassfierIdc, Relations)
            '    End If
            'Next
            'DALClassifiers = Nothing
        End Sub

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Return an object Collection of BusinessUnit
        ''' </summary>
        ''' <param name="Order">Object order property column </param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>A Collection of type BusinessUnit</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function List(ByVal Order As String, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.UNSucursal)
            Dim strQuery As String = "SELECT * FROM base_msbusinessunit ORDER By " & Order
            Dim Collection As List(Of BAS.UNSucursal) = MyBase.SQLList(Of BAS.UNSucursal)(strQuery)
            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relations)
            End If
            Return Collection
        End Function

        Public Function List(ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.UNSucursal)
            Dim strQuery As String = "crm_base_unsucursal_lista"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of BAS.UNSucursal) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.UNSucursal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        ''' <summary>
        ''' 	Return an object Collection of BusinessUnit
        ''' </summary>
        ''' <param name="IdPadre">Object Identifier</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>A Collection of type BusinessUnit</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function List(ByVal IdPadre As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.UNSucursal)
            Dim strQuery As String = "SELECT * FROM base_msbusinessunit WHERE IdPadre = " & IdPadre.ToString
            Dim Collection As List(Of BAS.UNSucursal) = MyBase.SQLList(Of BAS.UNSucursal)(strQuery)
            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relations)
            End If
            Return Collection
        End Function

#End Region

#Region " Search Methods "

        ''' <summary>
        ''' 	Search an object of type BusinessUnit    	''' </summary>
        ''' <param name="Id">Object identifier BusinessUnit</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>An object of type BusinessUnit</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As BAS.UNSucursal
            Dim strQuery As String = "SELECT * FROM crm_base_unsucursal WHERE Id = " & Id
            Dim BEBusinessUnit As BAS.UNSucursal = MyBase.SQLSearch(Of BAS.UNSucursal)(strQuery)
            If BEBusinessUnit IsNot Nothing Then
                Me.LoadRelations(BEBusinessUnit, Relations)
            End If
            Return BEBusinessUnit
        End Function

        ''' <summary>
        ''' 	Search an object of type BusinessUnit    	''' </summary>
        ''' <param name="PlaceId">Object identifier BusinessUnit</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>An object of type BusinessUnit</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function SearchByPlace(ByVal PlaceId As Int32, ByVal ParamArray Relations() As [Enum]) As BAS.UNSucursal
            Dim strQuery As String = "SELECT * FROM base_msbusinessunit WHERE ClassifierId = " & PlaceId.ToString
            Dim BEBusinessUnit As BAS.UNSucursal = MyBase.SQLSearch(Of BAS.UNSucursal)(strQuery)
            If BEBusinessUnit IsNot Nothing Then
                Me.LoadRelations(BEBusinessUnit, Relations)
            End If
            Return BEBusinessUnit
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
        Public Sub New(ByVal UseDBCon As Boolean, ByRef BD As Object, ByVal Transaction As DbTransaction)
            MyBase.New(UseDBCon, BD, Transaction)
        End Sub

#End Region

    End Class

End Namespace

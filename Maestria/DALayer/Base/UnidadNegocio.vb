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
Imports System.Coleccions.Generic

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
    <Serializable> Partial Public Class UnidadNegocio
        Inherits DALEntity

#Region " Save Methods"

        ''' <summary>
        ''' 	Saves business information object of type BusinessUnit
        ''' </summary>
        ''' <param name="BEObj">Business object of type BusinessUnit </param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Save(ByRef BEObj As BAS.UnidadNegocio)

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

            Try

                If BEObj.StatusType <> BE.StatusType.NoAction Then

                    MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEObj.Id)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEObj.Nombre)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Correlativo", DbType.Int32, BEObj.Correlativo)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@AliasUN", DbType.String, BEObj.AliasUN)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEObj.FechaReg)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaActualizacion", DbType.DateTime, BEObj.FechaActualizacion)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int32, BEObj.Estado)

                    If BEObj.StatusType = BE.StatusType.Insert Then
                        BEObj.Id = CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command, MyBase.Transaction))
                    Else
                        MyBase.DBFactory.ExecuteNonQuery(MyBase.Command, MyBase.Transaction)
                    End If

                End If

            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
            Finally
                MyBase.DisposeCommand()
            End Try
        End Sub

        ''' <summary>
    	''' 	Saves a Coleccion business information object of type  BusinessUnit
    	''' </summary>
    	''' <param name="colBEObj">Business object of type BusinessUnit para Save</param>
    	''' <remarks>
    	''' </remarks>
        Public Sub Save(ByRef colBEObj As List(Of BAS.UnidadNegocio), ByVal IdMaster As Int32)

            Dim strQuery As String = ""

            Try

                For Each BEobj As BAS.UnidadNegocio In colBEObj

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

                    If BEobj.StatusType <> BE.StatusType.NoAction Then

                        MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEobj.Id)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEobj.Nombre)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Correlativo", DbType.Int32, BEobj.Correlativo)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@AliasUN", DbType.String, BEobj.AliasUN)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEobj.PersonalId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEobj.FechaReg)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaActualizacion", DbType.DateTime, BEobj.FechaActualizacion)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int32, BEobj.Estado)

                        If BEobj.StatusType = BE.StatusType.Insert Then
                            BEobj.Id = CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command, MyBase.Transaction))
                        Else
                            MyBase.DBFactory.ExecuteNonQuery(MyBase.Command, MyBase.Transaction)
                        End If

                    End If
                    BEobj.StatusType = BE.StatusType.NoAction
                Next
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
            Finally
                MyBase.DisposeCommand()
            End Try

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
        Friend Function ReturnMaster(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As BAS.UnidadNegocio
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
        Friend Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.UnidadNegocio)
            'Dim llaves As String = MyBase.KeysArray(Keys).ToString.Replace("(", "(',")
            'llaves = llaves.Replace(")", ",')")
            'Dim strQuery As String = "crm_base_unidadnegocio_buscarunidadesnegocio"
            Dim strQuery2 As String = "SELECT * from crm_base_unidadnegocio where Id IN " & MyBase.KeysArray(Keys)
            Try
                MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery2)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@llaves", DbType.String, llaves)
                Dim Coleccion As List(Of BAS.UnidadNegocio) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.UnidadNegocio)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
            Dim BEObject As New BAS.UnidadNegocio
            With DR
                BEObject.Id = .GetInt32(0)
                BEObject.Nombre = .GetString(1)
                BEObject.Correlativo = .GetInt32(2)
                BEObject.AliasUN = .GetString(3)
                BEObject.PersonalId = .GetInt32(4)
                BEObject.FechaReg = .GetDateTime(5)
                BEObject.FechaActualizacion = .GetDateTime(6)
                BEObject.Estado = .GetInt32(7)
            End With
            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        ''' <summary>
        ''' Carga las Relations de la Coleccion dada
        ''' </summary>
        ''' <param name="Coleccion">Lista de objetos para cargar las Relations</param>
        ''' <param name="Relations">Enumerador de Relations a cargar</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef Coleccion As List(Of BAS.UnidadNegocio), ByVal ParamArray Relations() As [Enum])

            'Dim DALClasificadores As Clasificadores
            'Dim colClasificadores As List(Of BAS.Clasificadores) = Nothing
            Dim DALUNSucursal As UNSucursal
            Dim colUNSucursal As List(Of BAS.UNSucursal) = Nothing
            Dim Keys As IEnumerable(Of Int32)

            'For Each RelationEnum As [Enum] In Relations
            '    If RelationEnum.Equals(BAS.relClasificadoresTipo.Clasificadores) Then
            '        DALClasificadores = New Clasificadores(True, CType(MyBase.DBFactory, Object))
            '        Keys = (From BEClassifierType In Coleccion Select BEClassifierType.ClassfierIdc)
            '        colClasificadores = DALClasificadores.ReturnChild(Keys, Relations)
            '    End If
            'Next

            'If Relations.GetLength(0) > 0 Then
            '    For Each BEBusinessUnit In Coleccion
            '        If colClasificadores IsNot Nothing Then
            '            BEBusinessUnit.Clasificadores = (From BEObject In colClasificadores
            '                                             Where BEObject.Id = BEBusinessUnit.ClassfierIdc
            '                                             Select BEObject).FirstOrDefault
            '        End If
            '    Next
            'End If

            For Each RelationEnum As [Enum] In Relations
                If RelationEnum.Equals(BAS.relUnidadNegocio.UNSucursal) Then
                    DALUNSucursal = New UNSucursal(True, CType(MyBase.DBFactory, Object))
                    Keys = (From BEUnidadNegocio In Coleccion Select BEUnidadNegocio.Id)
                    colUNSucursal = DALUNSucursal.ReturnChild(Keys, Relations)
                End If
            Next

            If Relations.GetLength(0) > 0 Then
                For Each BEUnidadNegocio In Coleccion
                    If colUNSucursal IsNot Nothing Then
                        BEUnidadNegocio.ColeccionUNSucursal = (From BEObject In colUNSucursal
                                                               Where BEObject.UnidadNegocioId = BEUnidadNegocio.Id
                                                               Select BEObject).ToList
                    End If
                Next
            End If

            DALUNSucursal = Nothing

        End Sub

        ''' <summary>
        ''' Load Relationship of an Object
        ''' </summary>
        ''' <param name="BEUnidadNegocio">Given Object</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <remarks></remarks>
        Protected Sub LoadRelations(ByRef BEUnidadNegocio As BAS.UnidadNegocio, ByVal ParamArray Relations() As [Enum])
            'Dim DALClassifiers As Classifiers
            'For Each RelationEnum As [Enum] In Relations
            '    If RelationEnum.Equals(BAS.relClassifierType.Classifiers) Then
            '        DALClassifiers = New Classifiers(True, CType(MyBase.DBFactory, Object))
            '        BEBusinessUnit.Classifier = DALClassifiers.ReturnMaster(BEBusinessUnit.ClassfierIdc, Relations)
            '    End If
            'Next
            'DALClassifiers = Nothing
            Dim DALUNSucursal As UNSucursal
            For Each RelationEnum As [Enum] In Relations
                If RelationEnum.Equals(BAS.relUnidadNegocio.UNSucursal) Then
                    DALUNSucursal = New UNSucursal(True, CType(MyBase.DBFactory, Object))
                    BEUnidadNegocio.UNSucursal = DALUNSucursal.ReturnMaster(BEUnidadNegocio.Id, Relations)
                End If
            Next
            DALUNSucursal = Nothing
        End Sub

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Return an object Coleccion of BusinessUnit
        ''' </summary>
        ''' <param name="Order">Object order property column </param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>A Coleccion of type BusinessUnit</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function List(ByVal Order As String, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.UnidadNegocio)
            Dim strQuery As String = "SELECT * FROM base_msbusinessunit ORDER By " & Order
            Dim Coleccion As List(Of BAS.UnidadNegocio) = MyBase.SQLList(Of BAS.UnidadNegocio)(strQuery)
            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relations)
            End If
            Return Coleccion
        End Function

        Public Function List(ByVal ParamArray Relations() As [Enum]) As List(Of BAS.UnidadNegocio)
            Dim strQuery As String = "crm_base_unidadnegocio_list_all"
            Dim Coleccion As List(Of BAS.UnidadNegocio) = MyBase.SQLList(Of BAS.UnidadNegocio)(strQuery)
            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relations)
            End If
            Return Coleccion
        End Function

        ''' <summary>
        ''' 	Return an object Coleccion of BusinessUnit
        ''' </summary>
        ''' <param name="IdPadre">Object Identifier</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>A Coleccion of type BusinessUnit</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function List(ByVal IdPadre As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.UnidadNegocio)
            Dim strQuery As String = "SELECT * FROM base_msbusinessunit WHERE IdPadre = " & IdPadre.ToString
            Dim Coleccion As List(Of BAS.UnidadNegocio) = MyBase.SQLList(Of BAS.UnidadNegocio)(strQuery)
            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relations)
            End If
            Return Coleccion
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
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As BAS.UnidadNegocio
            Dim strQuery As String = "SELECT * FROM base_msbusinessunit WHERE Id = " & Id.ToString
            Dim BEBusinessUnit As BAS.UnidadNegocio = MyBase.SQLSearch(Of BAS.UnidadNegocio)(strQuery)
            If BEBusinessUnit IsNot Nothing Then
                Me.LoadRelations(BEBusinessUnit, Relations)
            End If
            Return BEBusinessUnit
        End Function

        Public Function Search(ByVal Id As Int32) As BAS.UnidadNegocio
            Dim strQuery As String = "crm_base_unidadnegocio_searchById"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, Id)
                Dim Coleccion As BEntities.Base.UnidadNegocio = MyBase.SQLConvertidorIDataReader(Of BEntities.Base.UnidadNegocio)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Coleccion
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        ''' <summary>
        ''' 	Search an object of type BusinessUnit    	''' </summary>
        ''' <param name="PlaceId">Object identifier BusinessUnit</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>An object of type BusinessUnit</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function SearchByPlace(ByVal PlaceId As Int32, ByVal ParamArray Relations() As [Enum]) As BAS.UnidadNegocio
            Dim strQuery As String = "SELECT * FROM base_msbusinessunit WHERE ClassifierId = " & PlaceId.ToString
            Dim BEBusinessUnit As BAS.UnidadNegocio = MyBase.SQLSearch(Of BAS.UnidadNegocio)(strQuery)
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

Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports System.Data.Common
Imports BAS = BEntities.Bitacora
Imports BE = BEntities

Namespace Bitacora
    <Serializable> Partial Public Class BitacoraGeneral
        Inherits DALEntity

#Region " Save Methods"

        ''' <summary>
        ''' 	Saves business information object of type Cargo
        ''' </summary>
        ''' <param name="BEObj">Business object of type Cargo </param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Save(ByRef BEObj As BAS.BitacoraGeneral)

            Dim strQuery As String = ""

            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "crm_bitacora_enjoy_general_insert"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                strQuery = "crm_base_cargo_update"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                strQuery = "crm_base_cargo_update"
            End If

            Try

                If BEObj.StatusType <> BE.StatusType.NoAction Then

                    MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                    If (BEObj.StatusType <> BE.StatusType.Insert) Then
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEObj.Id)
                    End If
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@TransaccionId", DbType.Int32, BEObj.TransaccionId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Procedimiento", DbType.String, BEObj.Procedimiento)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Accion", DbType.String, BEObj.Accion)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Campos", DbType.String, BEObj.Campos)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@IpCliente", DbType.String, BEObj.IpCliente)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@MaquinaCliente", DbType.String, BEObj.MaquinaCliente)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEObj.FechaReg)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@TipoTransaccionIdc", DbType.Int32, BEObj.TipoTransaccionIdc)

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
    	''' 	Saves a collection business information object of type  Cargo
    	''' </summary>
    	''' <param name="colBEObj">Business object of type Cargo para Save</param>
    	''' <remarks>
    	''' </remarks>
        Public Sub Save(ByRef colBEObj As List(Of BAS.BitacoraGeneral), ByVal IdMaster As Int32)

            Dim strQuery As String = ""

            Try

                For Each BEobj As BAS.BitacoraGeneral In colBEObj
                    If BEobj.StatusType = BE.StatusType.Insert Then
                        strQuery = "INSERT INTO base_msCargo " &
                        "VALUES(@Id,@CargoId,@Description,@OrganizationChart,@BaseFee); " &
                        "SELECT @@IDENTITY;"
                    ElseIf BEobj.StatusType = BE.StatusType.Update Then
                        strQuery = "UPDATE base_msCargo SET " &
                        "CargoId = @CargoId,Description = @Description,OrganizationChart = @OrganizationChart, BaseFee = @BaseFee " &
                        "WHERE Id = @Id"
                    ElseIf BEobj.StatusType = BE.StatusType.Delete Then
                        strQuery = "DELETE FROM base_msCargo WHERE Id = @Id"
                    End If

                    If BEobj.StatusType <> BE.StatusType.NoAction Then

                        MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery)

                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@TransaccionId", DbType.Int32, BEobj.TransaccionId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Procedimiento", DbType.String, BEobj.Procedimiento)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Accion", DbType.String, BEobj.Accion)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Campos", DbType.String, BEobj.Campos)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEobj.PersonalId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@IpCliente", DbType.String, BEobj.IpCliente)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@MaquinaCliente", DbType.String, BEobj.MaquinaCliente)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEobj.FechaReg)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@TipoTransaccion", DbType.Int32, BEobj.TipoTransaccionIdc)
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
        ''' 	Metodo sobrecargardo que permite cargar la informacion de la base de datos al
        '''		Entidad de Negocio
        ''' </summary>
        ''' <param name="DR">El Datareader que contiene la informacion del objeto</param>
        ''' <returns>Un tipo Generico que herreda de BEGenerico.BEEntidad</returns>
        ''' <remarks>
        ''' 	Para el conjunto de Hijos se deben pasar los enumeradores de los objetos relacionados
        ''' </remarks>
        Protected Overrides Function LoadBE(Of T As BE.BEntity)(ByRef DR As IDataReader) As T
            Dim BEObject As New BAS.BitacoraGeneral

            With DR
                BEObject.Id = .GetInt32(0)
                If Not .IsDBNull(1) Then
                    BEObject.TransaccionId = .GetInt32(1)
                End If
                BEObject.Procedimiento = .GetString(2)
                BEObject.Accion = .GetString(3)
                BEObject.Campos = .GetString(4)
                BEObject.IpCliente = .GetString(5)
                BEObject.MaquinaCliente = .GetString(6)
                BEObject.TipoTransaccionIdc = .GetInt32(7)
                BEObject.PersonalId = .GetInt32(8)
                BEObject.FechaReg = .GetDateTime(9)
            End With

            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        ''' <summary>
        ''' Carga las Relaciones de la Collection dada
        ''' </summary>
        ''' <param name="Collection">Lista de objetos para cargar las Relaciones</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a cargar</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef Collection As List(Of BAS.BitacoraGeneral), ByVal ParamArray Relaciones() As [Enum])
            'Dim DALCargo As Cargo
            'Dim colCargo As List(Of BAS.BitacoraGeneral) = Nothing
            'Dim DALPersonal As Personal
            'Dim colPersonal As List(Of SEC.Personal) = Nothing
            'Dim Keys As IEnumerable(Of Int32)

            'For Each RelationEnum As [Enum] In Relaciones
            '    If RelationEnum.Equals(BAS.relCargo.BitacoraGeneral) Then
            '        DALCargo = New Cargo(True, CType(MyBase.DBFactory, Object))
            '        Keys = (From BEObject In Collection Select BEObject.BitacoraGeneralId).Distinct
            '        colCargo = DALCargo.ReturnChild(Keys, Relaciones)
            '    End If

            '    Keys = From BECargo In Collection Select BECargo.Id
            '    'If RelationEnum.Equals(BAS.relCargo.BitacoraGeneral) Then
            '    '    DALCargo = New Cargo(True, CType(MyBase.DBFactory, Object))
            '    '    colCargo = DALCargo.ReturnChild(Keys, Relaciones)
            '    'End If
            '    If RelationEnum.Equals(BAS.relCargo.Personal) Then
            '        DALPersonal = New Personal(True, CType(MyBase.DBFactory, Object))
            '        colPersonal = DALPersonal.ReturnChild(Keys, Relaciones)
            '    End If

            'Next

            'If Relaciones.GetLength(0) > 0 Then
            '    For Each BECargo In Collection
            '        If colCargo IsNot Nothing Then
            '            BECargo.BitacoraGeneral = (From BEObject In colCargo
            '                                       Where BEObject.Id = BECargo.BitacoraGeneralId
            '                                       Select BEObject).FirstOrDefault
            '            If BECargo.BitacoraGeneral Is Nothing Then
            '                BECargo.BitacoraGeneral = New BAS.BitacoraGeneral
            '            End If
            '        End If
            '        If colCargo IsNot Nothing Then
            '            BECargo.ColeccionCargo = (From BEObject In colCargo
            '                                      Where BEObject.BitacoraGeneralId = BECargo.Id
            '                                      Select BEObject).ToList
            '        End If
            '        If colPersonal IsNot Nothing Then
            '            BECargo.ColeccionPersonal = (From BEObject In colPersonal
            '                                         Where BEObject.BitacoraGeneralId = BECargo.Id
            '                                         Select BEObject).ToList
            '        End If
            '    Next
            'End If

            'DALCargo = Nothing
            'DALPersonal = Nothing
        End Sub

        ''' <summary>
        ''' Load Relacioneship of an Object
        ''' </summary>
        ''' <param name="BECargo">Given Object</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <remarks></remarks>
        Protected Sub LoadRelaciones(ByRef BECargo As BAS.BitacoraGeneral, ByVal ParamArray Relaciones() As [Enum])
            'Dim DALCargo As Cargo
            'Dim DALPersonal As Personal

            'For Each RelationEnum As [Enum] In Relaciones
            '    If RelationEnum.Equals(BAS.relCargo.BitacoraGeneral) Then
            '        DALCargo = New Cargo(True, CType(MyBase.DBFactory, Object))
            '        BECargo.BitacoraGeneral = DALCargo.ReturnMaster(BECargo.BitacoraGeneralId, Relaciones)
            '    End If

            '    Dim Keys() As Int32 = {BECargo.Id}
            '    If RelationEnum.Equals(BAS.relCargo.BitacoraGeneral) Then
            '        DALCargo = New Cargo(True, CType(MyBase.DBFactory, Object))
            '        BECargo.ColeccionCargo = DALCargo.ReturnChild(Keys, Relaciones)
            '    End If
            '    If RelationEnum.Equals(BAS.relCargo.Personal) Then
            '        DALPersonal = New Personal(True, CType(MyBase.DBFactory, Object))
            '        BECargo.ColeccionPersonal = DALPersonal.ReturnChild(Keys, Relaciones)
            '    End If

            'Next

            'DALCargo = Nothing
            'DALPersonal = Nothing
            'DALCargo = Nothing
        End Sub

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Return an object Collection of Cargo
        ''' </summary>
        ''' <param name="Order">Object order property column </param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>A Collection of type Cargo</returns>
        ''' <remarks>
        ''' </remarks>
        '''
        Public Function List(ByVal buscar As String, ByVal estado As Int16, ByVal CantidadRegistros As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.BitacoraGeneral)
            Dim strQuery As String = "crm_base_cargo_listalike"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@buscar", DbType.String, buscar)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, estado)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CantidadRegistros", DbType.String, CantidadRegistros)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@NumeroPagina", DbType.String, NumeroPagina)
                Dim Coleccion As List(Of BAS.BitacoraGeneral) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.BitacoraGeneral)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
        'Public Function List(ByVal Order As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.BitacoraGeneral)
        '    Dim strQuery As String = "SELECT * FROM base_msCargo ORDER By " & Order
        '    Dim Collection As List(Of BAS.BitacoraGeneral) = MyBase.SQLList(Of BAS.BitacoraGeneral)(strQuery)

        '    If Collection.Count > 0 Then
        '        Me.CargarRelaciones(Collection, Relaciones)
        '    End If
        '    Return Collection
        'End Function


        Public Function List(ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.BitacoraGeneral)
            Dim strQuery As String = "select * from crm_bitacora_enjoy_general where Accion = 'LOGIN' order by Id desc"
            Try
                MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery)
                Dim Collection As List(Of BAS.BitacoraGeneral) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.BitacoraGeneral)(MyBase.DBFactory.ExecuteReader(MyBase.Command))

                If Collection.Count > 0 Then
                    Me.CargarRelaciones(Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Public Function Count(ByVal text As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.BitacoraGeneral)

            Dim strQuery As String = "crm_base_cargo_count"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@buscar", DbType.String, text)
                Dim Collection As List(Of BAS.BitacoraGeneral) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.BitacoraGeneral)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    Me.CargarRelaciones(Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try

        End Function
        Public Function ListAutorizados(ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.BitacoraGeneral)
            Dim strQuery As String = "crm_base_cargo_listaautorizados"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of BAS.BitacoraGeneral) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.BitacoraGeneral)(MyBase.DBFactory.ExecuteReader(MyBase.Command))

                If Collection.Count > 0 Then
                    Me.CargarRelaciones(Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        ''' <summary>
        ''' 	Return an object Collection of Cargo
        ''' </summary>
        ''' <param name="IdPadre">Object Identifier</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>A Collection of type Cargo</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function List(ByVal IdPadre As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.BitacoraGeneral)
            Dim strQuery As String = "SELECT * FROM base_msCargo WHERE IdPadre = " & IdPadre.ToString
            Dim Collection As List(Of BAS.BitacoraGeneral) = MyBase.SQLList(Of BAS.BitacoraGeneral)(strQuery)

            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relaciones)
            End If
            Return Collection
        End Function

#End Region

#Region " Search Methods "

        ''' <summary>
        ''' 	Search an object of type Cargo    	''' </summary>
        ''' <param name="Id">Object identifier Cargo</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type Cargo</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.BitacoraGeneral
            Dim strQuery As String = "crm_base_cargo_buscarporid"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.String, Id)
                Dim Coleccion As BAS.BitacoraGeneral = MyBase.SQLConvertidorIDataReader(Of BAS.BitacoraGeneral)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Coleccion
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function SearchPorComentario(ByVal IdPersona As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.BitacoraGeneral
            Dim strQuery As String = "crm_base_cargo_porcomentario"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@IdPersona", DbType.Int32, IdPersona)
                Dim Coleccion As BAS.BitacoraGeneral = MyBase.SQLConvertidorIDataReader(Of BAS.BitacoraGeneral)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Coleccion
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
        Public Sub New(ByVal UseDBCon As Boolean, ByRef BD As Object, ByVal Transaction As DbTransaction)
            MyBase.New(UseDBCon, BD, Transaction)
        End Sub

#End Region

    End Class

End Namespace
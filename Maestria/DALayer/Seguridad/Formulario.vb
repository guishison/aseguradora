Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BE = BEntities
Imports SEC = BEntities.Seguridad
Imports System.Data.Common
Imports System.Net

Namespace Seguridad
    <Serializable>
    Public Class Formulario
        Inherits DALEntity

#Region " Save Methods"
        Public Sub Save(ByRef BEObj As SEC.Formulario)
            Dim strQuery As String = ""
            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "crm_seguridad_formulario_insert"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                strQuery = "crm_seguridad_formulario_update"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                strQuery = "crm_seguridad_formulario_update"
            End If

            Dim DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
            DALBitacora.OpenConnection()
            Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
            With beBitacora
                .Id = 0
                .Procedimiento = strQuery
                .Accion = If(BEObj.StatusType = BEntities.StatusType.Insert, "INSERT", If(BEObj.StatusType = BEntities.StatusType.Update, "UPDATE", "DELETE"))
                .PersonalId = 0
                .StatusType = BEntities.StatusType.Insert
                .FechaReg = Now
                .TipoTransaccionIdc = BEntities.TipoTransaccionBitacora.Exitosa

            End With

            Try
                If BEObj.StatusType <> BE.StatusType.NoAction Then
                    MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                    If (BEObj.StatusType <> BE.StatusType.Insert) Then
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEObj.Id)
                    End If
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Url", DbType.String, BEObj.Url)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEobj.Nombre)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@TipoFormulario", DbType.String, BEObj.TipoFormulario)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@ModuloId", DbType.Int32, BEObj.ModuloId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Icono", DbType.String, BEobj.Icono)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Orden", DbType.Int32, BEobj.Orden)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEobj.Estado)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Acciones", DbType.String, BEObj.Acciones)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, BEobj.UnidadNegocioId)

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
                    If BEObj.StatusType = BEntities.StatusType.Insert Then
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
                beBitacora.TipoTransaccionIdc = BEntities.TipoTransaccionBitacora.Fallida
                DALBitacora.Save(beBitacora)
                DALBitacora.Commit()
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
            Finally
                DALBitacora.CloseConnection()
                MyBase.DisposeCommand()
            End Try
        End Sub

        Public Sub Save(ByRef colBEObj As List(Of SEC.Formulario))
            Dim strQuery As String = ""
            For Each BEobj As SEC.Formulario In colBEObj
                If BEobj.StatusType = BE.StatusType.Insert Then
                    strQuery = "crm_seguridad_formulario_insert"
                ElseIf BEobj.StatusType = BE.StatusType.Update Then
                    strQuery = "crm_seguridad_formulario_update"
                ElseIf BEobj.StatusType = BE.StatusType.Delete Then
                    strQuery = "crm_seguridad_formulario_update"
                End If

                Dim DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
                DALBitacora.OpenConnection()
                Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
                With beBitacora
                    .Id = 0
                    .Procedimiento = strQuery
                    .Accion = If(BEobj.StatusType = BEntities.StatusType.Insert, "INSERT", If(BEobj.StatusType = BEntities.StatusType.Update, "UPDATE", "DELETE"))
                    .PersonalId = 0
                    .StatusType = BEntities.StatusType.Insert
                    .FechaReg = Now
                    .TipoTransaccionIdc = BEntities.TipoTransaccionBitacora.Exitosa

                End With

                Try
                    If BEobj.StatusType <> BE.StatusType.NoAction Then
                        MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                        If (BEobj.StatusType <> BE.StatusType.Insert) Then
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEobj.Id)
                        End If
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Url", DbType.String, BEobj.Url)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEobj.Nombre)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@TipoFormulario", DbType.String, BEobj.TipoFormulario)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@ModuloId", DbType.Int32, BEobj.ModuloId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Icono", DbType.String, BEobj.Icono)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Orden", DbType.Int32, BEobj.Orden)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEobj.Estado)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Acciones", DbType.String, BEobj.Acciones)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, BEobj.UnidadNegocioId)


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
                        If BEobj.StatusType = BEntities.StatusType.Insert Then
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
                    beBitacora.TipoTransaccionIdc = BEntities.TipoTransaccionBitacora.Fallida
                    DALBitacora.Save(beBitacora)
                    DALBitacora.Commit()
                    MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Finally
                    DALBitacora.CloseConnection()
                    MyBase.DisposeCommand()
                End Try
            Next
        End Sub



        ''' <summary>
        ''' Saves Profile relationship n:n
        ''' </summary>
        ''' <param name="ColeccionPerfil"></param>
        ''' <param name="IdMaster"></param>
        ''' <remarks></remarks>
        Public Sub SaveProfile(ByRef ColeccionPerfil As List(Of SEC.Perfil), ByVal IdMaster As Long)
            Dim strQuery As String = "DELETE FROM PsProfileOption WHERE OptionId = " & IdMaster.ToString
            Try
                MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery)
                MyBase.DBFactory.ExecuteNonQuery(MyBase.Command)

                For Each BEObj In ColeccionPerfil
                    If BEObj.StatusType <> BE.StatusType.NoAction And BEObj.StatusType <> BE.StatusType.Delete Then
                        strQuery = "INSERT INTO PsProfileOption Values(@ProfileId,@OptionId)"
                        MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@OptionId", DbType.Int64, IdMaster)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@ProfileId", DbType.Int64, BEObj.Id)
                        MyBase.DBFactory.ExecuteNonQuery(MyBase.Command)
                    End If
                Next
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region " Methods "

        ''' <summary>
        ''' 	For use on data access layer at assembly level, return an  Option type object
        ''' </summary>
        ''' <param name="Id">Object Identifier Option</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>An object of type Option</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnMaster(ByVal Id As Long, ByVal ParamArray Relations() As [Enum]) As SEC.Formulario
            Return Buscar(Id, Relations)
        End Function

        ''' <summary>
        ''' 	Devuelve un objeto Option de tipo uno a uno con otro objeto
        ''' </summary>
        ''' <param name="Keys">Los identificadores de los objetos relacionados a Option</param>
        ''' <param name="Relations">Enumerador de Relations a retorar</param>
        ''' <returns>Un Objeto de tipo Option</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)
            Dim llaves As String = MyBase.KeysArray(Keys).ToString.Replace("(", "(',")
            llaves = llaves.Replace(")", ",')")
            Dim strQuery As String = "crm_seguridad_formulario_buscaformularios"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@llaves", DbType.String, llaves)
                Dim Coleccion As List(Of SEC.Formulario) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Formulario)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Coleccion.Count > 0 Then
                    'Me.CargarRelaciones(Coleccion, Relations)
                End If
                Return Coleccion
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal hotelId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)
            Dim strQuery As String = "select * from crm_seguridad_formulario where UnidadNegocioId = " & hotelId & " and ModuloId IN " & MyBase.KeysArray(Keys) & " and Estado = 1"
            Dim Collection As List(Of SEC.Formulario) = Nothing
            Collection = MyBase.SQLList(Of SEC.Formulario)(strQuery)
            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relations)
            End If
            Return Collection
        End Function
        Public Function ReturnChild2(ByVal Keys As IEnumerable(Of String), ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)
            Dim strQuery As String = "select * from crm_seguridad_formulario where Id IN " & MyBase.KeysArray(Keys)
            Dim Collection As List(Of SEC.Formulario) = Nothing
            Collection = MyBase.SQLList(Of SEC.Formulario)(strQuery)
            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relations)
            End If
            Return Collection
        End Function
        Public Function ReturnChildByModuloId(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)
            Dim strQuery As String = "select * from crm_seguridad_formulario where ModuloId IN " & MyBase.KeysArray(Keys)
            Dim Collection As List(Of SEC.Formulario) = Nothing
            Collection = MyBase.SQLList(Of SEC.Formulario)(strQuery)
            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relations)
            End If
            Return Collection
        End Function

        ''' <summary>
        ''' 	Devuelve un objeto Option de tipo uno a uno con otro objeto
        ''' </summary>
        ''' <param name="Keys">Los identificadores de los objetos relacionados a Option</param>
        ''' <param name="Relations">Enumerador de Relations a retorar</param>
        ''' <returns>Un Objeto de tipo Option</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnChildOption(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)
            Dim strQuery As String = "SELECT O.* FROM security_msoption O INNER JOIN security_psprofileoption PO on O.Id = PO.OptionId And PO.ProfileId IN " & MyBase.KeysArray(Keys)
            Dim Collection As List(Of SEC.Formulario) = Nothing
            Collection = MyBase.SQLList(Of SEC.Formulario)(strQuery)
            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relations)
            End If
            Return Collection
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
            Dim BEObject As New SEC.Formulario

            With DR
                BEObject.Id = .GetInt32(0)
                BEObject.Url = .GetString(1).Trim
                BEObject.Nombre = .GetString(2)
                If Not .IsDBNull(3) Then
                    BEObject.TipoFormulario = .GetString(3)
                End If
                If Not .IsDBNull(4) Then
                    BEObject.ModuloId = .GetInt32(4)
                End If
                If Not .IsDBNull(5) Then
                    BEObject.Icono = .GetString(5)
                End If
                If Not .IsDBNull(6) Then
                    BEObject.Orden = .GetInt32(6)
                End If
                If Not .IsDBNull(7) Then
                    BEObject.Estado = .GetInt16(7)
                End If
                If Not .IsDBNull(8) Then
                    BEObject.Acciones = .GetString(8)
                End If
                If Not .IsDBNull(9) Then
                    BEObject.UnidadNegocioId = .GetInt32(9)
                End If
            End With

            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        ''' <summary>
        ''' Carga las Relations de la Collection dada
        ''' </summary>
        ''' <param name="Collection">Lista de objetos para cargar las Relations</param>
        ''' <param name="Relations">Enumerador de Relations a cargar</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef Collection As List(Of SEC.Formulario), ByVal ParamArray Relations() As [Enum])
            Dim DALProfile As Perfil
            Dim colPerfil As List(Of SEC.Perfil) = Nothing
            Dim DALModule As Modulo
            Dim colModule As List(Of SEC.Modulo) = Nothing
            Dim Keys As IEnumerable(Of Int32) = Nothing

            For Each RelationEnum As [Enum] In Relations
                If RelationEnum.Equals(SEC.relFormulario.Modulo) Then
                    DALModule = New Modulo(True, CType(MyBase.DBFactory, Object))
                    Keys = (From BEObject In Collection Select BEObject.ModuloId).Distinct
                    'colModule = DALModule.ReturnChild(Keys, Relations)
                End If

                Keys = From BEOption In Collection Select BEOption.Id
                If RelationEnum.Equals(SEC.relFormulario.Perfil) Then
                    DALProfile = New Perfil(True, CType(MyBase.DBFactory, Object))
                    colPerfil = DALProfile.ReturnChild(Keys, Relations)
                End If

            Next

            If Relations.GetLength(0) > 0 Then
                For Each BEOption In Collection
                    If colModule IsNot Nothing Then
                        BEOption.Modulo = (From BEObject In colModule
                                           Where BEObject.Id = BEOption.ModuloId
                                           Select BEObject).FirstOrDefault
                    End If
                    If colPerfil IsNot Nothing Then
                        Dim Relationship As List(Of BE.Relation)

                        Relationship = Me.ReturnProfile(Keys)
                        BEOption.ColeccionPerfil = (From BEObject In colPerfil Join Key In Relationship On Key.FirstKey Equals BEObject.Id
                                                    Where Key.SecondKey = BEOption.Id
                                                    Select BEObject).ToList
                    End If
                Next
            End If

            DALProfile = Nothing
            DALModule = Nothing
        End Sub

        ''' <summary>
        ''' Load Relationship of an Object
        ''' </summary>
        ''' <param name="BEOption">Given Object</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef BEOption As SEC.Formulario, ByVal ParamArray Relations() As [Enum])
            Dim DALProfile As Perfil
            Dim DALModule As Modulo

            For Each RelationEnum As [Enum] In Relations
                If RelationEnum.Equals(SEC.relFormulario.Modulo) Then
                    DALModule = New Modulo(True, CType(MyBase.DBFactory, Object))
                    BEOption.Modulo = DALModule.ReturnMaster(BEOption.ModuloId, Relations)
                End If

                Dim Keys() As Int32 = {BEOption.Id}
                If RelationEnum.Equals(SEC.relFormulario.Perfil) Then
                    DALProfile = New Perfil(True, CType(MyBase.DBFactory, Object))
                    BEOption.ColeccionPerfil = DALProfile.ReturnChild(Keys, Relations)
                End If

            Next

            DALProfile = Nothing
            DALModule = Nothing
        End Sub

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Return an object Collection of Option
        ''' </summary>
        ''' <param name="Order">Object order property column </param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>A Collection of type Option</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Listar(ByVal Order As String, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)
            Dim strQuery As String = "SELECT * FROM security_msoption ORDER By " & Order
            Dim Collection As List(Of SEC.Formulario) = MyBase.SQLList(Of SEC.Formulario)(strQuery)

            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relations)
            End If
            Return Collection
        End Function
        Public Function ListByUrlContains(ByVal Url As String, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)
            Dim strQuery As String = "crm_seguridad_formulario_listbyurlcontains"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Url", DbType.String, Url)
                Dim Collection As List(Of SEC.Formulario) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Formulario)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    Me.CargarRelaciones(Collection, Relations)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        ''' <summary>
        ''' 	Return an object Collection of Option
        ''' </summary>
        ''' <param name="IdPadre">Object Identifier</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>A Collection of type Option</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function List(ByVal HotelId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)
            Dim strQuery As String = "crm_seguridad_formulario_buscarporhotel"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@HotelId", DbType.Int32, HotelId)
                Dim Collection As List(Of SEC.Formulario) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Formulario)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    Me.CargarRelaciones(Collection, Relations)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListByModulo(ByVal ModuloId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)
            Dim strQuery As String = "crm_seguridad_formulario_buscarpormodulo"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Modulo", DbType.Int32, ModuloId)
                Dim Collection As List(Of SEC.Formulario) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Formulario)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    Me.CargarRelaciones(Collection, Relations)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        ''' <summary>
        ''' Return an Array of keys from table ProfileOption based on the project key
        ''' </summary>
        ''' <param name="Keys"></param>
        ''' <param name="Relations"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Function ReturnProfile(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relations() As [Enum]) As List(Of BE.Relation)
            Dim strQuery As String = "SELECT IT.* FROM PsProfileOption IT INNER JOIN " &
                                     "MsOption A ON A.Id =  IT.OptionId WHERE IT.OptionId in " & MyBase.KeysArray(Keys)
            Dim RelationshipKeys As New List(Of BE.Relation)
            Dim drReader As IDataReader = Nothing
            Dim BERelationship As New BE.Relation

            Try
                drReader = MyBase.GenericList(strQuery)
                With drReader
                    Do While .Read()
                        BERelationship = New BE.Relation
                        BERelationship.FirstKey = drReader.GetInt64(0)
                        BERelationship.SecondKey = drReader.GetInt64(1)
                        RelationshipKeys.Add(BERelationship)
                    Loop
                End With
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
            Finally
                DisposeReader(drReader)
            End Try
            Return RelationshipKeys
        End Function


        Public Function ListMenuOptions(ByVal Id As Int32, ByVal UnidadNegocioId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)

            Dim strQuery As String = "crm_seguridad_formulario_listapermisos"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.String, Id)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.String, UnidadNegocioId)
                Dim Collection As List(Of SEC.Formulario) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Formulario)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Public Function ListaMenuFormulario(ByVal Id As Int32, ByVal UnidadNegocioId As Int32) As List(Of SEC.Formulario)
            Dim strQuery As String = "crm_seguridad_formulario_listapermisos"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, Id)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, UnidadNegocioId)
                Dim Collection As List(Of SEC.Formulario) = CType(MyBase.DBFactory.ExecuteReader(MyBase.Command), List(Of SEC.Formulario))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

#End Region

#Region " Search Methods "

        ''' <summary>
        ''' 	Search an object of type Option    	''' </summary>
        ''' <param name="Id">Object identifier Option</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>An object of type Option</returns>
        ''' <remarks>
        ''' </remarks> crm_seguridad_formulario_buscarformularioporun
        Public Function Buscar(ByVal Id As Long, ByVal ParamArray Relations() As [Enum]) As SEC.Formulario
            Dim strQuery As String = "SELECT * FROM security_msoption WHERE Id = " & Id.ToString
            Dim BEOption As SEC.Formulario = MyBase.SQLSearch(Of SEC.Formulario)(strQuery)

            If BEOption IsNot Nothing Then
                Me.CargarRelaciones(BEOption, Relations)
            End If
            Return BEOption
        End Function


        Public Function BuscarporUnidadNegocio(ByVal Url As String, ByVal UnidadNegocioId As Int32, ByVal ParamArray Relations() As [Enum]) As SEC.Formulario
            Dim strQuery As String = "crm_seguridad_formulario_buscarformularioporun"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Url", DbType.String, Url)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.String, UnidadNegocioId)
                Dim Collection As SEC.Formulario = MyBase.SQLConvertidorIDataReader(Of SEC.Formulario)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
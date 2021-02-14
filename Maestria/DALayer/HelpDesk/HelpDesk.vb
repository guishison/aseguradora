Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports System.Data.Common
Imports System.Net
Imports DALayer.Base
Imports DALayer.Seguridad
Imports BAS = BEntities.AyudaUsuario
Imports BBS = BEntities.Base
Imports BE = BEntities
Imports BES = BEntities.Seguridad

Namespace AyudaUsuario

    <Serializable>
    Partial Public Class HelpDesk
        Inherits DALEntity

#Region " Save Methods"

        ''' <summary>
        ''' 	Saves business information object of type Cargo
        ''' </summary>
        ''' <param name="BEObj">Business object of type Cargo </param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Save(ByRef BEObj As BAS.HelpDesk)

            Dim strQuery As String = ""

            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "crm_helpdesk_helpdesk_insert"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                strQuery = "crm_helpdesk_helpdesk_update"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                strQuery = "crm_helpdesk_helpdesk_update"
            End If

            Dim DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
            DALBitacora.OpenConnection()
            Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
            With beBitacora
                .Id = 0
                .Procedimiento = strQuery
                .Accion = If(BEObj.StatusType = BEntities.StatusType.Insert, "INSERT", If(BEObj.StatusType = BEntities.StatusType.Update, "UPDATE", "DELETE"))
                .PersonalId = BEObj.PersonalId
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
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalSolicitanteId", DbType.Int32, BEObj.PersonalSolicitanteId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@ModuloId", DbType.Int32, BEObj.ModuloId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FormularioId", DbType.Int32, BEObj.FormularioId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@ProblemaDescripcion", DbType.String, BEObj.ProblemaDescripcion)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@TipoTicketIdc", DbType.Int32, BEObj.TipoTicketIdc)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PrioridadIdc", DbType.Int32, BEObj.PrioridadIdc)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@EstadoHelpDeskIdc", DbType.Int32, BEObj.EstadoHelpDeskIdc)
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

        ''' <summary>
    	''' 	Saves a collection business information object of type  Cargo
    	''' </summary>
    	''' <param name="colBEObj">Business object of type Cargo para Save</param>
    	''' <remarks>
    	''' </remarks>
        Public Sub Save(ByRef colBEObj As List(Of BAS.HelpDesk))

            Dim strQuery As String = ""

            Try

                For Each BEobj As BAS.HelpDesk In colBEObj

                    If BEobj.StatusType = BE.StatusType.Insert Then
                        strQuery = "crm_helpdesk_helpdesk_insert"
                    ElseIf BEobj.StatusType = BE.StatusType.Update Then
                        strQuery = "crm_helpdesk_helpdesk_update"
                    ElseIf BEobj.StatusType = BE.StatusType.Delete Then
                        strQuery = "crm_helpdesk_helpdesk_update"
                    End If

                    If BEobj.StatusType <> BE.StatusType.NoAction Then

                        MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                        If (BEobj.StatusType <> BE.StatusType.Insert) Then
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEobj.Id)
                        End If
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalSolicitanteId", DbType.Int32, BEobj.PersonalSolicitanteId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@ModuloId", DbType.Int32, BEobj.ModuloId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FormularioId", DbType.Int32, BEobj.FormularioId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@ProblemaDescripcion", DbType.String, BEobj.ProblemaDescripcion)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@TipoTicketIdc", DbType.Int32, BEobj.TipoTicketIdc)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PrioridadIdc", DbType.Int32, BEobj.PrioridadIdc)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@EstadoHelpDeskIdc", DbType.Int32, BEobj.EstadoHelpDeskIdc)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEobj.PersonalId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEobj.FechaReg)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaActualizacion", DbType.DateTime, BEobj.FechaActualizacion)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEobj.Estado)
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
        ''' 	For use on data access layer at assembly level, return an  Cargo type object
        ''' </summary>
        ''' <param name="Id">Object Identifier Cargo</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type Cargo</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnMaster(ByVal Id As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.HelpDesk
            Return Search(Id, Relaciones)
        End Function

        ''' <summary>
        ''' 	Devuelve un objeto Cargo de tipo uno a uno con otro objeto
        ''' </summary>
        ''' <param name="Keys">Los identificadores de los objetos relacionados a Cargo</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a retorar</param>
        ''' <returns>Un Objeto de tipo Cargo</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)
            Dim llaves As String = MyBase.KeysArray(Keys).ToString.Replace("(", "(',")
            llaves = llaves.Replace(")", ",')")
            Dim strQuery As String = "crm_base_cargo_buscarcargos"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@llaves", DbType.String, llaves)
                Dim Coleccion As List(Of BAS.HelpDesk) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.HelpDesk)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
            Dim BEObject As New BAS.HelpDesk

            With DR
                BEObject.Id = .GetInt32(0)
                BEObject.PersonalSolicitanteId = .GetInt32(1)
                BEObject.ModuloId = .GetInt32(2)
                BEObject.FormularioId = .GetInt32(3)
                BEObject.ProblemaDescripcion = .GetString(4)
                BEObject.TipoTicketIdc = .GetInt32(5)
                BEObject.PrioridadIdc = .GetInt32(6)
                BEObject.EstadoHelpDeskIdc = .GetInt32(7)
                BEObject.PersonalId = .GetInt32(8)
                BEObject.FechaReg = .GetDateTime(9)
                BEObject.FechaActualizacion = .GetDateTime(10)
                BEObject.Estado = .GetInt16(11)
            End With

            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        ''' <summary>
        ''' Carga las Relaciones de la Collection dada
        ''' </summary>
        ''' <param name="Collection">Lista de objetos para cargar las Relaciones</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a cargar</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef Collection As List(Of BAS.HelpDesk), ByVal ParamArray Relaciones() As [Enum])
            Dim colHelpDeskDetalle As List(Of BAS.HelpDeskDetalle) = Nothing
            Dim colModulo As List(Of BES.Modulo) = Nothing
            Dim colFormulario As List(Of BES.Formulario) = Nothing
            Dim colPersonal As List(Of BES.Personal) = Nothing
            Dim colTipoTicket As List(Of BBS.Clasificadores) = Nothing
            Dim colPrioridad As List(Of BBS.Clasificadores) = Nothing
            Dim colEstadoHelpDesk As List(Of BBS.Clasificadores) = Nothing
            'Dim DALPersonal As Personal
            'Dim colPersonal As List(Of SEC.Personal) = Nothing
            Dim Keys As IEnumerable(Of Int32)

            For Each RelationEnum As [Enum] In Relaciones
                'Keys = From BEHelpDesk In Collection Select BEHelpDesk.Id
                If RelationEnum.Equals(BAS.relHelpDesk.HelpDeskDetalle) Then
                    For Each item In Collection
                        Using DALHelpDeskDetalle = New HelpDeskDetalle(True, CType(MyBase.DBFactory, Object))
                            Keys = (From BEObject In Collection Select BEObject.Id).Distinct
                            item.ListaDetalleHelpDesk = DALHelpDeskDetalle.ListByHelpDekId(item.Id)
                        End Using
                    Next
                End If

                'If RelationEnum.Equals(BAS.relCargo.HelpDesk) Then
                '    DALCargo = New Cargo(True, CType(MyBase.DBFactory, Object))
                '    colCargo = DALCargo.ReturnChild(Keys, Relaciones)
                'End If
                If RelationEnum.Equals(BAS.relHelpDesk.Modulo) Then
                    Using DALModulo = New Modulo(True, CType(MyBase.DBFactory, Object))
                        Keys = (From BEObject In Collection Select BEObject.ModuloId).Distinct
                        colModulo = DALModulo.ReturnChild(Keys, Relaciones)
                    End Using
                End If
                If RelationEnum.Equals(BAS.relHelpDesk.Formulario) Then
                    Using DALFormulario = New Formulario(True, CType(MyBase.DBFactory, Object))
                        Keys = (From BEObject In Collection Select BEObject.FormularioId).Distinct
                        colFormulario = DALFormulario.ReturnChild(Keys, Relaciones)
                    End Using
                End If
                If RelationEnum.Equals(BAS.relHelpDesk.Personal) Then
                    Using DALPersonal = New Personal(True, CType(MyBase.DBFactory, Object))
                        Keys = (From BEObject In Collection Select BEObject.PersonalSolicitanteId).Distinct
                        colPersonal = DALPersonal.ReturnChild(Keys, Relaciones)
                    End Using
                End If
                If RelationEnum.Equals(BAS.relHelpDesk.TipoTicket) Then
                    Using DALClasificadores = New Clasificadores(True, CType(MyBase.DBFactory, Object))
                        Keys = (From BEObject In Collection Select BEObject.TipoTicketIdc).Distinct
                        colTipoTicket = DALClasificadores.ReturnChild(Keys, Relaciones)
                    End Using
                End If
                If RelationEnum.Equals(BAS.relHelpDesk.Prioridad) Then
                    Using DALClasificadores = New Clasificadores(True, CType(MyBase.DBFactory, Object))
                        Keys = (From BEObject In Collection Select BEObject.PrioridadIdc).Distinct
                        colPrioridad = DALClasificadores.ReturnChild(Keys, Relaciones)
                    End Using
                End If
                If RelationEnum.Equals(BAS.relHelpDesk.EstadoHelpDesk) Then
                    Using DALClasificadores = New Clasificadores(True, CType(MyBase.DBFactory, Object))
                        Keys = (From BEObject In Collection Select BEObject.EstadoHelpDeskIdc).Distinct
                        colEstadoHelpDesk = DALClasificadores.ReturnChild(Keys, Relaciones)
                    End Using
                End If

            Next

            If Relaciones.GetLength(0) > 0 Then
                For Each BEHelpDesk In Collection
                    If colModulo IsNot Nothing Then
                        BEHelpDesk.Modulo = (From BEObject In colModulo
                                             Where BEObject.Id = BEHelpDesk.ModuloId
                                             Select BEObject).FirstOrDefault
                    End If
                    If colFormulario IsNot Nothing Then
                        BEHelpDesk.Formulario = (From BEObject In colFormulario
                                                 Where BEObject.Id = BEHelpDesk.FormularioId
                                                 Select BEObject).FirstOrDefault
                    End If
                    If colPersonal IsNot Nothing Then
                        BEHelpDesk.Personal = (From BEObject In colPersonal
                                               Where BEObject.Id = BEHelpDesk.PersonalSolicitanteId
                                               Select BEObject).FirstOrDefault
                    End If
                    If colTipoTicket IsNot Nothing Then
                        BEHelpDesk.TipoTicket = (From BEObject In colTipoTicket
                                                 Where BEObject.Id = BEHelpDesk.TipoTicketIdc
                                                 Select BEObject).FirstOrDefault
                    End If
                    If colPrioridad IsNot Nothing Then
                        BEHelpDesk.Prioridad = (From BEObject In colPrioridad
                                                Where BEObject.Id = BEHelpDesk.PrioridadIdc
                                                Select BEObject).FirstOrDefault
                    End If
                    If colEstadoHelpDesk IsNot Nothing Then
                        BEHelpDesk.EstadoHelpDesk = (From BEObject In colEstadoHelpDesk
                                                     Where BEObject.Id = BEHelpDesk.EstadoHelpDeskIdc
                                                     Select BEObject).FirstOrDefault
                    End If
                Next
            End If

        End Sub

        ''' <summary>
        ''' Load Relacioneship of an Object
        ''' </summary>
        ''' <param name="BEHelpDesk">Given Object</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <remarks></remarks>
        Protected Sub LoadRelaciones(ByRef BEHelpDesk As BAS.HelpDesk, ByVal ParamArray Relaciones() As [Enum])
            'Dim DALPersonal As Personal

            'For Each RelationEnum As [Enum] In Relaciones
            '    If RelationEnum.Equals(BAS.relCargo.HelpDesk) Then
            '        DALCargo = New Cargo(True, CType(MyBase.DBFactory, Object))
            '        BECargo.HelpDesk = DALCargo.ReturnMaster(BECargo.HelpDeskId, Relaciones)
            '    End If

            Dim Keys() As Int32 = {BEHelpDesk.Id}
            If Relaciones.Equals(BAS.relHelpDesk.HelpDeskDetalle) Then
                Using DALHelpDeskDetalle = New HelpDeskDetalle(True, CType(MyBase.DBFactory, Object))
                    BEHelpDesk.ListaDetalleHelpDesk = DALHelpDeskDetalle.ReturnChild(Keys, Relaciones)
                End Using
            End If
            '    If RelationEnum.Equals(BAS.relCargo.Personal) Then
            '        DALPersonal = New Personal(True, CType(MyBase.DBFactory, Object))
            '        BECargo.ColeccionPersonal = DALPersonal.ReturnChild(Keys, Relaciones)
            '    End If

            'Next

            'DALPersonal = Nothing
            'DALCargo = Nothing
        End Sub

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Return an object Collection of Cargo
        ''' </summary>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>A Collection of type Cargo</returns>
        ''' <remarks>
        ''' </remarks>
        '''
        Public Function List(ByVal buscar As String, ByVal estado As Int16, ByVal CantidadRegistros As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)
            Dim strQuery As String = "crm_base_cargo_listalike"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@buscar", DbType.String, buscar)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, estado)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CantidadRegistros", DbType.String, CantidadRegistros)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@NumeroPagina", DbType.String, NumeroPagina)
                Dim Coleccion As List(Of BAS.HelpDesk) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.HelpDesk)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
        'Public Function List(ByVal Order As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)
        '    Dim strQuery As String = "SELECT * FROM base_msCargo ORDER By " & Order
        '    Dim Collection As List(Of BAS.HelpDesk) = MyBase.SQLList(Of BAS.HelpDesk)(strQuery)

        '    If Collection.Count > 0 Then
        '        Me.CargarRelaciones(Collection, Relaciones)
        '    End If
        '    Return Collection
        'End Function


        Public Function ListaBusqueda(ByVal Texto As String, ByVal Inicial As DateTime, ByVal Final As DateTime, ByVal EstadoHelpDesk As Int32, PageIndex As Int32, PageSize As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)
            Dim strQuery As String = "crm_helpdesk_helpdesk_list"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Text", DbType.String, Texto)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Inicial", DbType.DateTime, Inicial)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Final", DbType.DateTime, Final)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@EstadoHelpDesk", DbType.String, If(EstadoHelpDesk = -1, "%%", EstadoHelpDesk.ToString))
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@PageIndex", DbType.Int32, PageIndex)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@PageSize", DbType.Int32, PageSize)
                Dim Collection As List(Of BAS.HelpDesk) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.HelpDesk)(MyBase.DBFactory.ExecuteReader(MyBase.Command))

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
        Public Function ListaBusquedaCount(ByVal Texto As String, ByVal Inicial As DateTime, ByVal Final As DateTime, ByVal EstadoHelpDesk As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)
            Dim strQuery As String = "crm_helpdesk_helpdesk_listcount"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Text", DbType.String, Texto)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Inicial", DbType.DateTime, Inicial)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Final", DbType.DateTime, Final)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@EstadoHelpDesk", DbType.String, If(EstadoHelpDesk = -1, "%%", EstadoHelpDesk.ToString))
                Dim Collection As List(Of BAS.HelpDesk) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.HelpDesk)(MyBase.DBFactory.ExecuteReader(MyBase.Command))

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
        Public Function List(ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)
            Dim strQuery As String = "crm_helpdesk_helpdesk_listcount2"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of BAS.HelpDesk) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.HelpDesk)(MyBase.DBFactory.ExecuteReader(MyBase.Command))

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

        Public Function Count(ByVal text As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)

            Dim strQuery As String = "crm_base_cargo_count"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@buscar", DbType.String, text)
                Dim Collection As List(Of BAS.HelpDesk) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.HelpDesk)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
        Public Function ListAutorizados(ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)
            Dim strQuery As String = "crm_base_cargo_listaautorizados"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of BAS.HelpDesk) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.HelpDesk)(MyBase.DBFactory.ExecuteReader(MyBase.Command))

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
        Public Function List(ByVal IdPadre As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)
            Dim strQuery As String = "SELECT * FROM base_msCargo WHERE IdPadre = " & IdPadre.ToString
            Dim Collection As List(Of BAS.HelpDesk) = MyBase.SQLList(Of BAS.HelpDesk)(strQuery)

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
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.HelpDesk
            Dim strQuery As String = "crm_helpdesk_helpdesk_search"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.String, Id)
                Dim Coleccion As BAS.HelpDesk = MyBase.SQLConvertidorIDataReader(Of BAS.HelpDesk)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Coleccion IsNot Nothing Then
                    Me.LoadRelaciones(Coleccion, Relaciones)
                End If
                Return Coleccion
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function SearchPorComentario(ByVal IdPersona As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.HelpDesk
            Dim strQuery As String = "crm_base_cargo_porcomentario"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@IdPersona", DbType.Int32, IdPersona)
                Dim Coleccion As BAS.HelpDesk = MyBase.SQLConvertidorIDataReader(Of BAS.HelpDesk)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
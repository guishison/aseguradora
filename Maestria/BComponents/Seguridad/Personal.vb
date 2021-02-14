Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BE = BEntities
Imports DAL = DALayer.Seguridad
Imports SEC = BEntities.Seguridad

Namespace Seguridad
    <Serializable>
    Public Class Personal
        Inherits BCEntity

#Region " Buscar Methods "

        ''' <summary>
        ''' 	Buscar for business objects of type User
        ''' </summary>
        ''' <param name="Id">Object identifier User</param>
        ''' <param name="Relations">relationship enumetators</param>
        ''' <returns>An object of type User</returns>
        ''' <remarks>
        ''' 	To get relationship objects, suply relationship enumetators
        ''' </remarks>
        Public Function Buscar(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As SEC.Personal
            Dim BEObject As SEC.Personal = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.Buscar(Id, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function

        Public Function Buscar2(ByVal Id As Long, ByVal ParamArray Relations() As [Enum]) As SEC.Personal
            Dim BEObject As SEC.Personal = Nothing

            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.Buscar2(Id, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function




        Public Function FindByProfileId(ByVal ProfileId As Long, ByVal ParamArray Relations() As [Enum]) As SEC.Personal
            Dim BEObject As SEC.Personal = Nothing

            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.FindByProfileId(ProfileId, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Buscar for collection business objects of type User
        ''' </summary>
        ''' <param name="Order">Property column to specify collection order</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>Object collection of type User</returns>
        ''' <remarks>
        ''' 	To get relationship objects, suply relationship enumetators
        ''' </remarks>
        Public Function Listar(ByVal Order As String, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DAL.Personal
                    BECollection = DALObject.Listar(Order, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function ListarNoAsignado(ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DAL.Personal
                    BECollection = DALObject.ListarNoAsignados(Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListarNoAsignado2(ByVal text As String, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DAL.Personal
                    BECollection = DALObject.ListarNoAsignados2(text, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListarNoAsignado3(ByVal text As String, ByVal SalaId As Int32, ByVal UnidadNegocioPadreId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DAL.Personal
                    BECollection = DALObject.ListarNoAsignados3(text, SalaId, UnidadNegocioPadreId, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function ListarNoAsignadoOperador(ByVal text As String, ByVal CargoId As Int32, ByVal UnidadNegocioPadreId As Int32, ByVal Estado As Int16, ByVal CantidadRegistros As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DAL.Personal
                    BECollection = DALObject.ListarNoAsignadosOperador(text, CargoId, UnidadNegocioPadreId, Estado, CantidadRegistros, NumeroPagina, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListarNoAsignadoOperadorCount(ByVal text As String, ByVal CargoId As Int32, ByVal UnidadNegocioPadreId As Int32) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DAL.Personal
                    BECollection = DALObject.ListarNoAsignadosOperadorCount(text, CargoId, UnidadNegocioPadreId)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListarPersonalPorCargo(ByVal idcargo As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim BEObject As List(Of SEC.Personal) = Nothing
            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.ListarPersonalPorCargo(idcargo, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function ListarPersonalPorCargo2(ByVal idcargo As Int32, ByVal UnidadNegocioPadreId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim BEObject As List(Of SEC.Personal) = Nothing
            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.ListarPersonalPorCargo2(idcargo, UnidadNegocioPadreId, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function ListarPersonalPorCargoxsala(ByVal idcargo As Int32, ByVal idsala As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim BEObject As List(Of SEC.Personal) = Nothing
            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.ListarPersonalPorCargoxsala(idcargo, idsala, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function ListarPersonalOperadorTmk(ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim BEObject As List(Of SEC.Personal) = Nothing
            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.ListarPersonalOperadorTmk(Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function ListarPersonalPorGrupoCargo(ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim BEObject As List(Of SEC.Personal) = Nothing

            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.ListarPersonalPorGrupoCargo(Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function

        Public Function ListarPersonalPorGrupoCargoxsala(ByVal IdSala As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim BEObject As List(Of SEC.Personal) = Nothing

            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.ListarPersonalPorGrupoCargoxsala(IdSala, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function ListCobrador(ByVal IdCobrador As Int32, ByVal Estado As Int16, ByVal CantidadRegistros As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DALayer.Seguridad.Personal
                    BECollection = DALObject.ListarPersonalPorCargo(IdCobrador, Estado, CantidadRegistros, NumeroPagina, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListCobradorsearch(ByVal text As String, ByVal IdCobrador As Int32, ByVal Estado As Int16, ByVal CantidadRegistros As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DALayer.Seguridad.Personal
                    BECollection = DALObject.ListarPersonalSearchNombrePorCargo(text, IdCobrador, Estado, CantidadRegistros, NumeroPagina, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function ListCobradorsearchCount(ByVal text As String, ByVal IdCobrador As Int32, ByVal Estado As Int16, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DALayer.Seguridad.Personal
                    BECollection = DALObject.ListarPersonalSearchNombrePorCargoCount(text, IdCobrador, Estado, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function ListCobradorCount(ByVal IdCobrador As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DALayer.Seguridad.Personal
                    BECollection = DALObject.ListarPersonalPorCargoCount(IdCobrador, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function ListarCompleto(ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim BEObject As List(Of SEC.Personal) = Nothing

            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.ListarCompleto(Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function ListarCompletoUN(ByVal UnidadNegocioPadreId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim BEObject As List(Of SEC.Personal) = Nothing

            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.ListarCompletoUN(UnidadNegocioPadreId, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function ListarCloser(ByVal Idsala As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim BEObject As List(Of SEC.Personal) = Nothing

            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.ListarCloser(Idsala, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function



        ''' <summary>
        ''' 	Buscar for collection business objects of type User
        ''' </summary>
        ''' <param name="IdPosition">Relationship Identifier</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>Object collection of type User</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Listar(ByVal IdPosition As Long, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DAL.Personal
                    BECollection = DALObject.Listar(IdPosition, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListByDepartamento(ByVal Text As String, ByVal DepartamentoId As String, ByVal Estado As Int16, ByVal CantidadRegistros As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DALayer.Seguridad.Personal
                    BECollection = DALObject.ListByDepartamento(Text, DepartamentoId, Estado, CantidadRegistros, NumeroPagina, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListByDepartamentoCount(ByVal Text As String, ByVal DepartamentoId As String, ByVal Estado As Int16, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DALayer.Seguridad.Personal
                    BECollection = DALObject.ListByDepartamentoCount(Text, DepartamentoId, Estado, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

#End Region

#Region " Save Methods "

        ''' <summary>
        ''' 	Saves data object of type
        ''' </summary>
        ''' <param name="BEObj">Object type User</param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Guardar(ByRef BEObj As SEC.Personal)
            Dim DALObject As DAL.Personal = Nothing
            Dim DALPersonalPassword As DAL.PersonalPassword = Nothing
            'Dim DALPerfilPersonal As DAL.PerfilPersonal = Nothing

            Dim DALSupervisorPersonal As DAL.PersonalSupervisor = Nothing
            Me.ErrorCollection.Clear()

            If Me.Validar(BEObj) Then
                Try
                    DALObject = New DAL.Personal
                    DALObject.OpenConnection()
                    DALObject.Guardar(BEObj)
                    'DALPerfilPersonal = New DAL.PerfilPersonal(True, CObj(DALObject.DBFactory), DALObject.Transaction)
                    DALSupervisorPersonal = New DAL.PersonalSupervisor(True, CObj(DALObject.DBFactory), DALObject.Transaction)
                    DALPersonalPassword = New DAL.PersonalPassword(True, CObj(DALObject.DBFactory), DALObject.Transaction)

                    'BEObj.PerfilPersonal.PersonalId = BEObj.Id
                    'DALPerfilPersonal.Guardar(BEObj.PerfilPersonal)
                    If BEObj.PersonalPassword IsNot Nothing Then
                        With BEObj.PersonalPassword
                            .StatusType = BE.StatusType.Insert
                            .PersonalId = BEObj.Id
                        End With

                        DALPersonalPassword.Guardar(BEObj.PersonalPassword)
                    End If
                    BEObj.SupervisorPersonal.PersonalId = BEObj.Id
                    If BEObj.SupervisorPersonal.SupervisorId <> -1 Then

                        DALSupervisorPersonal.Guardar(BEObj.SupervisorPersonal)
                    End If


                    DALObject.Commit()
                Catch ex As Exception
                    DALObject.Rollback()
                    MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Finally
                    DALSupervisorPersonal.CloseConnection()
                    DALObject.CloseConnection()
                End Try
            Else
                MyBase.ErrorHandler(New BCException(Me.ErrorCollection), ErrorPolicy.BCNew)
            End If
        End Sub
        Public Sub Guardar(ByRef BEObj As List(Of SEC.Personal))
            Dim DALObject As DAL.Personal = Nothing
            Dim DALSupervisorPersonal As DAL.PersonalSupervisor = Nothing
            Me.ErrorCollection.Clear()

            Dim MapSupervisor As New Dictionary(Of String, Int32)
            'If Me.Validar(BEObj) Then
            Try
                DALObject = New DAL.Personal
                DALObject.OpenConnection()
                DALObject.Guardar(BEObj)
                'DALPerfilPersonal = New DAL.PerfilPersonal(True, CObj(DALObject.DBFactory), DALObject.Transaction)
                DALSupervisorPersonal = New DAL.PersonalSupervisor(True, CObj(DALObject.DBFactory), DALObject.Transaction)

                'Dim listSupervisor = DALObject.ListarCompleto
                For Each i In BEObj
                    MapSupervisor.Add(i.Nombre, i.Id)
                Next
                'BEObj.PerfilPersonal.PersonalId = BEObj.Id
                'DALPerfilPersonal.Guardar(BEObj.PerfilPersonal)
                For Each BEObj2 In BEObj
                    BEObj2.SupervisorPersonal.SupervisorId = If(MapSupervisor.ContainsKey(BEObj2.SupervisorPersonal.NombreSupervisor), MapSupervisor.Item(BEObj2.SupervisorPersonal.NombreSupervisor), -1)
                    BEObj2.SupervisorPersonal.PersonalId = BEObj2.Id
                    If BEObj2.SupervisorPersonal.SupervisorId > 0 Then
                        BEObj2.StatusType = BE.StatusType.Update
                        BEObj2.SupervisorId = BEObj2.SupervisorPersonal.SupervisorId
                        DALObject.Guardar(BEObj2)
                        DALSupervisorPersonal.Guardar(BEObj2.SupervisorPersonal)
                    End If

                Next

                DALObject.Commit()
            Catch ex As Exception
                DALObject.Rollback()
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            Finally
                DALSupervisorPersonal.CloseConnection()
                DALObject.CloseConnection()
            End Try
            'Else
            '    MyBase.ErrorHandler(New BCException(Me.ErrorCollection), ErrorPolicy.BCNew)
            'End If
        End Sub

        Public Sub Guardar3(ByRef BEObj As SEC.Personal)
            Dim DALObject As DAL.Personal = Nothing
            'Dim DALPerfilPersonal As DAL.PerfilPersonal = Nothing

            'Dim DALSupervisorPersonal As DAL.PersonalSupervisor = Nothing
            Me.ErrorCollection.Clear()

            If Me.Validar(BEObj) Then
                Try
                    DALObject = New DAL.Personal
                    DALObject.OpenConnection()
                    DALObject.Guardar(BEObj)
                    'DALPerfilPersonal = New DAL.PerfilPersonal(True, CObj(DALObject.DBFactory), DALObject.Transaction)

                    DALObject.Commit()
                Catch ex As Exception
                    DALObject.Rollback()
                    MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Finally
                    DALObject.CloseConnection()
                End Try
            Else
                MyBase.ErrorHandler(New BCException(Me.ErrorCollection), ErrorPolicy.BCNew)
            End If
        End Sub
        Public Sub Guardar4(ByRef BEObj As SEC.Personal)
            Dim DALObject As DAL.Personal = Nothing
            'Dim DALPerfilPersonal As DAL.PerfilPersonal = Nothing

            'Dim DALSupervisorPersonal As DAL.PersonalSupervisor = Nothing
            Me.ErrorCollection.Clear()

            If Me.Validar(BEObj) Then
                Try
                    DALObject = New DAL.Personal
                    DALObject.OpenConnection()
                    DALObject.Guardar2(BEObj)
                    'DALPerfilPersonal = New DAL.PerfilPersonal(True, CObj(DALObject.DBFactory), DALObject.Transaction)

                    DALObject.Commit()
                Catch ex As Exception
                    DALObject.Rollback()
                    MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Finally
                    DALObject.CloseConnection()
                End Try
            Else
                MyBase.ErrorHandler(New BCException(Me.ErrorCollection), ErrorPolicy.BCNew)
            End If
        End Sub
        Public Sub Guardar2(ByRef BEObj As SEC.Personal)
            Dim DALObject As DAL.Personal = Nothing

            Me.ErrorCollection.Clear()

            If Me.Validar(BEObj) Then
                Try
                    DALObject = New DAL.Personal
                    DALObject.OpenConnection()
                    DALObject.Guardar(BEObj)
                    DALObject.Commit()
                Catch ex As Exception
                    DALObject.Rollback()
                    MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Finally
                    DALObject.CloseConnection()
                End Try
            Else
                MyBase.ErrorHandler(New BCException(Me.ErrorCollection), ErrorPolicy.BCNew)
            End If
        End Sub

#End Region


#Region " Methods "

        ''' <summary>
        ''' 	Validates a business object before save it
        ''' </summary>
        ''' <param name="BEObj">Object Type User</param>
        ''' <returns>True: if object were validated</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function Validar(ByRef BEObj As SEC.Personal) As Boolean
            Dim bolOk As Boolean = True
            Dim beTemp As SEC.Personal
            Dim beTemp2 As SEC.Personal

            If BEObj.StatusType <> BE.StatusType.NoAction Then
                If BEObj.StatusType <> BE.StatusType.Insert Then
                    If BEObj.Id = 0 Then
                        MyBase.ErrorCollection.Add("No se ha proporcionado el Identificador")
                        bolOk = False
                    End If
                End If
                If BEObj.StatusType <> BE.StatusType.Delete Then
                    beTemp = Me.BuscarLogin(BEObj.Login)
                    If beTemp IsNot Nothing AndAlso BEObj.StatusType = BE.StatusType.Insert Then
                        MyBase.ErrorCollection.Add("El Login ya existe")
                        bolOk = False
                    ElseIf beTemp IsNot Nothing AndAlso BEObj.StatusType = BE.StatusType.Update AndAlso BEObj.Id <> beTemp.Id Then
                        MyBase.ErrorCollection.Add("El Login ya existe")
                        bolOk = False
                    Else
                        beTemp2 = BuscarRut(BEObj.Rut)
                        If beTemp2 IsNot Nothing AndAlso BEObj.StatusType = BE.StatusType.Insert Then
                            MyBase.ErrorCollection.Add("El Rut ya existe")
                            bolOk = False
                        ElseIf beTemp2 IsNot Nothing AndAlso BEObj.StatusType = BE.StatusType.Update AndAlso BEObj.Id <> beTemp.Id Then
                            MyBase.ErrorCollection.Add("El Rut ya existe")
                            bolOk = False
                        End If
                    End If
                    'beTemp = Me.Buscar(BEObj.Name)
                    'If beTemp IsNot Nothing AndAlso BEObj.StatusType = BE.StatusType.Insert Then
                    '    MyBase.ErrorCollection.Add("El Nombre ya existe")
                    '    bolOk = False
                    'ElseIf beTemp IsNot Nothing AndAlso BEObj.StatusType = BE.StatusType.Update AndAlso BEObj.Id <> beTemp.Id Then
                    '    MyBase.ErrorCollection.Add("El Nombre ya existe")
                    '    bolOk = False
                    'End If
                    'beTemp = Me.Buscar(BEObj.Email)
                    'If beTemp IsNot Nothing AndAlso BEObj.StatusType = BE.StatusType.Insert Then
                    '    MyBase.ErrorCollection.Add("El eMail ya existe")
                    '    bolOk = False
                    'ElseIf beTemp IsNot Nothing AndAlso BEObj.StatusType = BE.StatusType.Update AndAlso BEObj.Id <> beTemp.Id Then
                    '    MyBase.ErrorCollection.Add("El eMail ya existe")
                    '    bolOk = False
                    'End If
                End If
            End If

            Return bolOk
        End Function

        Public Function Count() As Int32
            Dim lngResult As Int32 = 0
            'Dim DALObject As DAL.Personal = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.Personal
                    lngResult = DALObject.Count()
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return lngResult
        End Function

        Public Function Count(ByVal Nombre As String, ByVal PerfilId As String, ByVal IdPosition As String, UnidadNegocioPadreId As Int32) As Int32
            'Dim lngResult As Long = 0

            Dim BECollection As Int32 = 0
            Me.ErrorCollection.Clear()
            Try
                Using DALObject As New DAL.Personal
                    BECollection = DALObject.Listar(Nombre, IdPosition, UnidadNegocioPadreId)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BECollection
            'Return lngResult
        End Function

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Buscar for collection business objects of type User
        ''' </summary>
        ''' <param name="IdProfile">Relationship Identifier</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>Object collection of type User</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function ListByProfile(ByVal IdProfile As Integer, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DAL.Personal
                    BECollection = DALObject.ListByProfile(IdProfile, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListByProfileAll(ByVal IdProfile As Integer, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DAL.Personal
                    BECollection = DALObject.ListByProfileAll(IdProfile, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListByProfileForSupervisor(ByVal IdSupervisor As Integer, ByVal IdProfile As Integer, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DAL.Personal
                    BECollection = DALObject.ListByProfileForSupervisor(IdSupervisor, IdProfile, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function



        ''' <summary>
        ''' 	Buscar for collection business objects of type User
        ''' </summary>
        ''' <param name="PositionId">Relationship Identifier</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>Object collection of type User</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function ListByPostion(ByVal PositionId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)
                'Dim Filter As BE.QueryFilter
                'Filter = New BE.QueryFilter(Function(x As SEC.Personal) x.PositionId, BE.QBOperator.Equal, PositionId)
                Using DALObject As New DAL.Personal
                    BECollection = DALObject.ListarPersonalPorCargo(PositionId)
                End Using
                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListByPostionUN(ByVal PositionId As Int32, ByVal UnidadNegocioPadreId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)
                'Dim Filter As BE.QueryFilter
                'Filter = New BE.QueryFilter(Function(x As SEC.Personal) x.PositionId, BE.QBOperator.Equal, PositionId)
                Using DALObject As New DAL.Personal
                    BECollection = DALObject.ListarPersonalPorCargoUN(PositionId, UnidadNegocioPadreId, Relations)
                End Using
                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function



        ''' <summary>
        ''' 	Buscar for collection business objects of type User
        ''' </summary>
        ''' <param name="PerfilId">Perfil del Personal</param>
        ''' <param name="Login">Login del Personal</param>
        ''' <param name="Nombre">Nombre del Personal</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>Object collection of type User</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Listar(ByVal Nombre As String, ByVal UnidadNegocioPadreId As Int32, ByVal CargoId As String, Login As String, PageIndex As Int32, PageSize As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim BECollection As List(Of SEC.Personal) = New List(Of SEC.Personal)
            'Dim DALObject As DAL.Personal = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.Personal
                    BECollection = DALObject.Listar(Nombre, UnidadNegocioPadreId, CargoId, Login, PageIndex, PageSize, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BECollection
        End Function

        ''' <summary>
        ''' 	Buscar for collection business objects of type User
        ''' </summary>
        ''' <param name="Order">Property column to specify collection order</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>Object collection of type User</returns>
        ''' <remarks>
        ''' 	To get relationship objects, suply relationship enumetators
        ''' </remarks>
        Public Function PagedList(ByVal Order As String, PageIndex As Integer, PageSize As Integer, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Try
                Dim BECollection As List(Of SEC.Personal)

                Using DALObject As New DAL.Personal
                    BECollection = DALObject.PagedList(Order, PageIndex, PageSize, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

#End Region

#Region " Buscar Methods "
        ''' <summary>
        ''' 	Buscar for business objects of type User
        ''' </summary>
        ''' <param name="Login">User's login</param>
        ''' <param name="Password">User's password</param>
        ''' <returns>An object of type User</returns>
        ''' <remarks>
        ''' 	Validate a user
        ''' </remarks>
        Public Function ValidateUser(ByVal Login As String, ByVal Password As String, ByVal Estado As Int32) As SEC.Personal
            Dim BEObject As SEC.Personal = Nothing

            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.ValidateUser(Login, Password, Estado)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function ValidarPersonal(ByVal Login As String, ByVal Password As String, ByVal Estado As Int32, IP As String, host As String) As SEC.Personal
            Dim BEObject As SEC.Personal = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.ValidarPersonal(Login, Password, Estado, IP, host)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function

        ''' <summary>
        ''' 	Buscar for business objects of type User
        ''' </summary>
        ''' <param name="Login">Object identifier User</param>
        ''' <param name="Relations">relationship enumetators</param>
        ''' <returns>An object of type User</returns>
        ''' <remarks>
        ''' 	To get relationship objects, suply relationship enumetators
        ''' </remarks>
        Public Function BuscarLogin(ByVal Login As String, ByVal ParamArray Relations() As [Enum]) As SEC.Personal
            Dim BEObject As SEC.Personal = Nothing

            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.Buscar(Login, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function BuscarRut(ByVal Rut As String, ByVal ParamArray Relations() As [Enum]) As SEC.Personal
            Dim BEObject As SEC.Personal = Nothing

            Try
                Using DALObject As New DAL.Personal
                    BEObject = DALObject.BuscarRut(Rut, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function BuscarPorId(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As SEC.Personal
            Dim BEObject As SEC.Personal = Nothing
            'Dim DALObject As DAL.Personal = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.Personal
                    BEObject = DALObject.BuscarPorId(Id, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function BuscarPorIdCargo(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As SEC.Personal
            Dim BEObject As SEC.Personal = Nothing
            'Dim DALObject As DAL.Personal = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.Personal
                    BEObject = DALObject.BuscarPorIdCargo(Id)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function BuscarPorIdCargoYSala(ByVal Id As Int32, ByVal SalaId As Int32, ByVal ParamArray Relations() As [Enum]) As SEC.Personal
            Dim BEObject As SEC.Personal = Nothing
            'Dim DALObject As DAL.Personal = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.Personal
                    BEObject = DALObject.BuscarPorIdCargoYSala(Id, SalaId)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function


#End Region

#Region " Constructors "
        ''' <summary>
        '''  Default Constructors
        ''' </summary>
        ''' <remarks>
        '''	</remarks>
        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

#End Region

    End Class

End Namespace
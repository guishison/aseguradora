Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BE = BEntities
Imports DAL = DALayer.Seguridad
Imports SEC = BEntities.Seguridad

Namespace Seguridad
    <Serializable>
    Public Class Plantilla
        Inherits BCEntity

#Region " Search Methods "

        ''' <summary>
        ''' 	Search for business objects of type Profile
        ''' </summary>
        ''' <param name="Id">Object identifier Profile</param>
        ''' <param name="Relations">relationship enumetators</param>
        ''' <returns>An object of type Profile</returns>
        ''' <remarks>
        ''' 	To get relationship objects, suply relationship enumetators
        ''' </remarks>
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As SEC.Plantilla
            Dim BEObject As SEC.Plantilla = Nothing

            Try
                Using DALObject As New DAL.Plantilla
                    BEObject = DALObject.Buscar(Id, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function existe(ByVal NombrePlantilla As String, ByVal ParamArray Relations() As [Enum]) As SEC.Plantilla
            Dim BEObject As SEC.Plantilla = Nothing

            Try
                Using DALObject As New DAL.Plantilla
                    BEObject = DALObject.Existe(NombrePlantilla, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Search for collection business objects of type Profile
        ''' </summary>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>Object collection of type Profile</returns>
        ''' <remarks>
        ''' 	To get relationship objects, suply relationship enumetators
        ''' </remarks>
        Public Function Listar(ByVal Nombre As String, ByVal UnidadNegocioPadreId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Plantilla)
            Try
                Dim BECollection As List(Of SEC.Plantilla)

                Using DALObject As New DAL.Plantilla
                    BECollection = DALObject.Listar(If(Nombre = "", "%%", String.Concat("%", Nombre, "%")), UnidadNegocioPadreId, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function ListFilter(PageIndex As Integer, PageSize As Integer, order As String, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Plantilla)
            Try
                Dim BECollection As List(Of SEC.Plantilla)
                Using DALObject As New DAL.Plantilla
                    BECollection = DALObject.PagedFilterList(PageIndex, PageSize, order, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function Count() As Long
            Dim lngResult As Long = 0

            Try
                Using DALObject As New DAL.Plantilla
                    lngResult = DALObject.Count()
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return lngResult
        End Function

        ''' <summary>
        ''' 	Search for collection business objects of type Profile
        ''' </summary>
        ''' <param name="IdMaster">Relationship Identifier</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>Object collection of type Profile</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Listar2(ByVal IdMaster As Long, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Plantilla)
            Try
                Dim BECollection As List(Of SEC.Plantilla)

                Using DALObject As New DAL.Plantilla
                    BECollection = DALObject.Listar(IdMaster, Relations)
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
        ''' <param name="BEObj">Object type Profile</param>    	
        ''' <remarks>
        ''' </remarks>
        Public Sub Guardar(ByRef BEObj As SEC.Plantilla)
            Dim DALObject As DAL.Plantilla = Nothing

            Me.ErrorCollection.Clear()

            If Me.Validar(BEObj) Then
                Try
                    DALObject = New DAL.Plantilla
                    DALObject.OpenConnection()
                    DALObject.Guardar(BEObj)
                    'DALObject.GuardarFormulario(BEObj.ColeccionFormulario, BEObj.Id)
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


        ''' <summary>
        ''' 	Validates a business object before save it 
        ''' </summary>
        ''' <param name="BEObj">Object Type Profile</param>
        ''' <returns>True: if object were validated</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function Validar(ByRef BEObj As SEC.Plantilla) As Boolean
            Dim bolOk As Boolean = True

            If BEObj.StatusType <> BE.StatusType.NoAction Then
                If BEObj.StatusType <> BE.StatusType.Insert Then
                    If BEObj.Id = 0 Then
                        MyBase.ErrorCollection.Add("No se ha proporcionado el Identificador")
                        bolOk = False
                    End If
                End If
            End If

            Return bolOk
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

#End Region

    End Class

End Namespace
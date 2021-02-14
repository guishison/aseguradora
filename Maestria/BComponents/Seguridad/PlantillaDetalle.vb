
Option Strict On
Option Compare Text
Option Explicit On
Option Infer On
Imports DAL = DALayer.Seguridad
Imports SEC = BEntities.Seguridad

Namespace Seguridad

    <Serializable>
    Public Class PlantillaDetalle
        Inherits BCEntity

#Region " Search Methods "

        ''' <summary>
        ''' 	Search for business objects of type Module
        ''' </summary>
        ''' <param name="Id">Object identifier Module</param>
        ''' <param name="Relaciones">Relacioneship enumetators</param>
        ''' <returns>An object of type Module</returns>
        ''' <remarks>
        ''' 	To get Relacioneship objects, suply Relacioneship enumetators
        ''' </remarks>
        Public Function Buscar(ByVal Id As Long, ByVal ParamArray Relaciones() As [Enum]) As SEC.PlantillaDetalle
            Dim BEObject As SEC.PlantillaDetalle = Nothing
            Try
                Using DALObject As New DAL.PlantillaDetalle
                    BEObject = DALObject.Buscar(Id, Relaciones)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function


        Public Function BuscarPlantillaDetallePorPersonal(ByVal PlantillaId As Int32, ByVal FormularioId As Int32, ByVal UnidadNegocioId As Int32, ByVal ParamArray Relaciones() As [Enum]) As SEC.PlantillaDetalle
            Dim BEObject As SEC.PlantillaDetalle = Nothing
            Try
                Using DALObject As New DAL.PlantillaDetalle
                    BEObject = DALObject.BuscarPrivilegioPorPersonal(PlantillaId, FormularioId, UnidadNegocioId, Relaciones)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Search for collection business objects of type Module
        ''' </summary>
        ''' <param name="Order">Property column to specify collection order</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>Object collection of type Module</returns>
        ''' <remarks>
        ''' 	To get Relacioneship objects, suply Relacioneship enumetators
        ''' </remarks>
        Public Function Listar(ByVal Order As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.PlantillaDetalle)
            Try
                Dim BECollection As List(Of SEC.PlantillaDetalle)

                Using DALObject As New DAL.PlantillaDetalle
                    BECollection = DALObject.Listar(Order, Relaciones)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' 	Search for collection business objects of type Module
        ''' </summary>
        ''' <param name="IdMaster">Relacioneship Identifier</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>Object collection of type Module</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Listar(ByVal IdMaster As Long, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.PlantillaDetalle)
            Try
                Dim BECollection As List(Of SEC.PlantillaDetalle)

                Using DALObject As New DAL.PlantillaDetalle
                    BECollection = DALObject.Listar(IdMaster, Relaciones)
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
        ''' 	Validates a business object before save it 
        ''' </summary>
        ''' <param name="BEObj">Object Type Module</param>
        ''' <remarks>
        ''' </remarks>



        Public Sub Guardar(ByRef BEObj As List(Of SEC.PlantillaDetalle))
            Dim DALObject As DAL.PlantillaDetalle = Nothing
            Me.ErrorCollection.Clear()

            Try
                DALObject = New DAL.PlantillaDetalle
                DALObject.OpenConnection()
                DALObject.Guardar(BEObj)
                DALObject.Commit()
            Catch ex As Exception
                DALObject.Rollback()
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            Finally
                DALObject.CloseConnection()
            End Try
        End Sub


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
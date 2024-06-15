Imports Entidades.Entidades
Imports DAL.DAL

Namespace BLL
    'capa de negocios
    Public Class ClientesBLL
        Public Function ListarClientes() As List(Of Cliente)
            Dim clientesDAL As New clientesDAL()
            Return clientesDAL.Listar()
        End Function

        Public Function AgregarModificarCliente(nuevoCliente As Cliente, ID As Integer?) As Boolean
            Dim clientesDAL As New clientesDAL()
            Return clientesDAL.AgregarModificar(nuevoCliente, ID)
        End Function
        Public Function BuscarClientePorID(id As Integer) As Cliente
            Dim clientesDAL As New clientesDAL()
            Return clientesDAL.BuscarID(id)
        End Function
        Public Function EliminarCliente(ID As Integer?) As Boolean
            Dim clientesDAL As New clientesDAL()
            Return clientesDAL.Eliminar(ID)
        End Function
        Public Function BuscarClientesPorFiltro(nombre As String, telefono As String) As List(Of Cliente)
            Dim clientesDAL As New clientesDAL()
            Return clientesDAL.BuscarClientesPorFiltro(nombre, telefono)
        End Function
    End Class
End Namespace

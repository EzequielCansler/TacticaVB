Imports Entidades.Entidades
Imports DAL.DAL

Namespace BLL
    Public Class ProductosBLL
        Public Function ListarProductos() As List(Of Producto)
            Dim productosDAL As New productosDAL()
            Return productosDAL.Listar()
        End Function
        Public Function AgregarModificarProducto(nuevoProducto As Producto, ID As Integer?) As Boolean
            Dim productosDAL As New ProductosDAL()

            Return productosDAL.AgregarModificarProdu(nuevoProducto, ID)
        End Function
        Public Function BuscarProductoPorID(ID As Integer) As Producto
            Dim productosDAL As New ProductosDAL()
            Return productosDAL.BuscarID(ID)
        End Function
        Public Function EliminarProducto(ID As Integer?) As Boolean
            Dim productosDAL As New ProductosDAL()

            Return productosDAL.Eliminar(ID)
        End Function
        Public Function BuscarProductosPorFiltro(nombre As String, categoria As String) As List(Of Producto)
            Dim productosDAL As New ProductosDAL()
            Return productosDAL.BuscarProductosPorFiltro(nombre, categoria)
        End Function
    End Class
End Namespace
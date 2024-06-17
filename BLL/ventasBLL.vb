Imports Entidades.Entidades
Imports DAL.DAL


Namespace BLL
    Public Class ventasBLL
        Public Function Listar() As List(Of Venta)
            Dim ventasDAL As New VentasDAL()
            Dim ventas = ventasDAL.Listar()
            For Each venta In ventas
                Dim sumaTotal = 0
                Dim SumaCantidad = 0
                For Each item In venta.Items
                    sumaTotal += item.PrecioUnitario
                    SumaCantidad += item.Cantidad

                Next
                venta.Total = sumaTotal
                venta.CantidadTotal = SumaCantidad
            Next
            Return ventas
        End Function
        Public Function AgregarVenta(items As List(Of VentaItem), clienteId As Integer, ID As Integer?) As Boolean
            Try
                Dim Fecha = DateTime.Now
                Dim PrecioTotal As Double = 0

                Dim ventasDAL As New VentasDAL()
                Dim productosDAL As New ProductosDAL()
                For Each item As VentaItem In items
                    Dim producto As Producto = productosDAL.BuscarID(item.IDProducto)
                    If producto IsNot Nothing Then
                        Dim PrecioUnitario = producto.Precio
                        Dim TotalDeProductos = item.Cantidad * producto.Precio
                        PrecioTotal += TotalDeProductos
                        item.PrecioTotal = TotalDeProductos
                        item.PrecioUnitario = PrecioUnitario


                    End If

                Next
                Dim Resultado As Boolean = ventasDAL.AgregarVenta(items, Fecha, PrecioTotal, clienteId, ID)


                Return Resultado

            Catch ex As Exception

                Return False
            End Try
        End Function

        Public Function BuscarPorID(id As Integer) As Venta
            Dim ventaDAL As New VentasDAL()
            Dim venta = ventaDAL.BuscarID(id)
            venta.Items = ventaDAL.ItemDetallePorVentaID(id)
            Return venta
        End Function
        Public Function Eliminar(id As Integer) As Boolean
            Dim ventaDAL As New VentasDAL()
            Dim exito = ventaDAL.EliminarVenta(id)

            Return exito
        End Function
        Public Function BuscarPorCliente(nombre As String) As List(Of Venta)
            Dim ventaDAL As New VentasDAL()
            Return ventaDAL.BuscarVentaPorNombreDeCliente(nombre)
        End Function

    End Class
End Namespace


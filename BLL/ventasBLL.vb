Imports Entidades.Entidades
Imports DAL.DAL


Namespace BLL
    Public Class ventasBLL
        Public Function AgregarVenta(items As List(Of VentaItem), clienteId As Integer) As Boolean
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
                ventasDAL.AgregarVenta(items, Fecha, PrecioTotal, clienteId)

                'Return ventasDAL.AgregarVenta()


            Catch ex As Exception

                Return False
            End Try
        End Function
    End Class
End Namespace


Imports System.Web.Mvc
Imports BLL.BLL
Imports Entidades.Entidades
Imports Newtonsoft.Json
Imports TacticaVB.Util

Namespace Controllers
    Public Class VentaController


        Inherits Controller

        ' GET: Venta
        Function Index(Optional nombre As String = "") As ActionResult
            Dim ventasBLL As New ventasBLL()
            Dim clientesBLL As New ClientesBLL()
            Dim Ventas As List(Of Venta)
            Dim clientes = clientesBLL.ListarClientes()
            If String.IsNullOrEmpty(nombre) Then
                ventas = ventasBLL.Listar()
            Else
                ventas = ventasBLL.BuscarPorCliente(nombre)
            End If

            ViewBag.Clientes = clientes
            Return View(ventas)
        End Function
        Function Detalles(id As Integer)
            Dim ventasBLL As New ventasBLL()
            Dim clientesBLL As New ClientesBLL()

            Dim venta = ventasBLL.BuscarPorID(id)
            Dim clientes = clientesBLL.BuscarClientePorID(venta.IDCliente)

            ViewBag.Clientes = clientes
            Return View(venta)
        End Function
        Function AgregarVenta(Optional id As Integer? = Nothing) As ActionResult
            Dim productosBLL As New ProductosBLL()
            Dim productos = productosBLL.ListarProductos()

            Dim clientesBLL As New ClientesBLL()
            Dim clientes = clientesBLL.ListarClientes()

            Dim ventasBLL As New ventasBLL()
            Dim ItemDetalle As ItemDetalle = Nothing
            Dim venta As New Venta()

            If id.HasValue Then

                venta = ventasBLL.BuscarPorID(id.Value)
                If venta Is Nothing Then
                    ViewBag.Message = "Cliente no encontrado."
                End If
            End If


            ViewBag.Productos = productos
            ViewBag.Clientes = clientes
            Return View(venta)
        End Function
        <HttpPost>
        Public Function CrearVenta(VentaItem As String, IDCliente As Integer, Optional VentaId As Integer? = Nothing) As ActionResult
            Try
                Dim productos As List(Of VentaItemJSON) = JsonConvert.DeserializeObject(Of List(Of VentaItemJSON))(VentaItem)
                Dim bll As New ventasBLL()
                Dim itemsVenta As New List(Of VentaItem)()
                For Each itemJSON In productos
                    itemsVenta.Add(Convertidor.ConvertirAVentaItem(itemJSON)) 'cambio de VentaItemJSON a VentaItem

                Next
                Dim resultado As Boolean = bll.AgregarVenta(itemsVenta, IDCliente, VentaId)


                Return RedirectToAction("Index")
            Catch ex As Exception
                Return RedirectToAction("Index")
            End Try
        End Function
        Public Function EliminarVenta(id As Integer) As ActionResult
            Dim ventaBLL As New ventasBLL()
            ventaBLL.Eliminar(id)

            Return RedirectToAction("Index")
        End Function
    End Class
End Namespace
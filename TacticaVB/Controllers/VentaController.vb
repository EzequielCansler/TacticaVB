Imports System.Web.Mvc
Imports BLL.BLL
Imports Entidades.Entidades
Imports Newtonsoft.Json
Imports TacticaVB.Util

Namespace Controllers
    Public Class VentaController


        Inherits Controller

        ' GET: Venta
        Function Index() As ActionResult
            Dim ventasBLL As New ventasBLL()
            Dim ventasitemBLL As New ventasitemsBLL()
            Dim clientesBLL As New ClientesBLL()

            Dim clientes = clientesBLL.ListarClientes()
            Dim ventaItem = ventasitemBLL.ListarItems()
            Dim ventas = ventasBLL.Listar()

            ViewBag.Clientes = clientes
            ViewBag.VentaItem = ventaItem
            Return View(ventas)
        End Function

        Function AgregarVenta() As ActionResult
            Dim productosBLL As New ProductosBLL()
            Dim productos = productosBLL.ListarProductos()
            Dim clientesBLL As New ClientesBLL()
            Dim clientes = clientesBLL.ListarClientes()

            If clientes Is Nothing Then
                clientes = New List(Of Cliente)()
            End If

            ViewBag.Clientes = clientes
            Return View(productos)
        End Function
        <HttpPost>
        Public Function CrearVenta(VentaItem As String, clienteID As Integer) As ActionResult
            Try
                Dim productos As List(Of VentaItemJSON) = JsonConvert.DeserializeObject(Of List(Of VentaItemJSON))(VentaItem)
                Dim bll As New ventasBLL()
                Dim itemsVenta As New List(Of VentaItem)()
                For Each itemJSON In productos
                    itemsVenta.Add(Convertidor.ConvertirAVentaItem(itemJSON)) 'cambio de VentaItemJSON a VentaItem

                Next
                Dim resultado As Boolean = bll.AgregarVenta(itemsVenta, clienteID)


                Return RedirectToAction("Index")
            Catch ex As Exception
                Return RedirectToAction("Index")
            End Try
        End Function
    End Class
End Namespace
Imports BLL.BLL
Imports Entidades.Entidades

Namespace Controllers
    Public Class ProductoController
        Inherits Controller

        ' GET: Producto
        Function Index(Optional nombre As String = "", Optional categoria As String = "") As ActionResult
            Dim productosBLL As New ProductosBLL()
            Dim productos = productosBLL.ListarProductos()


            If Not String.IsNullOrEmpty(nombre) Then
                productos = productos.Where(Function(c) c.NombreProducto.ToLower().Contains(nombre.ToLower())).ToList()
            End If

            If Not String.IsNullOrEmpty(categoria) Then
                productos = productos.Where(Function(c) c.Categoria.Contains(categoria)).ToList()
            End If

            Return View(productos)
        End Function

        Function ProductoCambioNuevo(Optional ID As Integer? = Nothing) As ActionResult
            Dim producto As New Producto()

            If ID.HasValue Then
                Dim productosBLL As New ProductosBLL()
                producto = productosBLL.BuscarProductoPorID(ID.Value)

                If producto Is Nothing Then
                    ViewBag.Message = "Producto no encontrado."
                End If
            End If

            Return View(producto)
        End Function

        <HttpPost>
        Function AgregarModificarProdu(producto As Producto) As ActionResult
            Dim productoDAL As New ProductosBLL()
            Dim exito = productoDAL.AgregarModificarProducto(producto, producto.ID)

            If exito Then
                Return RedirectToAction("Index")
            Else
                ' Manejar el caso de error aquí
                ViewBag.Message = "Error al agregar/modificar el producto."
                Return View("ProductoCambioNuevo", ProductoCambioNuevo) ' Retornar la vista con el cliente para corregir los datos
            End If
        End Function
        Function BuscarPorID(id As Integer) As ActionResult
            Dim productosBLL As New ProductosBLL()
            Dim producto As Producto = productosBLL.BuscarProductoPorID(id)
            If producto IsNot Nothing Then
                Return View(producto)
            Else
                ViewBag.Message = "Cliente no encontrado."
                Return View()
            End If
        End Function

        Function EliminarProducto(ID As Integer) As ActionResult
            Dim productosDAL As New ProductosBLL()

            Dim exito = productosDAL.EliminarProducto(ID)

            Return RedirectToAction("Index")

        End Function

    End Class
End Namespace

Imports BLL.BLL
Imports Entidades.Entidades

Namespace Controllers
    Public Class ClienteController
        Inherits Controller


        Function Index(Optional nombre As String = "", Optional telefono As String = "") As ActionResult
            Dim clientesBLL = New ClientesBLL()
            Dim clientes = clientesBLL.ListarClientes()


            If Not String.IsNullOrEmpty(nombre) Then
                clientes = clientes.Where(Function(c) c.NombreCliente.ToLower().Contains(nombre.ToLower())).ToList()
            End If

            If Not String.IsNullOrEmpty(telefono) Then
                clientes = clientes.Where(Function(c) c.Telefono.Contains(telefono)).ToList()
            End If

            Return View(clientes)
        End Function
        Function ClienteCambioNuevo(Optional id As Integer? = Nothing) As ActionResult
            Dim cliente As New Cliente()

            If id.HasValue Then
                Dim clientesBLL As New ClientesBLL()
                cliente = clientesBLL.BuscarClientePorID(id.Value)

                If cliente Is Nothing Then
                    ViewBag.Message = "Cliente no encontrado."
                End If
            End If

            Return View(cliente)
        End Function

        <HttpPost>
        Function AgregarModificar(cliente As Cliente) As ActionResult
            Dim clientesDal = New ClientesBLL()
            Dim exito = clientesDal.AgregarModificarCliente(cliente, cliente.ID)

            ' Redirigir a la acción Index si la operación fue exitosa
            If exito Then
                Return RedirectToAction("Index")
            Else
                ' Manejar el caso de error aquí
                ViewBag.Message = "Error al agregar/modificar el cliente."
                Return View("ClienteCambioNuevo", ClienteCambioNuevo) ' Retornar la vista con el cliente para corregir los datos
            End If
        End Function
        Function EliminarCliente(ID As Integer) As ActionResult
            Dim clientesDal = New ClientesBLL()
            Dim exito = clientesDal.EliminarCliente(ID)

            Return RedirectToAction("Index")

        End Function

        Function BuscarPorID(id As Integer) As ActionResult
            Dim clientesBLL As New ClientesBLL()
            Dim cliente As Cliente = clientesBLL.BuscarClientePorID(id)
            If cliente IsNot Nothing Then
                Return View(cliente)
            Else
                ViewBag.Message = "Cliente no encontrado."
                Return View()
            End If
        End Function
    End Class
End Namespace
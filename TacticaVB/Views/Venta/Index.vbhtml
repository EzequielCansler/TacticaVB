@Imports Entidades.Entidades
@ModelType List(Of Venta)

@Code
    ViewData("Title") = "Ventas"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Ventas Existentes</h2>

<table id="example" class="table table-striped" style="width:100%">
    <thead>
        <tr>
            <th>Cliente</th>
            <th>Cantidad</th>
            <th>Fecha</th>
            <th>Total</th>
        </tr>
    </thead>
    <tbody>
        @For Each venta As Venta In Model
            @<tr>
                <td>
                    @If ViewBag.Clientes IsNot Nothing Then
                        @For Each cliente In ViewBag.Clientes
                            @If cliente.ID = venta.IDCliente Then
                                @<span>@cliente.NombreCliente</span>
                            End If
                        Next
                    End If
                </td>
                <td>
                    @If ViewBag.VentaItem IsNot Nothing Then
                        @For Each item In ViewBag.VentaItem
                            @If item.IDVenta = venta.ID Then
                                @<span>@item.Cantidad</span>
                            End If
                        Next
                    End If
                </td>
                <td>@venta.Fecha.ToShortDateString()</td>
                <td>@venta.Total</td>
                @Using Html.BeginForm("AgregarVenta", "Venta", New With {.id = venta.ID}, FormMethod.Post)
                    @<td><button type="submit" class="btn btn-primary">Modificar Venta</button></td>
                End Using
                @Using Html.BeginForm("Eliminar", "Venta", New With {.id = venta.ID}, FormMethod.Post)
                    @<td><button type="submit" class="btn btn-primary">Eliminar Venta</button></td>
                End Using
            </tr>
        Next
    </tbody>
</table>

<a href="@Url.Action("AgregarVenta", "Venta")" class="btn btn-primary">Agregar Venta</a>

@Imports Entidades.Entidades
@ModelType List(Of Venta)

@Code
    ViewData("Title") = "Ventas"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Ventas Existentes</h2>
@Using Html.BeginForm("Index", "Venta", FormMethod.Get)
    @<div class="form-group">
        <Label for="nombre">Nombre</Label>
        <input type="text" id="nombre" name="nombre" class="form-control" placeholder="Ingrese el nombre del cliente" value="@Request("nombre")" />
    </div>
    @<Button type="submit" class="btn btn-primary">Buscar</Button>End Using

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
                    <span>@venta.CantidadTotal</span>
                </td>
                <td>@venta.Fecha.ToShortDateString()</td>
                <td>@venta.Total</td>
                @Using Html.BeginForm("AgregarVenta", "Venta", New With {.id = venta.ID}, FormMethod.Post)
                    @<td><button type="submit" class="btn btn-primary">Modificar Venta</button></td>
                End Using
                @Using Html.BeginForm("EliminarVenta", "Venta", New With {.id = venta.ID}, FormMethod.Post)
                    @<td><button type="submit" class="btn btn-primary">Eliminar Venta</button></td>
                End Using
            </tr>
        Next
    </tbody>
</table>

<a href="@Url.Action("AgregarVenta", "Venta")" class="btn btn-primary">Agregar Venta</a>

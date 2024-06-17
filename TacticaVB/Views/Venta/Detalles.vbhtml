@Imports Entidades.Entidades
@ModelType Venta

@Code
    ViewData("Title") = "Detalles"
End Code

<h2>Detalles de Venta</h2>

<h3>Cliente: @ViewBag.Clientes.NombreCliente</h3>
<p>@Model.Fecha</p>


<table id="productosSeleccionados" class="table">
    <thead>
        <tr>
            <th>Nombre De Producto</th>
            <th>Cantidad</th>
            <th>Precio Unitario</th>
        </tr>
    </thead>
    <tbody>
            @For Each item As VentaItem In Model.Items
        @<tr>
                <td>@item.NombreProducto</td>
                <td>@item.Cantidad</td>
                <td>@item.PrecioUnitario</td>
        </tr>
            Next
    </tbody>
</table>
<div>
    <p>@Model.Total</p>

</div>


<a href="@Url.Action("Index", "Venta")" class="btn btn-primary">Volver</a>

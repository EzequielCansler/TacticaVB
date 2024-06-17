@Imports Newtonsoft.Json
@Imports Entidades.Entidades
@ModelType Venta

@Code
    ViewData("Title") = "Crear Venta"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Crear Venta</h2>

<form id="ventaForm" action="@Url.Action("CrearVenta", "Venta")" method="post">
    <input hidden id="Venta-Item" name="VentaItem" />
    <input hidden id="Venta-id" name="VentaId" value="@Model.ID" />

    <div class="form-group">
        <label for="clienteID">Cliente</label>

        @Html.DropDownListFor(Function(m) m.IDCliente, New SelectList(DirectCast(ViewBag.Clientes, List(Of Cliente)), "Id", "NombreCliente"), "Selecciona una opción", New With {.id = "clienteID", .name = "clienteID", .class = "form-control"})


    </div>

    <h3>Productos</h3>
    <div id="productosContainer">
        <div class="form-group">
            <label for="productoID">Producto</label>
            <select id="productoID" class="form-control">
                @For Each producto As Producto In ViewBag.Productos
                    @<option value="@producto.ID">@producto.NombreProducto</option>
                Next
            </select>
        </div>
        <div class="form-group">
            <label for="cantidad">Cantidad</label>
            <input type="number" id="cantidad" class="form-control" placeholder="Ingrese la cantidad" />
        </div>
        <button type="button" id="addProducto" class="btn btn-primary">Agregar Producto</button>
    </div>

    <h3>Productos Seleccionados</h3>
    <table id="productosSeleccionados" class="table">
        <thead>
            <tr>
                <th>Producto ID</th>
                <th>Nombre Producto</th>
                <th>Cantidad</th>
                <th>Acción</th>
            </tr>
        </thead>
        <tbody>

            @For Each item As VentaItem In Model.Items
                @<tr>
                    <td>@item.IDProducto</td>
                    <td>@item.NombreProducto</td>
                    <td>@item.Cantidad</td>
                    <td>
                        <button type="button" class="btn btn-danger removeProducto" data-id="@item.ID">Eliminar</button>
                    </td>
                </tr>
            Next

        </tbody>
    </table>

    <button type="submit" id="submitVenta" class="btn btn-success">Crear Venta</button>
</form>

@section Scripts
    {
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            var productos = [];

            $('#addProducto').click(function () {
                var productoID = $('#productoID').val();
                var cantidad = $('#cantidad').val();
                var productoNombre = $('#productoID option:selected').text();

                if (productoID && cantidad > 0) {
                    // Agregar el producto al arreglo de productos
                    productos.push({ ProductoID: productoID, Cantidad: cantidad, Id: 0});

                    // Mostrar el producto en la tabla de productos seleccionados
                    $('#productosSeleccionados tbody').append('<tr><td>' + productoID + '</td><td>' + productoNombre + '</td><td>' + cantidad + '</td><td><button type="button" class="btn btn-danger removeProducto">Eliminar</button></td></tr>');

                    // Limpiar los campos de selección de producto y cantidad
                    $('#productoID').val('');
                    $('#cantidad').val('');
                }
            });

            $(document).on('click', '.removeProducto', function () {
                var row = $(this).closest('tr');
                var productoID = row.find('td:first').text();
                var Id = String($(this).data('id'));

             
                productos.push({ ProductoID: productoID, Cantidad: 0, Id: Id });

                // Eliminar la fila de la tabla de productos seleccionados
                row.remove();
            });

            $('#ventaForm').submit(function () {
                var clienteID = $('#clienteID').val();
                
                $('#Venta-Item').val(JSON.stringify(productos));
            });
        });
    </script>
    }

End Section
@Imports Entidades.Entidades
@ModelType List(Of Producto)

@Code
    ViewData("Title") = "Index"
End Code

<h2>Productos</h2>
@Using Html.BeginForm("Index", "Producto", FormMethod.Get)
    @<div class="form-group">
        <Label for="nombre">Nombre</Label>
        <input type="text" id="nombre" name="nombre" class="form-control" placeholder="Ingrese el nombre del producto" value="@Request("nombre")" />
    </div>
    @<div class="form-group">
         <Label for="categoria">Categoria</Label>
        <input type="text" id="categoria" name="categoria" class="form-control" placeholder="Ingrese la categoria del producto" value="@Request("categoria")" />
    </div>
    @<Button type="submit" class="btn btn-primary">Buscar</Button>
End Using

<table id="example" class="table table-striped" style="width:100%">
    <thead>
        <tr>
            <th>Id</th>
            <th>Nombre</th>
            <th>Precio</th>
            <th>Categoria</th>
        </tr>
    </thead>
    <tbody>
        @For Each producto As Producto In Model
            @<tr>
                <td>@producto.ID</td>
                <td>@producto.NombreProducto</td>
                <td>@producto.Precio</td>
                <td>@producto.Categoria</td>
                @Using Html.BeginForm("ProductoCambioNuevo", "Producto", New With {.id = producto.ID}, FormMethod.Post)
                    @<td><button type="submit" class="btn btn-primary">Modificar Cliente</button></td>
                End Using
                <td>
                    @Using Html.BeginForm("EliminarProducto", "Producto", New With {.id = producto.ID}, FormMethod.Post)
                        @<button type="submit" class="btn btn-primary">Eliminar Cliente</button>
                    End Using
                </td>
            </tr>
        Next
    </tbody>
</table>

<a href="@Url.Action("ProductoCambioNuevo", "Producto")" class="btn btn-primary">Agregar Producto</a>


@Code
    ViewData("Title") = "Agregar o Modificar"
End Code

<h2>Agregar-Modificar</h2>

@Using Html.BeginForm("AgregarModificarProdu", "Producto", FormMethod.Post)
    @<input type="hidden" name="ID" value="@Model.ID" />
    @<div Class="form-group">
        <Label for="NombreProducto">Nombre</Label>
        <input type="text" id="NombreProducto" name="NombreProducto" Class="form-control" placeholder="Ingrese el nombre del producto" value="@Model.NombreProducto" required />
    </div>
    @<div Class="form-group">
        <Label for="Precio">Precio</Label>
        <input type="number" id="Precio" name="Precio" Class="form-control" placeholder="Ingrese el precio del producto" value="@Model.Precio" required />
    </div>
    @<div Class="form-group">
        <Label for="Categoria">Categoria</Label>
        <input type="text" id="Categoria" name="Categoria" Class="form-control" placeholder="Ingrese la cateogoria del cliente" value="@Model.Categoria" required />
    </div>
    @<Button type="submit" Class="btn btn-primary">Guardar Producto</Button>
End Using
<div>

<a href="@Url.Action("Index", "Producto")" class="btn btn-primary">Cancelar</a>
</div>

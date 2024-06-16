@Code
    ViewData("Title") = "Home Page"
End Code
<h1>Pureba tecnica</h1>
<a href="@Url.Action("Index", "Cliente")" class="btn btn-primary">Clientes</a>
<a href="@Url.Action("Index", "Producto")" class="btn btn-primary">Productos</a>
<a href="@Url.Action("Index", "Venta")" class="btn btn-primary">Ventas</a>


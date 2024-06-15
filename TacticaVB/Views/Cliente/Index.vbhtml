@Imports Entidades.Entidades
@ModelType List(Of Cliente)

@Code
    ViewData("Title") = "Index"
End Code

<h2>Clientes</h2>

@Using Html.BeginForm("Index", "Cliente", FormMethod.Get)
@<div class="form-group">
<Label for="nombre">Nombre</Label>
<input type = "text" id="nombre" name="nombre" class="form-control" placeholder="Ingrese el nombre del cliente" value="@Request("nombre")" />
</div>
@<div class="form-group">
<Label for="telefono">Teléfono</Label>
<input type = "text" id="telefono" name="telefono" class="form-control" placeholder="Ingrese el teléfono del cliente" value="@Request("telefono")" />
</div>
@<Button type = "submit" class="btn btn-primary">Buscar</Button>
End Using

<table id="example" class="table table-striped" style="width:100%">
    <thead>
        <tr>
            <th>Id</th>
            <th>Nombre</th>
            <th>Telefono</th>
            <th>Correo</th>
        </tr>
    </thead>
    <tbody>
        @For Each cliente As Cliente In Model
@<tr>
    <td>@cliente.ID</td>
    <td>@cliente.NombreCliente</td>
    <td>@cliente.Telefono</td>
    <td>@cliente.Correo</td>
    @Using Html.BeginForm("ClienteCambioNuevo", "Cliente", New With {.id = cliente.ID}, FormMethod.Post)
                    @<td><button type="submit" class="btn btn-primary">Modificar Cliente</button></td>
    End Using
    <td>
        @Using Html.BeginForm("EliminarCliente", "Cliente", New With {.id = cliente.ID}, FormMethod.Post)
        @<button type="submit" class="btn btn-primary">Eliminar Cliente</button>
        End Using
    </td>
</tr>
        Next
    </tbody>
</table>

<a href="@Url.Action("ClienteCambioNuevo", "Cliente")" class="btn btn-primary">Agregar Cliente</a>

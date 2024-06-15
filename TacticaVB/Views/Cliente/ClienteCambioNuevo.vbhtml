
@Code
    ViewData("Title") = "Agregar o Modificar"
End Code

<h2>Agregar-Modificar</h2>

@Using Html.BeginForm("AgregarModificar", "Cliente", FormMethod.Post)
    @<input type="hidden" name="ID" value="@Model.ID" />
    @<div Class="form-group">
        <Label for="NombreCliente">Cliente</Label>
        <input type="text" id="NombreCliente" name="NombreCliente" Class="form-control" placeholder="Ingrese el nombre del cliente" value="@Model.NombreCliente" required />
    </div>
    @<div Class="form-group">
        <Label for="Telefono">Telefono</Label>
        <input type="text" id="Telefono" name="Telefono" Class="form-control" placeholder="Ingrese el telefono del cliente" value="@Model.Telefono" required />
    </div>
    @<div Class="form-group">
        <Label for="Correo">Correo</Label>
        <input type="email" id="Correo" name="Correo" Class="form-control" placeholder="Ingrese el correo del cliente" value="@Model.Correo" required />
    </div>
    @<Button type="submit" Class="btn btn-primary">Guardar Cliente</Button>
End Using

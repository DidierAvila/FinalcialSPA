//Variable que alamcena los proveedores
var listaProveedor;
//Variable que alamcena las Orden por proveedor
var listaOrdenCompraProveedor;
//Variable que alamcena los proveedores
var dtProveedores;
//Variable que alamcena una orden de compra
var ConsultaOrdenCompra;
//Objeto sobre el cual se procesará la tarea
var objTarea;


$(document).ready(function () {
    CargarListaCamposProveedores();
    GenerarConsecutivoOrdenCompra();
    $("#Proveedor").change(CargarListaOrdenCompraProveedores);
    $("#BuscarProveedor").click();
    $("#BtnBuscarOrdenC").click(ConsultarOrdenCompraXid);

    $("#DivInformativo").show();
    $("#PageBody").hide();

    $("#BtnEntrada").click(function () {
        $("#DivOrdenCompra").hide();
        $("#DivProveedores").hide();
        $("#PageBody").show();
        $("#DivEntradaAlmacen").show();
    });

    $("#BtnProveedores").click(function () {
        $("#PageBody").show();
        $("#DivOrdenCompra").hide();
        $("#DivProveedores").show();
        $("#DivEntradaAlmacen").hide();
        ConsultarProveedoresXnombre();
    });

    $("#BtnOrdenCompra").click(function () {
        $("#PageBody").show();
        $("#DivOrdenCompra").show();
        $("#DivProveedores").hide();
        $("#DivEntradaAlmacen").hide();
    });

});

//Carga la lista de los proveedores
function CargarListaCamposProveedores() {

    $.ajax({
        url: "../Controllers/MainAplication.ashx?op=200",
        method: "POST",
        dataType: "json",
        async: true,
        success: function (result) {
            if (result.toString().indexOf("ERROR") == -1) {

                listaProveedor = result;

                $("#Proveedor").empty();

                $.each(listaProveedor, function () {
                    $("#Proveedor").append($("<option> " +
                    "</option>").val(this["ID"]).html(this["COLUMN_NAME"]));
                });

                $("#Proveedor").val($($(objTarea).children()[3]).html());

                //Se carga la lista de orden de compra
                //CargarListaOrdenCompraProveedores();
            } else {
                //$("#lblError").show();
            }
        },
        error: function (result) {
            $("#lblError").show();
        }
    });
};

//Carga la lista orden de compra por proveedores
function CargarListaOrdenCompraProveedores() {
    var Idproveedor = $('#Proveedor').val();

    if (Idproveedor.length == 0) {
        alert('Debe seleccionar un proveedor');
        return;
    }
    $.ajax({
        url: "../Controllers/MainAplication.ashx?op=300&Idproveedor=" + Idproveedor,
        method: "POST",
        dataType: "json",
        async: true,
        success: function (result) {
            if (result.toString().indexOf("ERROR") == -1) {

                listaOrdenCompraProveedor = result;

                $("#OrdenCompraXprov").empty();

                $.each(listaOrdenCompraProveedor, function () {
                    $("#OrdenCompraXprov").append($("<option> " +
                    "</option>").val(this["ID"]).html(this["COLUMN_NAME"]));
                });

               //$("#OrdenCompraXprov").val($($(objTarea).children()[3]).html());

            } else {
                //$("#lblError").show();
            }
        },
        error: function (result) {
            $("#lblError").show();
        }
    });
};

//Generar consecutivo
function GenerarConsecutivoOrdenCompra() {

    $.ajax({
        url: "../Controllers/MainAplication.ashx?op=400",
        method: "POST",
        dataType: "json",
        async: true,
        success: function (result) {
            if (result.toString().indexOf("ERROR") == -1) {

                listaProveedor = result;

                $("#NrnEntrada").empty();
                $("#NrnEntrada").val(listaProveedor);

            } else {
                //$("#lblError").show();
            }
        },
        error: function (result) {
            $("#lblError").show();
        }
    });
};

//Consultar proveedores
function ConsultarProveedoresXnombre() {
    var Nombre = "Didier";//$('#BuscarProveedor').val();

    if (Nombre.length == 0) {
        alert('Debe ingresar un nombre');
        return;
    }
    $.ajax({
        url: "../Controllers/MainAplication.ashx?op=500&Nombre=" + Nombre,
        method: "POST",
        dataType: "json",
        async: true,
        success: function (result) {
            if (result.toString().indexOf("ERROR") == -1) {

                dtProveedores = result;
                //$("#tblResultados").empty();

                tblRHead = $("#tblResultadosColsHead");
                tblRFoot = $("#tblResultadosColsFoot");

                var arrColumns = [];

                for (i = 0; i < dtProveedores.length; i++) {
                    var Id = dtProveedores[i].Id;
                    var Nombre = dtProveedores[i].Nombre;
                    var Estado = dtProveedores[i].Estado;

                    //Escribimos los datos mediante jquery en la tabla tbl-clientes que crearemos en la pagina web
                    $("#tblResultados tbody").append("<tr><td>" + Id + "</td>" +
                                                     "<td>" + Nombre + "</td>" +
                                                     "<td>" + Estado + "</td>");
                }
            } else {
                //$("#lblError").show();
            }
        },
        error: function (result) {
            $("#lblError").show();
        }
    });
};

//Consultar orden de compra x id
function ConsultarOrdenCompraXid() {
    var Id = $('#NrnOrdenC').val();

    if (Id.length == 0) {
        alert('Debe un Id');
        return;
    }
    $.ajax({
        url: "../Controllers/MainAplication.ashx?op=600&Id=" + Id,
        method: "POST",
        dataType: "json",
        async: true,
        success: function (result) {
            if (result.toString().indexOf("ERROR") == -1) {

                ConsultaOrdenCompra = result;
                //$("#tblResultados").empty();

                var arrColumns = [];

                for (i = 0; i < ConsultaOrdenCompra.length; i++) {
                    var Id = ConsultaOrdenCompra[i].Id;
                    var Fecha = ConsultaOrdenCompra[i].Fecha;
                    var ValorTotal = ConsultaOrdenCompra[i]["Valor total"];
                    var CantidadBienes = ConsultaOrdenCompra[i]["Cantidad bienes"];
                    var Proveedor = ConsultaOrdenCompra[i].Proveedor;
                    var Estado = ConsultaOrdenCompra[i].Estado;

                    //Escribimos los datos mediante jquery en la tabla tbl-clientes que crearemos en la pagina web
                    $("#tblResultadosOrdenC tbody").append("<tr><td>" + Id + "</td>" +
                                                     "<td>" + Fecha + "</td>" +
                                                     "<td>" + ValorTotal + "</td>" +
                                                     "<td>" + CantidadBienes + "</td>" +
                                                     "<td>" + Proveedor + "</td>" +
                                                     "<td>" + Estado + "</td>");
                }
            } else {
                //$("#lblError").show();
            }
        },
        error: function (result) {
            $("#lblError").show();
        }
    });
};

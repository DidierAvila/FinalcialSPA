//Variable que alamcena los proveedores
var listaProveedor;

$(document).ready(function () {
    $("#btnIniciarSesion").click(Login);
});


//Carga la lista de los campos de un archivador para los permisos por campo
function CargarListaCamposArchivador() {

 $.ajax({
     url: "../Controllers/MainAplication.ashx/ProcessRequestOther?op=200",
     method: "POST",
     dataType: "json",
     async: true,
     success: function (result) {

         if (result.toString().indexOf("ERROR") == -1) {
             
             listaProveedor = result;

             $("#Proveedor").empty();
             
             $.each(listaProveedor, function () {
                 $("#Proveedor").append($("<option> " +
                 "</option>").val(this["COLUMN_NAME"]).html(this["COLUMN_NAME"]));
             });

             $("#DETALLE_PC_CAMPO").val($($(objTarea).children()[3]).html());

             //estadoFormulario = "ingresando";
             //idFormulario = result;
             //$("#idFormulario").text(idFormulario);
         } else {
             //$("#lblError").show();
         }

     },
     error: function (result) {

         //$("#lblError").show();

     }
});


function Login() {

    Username = $('#TxtUsername').val();
    Password = $('#TxtPassword').val();

    if (Username.length == 0) {
        alert('Debe ingresar un usuario');
        return;
    }

    if (Password.length == 0) {
        alert('Debe ingresar un Password');
        return;
    }

    $.ajax({
        url: "../Controllers/CtrLogin.ashx/ProcessRequestOther?op=100&usuario=" + Username + "&contrasena=" + Password,
        method: "POST",
        dataType: "json",
        async: true,
        beforeSend: function () {

        },
        success: function (result) {
            if (result.val == "true") {
                $(location).attr('href', "MainAplication.html");
            } else {
                alert('KO!' + result);
            }
        },
        error: function (result) {
        }
    });
};
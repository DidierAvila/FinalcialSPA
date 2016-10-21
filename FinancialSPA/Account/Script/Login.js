$(document).ready(function () {
    $("#btnIniciarSesion").click(Login);
});


function Login() {

    Username = $('#TxtUsername').val();
    Password = $('#TxtPassword').val();

    if (Username.length == 0)
    {
        alert('Debe ingresar un usuario');
        return;
    }

    if (Password.length == 0) {
        alert('Debe ingresar un Password');
        return;
    }

    $.ajax({
        url: "../Controllers/MainAplication.ashx/ProcessRequestOther?op=100&Username=" + Username + "&Password=" + Password,
        method: "POST",
        dataType: "json",
        async: true,
        beforeSend: function () {
        },
        success: function (result) {
            if (result == true){
                document.location.href = "../MainAplication.html";
            } else {
                alert('KO!' + result);
            }
        },
        error: function (result) {
        }
    });
};


function AutenticarUserDwB() {

    $(".imgEspera").show();
    $("#vtnAlerta").hide();

    user = $("#txtUsuario").val();
    pwd = $("#txtContrasena").val();

    if (user.length == 0) {
        $("#vtnAlerta").show();
        $(".imgEspera").hide();
        $('#btnIngresar').attr("disabled", false);
        return;
    }
    $('#btnIngresar').attr("disabled", true);

    $.ajax({
        url: "../controladores/controladorCI.ashx?op=115&usuario=" + user + "&contrasena=" + pwd,
        method: "POST",
        dataType: "json",
        async: true,
        beforeSend: function () {
            /*
            $("#divMensajeError").hide();
            $(".divEspera").show("scale", "slow");
            */
        },
        success: function (result) {

            $(".imgEspera").hide();
            $('#btnIngresar').attr("disabled", false);

            if (result.toString().indexOf("ERROR") > -1) {

                $("#lblInfoError").val("Usuario y/o contraseña no valido");

                $("#vtnAlerta").show();
                //Comentar solo depuración
                //document.location.href = "mainUserBootStrap.aspx";



            } else {

                if (result.toString().indexOf("false") > -1) {
                    $("#lblInfoError").val("Usuario y/o contraseña no valido");
                    $("#vtnAlerta").show();
                } else {

                    storage = $.localStorage;
                    storage.set("loginusuario", user);

                    document.location.href = "mainUserBootStrap.aspx";

                }


            }



        },
        error: function (result) {
            $(".imgEspera").hide();
            $('#btnIngresar').attr("disabled", false);
            $("#lblInfoError").text("Usuario y/o contraseña no valido");
            $("#vtnAlerta").show();

            //Comentar solo depuración
            // document.location.href = "mainUserBootStrap.aspx";
        }
    });



}
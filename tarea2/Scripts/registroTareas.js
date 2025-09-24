// registroTareas.js

// Esperar a que el DOM esté listo
$(document).ready(function () {
    // Selector del formulario por ID
    const $form = $("#formRegistroTarea");

    // Evento de envío del formulario
    $form.on("submit", function (e) {
        e.preventDefault(); // Evita el envío tradicional

        // Validación básica en cliente
        const titulo = $("#Titulo").val().trim();
        const descripcion = $("#Descripcion").val().trim();
        const fecha = $("#FechaVencimiento").val().trim();
        const prioridad = $("#Prioridad").val();

        // Limpieza de mensajes previos
        $(".text-danger").text("");

        let errores = false;

        // Validaciones accesibles
        if (titulo === "") {
            $("#Titulo").next(".text-danger").text("El título es obligatorio.");
            errores = true;
        }

        if (descripcion === "") {
            $("#Descripcion").next(".text-danger").text("La descripción es obligatoria.");
            errores = true;
        }

        if (fecha === "") {
            $("#FechaVencimiento").next(".text-danger").text("La fecha es obligatoria.");
            errores = true;
        }

        if (prioridad === "" || prioridad === "-- Seleccione prioridad --") {
            $("#Prioridad").next(".text-danger").text("Debe seleccionar una prioridad.");
            errores = true;
        }

        // Si hay errores, no se envía
        if (errores) return;

        // Envío por Ajax
        $.ajax({
            url: $form.attr("action"),
            type: "POST",
            data: $form.serialize(),
            success: function (respuesta) {
                // Mostrar mensaje institucional de éxito
                mostrarMensaje("La tarea fue registrada correctamente.", "success");

                // Opcional: limpiar el formulario
                $form[0].reset();
            },
            error: function () {
                // Mostrar mensaje institucional de error
                mostrarMensaje("Ocurrió un error al registrar la tarea. Intente nuevamente.", "danger");
            }
        });
    });

    // Función para mostrar mensajes accesibles
    function mostrarMensaje(texto, tipo) {
        const $mensaje = $("<div>")
            .addClass(`alert alert-${tipo}`)
            .attr("role", "alert")
            .attr("aria-live", "assertive")
            .text(texto);

        $(".body-content").prepend($mensaje);

        // Eliminar mensaje después de 5 segundos
        setTimeout(() => $mensaje.fadeOut(500, () => $mensaje.remove()), 5000);
    }
});

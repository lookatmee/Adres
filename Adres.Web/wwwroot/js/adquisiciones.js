$(document).ready(function () {
    // Calcular valor total cuando cambie cantidad o valor unitario
    $('#Adquisicion_Cantidad, #Adquisicion_ValorUnitario').on('change', function () {
        calcularValorTotal();
    });

    // Mostrar modal de éxito
    if ($('#successMessage').val()) {
        $('#successModal').modal('show');
        setTimeout(function () {
            $('#successModal').modal('hide');
        }, 3000);
    }

    // Confirmar eliminación
    $('.btn-delete').on('click', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        $('#deleteModal').modal('show');
        $('#confirmDelete').data('id', id);
    });

    // Procesar eliminación
    $('#confirmDelete').on('click', function () {
        var id = $(this).data('id');
        window.location.href = '/Adquisiciones/Delete?id=' + id;
    });
});

function calcularValorTotal() {
    var cantidad = $('#Adquisicion_Cantidad').val() || 0;
    var valorUnitario = $('#Adquisicion_ValorUnitario').val() || 0;
    var total = cantidad * valorUnitario;
    $('#valorTotal').text(total.toLocaleString('es-CO', { style: 'currency', currency: 'COP' }));
}

// Validación del formulario
function validarFormulario() {
    var form = document.querySelector('form');
    if (!form.checkValidity()) {
        event.preventDefault();
        event.stopPropagation();
    }
    form.classList.add('was-validated');
    return form.checkValidity();
}

// Mostrar modal de éxito personalizado
function mostrarMensajeExito(mensaje) {
    $('#successMessage').text(mensaje);
    $('#successModal').modal('show');
    setTimeout(function () {
        $('#successModal').modal('hide');
    }, 3000);
} 
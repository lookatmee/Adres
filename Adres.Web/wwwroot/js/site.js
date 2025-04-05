// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Función para mostrar mensajes de éxito
function showSuccess(message) {
    Swal.fire({
        title: '¡Éxito!',
        text: message,
        icon: 'success',
        confirmButtonText: 'Aceptar',
        confirmButtonColor: '#1B2A4A'
    });
}

// Función para mostrar mensajes de error
function showError(message) {
    Swal.fire({
        title: 'Error',
        text: message,
        icon: 'error',
        confirmButtonText: 'Aceptar',
        confirmButtonColor: '#1B2A4A'
    });
}

// Función para confirmar acciones
function confirmAction(title, text) {
    return Swal.fire({
        title: title,
        text: text,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#1B2A4A',
        cancelButtonColor: '#6c757d',
        confirmButtonText: 'Sí, continuar',
        cancelButtonText: 'Cancelar'
    });
}

// Función para formatear moneda
function formatCurrency(amount) {
    return new Intl.NumberFormat('es-CO', {
        style: 'currency',
        currency: 'COP',
        minimumFractionDigits: 0
    }).format(amount);
}

// Función para formatear fechas
function formatDate(date) {
    return new Date(date).toLocaleDateString('es-CO', {
        year: 'numeric',
        month: 'long',
        day: 'numeric'
    });
}

// Función para validar formularios
function validateForm(formId) {
    const form = document.getElementById(formId);
    if (!form.checkValidity()) {
        event.preventDefault();
        event.stopPropagation();
        form.classList.add('was-validated');
        return false;
    }
    return true;
}

// Función para inicializar tooltips de Bootstrap
function initTooltips() {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
}

// Función para inicializar datepickers
function initDatepickers() {
    const datepickers = document.querySelectorAll('.datepicker');
    datepickers.forEach(dp => {
        new Datepicker(dp, {
            format: 'dd/mm/yyyy',
            language: 'es',
            autohide: true
        });
    });
}

// Función para manejar la carga de archivos
function handleFileUpload(inputId, previewId) {
    const input = document.getElementById(inputId);
    const preview = document.getElementById(previewId);
    
    input.addEventListener('change', function() {
        preview.innerHTML = '';
        for (let file of this.files) {
            const reader = new FileReader();
            reader.onload = function(e) {
                const div = document.createElement('div');
                div.className = 'file-preview';
                div.innerHTML = `
                    <i class="fas fa-file me-2"></i>
                    <span>${file.name}</span>
                `;
                preview.appendChild(div);
            }
            reader.readAsDataURL(file);
        }
    });
}

// Inicializar componentes cuando el documento está listo
document.addEventListener('DOMContentLoaded', function() {
    initTooltips();
    initDatepickers();
});

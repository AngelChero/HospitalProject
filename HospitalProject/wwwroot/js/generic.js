function confirmBtnModal(titulo = "¿Estás seguro de guardas los cambios?",
    texto = "¡Esta acción no se podrá revertir!") {
    return Swal.fire({
        title: titulo,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '¡Sí!'
    })
}
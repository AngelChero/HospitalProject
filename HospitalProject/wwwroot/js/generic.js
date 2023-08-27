function confirmBtnModal(titulo = "¿Estás seguro de guardar los cambios?",
    texto = "¡Esta acción no se podrá revertir!") {
    return Swal.fire({
        title: titulo,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '¡Sí, seguro!'
    })
}

function successModal(
    title = "Se eliminó correctamente") {
    return Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: title,
        showConfirmButton: false,
        timer: 5000
    })
}

function errorModal(
    title = "¡Lo siento ...!",
    text = "Algo salió mal."
    ) {
    return Swal.fire({
        icon: 'error',
        title: title,
        text: text
    })
}

function paintData(url, properties, propertyId, nameController) {
    let content = "";
    let tblData = document.getElementById('tblData');
    let nameProperty;
    let currentObject;
    $.get(url, function (data) {
        for (var i = 0; i < data.length; i++) {
            content += "<tr>";
            for (var j = 0; j < properties.length; j++) {
                nameProperty = properties[j];
                currentObject = data[i];
                content += `<td>${currentObject[nameProperty]}</td>`
            }
            content += `
                                <td>
                                    <a href="${nameController}/Edit/${currentObject[propertyId]}">
                                        <i class="fa fa-edit btn btn-warning"></i>
                                    </a>
                                    <i class="fa fa-trash btn btn-danger"
                                    onclick="btnDelete(${currentObject[propertyId]})">
                                    </i>
                                </td>
                            `
            content += "</tr>";
        }
        tblData.innerHTML = content;  
    })
}
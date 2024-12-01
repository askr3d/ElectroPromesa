
document.addEventListener('DOMContentLoaded', () => {
    fotoSubidaConfig();
});

const fotoSubidaConfig = () => {
    const fotoInput = document.querySelector("#FotoUrl");
    const extensionesValidas = "jpg,jpeg,svg,png";
    const fotoLabel = document.querySelector("#fotoLabel");
    const fotoPreview = document.querySelector("#fotoPreview");

    fotoInput.addEventListener('change', (e) => {
        try {
            const imagen = e.target.files[0];
            const fotoExtension = imagen.name.split('.').pop();
            if (extensionesValidas.includes(fotoExtension.toLowerCase())) {
                ocultarFotoLP(fotoLabel, fotoPreview);
                fotoPreview.src = URL.createObjectURL(imagen);
            } else {
                Swal.fire({
                    title: "Imagen Invalida",
                    text: "Formatos validos: .jpg, .jpeg, .png, .svg",
                    icon: "error"
                });
            }
        } catch (error) {

        }
    });
}

const ocultarFotoLP = (ocultar, mostrar) => {
    if (!ocultar.classList.contains('d-none')) {
        ocultar.classList.add('d-none');
    }
    if (mostrar.classList.contains('d-none')) {
        mostrar.classList.remove('d-none');
    }
}
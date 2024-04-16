document.addEventListener("DOMContentLoaded", function (event) {
    console.info("Pruebas")
});

document.getElementById('FormularioCrearViaje').addEventListener('submit', function (event) {
    event.preventDefault();
    
    const fecha_Desde = document.getElementById('fecha_Desde').value;
    const fecha_Hasta = document.getElementById('Fecha_Hasta').value;

    const datos = {
        Fecha_Desde: fecha_Desde,
        Fecha_Hasta: fecha_Hasta,
    };
    
    console.log(datos);

    fetch('http://localhost:5259/Viaje', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(datos)
    })
        .then(response => {
            console.log('response', response)
            if (!response.ok) {
                return response.json();
            }
            return response.json();
        })
        .then(data => {
            console.log('data', data);
            if (data?.success) {
                console.log('Respuesta de la API:', data);
                alert(data.message);
            } else {
                console.error('Error al enviar los datos:', data);
                alert('Hubo un error al crear la compra: ' + data?.[0]?.message);
            }
        })
        
})
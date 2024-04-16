document.addEventListener("DOMContentLoaded", function (event) {
    console.info("Pruebas")
});

document.getElementById('FormularioCrearCompra').addEventListener('submit', function (event) {
    event.preventDefault();

    const id_Producto = document.getElementById('cod_Producto').value;
    const dni_Cliente = document.getElementById('dni_Cliente').value;
    const cantidad = document.getElementById('cantidad').value;
    const fecha_Entrega = document.getElementById('fecha_Entrega').value;

    const datos = {
        Id_Producto: id_Producto,
        Dni_Cliente: dni_Cliente,
        Cantidad: cantidad,
        Fecha_Entrega: fecha_Entrega,
    };

    console.log(datos);
    fetch('http://localhost:5259/Compras/GenerarCompra', {
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

function llenarTablaHerramienta() {
    fetch('http://localhost:45856/Compras')
        .then(response => response.json())
        .then(data => {
                data.forEach(compra => {
                    var fila = document.createElement("tr");
                    fila.innerHTML = `
                        <td>${compra.Id_Producto}</td>
                        <td>${compra.Dni_Cliente}</td>
                        <td>${compra.Cantidad}</td>
                        <td>${compra.Fecha_Entrega}</td>
                    `;
                    tablaHerramientas.appendChild(fila);
                });
        })
        .catch(error => {
            console.error('Error al obtener datos:', error);
            var filaVacia = document.createElement("tr");
            filaVacia.innerHTML = `
                <td colspan="9">Error al obtener los datos</td>
            `;
            tablaProductos.appendChild(filaVacia);
        });
}
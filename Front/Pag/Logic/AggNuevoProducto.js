document.addEventListener("DOMContentLoaded", function (event) {
    console.info("Pruebas")
});

document.getElementById('FormularioCrearProductos').addEventListener('submit', function (event) {
    event.preventDefault();

    const nombre = document.getElementById('nombre').value;
    const marca = document.getElementById('marca').value;
    const alto_Caja = document.getElementById('alto_Caja').value;
    const ancho_Caja = document.getElementById('ancho_Caja').value;
    const profundidad_Caja = document.getElementById('profundidad_Caja').value;
    const precio_Unitario = document.getElementById('precio_Unitario').value;
    const stock_Minimo = document.getElementById('stock_Minimo').value;
    const en_Stock = document.getElementById('en_Stock').value;

    const datos = {
        Nombre: nombre,
        Marca: marca,
        Alto_Caja: alto_Caja,
        Ancho_Caja: ancho_Caja,
        Profundidad_Caja: profundidad_Caja,
        Precio_Unitario: precio_Unitario,
        Stock_Minimo: stock_Minimo,
        EnStock: en_Stock
    };

    console.log(datos);
    fetch('http://localhost:45856/Producto', { 
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
                alert('Hubo un error al crear el producto: ' + data?.[0]?.message);
            }
        })
        
})
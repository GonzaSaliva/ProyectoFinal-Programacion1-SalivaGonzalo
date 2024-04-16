document.getElementById('FormularioActualizar').addEventListener('submit', function (event) {
    event.preventDefault();

    const id = document.getElementById('productId').value;
    const Stock = document.getElementById('addStock').value;

    fetch(`http://localhost:45856/Producto/${id}/${stock}`, { 
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(Stock)
    })
        .then(response => {
            if (!response.ok) {
                return response.json();
            }
            return response.json();
        })
        .then(data => {
            if (data?.success) {
                console.log('Respuesta de la API:', data);
                alert(data.message);
            } else {
                console.error('Error al enviar los datos:', data);
                document.querySelector('.cont-form').classList.add('vibrar');
                setTimeout(() => {
                    document.querySelector('.cont-form').classList.remove('vibrar');
                    alert('Hubo un error al actualizar el stock: ' + data.message);
                }, 300);
            }
        })
})     
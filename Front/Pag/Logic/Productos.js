document.addEventListener("DOMContentLoaded", function (event) {
    console.info("Pruebas")
});

function llenarTablaUsuarios() {
    fetch('http://localhost:45856/Producto/ListaProductos')
        .then(response => response.json())
        .then(data => {
            var tablaProductos = document.getElementById("tablaProductos");
            var productosFiltrados = data.filter(producto => producto.enStock < producto.stock_Minimo);
            if (productosFiltrados.length === 0) {
                var filaVacia = document.createElement("tr");
                filaVacia.innerHTML = `
                    <td colspan="9">No hay productos con stock menor al stock mínimo.</td>
                `;
                tablaProductos.appendChild(filaVacia);
            } else {
                productosFiltrados.forEach(producto => {
                    var fila = document.createElement("tr");
                    fila.innerHTML = `
                        <td>${producto.codigo}</td>
                        <td>${producto.nombre}</td>
                        <td>${producto.marca}</td>
                        <td>${producto.alto_Caja}</td>
                        <td>${producto.ancho_Caja}</td>
                        <td>${producto.profundidad_Caja}</td>
                        <td>${producto.precio_Unitario}</td>
                        <td>${producto.stock_Minimo}</td>
                        <td>${producto.enStock}</td>
                    `;
                    tablaProductos.appendChild(fila);
                });
            }
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

document.addEventListener("DOMContentLoaded", llenarTablaUsuarios);


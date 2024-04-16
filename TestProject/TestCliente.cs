using DataProyect;
using DTOProyect;
using ServiceProyect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class TestCliente
    {
        [SetUp]
        public void Setup()
        {
            ClienteService serviceCliente = new ClienteService();
            ArchivoCliente.LimpiarClientesJson();
            DateTime fecha = new DateTime(2001, 10, 9);
            ClienteDto clienteDto = new ClienteDto() { Dni = 43494390, Nombre = "Gonzalo", Apellido = "Saliva", Email = "gonzisaliva@gmail.com", FechaNacimiento = fecha, Telefono = 15567150, Latitud = -31.25033, Longitud = -61.4867, };
            serviceCliente.CrearCliente(clienteDto);
        }

        [Test]
        public void TestCrearClienteDeberiaDevolverHttpStatusCode200()
        {
            ClienteService serviceCliente = new ClienteService();
            
            //arrange 
            ClienteDto clienteDto = new ClienteDto() { Dni = 43494391, Nombre = "Gonzalo", Apellido = "Saliva", Email = "gonzisaliva@gmail.com", FechaNacimiento = Convert.ToDateTime("09/10/2001 00:00:00"), Telefono = 15567150, Latitud = -31.25033, Longitud = -61.4867, };

            // act
            ResultadoResponse res = serviceCliente.CrearCliente(clienteDto);

            // Assert
            Assert.That(res.Result.Message, Is.EqualTo("El cliente se ha registrado con exito"));

        }

        [Test]
        public void TestCrearClienteDeberiaDevolverHttpStatusCode300()
        {
            ClienteService service = new ClienteService();
            // act
            ClienteDto clienteDto2 = new ClienteDto() { Dni = 43494390, Nombre = "Gonzalo", Apellido = "Saliva", Email = "gonzisaliva@gmail.com", FechaNacimiento = Convert.ToDateTime("09/10/2001 00:00:00"), Telefono = 15567150, Latitud = -31.25033, Longitud = -61.4867, };
            ResultadoResponse res = service.CrearCliente(clienteDto2);

            // Assert
            Assert.That(res.Result.Message, Is.EqualTo("El DNI ingresado ya se encuentra registrado en el sistema"));
        }

        [Test]
        public void TestEliminarClienteDeveriaDevolverHttpStatusCode200()
        {
            ClienteService serviceCliente = new ClienteService();
            // act
            ResultadoResponse res = serviceCliente.EliminarCliente(43494390);

            // Assert
            Assert.That(res.Result.Message, Is.EqualTo("El cliente se eliminó con éxito"));
        }

        [Test]
        public void TestEliminarClienteDeveriaDevolverHttpStatusCode400()
        {
            ClienteService serviceCliente = new ClienteService();
            // act
            ResultadoResponse res = serviceCliente.EliminarCliente(43494391);

            // Assert
            Assert.That(res.Result.Message, Is.EqualTo("El documento ingresado no corresponde a ningun cliente del sistema"));
        }
    }
}

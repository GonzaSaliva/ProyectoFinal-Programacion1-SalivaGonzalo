using DataProyect;

namespace TestProject
{
    public class TestCamioneta
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestObtenerListaCamionetasDeberiaDevolverListaCon3Elementos()
        {
            // act
            List<Camioneta> listadoCamionetas = ArchivoCamioneta.LeerCamionetaDesdeArchivoJson();

            // Assert
            Assert.That(listadoCamionetas.Count, Is.EqualTo(3));
        }
    }
}
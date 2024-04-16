using Newtonsoft.Json;

namespace DataProyect
{
    public class ArchivoCliente
    {
        public static Cliente GuardarClienteEnArchivoJson(Cliente data)
        {
            var listado = LeerClienteDesdeArchivoJson();
            if (data.Id != 0)
            {
                listado.RemoveAll(x => x.Id == data.Id);
            }
            else
            {
                data.Id = listado.Count + 1;
                data.FechaRegistro = DateTime.Now;
            }

            listado.Add(data);

            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cliente.json");
            string json = JsonConvert.SerializeObject(listado, Formatting.Indented);

            File.WriteAllText(rutaArchivo, json);

            return data;
        }

        public static List<Cliente> LeerClienteDesdeArchivoJson()
        {
            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cliente.json");

            if (File.Exists(rutaArchivo))
            {
                string json = File.ReadAllText(rutaArchivo);
                return JsonConvert.DeserializeObject<List<Cliente>>(json);
            }
            else
            {
                return new List<Cliente>();
            }
        }

        public static void LimpiarClientesJson()
        {
            var listaCliente = LeerClienteDesdeArchivoJson();
            listaCliente.Clear();

            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cliente.json");
            string json = JsonConvert.SerializeObject(listaCliente, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }
    }
}

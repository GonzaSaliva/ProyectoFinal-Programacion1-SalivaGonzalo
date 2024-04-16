using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProyect
{
    public class ArchivoCompra
    {
        public static Compra GuardarCompraEnArchivoJson(Compra data)
        {
            var listado = LeerCompraDesdeArchivoJson();
            if (data.Id != 0)
            {
                listado.RemoveAll(x => x.Id == data.Id);
            }
            else
            {
                data.Id = listado.Count + 1;
                data.FechaCompra = DateTime.Now;
            }

            listado.Add(data);

            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "compra.json");
            string json = JsonConvert.SerializeObject(listado, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);

            return data;
        }

        public static List<Compra> LeerCompraDesdeArchivoJson()
        {
            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "compra.json");

            if (File.Exists(rutaArchivo))
            {
                string json = File.ReadAllText(rutaArchivo);
                return JsonConvert.DeserializeObject<List<Compra>>(json);
            }
            else
            {
                return new List<Compra>();
            }
        }

        public static void LimpiarCompraJson()
        {
            var listaCompra = LeerCompraDesdeArchivoJson();
            listaCompra.Clear();

            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "compra.json");
            string json = JsonConvert.SerializeObject(listaCompra, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }
    }
}


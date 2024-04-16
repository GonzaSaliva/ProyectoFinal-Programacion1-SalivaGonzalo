using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProyect
{
    public class ArchivoProducto
    {
        public static Producto GuardarEnArchivoJson(Producto data)
        {
            var listado = LeerDesdeArchivoJson();
            if (data.CodigoAutoincremetnal != 0)
            {
                listado.RemoveAll(x => x.CodigoAutoincremetnal == data.CodigoAutoincremetnal);
            }
            else
            {
                data.CodigoAutoincremetnal = listado.Count + 1;
                data.FechaCreacion = DateTime.Now;
            }

            listado.Add(data);

            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "producto.json");
            string json = JsonConvert.SerializeObject(listado, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);

            return data;
        }
        public static List<Producto> LeerDesdeArchivoJson()
        {
            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "producto.json");

            if (File.Exists(rutaArchivo))
            {
                string json = File.ReadAllText(rutaArchivo);
                return JsonConvert.DeserializeObject<List<Producto>>(json);
            }
            else
            {
                return new List<Producto>();
            }
        }

        public static void LimpiarProductosJson()
        {
            var listaProducto = LeerDesdeArchivoJson();
            listaProducto.Clear();

            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "producto.json");
            string json = JsonConvert.SerializeObject(listaProducto, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }
    }
}

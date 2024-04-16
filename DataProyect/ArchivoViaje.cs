using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProyect
{
    public class ArchivoViaje
    {
        public static Viaje GuardarViajeaEnArchivoJson(Viaje data)
        {
            var listado = LeerViajeDesdeArchivoJson();
            if (data.Codigo != 0)
            {
                listado.RemoveAll(x => x.Codigo == data.Codigo);
            }
            else
            {
                data.Codigo = listado.Count + 1;
                data.FechaRegistro = DateTime.Now;
            }

            listado.Add(data);

            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "viaje.json");
            string json = JsonConvert.SerializeObject(listado, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);

            return data;
        }

        public static List<Viaje> LeerViajeDesdeArchivoJson()
        {
            string rutaArchivo = Path.GetFullPath("DataProyect/camionetas.json");

            rutaArchivo = rutaArchivo.Replace("\\ClienteWebApi", "");

            if (File.Exists(rutaArchivo))
            {
                string json = File.ReadAllText(rutaArchivo);
                return JsonConvert.DeserializeObject<List<Viaje>>(json);
            }
            else
            {
                return new List<Viaje>();
            }
        }

        public static void LimpiarViajesJson()
        {
            var listaProducto = LeerViajeDesdeArchivoJson();
            listaProducto.Clear();

            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "viaje.json");
            string json = JsonConvert.SerializeObject(listaProducto, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }
    }
}

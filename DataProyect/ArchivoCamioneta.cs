using System;
using System.Reflection;
using Newtonsoft.Json;

namespace DataProyect
{
    public class ArchivoCamioneta
    {
        public static List<Camioneta> LeerCamionetaDesdeArchivoJson()
        {
            string rutaArchivo = Path.GetFullPath("DataProyect/camionetas.json");

            rutaArchivo = rutaArchivo.Replace("\\ClienteWebApi", "");
            rutaArchivo = rutaArchivo.Replace("\\TestProject\\bin\\Debug\\net7.0","");

            if (File.Exists(rutaArchivo))
            {
                string json = File.ReadAllText(rutaArchivo);
                return JsonConvert.DeserializeObject<List<Camioneta>>(json);
            }
            else
            {
                return new List<Camioneta>();
            }
        }
    }
}

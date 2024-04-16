using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOProyect
{
    public class ViajeDto
    {
        [Required(ErrorMessage = "Debe ingresar la fecha desde")]
        [DataType(DataType.DateTime, ErrorMessage = "Ingrese la fecha de forma correcta.")]
        public DateTime FechaDesde { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha hasta")]
        [DataType(DataType.DateTime, ErrorMessage = "Ingrese la fecha de forma correcta.")]
        public DateTime FechaHasta { get; set; }

        public ValidacionDto EsValido()
        {
            ValidacionDto validacion = new ValidacionDto()
            {
                Errors = new List<Error>()
            };

            if (FechaDesde == null)
            {
                validacion.Errors.Add(new Error()
                {
                    Message = "La fecha desde es requerida."
                });
            }

            if (FechaHasta == null)
            {
                validacion.Errors.Add(new Error()
                {
                    Message = "La fecha hasta es requerida."
                });
            }

            if (FechaDesde < DateTime.Today)
            {
                validacion.Errors.Add(new Error()
                {
                    Message = "La fecha desde no puede ser menor a la fecha actual."
                });
            }

            //La feche hasta solo puede ser 7 dias mayor a la feacha_desde
            if (FechaHasta > FechaDesde.AddDays(7))
            {
                validacion.Errors.Add(new Error()
                {
                    Message = "La fecha hasta no puede ser mayor a 7 días."
                });
            }

            if (validacion.Errors.Count == 0)
            {
                validacion.Success = true;
            }

            return validacion;
        }
    }
}

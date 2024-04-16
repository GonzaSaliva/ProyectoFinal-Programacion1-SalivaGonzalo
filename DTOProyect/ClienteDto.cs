using System.ComponentModel.DataAnnotations;

namespace DTOProyect
{
    public class ClienteDto
    {
        [RegularExpression("^[0-9]+$", ErrorMessage = "El documento debe contener solo números.")]
        [Required(ErrorMessage = "Debe ingresar el documento")]
        public long? Dni { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "El nombre debe contener solo letras.")]
        [Required(ErrorMessage = "Debe ingresar el nombre")]
        public string? Nombre { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "El apellido debe contener solo letras.")]
        [Required(ErrorMessage = "Debe ingresar el apellido")]
        public string? Apellido { get; set; }

        [EmailAddress(ErrorMessage = "La dirección de correo electrónico no es válida.")]
        [Required(ErrorMessage = "Debe ingresar el correo electronico")]
        public string? Email { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "El número de teléfono no es válido.")]
        [Required(ErrorMessage = "Debe ingresar el telefono")]
        public long? Telefono { get; set; }

        [Range(-100.0, 100.0, ErrorMessage = "El valor debe estar entre -100 y 100.")]
        [Required(ErrorMessage = "Debe ingresar la latitud")]
        public double Latitud { get; set; }

        [Range(-100.0, 100.0, ErrorMessage = "El valor debe estar entre -100 y 100.")]
        [Required(ErrorMessage = "Debe ingresar la longitud")]
        public double Longitud { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Ingrese la fecha de nacimiento de forma correcta.")]
        [Required(ErrorMessage = "Debe ingresar la fecha de nacimiento")]
        public DateTime? FechaNacimiento { get; set; }
    }
}


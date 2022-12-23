using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebVistas.Models {
    public class Empleado {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar numero empleado")]
        [Display(Name = "Empleado")]
        public int NumeroEmpleado { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar numero empleado")]
        [Range(18, 100, ErrorMessage = "Debe estar entre 18 y 100 años")]
        public int Edad { get; set; }


        public bool Contratado { get; set; }

        [Display(Name = "Correo electronico")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)", ErrorMessage = "Correo inválido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar correo")]
        public string Email { get; set; }

        [Display(Name = "Clave")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        public Empleado(int numEmpleado, String nombre) {
            NumeroEmpleado = numEmpleado;
            Nombre = nombre;
        }

    }
}
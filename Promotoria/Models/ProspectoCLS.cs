using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Promotoria.Models
{
    public class ProspectoCLS
    {
   
        public int idProspecto { get; set; }
        [Required(ErrorMessage = "Introduce el Nombre")]
        public string Nombre { get; set; }
        //..................................
        [Required(ErrorMessage = "Introduce el primer apellido")]
        [Display(Name = "Apellido Paterno")]    
        public string PrimerApellido { get; set; }
        //..................................
        [Display(Name = "Apellido Materno")]
        public string SegundoApellido { get; set; }
        //..................................
        [Required(ErrorMessage = "Introduce la calle")]
        public string Calle { get; set; }
        //..................................
        [Required(ErrorMessage = "Introduce numero")]
        public string Numero { get; set; }
        //..................................
        [Required(ErrorMessage = "Introduce la colonia")]
        public string Colonia { get; set; }
        //..................................
        [Required(ErrorMessage = "Introduce el codigo postal")]
        [Display(Name = "Código Postal")]
        public string CodigoPostal { get; set; }
        //..................................
        //[StringLength(10, ErrorMessage = "Longitud maxima 10 Digitos")]
        [Required(ErrorMessage = "Introduce el telefono max 10 digitos")]
        public string Telefono { get; set; }
        //..................................
        [Required(ErrorMessage = "Introduce el RFC")]
        public string RFC { get; set; }
        //[Display(Name = "Documentos")]
        public  byte[] data { get; set; }
        [Required(ErrorMessage = "Introduce las observaciones")]
        [Display(Name = "Observaciones")]
        
        public string Observaciones { get; set; }
        public int Idestado { get; set; }
        //propiedades adicionales
        public string mensajeError { get; set; }
        [Display(Name = "Estado")]
        public string nomEstado { get; set; }

        public int idDoc { get; set; }

        
        public string ruta { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Promotoria.Models
{
    public class DocumentosCLS
    {
        public int idDoc { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Data")]
        public byte[] Data { get; set; }
        [Display(Name = "Ruta del Archivo")]
        public string ruta { get; set; }
        public int idprosp { get; set; }
    }
}
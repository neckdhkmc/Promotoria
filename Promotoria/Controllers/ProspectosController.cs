using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Promotoria.Models;
using System.Text;

namespace Promotoria.Controllers
{
    public class ProspectosController : Controller
    {
        // GET: Propsectos
        public ActionResult Index()
        {

            List<ProspectoCLS> ListadeProspectos = null;

            using (var bd = new TalperBDEntities())
            {
                ListadeProspectos = (from prop in bd.Prospecto
                                     join std in bd.Estados
                                     on prop.IdEstado equals std.IdEstado
                                     select new ProspectoCLS
                                     {
                                         idProspecto = prop.IdProspecto,
                                     
                                         Nombre = prop.Nombre,
                                         PrimerApellido = prop.PrimerApellido,
                                         SegundoApellido = prop.SegundoApellido,
                                         //Calle = prop.Calle,
                                         //Colonia = prop.Colonia,
                                         //CodigoPostal = prop.CodigoPostal,
                                         //Telefono = prop.Telefono,
                                         //RFC = prop.RFC,
                                         Observaciones = prop.Observaciones,
                                         nomEstado = std.NombreEstado

                                     }
                                    ).ToList();
                return View(ListadeProspectos);
            }


        }

        public ActionResult IndexEvaluacion()
        {
            List<ProspectoCLS> ListadeProspectosEvaluacion = null;

            using (var bd = new TalperBDEntities())
            {
                ListadeProspectosEvaluacion = (from prop in bd.Prospecto
                                               join std in bd.Estados
                                               on prop.IdEstado equals std.IdEstado
                                               select new ProspectoCLS
                                               {
                                                   Nombre = prop.Nombre,
                                                   PrimerApellido = prop.PrimerApellido,
                                                   SegundoApellido = prop.SegundoApellido,
                                                   Calle = prop.Calle,
                                                   Numero = prop.numero,
                                                   Colonia = prop.Colonia,
                                                   CodigoPostal = prop.CodigoPostal,
                                                   Telefono = prop.Telefono,
                                                   RFC = prop.RFC,
                                                   nomEstado = std.NombreEstado

                                               }
                                    ).ToList();
                return View(ListadeProspectosEvaluacion);
            }
        }

        public ActionResult Documentos(int id)
        {
            List<DocumentosCLS> listadoc = null;
            using (var bd = new TalperBDEntities())
            {
                listadoc = (from doc in bd.Documentos
                            where doc.idProsp == id
                            select new DocumentosCLS
                            {
                                idDoc =  doc.IdDoc,
                                nombre = doc.nombre,
                                ruta = doc.ruta,
                                Data = doc.Data

                            }
                            ).ToList();
            
            
            }
                return View(listadoc);
        }
        [HttpPost]
        public FileResult DownloadFile(int? fileId)// metodo para descargar agregado
        {
            TalperBDEntities entities = new TalperBDEntities();
            Documentos file = entities.Documentos.ToList().Find(p => p.IdDoc == fileId.Value);
            return File(file.Data, file.ruta);
        }

        public ActionResult Agregar()
        {
            ProspectoCLS obj = new ProspectoCLS();

            return View(obj);
        }

        [HttpPost]
        public ActionResult Agregar(ProspectoCLS obj)
        {

            if (!ModelState.IsValid)
            {


                return View(obj);


            }
            else
            {
                using (var bd = new TalperBDEntities())
                {
                    Prospecto objeto = new Prospecto();
                    //objeto.IdProspecto = obj.idProspecto;
                    objeto.Nombre = obj.Nombre;
                    objeto.PrimerApellido = obj.PrimerApellido;
                    objeto.SegundoApellido = obj.SegundoApellido;
                    objeto.Calle = obj.Calle;
                    objeto.numero = obj.Numero;
                    objeto.Colonia = obj.Colonia;
                    objeto.CodigoPostal = obj.CodigoPostal;
                    objeto.Telefono = obj.Telefono;
                    objeto.RFC = obj.RFC;
                    objeto.Observaciones = obj.Observaciones;
                    objeto.IdEstado = 1;

                    bd.Prospecto.Add(objeto);
                    bd.SaveChanges();

                    //Agregar documentos
                    List<DocumentosCLS> lstDocs = new List<DocumentosCLS>();
                    HttpFileCollectionBase coleccionDocu = null;
                    int id = bd.Prospecto.Max(p => p.IdProspecto);
                    coleccionDocu = Request.Files;
                    if (coleccionDocu != null && coleccionDocu.Count > 0)
                    {
                        for (int i = 0; i < coleccionDocu.Count; i++)
                        {
                            HttpPostedFileBase DatosDocu = coleccionDocu[i];
                            Documentos obDo = new Documentos();

                            using (var ms = new MemoryStream())
                            {
                                DatosDocu.InputStream.CopyTo(ms);
                                var filebytes = ms.ToArray();
                                string s = Convert.ToBase64String(filebytes);
                                obDo.Data = filebytes;
                                obDo.idProsp = id;
                                obDo.nombre = DatosDocu.FileName;
                                obDo.ruta = DatosDocu.ContentType;

                            }
                            bd.Documentos.Add(obDo);
                            bd.SaveChanges();




                        }


                    }
                }

                return RedirectToAction("Index");
            }
        
        }
        public ActionResult PatallaInicial()
        {

            return View();
        }

        public ActionResult CargarDatosProspecto(int id)
        {
            ProspectoCLS objeto = new ProspectoCLS();
            using (var bd = new TalperBDEntities())
            {
                Prospecto oPro = bd.Prospecto.Where(p => p.IdProspecto.Equals(id)).First();
              
                objeto.idProspecto = oPro.IdProspecto;
                objeto.Nombre = oPro.Nombre;
                objeto.PrimerApellido = oPro.PrimerApellido;
                objeto.SegundoApellido = oPro.SegundoApellido;
                objeto.Calle = oPro.Calle;
                objeto.Numero = oPro.numero;
                objeto.Colonia = oPro.Colonia;
                objeto.CodigoPostal = oPro.CodigoPostal;
                objeto.Telefono = oPro.Telefono;
                objeto.RFC = oPro.RFC;
                objeto.Observaciones = oPro.Observaciones;

            }
                return View(objeto);
        }

        public ActionResult VistaDatosCompletos(int id)
        {
            ProspectoCLS obj = new ProspectoCLS();
            using (var bd = new TalperBDEntities())
            {
                Prospecto oProsp = bd.Prospecto.Where(p => p.IdProspecto.Equals(id)).First();
                obj.idProspecto = oProsp.IdProspecto;
                obj.Nombre = oProsp.Nombre;
                obj.PrimerApellido = oProsp.PrimerApellido;
                obj.SegundoApellido = oProsp.SegundoApellido;
                obj.Calle = oProsp.Calle;
                obj.Numero = oProsp.numero;
                obj.Colonia = oProsp.Colonia;
                obj.CodigoPostal = oProsp.CodigoPostal;
                obj.Telefono = oProsp.Telefono;
                obj.RFC = oProsp.RFC;
                obj.Observaciones = oProsp.Observaciones;

            }

            return View(obj);
        }




    }
}
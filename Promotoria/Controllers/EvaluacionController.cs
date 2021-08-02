using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Promotoria.Models;

namespace Promotoria.Controllers
{
    public class EvaluacionController : Controller
    {
        // GET: Evaluacion
        public ActionResult Index()
        {
            List<ProspectoCLS> ListadeProspectosEvaluacion = null;

            using (var bd = new TalperBDEntities())
            {
                ListadeProspectosEvaluacion = (from prop in bd.Prospecto
                                               join std in bd.Estados
                                               on prop.IdEstado equals std.IdEstado
                                               select new ProspectoCLS
                                               {
                                                   idProspecto = prop.IdProspecto,
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
        public ActionResult PatallaInicialEvaluador()
        {

            return View();
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
        [HttpPost]
        public ActionResult VistaDatosCompletos(ProspectoCLS oProspecto)
        {

            if (!ModelState.IsValid)
            {
                return View(oProspecto);
            }
            else
            {

                int id = oProspecto.idProspecto;
                using (var bd = new TalperBDEntities())
                {
                    Prospecto obj = bd.Prospecto.Where(p => p.IdProspecto.Equals(id)).SingleOrDefault();
                    obj.Observaciones = oProspecto.Observaciones;
                    bd.SaveChanges();

                }
            }
            return RedirectToAction("Index");
        
        }

        public ActionResult ProspectoAceptado(int id)
        {
            using (var bd = new TalperBDEntities())
            {
                Prospecto obj = bd.Prospecto.Where(p => p.IdProspecto.Equals(id)).First();
                obj.IdEstado = 2;
                bd.SaveChanges();
            }
                return RedirectToAction("Index");
                
        }

        public ActionResult ProspectoRechazado(int id)
        {
            using (var bd = new TalperBDEntities())
            {
               
                Prospecto obj = bd.Prospecto.Where(p => p.IdProspecto.Equals(id)).First();
                obj.IdEstado = 3;
                //obj.Observaciones = obj2.Observaciones;
              
                bd.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        public ActionResult Editar(int id)
        {
            ProspectoCLS obj = new ProspectoCLS();
            using (var bd = new TalperBDEntities())
            {
                Prospecto oProspectos = bd.Prospecto.Where(p => p.IdProspecto.Equals(id)).First();
                obj.idProspecto = oProspectos.IdProspecto;
                obj.Nombre = oProspectos.Nombre;
                obj.PrimerApellido = oProspectos.PrimerApellido;
                obj.SegundoApellido = oProspectos.SegundoApellido;
                obj.Calle = oProspectos.Calle;
                obj.Numero = oProspectos.numero;
                obj.Colonia = oProspectos.Colonia;
                obj.CodigoPostal = oProspectos.CodigoPostal;
                obj.Telefono = oProspectos.Telefono;
                obj.RFC = oProspectos.RFC;
                obj.Observaciones = oProspectos.Observaciones;
            
            
            }
            return View(obj);
        
        }
        [HttpPost]
        public ActionResult Editar(ProspectoCLS objetoCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(objetoCLS);

            }

          
            using (var bd = new TalperBDEntities())
            {
                int idProspecto = objetoCLS.idProspecto;
                Prospecto oPros = bd.Prospecto.Where(p => p.IdProspecto.Equals(idProspecto)).First();
                
                oPros.Nombre = objetoCLS.Nombre;
                oPros.PrimerApellido = objetoCLS.PrimerApellido;
                oPros.SegundoApellido = objetoCLS.SegundoApellido;
                oPros.Calle = objetoCLS.Calle;
                oPros.numero = objetoCLS.Numero;
                oPros.Colonia = objetoCLS.Colonia;
                oPros.CodigoPostal = objetoCLS.CodigoPostal;
                oPros.Telefono = objetoCLS.Telefono;
                oPros.RFC = objetoCLS.RFC;
                oPros.IdEstado = 3;
                oPros.Observaciones = objetoCLS.Observaciones;
                
                bd.SaveChanges();
            
            }
            return RedirectToAction("Index");
        
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
                                idDoc = doc.IdDoc,
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



    }
}
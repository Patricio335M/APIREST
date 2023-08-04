using ServicioRestComida.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServicioRestComida.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<Models.comida> Get()
        {
            IEnumerable<Models.comida> lst;
            using (Models.ComidaEntities db= new Models.ComidaEntities())
            {
                 lst = db.comida.ToList();
            }
            return lst;
        }

        public Models.comida Get(int id)
        {
            using (Models.ComidaEntities db = new Models.ComidaEntities())
            {
                Models.comida comida = db.comida.FirstOrDefault(u => u.id == id);

                if (comida == null)
                {
                    throw new ArgumentException("No se encontro ningun usuario con el ID especificado");
                }

                return comida;
            }
        }

        public IHttpActionResult Post([FromBody] comida comida)
        {
            if (ModelState.IsValid)
            {
                using (ComidaEntities db = new ComidaEntities())
                {
                    db.comida.Add(comida);
                    db.SaveChanges();
                }
                return Ok(comida);
            }
            else
            {
                return BadRequest("Datos inválidos");
            }
        }

        public IHttpActionResult Put(int id, [FromBody] comida comida)
        {
            if (ModelState.IsValid)
            {
                using (ComidaEntities db = new ComidaEntities())
                {
                    comida existingUsuario = db.comida.FirstOrDefault(u => u.id == id);

                    if (existingUsuario == null)
                    {
                        return NotFound();
                    }

                    existingUsuario.nombre = comida.nombre;
                    existingUsuario.cantidad= comida.cantidad;
                    
                
                    
                    // Agrega más propiedades según tus necesidades.

                    db.SaveChanges();
                }

                return Ok(comida);
            }
            else
            {
                return BadRequest("Datos inválidos");
            }
        }

        public IHttpActionResult Delete(int id)
        {
            using (ComidaEntities db = new ComidaEntities())
            {
                comida comida= db.comida.FirstOrDefault(u => u.id == id);

                if (comida == null)
                {
                    return NotFound();
                }

                db.comida.Remove(comida);
                db.SaveChanges();

                return Ok(comida);
            }
        }

    }
}

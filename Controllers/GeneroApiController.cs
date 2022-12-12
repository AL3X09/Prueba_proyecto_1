using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_proyecto_1.Context;
using Prueba_proyecto_1.Models;

namespace Prueba_proyecto_1.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Genero")]
    [ApiController]
    public class GeneroApiController : ControllerBase
    {
        private readonly Proyecto1DbContext _context;

        public GeneroApiController(Proyecto1DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Devuelve toda la cantidad total de los datos enviados por la BD
        /// </summary>
        /// <returns>Un Conjunto de datos</returns>
        /// <remarks>API de Generos</remarks>
        /// <response code="200">La solicitud ha tenido éxito</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // GET: api/GeneroApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneroClass>>> GetGenero()
        {
            return await _context.Genero.ToListAsync();
        }

        /// <summary>
        /// Listado de un dato el id unico 
        /// </summary>
        /// <remarks>
        ///     GET
        ///     {
        ///        "id": "1"
        ///     }
        /// /api/Genero/{id}
        /// </remarks>
        /// <param name="id" example="1">ID del objeto a consultar</param>
        /// <response code="200">la solicitud ha tenido éxito</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // GET: api/GeneroApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneroClass>> GetGeneroClass(int id)
        {
            var generoClass = await _context.Genero.FindAsync(id);

            if (generoClass == null)
            {
                return NotFound();
            }

            return generoClass;
        }

        /// <summary>
        /// Listado de un dato el id unico 
        /// </summary>
        /// <remarks>
        ///     GET
        ///     {
        ///        "name": "t"
        ///     }
        /// /api/Genero/{name}
        /// </remarks>
        /// <response code="200">la solicitud ha tenido éxito</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // GET: api/GeneroApi/T
        [HttpGet("{name}")]
        public async Task<ActionResult<GeneroClass>> GetGeneroClassName(string name)
        {
            List<GeneroClass> generoClass = null;
            generoClass = await _context.Genero.Where(g => g.Genero.Equals(name)).ToListAsync();

            if (generoClass == null)
            {
                return NotFound();
            }

            return new JsonResult(generoClass);
        }

        /// <summary>
        /// Listado de un dato el id unico 
        /// </summary>
        /// <remarks>
        ///     GET
        ///     {
        ///        "id": "1",
        ///        "genero": "terror"
        ///     }
        /// /api/Genero/{id}
        /// </remarks>
        /// <param name="id" example="1">ID del objeto a consultar</param>
        /// <response code="200">la solicitud ha tenido éxito</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // GET: api/GeneroApi/5
        [HttpGet("{id},{genero}")]
        public async Task<ActionResult<GeneroClass>> GetGeneroClass(int id, string genero)
        {
            var generoClass = new GeneroClass();

            if (genero != null)
            {
                
                generoClass = await _context.Genero.FindAsync(genero);//Where(g => g.Id.Equals(id) && g.Genero.Equals(genero)).FirstOrDefaultAsync();
            }
            else if(id != null && id > 0)
            {
                generoClass = await _context.Genero.FindAsync(genero);
            }
            else if(id != null && genero != null)
            {
                generoClass = await _context.Genero.Where(g => g.Id.Equals(id) && g.Genero.Equals(genero)).FirstOrDefaultAsync();
            }

            if (generoClass == null)
            {
                return NotFound();
            }

            return generoClass;
        }

        /// <summary>
        /// Actualizar un dato el id unico 
        /// </summary>
        /// <remarks>
        ///  PUT
        ///     {
        ///        "id": "1",
        ///        "Genero": "Terror"
        ///     }
        /// </remarks>
        /// <param name="id" example="1">ID del objeto a actualizar</param>
        /// <param name="generoClass" example="terrro">Nombre  del objeto a actualizar</param>
        /// <response code="200">la solicitud ha tenido éxito</response>
        /// <response code="201">La solicitud ha dado lugar a la actualización de uno o más recursos.</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // PUT: api/GeneroApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeneroClass(int id, GeneroClass generoClass)
        {
            if (id != generoClass.Id)
            {
                return BadRequest();
            }

            _context.Entry(generoClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneroClassExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Insertar un dato a la BD
        /// </summary>
        /// <remarks>
        ///  POST
        ///     {
        ///        "Genero": "Terror"
        ///     }
        /// </remarks>
        /// <response code="200">la solicitud ha tenido éxito</response>
        /// <response code="201">La solicitud ha dado lugar a la creación de uno o más recursos.</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // POST: api/GeneroApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GeneroClass>> PostGeneroClass([FromForm] GeneroClass generoClass)
        {
            try
            {
                _context.Genero.Add(generoClass);
                await _context.SaveChangesAsync();

                
                var response = new
                {
                    status = 201,
                    error = "FALSE",
                    message = "Recurso creado satisfactoriamente"
                };
                
                return Ok(response);
                //return CreatedAtAction("GetGeneroClass", new { id = generoClass.Id }, generoClass);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// Borrar un dato el id unico 
        /// </summary>
        /// <remarks>
        ///  DELETE
        ///     {
        ///        "id": "1"
        ///     }
        /// </remarks>
        /// <param name="id" example="1">ID del objeto a consultar</param>
        /// <response code="200">la solicitud ha tenido éxito</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // DELETE: api/GeneroApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeneroClass(int id)
        {
            var generoClass = await _context.Genero.FindAsync(id);
            if (generoClass == null)
            {
                return NotFound();
            }

            _context.Genero.Remove(generoClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GeneroClassExists(int id)
        {
            return _context.Genero.Any(e => e.Id == id);
        }
    }
}

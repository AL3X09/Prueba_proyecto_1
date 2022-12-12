using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_proyecto_1.Context;
using Prueba_proyecto_1.Models;

namespace Prueba_proyecto_1.Controllers
{
    [Route("api/Autor")]
    [ApiController]
    public class AutorApiController : ControllerBase
    {
        private readonly Proyecto1DbContext _context;

        public AutorApiController(Proyecto1DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Devuelve toda la cantidad total de los datos enviados por la BD
        /// </summary>
        /// <returns>Un Conjunto de datos</returns>
        /// <remarks>API de Autor</remarks>
        /// <response code="200">La solicitud ha tenido éxito</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // GET: api/AutorApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorClass>>> GetAutor()
        {
            return await _context.Autor.ToListAsync();
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
        // GET: api/AutorApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AutorClass>> GetAutorClass(int id)
        {
            var autorClass = await _context.Autor.FindAsync(id);

            if (autorClass == null)
            {
                return NotFound();
            }

            return autorClass;
        }

        /// <summary>
        /// Listado de un dato el id unico 
        /// </summary>
        /// <remarks>
        ///     GET
        ///     {
        ///        "name": "t"
        ///     }
        /// </remarks>
        /// <response code="200">la solicitud ha tenido éxito</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // GET: api/AutorApi/t
        [HttpGet("{name}")]
        public async Task<ActionResult<AutorClass>> GetAutorClass(string name)
        {
            List<AutorClass> autorClass = null;
            autorClass = await _context.Autor.Where(a => a.Nombre.Equals(name)).ToListAsync();

            if (autorClass == null)
            {
                return NotFound();
            }

            return new JsonResult(autorClass);
        }

        /// <summary>
        /// Actualizar un dato el id unico 
        /// </summary>
        /// <remarks>
        ///  PUT
        ///     {
        ///        "id": "1",
        ///        "Autor": "Contoso"
        ///     }
        /// </remarks>
        /// <param name="id" example="1">ID del objeto a actualizar</param>
        /// <param name="autorClass" example="terrro">Nombre  del objeto a actualizar</param>
        /// <response code="200">la solicitud ha tenido éxito</response>
        /// <response code="201">La solicitud ha dado lugar a la actualización de uno o más recursos.</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // PUT: api/AutorApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutorClass(int id, AutorClass autorClass)
        {
            if (id != autorClass.Id)
            {
                return BadRequest();
            }

            _context.Entry(autorClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorClassExists(id))
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
        ///        "Autor": "Contoso"
        ///     }
        /// </remarks>
        /// <response code="200">la solicitud ha tenido éxito</response>
        /// <response code="201">La solicitud ha dado lugar a la creación de uno o más recursos.</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // POST: api/AutorApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<AutorClass>> PostAutorClass(AutorClass autorClass)
        {
            _context.Autor.Add(autorClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAutorClass", new { id = autorClass.Id }, autorClass);
        }*/

        /// <summary>
        /// Insertar un dato a la BD
        /// </summary>
        /// <remarks>
        ///  POST
        ///     {
        ///        "Autor": "Contoso"
        ///     }
        /// </remarks>
        /// <response code="200">la solicitud ha tenido éxito</response>
        /// <response code="201">La solicitud ha dado lugar a la creación de uno o más recursos.</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // POST: api/AutorApi
        [HttpPost]
        public async Task<ActionResult<AutorClass>> PostAutorClass([FromForm] AutorClass autorClass)
        {
            try
            {
                _context.Autor.Add(autorClass);
                await _context.SaveChangesAsync();


                var response = new
                {
                    status = 201,
                    error = "FALSE",
                    message = "Recurso creado satisfactoriamente"
                };

                return Ok(response);
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
        // DELETE: api/AutorApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutorClass(int id)
        {
            var autorClass = await _context.Autor.FindAsync(id);
            if (autorClass == null)
            {
                return NotFound();
            }

            _context.Autor.Remove(autorClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AutorClassExists(int id)
        {
            return _context.Autor.Any(e => e.Id == id);
        }
    }
}

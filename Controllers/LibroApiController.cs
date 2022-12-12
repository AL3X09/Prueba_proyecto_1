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
    [Route("api/Libro")]
    [ApiController]
    public class LibroApiController : ControllerBase
    {
        private readonly Proyecto1DbContext _context;

        public LibroApiController(Proyecto1DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Devuelve toda la cantidad total de los datos enviados por la BD
        /// </summary>
        /// <returns>Un Conjunto de datos</returns>
        /// <remarks>API de Libor</remarks>
        /// <response code="200">La solicitud ha tenido éxito</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // GET: api/LibroApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroClass>>> GetLibro()
        {
            return await _context.Libro.ToListAsync();
        }

        /// <summary>
        /// Listado de un dato el id unico 
        /// </summary>
        /// <remarks>
        ///     GET
        ///     {
        ///        "id": "1"
        ///     }
        /// </remarks>
        /// <param name="id" example="1">ID del objeto a consultar</param>
        /// <response code="200">la solicitud ha tenido éxito</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // GET: api/LibroApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibroClass>> GetLibroClass(int id)
        {
            var libroClass = await _context.Libro.FindAsync(id);

            if (libroClass == null)
            {
                return NotFound();
            }

            return libroClass;
        }

        /// <summary>
        /// Actualizar un dato el id unico 
        /// </summary>
        /// <remarks>
        ///  PUT
        ///     {
        ///        "id": "1",
        ///        "Titulo": "Contoso"
        ///        "año": "2000"
        ///        "Numero_paginas": "1"
        ///        "Autor": "1"
        ///        "Genero": "1"
        ///     }
        /// </remarks>
        /// <param name="id" example="1">ID del objeto a actualizar</param>
        /// <param name="libroClass" example="terrro">Nombre  del objeto a actualizar</param>
        /// <response code="200">la solicitud ha tenido éxito</response>
        /// <response code="201">La solicitud ha dado lugar a la actualización de uno o más recursos.</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // PUT: api/LibroApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibroClass(int id, LibroClass libroClass)
        {
            if (id != libroClass.Id)
            {
                return BadRequest();
            }

            _context.Entry(libroClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibroClassExists(id))
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
        ///        "id": "1",
        ///        "Titulo": "Contoso"
        ///        "año": "2000"
        ///        "Numero_paginas": "1"
        ///        "Autor": "1"
        ///        "Genero": "1"
        ///     }
        /// </remarks>
        /// <response code="200">la solicitud ha tenido éxito</response>
        /// <response code="201">La solicitud ha dado lugar a la creación de uno o más recursos.</response>
        /// <response code="400">Los datos de retorno son null</response>
        /// <response code="401">Desautorizado, no se ha proporcionado la llave de autenticación</response>
        /// <response code="500">Error en el servidor no responde, el servidor esta apagado o los servidores no retornan datos</response>
        // POST: api/LibroApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<LibroClass>> PostLibroClass(LibroClass libroClass)
        {
            _context.Libro.Add(libroClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibroClass", new { id = libroClass.Id }, libroClass);
        }*/

        [HttpPost]
        public async Task<ActionResult> PostLibroClass([FromForm] LibroClass libroClass)
        //public async Task<ActionResult> PostLibroClass([Bind("Id,Titulo,Anio,Numero_paginas,AutorId,GeneroId")] LibroClass libroClass)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Libro.Add(libroClass);
                    await _context.SaveChangesAsync();
                    var response = new
                    {
                        status = 201,
                        error = "FALSE",
                        message = "Recurso creado satisfactoriamente"
                    };
                    return Ok(response);

                }
                return Ok();
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
        // DELETE: api/LibroApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibroClass(int id)
        {
            var libroClass = await _context.Libro.FindAsync(id);
            if (libroClass == null)
            {
                return NotFound();
            }

            _context.Libro.Remove(libroClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibroClassExists(int id)
        {
            return _context.Libro.Any(e => e.Id == id);
        }
    }
}

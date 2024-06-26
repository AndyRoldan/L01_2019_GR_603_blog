﻿using L01_2019_603.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2019_603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {

        private readonly labContext _labContexto;

        public CalificacionesController(labContext UsuariosContexto)
        {
            _labContexto = UsuariosContexto;
        }
        ///<sumary>
        /// EndPoint que retorna el liustado de todos los Usuarios existentes
        /// </sumary>
        /// <returns></returns>
        [HttpGet]
        [Route("OBTENER TODO")]
        public IActionResult Get()
        {
            List<Calificaciones> listadoUsuarios = (from e in _labContexto.Calificaciones
                                              select e).ToList();

            if (listadoUsuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoUsuarios);
        }


        /// EndPoint que CREA los registros de una tablas 

        [HttpPost]
        [Route("AGREGAR")]
        public IActionResult GuardarEquipo([FromBody] Calificaciones calificaciones)
        {
            try
            {
                _labContexto.Calificaciones.Add(calificaciones);
                _labContexto.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// EndPoint que MODIFICAR los registros de una tablas
        /// 


        /* [HttpPut]
         [Route("ACTUALIZAR")]
         public IActionResult ActualizarEquipo(int id, [FromBody] Usuarios usuarioAmodificar)
         {
             ///Para actualizar un regisro, se obriene el registro original de la base de datos
             ///al cual alteraremos alguna propiedad
             Usuarios? equipoActual = (from e in _labContexto.usuarios
                                      where e.usuarioId == id
                                      select e).FirstOrDefault();

             ///Verificamos que estisa el registro segun su ID
             if (equipoActual == null)
             { return NotFound(); }

             ///Si se encuentra el registro, se alteran los campos modificables
             equipoActual.rolId = usuarioAmodificar.rolId;
             equipoActual.nombreUsuario = usuarioAmodificar.nombreUsuario;
             equipoActual.clave = usuarioAmodificar.clave;
             equipoActual.nombre = usuarioAmodificar.nombre;
             equipoActual.apellido = usuarioAmodificar.apellido;

             ///Se marca el registro como modificado en el contexto
             ///y se envia la modificacion a la base de datos
             _labContexto.Entry(equipoActual).State = EntityState.Modified;
             _labContexto.SaveChanges();
             return Ok(_labContexto);

         }*/

        [HttpPut]
        [Route("ACTUALIZAR")]
        public IActionResult ActualizarEquipo(int id, [FromBody] Calificaciones usuarioAmodificar)
        {
            try
            {
                // Buscar el usuario en la base de datos por su ID
                Calificaciones? equipoActual = _labContexto.Calificaciones.FirstOrDefault(e => e.calificacionId == id);

                // Verificar si se encontró el usuario
                if (equipoActual == null)
                {
                    return NotFound(); // Devolver HTTP 404 si no se encuentra el usuario
                }

                // Actualizar las propiedades del usuario con los nuevos valores
                equipoActual.calificacionId = usuarioAmodificar.calificacionId;
                equipoActual.publicacionId = usuarioAmodificar.publicacionId;
                equipoActual.usuarioId = usuarioAmodificar.usuarioId;
                equipoActual.calificacion = usuarioAmodificar.calificacion;

                // Marcar el usuario como modificado en el contexto de la base de datos
                _labContexto.Entry(equipoActual).State = EntityState.Modified;

                // Guardar los cambios en la base de datos
                _labContexto.SaveChanges();

                // Devolver HTTP 200 OK junto con el usuario actualizado
                return Ok(equipoActual);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la actualización
                // Devolver un error HTTP 500 y posiblemente un mensaje de error
                return StatusCode(500, $"Ocurrió un error al actualizar el usuario: {ex.Message}");
            }
        }


        /// EndPoint que ELIMANR los registros de una tablas
        /// 

        [HttpDelete]
        [Route("ELIMINAR")]
        public IActionResult EliminarEqipo(int id)
        {
            ///Para actualizar un regisro, se obriene el registro original de la base de datos
            ///al cual Eliminaremos
            Calificaciones? usuarios = (from e in _labContexto.Calificaciones
                                  where e.calificacion == id
                                  select e).FirstOrDefault();

            ///Verificamos que exista el registro segun su ID
            if (usuarios == null)
            { return NotFound(); }


            ///Ejecutamos la accion de elimnar el registro

            _labContexto.Calificaciones.Attach(usuarios);
            _labContexto.Calificaciones.Remove(usuarios);
            _labContexto.SaveChanges();

            return Ok(usuarios);

        }
        [HttpGet("FILTRAR POR UNA PUBLICACION EN ESPECIFICO")]
        public async Task<ActionResult<IEnumerable<Calificaciones>>> ObtenerCalificacionesPorPublicacion(int id)
        {
            var calificaciones = await _labContexto.Calificaciones
                .Where(c => c.publicacionId == id)
                .ToListAsync();

            if (calificaciones == null || calificaciones.Count == 0)
            {
                return NotFound();
            }

            return calificaciones;
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using tarea2.Models;

namespace tarea2.Controllers
{
    /// <summary>
    /// Controlador que gestiona el registro, edición y visualización de tareas.
    /// Las tareas se almacenan en un archivo JSON ubicado en App_Data.
    /// </summary>
    public class RegistroTareasController : Controller
    {
        // Ruta física del archivo JSON donde se guardan las tareas
        private readonly string rutaArchivo = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/tareas_registradas.json");

        /// <summary>
        /// Muestra el formulario para registrar una nueva tarea.
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Procesa el envío del formulario de registro.
        /// Valida el modelo y guarda la tarea en el archivo JSON.
        /// Devuelve respuesta JSON para integración con Ajax.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(TareaRegistrada tarea)
        {
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { exito = false, errores });
            }

            try
            {
                var tareasExistentes = new List<TareaRegistrada>();
                if (System.IO.File.Exists(rutaArchivo))
                {
                    var contenido = System.IO.File.ReadAllText(rutaArchivo);
                    tareasExistentes = JsonConvert.DeserializeObject<List<TareaRegistrada>>(contenido) ?? new List<TareaRegistrada>();
                }

                tareasExistentes.Add(tarea);

                var jsonActualizado = JsonConvert.SerializeObject(tareasExistentes, Formatting.Indented);
                System.IO.File.WriteAllText(rutaArchivo, jsonActualizado);

                return Json(new { exito = true });
            }
            catch (Exception)
            {
                return Json(new
                {
                    exito = false,
                    errores = new[] {
                        "Ocurrió un error al guardar la tarea. Intente nuevamente o contacte al soporte técnico."
                    }
                });
            }
        }

        /// <summary>
        /// Muestra la lista completa de tareas registradas.
        /// </summary>
        public ActionResult Listado()
        {
            var tareas = new List<TareaRegistrada>();

            if (System.IO.File.Exists(rutaArchivo))
            {
                var contenido = System.IO.File.ReadAllText(rutaArchivo);
                tareas = JsonConvert.DeserializeObject<List<TareaRegistrada>>(contenido) ?? new List<TareaRegistrada>();
            }

            return View(tareas);
        }

        /// <summary>
        /// Muestra el formulario de edición para una tarea existente.
        /// Se identifica por el título como clave temporal.
        /// </summary>
        public ActionResult Editar(string id)
        {
            if (string.IsNullOrEmpty(id)) return HttpNotFound();

            var tareas = new List<TareaRegistrada>();
            if (System.IO.File.Exists(rutaArchivo))
            {
                var contenido = System.IO.File.ReadAllText(rutaArchivo);
                tareas = JsonConvert.DeserializeObject<List<TareaRegistrada>>(contenido) ?? new List<TareaRegistrada>();
            }

            var tarea = tareas.Find(t => t.Titulo == id);
            if (tarea == null) return HttpNotFound();

            return View(tarea);
        }

        /// <summary>
        /// Procesa el formulario de edición.
        /// Actualiza la tarea en el archivo JSON si la validación es exitosa.
        /// Devuelve respuesta JSON para integración con Ajax.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Editar(TareaRegistrada tareaEditada)
        {
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { exito = false, errores });
            }

            var tareas = new List<TareaRegistrada>();
            if (System.IO.File.Exists(rutaArchivo))
            {
                var contenido = System.IO.File.ReadAllText(rutaArchivo);
                tareas = JsonConvert.DeserializeObject<List<TareaRegistrada>>(contenido) ?? new List<TareaRegistrada>();
            }

            var index = tareas.FindIndex(t => t.Titulo == tareaEditada.Titulo);
            if (index == -1)
            {
                return Json(new { exito = false, errores = new[] { "La tarea no fue encontrada." } });
            }

            tareas[index] = tareaEditada;

            var jsonActualizado = JsonConvert.SerializeObject(tareas, Formatting.Indented);
            System.IO.File.WriteAllText(rutaArchivo, jsonActualizado);

            return Json(new { exito = true });
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;
using tarea2.Models;

namespace tarea2.Controllers
{
    /// <summary>
    /// Controlador que gestiona el registro de tareas y su almacenamiento en archivo JSON.
    /// </summary>
    public class RegistroTareasController : Controller
    {
        // Ruta del archivo JSON donde se guardan las tareas
        private readonly string rutaArchivo = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/tareas_registradas.json");

        /// <summary>
        /// Muestra el formulario de registro de tareas.
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Recibe los datos del formulario, valida el modelo y guarda la tarea en archivo JSON.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TareaRegistrada tarea)
        {
            // Validación del modelo según DataAnnotations
            if (!ModelState.IsValid)
            {
                // Si hay errores, se devuelve la vista con mensajes de validación
                return View(tarea);
            }

            try
            {
                // Lista de tareas existente (si el archivo ya tiene contenido)
                var tareasExistentes = new List<TareaRegistrada>();

                if (System.IO.File.Exists(rutaArchivo))
                {
                    var contenido = System.IO.File.ReadAllText(rutaArchivo);
                    tareasExistentes = JsonConvert.DeserializeObject<List<TareaRegistrada>>(contenido) ?? new List<TareaRegistrada>();
                }

                // Agregar la nueva tarea a la lista
                tareasExistentes.Add(tarea);

                // Guardar la lista actualizada en el archivo JSON
                var jsonActualizado = JsonConvert.SerializeObject(tareasExistentes, Formatting.Indented);
                System.IO.File.WriteAllText(rutaArchivo, jsonActualizado);

                // Redirigir a una vista de confirmación o mostrar mensaje institucional
                TempData["MensajeExito"] = "La tarea fue registrada correctamente.";
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                // Manejo de errores con retroalimentación institucional
                ModelState.AddModelError("", "Ocurrió un error al guardar la tarea. Intente nuevamente o contacte al soporte.");
                return View(tarea);
            }
        }
        /// <summary>
        /// Muestra la lista de tareas registradas, leídas desde el archivo JSON.
        /// </summary>
        public ActionResult Listado()
        {
            // Crear una lista vacía para almacenar las tareas
            var tareas = new List<TareaRegistrada>();

            // Verificar si el archivo JSON existe
            if (System.IO.File.Exists(rutaArchivo))
            {
                // Leer el contenido del archivo
                var contenido = System.IO.File.ReadAllText(rutaArchivo);

                // Convertir el contenido JSON en una lista de objetos TareaRegistrada
                tareas = JsonConvert.DeserializeObject<List<TareaRegistrada>>(contenido) ?? new List<TareaRegistrada>();
            }

            // Enviar la lista de tareas a la vista
            return View(tareas);
        }

    }
}

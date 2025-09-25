using System;
using System.ComponentModel.DataAnnotations;

namespace tarea2.Models
{
    /// <summary>
    /// Modelo que representa una tarea registrada por el usuario.
    /// Se utiliza en formularios MVC y se valida tanto en cliente como en servidor.
    /// Incluye campos obligatorios, validación de formato y longitud, y valores por defecto.
    /// </summary>
    public class TareaRegistrada
    {
        /// <summary>
        /// Título breve de la tarea.
        /// Validación: obligatorio, mínimo 3 caracteres, máximo 100.
        /// Se utiliza como identificador temporal en edición.
        /// </summary>
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El título debe tener entre 3 y 100 caracteres.")]
        public string Titulo { get; set; }

        /// <summary>
        /// Descripción detallada del objetivo o contenido de la tarea.
        /// Validación: obligatorio, mínimo 10 caracteres, máximo 500.
        /// </summary>
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "La descripción debe tener entre 10 y 500 caracteres.")]
        public string Descripcion { get; set; }

        /// <summary>
        /// Fecha límite para completar la tarea.
        /// Validación: obligatorio, formato de fecha.
        /// </summary>
        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaVencimiento { get; set; }

        /// <summary>
        /// Nivel de prioridad asignado por el usuario.
        /// Valores esperados: Alta, Media, Baja.
        /// Validación: obligatorio.
        /// </summary>
        [Required(ErrorMessage = "La prioridad es obligatoria.")]
        public string Prioridad { get; set; }

        /// <summary>
        /// Lenguaje de programación utilizado en la tarea.
        /// Validación: obligatorio, mínimo 2 caracteres, máximo 100.
        /// Se muestra con etiqueta personalizada en formularios.
        /// </summary>
        [Required(ErrorMessage = "El lenguaje de programación es obligatorio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El lenguaje debe tener entre 2 y 100 caracteres.")]
        [Display(Name = "Lenguaje de programación utilizado")]
        public string Lenguaje { get; set; }

        /// <summary>
        /// Tipo de tarea según plataforma o enfoque técnico.
        /// Valores esperados: Consola, Web, API, Escritorio.
        /// Validación: obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El tipo de tarea es obligatorio.")]
        [Display(Name = "Tipo de tarea")]
        public string Tipo { get; set; }

        /// <summary>
        /// URL del repositorio asociado (GitHub, GitLab, etc.).
        /// Validación: obligatorio, formato de URL.
        /// Se muestra con etiqueta personalizada en formularios.
        /// </summary>
        [Required(ErrorMessage = "La URL del repositorio es obligatoria.")]
        [Url(ErrorMessage = "Ingresá una URL válida que comience con http:// o https://")]
        [Display(Name = "URL del repositorio")]
        public string UrlRepositorio { get; set; }

        /// <summary>
        /// Nombre del autor o responsable de la tarea.
        /// Validación: obligatorio, mínimo 3 caracteres, máximo 100.
        /// Se muestra con etiqueta personalizada en formularios.
        /// </summary>
        [Required(ErrorMessage = "El nombre del autor es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre del autor debe tener entre 3 y 100 caracteres.")]
        [Display(Name = "Autor de la tarea")]
        public string Autor { get; set; }

        /// <summary>
        /// Estado actual de la tarea.
        /// Valor por defecto: "Pendiente".
        /// Validación: obligatorio.
        /// Puede cambiarse a "Completada" o "Cancelada" en futuras extensiones.
        /// </summary>
        [Required(ErrorMessage = "El estado de la tarea es obligatorio.")]
        public string Estado { get; set; } = "Pendiente";
    }
}

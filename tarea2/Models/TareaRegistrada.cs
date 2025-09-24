using System;
using System.ComponentModel.DataAnnotations;

namespace tarea2.Models
{
    /// <summary>
    /// Modelo que representa una tarea registrada por el usuario.
    /// Se utiliza en formularios MVC y se valida tanto en cliente como en servidor.
    /// </summary>
    public class TareaRegistrada
    {
        /// <summary>
        /// Título breve de la tarea. Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no debe superar los 100 caracteres.")]
        public string Titulo { get; set; }

        /// <summary>
        /// Descripción detallada de la tarea. Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción no debe superar los 500 caracteres.")]
        public string Descripcion { get; set; }

        /// <summary>
        /// Fecha límite para completar la tarea. Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaVencimiento { get; set; }

        /// <summary>
        /// Nivel de prioridad: Alta, Media, Baja. Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "La prioridad es obligatoria.")]
        public string Prioridad { get; set; }

        /// <summary>
        /// Estado actual de la tarea. Valor por defecto: Pendiente.
        /// </summary>
        public string Estado { get; set; } = "Pendiente";
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Curso
    {
        public int CursoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public byte[] FotoPortada { get; set; }
        public Precio Precio { get; set; }
        
        public ICollection<Comentario> Comentario { get; set; }
        public ICollection<CursoInstructor> CursoInstructor { get; set; }
    }

}

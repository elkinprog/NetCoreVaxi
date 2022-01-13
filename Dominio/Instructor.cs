using System.Collections.Generic;
using System;
namespace Dominio
{
    public class Instructor
    {
        public Guid InstructorId {get;set;}
        public string Nombre {get;set;}
        public string Apellido {get;set;}
        public int Grado {get;set;}
        public byte[] FotoPerfil{get;set;}

        public ICollection<CursoInstructor> CursoInstructor {get;set;}
        
    }
}

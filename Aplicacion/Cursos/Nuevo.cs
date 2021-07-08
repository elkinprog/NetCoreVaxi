using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion
{
    public class Nuevo     {
        public class Ejecuta: IRequest
        {
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime  FechaPublicacion { get; set; }
        }


        

  
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ConnectionContext _context;
            public Manejador(ConnectionContext context)
            {
                this._context= context;
            }
 
        
            public async  Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {        
                var curso = new Curso
                {     
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion   
                };

                _context.Curso.Add(curso);
               var valor = await _context.SaveChangesAsync(); 
               if(valor > 0){
                    return Unit.Value;
               }
                throw  new  Exception  ("No se realiza la accion de crear nuevo dato");

            }           
        }
    }
}        


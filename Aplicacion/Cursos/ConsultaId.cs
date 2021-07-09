using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<Curso> {
            public int Id {get;set;}
        }

        public class Manejador: IRequestHandler<CursoUnico, Curso>
        {
            private readonly ConnectionContext _context;
            public Manejador(ConnectionContext context)
            {
                this._context = context;
            } 

            public async Task<Curso> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
               var curso = await  _context.Curso.FindAsync(request.Id);

               if(curso == null){
                    //throw new Exception("El curso no existe");
                    throw new  ManejadorExcepcion(HttpStatusCode.NotFound, new {curso= "El curso no existe"});
                }
               return curso;
            }
        }
    }  
}
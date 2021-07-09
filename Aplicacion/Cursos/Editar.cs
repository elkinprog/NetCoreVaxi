using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Editar
    { 
          public class Ejecuta: IRequest
        {
            public int CursoId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime ?  FechaPublicacion { get; set; }
        }


         public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(X => X.Titulo).NotEmpty();
                RuleFor(X => X.Descripcion).NotEmpty();
                RuleFor(X => X.FechaPublicacion).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ConnectionContext _context;

            public Manejador(ConnectionContext context)
            {
                _context= context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.CursoId);
                if(curso == null){
                    //throw new Exception("El curso no existe");
                    throw new  ManejadorExcepcion(HttpStatusCode.NotFound, new {curso= "El curso no existe"});
                }
                curso.Titulo=               request.Titulo              ?? curso.Titulo;
                curso.Descripcion=          request.Descripcion         ?? curso.Descripcion;
                curso.FechaPublicacion=     request.FechaPublicacion    ?? curso.FechaPublicacion;

                var resultado = await _context.SaveChangesAsync();
                if(resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se guardaron los cambios en el curso");

            }
        }
    }
}   
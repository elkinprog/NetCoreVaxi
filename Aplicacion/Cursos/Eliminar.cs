using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest {
            public int Id { get; set; }
        }



        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ConnectionContext _context;
            public Manejador(ConnectionContext context)
            {
                _context = context;
            }


            public async  Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso =  await _context.Curso.FindAsync(request.Id);
                if(curso == null)
                {
                    throw new Exception("No se puede eliminar curso");
                }
                _context.Remove(curso);

                var resultado =  await _context.SaveChangesAsync();
                 if(resultado > 0)
                 {
                     return Unit.Value;
                 }

                 throw new Exception("No se guardaron los cambios");
            }
        }




    }
}
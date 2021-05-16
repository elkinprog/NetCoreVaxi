using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion;
using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace webAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CursosController(IMediator mediator )
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get(){
            return await  _mediator.Send(new Consulta.ListaCursos());  
        }

        [HttpGet("{id}")]   
        public async Task<ActionResult<Curso>> GetId(int id){  
             return await _mediator.Send(new ConsultaId.CursoUnico{Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Nuevo.Ejecuta data){
            return await _mediator.Send(data);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(int id, Editar.Ejecuta data){

            data.CursoId = id; 
             return await _mediator.Send(data); 
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(int id){
            return await _mediator.Send(new Eliminar.Ejecuta{Id= id});
        }


        
    } 
}
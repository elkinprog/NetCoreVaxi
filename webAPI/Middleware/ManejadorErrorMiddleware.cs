using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Loggin;
using System.Threading.Tasks;


namespace webAPI.Middleware
{
    public class ManejadorErrorMiddleware
    { 
        private readonly RequestDelegate _next;
        private readonly ILogger<ManejadorErrorMiddleware> _logger;
          public ManejadorErrorMiddleware(RequestDelegate next, ILogger<ManejadorErrorMiddleware> logger)
          {
            _next = next;
            _logger = logger;
          }

          public async Task Invocar(HttpContext context)
          {
              try
              {
                  await _next(context);
              }
              catch (Exception ex)
              {
                   await  ManejadorExcepcionAsincrono(context, ex, _logger);
              }
          }

          private Task ManejadorExcepcionAsincrono(HttpContext context, Exception ex, ILogger<ManejadorErrorMiddleware> logger)
            {
                  object  errores = null; 
                    switch(ex){

                        case ManejadorExcepcion me : 

                        logger.LogError(ex, "Manejador Error");
                        errores = me.Errores; 
                        context.Response.StatusCode = (int)me.Codigo; 
                        break; 
                    }
            }
    }
}
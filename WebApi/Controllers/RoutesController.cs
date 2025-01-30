using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [SwaggerTag("Operações para gerenciamento e consulta de rotas de viagem")]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Registra uma nova rota",
            Description = "Adiciona uma nova rota ao sistema com origem, destino e custo"
        )]
        [SwaggerResponse(200, "Rota registrada com sucesso")]
        [SwaggerResponse(400, "Dados inválidos", typeof(ProblemDetails))]
        public async Task<IActionResult> AddRoute(
            [FromBody]
            [SwaggerRequestBody("Dados da nova rota", Required = true)]
            RouteInput routeInput)
        {
            await _routeService.AddRoute(routeInput);
            return Ok();
        }

        [HttpGet("best-route")]
        [SwaggerOperation(
            Summary = "Encontra a melhor rota",
            Description = "Calcula a rota mais barata entre dois pontos considerando todas as conexões disponíveis"
        )]
        [SwaggerResponse(200, "Melhor rota encontrada", typeof(BestRouteResponse))]
        [SwaggerResponse(404, "Rota não encontrada")]
        [ProducesResponseType(typeof(BestRouteResponse), 200)]
        public async Task<IActionResult> GetBestRoute(
            [FromQuery]
            [SwaggerParameter("Aeroporto de origem (3 caracteres)", Required = true)]
            string origin,

            [FromQuery]
            [SwaggerParameter("Aeroporto de destino (3 caracteres)", Required = true)]
            string destination)
        {
            var result = await _routeService.GetBestRoute(origin, destination);
            return result != null
                ? Ok(result)
                : NotFound("Rota não encontrada");
        }
    }
}
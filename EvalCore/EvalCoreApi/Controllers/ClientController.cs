using EvalCore.Entities;
using EvalCore.Interfaces.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvalCoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientService _clientService;


        public ClientController(ILogger<ClientController> logger, IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        //todos
        [HttpGet()]
        public async Task<ActionResult<Response<IEnumerable<Client>>>> GetAll()
        {
            try
            {
                var response = Response<IEnumerable<Client>>.CreateSuccessResponse(await _clientService.GetAllAsync());
                if (response.Data == null)
                {
                    return NotFound("No se encontraron datos");
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, this.ControllerContext.RouteData.Values["controller"].ToString());
                return BadRequest(Response<IEnumerable<Client>>.CreateFatalResponse(ex.Message));
            }
        }

        //por id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Response<Client>>> Get(int id)
        {
            try
            {
                var response = Response<Client>.CreateSuccessResponse(await _clientService.GetByIdAsync(id));

                if (response.Data == null)
                {
                    return NotFound("No se encontro cliente al id asociado");
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, ControllerContext.RouteData.Values["controller"].ToString());
                return BadRequest(Response<Client>.CreateFatalResponse(ex.Message));
            }
        }

        //insertar
        [HttpPost]
        public async Task<ActionResult<Response<string>>> CrearCliente([FromBody] Client obj)
        {
            try
            {
                var client = await _clientService.GetByIdAsync(obj.IdClient);
                if (client == null)
                {
                    await _clientService.AddAsync(obj);
                    return Ok("Registro agregado exitosamente");
                }
                return BadRequest($"Ya existe el Cliente Id {obj.IdClient}");
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, this.ControllerContext.RouteData.Values["controller"].ToString());
                return BadRequest(Response<string>.CreateFatalResponse(ex.Message));
            }

        }

        //updatear
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Response<Client>>> UpdateClient(int id, [FromBody] Client clientToUpdate)
        {
            try
            {
                var client = await _clientService.GetByIdAsync(id);
                if (client != null)
                {
                    await _clientService.Update(id, clientToUpdate);
                    return Ok(clientToUpdate);
                }
                return NotFound("No se encontro cliente al id asociado");

            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, ControllerContext.RouteData.Values["controller"].ToString());
                return BadRequest(Response<Client>.CreateFatalResponse(ex.Message));
            }
        }
        //delete
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<Response<string>>> Delete(int id)
        {
            try
            {
                var response = Response<Client>.CreateSuccessResponse(await _clientService.GetByIdAsync(id));
                if (response.Data == null)
                {
                    return NotFound(Response<string>.CreateNotFoundResponse("No se encontro cliente al id asociado"));
                }
                await _clientService.Remove(response.Data);
                return Ok("Cliente Eliminado");
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, ControllerContext.RouteData.Values["controller"].ToString());
                return BadRequest(Response<string>.CreateFatalResponse(ex.Message));
            }
        }
    }
}

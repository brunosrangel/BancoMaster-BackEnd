using BancoMaster.Domain.Model;
using BancoMaster.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotasController : ControllerBase
    {
        private readonly IGerenciarRotas _gerenciarRotas;
        private readonly ILogger<RotasController> _logger;

        public RotasController(IGerenciarRotas gerenciarRotas, ILogger<RotasController> logger)
        {
            _gerenciarRotas = gerenciarRotas;
            _logger = logger;
        }

        // GET: api/rotas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<rotas>>> GetRotas()
        {
            try
            {
                var rotas = await _gerenciarRotas.ConsultaRota();
                return Ok(rotas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter rotas: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao processar a solicitação.");
            }
        }

        // GET: api/rotas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<rotas>> GetRotaPorId(int id)
        {
            try
            {
                var rota = await _gerenciarRotas.ConsultaRotaPorId(id);

                if (rota == null)
                {
                    return NotFound();
                }

                return rota;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter rota com ID {id}: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao processar a solicitação.");
            }
        }

        // POST: api/rotas
        [HttpPost]
        public async Task<ActionResult<rotas>> SalvarRota(rotas rota)
        {
            try
            {
                await _gerenciarRotas.CriarRotas(rota);
                return CreatedAtAction(nameof(SalvarRota), new { id = rota.IdRota }, rota);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar rota: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao processar a solicitação.");
            }
        }

        // PUT: api/rotas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarRota(int id, rotas rota)
        {
            try
            {
                if (id != rota.IdRota)
                {
                    return BadRequest();
                }

                await _gerenciarRotas.AtualizarRota(rota);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar rota com ID {id}: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao processar a solicitação.");
            }
        }

        // DELETE: api/rotas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarRota(int id)
        {
            try
            {
                var rota = await _gerenciarRotas.ConsultaRotaPorId(id);
                if (rota == null)
                {
                    return NotFound();
                }

                await _gerenciarRotas.DeletarRotaAsync(rota);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir rota com ID {id}: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao processar a solicitação.");
            }
        }
        [HttpPost("ConsultarRotaMaisBarata")]
        public async Task<IActionResult> ConsultarRotaMaisBarata([FromBody] ConsultaModel consulta)
        {
            try
            {
                var rotas = await _gerenciarRotas.CalculoRotaMaisBarata(consulta);
                var srtMsg = $"Resposta: {rotas.trajeto} ao custo de ${rotas.CustoViagem}";
                return Ok(new { mensagem = srtMsg });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter rotas: {ex.Message}");
                return (OkObjectResult)StatusCode(500, "Ocorreu um erro ao processar a solicitação.");
            }
        }
    }
}

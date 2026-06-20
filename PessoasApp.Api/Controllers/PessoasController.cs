using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PessoasApp.Api.Contexts;
using PessoasApp.Api.Entities;
using System.Linq.Expressions;
using System.Text.Json;

namespace PessoasApp.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController(ILogger<PessoasController> logger) : ControllerBase {

        private readonly DataContext _dataContext = new DataContext();

        [HttpPost]
        public async Task<IActionResult> CriarAsync([FromBody] PessoaDto dto) {

            try {

                var pessoa = new Pessoa {

                    Nome = dto.Nome,
                    Email = dto.Email

                };

                await _dataContext.AddAsync(pessoa);
                await _dataContext.SaveChangesAsync();

                logger.LogInformation($"Pessoa cadastrada: {JsonSerializer.Serialize(pessoa)}");

                return StatusCode(201, pessoa);

            } catch (Exception e) {

                logger.LogError("Falha ao cadastrar pessoa: " + e.Message);

                return BadRequest(e.Message);

            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> AtualizarParcialAsync(Guid id, [FromBody] PessoaDto dto) {

            try {

                var pessoa = await _dataContext.Pessoas.FindAsync(id);

                if (pessoa == null) {

                    return NotFound(new {
                        mensagem = "Pessoa não encontrada."
                    });
                }

                if (!string.IsNullOrEmpty(dto.Nome)) {
                    pessoa.Nome = dto.Nome;
                }

                if (!string.IsNullOrEmpty(dto.Email)) {
                    pessoa.Email = dto.Email;
                }

                _dataContext.Update(pessoa);
                await _dataContext.SaveChangesAsync();

                logger.LogInformation($"Pessoa atualizada: {JsonSerializer.Serialize(pessoa)}");

                return StatusCode(200, pessoa);

            } catch (Exception e) {

                logger.LogError("Falha ao atualizar pessoa: " + e.Message);

                return BadRequest(e.Message);

            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] PessoaDto dto) {

            try {

                var pessoa = await _dataContext.Pessoas.FindAsync(id);

                if (pessoa == null) {

                    return NotFound(new {
                        mensagem = "Pessoa não encontrada."
                    });
                }

                pessoa.Nome = dto.Nome;
                pessoa.Email = dto.Email;

                _dataContext.Update(pessoa);
                await _dataContext.SaveChangesAsync();

                logger.LogInformation($"Pessoa atualizada: {JsonSerializer.Serialize(pessoa)}");

                return Ok(pessoa);

            } catch (Exception e) {

                logger.LogError("Falha ao atualizar pessoa: " + e.Message);

                return BadRequest(e.Message);

            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosAsync() {

            try {

                var pessoas = await _dataContext.Pessoas
                    .AsNoTracking()
                    .OrderBy(p => p.Nome)
                    .ToListAsync();

                logger.LogInformation($"Lista de pessoas: {JsonSerializer.Serialize(pessoas)}");

                return Ok(pessoas);


            } catch (Exception e) {

                logger.LogError("Falha ao exibir pessoas: " + e.Message);

                return BadRequest(e.Message);

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorIdAsync(Guid id) {

            try {

                var pessoa = await _dataContext.Pessoas.FindAsync(id);

                if (pessoa == null) {

                    return NotFound(new {
                        mensagem = "Pessoa não encontrada."
                    });
                }

                logger.LogInformation($"Pessoa exibida: {JsonSerializer.Serialize(pessoa)}");

                return Ok(pessoa);

            } catch (Exception e) {

                logger.LogError("Falha ao exibir a pessoa informada: " + e.Message);

                return BadRequest(e.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirAsync(Guid id) {

            try {

                var pessoa = await _dataContext.Pessoas.FindAsync(id);

                if (pessoa == null) {

                    return NotFound(new {
                        mensagem = "Pessoa não encontrada."
                    });

                }

                _dataContext.Remove(pessoa);
                await _dataContext.SaveChangesAsync();

                logger.LogInformation($"Pessoa excluída com sucesso: {JsonSerializer.Serialize(pessoa)}");

                return NoContent();



            } catch (Exception e) {

                logger.LogError("Falha ao excluir a pessoa informada: " + e.Message);

                return BadRequest(e.Message);
            }

        }
    }

    public record PessoaDto(

        string Nome,
        string Email

        );
}

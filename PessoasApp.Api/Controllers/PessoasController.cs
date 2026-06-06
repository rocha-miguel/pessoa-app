using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PessoasApp.Api.Contexts;
using PessoasApp.Api.Entities;
using System.Linq.Expressions;

namespace PessoasApp.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase {

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


                return StatusCode(201, pessoa);

            } catch (ApplicationException e) {

                return BadRequest(e.Message);

            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] PessoaDto dto) {

            try {

                var pessoa = _dataContext.Pessoas.Find(id);

                if (pessoa == null) {

                    return NotFound(new {
                        mensagem = "Pessoa não encontrada."
                    });
                }

                pessoa.Nome = dto.Nome;
                pessoa.Email = dto.Email;

                _dataContext.Update(pessoa);
                await _dataContext.SaveChangesAsync();

                return Ok(pessoa);

            } catch (ApplicationException e) {

                return BadRequest(e.Message);

            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosAsync() {

            try {

                var pessoas = _dataContext.Pessoas.ToList();

                return Ok(pessoas);


            } catch (ApplicationException e) {

                return BadRequest(e.Message);

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorIdAsync(Guid id) {

            try {

                var pessoa = _dataContext.Pessoas.Find(id);

                if (pessoa == null) {

                    return NotFound(new {
                        mensagem = "Pessoa não encontrada."
                    });
                }

                return Ok(pessoa);

            } catch (ApplicationException e) {

                return BadRequest(e.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirAsync(Guid id) {

            try {

                var pessoa = _dataContext.Pessoas.Find(id);

                if (pessoa == null) {

                    return NotFound(new {
                        mensagem = "Pessoa não encontrada."
                    });

                }

                _dataContext.Remove(pessoa);
                await _dataContext.SaveChangesAsync();

                return NoContent();



            } catch (ApplicationException e) {

                return BadRequest(e.Message);
            }

        }
    }

    public record PessoaDto(

        string Nome,
        string Email

        );
}

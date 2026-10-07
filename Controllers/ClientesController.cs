using Microsoft.AspNetCore.Mvc;
using LHPets.Models;

namespace LHPets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        // Lista estática para simular o banco de dados em memória
        private static readonly List<Cliente> _clientes = new List<Cliente>
        {
            new Cliente { Id = 1, Nome = "Mariana do Nascimento", Cpf = "123.456.789-00", Email = "Mariana@gmail.com" },
            new Cliente { Id = 2, Nome = "Marcos da Silva", Cpf = "987.654.321-11", Email = "Marcos@gmail.com" }
        };

        // GET /api/clientes
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            return Ok(_clientes);
        }

        // POST /api/clientes
        [HttpPost]
        public ActionResult<Cliente> Post([FromBody] Cliente novoCliente)
        {
            if (novoCliente == null)
            {
                return BadRequest("Dados do cliente inválidos.");
            }

            // Atribui um novo ID simulado baseado no último item
            novoCliente.Id = _clientes.Count > 0 ? _clientes.Max(c => c.Id) + 1 : 1;
            
            _clientes.Add(novoCliente);

            return CreatedAtAction(nameof(Get), new { id = novoCliente.Id }, novoCliente);
        }

        // Método auxiliar para os outros controladores checarem se o cliente existe
        public static bool ExisteCliente(int id)
        {
            return _clientes.Any(c => c.Id == id);
        }
    }
}

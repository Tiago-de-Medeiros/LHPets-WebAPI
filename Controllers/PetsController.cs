using Microsoft.AspNetCore.Mvc;
using LHPets.Models;

namespace LHPets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        // Lista estática para simular o banco de dados de pets em memória
        private static readonly List<Pet> _pets = new List<Pet>
        {
            new Pet { Id = 1, Nome = "Totó", Especie = "Cachorro", Raca = "Vira-Lata", ClienteId = 1 },
            new Pet { Id = 2, Nome = "Mel", Especie = "Gato", Raca = "Vira-Lata", ClienteId = 1 },
            new Pet { Id = 3, Nome = "Chico", Especie = "Cachorro", Raca = "PitBull", ClienteId = 2 }
        };

        // GET /api/pets
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get()
        {
            return Ok(_pets);
        }

        // POST /api/pets
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet novoPet)
        {
            if (novoPet == null)
            {
                return BadRequest("Dados do pet inválidos.");
            }

            // Validação: Verifica se o ClienteId informado realmente existe na lista de clientes
            if (!ClientesController.ExisteCliente(novoPet.ClienteId))
            {
                return BadRequest($"Não foi possível cadastrar. O ClienteId {novoPet.ClienteId} não existe.");
            }

            novoPet.Id = _pets.Count > 0 ? _pets.Max(p => p.Id) + 1 : 1;
            _pets.Add(novoPet);

            return CreatedAtAction(nameof(Get), new { id = novoPet.Id }, novoPet);
        }

        // GET /api/pets/cliente/{clienteId}
        [HttpGet("cliente/{clienteId}")]
        public ActionResult<IEnumerable<Pet>> GetPorCliente(int clienteId)
        {
            // Filtra os pets pelo ID do dono
            var petsDoCliente = _pets.Where(p => p.ClienteId == clienteId).ToList();
            
            return Ok(petsDoCliente);
        }
    }
}

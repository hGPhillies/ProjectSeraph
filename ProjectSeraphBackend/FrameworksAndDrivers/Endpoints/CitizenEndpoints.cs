using Microsoft.AspNetCore.Mvc;
using ProjectSeraphBackend.Application.Interfaces;
using ProjectSeraphBackend.Domain;

namespace ProjectSeraphBackend.FrameworksAndDrivers.Endpoints
{

    [ApiController]
    [Route("api/[controller]")]
    public class CitizenEndpoints : ControllerBase
    {
        //Leftover class for future citizen related endpoints
        public void MapCitizenEndpoints()
        {
        }

        private readonly ICitizenRepository _citizenRepository;
        // Constructor to inject the repository
        public CitizenEndpoints(ICitizenRepository citizenRepository)
        {
            _citizenRepository = citizenRepository;

        }

        // Endpoint to get all citizens
        public async Task<ActionResult<IEnumerable<Citizen>>> GetAll()
        {
            var citizens = await _citizenRepository.GetAllAsync();
            return Ok(citizens);
        }

        // Endpoint to get a citizen by ID
        public async Task<ActionResult<Citizen>> GetById(string citizenID)
        {
            var citizen = await _citizenRepository.GetByIdAsync(citizenID);
            if (citizen == null)
                return NotFound();
            return Ok(citizen);
        }

        // Endpoint to create a new citizen
        [HttpPost]
        public async Task<ActionResult<Citizen>> Create(Citizen citizen)
        {
            citizen.citizenID = string.Empty; // Let repository generate ID
            var created = await _citizenRepository.CreateAsync(citizen);
            return CreatedAtAction(nameof(GetById), new { citizenID = created.citizenID }, created);
        }
    }
}

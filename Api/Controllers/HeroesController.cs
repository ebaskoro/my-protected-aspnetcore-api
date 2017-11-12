using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Data;
using Api.Models;


namespace Api.Controllers
{

    /// <summary>
    /// Heroes controller.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class HeroesController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHeroRepository _heroRepository;


        /// <summary>
        /// Creates a new controller.
        /// </summary>
        /// <param name="context">Unit of work to use.</param>
        public HeroesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _heroRepository = unitOfWork.HeroRepository;
        }


        /// <summary>
        /// Gets all heroes.
        /// </summary>
        /// <returns>Collection of heroes.</returns>
        public async Task<IActionResult> GetAllHeroesAsync()
        {
            var heroes = await _heroRepository.GetAllAsync();
            var heroModels = new List<HeroModel>();
            heroes
                .ToList()
                .ForEach(hero => heroModels.Add(new HeroModel
                {
                    Id = hero.Id,
                    Name = hero.Name
                }));

            return Ok(heroModels);
        }


        /// <summary>
        /// Gets a hero by her ID.
        /// </summary>
        /// <param name="id">ID to look up.</param>
        /// <returns>The hero if found or NotFound otherwise.</returns>
        [HttpGet("{id}", Name = "GetHeroById")]
        public async Task<IActionResult> GetHeroByIdAsync(long id)
        {
            var foundHero = await _heroRepository.GetByIdAsync(id);

            if (foundHero == null)
            {
                return NotFound();
            }

            var heroModel = new HeroModel
            {
                Id = foundHero.Id,
                Name = foundHero.Name
            };

            return Ok(heroModel);
        }


        /// <summary>
        /// Gets heroes having matching name.
        /// </summary>
        /// <param name="name">Name to look up.</param>
        /// <returns>The matching heroes or all if no name specified.</returns>
        [HttpGet]
        public async Task<IActionResult> SearchHeroesByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await GetAllHeroesAsync();
            }

            var matchingHeroes = await _heroRepository.SearchByNameAsync(name);
            var heroModels = new List<HeroModel>();
            matchingHeroes
                .ToList()
                .ForEach(hero => heroModels.Add(new HeroModel
                {
                    Id = hero.Id,
                    Name = hero.Name
                }));

            return Ok(heroModels);
        }


        /// <summary>
        /// Creates a hero.
        /// </summary>
        /// <param name="heroModel">Hero to create.</param>
        /// <returns>The newly created hero.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateHeroAsync([FromBody] HeroModel heroModel)
        {
            if (heroModel == null
                || false == ModelState.IsValid)
            {
                return BadRequest();
            }

            var heroToAdd = new Hero
            {
                Name = heroModel.Name
            };
            await _heroRepository.AddAsync(heroToAdd);
            await _unitOfWork.SaveAsync();
            heroModel.Id = heroToAdd.Id;

            return CreatedAtRoute("GetHeroById", new { id = heroToAdd.Id }, heroModel);
        }


        /// <summary>
        /// Updates a hero.
        /// </summary>
        /// <param name="id">ID of the hero to update.</param>
        /// <param name="heroModel">Hero to update.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHeroAsync(long id, [FromBody] HeroModel heroModel)
        {
            if (heroModel == null
                || heroModel.Id != id)
            {
                return BadRequest();
            }

            var foundHero = await _heroRepository.GetByIdAsync(id);
            
            if (foundHero == null)
            {
                return NotFound();
            }

            foundHero.Name = heroModel.Name;
            await _unitOfWork.SaveAsync();

            return NoContent();
        }


        /// <summary>
        /// Deletes a hero.
        /// </summary>
        /// <param name="id">ID of the hero to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeroAsync(long id)
        {
            var heroToDelete = await _heroRepository.GetByIdAsync(id);
            
            if (heroToDelete == null)
            {
                return NotFound();
            }

            _heroRepository.Remove(heroToDelete);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
        
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Api.Data
{

    public class HeroRepository : IHeroRepository
    {

        private readonly HeroContext _context;
        private bool _disposed = false;


        public HeroRepository(HeroContext context)
        {
            _context = context;
        }


        public IEnumerable<Hero> GetAll()
        {
            var heroes = _context
                .Heroes
                .ToList();

            return heroes;
        }


        public async Task<IEnumerable<Hero>> GetAllAsync()
        {
            var heroes = await _context
                .Heroes
                .ToListAsync();

            return heroes;
        }


        public Hero GetById(long id)
        {
            var theHero = _context
                .Heroes
                .FirstOrDefault(hero => hero.Id == id);

            return theHero;
        }


        public async Task<Hero> GetByIdAsync(long id)
        {
            var theHero = await _context
                .Heroes
                .FirstOrDefaultAsync(hero => hero.Id == id);

            return theHero;
        }


        public IEnumerable<Hero> SearchByName(string nameToMatch)
        {
            nameToMatch = nameToMatch.ToLower();
            var matchingHeroes = _context
                .Heroes
                .Where(hero => hero.Name.ToLower().Contains(nameToMatch))
                .ToList();
            
            return matchingHeroes;
        }


        public async Task<IEnumerable<Hero>> SearchByNameAsync(string nameToMatch)
        {
            nameToMatch = nameToMatch.ToLower();
            var matchingHeroes = await _context
                .Heroes
                .Where(hero => hero.Name.ToLower().Contains(nameToMatch))
                .ToListAsync();

            return matchingHeroes;
        }


        public void Add(Hero heroToAdd)
        {
            _context.Heroes.Add(heroToAdd);
        }


        public async Task AddAsync(Hero heroToAdd)
        {
            await _context.Heroes.AddAsync(heroToAdd);
        }


        public void Remove(Hero heroToRemove)
        {
            _context.Heroes.Remove(heroToRemove);
        }

        
        public void RemoveById(long id)
        {
            var heroToRemove = GetById(id);

            if (heroToRemove != null)
            {
                Remove(heroToRemove);
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this._disposed = true;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
    
}

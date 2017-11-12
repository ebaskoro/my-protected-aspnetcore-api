using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Api.Data
{

    /// <summary>
    /// Hero repository.
    /// </summary>
    public interface IHeroRepository : IDisposable
    {

        /// <summary>
        /// Gets all heroes.
        /// </summary>
        /// <returns>Collection of all heroes.</returns>
        IEnumerable<Hero> GetAll();


        /// <summary>
        /// Gets all heroes.
        /// </summary>
        /// <returns>Collection of all heroes.</returns>
        Task<IEnumerable<Hero>> GetAllAsync();


        /// <summary>
        /// Gets a hero by her ID.
        /// </summary>
        /// <param name="id">The ID to look up.</param>
        /// <returns>The hero if found or NULL otherwise.</returns>
        Hero GetById(long id);


        /// <summary>
        /// Gets a hero by her ID.
        /// </summary>
        /// <param name="id">The ID to look up.</param>
        /// <returns>The hero if found or NULL otherwise.</returns>
        Task<Hero> GetByIdAsync(long id);


        /// <summary>
        /// Searches for heroes matching a name.
        /// </summary>
        /// <param name="nameToMatch">The name to match.</param>
        /// <returns>The collection of matching heroes or empty if none.</returns>
        IEnumerable<Hero> SearchByName(string nameToMatch);


        /// <summary>
        /// Searches for heroes matching a name.
        /// </summary>
        /// <param name="nameToMatch">The name to match.</param>
        /// <returns>The collection of matching heroes or empty if none.</returns>
        Task<IEnumerable<Hero>> SearchByNameAsync(string nameToMatch);


        /// <summary>
        /// Adds a hero.
        /// </summary>
        /// <param name="heroToAdd">The hero to add.</param>
        void Add(Hero heroToAdd);


        /// <summary>
        /// Adds a hero.
        /// </summary>
        /// <param name="heroToAdd">The hero to add.</param>
        Task AddAsync(Hero heroToAdd);


        /// <summary>
        /// Removes a hero.
        /// </summary>
        /// <param name="heroToRemove">The hero to remove.</param>
        void Remove(Hero heroToRemove);


        /// <summary>
        /// Removes a hero by her ID.
        /// </summary>
        /// <param name="id">The ID to look up.</param>
        void RemoveById(long id);

    }
}

using System;
using System.Threading.Tasks;


namespace Api.Data
{

    /// <summary>
    /// Unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {

        /// <summary>
        /// Gets the hero repository.
        /// </summary>
        IHeroRepository HeroRepository
        {
            get;
        }

        
        /// <summary>
        /// Saves the work.
        /// </summary>
        void Save();


        /// <summary>
        /// Saves the work.
        /// </summary>
        Task SaveAsync();

    }

}

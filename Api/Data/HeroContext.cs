using Microsoft.EntityFrameworkCore;


namespace Api.Data
{

    /// <summary>
    /// Hero database context.
    /// </summary>
    public class HeroContext : DbContext
    {

        /// <summary>
        /// Creates a new context.
        /// </summary>
        /// <param name="options">Options to use.</param>
        public HeroContext(DbContextOptions<HeroContext> options)
            : base(options)
        {
        }


        /// <summary>
        /// Gets or sets the collection of heroes.
        /// </summary>
        public DbSet<Hero> Heroes
        {
            get;

            set;
        }

    }

}

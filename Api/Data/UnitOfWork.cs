using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Api.Data
{

    public class UnitOfWork : IUnitOfWork
    {

        private readonly HeroContext _context;
        private bool _disposed = false;


        public UnitOfWork(HeroContext context)
        {
            _context = context;

            HeroRepository = new HeroRepository(context);
        }


        public IHeroRepository HeroRepository
        {
            get;
        }

            
        public void Save()
        {
            _context.SaveChanges();
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
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

using Domain.Entities;
using Infrastructure.Repositories;
using Infrastructure.Database;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IGenericRepository<Blog> Blogs { get; }
        public IGenericRepository<Category> Categories { get; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Blogs = new GenericRepository<Blog>(dbContext);
            Categories = new GenericRepository<Category>(dbContext);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}

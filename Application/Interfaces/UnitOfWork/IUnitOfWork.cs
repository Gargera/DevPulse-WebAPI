using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<Blog> Blogs { get; }
        public IGenericRepository<Category> Categories { get; }
        public Task<int> SaveChangesAsync();
    }
}

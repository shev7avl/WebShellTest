using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebShell.Data.Context;
using WebShell.Domain;

namespace WebShell.Data.Repository
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _dataContext;

        public EFRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(T entity)
        {
            await _dataContext.Set<T>().AddAsync(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<T> GetById(int id)
        {
            var entity = await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public Task<T> Update(T entity)
        {
            throw new System.NotImplementedException();
        }
        public Task DeleteById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}

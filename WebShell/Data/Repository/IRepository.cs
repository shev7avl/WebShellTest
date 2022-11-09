using System.Threading.Tasks;
using WebShell.Domain;

namespace WebShell.Data.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int id);
        Task Create(T entity);
        Task<T> Update(T entity);
        Task DeleteById(int id);
    }
}

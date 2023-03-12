using SalesProject.Domain.Entities.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesProject.Domain.Interfaces.Service
{
    public interface ICachingService
    {
        Task<T> GetAsync<T>(string key) where T : Model;
        Task<IEnumerable<T>> GetListAsync<T>(string key) where T : Model;
        Task SetAsync<T>(string key, T model, int absoluteExpiration, int slidingExpiration) where T : Model;
        Task SetListAsync<T>(string key, IEnumerable<T> models, int absoluteExpiration, int slidingExpiration) where T : Model;
        Task DeleteAsync(string key);
    }
}
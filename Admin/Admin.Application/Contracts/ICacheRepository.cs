using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Application.Contracts
{
    public interface ICacheRepository
    {
        Task SetAsync<T>(string key, T value);
    }
}

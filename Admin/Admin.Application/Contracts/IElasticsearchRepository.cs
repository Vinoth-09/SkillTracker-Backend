using Admin.Application.Features.Queries.SearchQuery;
using Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Application.Contracts
{
    public interface IElasticsearchRepository
    {
        Task SaveSingleAsync(ESDocument document);

        Task<IReadOnlyCollection<ESDocument>> SearchAsync(string query);
    }
}

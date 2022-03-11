using Admin.Application.Features.Queries.SearchQuery;
using Admin.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Application.Contracts
{
    public interface ISearchService
    {
        Task<IEnumerable<Profile>> Search(SearchProfileQuery query);
    }
}

using Admin.Application.Contracts;
using Admin.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Admin.Application.Features.Queries.SearchQuery
{
    public class SearchProfileQueryHandler : IRequestHandler<SearchProfileQuery, IEnumerable<Profile>>
    {
        private readonly ILogger<SearchProfileQueryHandler> _logger;
        private readonly ISearchService _searchService;

        public SearchProfileQueryHandler(
            ILogger<SearchProfileQueryHandler> logger,
            ISearchService searchService
        )
        {
            _logger = logger;
            _searchService = searchService;
        }

        public async Task<IEnumerable<Profile>> Handle(SearchProfileQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Search with EmpId={request.EmpId} Name={request.Name} Skill={request.Skill}");
            return await _searchService.Search(request);
        }

    }
}

using Admin.Application.Contracts;
using Admin.Application.Features.Queries.SearchQuery;
using Admin.Domain.Entities;
using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Infrastructure.ESCache
{
    public class ElasticsearchRepository : IElasticsearchRepository
    {
        private readonly IElasticClient _client;

        public ElasticsearchRepository(IElasticClient client)
        {
            _client = client;
        }

        public async Task SaveSingleAsync(ESDocument document)
        {
            await _client.IndexDocumentAsync(document);
        }

        public async Task<IReadOnlyCollection<ESDocument>> SearchAsync(string query)
        {
            var queryResult = await _client.SearchAsync<ESDocument>(
                s => s.Query(q => q.QueryString(d => d.Query(query)))
            );
            if (!queryResult.IsValid)
            {
                return null;

                // _logger.LogError("Failed to search documents");
            } else
            {
                return queryResult.Documents;
            }
            
        }
    }
}

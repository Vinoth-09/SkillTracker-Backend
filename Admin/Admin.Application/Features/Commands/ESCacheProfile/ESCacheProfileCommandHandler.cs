using Admin.Application.Contracts;
using Admin.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Admin.Application.Features.Commands.ESCacheProfile
{
    //public class ESCacheProfileCommandHandler : IRequestHandler<ESCacheProfileCommand, string>
    //{
    //    private readonly IElasticsearchRepository _repository;
    //    private readonly IMapper _mapper;
    //    private readonly ILogger<ESCacheProfileCommandHandler> _logger;

    //    public ESCacheProfileCommandHandler(IElasticsearchRepository repository, IMapper mapper, ILogger<ESCacheProfileCommandHandler> logger)
    //    {
    //        _repository = repository;
    //        _mapper = mapper;
    //        _logger = logger;
    //    }

    //    public async Task<string> Handle(ESCacheProfileCommand request, CancellationToken cancellationToken)
    //    {
    //        var document = new ESDocument
    //        {
    //            EmpId = request.EmpId,
    //            Name = request.Name,
    //            Skills = request.Skills.Select(s => s.Name).ToList()
    //        };

    //        await _repository.SaveSingleAsync(document);
    //        _logger.LogInformation($"Document {document} is successfully cached in Elasticsearch.");

    //        return request.EmpId;
    //    }
    //}
}

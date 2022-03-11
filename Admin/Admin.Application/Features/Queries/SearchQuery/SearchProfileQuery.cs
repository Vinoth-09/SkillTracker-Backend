using Admin.Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Admin.Application.Features.Queries.SearchQuery
{
    public class SearchProfileQuery: IRequest<IEnumerable<Profile>>
    {
        public string EmpId { get; set; }

        public string Name { get; set; }

        public string Skill { get; set; }
    }
}

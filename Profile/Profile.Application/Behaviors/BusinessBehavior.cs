using MediatR;
using Profile.Application.Features.Commands.AddProfile;
using Profile.Application.Features.Commands.UpdateProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Profile.Application.Behaviors
{
    public class BusinessBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is AddProfileCommand)
            {
                var commnd = request as AddProfileCommand;
                if (!commnd.EmpId.ToUpper().StartsWith("CTS"))
                {
                    commnd.EmpId = "CTS" + commnd.EmpId;
                }
            }
            if (request is UpdateProfileCommand)
            {
                var commnd = request as UpdateProfileCommand;
                if (!commnd.EmpId.ToUpper().StartsWith("CTS"))
                {
                    commnd.EmpId = "CTS" + commnd.EmpId;
                }
            }
            return await next();
        }
    }
}

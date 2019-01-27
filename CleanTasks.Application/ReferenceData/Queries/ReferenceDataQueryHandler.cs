using CleanTasks.Application.ReferenceData.Models;
using CleanTasks.Domain.Enums;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTasks.Application.ReferenceData.Queries
{
    public class ReferenceDataQueryHandler : IRequestHandler<ReferenceDataQuery, ReferenceDataDto>
    {
        public Task<ReferenceDataDto> Handle(ReferenceDataQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ReferenceDataDto()
            {
                Reasons = ((TodoReasons[])Enum.GetValues(typeof(TodoReasons))).Select(_ => new IdNameDto
                {
                    Id = (int)_,
                    Name = _.ToString()
                }),
                Statuses = ((TodoStatuses[])Enum.GetValues(typeof(TodoStatuses))).Select(_ => new IdNameDto
                {
                    Id = (int)_,
                    Name = _.ToString()
                }),
                Types = ((TodoTypes[])Enum.GetValues(typeof(TodoTypes))).Select(_ => new IdNameDto
                {
                    Id = (int)_,
                    Name = _.ToString()
                })
            });
        }
    }
}

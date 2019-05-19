using TodoTasks.Application.ReferenceData.Models;
using TodoTasks.Common;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TodoTasks.Application.ReferenceData.Queries
{
    public class ReferenceDataQueryHandler : IRequestHandler<ReferenceDataQuery, ReferenceDataDto>
    {
        public Task<ReferenceDataDto> Handle(ReferenceDataQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ReferenceDataDto()
            {
                Reasons = ((TodoReason[])Enum.GetValues(typeof(TodoReason))).Select(_ => new IdNameDto
                {
                    Id = (int)_,
                    Name = _.ToString()
                }),
                Statuses = ((TodoStatus[])Enum.GetValues(typeof(TodoStatus))).Select(_ => new IdNameDto
                {
                    Id = (int)_,
                    Name = _.ToString()
                }),
                Types = ((TodoType[])Enum.GetValues(typeof(TodoType))).Select(_ => new IdNameDto
                {
                    Id = (int)_,
                    Name = _.ToString()
                })
            });
        }
    }
}

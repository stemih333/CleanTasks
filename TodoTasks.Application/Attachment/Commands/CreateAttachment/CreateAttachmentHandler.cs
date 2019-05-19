﻿using TodoTasks.Application.Attachment.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TodoTasks.Application.Attachment.Commands.CreateAttachment
{
    public class CreateAttachmentHandler : IRequestHandler<CreateAttachmentCommand, AttachmentDto>
    {
        public Task<AttachmentDto> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new AttachmentDto());
        }
    }
}

using FluentValidation;

namespace TodoTasks.Application.Attachment.Commands
{
    public class DeleteAttachmentValidation : AbstractValidator<DeleteAttachmentCommand>
    {
        public DeleteAttachmentValidation()
        {
            RuleFor(_ => _.AttachmentId).NotEmpty();
        }
    }
}

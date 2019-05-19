using FluentValidation;

namespace TodoTasks.Application.Attachment.Commands.CreateAttachment
{
    public class CreateAttachmentValidation : AbstractValidator<CreateAttachmentCommand>
    {
        public CreateAttachmentValidation()
        {
        }
    }
}

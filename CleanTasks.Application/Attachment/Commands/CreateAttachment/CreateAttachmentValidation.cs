using FluentValidation;

namespace CleanTasks.Application.Attachment.Commands.CreateAttachment
{
    public class CreateAttachmentValidation : AbstractValidator<CreateAttachmentCommand>
    {
        public CreateAttachmentValidation()
        {
        }
    }
}

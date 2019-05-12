using FluentValidation;

namespace CleanTodoTasks.Application.Attachment.Commands.CreateAttachment
{
    public class CreateAttachmentValidation : AbstractValidator<CreateAttachmentCommand>
    {
        public CreateAttachmentValidation()
        {
        }
    }
}

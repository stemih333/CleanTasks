using FluentValidation;

namespace CleanTasks.Application.Attachment.Commands.CreateAttachment
{
    public class CreateCommentValidation : AbstractValidator<CreateAttachmentCommand>
    {
        public CreateCommentValidation()
        {
            RuleFor(_ => _.File).NotEmpty();
        }
    }
}

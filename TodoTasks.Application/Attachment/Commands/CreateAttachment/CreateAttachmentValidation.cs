using FluentValidation;

namespace TodoTasks.Application.Attachment.Commands
{
    public class CreateAttachmentValidation : AbstractValidator<CreateAttachmentCommand>
    {
        public CreateAttachmentValidation()
        {
            RuleFor(_ => _.FileStream).NotEmpty().Must(_ => _.Length > 0).WithMessage("Cannot upload an empty file.");
            RuleFor(_ => _.FileName).NotEmpty().MaximumLength(50);
            RuleFor(_ => _.FileType).NotEmpty().MaximumLength(50);
            RuleFor(_ => _.FileSize).NotEmpty();
            RuleFor(_ => _.UserId).NotEmpty().MaximumLength(50);
            RuleFor(_ => _.TodoId).NotEmpty();
        }
    }
}

using FluentValidation;

namespace TodoTasks.Application.Attachment.Queries
{
    public class GetSingleAttachmentValidation : AbstractValidator<GetSingleAttachmentQuery>
    {
        public GetSingleAttachmentValidation()
        {
            RuleFor(_ => _.AttachmentId).NotEmpty();
        }
    }
}

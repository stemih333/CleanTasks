using FluentValidation;

namespace TodoTasks.Application.Attachment.Queries
{
    public class SearchAttachmentsQueryValidation : AbstractValidator<SearchAttachmentsQuery>
    {
        public SearchAttachmentsQueryValidation()
        {
            RuleFor(_ => _.TodoId).NotEmpty();
        }
    }
}
